<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
  Inherits System.Windows.Forms.Form

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Me.trayStandBy = New System.Windows.Forms.NotifyIcon(Me.components)
    Me.mnuTray = New System.Windows.Forms.ContextMenu()
    Me.mnuStandby = New System.Windows.Forms.MenuItem()
    Me.mnuShortcut = New System.Windows.Forms.MenuItem()
    Me.mnuShortcutDisabled = New System.Windows.Forms.MenuItem()
    Me.mnuShortcutCA = New System.Windows.Forms.MenuItem()
    Me.mnuShortcutCAD = New System.Windows.Forms.MenuItem()
    Me.mnuShortcutCAM = New System.Windows.Forms.MenuItem()
    Me.mnuShortcutCAS = New System.Windows.Forms.MenuItem()
    Me.mnuShortcutCAV = New System.Windows.Forms.MenuItem()
    Me.mnuShortcutAS = New System.Windows.Forms.MenuItem()
    Me.mnuShortcutASD = New System.Windows.Forms.MenuItem()
    Me.mnuShortcutASM = New System.Windows.Forms.MenuItem()
    Me.mnuShortcutASS = New System.Windows.Forms.MenuItem()
    Me.mnuShortcutASV = New System.Windows.Forms.MenuItem()
    Me.mnuShortcutW = New System.Windows.Forms.MenuItem()
    Me.mnuShortcutWS = New System.Windows.Forms.MenuItem()
    Me.mnuShortcutWV = New System.Windows.Forms.MenuItem()
    Me.mnuStartUp = New System.Windows.Forms.MenuItem()
    Me.mnuSpace = New System.Windows.Forms.MenuItem()
    Me.mnuExit = New System.Windows.Forms.MenuItem()
    Me.tmrHide = New System.Windows.Forms.Timer(Me.components)
    Me.tmrIdle = New System.Windows.Forms.Timer(Me.components)
    Me.SuspendLayout()
    '
    'trayStandBy
    '
    Me.trayStandBy.ContextMenu = Me.mnuTray
    Me.trayStandBy.Icon = Global.Please_Stand_By.My.Resources.Resources.icon
    Me.trayStandBy.Text = "Left Click: Monitor Standby" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Right Click: Menu"
    Me.trayStandBy.Visible = True
    '
    'mnuTray
    '
    Me.mnuTray.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuStandby, Me.mnuShortcut, Me.mnuStartUp, Me.mnuSpace, Me.mnuExit})
    '
    'mnuStandby
    '
    Me.mnuStandby.Index = 0
    Me.mnuStandby.Text = "Enable Monitor Standby"
    '
    'mnuShortcut
    '
    Me.mnuShortcut.Index = 1
    Me.mnuShortcut.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuShortcutDisabled, Me.mnuShortcutCA, Me.mnuShortcutAS, Me.mnuShortcutW})
    Me.mnuShortcut.Text = "Keyboard Shortcut"
    '
    'mnuShortcutDisabled
    '
    Me.mnuShortcutDisabled.Index = 0
    Me.mnuShortcutDisabled.Text = "Disabled"
    '
    'mnuShortcutCA
    '
    Me.mnuShortcutCA.Index = 1
    Me.mnuShortcutCA.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuShortcutCAD, Me.mnuShortcutCAM, Me.mnuShortcutCAS, Me.mnuShortcutCAV})
    Me.mnuShortcutCA.Text = "Ctrl+Alt"
    '
    'mnuShortcutCAD
    '
    Me.mnuShortcutCAD.Index = 0
    Me.mnuShortcutCAD.Text = "Ctrl+Alt+D"
    '
    'mnuShortcutCAM
    '
    Me.mnuShortcutCAM.Index = 1
    Me.mnuShortcutCAM.Text = "Ctrl+Alt+M"
    '
    'mnuShortcutCAS
    '
    Me.mnuShortcutCAS.Index = 2
    Me.mnuShortcutCAS.Text = "Ctrl+Alt+S"
    '
    'mnuShortcutCAV
    '
    Me.mnuShortcutCAV.Index = 3
    Me.mnuShortcutCAV.Text = "Ctrl+Alt+V"
    '
    'mnuShortcutAS
    '
    Me.mnuShortcutAS.Index = 2
    Me.mnuShortcutAS.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuShortcutASD, Me.mnuShortcutASM, Me.mnuShortcutASS, Me.mnuShortcutASV})
    Me.mnuShortcutAS.Text = "Alt+Shift"
    '
    'mnuShortcutASD
    '
    Me.mnuShortcutASD.Index = 0
    Me.mnuShortcutASD.Text = "Alt+Shift+D"
    '
    'mnuShortcutASM
    '
    Me.mnuShortcutASM.Index = 1
    Me.mnuShortcutASM.Text = "Alt+Shift+M"
    '
    'mnuShortcutASS
    '
    Me.mnuShortcutASS.Index = 2
    Me.mnuShortcutASS.Text = "Alt+Shift+S"
    '
    'mnuShortcutASV
    '
    Me.mnuShortcutASV.Index = 3
    Me.mnuShortcutASV.Text = "Alt+Shift+V"
    '
    'mnuShortcutW
    '
    Me.mnuShortcutW.Index = 3
    Me.mnuShortcutW.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuShortcutWS, Me.mnuShortcutWV})
    Me.mnuShortcutW.Text = "Win"
    '
    'mnuShortcutWS
    '
    Me.mnuShortcutWS.Index = 0
    Me.mnuShortcutWS.Text = "Win+S"
    '
    'mnuShortcutWV
    '
    Me.mnuShortcutWV.Index = 1
    Me.mnuShortcutWV.Text = "Win+V"
    '
    'mnuStartUp
    '
    Me.mnuStartUp.Index = 2
    Me.mnuStartUp.Text = "Start with Windows"
    '
    'mnuSpace
    '
    Me.mnuSpace.Index = 3
    Me.mnuSpace.Text = "-"
    '
    'mnuExit
    '
    Me.mnuExit.Index = 4
    Me.mnuExit.Text = "Exit"
    '
    'tmrHide
    '
    Me.tmrHide.Enabled = True
    Me.tmrHide.Interval = 10
    '
    'tmrIdle
    '
    Me.tmrIdle.Interval = 500
    '
    'frmMain
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(232, 143)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
    Me.Icon = Global.Please_Stand_By.My.Resources.Resources.icon
    Me.Name = "frmMain"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Please Stand By..."
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents trayStandBy As System.Windows.Forms.NotifyIcon
  Friend WithEvents tmrHide As System.Windows.Forms.Timer
  Friend WithEvents mnuTray As System.Windows.Forms.ContextMenu
  Friend WithEvents mnuStandby As System.Windows.Forms.MenuItem
  Friend WithEvents mnuSpace As System.Windows.Forms.MenuItem
  Friend WithEvents mnuExit As System.Windows.Forms.MenuItem
  Friend WithEvents mnuStartUp As System.Windows.Forms.MenuItem
  Friend WithEvents mnuShortcut As System.Windows.Forms.MenuItem
  Friend WithEvents mnuShortcutDisabled As System.Windows.Forms.MenuItem
  Friend WithEvents mnuShortcutCA As System.Windows.Forms.MenuItem
  Friend WithEvents mnuShortcutAS As System.Windows.Forms.MenuItem
  Friend WithEvents mnuShortcutCAD As System.Windows.Forms.MenuItem
  Friend WithEvents mnuShortcutCAM As System.Windows.Forms.MenuItem
  Friend WithEvents mnuShortcutCAS As System.Windows.Forms.MenuItem
  Friend WithEvents mnuShortcutCAV As System.Windows.Forms.MenuItem
  Friend WithEvents mnuShortcutASD As System.Windows.Forms.MenuItem
  Friend WithEvents mnuShortcutASM As System.Windows.Forms.MenuItem
  Friend WithEvents mnuShortcutASS As System.Windows.Forms.MenuItem
  Friend WithEvents mnuShortcutASV As System.Windows.Forms.MenuItem
  Friend WithEvents mnuShortcutW As System.Windows.Forms.MenuItem
  Friend WithEvents mnuShortcutWS As System.Windows.Forms.MenuItem
  Friend WithEvents mnuShortcutWV As System.Windows.Forms.MenuItem
  Friend WithEvents tmrIdle As System.Windows.Forms.Timer

End Class
