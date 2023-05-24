Imports System
Imports System.Threading
Public Class ConsoleHelper
    Public Shared Function writePasswordText(ByVal password As String) As String
        Dim fp As String = ""

        For i As Integer = 0 To password.Length - 1
            If i = 0 Then
                Console.Write(password(i))
                i += 1
            Else
                Console.Write("*")
            End If
        Next

        Console.WriteLine()
        Return fp
    End Function

    Public Shared Function writeSmoothColor(ByVal message As String, ByVal color As String) As String
        Dim consoleColor As ConsoleColor

        Select Case color.ToLower()
            Case "black"
                consoleColor = ConsoleColor.Black
            Case "darkblue"
                consoleColor = ConsoleColor.DarkBlue
            Case "darkgreen"
                consoleColor = ConsoleColor.DarkGreen
            Case "darkcyan"
                consoleColor = ConsoleColor.DarkCyan
            Case "darkred"
                consoleColor = ConsoleColor.DarkRed
            Case "darkmagenta"
                consoleColor = ConsoleColor.DarkMagenta
            Case "darkyellow"
                consoleColor = ConsoleColor.DarkYellow
            Case "gray"
                consoleColor = ConsoleColor.Gray
            Case "darkgray"
                consoleColor = ConsoleColor.DarkGray
            Case "blue"
                consoleColor = ConsoleColor.Blue
            Case "green"
                consoleColor = ConsoleColor.Green
            Case "cyan"
                consoleColor = ConsoleColor.Cyan
            Case "red"
                consoleColor = ConsoleColor.Red
            Case "magenta"
                consoleColor = ConsoleColor.Magenta
            Case "yellow"
                consoleColor = ConsoleColor.Yellow
            Case "white"
                consoleColor = ConsoleColor.White
            Case Else
                consoleColor = ConsoleColor.White
        End Select

        Console.ForegroundColor = consoleColor

        For i As Integer = 0 To message.Length - 1
            Console.Write(message(i))
            Thread.Sleep(35)
        Next

        Console.Write(" ")
        Console.ResetColor()
        Return ""
    End Function

    Public Shared Sub smoothWrite(ByVal message As String)
        For i As Integer = 0 To message.Length - 1
            Console.Write(message(i))
            Thread.Sleep(35)
        Next
        Console.WriteLine("")
    End Sub
End Class
