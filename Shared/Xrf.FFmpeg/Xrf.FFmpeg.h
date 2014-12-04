// Xrf.FFmpeg.h

#ifdef XRFFFMPEG_EXPORTS
#define VISIBLE __declspec(dllexport)
#else
#define VISIBLE __declspec(dllimport)
#endif

namespace Xrf
{
	class FFmpeg
	{
	public:
		// Extracts a version of all frames, scaled to the specified scale factor, from the specified video path into the destination directory
		static VISIBLE int ExtractAllFrames(const char *szPath, const char *szDestination, float scaleFactor = 1.0);

		// Gets the framerate from the specified video file.
		static VISIBLE double GetFrameRate(const char *szPath);
	};
}