Public Class Form10
    Private Sub MEnter(sender As Object, e As EventArgs) Handles LinkLabel1.MouseEnter
        Form2.Show()
    End Sub
    Private Sub MLeave(sender As Object, e As EventArgs) Handles LinkLabel1.MouseLeave
        Form2.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        For Each cc As Control In Me.Controls
            If TypeOf cc Is CheckBox Then
                DirectCast(cc, CheckBox).Checked = False
            End If
        Next
        For Each cc As Control In Form1.Controls
            If TypeOf cc Is CheckBox Then
                DirectCast(cc, CheckBox).Checked = False
            End If
        Next
        For Each cc As Control In Form3.Controls
            If TypeOf cc Is CheckBox Then
                DirectCast(cc, CheckBox).Checked = False
            End If
        Next
        For Each cc As Control In Form4.Controls
            If TypeOf cc Is CheckBox Then
                DirectCast(cc, CheckBox).Checked = False
            End If
        Next
        For Each cc As Control In Form5.Controls
            If TypeOf cc Is CheckBox Then
                DirectCast(cc, CheckBox).Checked = False
            End If
        Next
        For Each cc As Control In Form6.Controls
            If TypeOf cc Is CheckBox Then
                DirectCast(cc, CheckBox).Checked = False
            End If
        Next
        For Each cc As Control In Form7.Controls
            If TypeOf cc Is CheckBox Then
                DirectCast(cc, CheckBox).Checked = False
            End If
        Next
    End Sub
End Class