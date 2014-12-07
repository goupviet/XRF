// Xrf.FFmpeg.h

namespace Xrf
{
	extern "C"
	{
		// Extracts a version of all frames, scaled to the specified scale factor, from the specified video path into the destination directory
		__declspec(dllexport) int __stdcall ExtractAllFrames(const char *szPath, const char *szDestination, float scaleFactor = 1.0);

		// Gets the framerate from the specified video file.
		__declspec(dllexport) double _stdcall GetFrameRate(const char *szPath);
	}
}