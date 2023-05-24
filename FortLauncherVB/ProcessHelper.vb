Imports System
Imports System.Diagnostics
Imports System.Runtime.InteropServices
Imports System.Text
Public Class ProcessHelper
    Public Shared Function StartProcess(ByVal path As String, ByVal shouldFreeze As Boolean, ByVal extraArgs As String) As Process
        Dim process As New Process()
        process.StartInfo.FileName = path
        process.StartInfo.Arguments = extraArgs
        process.Start()

        If shouldFreeze Then
            For Each thread As ProcessThread In process.Threads
                SuspendThread(OpenThread(2, False, thread.Id))
            Next
        End If

        Return process
    End Function

    Public Shared Sub InjectDll(ByVal processId As Integer, ByVal path As String)
        Dim hProcess As IntPtr = OpenProcess(1082, False, processId)
        Dim procAddress As IntPtr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA")
        Dim num As UInteger = CUInt((path.Length + 1) * Marshal.SizeOf(GetType(Char)))
        Dim intPtr As IntPtr = VirtualAllocEx(hProcess, IntPtr.Zero, num, 12288UI, 4UI)

        Dim bytes As Byte() = Encoding.Default.GetBytes(path)
        Dim uintPtr As UIntPtr
        WriteProcessMemory(hProcess, intPtr, bytes, num, uintPtr)
        CreateRemoteThread(hProcess, IntPtr.Zero, 0UI, procAddress, intPtr, 0UI, IntPtr.Zero)
    End Sub

    <DllImport("kernel32.dll")>
    Public Shared Function SuspendThread(ByVal hThread As IntPtr) As Integer
    End Function

    <DllImport("kernel32.dll")>
    Public Shared Function OpenThread(ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Boolean, ByVal dwThreadId As Integer) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Public Shared Function CloseHandle(ByVal hHandle As IntPtr) As Boolean
    End Function

    <DllImport("kernel32.dll")>
    Public Shared Function OpenProcess(ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Boolean, ByVal dwProcessId As Integer) As IntPtr
    End Function

    <DllImport("kernel32", CharSet:=CharSet.Ansi, ExactSpelling:=True, SetLastError:=True)>
    Public Shared Function GetProcAddress(ByVal hModule As IntPtr, ByVal procName As String) As IntPtr
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function GetModuleHandle(ByVal lpModuleName As String) As IntPtr
    End Function

    <DllImport("kernel32.dll", ExactSpelling:=True, SetLastError:=True)>
    Public Shared Function VirtualAllocEx(ByVal hProcess As IntPtr, ByVal lpAddress As IntPtr, ByVal dwSize As UInteger, ByVal flAllocationType As UInteger, ByVal flProtect As UInteger) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Public Shared Function WriteProcessMemory(ByVal hProcess As IntPtr, ByVal lpBaseAddress As IntPtr, ByVal lpBuffer As Byte(), ByVal nSize As UInteger, ByRef lpNumberOfBytesWritten As UIntPtr) As Boolean
    End Function

    <DllImport("kernel32.dll")>
    Public Shared Function CreateRemoteThread(ByVal hProcess As IntPtr, ByVal lpThreadAttributes As IntPtr, ByVal dwStackSize As UInteger, ByVal lpStartAddress As IntPtr, ByVal lpParameter As IntPtr, ByVal dwCreationFlags As UInteger, ByRef lpThreadId As IntPtr) As IntPtr
    End Function
End Class
