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


Public Class LyricsFrm
    Private Sub Form2_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        TextBox1.Width = Me.ClientRectangle.Width - 24
        TextBox1.Height = Me.ClientRectangle.Height - 24
    End Sub
End Class