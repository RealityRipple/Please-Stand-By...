Imports System.Runtime.InteropServices
Imports IWshRuntimeLibrary
Imports Please_Stand_By.NativeMethods
Public Class frmMain
  Private Enum MOD_
    Alt = &H1
    Control = &H2
    Shift = &H4
    Win = &H8
    NoRepeat = &H4000
  End Enum
#Region "UI"
  Private Sub tmrHide_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrHide.Tick
    tmrHide.Enabled = False
    Me.Hide()
    ApplyHotkey()
    SetTrayText()
    DisableTrayMenus()
  End Sub
  Private Sub tmrIdle_Tick(sender As System.Object, e As System.EventArgs) Handles tmrIdle.Tick
    tmrIdle.Stop()
    Dim inInfo As New LASTINPUTINFO
    inInfo.cbSize = Marshal.SizeOf(inInfo)
    If Not GetLastInputInfo(inInfo) Then
      MonitorStandby()
      Return
    End If
    Dim tickTime As ULong = GetTickCount()
    Dim lastTime As ULong = 0
    If tickTime > inInfo.dwTime Then lastTime = tickTime - inInfo.dwTime
    If lastTime > 5000 Then
      trayStandBy.Icon = My.Resources.icon
      MonitorStandby()
      Return
    End If
    If lastTime < 500 AndAlso Environment.TickCount - WaitStart > 15000 Then
      trayStandBy.Icon = My.Resources.icon
      Return
    End If
    If lastTime > 4000 Then
      trayStandBy.Icon = My.Resources.ico1
    ElseIf lastTime > 3000 Then
      trayStandBy.Icon = My.Resources.ico2
    ElseIf lastTime > 2000 Then
      trayStandBy.Icon = My.Resources.ico3
    Else
      trayStandBy.Icon = My.Resources.ico0
    End If
    tmrIdle.Start()
  End Sub
  Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
    If m.Msg = &H312 Then
      If m.WParam.ToInt32 = 1 Then
        WaitForIdle()
      End If
    End If
    MyBase.WndProc(m)
    If m.Msg = &HC083 Then
      DisableTrayMenus()
    End If
  End Sub
