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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainFrm))
        Me.LblSong = New System.Windows.Forms.Label()
        Me.SongBox = New System.Windows.Forms.TextBox()
        Me.LblArtist = New System.Windows.Forms.Label()
        Me.ArtistBox = New System.Windows.Forms.TextBox()
        Me.BtnGo = New System.Windows.Forms.Button()
        Me.LblStatus = New System.Windows.Forms.Label()
        Me.LblLanguages = New System.Windows.Forms.Label()
        Me.LanguageBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'LblSong
        '
        Me.LblSong.AutoSize = True
        Me.LblSong.Location = New System.Drawing.Point(12, 9)
        Me.LblSong.Name = "LblSong"
        Me.LblSong.Size = New System.Drawing.Size(35, 13)
        Me.LblSong.TabIndex = 1
        Me.LblSong.Text = "Song:"
        '
        'SongBox
        '
        Me.SongBox.AllowDrop = True
        Me.SongBox.Location = New System.Drawing.Point(12, 25)
        Me.SongBox.Name = "SongBox"
        Me.SongBox.Size = New System.Drawing.Size(284, 20)
        Me.SongBox.TabIndex = 2
        '
        'LblArtist
        '
        Me.LblArtist.AutoSize = True
        Me.LblArtist.Location = New System.Drawing.Point(12, 60)
        Me.LblArtist.Name = "LblArtist"
        Me.LblArtist.Size = New System.Drawing.Size(81, 13)
        Me.LblArtist.TabIndex = 3
        Me.LblArtist.Text = "Artist (Optional):"
        '
        'ArtistBox
        '
        Me.ArtistBox.AllowDrop = True
        Me.ArtistBox.Location = New System.Drawing.Point(12, 76)
        Me.ArtistBox.Name = "ArtistBox"
        Me.ArtistBox.Size = New System.Drawing.Size(283, 20)
        Me.ArtistBox.TabIndex = 4
        '
        'BtnGo
        '
        Me.BtnGo.Location = New System.Drawing.Point(12, 231)
        Me.BtnGo.Name = "BtnGo"
        Me.BtnGo.Size = New System.Drawing.Size(75, 23)
        Me.BtnGo.TabIndex = 8
        Me.BtnGo.Text = "Get Lyrics"
        Me.BtnGo.UseVisualStyleBackColor = True
        '
        'LblStatus
        '
        Me.LblStatus.AutoSize = True
        Me.LblStatus.Location = New System.Drawing.Point(93, 236)
        Me.LblStatus.Name = "LblStatus"
        Me.LblStatus.Size = New System.Drawing.Size(0, 13)
        Me.LblStatus.TabIndex = 9
        '
        'LblLanguages
        '
        Me.LblLanguages.AutoSize = True
        Me.LblLanguages.Location = New System.Drawing.Point(12, 112)
        Me.LblLanguages.Name = "LblLanguages"
        Me.LblLanguages.Size = New System.Drawing.Size(111, 13)
        Me.LblLanguages.TabIndex = 5
        Me.LblLanguages.Text = "Languages (Optional):"
        '
        'LanguageBox
        '
        Me.LanguageBox.Location = New System.Drawing.Point(12, 129)
        Me.LanguageBox.Name = "LanguageBox"
        Me.LanguageBox.Size = New System.Drawing.Size(283, 20)
        Me.LanguageBox.TabIndex = 6
        Me.LanguageBox.Text = "orig/Original, rom/Romanised, ja/Japanese, en/English"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 152)
        Me.Label1.MaximumSize = New System.Drawing.Size(283, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(283, 65)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = resources.GetString("Label1.Text")
        '
        'MainFrm
        '
        Me.AcceptButton = Me.BtnGo
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(308, 266)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LanguageBox)
        Me.Controls.Add(Me.LblLanguages)
        Me.Controls.Add(Me.LblStatus)
        Me.Controls.Add(Me.BtnGo)
        Me.Controls.Add(Me.ArtistBox)
        Me.Controls.Add(Me.LblArtist)
        Me.Controls.Add(Me.SongBox)
        Me.Controls.Add(Me.LblSong)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "MainFrm"
        Me.Text = "VocaDbLyricsLibDemo"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblSong As System.Windows.Forms.Label
    Friend WithEvents SongBox As System.Windows.Forms.TextBox
    Friend WithEvents LblArtist As System.Windows.Forms.Label
    Friend WithEvents ArtistBox As System.Windows.Forms.TextBox
    Friend WithEvents BtnGo As System.Windows.Forms.Button
    Friend WithEvents LblStatus As System.Windows.Forms.Label
    Friend WithEvents LblLanguages As Label
    Friend WithEvents LanguageBox As TextBox
    Friend WithEvents Label1 As Label
End Class
