﻿Namespace My
  ' The following events are available for MyApplication:
  ' Startup: Raised when the application starts, before the startup form is created.
  ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
  ' UnhandledException: Raised if the application encounters an unhandled exception.
  ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
  ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
  Partial Friend Class MyApplication
    Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
      Dim v As Authenticode.Validity = Authenticode.IsSelfSigned(Reflection.Assembly.GetExecutingAssembly().Location)
      If Not (v = Authenticode.Validity.SignedAndValid Or v = Authenticode.Validity.SignedButUntrusted) Then
        Dim sErr As String = "0x" & v.ToString("x")
        If Not CStr(v) = v.ToString Then sErr = v.ToString & " (0x" & v.ToString("x") & ")"
        If MsgBox("The Executable """ & IO.Path.GetFileName(Reflection.Assembly.GetExecutingAssembly().Location) & """ is not signed and may be corrupted or modified." & vbNewLine & "Error Code: " & sErr & vbNewLine & vbNewLine & "Would you like to continue loading " & My.Application.Info.ProductName & " anyway?", MsgBoxStyle.Critical Or MsgBoxStyle.SystemModal Or MsgBoxStyle.YesNo, My.Application.Info.ProductName) = MsgBoxResult.No Then
          e.Cancel = True
          Return
        End If
      End If
      If e.CommandLine.Contains("/uninstall") Then
        cSettings.RemoveAll()
        e.Cancel = True
        Return
      End If
    End Sub
    Private Sub MyApplication_StartupNextInstance(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
      If e.CommandLine.Contains("/uninstall") Then
        cSettings.RemoveAll()
        System.Windows.Forms.Application.Exit()
        Return
      End If
    End Sub
  End Class
End Namespace