#Region "Tray Icon"
  Private Sub trayStandBy_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trayStandBy.MouseUp
    If e.Button = Windows.Forms.MouseButtons.Left Then WaitForIdle()
  End Sub
  Private Sub SetNotifyIconText(ni As NotifyIcon, text As String)
    If text.Length >= 128 Then text = text.Substring(0, 124) & "..."
    Dim t As Type = GetType(NotifyIcon)
    Dim hidden As Reflection.BindingFlags = Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance
    t.GetField("text", hidden).SetValue(ni, text)
    If CBool(t.GetField("added", hidden).GetValue(ni)) Then
      t.GetMethod("UpdateIcon", hidden).Invoke(ni, New Object() {True})
    End If
  End Sub
  Private Sub SetTrayText()
    Dim sTrayText As String = "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu"
    Using myRegKey As Microsoft.Win32.RegistryKey = GetRegKey(False)
      Select Case myRegKey.GetValue("KeyboardShortcut", 0)
        Case 1 : SetNotifyIconText(trayStandBy, sTrayText & vbNewLine & "Standby Shortcut: Ctrl+Alt+D")
        Case 2 : SetNotifyIconText(trayStandBy, sTrayText & vbNewLine & "Standby Shortcut: Ctrl+Alt+M")
        Case 3 : SetNotifyIconText(trayStandBy, sTrayText & vbNewLine & "Standby Shortcut: Ctrl+Alt+S")
        Case 4 : SetNotifyIconText(trayStandBy, sTrayText & vbNewLine & "Standby Shortcut: Ctrl+Alt+V")
        Case 5 : SetNotifyIconText(trayStandBy, sTrayText & vbNewLine & "Standby Shortcut: Alt+Shift+D")
        Case 6 : SetNotifyIconText(trayStandBy, sTrayText & vbNewLine & "Standby Shortcut: Alt+Shift+M")
        Case 7 : SetNotifyIconText(trayStandBy, sTrayText & vbNewLine & "Standby Shortcut: Alt+Shift+S")
        Case 8 : SetNotifyIconText(trayStandBy, sTrayText & vbNewLine & "Standby Shortcut: Alt+Shift+V")
        Case 9 : SetNotifyIconText(trayStandBy, sTrayText & vbNewLine & "Standby Shortcut: Win+S")
        Case 10 : SetNotifyIconText(trayStandBy, sTrayText & vbNewLine & "Standby Shortcut: Win+V")
        Case Else : SetNotifyIconText(trayStandBy, sTrayText)
      End Select
    End Using
  End Sub
  Private Sub SetTrayMenus()
    mnuShortcutDisabled.Checked = False
    mnuShortcutASD.Checked = False
    mnuShortcutASM.Checked = False
    mnuShortcutASS.Checked = False
    mnuShortcutASV.Checked = False
    mnuShortcutCAD.Checked = False
    mnuShortcutCAM.Checked = False
    mnuShortcutCAS.Checked = False
    mnuShortcutCAV.Checked = False
    mnuShortcutWS.Checked = False
    mnuShortcutWV.Checked = False
    Using myRegKey As Microsoft.Win32.RegistryKey = GetRegKey(False)
      Select Case myRegKey.GetValue("KeyboardShortcut", 0)
        Case 1 : mnuShortcutCAD.Checked = True
        Case 2 : mnuShortcutCAM.Checked = True
        Case 3 : mnuShortcutCAS.Checked = True
        Case 4 : mnuShortcutCAV.Checked = True
        Case 5 : mnuShortcutASD.Checked = True
        Case 6 : mnuShortcutASM.Checked = True
        Case 7 : mnuShortcutASS.Checked = True
        Case 8 : mnuShortcutASV.Checked = True
        Case 9 : mnuShortcutWS.Checked = True
        Case 10 : mnuShortcutWV.Checked = True
        Case Else : mnuShortcutDisabled.Checked = True
      End Select
    End Using
  End Sub
  Private Sub DisableTrayMenus()
    Dim activeKey As UInt32 = GetRegKey(False).GetValue("KeyboardShortcut", 0)
    For I As Byte = 1 To 10
      Dim hotKeyd As Boolean = False
      If I = activeKey Then
        hotKeyd = True
      Else
        Select Case I
          Case 1 : hotKeyd = RegisterHotKey(Me.Handle, 2, MOD_.Control Or MOD_.Alt Or MOD_.NoRepeat, Keys.D)
          Case 2 : hotKeyd = RegisterHotKey(Me.Handle, 2, MOD_.Control Or MOD_.Alt Or MOD_.NoRepeat, Keys.M)
          Case 3 : hotKeyd = RegisterHotKey(Me.Handle, 2, MOD_.Control Or MOD_.Alt Or MOD_.NoRepeat, Keys.S)
          Case 4 : hotKeyd = RegisterHotKey(Me.Handle, 2, MOD_.Control Or MOD_.Alt Or MOD_.NoRepeat, Keys.V)
          Case 5 : hotKeyd = RegisterHotKey(Me.Handle, 2, MOD_.Alt Or MOD_.Shift Or MOD_.NoRepeat, Keys.D)
          Case 6 : hotKeyd = RegisterHotKey(Me.Handle, 2, MOD_.Alt Or MOD_.Shift Or MOD_.NoRepeat, Keys.M)
          Case 7 : hotKeyd = RegisterHotKey(Me.Handle, 2, MOD_.Alt Or MOD_.Shift Or MOD_.NoRepeat, Keys.S)
          Case 8 : hotKeyd = RegisterHotKey(Me.Handle, 2, MOD_.Alt Or MOD_.Shift Or MOD_.NoRepeat, Keys.V)
          Case 9 : hotKeyd = RegisterHotKey(Me.Handle, 2, MOD_.Win Or MOD_.NoRepeat, Keys.S)
          Case 10 : hotKeyd = RegisterHotKey(Me.Handle, 2, MOD_.Win Or MOD_.NoRepeat, Keys.V)
        End Select
        UnregisterHotKey(Me.Handle, 2)
      End If
      Select Case I
        Case 1 : mnuShortcutCAD.Enabled = hotKeyd
        Case 2 : mnuShortcutCAM.Enabled = hotKeyd
        Case 3 : mnuShortcutCAS.Enabled = hotKeyd
        Case 4 : mnuShortcutCAV.Enabled = hotKeyd
        Case 5 : mnuShortcutASD.Enabled = hotKeyd
        Case 6 : mnuShortcutASM.Enabled = hotKeyd
        Case 7 : mnuShortcutASS.Enabled = hotKeyd
        Case 8 : mnuShortcutASV.Enabled = hotKeyd
        Case 9 : mnuShortcutWS.Enabled = hotKeyd
        Case 10 : mnuShortcutWV.Enabled = hotKeyd
      End Select
    Next
  End Sub
