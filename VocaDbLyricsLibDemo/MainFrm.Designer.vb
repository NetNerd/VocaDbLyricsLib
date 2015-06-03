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


<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainFrm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.LangBox1 = New System.Windows.Forms.ListBox()
        Me.LangBox2 = New System.Windows.Forms.ListBox()
        Me.BtnL = New System.Windows.Forms.Button()
        Me.BtnR = New System.Windows.Forms.Button()
        Me.LblLang1 = New System.Windows.Forms.Label()
        Me.LblLang2 = New System.Windows.Forms.Label()
        Me.LblSong = New System.Windows.Forms.Label()
        Me.SongBox = New System.Windows.Forms.TextBox()
        Me.LblArtist = New System.Windows.Forms.Label()
        Me.ArtistBox = New System.Windows.Forms.TextBox()
        Me.BtnGo = New System.Windows.Forms.Button()
        Me.LblStatus = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'LangBox1
        '
        Me.LangBox1.AllowDrop = True
        Me.LangBox1.FormattingEnabled = True
        Me.LangBox1.Location = New System.Drawing.Point(13, 29)
        Me.LangBox1.Name = "LangBox1"
        Me.LangBox1.Size = New System.Drawing.Size(120, 56)
        Me.LangBox1.TabIndex = 0
        '
        'LangBox2
        '
        Me.LangBox2.AllowDrop = True
        Me.LangBox2.FormattingEnabled = True
        Me.LangBox2.Location = New System.Drawing.Point(176, 28)
        Me.LangBox2.Name = "LangBox2"
        Me.LangBox2.Size = New System.Drawing.Size(120, 56)
        Me.LangBox2.TabIndex = 3
        '
        'BtnL
        '
        Me.BtnL.Location = New System.Drawing.Point(140, 31)
        Me.BtnL.Name = "BtnL"
        Me.BtnL.Size = New System.Drawing.Size(29, 25)
        Me.BtnL.TabIndex = 1
        Me.BtnL.Text = "<"
        Me.BtnL.UseVisualStyleBackColor = True
        '
        'BtnR
        '
        Me.BtnR.Location = New System.Drawing.Point(140, 57)
        Me.BtnR.Name = "BtnR"
        Me.BtnR.Size = New System.Drawing.Size(29, 25)
        Me.BtnR.TabIndex = 2
        Me.BtnR.Text = ">"
        Me.BtnR.UseVisualStyleBackColor = True
        '
        'LblLang1
        '
        Me.LblLang1.AutoSize = True
        Me.LblLang1.Location = New System.Drawing.Point(10, 13)
        Me.LblLang1.Name = "LblLang1"
        Me.LblLang1.Size = New System.Drawing.Size(109, 13)
        Me.LblLang1.TabIndex = 4
        Me.LblLang1.Text = "Available Languages:"
        '
        'LblLang2
        '
        Me.LblLang2.AutoSize = True
        Me.LblLang2.Location = New System.Drawing.Point(173, 13)
        Me.LblLang2.Name = "LblLang2"
        Me.LblLang2.Size = New System.Drawing.Size(112, 13)
        Me.LblLang2.TabIndex = 5
        Me.LblLang2.Text = "Displayed Languages:"
        '
        'LblSong
        '
        Me.LblSong.AutoSize = True
        Me.LblSong.Location = New System.Drawing.Point(9, 136)
        Me.LblSong.Name = "LblSong"
        Me.LblSong.Size = New System.Drawing.Size(35, 13)
        Me.LblSong.TabIndex = 6
        Me.LblSong.Text = "Song:"
        '
        'SongBox
        '
        Me.SongBox.AllowDrop = True
        Me.SongBox.Location = New System.Drawing.Point(12, 152)
        Me.SongBox.Name = "SongBox"
        Me.SongBox.Size = New System.Drawing.Size(284, 20)
        Me.SongBox.TabIndex = 7
        '
        'LblArtist
        '
        Me.LblArtist.AutoSize = True
        Me.LblArtist.Location = New System.Drawing.Point(9, 183)
        Me.LblArtist.Name = "LblArtist"
        Me.LblArtist.Size = New System.Drawing.Size(81, 13)
        Me.LblArtist.TabIndex = 8
        Me.LblArtist.Text = "Artist (Optional):"
        '
        'ArtistBox
        '
        Me.ArtistBox.AllowDrop = True
        Me.ArtistBox.Location = New System.Drawing.Point(12, 199)
        Me.ArtistBox.Name = "ArtistBox"
        Me.ArtistBox.Size = New System.Drawing.Size(283, 20)
        Me.ArtistBox.TabIndex = 9
        '
        'BtnGo
        '
        Me.BtnGo.Location = New System.Drawing.Point(12, 231)
        Me.BtnGo.Name = "BtnGo"
        Me.BtnGo.Size = New System.Drawing.Size(75, 23)
        Me.BtnGo.TabIndex = 10
        Me.BtnGo.Text = "Get Lyrics"
        Me.BtnGo.UseVisualStyleBackColor = True
        '
        'LblStatus
        '
        Me.LblStatus.AutoSize = True
        Me.LblStatus.Location = New System.Drawing.Point(93, 236)
        Me.LblStatus.Name = "LblStatus"
        Me.LblStatus.Size = New System.Drawing.Size(0, 13)
        Me.LblStatus.TabIndex = 11
        '
        'MainFrm
        '
        Me.AcceptButton = Me.BtnGo
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(308, 266)
        Me.Controls.Add(Me.LblStatus)
        Me.Controls.Add(Me.BtnGo)
        Me.Controls.Add(Me.ArtistBox)
        Me.Controls.Add(Me.LblArtist)
        Me.Controls.Add(Me.SongBox)
        Me.Controls.Add(Me.LblSong)
        Me.Controls.Add(Me.LblLang2)
        Me.Controls.Add(Me.LblLang1)
        Me.Controls.Add(Me.BtnR)
        Me.Controls.Add(Me.BtnL)
        Me.Controls.Add(Me.LangBox2)
        Me.Controls.Add(Me.LangBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "MainFrm"
        Me.Text = "VocaDbLyricsLibDemo"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LangBox1 As System.Windows.Forms.ListBox
    Friend WithEvents LangBox2 As System.Windows.Forms.ListBox
    Friend WithEvents BtnL As System.Windows.Forms.Button
    Friend WithEvents BtnR As System.Windows.Forms.Button
    Friend WithEvents LblLang1 As System.Windows.Forms.Label
    Friend WithEvents LblLang2 As System.Windows.Forms.Label
    Friend WithEvents LblSong As System.Windows.Forms.Label
    Friend WithEvents SongBox As System.Windows.Forms.TextBox
    Friend WithEvents LblArtist As System.Windows.Forms.Label
    Friend WithEvents ArtistBox As System.Windows.Forms.TextBox
    Friend WithEvents BtnGo As System.Windows.Forms.Button
    Friend WithEvents LblStatus As System.Windows.Forms.Label

End Class
