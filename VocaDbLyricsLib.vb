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


Imports System.Runtime.InteropServices

<ComVisible(True)>
<Guid("D0A174A7-2C20-40EA-B797-EC3B8D199875")>
<ClassInterface(ClassInterfaceType.AutoDispatch)>
Public Class VocaDbLyricsLib
    'Public variables
    ''' <summary>The user agent to be used in any web requests.</summary>
    Public UserAgent As String = "VocaDbLyricsLib/0.4"
    Private DefaultUserAgent As String = "VocaDbLyricsLib/0.4"

    ''' <summary>Controls whether or not the default user agent of the library is appended to the provided one. Recommended if using a custom user agent. Defaults to true.</summary>
    Public AppendDefaultUserAgent As Boolean = True

    ''' <summary>The web proxy to be used. 'Nothing' (and presumably 'null') use the default system proxy.</summary>
    ''' Nothing is equivalent to not setting anything as far as the WebClient is concerned.
    Public Proxy As Net.WebProxy = Nothing

    ''' <summary>The URL of the site to get results from. This allows the library to be used for other sites based on the VocaDB code.</summary>
    Public DatabaseUrl As Uri = New Uri("http://vocadb.net/")

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

    ''' <summary>Use the old behavior when processing <see cref="ForceArtistMatch" />. "Only return lyrics if the (first) artist is found in the DB and matches the track's artist." Nolte that <see cref="ForceArtistMatch" /> must be enabled for this to have any effect.</summary>
    Public UseOldForceArtistMatch = False

    'Data Structures
    ''' <summary>Contains lyrics and associated information.</summary>
    Public Structure LyricsContainer
        Public Language As String
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

        Sub New(LyricsContainerArray() As LyricsContainer)
            LyricsContainers = LyricsContainerArray
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

    ''' <summary>
    ''' Returns the lyrics of a song on VocaDB from its name.
    ''' </summary>
    ''' <param name="Song">The name of the song.</param>
    ''' <param name="Artist">One or more artists of the song. Only the first provided artist that is found in the database will be used to find the song. (Optional)</param>
    Public Function GetLyricsFromName(Song As String, Optional Artist As String = Nothing) As LyricsResult
        Dim LyricsResult As New LyricsResult({})
        'Dim ArtistId As Integer = -1
        Dim ArtistIds() As Integer = {-1}

        If Song Is Nothing OrElse Song.Length = 0 Then
            LyricsResult.ErrorType = VocaDbLyricsError.NoSong
            Return LyricsResult
        End If

        Song = Song.Split(SongSplitStrings, 2, StringSplitOptions.RemoveEmptyEntries)(0).Trim()

        If Artist IsNot Nothing Then
            Dim Artists() As String
            Artists = Artist.Split(ArtistSplitStrings, 2, StringSplitOptions.RemoveEmptyEntries)

            ReDim ArtistIds(Artists.Length - 1)
            For i As Integer = 0 To Artists.Length - 1
                ArtistIds(i) = -1
            Next

            Dim ArtistIdDelegates(Artists.Length - 1) As GetArtistIdDelegate
            Dim ThreadAsyncResults(Artists.Length - 1) As IAsyncResult

            For i As Integer = 0 To Artists.Length - 1
                Artists(i) = Artists(i).Trim()
                ArtistIdDelegates(i) = New GetArtistIdDelegate(AddressOf GetArtistId)
                ThreadAsyncResults(i) = ArtistIdDelegates(i).BeginInvoke(Artists(i), Nothing, Nothing)
            Next


            For i As Integer = 0 To Artists.Length - 1
                ArtistIds(i) = ArtistIdDelegates(i).EndInvoke(ThreadAsyncResults(i))
            Next
        End If

        ''This could definitely be done better, but meh.
        If ForceArtistMatch = True Then
            If UseOldForceArtistMatch = True Then
                If ArtistIds(0) = -1 Then
                    LyricsResult.WarningType = VocaDbLyricsWarning.NoArtist
                    LyricsResult.ErrorType = VocaDbLyricsError.NoSong
                    Return LyricsResult
                End If
            Else
                For Each Id As Integer In ArtistIds
                    If Id = -1 Then
                        LyricsResult.WarningType = VocaDbLyricsWarning.NoArtist
                        LyricsResult.ErrorType = VocaDbLyricsError.NoSong
                        Return LyricsResult
                    End If
                Next
            End If
        End If


        LyricsResult = GetLyricsResultFromXml(GetSongFromName(Song, ArtistIds))

        For i As Integer = 0 To ArtistIds.Length - 1             'For each ID
            If Not ArtistIds(i) = -1 Then                        'If in DB
                If i > 0 Then                                    'Not first iteration (first failed)
                    LyricsResult.WarningType = VocaDbLyricsWarning.SomeArtists
                Else                                             'Else: Is first iteration
                    For j As Integer = 1 To ArtistIds.Length - 1
                        If ArtistIds(j) = -1 Then LyricsResult.WarningType = VocaDbLyricsWarning.SomeArtists
                    Next
                End If

                Return LyricsResult
            End If                                               'Getting here means no artists matched
        Next

        If Artist IsNot Nothing AndAlso Artist.Length > 0 Then
            LyricsResult.WarningType = VocaDbLyricsWarning.NoArtist
        End If

        Return LyricsResult
    End Function

    ''' <summary>
    ''' Returns the lyrics for a song on VocaDB from its ID.
    ''' </summary>
    ''' <param name="SongId">The ID of the song.</param>
    Public Function GetLyricsFromId(SongId As Integer) As LyricsResult
        Return GetLyricsResultFromXml(GetSongFromId(SongId))
    End Function


    Private Function GetArtistId(Artist As String) As Integer
        Dim Xml As New Xml.XmlDocument

        Try
            Xml.LoadXml(DownloadXml(DatabaseUrl.AbsoluteUri & "api/artists?query=" & Uri.EscapeDataString(Artist) & "&sort=" & ArtistSortRule.ToString & "&maxResults=1&nameMatchMode=exact&maxResults=1"))
        Catch
            Return -1
        End Try

        If Xml.GetElementsByTagName("Id").Count > 0 Then
            Return CInt(Xml.GetElementsByTagName("Id")(0).InnerText)
        Else
            Return -1
        End If
    End Function

    Private Delegate Function GetArtistIdDelegate(Artist As String) As Integer

    Private Function GetSongFromName(Song As String, Optional ArtistIds() As Integer = Nothing) As Xml.XmlDocument
        Dim Xml As New Xml.XmlDocument

        Dim IdStr As String = ""

        For Each Id In ArtistIds
            If Not Id = -1 Then IdStr += ("&artistId=" + Id.ToString)
        Next

        Try
            Xml.LoadXml(DownloadXml(DatabaseUrl.AbsoluteUri & "api/songs?query=" & Uri.EscapeDataString(Song) & IdStr & "&sort=" & SongSortRule.ToString & "&fields=lyrics,tags&nameMatchMode=exact&maxResults=" & SearchSongs))

            'I'm good for testing getting the original version from within the XML. I haven't found a real-world scenario for this yet.
            'Xml.LoadXml(DownloadXml("http://vocadb.net/api/songs?query=twitter&artistId=130&fields=lyrics,tags&sort=AdditionDate&nameMatchMode=words&maxResults=3"))

        Catch
            Return Nothing
        End Try
        Return Xml
    End Function

    Private Function GetSongFromId(SongId As Integer) As Xml.XmlDocument
        Dim Xml As New Xml.XmlDocument

        Try
            Xml.LoadXml(DownloadXml(DatabaseUrl.AbsoluteUri & "api/songs/" & SongId & "?fields=lyrics,tags"))
        Catch
            Return Nothing
        End Try

        Return Xml
    End Function

    Private Function DownloadXml(Url As String) As String
        Dim WebClient As New System.Net.WebClient
        Dim RtnStr As String

        If UserAgent IsNot DefaultUserAgent AndAlso AppendDefaultUserAgent = True Then WebClient.Headers.Add(Net.HttpRequestHeader.UserAgent, UserAgent & " (" & DefaultUserAgent & ")") _
            Else WebClient.Headers.Add(Net.HttpRequestHeader.UserAgent, UserAgent)

        WebClient.Headers.Add(Net.HttpRequestHeader.Accept, "application/xml")
        WebClient.Proxy = Proxy
        WebClient.Encoding = System.Text.Encoding.UTF8

        RtnStr = WebClient.DownloadString(Url)
        WebClient.Dispose()
        Return RtnStr
    End Function


    'This new version can parse multiple songs at once.
    'This means that we can retrieve multiple songs in one DB query.
    Private Function GetLyricsResultFromXml(Xml As Xml.XmlDocument) As LyricsResult
        Dim LyricsResult As New LyricsResult({})
        'Dim LyricsContainers() As LyricsContainer

        'This is the error detection code:
        If Xml Is Nothing Then
            LyricsResult.ErrorType = VocaDbLyricsError.ConnectionError
            Return LyricsResult
        End If

        If Xml.GetElementsByTagName("SongForApiContract").Count = 0 Then
            LyricsResult.ErrorType = VocaDbLyricsError.NoSong
            Return LyricsResult
        End If


        'The fun part:
        For Each SongContract As Xml.XmlNode In Xml.GetElementsByTagName("SongForApiContract")
            If SongContract.Item("Lyrics").FirstChild Is Nothing Then

                If DetectInstrumental = True Then
                    'We only want to return lyrics for non-instrumentals. If we detect an instrumental, we can return an error.

                    For Each Tag As Xml.XmlNode In SongContract.Item("Tags").ChildNodes
                        If Tag.Item("Tag").Item("Name").InnerText = "instrumental" Then
                            'Detecting an instrumental causes searching to stop, because there's a decent chance that continued searching will falsely return lyrics.
                            LyricsResult.ErrorType = VocaDbLyricsError.IsInstrumental
                            Return LyricsResult
                        End If
                    Next
                End If

                If SongContract.Item("OriginalVersionId") IsNot Nothing Then
                    Dim OrigContract As Integer = OriginalContractIndex(Xml, SongContract.Item("OriginalVersionId").InnerText)

                    If OrigContract > -1 Then
                        Dim XmlDoc As New Xml.XmlDocument
                        XmlDoc.LoadXml(Xml.GetElementsByTagName("SongForApiContract").Item(OrigContract).OuterXml)
                        LyricsResult = GetLyricsResultFromXml(XmlDoc)
                        If LyricsResult.LyricsContainers.Count > 0 Then
                            LyricsResult.WarningType = VocaDbLyricsWarning.UsedOriginal
                            Return LyricsResult
                        End If
                    Else
                        LyricsResult = GetLyricsResultFromXml(GetSongFromId(SongContract.Item("OriginalVersionId").InnerText))
                        If LyricsResult.LyricsContainers.Count > 0 Then
                            LyricsResult.WarningType = VocaDbLyricsWarning.UsedOriginal
                            Return LyricsResult
                        End If
                    End If
                End If

            Else
                ReDim LyricsResult.LyricsContainers(SongContract.Item("Lyrics").ChildNodes.Count - 1)
                For i As Integer = 0 To LyricsResult.LyricsContainers.Length - 1
                    Dim Lyrics = SongContract.Item("Lyrics").ChildNodes(i)
                    LyricsResult.LyricsContainers(i).Language = Lyrics.Item("Language").InnerText
                    LyricsResult.LyricsContainers(i).Lyrics = Lyrics.Item("Value").InnerText
                    While LyricsResult.LyricsContainers(i).Lyrics.First = vbCr Or LyricsResult.LyricsContainers(i).Lyrics.First = vbLf
                        LyricsResult.LyricsContainers(i).Lyrics = LyricsResult.LyricsContainers(i).Lyrics.Remove(0, 1)
                    End While
                    While LyricsResult.LyricsContainers(i).Lyrics.Last = vbCr Or LyricsResult.LyricsContainers(i).Lyrics.Last = vbLf
                        LyricsResult.LyricsContainers(i).Lyrics = LyricsResult.LyricsContainers(i).Lyrics.Remove(LyricsResult.LyricsContainers(i).Lyrics.Length - 1, 1)
                    End While
                Next
                Return LyricsResult
            End If
        Next
        LyricsResult.ErrorType = VocaDbLyricsError.NoLyrics
        Return LyricsResult
    End Function

    Private Function OriginalContractIndex(Xml As Xml.XmlDocument, OriginalID As Integer) As Integer
        For i = 0 To Xml.GetElementsByTagName("SongForApiContract").Count - 1
            If Xml.GetElementsByTagName("SongForApiContract").Item(i).Item("Id").InnerText = OriginalID Then
                Return i
            End If
        Next
        Return -1
    End Function
End Class