#End Region
#Region "Menus"
  Private Sub mnuTray_Popup(sender As System.Object, e As EventArgs) Handles mnuTray.Popup
    mnuStartUp.Checked = My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.Programs & "\Startup\Please Stand By.lnk")
    SetTrayMenus()
  End Sub
  Private Sub mnuStandby_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuStandby.Click
    WaitForIdle()
  End Sub
  Private Sub mnuStartUp_Click(sender As Object, e As System.EventArgs) Handles mnuStartUp.Click
    If Not My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.Programs & "\Startup\Please Stand By.lnk") Then
      CreateShortCut()
    Else
      If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.Programs & "\Startup\Please Stand By.lnk") Then My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.SpecialDirectories.Programs & "\Startup\Please Stand By.lnk")
    End If
  End Sub
  Private Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuExit.Click
    Application.Exit()
  End Sub
  Private Sub mnuShortcutDisabled_Click(sender As System.Object, e As System.EventArgs) Handles mnuShortcutDisabled.Click
    GetRegKey(True).DeleteValue("KeyboardShortcut")
    UnregisterHotKey(Me.Handle, 1)
    SetTrayText()
  End Sub
  Private Sub mnuShortcutCAD_Click(sender As System.Object, e As System.EventArgs) Handles mnuShortcutCAD.Click
    GetRegKey(True).SetValue("KeyboardShortcut", 1, Microsoft.Win32.RegistryValueKind.DWord)
    UnregisterHotKey(Me.Handle, 1)
    If Not ApplyHotkey() Then MsgBox("The Keyboard Shortcut ""Ctrl+Alt+D"" is already in use!" & vbNewLine & "Please select a different Keyboard Shortcut to use with Please Stand By...", MsgBoxStyle.Exclamation Or MsgBoxStyle.SystemModal, "Please Stand By... Keyboard Shortcut")
    SetTrayText()
  End Sub
  Private Sub mnuShortcutCAM_Click(sender As System.Object, e As System.EventArgs) Handles mnuShortcutCAM.Click
    GetRegKey(True).SetValue("KeyboardShortcut", 2, Microsoft.Win32.RegistryValueKind.DWord)
    UnregisterHotKey(Me.Handle, 1)
    If Not ApplyHotkey() Then MsgBox("The Keyboard Shortcut ""Ctrl+Alt+M"" is already in use!" & vbNewLine & "Please select a different Keyboard Shortcut to use with Please Stand By...", MsgBoxStyle.Exclamation Or MsgBoxStyle.SystemModal, "Please Stand By... Keyboard Shortcut")
    SetTrayText()
  End Sub
  Private Sub mnuShortcutCAS_Click(sender As System.Object, e As System.EventArgs) Handles mnuShortcutCAS.Click
    GetRegKey(True).SetValue("KeyboardShortcut", 3, Microsoft.Win32.RegistryValueKind.DWord)
    UnregisterHotKey(Me.Handle, 1)
    If Not ApplyHotkey() Then MsgBox("The Keyboard Shortcut ""Ctrl+Alt+S"" is already in use!" & vbNewLine & "Please select a different Keyboard Shortcut to use with Please Stand By...", MsgBoxStyle.Exclamation Or MsgBoxStyle.SystemModal, "Please Stand By... Keyboard Shortcut")
    SetTrayText()
  End Sub
  Private Sub mnuShortcutCAV_Click(sender As System.Object, e As System.EventArgs) Handles mnuShortcutCAV.Click
    GetRegKey(True).SetValue("KeyboardShortcut", 4, Microsoft.Win32.RegistryValueKind.DWord)
    UnregisterHotKey(Me.Handle, 1)
    If Not ApplyHotkey() Then MsgBox("The Keyboard Shortcut ""Ctrl+Alt+V"" is already in use!" & vbNewLine & "Please select a different Keyboard Shortcut to use with Please Stand By...", MsgBoxStyle.Exclamation Or MsgBoxStyle.SystemModal, "Please Stand By... Keyboard Shortcut")
    SetTrayText()
  End Sub
  Private Sub mnuShortcutASD_Click(sender As System.Object, e As System.EventArgs) Handles mnuShortcutASD.Click
    GetRegKey(True).SetValue("KeyboardShortcut", 5, Microsoft.Win32.RegistryValueKind.DWord)
    UnregisterHotKey(Me.Handle, 1)
    If Not ApplyHotkey() Then MsgBox("The Keyboard Shortcut ""Alt+Shift+D"" is already in use!" & vbNewLine & "Please select a different Keyboard Shortcut to use with Please Stand By...", MsgBoxStyle.Exclamation Or MsgBoxStyle.SystemModal, "Please Stand By... Keyboard Shortcut")
    SetTrayText()
  End Sub
  Private Sub mnuShortcutASM_Click(sender As System.Object, e As System.EventArgs) Handles mnuShortcutASM.Click
    GetRegKey(True).SetValue("KeyboardShortcut", 6, Microsoft.Win32.RegistryValueKind.DWord)
    UnregisterHotKey(Me.Handle, 1)
    If Not ApplyHotkey() Then MsgBox("The Keyboard Shortcut ""Alt+Shift+M"" is already in use!" & vbNewLine & "Please select a different Keyboard Shortcut to use with Please Stand By...", MsgBoxStyle.Exclamation Or MsgBoxStyle.SystemModal, "Please Stand By... Keyboard Shortcut")
    SetTrayText()
  End Sub
  Private Sub mnuShortcutASS_Click(sender As System.Object, e As System.EventArgs) Handles mnuShortcutASS.Click
    GetRegKey(True).SetValue("KeyboardShortcut", 7, Microsoft.Win32.RegistryValueKind.DWord)
    UnregisterHotKey(Me.Handle, 1)
    If Not ApplyHotkey() Then MsgBox("The Keyboard Shortcut ""Alt+Shift+S"" is already in use!" & vbNewLine & "Please select a different Keyboard Shortcut to use with Please Stand By...", MsgBoxStyle.Exclamation Or MsgBoxStyle.SystemModal, "Please Stand By... Keyboard Shortcut")
    SetTrayText()
  End Sub
  Private Sub mnuShortcutASV_Click(sender As System.Object, e As System.EventArgs) Handles mnuShortcutASV.Click
    GetRegKey(True).SetValue("KeyboardShortcut", 8, Microsoft.Win32.RegistryValueKind.DWord)
    UnregisterHotKey(Me.Handle, 1)
    If Not ApplyHotkey() Then MsgBox("The Keyboard Shortcut ""Alt+Shift+V"" is already in use!" & vbNewLine & "Please select a different Keyboard Shortcut to use with Please Stand By...", MsgBoxStyle.Exclamation Or MsgBoxStyle.SystemModal, "Please Stand By... Keyboard Shortcut")
    SetTrayText()
  End Sub
  Private Sub mnuShortcutWS_Click(sender As System.Object, e As System.EventArgs) Handles mnuShortcutWS.Click
    GetRegKey(True).SetValue("KeyboardShortcut", 9, Microsoft.Win32.RegistryValueKind.DWord)
    UnregisterHotKey(Me.Handle, 1)
    If Not ApplyHotkey() Then MsgBox("The Keyboard Shortcut ""Win+S"" is already in use!" & vbNewLine & "Please select a different Keyboard Shortcut to use with Please Stand By...", MsgBoxStyle.Exclamation Or MsgBoxStyle.SystemModal, "Please Stand By... Keyboard Shortcut")
    SetTrayText()
  End Sub
  Private Sub mnuShortcutWV_Click(sender As System.Object, e As System.EventArgs) Handles mnuShortcutWV.Click
    GetRegKey(True).SetValue("KeyboardShortcut", 10, Microsoft.Win32.RegistryValueKind.DWord)
    UnregisterHotKey(Me.Handle, 1)
    If Not ApplyHotkey() Then MsgBox("The Keyboard Shortcut ""Win+V"" is already in use!" & vbNewLine & "Please select a different Keyboard Shortcut to use with Please Stand By...", MsgBoxStyle.Exclamation Or MsgBoxStyle.SystemModal, "Please Stand By... Keyboard Shortcut")
    SetTrayText()
  End Sub
