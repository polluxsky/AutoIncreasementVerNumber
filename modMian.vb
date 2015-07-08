Imports System.IO
Imports System.Text

Module modMian

    Sub Main()

        Try
            If My.Application.CommandLineArgs.Count <> 1 Then
                Throw New Exception("Invalid Agreement.")
            End If

            'Dim rs As New FileStream(My.Application.CommandLineArgs(0), FileMode.Open, FileAccess.ReadWrite, FileShare.Read) 
            Dim rs As New StreamReader(My.Application.CommandLineArgs(0), True)
            Dim newcontent As New StringBuilder
            While Not rs.EndOfStream
                Dim strline As String = rs.ReadLine
                If strline.StartsWith("<Assembly: AssemblyVersion") Then
                    Dim strVer As String = strline.Split("""")(1)
                    Dim arrVer() As String = strVer.Split(".")
                    '修订号 自增
                    If CInt(arrVer(3)) = 8000 Then
                        arrVer(3) = "0"
                        arrVer(2) = CInt(arrVer(2)) + 1
                        '生成号 自增
                        If CInt(arrVer(2)) = 99 Then
                            arrVer(2) = "0"
                            arrVer(1) = CInt(arrVer(1)) + 1
                            '次版本号自增
                            If CInt(arrVer(1)) = 9 Then
                                arrVer(1) = "0"
                                arrVer(0) = CInt(arrVer(0)) + 1
                            Else
                                arrVer(1) = CInt(arrVer(1)) + 1
                            End If
                        Else
                            arrVer(2) = CInt(arrVer(2)) + 1
                        End If

                    Else
                        arrVer(3) = CInt(arrVer(3)) + 1
                    End If
                    strline = strline.Replace(strVer, arrVer(0) & "." & arrVer(1) & "." & arrVer(2) & "." & arrVer(3))
                End If

                If strline.StartsWith("<Assembly: AssemblyFileVersion") Then
                    Dim strFileVer As String = strline.Split("""")(1)
                    Dim arrVer() As String = strFileVer.Split(".")
                    '修订号 自增
                    If CInt(arrVer(3)) = 8000 Then
                        arrVer(3) = "0"
                        arrVer(2) = CInt(arrVer(2)) + 1
                        '生成号 自增
                        If CInt(arrVer(2)) = 99 Then
                            arrVer(2) = "0"
                            arrVer(1) = CInt(arrVer(1)) + 1
                            '次版本号自增
                            If CInt(arrVer(1)) = 9 Then
                                arrVer(1) = "0"
                                arrVer(0) = CInt(arrVer(0)) + 1
                            Else
                                arrVer(1) = CInt(arrVer(1)) + 1
                            End If
                        Else
                            arrVer(2) = CInt(arrVer(2)) + 1
                        End If

                    Else
                        arrVer(3) = CInt(arrVer(3)) + 1
                    End If
                    strline = strline.Replace(strFileVer, arrVer(0) & "." & arrVer(1) & "." & arrVer(2) & "." & arrVer(3))
                End If
                newcontent.AppendLine(strline)
            End While
            rs.Close()
            Dim ws As New StreamWriter(My.Application.CommandLineArgs(0), False)
            ws.WriteLine(newcontent.ToString)
            ws.Close()
        Catch ex As Exception
            'Console.Write(ex.Message)
        End Try
        
        'Console.ReadKey()
    End Sub

End Module
