﻿Public Class Form_ZX81
    Public Globalvars As New Stack
    ''' <summary>
    ''' Uses Compiler Memory for Expressions
    ''' </summary>
    Public LocalVars As New Environment_Memory
    Public Sub Print(ByRef Userinput As String)
        Me.Zx81_DisplayScreen.Text = Userinput
    End Sub
    Public Sub CLS()
        Me.Zx81_DisplayScreen.Text = ""
    End Sub
    Public Function Input(ByRef Message As String) As String
        '    Default = "1"    ' Set default.
        ' Display message, title, and default value.
        Return InputBox(Message, "INPUT")
    End Function

End Class