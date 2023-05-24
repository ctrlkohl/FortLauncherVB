Imports System
Imports System.Diagnostics
Imports System.Runtime.InteropServices
Imports System.Text

Public Class UEVer
    <DllImport("version.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function GetFileVersionInfoSize(ByVal lptstrFilename As String, ByRef lpdwHandle As Integer) As Integer
    End Function

    <DllImport("version.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function GetFileVersionInfo(ByVal lptstrFilename As String, ByVal dwHandle As Integer, ByVal dwLen As Integer, ByVal lpData As Byte()) As Boolean
    End Function

    <DllImport("version.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function VerQueryValue(ByVal pBlock As Byte(), ByVal lpSubBlock As String, ByRef lplpBuffer As IntPtr, ByRef puLen As Integer) As Boolean
    End Function

    <StructLayout(LayoutKind.Sequential)>
    Private Structure VS_FIXEDFILEINFO
        Public dwSignature As Integer
        Public dwStrucVersion As Integer
        Public dwFileVersionMS As Integer
        Public dwFileVersionLS As Integer
        Public dwProductVersionMS As Integer
        Public dwProductVersionLS As Integer
        Public dwFileFlagsMask As Integer
        Public dwFileFlags As Integer
        Public dwFileOS As Integer
        Public dwFileType As Integer
        Public dwFileSubtype As Integer
        Public dwFileDateMS As Integer
        Public dwFileDateLS As Integer
    End Structure

    Public Shared Function GetEngineVersion(FNExecutable As String) As String
        Dim fileName As String = FNExecutable

        ' Get the file version info size
        Dim verSize As Integer = GetFileVersionInfoSize(fileName, 0)
        If verSize = 0 Then
            Console.WriteLine("Error getting file version info size: " & Marshal.GetLastWin32Error())
            Return "ERROR"
        End If

        ' Allocate memory for the file version info
        Dim verData(verSize - 1) As Byte
        If Not GetFileVersionInfo(fileName, 0, verSize, verData) Then
            Console.WriteLine("Error getting file version info: " & Marshal.GetLastWin32Error())
            Return "ERROR"
        End If

        ' Get the file version information
        Dim fileInfoPtr As IntPtr = IntPtr.Zero
        Dim fileInfoSize As Integer = 0
        If Not VerQueryValue(verData, "\", fileInfoPtr, fileInfoSize) Then
            Console.WriteLine("Error getting file version: " & Marshal.GetLastWin32Error())
            Return "ERROR"
        End If

        Dim fileInfo As VS_FIXEDFILEINFO = DirectCast(Marshal.PtrToStructure(fileInfoPtr, GetType(VS_FIXEDFILEINFO)), VS_FIXEDFILEINFO)

        ' Construct the file version string
        Dim fileVersion As String = GetHighWord(fileInfo.dwFileVersionMS).ToString() & "." &
                                GetLowWord(fileInfo.dwFileVersionMS).ToString() & "." &
                                GetHighWord(fileInfo.dwFileVersionLS).ToString() & "." &
                                GetLowWord(fileInfo.dwFileVersionLS).ToString()

        Return fileVersion
    End Function

    Private Shared Function GetHighWord(value As Integer) As Integer
        Return (value >> 16) And &HFFFF
    End Function

    Private Shared Function GetLowWord(value As Integer) As Integer
        Return value And &HFFFF
    End Function
    Private Function HIWORD(ByVal dword As Integer) As Integer
        Return (dword >> 16) And &HFFFF
    End Function

    Private Function LOWORD(ByVal dword As Integer) As Integer
        Return dword And &HFFFF
    End Function
End Class
