Cutting Room Floor [XRF]
=========


An extensible, moddable, and hackable linear video frame editor and pixel shader, written in C#, and powered by FFmpeg, WPF, and superhuman-amounts of caffeine.

Features
--------

- Bulk/Range/Single frame extraction.
- File/codec conversion.
- Per-frame arithmetic image blending.
- RGBAW matrix transformations.
- GDI+ pixel shaders.
- Customisable UI using XAML/Blend and MVVM bindings.
- Powered by FFmpeg.

XRF StainedGlass
----------------

XRF includes a feature named StainedGlass, a live shader composition environment, allowing new shaders to be created or existing shaders modified using XSSL, XRF Shader Sequencing Language. A stripped down version of StainedGlass, only consisiting of the editor, is built-in to XRF. The external version includes the editor, line-by-line output comparison, and effect fine-tuning GUIs.

Tech
----

XRF uses the following technologies and packages:

* C# .NET + WPF / XAML + MVVM
* [FFmpeg] - commandline media editing (frame extraction and re-interpolation)
* [MediaInfo] - media metadata library
* [WPF Sound Visualization Library] - WPF sound editing control library
* [Extended WPF Toolkit] - WPF (better) control library
* [Fluent Ribbon] - ribbon toolbar control
* [AvalonDock] - multiple document interface and window docking library

License
----

GNU GPL v3.0


Twitter
----
*shameless promotion: [@TheBoothy]*


[FFmpeg]:https://www.ffmpeg.org
[MediaInfo]:http://mediaarea.net
[WPF Sound Visualization Library]:http://wpfsvl.codeplex.com
[Extended WPF Toolkit]:http://wpftoolkit.codeplex.com
[Fluent Ribbon]:http://fluent.codeplex.com
[AvalonDock]:http://avalondock.codeplex.com
[@TheBoothy]:http://twitter.com/TheBoothy
