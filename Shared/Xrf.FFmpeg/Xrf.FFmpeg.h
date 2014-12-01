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
		static VISIBLE int ExtractAllFrames(const char *szPath, const char *szDestination);
		static VISIBLE int ExtractAllScaledFrames(const char *szPath, const char *szDestination, int scaleWidth, int scaleHeight);
		static VISIBLE double GetFrameRate(const char *szPath)
	};
}