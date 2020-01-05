Imports System.Runtime.InteropServices
Imports IWshRuntimeLibrary
Public Class frmMain
  <DllImport("user32", SetLastError:=True, CharSet:=CharSet.Unicode)>
  Private Shared Function PostMessage(ByVal hWnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
  End Function

  <DllImport("user32", SetLastError:=True, CharSet:=CharSet.Unicode)>
  Private Shared Function RegisterHotKey(ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Boolean
  End Function

  <DllImport("user32", SetLastError:=True, CharSet:=CharSet.Unicode)>
  Private Shared Function UnregisterHotKey(ByVal hwnd As IntPtr, ByVal id As Integer) As Boolean
  End Function

  <StructLayout(LayoutKind.Sequential)>
  Private Structure LASTINPUTINFO
    Public cbSize As UInteger
    Public dwTime As UInteger
  End Structure
  <DllImport("user32", setlasterror:=True, CharSet:=CharSet.Unicode)>
  Private Shared Function GetLastInputInfo(ByRef inputInfo As LASTINPUTINFO) As Boolean
  End Function

  Private Enum MOD_
    Alt = &H1
    Control = &H2
    Shift = &H4
    Win = &H8
    NoRepeat = &H4000
  End Enum

  Private Function GetTickCount() As UInt64
    Dim tCount As Integer = Environment.TickCount
    If tCount > 0 Then Return tCount
    Return CULng(CLng(tCount) + CLng(UInt32.MaxValue + 1))
  End Function

  Private Sub tmrHide_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrHide.Tick
    tmrHide.Enabled = False
    Me.Hide()
    If Application.UserAppDataRegistry.ValueCount > 0 Then
      Select Case Application.UserAppDataRegistry.GetValue("KeyboardShortcut", 0)
        Case 0
          SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu")
        Case 1
          If Not RegisterHotKey(Me.Handle, 1, MOD_.Control Or MOD_.Alt Or MOD_.NoRepeat, Keys.D) Then Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 0)
          SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu" & vbNewLine & "Standby Shortcut: Ctrl+Alt+D")
        Case 2
          If Not RegisterHotKey(Me.Handle, 1, MOD_.Control Or MOD_.Alt Or MOD_.NoRepeat, Keys.M) Then Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 0)
          SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu" & vbNewLine & "Standby Shortcut: Ctrl+Alt+M")
        Case 3
          If Not RegisterHotKey(Me.Handle, 1, MOD_.Control Or MOD_.Alt Or MOD_.NoRepeat, Keys.S) Then Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 0)
          SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu" & vbNewLine & "Standby Shortcut: Ctrl+Alt+S")
        Case 4
          If Not RegisterHotKey(Me.Handle, 1, MOD_.Control Or MOD_.Alt Or MOD_.NoRepeat, Keys.V) Then Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 0)
          SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu" & vbNewLine & "Standby Shortcut: Ctrl+Alt+V")
        Case 5
          If Not RegisterHotKey(Me.Handle, 1, MOD_.Alt Or MOD_.Shift Or MOD_.NoRepeat, Keys.D) Then Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 0)
          SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu" & vbNewLine & "Standby Shortcut: Alt+Shift+D")
        Case 6
          If Not RegisterHotKey(Me.Handle, 1, MOD_.Alt Or MOD_.Shift Or MOD_.NoRepeat, Keys.M) Then Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 0)
          SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu" & vbNewLine & "Standby Shortcut: Alt+Shift+M")
        Case 7
          If Not RegisterHotKey(Me.Handle, 1, MOD_.Alt Or MOD_.Shift Or MOD_.NoRepeat, Keys.S) Then Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 0)
          SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu" & vbNewLine & "Standby Shortcut: Alt+Shift+S")
        Case 8
          If Not RegisterHotKey(Me.Handle, 1, MOD_.Alt Or MOD_.Shift Or MOD_.NoRepeat, Keys.V) Then Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 0)
          SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu" & vbNewLine & "Standby Shortcut: Alt+Shift+V")
        Case 9
          If Not RegisterHotKey(Me.Handle, 1, MOD_.Win Or MOD_.NoRepeat, Keys.S) Then Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 0)
          SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu" & vbNewLine & "Standby Shortcut: Win+S")
        Case 10
          If Not RegisterHotKey(Me.Handle, 1, MOD_.Win Or MOD_.NoRepeat, Keys.V) Then Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 0)
          SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu" & vbNewLine & "Standby Shortcut: Win+V")
      End Select
    End If
  End Sub

  Private Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuExit.Click
    Application.Exit()
  End Sub

  Private Sub mnuStandby_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuStandby.Click
    WaitForIdle()
  End Sub

  Private Sub trayStandBy_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trayStandBy.MouseUp
    If e.Button = Windows.Forms.MouseButtons.Left Then WaitForIdle()
  End Sub

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

  Private Sub mnuStartUp_Click(sender As Object, e As System.EventArgs) Handles mnuStartUp.Click
    If Not My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.Programs & "\Startup\Please Stand By.lnk") Then
      CreateShortCut()
    Else
      If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.Programs & "\Startup\Please Stand By.lnk") Then My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.SpecialDirectories.Programs & "\Startup\Please Stand By.lnk")
    End If
  End Sub

  Private Sub mnuTray_Popup(sender As System.Object, e As EventArgs) Handles mnuTray.Popup
    mnuStartUp.Checked = My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.Programs & "\Startup\Please Stand By.lnk")
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
    If Application.UserAppDataRegistry.ValueCount > 0 Then
      Select Case Application.UserAppDataRegistry.GetValue("KeyboardShortcut", 0)
        Case 0 : mnuShortcutDisabled.Checked = True
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
      End Select
    Else
      mnuShortcutDisabled.Checked = True
    End If
  End Sub

  Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
    If m.Msg = &H312 Then
      If m.WParam.ToInt32 = 1 Then
        WaitForIdle()
      End If
    End If
    MyBase.WndProc(m)
  End Sub

  Private WaitStart As Integer
  Private Sub WaitForIdle()
    SyncLock tmrIdle
      If tmrIdle.Enabled Then tmrIdle.Stop()
      WaitStart = Environment.TickCount
      tmrIdle.Start()
    End SyncLock
  End Sub

  Private Sub tmrIdle_Tick(sender As System.Object, e As System.EventArgs) Handles tmrIdle.Tick
    tmrIdle.Stop()
    Dim inInfo As New LASTINPUTINFO
    inInfo.cbSize = Marshal.SizeOf(inInfo)
    If GetLastInputInfo(inInfo) Then
      Dim tickTime As ULong = GetTickCount()
      Dim lastTime As ULong = 0
      If tickTime > inInfo.dwTime Then lastTime = tickTime - inInfo.dwTime
      If lastTime > 5000 Then
        MonitorStandby()
      Else
        If (lastTime > 500) Or (Environment.TickCount - WaitStart < 15000) Then tmrIdle.Start()
      End If
    Else
      MonitorStandby()
    End If
  End Sub

  Private Sub MonitorStandby()
    PostMessage(Me.Handle, &H112&, &HF170&, 2)
  End Sub

  Private Sub mnuShortcutDisabled_Click(sender As System.Object, e As System.EventArgs) Handles mnuShortcutDisabled.Click
    Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 0)
    UnregisterHotKey(Me.Handle, 1)
    SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu")
  End Sub

  Private Sub mnuShortcutCAD_Click(sender As System.Object, e As System.EventArgs) Handles mnuShortcutCAD.Click
    Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 1)
    UnregisterHotKey(Me.Handle, 1)
    SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu" & vbNewLine & "Standby Shortcut: Ctrl+Alt+D")
    If Not RegisterHotKey(Me.Handle, 1, MOD_.Control Or MOD_.Alt Or MOD_.NoRepeat, Keys.D) Then
      MsgBox("The Keyboard Shortcut ""Ctrl+Alt+D"" is already in use!" & vbNewLine & "Please select a different Keyboard Shortcut to use with Please Stand By...", MsgBoxStyle.Exclamation Or MsgBoxStyle.SystemModal, "Please Stand By... Keyboard Shortcut")
      Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 0)
      SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu")
    End If
  End Sub

  Private Sub mnuShortcutCAM_Click(sender As System.Object, e As System.EventArgs) Handles mnuShortcutCAM.Click
    Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 2)
    UnregisterHotKey(Me.Handle, 1)
    SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu" & vbNewLine & "Standby Shortcut: Ctrl+Alt+M")
    If Not RegisterHotKey(Me.Handle, 1, MOD_.Control Or MOD_.Alt Or MOD_.NoRepeat, Keys.M) Then
      MsgBox("The Keyboard Shortcut ""Ctrl+Alt+M"" is already in use!" & vbNewLine & "Please select a different Keyboard Shortcut to use with Please Stand By...", MsgBoxStyle.Exclamation Or MsgBoxStyle.SystemModal, "Please Stand By... Keyboard Shortcut")
      Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 0)
      SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu")
    End If
  End Sub

  Private Sub mnuShortcutCAS_Click(sender As System.Object, e As System.EventArgs) Handles mnuShortcutCAS.Click
    Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 3)
    UnregisterHotKey(Me.Handle, 1)
    SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu" & vbNewLine & "Standby Shortcut: Ctrl+Alt+S")
    If Not RegisterHotKey(Me.Handle, 1, MOD_.Control Or MOD_.Alt Or MOD_.NoRepeat, Keys.S) Then
      MsgBox("The Keyboard Shortcut ""Ctrl+Alt+S"" is already in use!" & vbNewLine & "Please select a different Keyboard Shortcut to use with Please Stand By...", MsgBoxStyle.Exclamation Or MsgBoxStyle.SystemModal, "Please Stand By... Keyboard Shortcut")
      Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 0)
      SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu")
    End If
  End Sub

  Private Sub mnuShortcutCAV_Click(sender As System.Object, e As System.EventArgs) Handles mnuShortcutCAV.Click
    Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 4)
    UnregisterHotKey(Me.Handle, 1)
    SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu" & vbNewLine & "Standby Shortcut: Ctrl+Alt+V")
    If Not RegisterHotKey(Me.Handle, 1, MOD_.Control Or MOD_.Alt Or MOD_.NoRepeat, Keys.V) Then
      MsgBox("The Keyboard Shortcut ""Ctrl+Alt+V"" is already in use!" & vbNewLine & "Please select a different Keyboard Shortcut to use with Please Stand By...", MsgBoxStyle.Exclamation Or MsgBoxStyle.SystemModal, "Please Stand By... Keyboard Shortcut")
      Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 0)
      SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu")
    End If
  End Sub

  Private Sub mnuShortcutASD_Click(sender As System.Object, e As System.EventArgs) Handles mnuShortcutASD.Click
    Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 5)
    UnregisterHotKey(Me.Handle, 1)
    SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu" & vbNewLine & "Standby Shortcut: Alt+Shift+D")
    If Not RegisterHotKey(Me.Handle, 1, MOD_.Alt Or MOD_.Shift Or MOD_.NoRepeat, Keys.D) Then
      MsgBox("The Keyboard Shortcut ""Alt+Shift+D"" is already in use!" & vbNewLine & "Please select a different Keyboard Shortcut to use with Please Stand By...", MsgBoxStyle.Exclamation Or MsgBoxStyle.SystemModal, "Please Stand By... Keyboard Shortcut")
      Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 0)
      SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu")
    End If
  End Sub

  Private Sub mnuShortcutASM_Click(sender As System.Object, e As System.EventArgs) Handles mnuShortcutASM.Click
    Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 6)
    UnregisterHotKey(Me.Handle, 1)
    SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu" & vbNewLine & "Standby Shortcut: Alt+Shift+M")
    If Not RegisterHotKey(Me.Handle, 1, MOD_.Alt Or MOD_.Shift Or MOD_.NoRepeat, Keys.M) Then
      MsgBox("The Keyboard Shortcut ""Alt+Shift+M"" is already in use!" & vbNewLine & "Please select a different Keyboard Shortcut to use with Please Stand By...", MsgBoxStyle.Exclamation Or MsgBoxStyle.SystemModal, "Please Stand By... Keyboard Shortcut")
      Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 0)
      SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu")
    End If
  End Sub

  Private Sub mnuShortcutASS_Click(sender As System.Object, e As System.EventArgs) Handles mnuShortcutASS.Click
    Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 7)
    UnregisterHotKey(Me.Handle, 1)
    SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu" & vbNewLine & "Standby Shortcut: Alt+Shift+S")
    If Not RegisterHotKey(Me.Handle, 1, MOD_.Alt Or MOD_.Shift Or MOD_.NoRepeat, Keys.S) Then
      MsgBox("The Keyboard Shortcut ""Alt+Shift+S"" is already in use!" & vbNewLine & "Please select a different Keyboard Shortcut to use with Please Stand By...", MsgBoxStyle.Exclamation Or MsgBoxStyle.SystemModal, "Please Stand By... Keyboard Shortcut")
      Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 0)
      SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu")
    End If
  End Sub

  Private Sub mnuShortcutASV_Click(sender As System.Object, e As System.EventArgs) Handles mnuShortcutASV.Click
    Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 8)
    UnregisterHotKey(Me.Handle, 1)
    SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu" & vbNewLine & "Standby Shortcut: Alt+Shift+V")
    If Not RegisterHotKey(Me.Handle, 1, MOD_.Alt Or MOD_.Shift Or MOD_.NoRepeat, Keys.V) Then
      MsgBox("The Keyboard Shortcut ""Alt+Shift+V"" is already in use!" & vbNewLine & "Please select a different Keyboard Shortcut to use with Please Stand By...", MsgBoxStyle.Exclamation Or MsgBoxStyle.SystemModal, "Please Stand By... Keyboard Shortcut")
      Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 0)
      SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu")
    End If
  End Sub

  Private Sub mnuShortcutWS_Click(sender As System.Object, e As System.EventArgs) Handles mnuShortcutWS.Click
    Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 9)
    UnregisterHotKey(Me.Handle, 1)
    SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu" & vbNewLine & "Standby Shortcut: Win+S")
    If Not RegisterHotKey(Me.Handle, 1, MOD_.Win Or MOD_.NoRepeat, Keys.S) Then
      MsgBox("The Keyboard Shortcut ""Win+S"" is already in use!" & vbNewLine & "Please select a different Keyboard Shortcut to use with Please Stand By...", MsgBoxStyle.Exclamation Or MsgBoxStyle.SystemModal, "Please Stand By... Keyboard Shortcut")
      Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 0)
      SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu")
    End If
  End Sub

  Private Sub mnuShortcutWV_Click(sender As System.Object, e As System.EventArgs) Handles mnuShortcutWV.Click
    Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 10)
    UnregisterHotKey(Me.Handle, 1)
    SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu" & vbNewLine & "Standby Shortcut: Win+V")
    If Not RegisterHotKey(Me.Handle, 1, MOD_.Win Or MOD_.NoRepeat, Keys.V) Then
      MsgBox("The Keyboard Shortcut ""Win+V"" is already in use!" & vbNewLine & "Please select a different Keyboard Shortcut to use with Please Stand By...", MsgBoxStyle.Exclamation Or MsgBoxStyle.SystemModal, "Please Stand By... Keyboard Shortcut")
      Application.UserAppDataRegistry.SetValue("KeyboardShortcut", 0)
      SetNotifyIconText(trayStandBy, "Left Click: Monitor Standby" & vbNewLine & "Right Click: Menu")
    End If
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
End Class
