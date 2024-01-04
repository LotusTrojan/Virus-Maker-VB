Public Class Form3
    Private Sub Save_Click(sender As Object, e As EventArgs) Handles Save.Click
        '0 = vbOKOnly - OK button only
        '1 = vbOKCancel - OK and Cancel buttons
        '2 = vbAbortRetryIgnore - Abort, Retry, and Ignore buttons
        '3 = vbYesNoCancel - Yes, No, and Cancel buttons
        '4 = vbYesNo - Yes and No buttons
        '5 = vbRetryCancel - Retry and Cancel buttons
        '16 = vbCritical - Critical Message icon
        '32 = vbQuestion - Warning Query icon
        '48 = vbExclamation - Warning Message icon
        '64 = vbInformation - Information Message icon


        'If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

        'Using write As New System.IO.StreamWriter(SaveFileDialog1.FileName, False)

        Dim iconnumber As Integer
        Dim buttonsnumber As Integer

        If ComboBox1.SelectedItem = ("Error") Then iconnumber = 16
        If ComboBox1.SelectedItem = ("Message") Then iconnumber = 0
        If ComboBox1.SelectedItem = ("Exclamation") Then iconnumber = 48
        If ComboBox1.SelectedItem = ("Question") Then iconnumber = 32
        If ComboBox1.SelectedItem = ("Information") Then iconnumber = 64

        buttonsnumber = ComboBox2.SelectedIndex

        'Per fare le " dentro una stringa possiamo fare in questo modo :
        'Il file "CIAO" è stato salvato --> "Il file ""CIAO"" è stato salvato"
        If CheckBox1.Checked = True Then
            'Write.WriteLine("MsgBox"""+"," + iconnumber.ToString + "+" + buttonsnumber.ToString + ",""" + Titolo.Text + """")
            TextBox1.Text = "echo do>>msg.vbs" + vbCrLf + "echo Msgbox""" + t2.Text + """," + iconnumber.ToString + "+" + buttonsnumber.ToString + ",""" + t1.Text + """" + ">>msg.vbs" + vbCrLf + "echo loop>>msg.vbs" + vbCrLf + "start msg.vbs"
            Form1.CheckBox9.Checked = True
            'End Using
            'MsgBox("The VBS script was succesfully saved.", MsgBoxStyle.Information)
            'End If
        Else
            TextBox1.Text = "echo Msgbox""" + t2.Text + """," + iconnumber.ToString + "+" + buttonsnumber.ToString + ",""" + t1.Text + """" + ">>msg.vbs" + vbCrLf + "start msg.vbs"
            Form1.CheckBox9.Checked = True
        End If
        Me.Hide()
    End Sub

    Private Sub button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        Form1.CheckBox9.Checked = False
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim msg As String
        Dim title As String
        Dim mbstyle As String
        msg = t2.Text
        title = t1.Text
        If ComboBox1.Text = "Message" Then
            mbstyle += MsgBoxStyle.ApplicationModal
        End If
        If ComboBox1.Text = "Information" Then
            mbstyle += MsgBoxStyle.Information
        End If
        If ComboBox1.Text = "Question" Then
            mbstyle += MsgBoxStyle.Question
        End If
        If ComboBox1.Text = "Exclamation" Then
            mbstyle += MsgBoxStyle.Exclamation
        End If
        If ComboBox1.Text = "Critical" Then
            mbstyle += MsgBoxStyle.Critical
        End If
        If ComboBox2.Text = "Ok" Then
            mbstyle += MsgBoxStyle.OkOnly
        End If
        If ComboBox2.Text = "Ok - Cancel" Then
            mbstyle += MsgBoxStyle.OkCancel
        End If
        If ComboBox2.Text = "Abort - Retry - Ignore" Then
            mbstyle += MsgBoxStyle.AbortRetryIgnore
        End If
        If ComboBox2.Text = "Yes - No - Cancel" Then
            mbstyle += MsgBoxStyle.YesNoCancel
        End If
        If ComboBox2.Text = "Yes - No" Then
            mbstyle += MsgBoxStyle.YesNo
        End If
        If ComboBox2.Text = "Retry - Cancel" Then
            mbstyle += MsgBoxStyle.RetryCancel
        End If
        If ComboBox1.Text = "" Then
            mbstyle += MsgBoxStyle.ApplicationModal
        End If
        If ComboBox2.Text = "" Then
            mbstyle += MsgBoxStyle.OkOnly
        End If
        MsgBox(msg, mbstyle, title)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class