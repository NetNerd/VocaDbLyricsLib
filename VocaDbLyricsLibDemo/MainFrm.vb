'Copyright © 2015 NetNerd


'This file is part of VocaDbLyricsLib(Demo).

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


Public Class MainFrm
    Dim GetLyricsThread As New Threading.Thread(New Threading.ThreadStart(AddressOf GetLyricsSub))

    Dim SpecialLanguages As Dictionary(Of String, String) =
        New Dictionary(Of String, String) From {{"orig", "Original"}, {"rom", "Romanized"}}

    Private Sub TextBox_DragDrop(sender As TextBox, e As DragEventArgs) Handles SongBox.DragDrop, ArtistBox.DragDrop, LanguageBox.DragDrop
        sender.Text = e.Data.GetData(DataFormats.Text)
    End Sub

    Private Sub TextBox_DragEnter(sender As TextBox, e As DragEventArgs) Handles SongBox.DragEnter, ArtistBox.DragEnter, LanguageBox.DragEnter
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub BtnGo_Click(sender As Object, e As EventArgs) Handles BtnGo.Click
        LblStatus.Text = "Retreiving lyrics."
        For Each Control As Control In Me.Controls
            Control.Enabled = False
        Next
        LblStatus.Enabled = True

        'This can and will lock up your UI thread.
        GetLyricsThread = New Threading.Thread(New Threading.ThreadStart(AddressOf GetLyricsSub))
        GetLyricsThread.IsBackground = True
        GetLyricsThread.Start()
    End Sub

    Private Sub GetLyricsSub()
        Dim LyricsLib As New VocaDbLyricsLib
        Dim LyricsResult As VocaDbLyricsLib.LyricsResult = LyricsLib.GetLyricsFromName(SongBox.Text, ArtistBox.Text)
        Dim LyricsWriter As New IO.StringWriter

        Select Case LyricsResult.ErrorType
            Case VocaDbLyricsLib.VocaDbLyricsError.None
                Dim DoneLyrics As VocaDbLyricsLib.LyricsContainer()
                ReDim DoneLyrics(0)

                Dim LangboxtextClean = LanguageBox.Text.Trim()
                Dim Languages = LangboxtextClean.Split(",")
                For Each Item In Languages
                    Dim ItemFriendly As String
                    If Item.Contains("/") Then
                        ItemFriendly = Item.Split("/")(1).Trim()
                        Item = Item.Split("/")(0).ToLower().Trim()
                    Else
                        ItemFriendly = Item.Trim()
                        Item = Item.ToLower()
                    End If

                    For Each LyricsContainer In LyricsResult.LyricsContainers
                        If Not DoneLyrics.Contains(LyricsContainer) Then
                            If LangboxtextClean.Length = 0 Or 'Make optional to specify languages
                               (LyricsContainer.Language = Item And Not LyricsContainer.TranslationType = "Romanized") Or 'Romanized requires special case
                               (SpecialLanguages.ContainsKey(Item) AndAlso LyricsContainer.TranslationType = SpecialLanguages.Item(Item)) Then

                                ReDim Preserve DoneLyrics(DoneLyrics.Length)
                                DoneLyrics(DoneLyrics.Length - 1) = LyricsContainer

                                If LyricsWriter.ToString.Length > 0 Then LyricsWriter.Write(vbNewLine & vbNewLine & vbNewLine & vbNewLine)
                                If LangboxtextClean.Length = 0 Or Languages.Length > 1 Then
                                    If ItemFriendly.Length > 0 Then
                                        LyricsWriter.WriteLine(ItemFriendly & ":")
                                    Else
                                        LyricsWriter.WriteLine(LyricsContainer.Language & ":")
                                    End If
                                End If
                                LyricsWriter.Write(LyricsContainer.Lyrics)
                            End If
                        End If
                    Next
                Next

            Case VocaDbLyricsLib.VocaDbLyricsError.NoSong
                Me.Invoke(Sub() LblStatus.Text = "Song not found.")

            Case VocaDbLyricsLib.VocaDbLyricsError.NoLyrics
                Me.Invoke(Sub() LblStatus.Text = "No lyrics found.")

            Case VocaDbLyricsLib.VocaDbLyricsError.ConnectionError
                Me.Invoke(Sub() LblStatus.Text = "Could not connect to VocaDB.")

            Case VocaDbLyricsLib.VocaDbLyricsError.IsInstrumental
                Me.Invoke(Sub() LblStatus.Text = "Detected an instrumental track.")

            Case Else
                Me.Invoke(Sub() LblStatus.Text = "Unknown error.")
        End Select

        If LyricsWriter.ToString.Length > 0 Then
            Select Case LyricsResult.WarningType
                Case VocaDbLyricsLib.VocaDbLyricsWarning.NoArtist
                    Me.Invoke(Sub() LblStatus.Text = "Note: Arist was ignored.")

                Case VocaDbLyricsLib.VocaDbLyricsWarning.SomeArtists
                    Me.Invoke(Sub() LblStatus.Text = "Note: Only some artists used.")

                Case VocaDbLyricsLib.VocaDbLyricsWarning.UsedOriginal
                    'This happens when the song is detected as a cover/remix and has no lyrics, but does have the 'OriginalVersionId' set.
                    'Sometimes a cover/remix result can be returned when searching for an original song.
                    Me.Invoke(Sub() LblStatus.Text = "Note: Lyrics are from the original version.")

                Case Else
                    Me.Invoke(Sub() LblStatus.Text = "")
            End Select

            Me.Invoke(Sub() LyricsFrm.TextBox1.Text = LyricsWriter.ToString)
            Me.Invoke(Sub() LyricsFrm.Show())

        ElseIf LyricsResult.ErrorType = VocaDbLyricsLib.VocaDbLyricsError.None Then
            Me.Invoke(Sub() LblStatus.Text = "No lyrics for the selected language(s).")
        End If

        Me.Invoke(Sub()
                      For Each Control As Control In Me.Controls
                          Control.Enabled = True
                      Next
                  End Sub)
    End Sub
End Class
