// XRFUtils.h

#ifdef XRF_EXPORTS
	#define XRF_API __declspec(dllexport) 
#else
	#define XRF_API __declspec(dllimport) 
#endif

#include <string>

namespace XrfUtils
{
	// This class is exported from the XrfUtils.dll
	class XrfUtilsFunctions
	{
	public:
		// Extracts frames
		static XRF_API void ExtractFrames(std::string inputpath, std::string outputpath);
	};
}