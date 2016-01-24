# VocaDbLyricsLib
This is a library I wrote in VB.NET to make it easy for me to retreive lyrics for songs from VocaDB (and sites based on the same code like UtaiteDB).
At first it was extremely basic, but it quickly expanded to have much more functionality.

The library supports finding songs by name or id and returns results in multiple languages where available.
I don't think there's any major issues with it, but I've been wrong about many things before. If something doesn't work or could be done better, feel free to create an issue or put in a pull request.

My tests are just included because they were there. They check some basic functionality, but shouldn't be considered a full testing suite.

&nbsp;

### Usage notes:
Usage is simple, just create a new `VocaDbLyricsLib` and run `YourLyricsLibInstance.GetLyricsFromName` or `YourLyricsLibInstance.GetLyricsId`.
Documentation on `LyricsResult`s and `LyricsContainer`s is currently unavailable, but it shouldn't be too hard to figure them out from the code.
For configuration options, just change around the public variables of `YourLyricsLibInstance`. Most of it should be explained fairly well by the summaries in the code/your IDE.

Under any circumstances I can think of, the library should return a valid `LyricsResult` with its `LyricsContainers` property initialised.
This means that error checking shouldn't really be necessary for a developer using the library (just look at the demo code to see what I mean).
The library doesn't implement any exceptions (look, I'm not a professional developer, but I didn't really find them necessary), but instead utilises enumerations of error/warning types which can be checked to see what went wrong. Note that a warning won't stop lyrics from being returned - they just provide some additional information that may be useful).

&nbsp;

### Legal info:
NetNerd is not affiliated in any way with VocaDB. This library is completely unofficial and is not to be considered in any way official.

VocaDbLyricsLib comes with no warranty or guarantees. It probably won't break anything, but I won't promise you anything. Use is at your own risk.

&nbsp;

### Licensing:
VocaDbLyricsLib is licensed under the LGPL v3.

Source code can be found at https://github.com/NetNerd/VocaDbLyricsLib.