#End Region
#End Region
#Region "Helpful Functions"
  Private Function GetTickCount() As UInt64
    Dim tCount As Integer = Environment.TickCount
    If tCount > 0 Then Return tCount
    Return CULng(CLng(tCount) + CLng(UInt32.MaxValue + 1))
  End Function
  Private Function GetRegKey(ByVal WriteEnabled As Boolean) As Microsoft.Win32.RegistryKey
    If WriteEnabled Then
      If Not My.Computer.Registry.CurrentUser.OpenSubKey("Software").GetSubKeyNames.Contains(Application.CompanyName) Then My.Computer.Registry.CurrentUser.OpenSubKey("Software", True).CreateSubKey(Application.CompanyName)
      If Not My.Computer.Registry.CurrentUser.OpenSubKey("Software").OpenSubKey(Application.CompanyName).GetSubKeyNames.Contains(Application.ProductName) Then My.Computer.Registry.CurrentUser.OpenSubKey("Software", True).OpenSubKey(Application.CompanyName, True).CreateSubKey(Application.ProductName)
      Return My.Computer.Registry.CurrentUser.OpenSubKey("Software", True).OpenSubKey(Application.CompanyName, True).OpenSubKey(Application.ProductName, True)
    Else
      If My.Computer.Registry.CurrentUser.OpenSubKey("Software").GetSubKeyNames.Contains(Application.CompanyName) Then
        If My.Computer.Registry.CurrentUser.OpenSubKey("Software").OpenSubKey(Application.CompanyName).GetSubKeyNames.Contains(Application.ProductName) Then
          Return My.Computer.Registry.CurrentUser.OpenSubKey("Software").OpenSubKey(Application.CompanyName).OpenSubKey(Application.ProductName)
        End If
      End If
      Return Nothing
    End If
  End Function
  Public Sub CreateShortCut()
    Dim shell As New WshShell
    Dim shortCut As IWshRuntimeLibrary.IWshShortcut
    shortCut = CType(shell.CreateShortcut(My.Computer.FileSystem.SpecialDirectories.Programs & "\Startup\Please Stand By.lnk"), IWshRuntimeLibrary.IWshShortcut)
    With shortCut
      .TargetPath = Application.ExecutablePath
      .WindowStyle = 1
      .Description = "Please Stand By..."
      .WorkingDirectory = Application.StartupPath
      .IconLocation = Application.ExecutablePath & ",0"
      .Save()
    End With
  End Sub
  Private Function ApplyHotkey() As Boolean
    Using myRegKey As Microsoft.Win32.RegistryKey = GetRegKey(True)
      If Not myRegKey.GetValueNames.Contains("KeyboardShortcut") Then Return False
      Dim hotKeyd As Boolean = False
      Select Case myRegKey.GetValue("KeyboardShortcut", 0)
        Case 1 : hotKeyd = RegisterHotKey(Me.Handle, 1, MOD_.Control Or MOD_.Alt Or MOD_.NoRepeat, Keys.D)
        Case 2 : hotKeyd = RegisterHotKey(Me.Handle, 1, MOD_.Control Or MOD_.Alt Or MOD_.NoRepeat, Keys.M)
        Case 3 : hotKeyd = RegisterHotKey(Me.Handle, 1, MOD_.Control Or MOD_.Alt Or MOD_.NoRepeat, Keys.S)
        Case 4 : hotKeyd = RegisterHotKey(Me.Handle, 1, MOD_.Control Or MOD_.Alt Or MOD_.NoRepeat, Keys.V)
        Case 5 : hotKeyd = RegisterHotKey(Me.Handle, 1, MOD_.Alt Or MOD_.Shift Or MOD_.NoRepeat, Keys.D)
        Case 6 : hotKeyd = RegisterHotKey(Me.Handle, 1, MOD_.Alt Or MOD_.Shift Or MOD_.NoRepeat, Keys.M)
        Case 7 : hotKeyd = RegisterHotKey(Me.Handle, 1, MOD_.Alt Or MOD_.Shift Or MOD_.NoRepeat, Keys.S)
        Case 8 : hotKeyd = RegisterHotKey(Me.Handle, 1, MOD_.Alt Or MOD_.Shift Or MOD_.NoRepeat, Keys.V)
        Case 9 : hotKeyd = RegisterHotKey(Me.Handle, 1, MOD_.Win Or MOD_.NoRepeat, Keys.S)
        Case 10 : hotKeyd = RegisterHotKey(Me.Handle, 1, MOD_.Win Or MOD_.NoRepeat, Keys.V)
      End Select
      If hotKeyd Then Return True
      myRegKey.DeleteValue("KeyboardShortcut")
    End Using
    Return False
  End Function
  Private WaitStart As Integer
  Private Sub WaitForIdle()
    SyncLock tmrIdle
      If tmrIdle.Enabled Then tmrIdle.Stop()
      trayStandBy.Icon = My.Resources.ico0
      WaitStart = Environment.TickCount
      tmrIdle.Start()
    End SyncLock
  End Sub
  Private Sub MonitorStandby()
    PostMessage(Me.Handle, &H112&, &HF170&, 2)
  End Sub
#End Region
End Class
