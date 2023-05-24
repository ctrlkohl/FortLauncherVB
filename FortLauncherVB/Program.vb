Imports System.Diagnostics
Imports System.Reflection
Imports System.IO
Imports System.Text.Json

Module Program
    Sub Main(args As String())
        ' Choose your DLL
        Dim dllToInejct As String = "Example.dll" ' Customize this as you please

        If File.Exists("config.json") Then
            ConsoleHelper.smoothWrite(ConsoleHelper.writeSmoothColor("[FortLauncherVB]", "magenta") & "Do you want to load your previous configuration? (Yes/No)")

            Dim jsonString As String = File.ReadAllText("config.json")

            ' Parse the JSON string into a JsonDocument
            Dim jsonDocument As JsonDocument = JsonDocument.Parse(jsonString)

            ' Access the root element of the JSON document
            Dim rootElement As JsonElement = jsonDocument.RootElement

            ' Access the username and Fortnite path properties
            Dim username As String = rootElement.GetProperty("Username").GetString()
            Dim fortnitePath As String = rootElement.GetProperty("FortnitePath").GetString()
            Dim password31 As String = rootElement.GetProperty("Password").GetString()

            Console.WriteLine("Username: " & username)
            Console.WriteLine("Fortnite Path: " & fortnitePath)
            'Console.WriteLine("UE4: " + UEVer.GetEngineVersion("")) - THIS IS HOW IT WORKS (WOW!)
            Console.Write("Password: ")
            ConsoleHelper.writePasswordText(password31)

            ' Dispose the JsonDocument when done
            jsonDocument.Dispose()

            Console.Write("Option: ")
            Dim bRead As String = Console.ReadLine()

            If bRead.ToLower() = "yes" Then
                Console.Clear()
                ConsoleHelper.smoothWrite(ConsoleHelper.writeSmoothColor("[FortLauncherVB]", "magenta") & "Alright, Starting fortnite!")

                Dim process As Process = ProcessHelper.StartProcess(fortnitePath & "\FortniteGame\Binaries\Win64\FortniteLauncher.exe", True, "")
                Dim process2 As Process = ProcessHelper.StartProcess(fortnitePath & "\FortniteGame\Binaries\Win64\FortniteClient-Win64-Shipping_BE.exe", True, "")
                Dim process4 As Process = ProcessHelper.StartProcess(fortnitePath & "\FortniteGame\Binaries\Win64\FortniteClient-Win64-Shipping_EAC.exe", True, "")
                Dim process3 As Process = ProcessHelper.StartProcess(fortnitePath & "\FortniteGame\Binaries\Win64\FortniteClient-Win64-Shipping.exe", False, "-epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -nobe -fromfl=eac -caldera=eyJhbGciOiJFUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2NvdW50X2lkIjoiZmY0YzEyMjQ5NzU5NGI5MGJlMDk1OWYxOGM2NWQwOGIiLCJnZW5lcmF0ZWQiOjE2NDEwOTI1NjUsImNhbGRlcmFHdWlkIjoiODQ0ODdkZmMtMGMxNC00YTUyLWFmYjgtNGY1ZWM5YzQyMjg0IiwiYWNQcm92aWRlciI6IkJhdHRsRXllIiwibm90ZXMiOiIiLCJmYWxsYmFjayI6ZmFsc2V9.E74n07NqNGmPPJ7NnK9EewIIb2Yjj3YP6Ghqrsd2iBe8e-z-ZkUiUwIH0DTd78yB5UDBDXdzOKBdsD0Mdjy5_A -AUTH_TYPE=epic -AUTH_LOGIN=""" & username & """ -AUTH_PASSWORD=""" & password31 & """ -SKIPPATCHCHECK")
                ProcessHelper.InjectDll(process3.Id, Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), dllToInejct))
                process3.WaitForExit()
                process.Close()
                process2.Close()
                process3.Close()
                process4.Close()
            Else
                ConsoleHelper.smoothWrite(ConsoleHelper.writeSmoothColor("[FortLauncherVB]", "magenta") & "Welcome to FortLauncherVB! Enter your username and password to launch FortLauncherVB")

                Console.Write(ConsoleHelper.writeSmoothColor("[FortLauncherVB Config]", "red") & "Enter Username: ")
                Dim email As String = Console.ReadLine()

                Console.Write(ConsoleHelper.writeSmoothColor("[FortLauncherVB Config]", "red") & "Enter Password: ")
                Dim password As String = Console.ReadLine()

                Console.Clear()

                ConsoleHelper.smoothWrite(ConsoleHelper.writeSmoothColor("[FortLauncherVB]", "magenta") & "Perfect! now just put in your fortnite path (Make sure it's where your FortniteGame and Engine files are there)")

                Console.Write(ConsoleHelper.writeSmoothColor("[FortLauncherVB Config]", "red") & "Enter Fortnite Path: ")
                Dim fnpath As String = Console.ReadLine()

                ConsoleHelper.smoothWrite(ConsoleHelper.writeSmoothColor("[FortLauncherVB]", "magenta") & "Do you wish to save your configuration? (Yes/No)")

                Console.Write("Option: ")
                Dim opt As String = Console.ReadLine()

                If opt.ToLower() = "yes" Then
                    ' Create a JSON object with the username and Fortnite path
                    Dim jsonObject2 = New With {
                .Username = email,
                .FortnitePath = fnpath,
                .Password = password
            }

                    ' Convert the JSON object to a JSON string
                    Dim jsonString2 As String = JsonSerializer.Serialize(jsonObject2)

                    ' Write the JSON string to a file
                    File.WriteAllText("config.json", jsonString2)

                    Console.WriteLine("JSON file created successfully!")
                End If

                Console.Clear()
                ConsoleHelper.smoothWrite(ConsoleHelper.writeSmoothColor("[FortLauncherVB]", "magenta") & "Alright, Starting fortnite!")

                Dim process As Process = ProcessHelper.StartProcess(fnpath & "\FortniteGame\Binaries\Win64\FortniteLauncher.exe", True, "")
                Dim process2 As Process = ProcessHelper.StartProcess(fnpath & "\FortniteGame\Binaries\Win64\FortniteClient-Win64-Shipping_BE.exe", True, "")
                Dim process4 As Process = ProcessHelper.StartProcess(fnpath & "\FortniteGame\Binaries\Win64\FortniteClient-Win64-Shipping_EAC.exe", True, "")
                Dim process3 As Process = ProcessHelper.StartProcess(fnpath & "\FortniteGame\Binaries\Win64\FortniteClient-Win64-Shipping.exe", False, "-AUTH_TYPE=epic -AUTH_LOGIN=""" & email & """ -AUTH_PASSWORD=""" & password & """ -SKIPPATCHCHECK")
                ProcessHelper.InjectDll(process3.Id, Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), dllToInejct))
                process3.WaitForExit()
                process.Close()
                process2.Close()
                process3.Close()
                process4.Close()
            End If
        Else
            ConsoleHelper.smoothWrite(ConsoleHelper.writeSmoothColor("[FortLauncherVB]", "magenta") & "Welcome to FortLauncherVB! Enter your username and password to launch FortLauncherVB")

            Console.Write(ConsoleHelper.writeSmoothColor("[FortLauncherVB Config]", "red") & "Enter Username: ")
            Dim email As String = Console.ReadLine()

            Console.Write(ConsoleHelper.writeSmoothColor("[FortLauncherVB Config]", "red") & "Enter Password: ")
            Dim password As String = Console.ReadLine()

            Console.Clear()

            ConsoleHelper.smoothWrite(ConsoleHelper.writeSmoothColor("[FortLauncherVB]", "magenta") & "Perfect! now just put in your fortnite path (Make sure it's where your FortniteGame and Engine files are there)")

            Console.Write(ConsoleHelper.writeSmoothColor("[FortLauncherVB Config]", "red") & "Enter Fortnite Path: ")
            Dim fnpath As String = Console.ReadLine()

            ConsoleHelper.smoothWrite(ConsoleHelper.writeSmoothColor("[FortLauncherVB]", "magenta") & "Do you wish to save your configuration? (Yes/No)")

            Console.Write("Option: ")
            Dim opt As String = Console.ReadLine()

            If opt.ToLower() = "yes" Then
                ' Create a JSON object with the username and Fortnite path
                Dim jsonObject = New With {
            .Username = email,
            .FortnitePath = fnpath,
            .Password = password
        }

                ' Convert the JSON object to a JSON string
                Dim jsonString As String = JsonSerializer.Serialize(jsonObject)

                ' Write the JSON string to a file
                File.WriteAllText("config.json", jsonString)

                Console.WriteLine("JSON file created successfully!")
            End If

            Console.Clear()
            ConsoleHelper.smoothWrite(ConsoleHelper.writeSmoothColor("[FortLauncherVB]", "magenta") & "Alright, Starting fortnite!")

            Dim process As Process = ProcessHelper.StartProcess(fnpath & "\FortniteGame\Binaries\Win64\FortniteLauncher.exe", True, "")
            Dim process2 As Process = ProcessHelper.StartProcess(fnpath & "\FortniteGame\Binaries\Win64\FortniteClient-Win64-Shipping_BE.exe", True, "")
            Dim process4 As Process = ProcessHelper.StartProcess(fnpath & "\FortniteGame\Binaries\Win64\FortniteClient-Win64-Shipping_EAC.exe", True, "")
            Dim process3 As Process = ProcessHelper.StartProcess(fnpath & "\FortniteGame\Binaries\Win64\FortniteClient-Win64-Shipping.exe", False, "-AUTH_TYPE=epic -AUTH_LOGIN=""" & email & """ -AUTH_PASSWORD=""" & password & """ -SKIPPATCHCHECK")
            ProcessHelper.InjectDll(process3.Id, Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), dllToInejct))
            process3.WaitForExit()
            process.Close()
            process2.Close()
            process3.Close()
            process4.Close()
        End If
    End Sub
End Module
