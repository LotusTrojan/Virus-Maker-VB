Public Class Form2
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim working_area As Rectangle = _
            SystemInformation.WorkingArea
        Dim x As Integer = _
            working_area.Left + _
            working_area.Width - _
            Me.Width
        Dim y As Integer = _
            working_area.Top + _
            working_area.Height - _
            Me.Height
        Me.Location = New Point(x, y)
    End Sub
End Class