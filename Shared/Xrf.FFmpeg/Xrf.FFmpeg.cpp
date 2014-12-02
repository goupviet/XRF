// Xrf.FFmpeg.cpp : Defines the exported functions for the DLL application.
//

# pragma comment (lib, "avcodec.lib")
# pragma comment (lib, "avformat.lib")
# pragma comment (lib, "swscale.lib")
# pragma comment (lib, "avutil.lib")

#include "stdafx.h"
#include <stdio.h>

#include <libavcodec\avcodec.h>
#include <libavformat\avformat.h>
#include <libswscale\swscale.h>
#include <libavutil\mathematics.h>

#include <boost\filesystem.hpp>

#include "Xrf.FFmpeg.h"

using namespace std;
namespace fs = boost::filesystem;

namespace Xrf
{
	/** 
		<summary>Saves an AVFrame object as a PPM image.</summary>
		<param name='*pFrame'>Pointer to the AVFrame object to export as image.</param>
		<param name='width'>The width of the image to write.</param>
		<param name='height'>The height of the image to write.</param>
		<param name='iFrame'>The index of the frame.</param>
		<param name='*szDestination'>The directory path to write the frame to.</param>
	*/
	void SaveFrame(AVFrame *pFrame, int width, int height, int iFrame, const char *szDestination)
	{
		FILE *pFile;
		char szFilename[32];
		int y;

		//Open file
		sprintf(szFilename, "frame%d.ppm", iFrame);
		const char *szPath = JoinPaths(szFilename, szDestination);

		pFile = fopen(szPath, "wb");
		if (pFile == NULL) return;

		//Write header
		fprintf(pFile, "P6\n%d %d\n255\n", width, height);

		//Write pixel data
		for (y = 0; y<height; y++)
			fwrite(pFrame->data[0] + y*pFrame->linesize[0], 1, width * 3, pFile);

		// Close file
		fclose(pFile);
	}

