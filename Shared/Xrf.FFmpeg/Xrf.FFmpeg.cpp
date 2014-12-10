// Xrf.FFmpeg.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#pragma comment (lib, "avcodec.lib")
#pragma comment (lib, "avformat.lib")
#pragma comment (lib, "avutil.lib")
#pragma comment (lib, "swscale.lib")

extern "C"
{
	#include <libavcodec\avcodec.h>
	#include <libavformat\avformat.h>
	#include <libavutil\avutil.h>
	#include <libswscale\swscale.h>
}

#include <string>
#include "Xrf.FFmpeg.hpp"

namespace Xrf
{

#pragma region Test Functions

	void second_string_test(const std::string& sTest)
	{
		std::string rebuffer = sTest;
		fprintf(stdout, "rebuffer is %s\n", rebuffer);
		rebuffer.append("\\");
		rebuffer.append("test string" + std::to_string(69));
		fprintf(stdout, "rebuffer is %s\n", rebuffer);
		fprintf(stdout, "rebuffer.c_str() is %s\n", rebuffer.c_str());
		fprintf(stdout, "string test complete.");
	}

	void __stdcall test_string_cast(const char* cTest)
	{
		fprintf(stdout, "cTest is %s\n", cTest);
		std::string strTest = cTest;
		fprintf(stdout, "After buffering to strTest: %s\nPassing to next function.\n", strTest);
		second_string_test(strTest);
	}

#pragma endregion

#pragma region Private Functions

	/** 
		<summary>Saves an AVFrame object as a PPM image.</summary>
		<param name='*pFrame'>Pointer to the AVFrame object to export as image.</param>
		<param name='iFrame'>The index of the frame.</param>
		<param name='*szDestination'>The directory path to write the frame to.</param>
	*/
	void save_frame(AVFrame* pFrame, int iFrame, const std::string& destination)
	{
		//Create filename
		std::string buffer = destination;
		buffer.append("\\");
		buffer.append("frame" + std::to_string(iFrame) + ".ppm\0");

		fprintf(stdout, "framepath: %s\n", buffer);

		//Open file
		FILE* pFile = fopen(buffer.c_str(), "wb");

		if (pFile == NULL)
		{
			return;
		}

		//Write header
		int width = pFrame->width;
		int height = pFrame->height;

		fprintf(pFile, "P6\n%d %d\n255\n", width, height);

		//Write pixel data
		for (int y = 0; y < height; y++)
		{
			fwrite(pFrame->data[0] + y * pFrame->linesize[0], 1, width * 3, pFile);
		}

		// Close file
		fclose(pFile);
		fprintf(stdout, "done\n");
	}

#pragma endregion

#pragma region Exported Functions

