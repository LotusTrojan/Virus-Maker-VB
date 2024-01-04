Public Class Form6
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)
        Dim msg As String
        Dim title As String
        Dim style As MsgBoxStyle
        Dim res As MsgBoxResult
        msg = "Do you want visit www.blackhost.xyz?"
        title = "BlackHost"
        style = MsgBoxStyle.Question + MsgBoxStyle.YesNo
        res = MsgBox(msg, style, title)
        If res = MsgBoxResult.Yes Then
            Dim BlackHost As String
            BlackHost = "http://www.blackhost.xyz."
            Process.Start(BlackHost)
        End If
    End Sub
End Class