	/**
		<summary>Extracts and saves all frames from a given video file.</summary>
		<param name='*szPath'>The path of the file to extract frames from.</param>
		<param name='*szDestination'>The directory to save files to.</param>
	*/
	int FFmpeg::ExtractAllFrames(const char *szPath, const char *szDestination)
	{
		AVFormatContext *pFormatCtx = NULL;
		int i, videoStream;
		AVCodecContext *pCodecCtx = NULL;
		AVCodec *pCodec = NULL;
		AVFrame *pFrame = NULL;
		AVFrame *pFrameRGB = NULL;
		AVPacket packet;
		int frameFinished;
		int numBytes;
		uint8_t *buffer = NULL;

		AVDictionary *optionsDict = NULL;
		struct SwsContext *sws_ctx = NULL;

		// Register all formats and codecs
		av_register_all();

		if (avformat_open_input(&pFormatCtx, szPath, NULL, NULL) != 0) return -1; // Couldn't open file

		// Retrieve stream information
		if (avformat_find_stream_info(pFormatCtx, NULL)<0) return -1; // Couldn't find stream information

		// Dump information about file onto standard error
		av_dump_format(pFormatCtx, 0, szPath, 0);

		// Find the first video stream
		videoStream = -1;
		for (i = 0; i<pFormatCtx->nb_streams; i++)
			if (pFormatCtx->streams[i]->codec->codec_type == AVMEDIA_TYPE_VIDEO) {
				videoStream = i;
				break;
			}
		if (videoStream == -1)
			return -1; // Didn't find a video stream

		// Get a pointer to the codec context for the video stream
		pCodecCtx = pFormatCtx->streams[videoStream]->codec;

		// Find the decoder for the video stream
		pCodec = avcodec_find_decoder(pCodecCtx->codec_id);
		if (pCodec == NULL) {
			fprintf(stderr, "Unsupported codec!\n");
			return -1; // Codec not found
		}
		// Open codec
		if (avcodec_open2(pCodecCtx, pCodec, &optionsDict)<0)
			return -1; // Could not open codec
		// Allocate video frame
		pFrame = av_frame_alloc();
		// Allocate an AVFrame structure
		pFrameRGB = av_frame_alloc();
		if (pFrameRGB == NULL)
			return -1;

		// Determine required buffer size and allocate buffer
		numBytes = avpicture_get_size(PIX_FMT_RGB24, 
									  pCodecCtx->width, 
									  pCodecCtx->height);
		buffer = (uint8_t *)av_malloc(numBytes*sizeof(uint8_t));

		sws_ctx = sws_getContext(pCodecCtx->width,
								 pCodecCtx->height,
								 pCodecCtx->pix_fmt,
								 pCodecCtx->width,
								 pCodecCtx->height,
								 PIX_FMT_RGB24,
								 SWS_BILINEAR,
								 NULL,
								 NULL,
								 NULL);

		// Assign appropriate parts of buffer to image planes in pFrameRGB
		// Note that pFrameRGB is an AVFrame, but AVFrame is a superset
		// of AVPicture
		avpicture_fill((AVPicture *)pFrameRGB, 
					   buffer, 
					   PIX_FMT_RGB24, 
					   pCodecCtx->width, 
					   pCodecCtx->height);

		// Read frames and save first five frames to disk
		i = 0;
		while (av_read_frame(pFormatCtx, &packet) >= 0) {

			// Is this a packet from the video stream?
			if (packet.stream_index == videoStream) {

				// Decode video frame
				avcodec_decode_video2(pCodecCtx, pFrame, &frameFinished, &packet);

				// Did we get a video frame?
				if (frameFinished) {
					// Convert the image from its native format to RGB
					sws_scale(sws_ctx,
							  (uint8_t const * const *)pFrame->data,
							  pFrame->linesize,
							  0,
							  pCodecCtx->height,
							  pFrameRGB->data,
							  pFrameRGB->linesize);

					// Save the frame to disk
					if (++i <= 5)
						SaveFrame(pFrameRGB, 
								  pCodecCtx->width, 
								  pCodecCtx->height, 
								  i, 
								  szDestination);
				}
			}

			// Free the packet that was allocated by av_read_frame
			av_free_packet(&packet);
		}

		// Free the RGB image
		av_free(buffer);
		av_free(pFrameRGB);
		// Free the YUV frame
		av_free(pFrame);
		// Close the codec
		avcodec_close(pCodecCtx);
		// Close the video file
		avformat_close_input(&pFormatCtx);
		return 0;
	}

