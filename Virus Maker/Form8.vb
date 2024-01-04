Imports System.IO.File
Public Class Form8
    Dim write As System.IO.StreamWriter
    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'combobox1
        ComboBox1.Items.Add("Random")
        ComboBox1.Items.Add("0 and 1")
        ComboBox1.Items.Add("Rain")
        'combobox2
        ComboBox2.Items.Add("Black")
        ComboBox2.Items.Add("Dark blue")
        ComboBox2.Items.Add("Green")
        ComboBox2.Items.Add("Teal")
        ComboBox2.Items.Add("Bordeaux")
        ComboBox2.Items.Add("Violet")
        ComboBox2.Items.Add("Olive green")
        ComboBox2.Items.Add("Light grey")
        ComboBox2.Items.Add("Grey")
        ComboBox2.Items.Add("Blue")
        ComboBox2.Items.Add("Lime")
        ComboBox2.Items.Add("Light blue")
        ComboBox2.Items.Add("Red")
        ComboBox2.Items.Add("Fuchsia")
        ComboBox2.Items.Add("Yellow")
        ComboBox2.Items.Add("White")
        'combobox3
        ComboBox3.Items.Add("Black")
        ComboBox3.Items.Add("Dark blue")
        ComboBox3.Items.Add("Green")
        ComboBox3.Items.Add("Teal")
        ComboBox3.Items.Add("Bordeaux")
        ComboBox3.Items.Add("Violet")
        ComboBox3.Items.Add("Olive green")
        ComboBox3.Items.Add("Light grey")
        ComboBox3.Items.Add("Grey")
        ComboBox3.Items.Add("Blue")
        ComboBox3.Items.Add("Lime")
        ComboBox3.Items.Add("Light blue")
        ComboBox3.Items.Add("Red")
        ComboBox3.Items.Add("Fuchsia")
        ComboBox3.Items.Add("Yellow")
        ComboBox3.Items.Add("White")
        'combobox4
        ComboBox4.Items.Add("Black")
        ComboBox4.Items.Add("Green")
        ComboBox4.Items.Add("Cyan")
        ComboBox4.Items.Add("Grey")
        ComboBox4.Items.Add("Blue")
        ComboBox4.Items.Add("Red")
        ComboBox4.Items.Add("Magenta")
        ComboBox4.Items.Add("Yellow")
        ComboBox4.Items.Add("White")
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.Text = "" Then
            MsgBox("Please select valid options", MsgBoxStyle.Critical, "Error")
            Exit Sub
        Else
            If ComboBox1.Text = "Rain" Then
                If ComboBox4.Text = "" Then
                    MsgBox("Please select valid options", MsgBoxStyle.Critical, "Error")
                    Exit Sub
                End If
            Else
                If ComboBox2.Text = "" Then
                    MsgBox("Please select valid options", MsgBoxStyle.Critical, "Error")
                    Exit Sub
                End If
                If ComboBox3.Text = "" Then
                    MsgBox("Please select valid options", MsgBoxStyle.Critical, "Error")
                    Exit Sub
                End If
            End If
        End If
        ''dim
        Dim backcolor As String
        Dim color As String
        ''set colors
        If ComboBox2.Text = "Black" Then
            backcolor = "0"
        ElseIf ComboBox2.Text = "Dark blue" Then
            backcolor = "1"
        ElseIf ComboBox2.Text = "Green" Then
            backcolor = "2"
        ElseIf ComboBox2.Text = "Teal" Then
            backcolor = "3"
        ElseIf ComboBox2.Text = "Bordeaux" Then
            backcolor = "4"
        ElseIf ComboBox2.Text = "Violet" Then
            backcolor = "5"
        ElseIf ComboBox2.Text = "Olive green" Then
            backcolor = "6"
        ElseIf ComboBox2.Text = "Light grey" Then
            backcolor = "7"
        ElseIf ComboBox2.Text = "Grey" Then
            backcolor = "8"
        ElseIf ComboBox2.Text = "Blue" Then
            backcolor = "9"
        ElseIf ComboBox2.Text = "Lime" Then
            backcolor = "A"
        ElseIf ComboBox2.Text = "Light blue" Then
            backcolor = "B"
        ElseIf ComboBox2.Text = "Red" Then
            backcolor = "C"
        ElseIf ComboBox2.Text = "Fuchsia" Then
            backcolor = "D"
        ElseIf ComboBox2.Text = "Yellow" Then
            backcolor = "E"
        ElseIf ComboBox2.Text = "White" Then
            backcolor = "F"
        End If
        If ComboBox3.Text = "Black" Then
            color = "0"
        ElseIf ComboBox3.Text = "Dark blue" Then
            color = "1"
        ElseIf ComboBox3.Text = "Green" Then
            color = "2"
        ElseIf ComboBox3.Text = "Teal" Then
            color = "3"
        ElseIf ComboBox3.Text = "Bordeaux" Then
            color = "4"
        ElseIf ComboBox3.Text = "Violet" Then
            color = "5"
        ElseIf ComboBox3.Text = "Olive green" Then
            color = "6"
        ElseIf ComboBox3.Text = "Light grey" Then
            color = "7"
        ElseIf ComboBox3.Text = "Grey" Then
            color = "8"
        ElseIf ComboBox3.Text = "Blue" Then
            color = "9"
        ElseIf ComboBox3.Text = "Lime" Then
            color = "A"
        ElseIf ComboBox3.Text = "Light blue" Then
            color = "B"
        ElseIf ComboBox3.Text = "Red" Then
            color = "C"
        ElseIf ComboBox3.Text = "Fuchsia" Then
            color = "D"
        ElseIf ComboBox3.Text = "Yellow" Then
            color = "E"
        ElseIf ComboBox3.Text = "White" Then
            color = "F"
        End If
        If ComboBox1.Text = "Random" Then
            ''generate code
            TextBox1.Text = "@echo off" + vbCrLf + "title matrix" + vbCrLf + "color " + backcolor + color + vbCrLf + ":a" + vbCrLf + "echo %random%%random%%random%%random%%random%%random%%random%%random%%random%%random%%random%%random%%random%%random%%random%%random%" + vbCrLf + "goto a"
            Dim filenamePath As String
            Dim sfd As New SaveFileDialog
            sfd.DefaultExt = ".bat"
            sfd.Title = "Save matrix virus"
            sfd.Filter = "Batch file (*.bat)|*.bat"
            sfd.FilterIndex = 1
            Dim filename As String
            If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
                filename = sfd.FileName
                filenamePath = filename
                TextBox2.Text = filename
            Else : Exit Sub
            End If
            ''writing batch
            write = System.IO.File.AppendText(TextBox2.Text)
            write.WriteLine(TextBox1.Text)
            write.Close()
        End If
        If ComboBox1.Text = "0 and 1" Then
            TextBox1.Text = "@echo off" + vbCrLf + "title matrix" + vbCrLf + "cls" + vbCrLf + "color " + backcolor + color + vbCrLf + TextBox3.Text
            Dim filenamePath As String
            Dim sfd As New SaveFileDialog
            sfd.DefaultExt = ".bat"
            sfd.Title = "Save matrix virus"
            sfd.Filter = "Batch file (*.bat)|*.bat"
            sfd.FilterIndex = 1
            Dim filename As String
            If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
                filename = sfd.FileName
                filenamePath = filename
                TextBox2.Text = filename
            Else : Exit Sub
            End If
            ''writing batch
            write = System.IO.File.AppendText(TextBox2.Text)
            write.WriteLine(TextBox1.Text)
            write.Close()
        End If
        If ComboBox1.Text = "Rain" Then
            Dim downmatrix As String
            Dim file As String = Application.StartupPath + "/The Matrix " + ComboBox4.Text + ".exe"
            downmatrix = "http://www.blackhost.xyz/prg/tmx/tm" + ComboBox4.Text + ".exe"
            Try
                MsgBox("Download started.", MsgBoxStyle.Information, "Information")
                My.Computer.Network.DownloadFile(downmatrix, file)
                MsgBox("The Matrix has been succesfully donwloaded.", MsgBoxStyle.Information, "Information")
            Catch ex As Exception
                MsgBox("An error occured.", MsgBoxStyle.Critical, "Information")
            End Try
        End If
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Text = "Rain" Then
            ComboBox2.Enabled = False
            ComboBox3.Visible = False
            ComboBox4.Visible = True
        Else
            ComboBox2.Enabled = True
            ComboBox3.Visible = True
            ComboBox4.Visible = False
        End If
    End Sub
End Class