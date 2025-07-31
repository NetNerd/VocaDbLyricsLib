'Copyright © 2015 NetNerd


'This file is part of VocaDbLyricsLib.

'VocaDbLyricsLib is free software: you can redistribute it and/or modify
'it under the terms Of the GNU Lesser General Public License As published by
'the Free Software Foundation, either version 3 Of the License, Or
'(at your option) any later version.

'VocaDbLyricsLib Is distributed In the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty Of
'MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU Lesser General Public License For more details.

'You should have received a copy Of the GNU Lesser General Public License
'along with VocaDbLyricsLib.  If Not, see < http: //www.gnu.org/licenses/>.


Imports System.Collections.Concurrent
Imports System.Net
Imports System.Net.Http
Imports System.Runtime.InteropServices
Imports Newtonsoft.Json

<ComVisible(True)>
<Guid("D0A174A7-2C20-40EA-B797-EC3B8D199875")>
<ClassInterface(ClassInterfaceType.AutoDispatch)>
Public Class VocaDbLyricsLib
    'Public variables
    ''' <summary>The user agent to be used in any web requests.</summary>
    Public UserAgent As String = "VocaDbLyricsLib/0.6"
    Private DefaultUserAgent As String = "VocaDbLyricsLib/0.6"

    ''' <summary>Controls whether or not the default user agent of the library is appended to the provided one. Recommended if using a custom user agent. Defaults to true.</summary>
    Public AppendDefaultUserAgent As Boolean = True

    ''' <summary>The web proxy to be used. 'Nothing' (and presumably 'null') use the default system proxy.</summary>
    ''' Nothing is equivalent to not setting anything as far as the WebClient is concerned.
    Public Proxy As Net.WebProxy = Nothing

    ''' <summary>The URL of the site to get results from. This allows the library to be used for other sites based on the VocaDB code.</summary>
    Public DatabaseUrl As Uri = New Uri("https://vocadb.net/")

    ''' <summary>An array of strings used to separate the song name from additional information in the title (eg. " feat.").</summary>
    Public SongSplitStrings() As String = {" feat.", " ft.", " feat ", " ft ", "(feat", "(ft"}

    ''' <summary>An array of strings used to split the artist field into multiple strings (eg. " feat.", " and ").</summary>
    Public ArtistSplitStrings() As String = {" feat.", " ft.", " feat ", " ft ", " X ", " x ", " Ｘ ", " ｘ ", "×", "✕", "✖", "⨯", "╳", ",", ";", "+", "(", "&", " and "}

    ''' <summary>Sets how many entries the library will request from VocaDB when searching for lyrics. An OriginalSongId or lyrics being found will return lyrics without searching through more results. Not applicable when getting lyrics by ID.</summary>
    Public SearchSongs As Integer = 3

    ''' <summary>When this is true, the library will stop searching through results (see <see cref="SearchSongs" />) if it finds one with the instrumental tag. In most cases, this helps to prevent issues from finding versions with lyrics, but it may cause issues.</summary>
    Public DetectInstrumental As Boolean = True

    ''' <summary>VocaDB supports multiple orders to present results in when searching. This selects the one to use for songs. By default, FavoritedTimes is used to favour more popular songs.</summary>
    Public SongSortRule As SongSortRules = SongSortRules.FavoritedTimes

    ''' <summary>VocaDB supports multiple orders to present results in when searching. This selects the one to use for artists. By default, FollowerCount is used to favour more popular artists.</summary>
    Public ArtistSortRule As ArtistSortRules = ArtistSortRules.FollowerCount

    ''' <summary>Only return lyrics if all artists are found in the DB and match the track's artists. Normal behaviour allows lyrics to be returned when artists are not in the DB.</summary>
    Public ForceArtistMatch As Boolean = False

    ''' <summary>Use the old behavior when processing <see cref="ForceArtistMatch" />. "Only return lyrics if the (first) artist is found in the DB and matches the track's artist." Note that <see cref="ForceArtistMatch" /> must be enabled for this to have any effect.</summary>
    Public UseOldForceArtistMatch = False

    'Artist ID cache
    Private ArtistIdCache As New ConcurrentDictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)

    'Data Structures
    ''' <summary>Contains lyrics and associated information.</summary>
    Public Structure LyricsContainer
        Public Language As String
        Public TranslationType As String
        Public Lyrics As String
    End Structure

    ''' <summary>Contains a LyricsContainer, as well as any errors or warnings encountered while getting the lyrics.</summary>
    ''' <remarks>
    ''' Errors mean that no lyrics have been returned; the LyricsContainer is Nothing.
    ''' Warnings mean that lyrics were successfully retrieved, but there may be issues with them.
    ''' </remarks>
    Public Structure LyricsResult
        Public LyricsContainers() As LyricsContainer
        Public ErrorType As VocaDbLyricsError
        Public WarningType As VocaDbLyricsWarning

        Sub New(arr() As LyricsContainer)
            LyricsContainers = arr
        End Sub
    End Structure


    'Enums (errors/warnings)
    ''' <summary>Indicates the type of error that was encountered.</summary>
    Public Enum VocaDbLyricsError
        ''' <summary>No error was encountered.</summary>
        None = 0

        ''' <summary>The specified song was not found.</summary>
        NoSong = 1

        ''' <summary>No lyrics were found for the song.</summary>
        NoLyrics = 2

        ''' <summary>There was an error connecting to VocaDB.</summary>
        ConnectionError = 3

        ''' <summary>The song was detected as intrumental. Only used if <see cref="DetectInstrumental" /> is true.</summary>
        IsInstrumental = 4
    End Enum

    ''' <summary>Indicates the type of warning that was encountered.</summary>
    Public Enum VocaDbLyricsWarning
        ''' <summary>No warning was encountered.</summary>
        None = 0

        ''' <summary>None of the specified artists were found in the DB, and therefore they were ignored.</summary>
        NoArtist = 1

        ''' <summary>Lyrics were taken from the original version of the song instead of the detected cover/remix.</summary>
        UsedOriginal = 2

        ''' <summary>Some artists were found in the DB and some weren't. Only the found ones were used.</summary>
        SomeArtists = 3
    End Enum

    ''' <summary>Sorting methods for songs.</summary>
    Public Enum SongSortRules
        ''' <summary>No sorting.</summary>
        None

        ''' <summary>Sort by name.</summary>
        Name

        ''' <summary>Sort by the date of addition to the database.</summary>
        AdditionDate

        ''' <summary>Sort by the number of favorites on the song.</summary>
        FavoritedTimes

        ''' <summary>Sort by the rating of the song.</summary>
        RatingScore
    End Enum

    ''' <summary>Sorting methods for artists.</summary>
    Public Enum ArtistSortRules
        ''' <summary>No sorting.</summary>
        None

        ''' <summary>Sort by name.</summary>
        Name

        ''' <summary>Sort by the date of addition to the database (descending).</summary>
        AdditionDate

        ''' <summary>Sort by the date of addition to the database (ascending).</summary>
        AdditionDateAsc

        ''' <summary>Sort by the number of songs the artist has.</summary>
        SongCount

        ''' <summary>Sort by the average(?) rating of the artist's songs.</summary>
        SongRating

        ''' <summary>Sort by the number of followers the artist has.</summary>
        FollowerCount
    End Enum

    'Classes for JSON models
    Private Class SongListResult
        Public Items As List(Of SongContract)
    End Class

    Private Class ArtistListResult
        Public Items As List(Of ArtistContract)
    End Class

    Private Class SongContract
        Public Id As Integer
        Public Title As String
        Public Lyrics As List(Of LyricsForSongContract)
        Public Tags As List(Of TagUsageContract)
        Public OriginalVersionId As Integer?
    End Class

    Private Class ArtistContract
        Public Id As Integer
        Public Name As String
    End Class

    Private Class LyricsForSongContract
        Public CultureCode As String
        Public Value As String
        Public TranslationType As String
    End Class

    Private Class TagUsageContract
        Public Tag As TagContract
    End Class

    Private Class TagContract
        Public Name As String
    End Class

    ''' <summary>
    ''' Returns Artist ID by its name with caching.
    ''' </summary>
    ''' <param name="artistName">The name of the artist.</param>
    Private Function GetArtistId(ArtistName As String) As Integer
        If String.IsNullOrWhiteSpace(ArtistName) Then Return -1
        If ArtistIdCache.ContainsKey(ArtistName) Then Return ArtistIdCache(ArtistName)

        Try
            Dim url = $"{DatabaseUrl}api/artists?query={Uri.EscapeDataString(ArtistName)}&sort={ArtistSortRule}&nameMatchMode=Exact&maxResults=1"
            Dim json = DownloadJsonAsync(url).Result
            Dim result = JsonConvert.DeserializeObject(Of ArtistListResult)(json)

            If result?.Items IsNot Nothing AndAlso result.Items.Count > 0 Then
                Dim id = result.Items(0).Id
                ArtistIdCache(ArtistName) = id
                Return id
            End If
        Catch
            ' ignore
        End Try

        ArtistIdCache(ArtistName) = -1
        Return -1 ' not found
    End Function

    ''' <summary>
    ''' Returns the lyrics of a song on VocaDB from its name.
    ''' </summary>
    ''' <param name="Song">The name of the song.</param>
    ''' <param name="Artist">One or more artists of the song. Only the first provided artist that is found in the database will be used to find the song. (Optional)</param>
    Public Function GetLyricsFromName(Song As String, Optional Artist As String = Nothing) As LyricsResult
        Dim result As New LyricsResult({})

        If String.IsNullOrWhiteSpace(Song) Then
            result.ErrorType = VocaDbLyricsError.NoSong
            Return result
        End If

        Dim artistIds As New List(Of Integer)
        If Not String.IsNullOrWhiteSpace(Artist) Then
            Dim artists = Artist.Split(ArtistSplitStrings, StringSplitOptions.RemoveEmptyEntries).Select(Function(a) a.Trim()).ToArray()
            For Each a In artists
                Dim id = GetArtistId(a)
                artistIds.Add(id)
            Next

            If ForceArtistMatch Then
                If UseOldForceArtistMatch Then
                    If artistIds(0) = -1 Then
                        result.ErrorType = VocaDbLyricsError.NoSong
                        result.WarningType = VocaDbLyricsWarning.NoArtist
                        Return result
                    End If
                Else
                    If artistIds.Any(Function(id) id = -1) Then
                        result.ErrorType = VocaDbLyricsError.NoSong
                        result.WarningType = VocaDbLyricsWarning.NoArtist
                        Return result
                    End If
                End If
            End If
        End If

        Dim cleanSong = Song.Split(SongSplitStrings, 2, StringSplitOptions.RemoveEmptyEntries)(0).Trim()
        Dim songlist As SongListResult

        Try
            songlist = GetSongListFromName(cleanSong, artistIds)
        Catch
            result.ErrorType = VocaDbLyricsError.ConnectionError
            Return result
        End Try

        result = GetLyricsResultFromSongList(songlist)

        For i As Integer = 0 To artistIds.Count - 1              'For each ID
            If Not artistIds(i) = -1 Then                        'If in DB
                If i > 0 Then                                    'Not first iteration (first failed)
                    result.WarningType = VocaDbLyricsWarning.SomeArtists
                Else                                             'Else: Is first iteration
                    For j As Integer = 1 To artistIds.Count - 1
                        If artistIds(j) = -1 Then result.WarningType = VocaDbLyricsWarning.SomeArtists
                    Next
                End If
                Return result
            End If                                               'Getting here means no artists matched
        Next

        If Artist IsNot Nothing AndAlso Artist.Length > 0 Then
            result.WarningType = VocaDbLyricsWarning.NoArtist
        End If

        Return result
    End Function

    ''' <summary>
    ''' Returns the lyrics for a song on VocaDB from its ID.
    ''' </summary>
    ''' <param name="SongId">The ID of the song.</param>
    Public Function GetLyricsFromId(SongId As Integer) As LyricsResult
        Dim result As New LyricsResult({})
        Dim song As SongContract

        Try
            song = GetSongContractFromId(SongId)
        Catch
            result.ErrorType = VocaDbLyricsError.ConnectionError
            Return result
        End Try

        If song Is Nothing Then
            result.ErrorType = VocaDbLyricsError.NoSong
            Return result
        End If

        Dim songlist = New SongListResult()
        songlist.Items = New List(Of SongContract)
        songlist.Items.Add(song)
        result = GetLyricsResultFromSongList(songlist)
        Return result
    End Function

    ''' <summary>
    ''' Returns SongListResult containing songs on VocaDB from name search.
    ''' </summary>
    ''' <param name="Song">Name of the song (already cleaned as appropriate).</param>
    ''' <param name="ArtistIds">List of artist IDs.</param>
    Private Function GetSongListFromName(Song As String, ArtistIds As List(Of Integer)) As SongListResult
        Dim url = $"{DatabaseUrl}api/songs?query={Uri.EscapeDataString(Song)}&sort={SongSortRule}&fields=lyrics,tags&nameMatchMode=Exact&maxResults={SearchSongs}"

        For Each id In ArtistIds
            If id <> -1 Then
                url &= "&artistId%5B%5D=" & id.ToString()
            End If
        Next

        Dim json = DownloadJsonAsync(url).Result
        Return JsonConvert.DeserializeObject(Of SongListResult)(json)
    End Function

    ''' <summary>
    ''' Returns SongContract for song on VocaDB from its ID. (or Nothing if song doesn't exist)
    ''' </summary>
    ''' <param name="SongId">Song ID</param>
    Private Function GetSongContractFromId(SongId As Integer) As SongContract
        Dim url = $"{DatabaseUrl}api/songs/{SongId}?fields=lyrics,tags"
        Dim json = DownloadJsonAsync(url).Result
        Return JsonConvert.DeserializeObject(Of SongContract)(json)
    End Function

    'Note: can parse multiple SongContracts at once, to search for original version - quite useful
    Private Function GetLyricsResultFromSongList(songlist As SongListResult) As LyricsResult
        Dim result As New LyricsResult({})

        If songlist?.Items Is Nothing OrElse songlist.Items.Count = 0 Then
            result.ErrorType = VocaDbLyricsError.NoSong
            Return result
        End If

        'The fun part:
        For Each song In songlist.Items
            If song.Lyrics Is Nothing OrElse song.Lyrics.Count = 0 Then
                If DetectInstrumental = True Then
                    'We only want to return lyrics for non-instrumentals. If we detect an instrumental, we can return an error.
                    For Each tag In song.Tags
                        If tag.Tag.Name = "instrumental" Then
                            'Detecting an instrumental causes searching to stop,
                            'because there's a decent chance that continued searching will falsely return lyrics.
                            result.ErrorType = VocaDbLyricsError.IsInstrumental
                            Return result
                        End If
                    Next
                End If

                If song.OriginalVersionId IsNot Nothing Then
                    Dim origsong = songlist.Items.Find(Function(x As SongContract) x.Id = song.OriginalVersionId)

                    If origsong IsNot Nothing Then  'Original version of song is in results we already retrieved, no need for another API query
                        Dim origlist = New SongListResult()
                        origlist.Items = New List(Of SongContract)
                        origlist.Items.Add(origsong)
                        result = GetLyricsResultFromSongList(origlist)
                        If result.LyricsContainers.Count > 0 Then
                            result.WarningType = VocaDbLyricsWarning.UsedOriginal
                            Return result
                        End If
                    Else
                        result = GetLyricsFromId(song.OriginalVersionId)
                        If result.LyricsContainers.Count > 0 Then
                            result.WarningType = VocaDbLyricsWarning.UsedOriginal
                            Return result
                        End If
                    End If
                End If

            Else
                ReDim result.LyricsContainers(song.Lyrics.Count - 1)
                For i = 0 To song.Lyrics.Count - 1
                    result.LyricsContainers(i) = New LyricsContainer With {
                        .Language = song.Lyrics(i).CultureCode,
                        .TranslationType = song.Lyrics(i).TranslationType,
                        .Lyrics = song.Lyrics(i).Value.Trim(vbCr, vbLf)  'only trim newlines - some lyrics are deliberately indented
                    }
                Next
                Return result
            End If
        Next
        result.ErrorType = VocaDbLyricsError.NoLyrics
        Return result
    End Function

    Private Async Function DownloadJsonAsync(url As String) As Task(Of String)
        Using handler = New HttpClientHandler()
            If Proxy IsNot Nothing Then handler.Proxy = Proxy
            Using client As New HttpClient(handler)
                Dim finalUserAgent = If(UserAgent IsNot DefaultUserAgent AndAlso AppendDefaultUserAgent, $"{UserAgent} ({DefaultUserAgent})", UserAgent)
                client.DefaultRequestHeaders.Add("User-Agent", finalUserAgent)
                client.DefaultRequestHeaders.Add("Accept", "application/json")
                Return Await client.GetStringAsync(url)
            End Using
        End Using
    End Function

End Class

