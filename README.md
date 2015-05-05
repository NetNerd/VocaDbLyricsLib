# VocaDbLyricsLib
This is a library I wrote in VB.NET to make it easy for me to retreive lyrics for songs from VocaDB.
At first it was extremely basic, but it quickly expanded to have much more functionality.

The library supports finding songs by name or id and returns results in multiple languages where available.
I don't think there's any major issues with it, but I've been wrong about many things before. If something doesn't work or could be done better, feel free to create an issue or put in a pull request.

My tests are just included because they were there. They check some basic functionality, but shouldn't be considered a full testing suite.



### Usage notes:
Usage is simple, just create a new VocaDbLyricsLib and run YourLyricsLibInstance.GetLyricsFromName or YourLyricsLibInstance.GetLyricsId
Documentation on LyricsResults and LyricsContainers is currently unavailable, but it shouldn't be too hard to figure them out from the code.

Under any circumstances I can think of, the library should return a valid LyricsResult with its LyricsContainers property initialised.
This means that error checking shouldn't really be necessary for a developer using the library (just look at the demo code to see what I mean).
The library doesn't implement any exceptions (look, I'm not a professional developer, but I didn't really find them necessary), but instead utilises enumerations of error/warning types which can be checked to see what went wrong. Note that a warning won't stop lyrics from being returned - they just provide some additional information that may be useful).



### Legal info:
netnerd is not affiliated in any way with VocaDB. This library is completely unofficial and is not to be considered in any way official.

VocaDbLyricsLib comes with no warranty or guarantees. It probably won't break anything, but I won't promise you anything. Use is at your own risk.



### Licensing:
Currently, I haven't decided on a license, but I wanted to get this on github already.
For now, feel free to use it (via source or a compiled dll) in your projects.
Modification is allowed, but I ask you to contribute your changes back to the main project where possible (and if possible, defining a custom user-agent would be appreciated).
