﻿Public Class CPU_FUNCTIONS
    Public Function _Binary_op(ByRef Left As Integer, ByRef Right As Integer, ByRef iOperator As String) As String
        _Binary_op = 0
        Dim VM As STACK_VM.ZX81_CPU
        Dim PROGRAM As New List(Of String)
        Select Case iOperator
            Case "-"
                PROGRAM.Add("PUSH")
                PROGRAM.Add(Left.ToString)
                PROGRAM.Add("PUSH")
                PROGRAM.Add(Right.ToString)
                PROGRAM.Add("SUB")
                PROGRAM.Add("HALT")
                VM = New STACK_VM.ZX81_CPU(PROGRAM)
                Return VM.Peek()
            Case "+"
                PROGRAM.Add("PUSH")
                PROGRAM.Add(Left.ToString)
                PROGRAM.Add("PUSH")
                PROGRAM.Add(Right.ToString)
                PROGRAM.Add("ADD")
                PROGRAM.Add("HALT")
                VM = New STACK_VM.ZX81_CPU(PROGRAM)
                Return VM.Peek()
            Case "/"
                PROGRAM.Add("PUSH")
                PROGRAM.Add(Left.ToString)
                PROGRAM.Add("PUSH")
                PROGRAM.Add(Right.ToString)
                PROGRAM.Add("DIV")
                PROGRAM.Add("HALT")
                VM = New STACK_VM.ZX81_CPU(PROGRAM)
                Return VM.Peek()
            Case "*"
                PROGRAM.Add("PUSH")
                PROGRAM.Add(Left.ToString)
                PROGRAM.Add("PUSH")
                PROGRAM.Add(Right.ToString)
                PROGRAM.Add("MUL")
                PROGRAM.Add("HALT")
                VM = New STACK_VM.ZX81_CPU(PROGRAM)
                Return VM.Peek()
            Case ">"
                PROGRAM.Add("PUSH")
                PROGRAM.Add(Left.ToString)
                PROGRAM.Add("PUSH")
                PROGRAM.Add(Right.ToString)
                PROGRAM.Add("IS_GT")
                PROGRAM.Add("HALT")
                VM = New STACK_VM.ZX81_CPU(PROGRAM)
                If VM.Peek = 1 Then
                    Return "TRUE"
                Else
                    Return "FALSE"
                End If
            Case "<"
                PROGRAM.Add("PUSH")
                PROGRAM.Add(Left.ToString)
                PROGRAM.Add("PUSH")
                PROGRAM.Add(Right.ToString)
                PROGRAM.Add("IS_LT")
                PROGRAM.Add("HALT")
                VM = New STACK_VM.ZX81_CPU(PROGRAM)
                If VM.Peek = 1 Then
                    Return "TRUE"
                Else
                    Return "FALSE"
                End If
            Case ">="
                PROGRAM.Add("PUSH")
                PROGRAM.Add(Left.ToString)
                PROGRAM.Add("PUSH")
                PROGRAM.Add(Right.ToString)
                PROGRAM.Add("IS_GTE")
                PROGRAM.Add("HALT")
                VM = New STACK_VM.ZX81_CPU(PROGRAM)
                If VM.Peek = 1 Then
                    Return "TRUE"
                Else
                    Return "FALSE"
                End If
            Case "<="
                PROGRAM.Add("PUSH")
                PROGRAM.Add(Left.ToString)
                PROGRAM.Add("PUSH")
                PROGRAM.Add(Right.ToString)
                PROGRAM.Add("IS_LTE")
                PROGRAM.Add("HALT")
                VM = New STACK_VM.ZX81_CPU(PROGRAM)
                If VM.Peek = 1 Then
                    Return "TRUE"
                Else
                    Return "FALSE"
                End If
            Case "="
                PROGRAM.Add("PUSH")
                PROGRAM.Add(Left.ToString)
                PROGRAM.Add("PUSH")
                PROGRAM.Add(Right.ToString)
                PROGRAM.Add("IS_EQ")
                PROGRAM.Add("HALT")
                VM = New STACK_VM.ZX81_CPU(PROGRAM)
                If VM.Peek = 1 Then
                    Return "TRUE"
                Else
                    Return "FALSE"
                End If


        End Select

    End Function
    Public Sub _print(ByRef Str As String)

        Dim VM As STACK_VM.ZX81_CPU
        Dim PROGRAM As New List(Of String)
        PROGRAM.Add("PUSH")
        PROGRAM.Add(Str)
        PROGRAM.Add("PRINT")
        VM = New STACK_VM.ZX81_CPU(PROGRAM)
    End Sub

End Class