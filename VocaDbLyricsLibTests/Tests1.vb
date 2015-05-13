Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class Tests1
    Dim LyricsLib As New VocaDbLyricsLib
    Dim LyricsResult As VocaDbLyricsLib.LyricsResult

    <TestMethod()> Public Sub ReturnsLyricsFromId()
        LyricsResult = LyricsLib.GetLyricsFromName("world is mine")
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

End Class