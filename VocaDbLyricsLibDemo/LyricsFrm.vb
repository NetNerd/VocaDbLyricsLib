Public Class LyricsFrm

    Private Sub Form2_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        TextBox1.Width = Me.ClientRectangle.Width - 24
        TextBox1.Height = Me.ClientRectangle.Height - 24
    End Sub
End Class