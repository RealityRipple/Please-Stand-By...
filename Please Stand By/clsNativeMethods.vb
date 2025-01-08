Imports System.Runtime.InteropServices
Public NotInheritable Class NativeMethods
  <DllImport("user32", SetLastError:=True, CharSet:=CharSet.Unicode)>
  Public Shared Function PostMessage(ByVal hWnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
  End Function
  <DllImport("user32", SetLastError:=True, CharSet:=CharSet.Unicode)>
  Public Shared Function RegisterHotKey(ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Boolean
  End Function
  <DllImport("user32", SetLastError:=True, CharSet:=CharSet.Unicode)>
  Public Shared Function UnregisterHotKey(ByVal hwnd As IntPtr, ByVal id As Integer) As Boolean
  End Function
  <StructLayout(LayoutKind.Sequential)>
  Public Structure LASTINPUTINFO
    Public cbSize As UInteger
    Public dwTime As UInteger
  End Structure
  <DllImport("user32", setlasterror:=True, CharSet:=CharSet.Unicode)>
  Public Shared Function GetLastInputInfo(ByRef inputInfo As LASTINPUTINFO) As Boolean
  End Function
  <DllImport("kernel32", SetLastError:=True, CharSet:=CharSet.Unicode)>
  Public Shared Function WritePrivateProfileStringW(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
  End Function
  <DllImport("kernel32", SetLastError:=True, CharSet:=CharSet.Unicode)>
  Public Shared Function GetPrivateProfileStringW(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Int32, ByVal lpFileName As String) As Integer
  End Function
End Class
