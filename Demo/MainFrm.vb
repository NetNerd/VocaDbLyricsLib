Public Class MainFrm
    Dim GetLyricsThread As New Threading.Thread(New Threading.ThreadStart(AddressOf GetLyricsSub))

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LangBox1.Items.Add("Japanese")
        LangBox2.Items.Add("Romaji")
        LangBox2.Items.Add("English")
    End Sub

    Private Sub LangBox_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles LangBox1.DragDrop, LangBox2.DragDrop
        If sender.PointToClient(New Point(e.X, e.Y)).Y < 0 Then
            sender.Items.Insert(0, e.Data.GetData(DataFormats.Text))
        ElseIf sender.PointToClient(New Point(e.X, e.Y)).Y > sender.ItemHeight * sender.Items.Count Then
            sender.Items.Add(e.Data.GetData(DataFormats.Text))
        Else
            sender.Items.Insert(sender.PointToClient(New Point(e.X, e.Y)).Y / sender.ItemHeight, e.Data.GetData(DataFormats.Text))
        End If

        If LangBox1.SelectedIndex > -1 AndAlso e.Data.GetData(DataFormats.Text) Is LangBox1.Items(LangBox1.SelectedIndex) Then
            LangBox1.Items.RemoveAt(LangBox1.SelectedIndex)
        End If

        If LangBox2.SelectedIndex > -1 AndAlso e.Data.GetData(DataFormats.Text) Is LangBox2.Items(LangBox2.SelectedIndex) Then
            LangBox2.Items.RemoveAt(LangBox2.SelectedIndex)
        End If
    End Sub

    Private Sub LangBox_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles LangBox1.DragOver, LangBox2.DragOver
        e.Effect = DragDropEffects.Move
    End Sub

    Private Sub LangBox_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LangBox1.MouseDown, LangBox2.MouseDown
        Threading.Thread.Sleep(200)
        sender.DoDragDrop(sender.Text, DragDropEffects.All)
    End Sub

    Private Sub BtnL_Click(sender As Object, e As EventArgs) Handles BtnL.Click
        If LangBox2.SelectedIndex > -1 Then
            LangBox1.Items.Add(LangBox2.Items(LangBox2.SelectedIndex))
            LangBox2.Items.RemoveAt(LangBox2.SelectedIndex)
        End If
    End Sub

    Private Sub BtnR_Click(sender As Object, e As EventArgs) Handles BtnR.Click
        If LangBox1.SelectedIndex > -1 Then
            LangBox2.Items.Add(LangBox1.Items(LangBox1.SelectedIndex))
            LangBox1.Items.RemoveAt(LangBox1.SelectedIndex)
        End If
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
                For Each Item In LangBox2.Items
                    For Each LyricsContainer In LyricsResult.LyricsContainers
                        If LyricsContainer.Language = Item.ToString Then
                            If LyricsWriter.ToString.Length > 0 Then LyricsWriter.Write(vbNewLine & vbNewLine & vbNewLine & vbNewLine)
                            LyricsWriter.WriteLine(LyricsContainer.Language & ":")
                            LyricsWriter.Write(LyricsContainer.Lyrics)
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