	/**
		<summary>Extracts, scales and saves all frames from a given video file.</summary>
		<param name='*szPath'>The path of the file to extract frames from.</param>
		<param name='*szDestination'>The directory to save files to.</param>
		<param name='scaleWidth'>The width to scale the frame to.</param>
		<param name='scaleHeight'>The height to scale the frame to.</param>
	*/
	int FFmpeg::ExtractAllScaledFrames(const char *szPath, const char *szDestination, int scaleWidth, int scaleHeight)
	{
		AVFormatContext *pFormatCtx = NULL;
		int i, videoStream;
		AVCodecContext *pCodecCtx = NULL;
		AVCodec *pCodec = NULL;
		AVFrame *pFrame = NULL;
		AVFrame *pFrameRGB = NULL;
		AVPacket packet;
		int frameFinished;
		int numBytes;
		uint8_t *buffer = NULL;

		AVDictionary *optionsDict = NULL;
		struct SwsContext *sws_ctx = NULL;

		// Register all formats and codecs
		av_register_all();

		if (avformat_open_input(&pFormatCtx, szPath, NULL, NULL) != 0) return -1; // Couldn't open file

		// Retrieve stream information
		if (avformat_find_stream_info(pFormatCtx, NULL)<0) return -1; // Couldn't find stream information

		// Dump information about file onto standard error
		av_dump_format(pFormatCtx, 0, szPath, 0);

		// Find the first video stream
		videoStream = -1;
		for (i = 0; i<pFormatCtx->nb_streams; i++)
			if (pFormatCtx->streams[i]->codec->codec_type == AVMEDIA_TYPE_VIDEO) {
				videoStream = i;
				break;
			}
		if (videoStream == -1)
			return -1; // Didn't find a video stream

		// Get a pointer to the codec context for the video stream
		pCodecCtx = pFormatCtx->streams[videoStream]->codec;

		// Find the decoder for the video stream
		pCodec = avcodec_find_decoder(pCodecCtx->codec_id);
		if (pCodec == NULL) {
			fprintf(stderr, "Unsupported codec!\n");
			return -1; // Codec not found
		}
		// Open codec
		if (avcodec_open2(pCodecCtx, pCodec, &optionsDict)<0)
			return -1; // Could not open codec
		// Allocate video frame
		pFrame = av_frame_alloc();
		// Allocate an AVFrame structure
		pFrameRGB = av_frame_alloc();
		if (pFrameRGB == NULL)
			return -1;

		// Determine required buffer size and allocate buffer
		numBytes = avpicture_get_size(PIX_FMT_RGB24,
			scaleWidth,
			scaleHeight);
		buffer = (uint8_t *)av_malloc(numBytes*sizeof(uint8_t));

		sws_ctx = sws_getContext(pCodecCtx->width,
								 pCodecCtx->height,
								 pCodecCtx->pix_fmt,
								 scaleWidth,
								 scaleHeight,
								 PIX_FMT_RGB24,
								 SWS_BILINEAR,
								 NULL,
								 NULL,
								 NULL);

		// Assign appropriate parts of buffer to image planes in pFrameRGB
		// Note that pFrameRGB is an AVFrame, but AVFrame is a superset
		// of AVPicture
		avpicture_fill((AVPicture *)pFrameRGB,
					   buffer,
					   PIX_FMT_RGB24,
					   scaleWidth,
					   scaleHeight);

		// Read frames and save first five frames to disk
		i = 0;
		while (av_read_frame(pFormatCtx, &packet) >= 0) {

			// Is this a packet from the video stream?
			if (packet.stream_index == videoStream) {

				// Decode video frame
				avcodec_decode_video2(pCodecCtx, pFrame, &frameFinished, &packet);

				// Did we get a video frame?
				if (frameFinished) {
					// Convert the image from its native format to RGB
					sws_scale(sws_ctx,
							  (uint8_t const * const *)pFrame->data,
							  pFrame->linesize,
							  0,
							  pCodecCtx->height,
							  pFrameRGB->data,
							  pFrameRGB->linesize);

					// Save the frame to disk
					if (++i <= 5) SaveFrame(pFrameRGB,
											scaleWidth,
											scaleHeight,
											i, szDestination);
				}
			}

			// Free the packet that was allocated by av_read_frame
			av_free_packet(&packet);
		}
		
		av_free(buffer); // Free the RGB image
		av_free(pFrameRGB);
		av_free(pFrame); // Free the YUV frame
		avcodec_close(pCodecCtx); // Close the codec
		avformat_close_input(&pFormatCtx); // Close the video file

		return 0;
	}

	/**
		<summary>Calculates framerate from a video file.</summary>
		<param name='*szPath'>The video file path.</param>
	*/
	double FFmpeg::GetFrameRate(const char *szPath)
	{

	}

	/**
	<summary>Extracts a single frame from a video file.</summary>
	<param name='frameIndex'>The position of the frame to extract.</param>
	<param name='*szPath'>The video file path.</param>
	<param name='*szDestination'>The directory to extract the frame to.</param>
	*/
	int FFmpeg::ExtractSpecificFrame(int frameIndex, const char *szPath, const char *szDestination)
	{

	}

	/**
		<summary>Joins a filename and a directory into one path.</summary>
		<param name='*szPath'>The filename.</param>
		<param name='*szDir'>The directory to join the filename to.</param>
	*/
	const char* JoinPaths(const char *szPath, const char *szDir)
	{
		fs::path dir(szDir);
		fs::path file(szPath);
		fs::path full_path = dir / file;
		return full_path.string().c_str();
	}
}
