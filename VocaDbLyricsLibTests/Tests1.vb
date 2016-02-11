'Copyright © 2015 NetNerd


'This file is part of VocaDbLyricsLib(Tests).

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


<TestClass()> Public Class Tests1
    Dim LyricsLib As New VocaDbLyricsLib
    Dim LyricsResult As VocaDbLyricsLib.LyricsResult

    <TestMethod()> Public Sub ReturnsLyricsFromId()
        LyricsResult = LyricsLib.GetLyricsFromId(1326) 'world is mine
        Assert.IsTrue(LyricsResult.LyricsContainers.Count > 0)
        Assert.IsTrue(LyricsResult.ErrorType = VocaDbLyricsLib.VocaDbLyricsError.None)
        Assert.IsTrue(LyricsResult.WarningType = VocaDbLyricsLib.VocaDbLyricsWarning.None)
    End Sub

    <TestMethod()> Public Sub ReturnsLyricsFromName_NoArtist()
        LyricsResult = LyricsLib.GetLyricsFromName("world is mine")
        Assert.IsTrue(LyricsResult.LyricsContainers.Count > 0)
        Assert.IsTrue(LyricsResult.ErrorType = VocaDbLyricsLib.VocaDbLyricsError.None)
        Assert.IsTrue(LyricsResult.WarningType = VocaDbLyricsLib.VocaDbLyricsWarning.None)
    End Sub

    <TestMethod()> Public Sub ReturnsLyricsFromName_WithArtist()
        LyricsResult = LyricsLib.GetLyricsFromName("world is mine", "ryo")
        Assert.IsTrue(LyricsResult.LyricsContainers.Count > 0)
        Assert.IsTrue(LyricsResult.ErrorType = VocaDbLyricsLib.VocaDbLyricsError.None)
        Assert.IsTrue(LyricsResult.WarningType = VocaDbLyricsLib.VocaDbLyricsWarning.None)
    End Sub

    <TestMethod()> Public Sub ReturnsLyricsFromName_NonexistantArtist()
        LyricsResult = LyricsLib.GetLyricsFromName("world is mine", "fdsarae")
        Assert.IsTrue(LyricsResult.LyricsContainers.Count > 0)
        Assert.IsTrue(LyricsResult.ErrorType = VocaDbLyricsLib.VocaDbLyricsError.None)
        Assert.IsTrue(LyricsResult.WarningType = VocaDbLyricsLib.VocaDbLyricsWarning.NoArtist)
    End Sub

    <TestMethod()> Public Sub DoesNotReturnLyricsFromName_IncorrectArtist()
        LyricsResult = LyricsLib.GetLyricsFromName("world is mine", "ryu")
        Assert.IsTrue(LyricsResult.LyricsContainers.Count = 0)
        Assert.IsTrue(LyricsResult.ErrorType = VocaDbLyricsLib.VocaDbLyricsError.NoSong)
        Assert.IsTrue(LyricsResult.WarningType = VocaDbLyricsLib.VocaDbLyricsWarning.None)
    End Sub

    <TestMethod()> Public Sub ReturnsLyricsFromName_UsingOriginal()
        LyricsResult = LyricsLib.GetLyricsFromName("two-faced lovers", "magicianP")
        Assert.IsTrue(LyricsResult.LyricsContainers.Count > 0)
        Assert.IsTrue(LyricsResult.ErrorType = VocaDbLyricsLib.VocaDbLyricsError.None)
        Assert.IsTrue(LyricsResult.WarningType = VocaDbLyricsLib.VocaDbLyricsWarning.UsedOriginal)
    End Sub

    <TestMethod()> Public Sub ReturnsLyricsFromId_UsingOriginal()
        LyricsResult = LyricsLib.GetLyricsFromId(13864)
        Assert.IsTrue(LyricsResult.LyricsContainers.Count > 0)
        Assert.IsTrue(LyricsResult.ErrorType = VocaDbLyricsLib.VocaDbLyricsError.None)
        Assert.IsTrue(LyricsResult.WarningType = VocaDbLyricsLib.VocaDbLyricsWarning.UsedOriginal)
    End Sub

    <TestMethod()> Public Sub DetectsInstrumental()
        LyricsResult = LyricsLib.GetLyricsFromId(5830)
        Assert.IsTrue(LyricsResult.LyricsContainers.Count = 0)
        Assert.IsTrue(LyricsResult.ErrorType = VocaDbLyricsLib.VocaDbLyricsError.IsInstrumental)
        Assert.IsTrue(LyricsResult.WarningType = VocaDbLyricsLib.VocaDbLyricsWarning.None)
    End Sub

    <TestMethod()> Public Sub DetectsInstrumental_NotOriginal()
        LyricsResult = LyricsLib.GetLyricsFromId(12000)
        Assert.IsTrue(LyricsResult.LyricsContainers.Count = 0)
        Assert.IsTrue(LyricsResult.ErrorType = VocaDbLyricsLib.VocaDbLyricsError.IsInstrumental)
        Assert.IsTrue(LyricsResult.WarningType = VocaDbLyricsLib.VocaDbLyricsWarning.None)
    End Sub

    <TestMethod()> Public Sub DetectsNoLyrics()
        LyricsLib.DetectInstrumental = False
        LyricsResult = LyricsLib.GetLyricsFromId(5830)
        Assert.IsTrue(LyricsResult.LyricsContainers.Count = 0)
        Assert.IsTrue(LyricsResult.ErrorType = VocaDbLyricsLib.VocaDbLyricsError.NoLyrics)
        Assert.IsTrue(LyricsResult.WarningType = VocaDbLyricsLib.VocaDbLyricsWarning.None)
        LyricsLib.DetectInstrumental = True
    End Sub

    <TestMethod()> Public Sub DetectsConnectionErr()
        LyricsLib.Proxy = New Net.WebProxy("123.456.789.10")
        LyricsResult = LyricsLib.GetLyricsFromName("world is mine")
        Assert.IsTrue(LyricsResult.LyricsContainers.Count = 0)
        Assert.IsTrue(LyricsResult.ErrorType = VocaDbLyricsLib.VocaDbLyricsError.ConnectionError)
        Assert.IsTrue(LyricsResult.WarningType = VocaDbLyricsLib.VocaDbLyricsWarning.None)
        LyricsLib.Proxy = Nothing
    End Sub

    <TestMethod()> Public Sub ForceArtist_Success()
        LyricsLib.ForceArtistMatch = True
        LyricsLib.UseOldForceArtistMatch = False
        LyricsResult = LyricsLib.GetLyricsFromName("twitter (HGS edition)", "darvishP, GUMI")
        Assert.IsTrue(LyricsResult.LyricsContainers.Count > 0)
        Assert.IsTrue(LyricsResult.ErrorType = VocaDbLyricsLib.VocaDbLyricsError.None)
        Assert.IsTrue(LyricsResult.WarningType = VocaDbLyricsLib.VocaDbLyricsWarning.UsedOriginal)
        LyricsLib.ForceArtistMatch = False
    End Sub

    <TestMethod()> Public Sub ForceArtist_Success_ManyArtists()
        LyricsLib.ForceArtistMatch = True
        LyricsLib.UseOldForceArtistMatch = False
        LyricsResult = LyricsLib.GetLyricsFromName("Birthday Song for ミク", "Mitchie M, 鏡音リン, 鏡音レン")
        Assert.IsTrue(LyricsResult.LyricsContainers.Count > 0)
        Assert.IsTrue(LyricsResult.ErrorType = VocaDbLyricsLib.VocaDbLyricsError.None)
        Assert.IsTrue(LyricsResult.WarningType = VocaDbLyricsLib.VocaDbLyricsWarning.None)
        LyricsLib.ForceArtistMatch = False
    End Sub

    <TestMethod()> Public Sub ForceArtist_Fail()
        LyricsLib.ForceArtistMatch = True
        LyricsLib.UseOldForceArtistMatch = False
        LyricsResult = LyricsLib.GetLyricsFromName("twitter (HGS edition)", "darvishP, dalvish, GUMI")
        Assert.IsTrue(LyricsResult.LyricsContainers.Count = 0)
        Assert.IsTrue(LyricsResult.ErrorType = VocaDbLyricsLib.VocaDbLyricsError.NoSong)
        Assert.IsTrue(LyricsResult.WarningType = VocaDbLyricsLib.VocaDbLyricsWarning.NoArtist)
        LyricsLib.ForceArtistMatch = False
    End Sub

    <TestMethod()> Public Sub ForceArtist_Old_Success()
        LyricsLib.ForceArtistMatch = True
        LyricsLib.UseOldForceArtistMatch = True
        LyricsResult = LyricsLib.GetLyricsFromName("twitter (HGS edition)", "darvishP, dalvish, GUMI")
        Assert.IsTrue(LyricsResult.LyricsContainers.Count > 0)
        Assert.IsTrue(LyricsResult.ErrorType = VocaDbLyricsLib.VocaDbLyricsError.None)
        Assert.IsTrue(LyricsResult.WarningType = VocaDbLyricsLib.VocaDbLyricsWarning.SomeArtists)
        LyricsLib.ForceArtistMatch = False
    End Sub

    <TestMethod()> Public Sub ForceArtist_Old_Fail()
        LyricsLib.ForceArtistMatch = True
        LyricsLib.UseOldForceArtistMatch = True
        LyricsResult = LyricsLib.GetLyricsFromName("twitter (HGS edition)", "dalvish, darvishP, GUMI")
        Assert.IsTrue(LyricsResult.LyricsContainers.Count = 0)
        Assert.IsTrue(LyricsResult.ErrorType = VocaDbLyricsLib.VocaDbLyricsError.NoSong)
        Assert.IsTrue(LyricsResult.WarningType = VocaDbLyricsLib.VocaDbLyricsWarning.NoArtist)
        LyricsLib.ForceArtistMatch = False
    End Sub

End Class