		/**
			<summary>Extracts, (optionally) scales, and saves all frames from a given video file.</summary>
			<param name='*szPath'>The path of the file to extract frames from.</param>
			<param name='*szDestination'>The directory to save files to.</param>
			<param name='scaleFactor'>The frame scale factor.</param>
			*/
		int __stdcall extract_all_frames(const char* szPath, const char* szDestination, float scaleFactor)
		{
			std::string sDestination = szDestination;
			fprintf(stdout, "cast destination to string\n");

			// Check if scaleFactor is valid
			if ((scaleFactor != 0.f) && 
				(scaleFactor > 3.f))
			{
				fprintf(stderr, "Xrf: Scale factor '%f' out of bounds!\nMust be greater than 0, and less then or equal to 3.0.\n", scaleFactor);
				return -1;
			}
			fprintf(stdout, "completed sf check, sf is %s", std::to_string(scaleFactor));

			// Register all formats and codecs
			av_register_all();
			fprintf(stdout, "av_register_all()\n");

			//FAILS HERE WTF
			AVFormatContext* pFormatCtx = avformat_alloc_context();
			if (avformat_open_input(&pFormatCtx, szPath, nullptr, nullptr) != 0)
			{
				fprintf(stderr, "libavformat: Couldn't open file '%s'!\n", szPath);
				return -1;
			}
			fprintf(stdout, "avformat_open_input()\n");

			// Retrieve stream information
			if (avformat_find_stream_info(pFormatCtx, nullptr) < 0)
			{
				fprintf(stderr, "libavformat: Unable to find stream information!\n");
				return -1;
			}
			fprintf(stdout, "avformat_find_stream_info()\n");

			// Dump information about file onto standard error
			av_dump_format(pFormatCtx, 0, szPath, 0);

			fprintf(stdout, "attempting video stream search\n");
			// Find the first video stream
			size_t i;
			int videoStream = -1;
			for (i = 0; i < pFormatCtx->nb_streams; i++)
			{
				if (pFormatCtx->streams[i]->codec->codec_type == AVMEDIA_TYPE_VIDEO)
				{
					videoStream = i;
					break;
				}
			}
			if (videoStream == -1)
			{
				fprintf(stderr, "libavformat: No video stream found!\n");
				return -1;
			}
			fprintf(stdout, "stream found\n");

			// Get a pointer to the codec context for the video stream
			AVCodecContext* pCodecCtx = pFormatCtx->streams[videoStream]->codec;
			fprintf(stdout, "codec context\n");

			// Scale the frame
			int scaleHeight = static_cast<int>(floor(pCodecCtx->height * scaleFactor));
			int scaleWidth = static_cast<int>(floor(pCodecCtx->width * scaleFactor));

			fprintf(stdout, "frame scaled: %s x %s\n", std::to_string(scaleWidth), std::to_string(scaleHeight));

			//Check if frame sizes are valid (not 0, because that's dumb)
			if (scaleHeight == 0 || scaleWidth == 0)
			{
				fprintf(stderr, "Xrf: Scale factor caused a zero value in either width or height!\n");
				return -1;
			}

			// Find the decoder for the video stream
			AVCodec* pCodec = avcodec_find_decoder(pCodecCtx->codec_id);
			if (pCodec == NULL)
			{
				fprintf(stderr, "libavcodec: Unsupported codec!\n");
				return -1; // Codec not found
			}
			fprintf(stdout, "there's that decoder!\n");

			// Open codec
			AVDictionary* optionsDict = nullptr;
			if (avcodec_open2(pCodecCtx, pCodec, &optionsDict) < 0)
			{
				fprintf(stderr, "libavcodec: Couldn't open codec '%s'!\n", pCodec->long_name);
				return -1;
			}
			fprintf(stdout, "the chamber of codecs has been opend. enemies of the heir, beware.\n");

			// Allocate video frame
			AVFrame* pFrame = av_frame_alloc();
			// Allocate an AVFrame structure
			AVFrame* pFrameRGB = av_frame_alloc();

			fprintf(stdout, "frame allocations\n");

			if (pFrameRGB == NULL)
			{
				fprintf(stderr, "libavformat: Unable to allocate a YUV->RGB resampling AVFrame!\n");
				return -1;
			}

			// Determine required buffer size and allocate buffer
			int numBytes = avpicture_get_size(PIX_FMT_RGB24, scaleWidth, scaleHeight);
			uint8_t* buffer = static_cast <uint8_t *> (av_malloc(numBytes * sizeof(uint8_t)));

			fprintf(stdout, "oh bby create dat buffer mmmmmmMMMMMM\n");

			struct SwsContext* sws_ctx = sws_getContext(pCodecCtx->width,
				pCodecCtx->height,
				pCodecCtx->pix_fmt,
				scaleWidth,
				scaleHeight,
				PIX_FMT_RGB24,
				SWS_BILINEAR,
				nullptr, nullptr, nullptr);
			fprintf(stdout, "sws_ctx\n");

			// Assign appropriate parts of buffer to image planes in pFrameRGB
			// Note that pFrameRGB is an AVFrame, but AVFrame is a superset
			// of AVPicture
			avpicture_fill(reinterpret_cast <AVPicture *> (pFrameRGB),
				buffer,
				PIX_FMT_RGB24,
				scaleWidth,
				scaleHeight);
			fprintf(stdout, "avpicture_fill\n");

			AVPacket packet;
			int frameFinished;
			while (av_read_frame(pFormatCtx, &packet) >= 0)
			{
				// Is this a packet from the video stream?
				if (packet.stream_index == videoStream)
				{
					// Decode video frame
					avcodec_decode_video2(pCodecCtx, pFrame, &frameFinished, &packet);
					fprintf(stdout, "frame decode\n");

					// Did we get a video frame?
					if (frameFinished)
					{
						// Convert the image from its native format to RGB
						sws_scale(sws_ctx,
							static_cast <uint8_t const * const *> (pFrame->data),
							pFrame->linesize,
							0,
							pCodecCtx->height,
							pFrameRGB->data,
							pFrameRGB->linesize);
						fprintf(stdout, "rescale\n");

						// Save the frame to disk
						save_frame(pFrameRGB, i, sDestination);
						fprintf(stdout, "saved\n");
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
		double __stdcall get_frame_rate(const char* szPath)
		{
			return 0;
		}

#pragma endregion
}
