Imports System.IO
Public Class Form9
    Dim fichier, icone As String
    Dim ofd1 As New OpenFileDialog
    'Private Sub ChromeButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    With ofd1
    '        .FileName = ""
    '        .Filter = "Executable (*.exe)|*.exe"
    '        .Title = "Selectionner votre fichier..."
    '        .ShowDialog()
    '        TextBox1.Text = .FileName
    '    End With
    'End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim ofd As New OpenFileDialog
        ofd.Title = "Select an icon"
        ofd.Filter = "Icon (*.ico)|*.ico"
        If ofd.ShowDialog <> vbOK Then
            Exit Sub
        End If
        TextBox1.Text = ofd.FileName
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        Form1.PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.ImageLocation = ofd.FileName
        Form1.PictureBox2.ImageLocation = ofd.FileName
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Hide()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox1.Clear()
        PictureBox1.Image = Nothing
        Form1.PictureBox2.Image = Nothing
        Me.Close()
    End Sub
End Class