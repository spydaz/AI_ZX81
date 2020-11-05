Imports AI_ZX81.ConcreteExpressions
Imports AI_ZX81.ZX81_RAM

Namespace STACK_VM


    ''' <summary>
    ''' Virtual Machine
    ''' </summary>
    Public Class ZX81_VM
        Private iProgram As List(Of List(Of AbstractSyntax))
        Public ReadOnly Property Program As List(Of List(Of AbstractSyntax))
            Get
                Return iProgram
            End Get
        End Property
        Public ReadOnly Property Name As String
            Get
                Return iname
            End Get
        End Property
        Private iname As String = ""
        Private Shared iCPU As STACK_VM.ZX81_CPU
        Public ReadOnly Property CPU As STACK_VM.ZX81_CPU
            Get
                Return iCPU
            End Get
        End Property
        ''' <summary>
        ''' To be used to store variables from functions/Expressions etc
        ''' each string should be looked up in memeory
        ''' </summary>
        Public ReadOnly Property RAM As ZX81_RAM
            Get
                Return iRAM
            End Get
        End Property
        Private iRAM As ZX81_RAM
        Public Sub New(ByRef iName As String)
            Me.iname = iName
            iRAM = New ZX81_RAM
            iCPU = New STACK_VM.ZX81_CPU(Name)
            iProgram = New List(Of List(Of AbstractSyntax))
        End Sub
        ''' <summary>
        ''' Executes Program on CPU stack
        ''' </summary>
        Public Sub ExecuteProgram()
            Dim Prog As New List(Of String)
            For Each ITEM In Program
                For Each TOK In ITEM
                    Select Case TOK.SyntaxName
                        Case "_PRINT"
                            Prog.AddRange(_print(""))
                        Case "_PRINT_STR"
                            Prog.AddRange(_print(TOK.RequiredTokens(1).TokenValue))
                        Case "_PRINT_INT"
                            Prog.AddRange(_print(TOK.RequiredTokens(1).TokenValue))
                        Case "_PRINT_BOOL"
                            Prog.AddRange(_print(TOK.RequiredTokens(1).TokenValue))
                        Case "_PRINT_VARIABLE"
                            Prog.AddRange(_print(RAM.GetVar(TOK.RequiredTokens(1).TokenValue)))
                        Case "Math_Operation"
                            'Token (1) = Operator
                            If TOK.RequiredTokens.Count > 0 Then
                                Dim iOperator As String = TOK.RequiredTokens(1).TokenValue
                                Prog.AddRange(_Binary_op(GetValue(TOK.RequiredTokens(0).TokenValue), GetValue(TOK.RequiredTokens(2).TokenValue), iOperator))
                            End If
                        Case "Conditional_Operation"
                            'Token (1) = Operator
                            If TOK.RequiredTokens.Count > 0 Then
                                Dim iOperator As String = TOK.RequiredTokens(1).TokenValue
                                Prog.AddRange(_Binary_op(GetValue(TOK.RequiredTokens(0).TokenValue), GetValue(TOK.RequiredTokens(2).TokenValue), iOperator))
                            End If
                        Case "_DIM_AS"
                            If TOK.RequiredTokens.Count = 4 Then
                                _DIM_AS(TOK.RequiredTokens(1).TokenValue, TOK.RequiredTokens(3).TokenValue)
                            Else
                            End If

                        Case "_DIM_AS_EQ"
                            If TOK.RequiredTokens.Count = 6 Then
                                _DIM_AS(TOK.RequiredTokens(1).TokenValue, TOK.RequiredTokens(3).TokenValue, TOK.RequiredTokens(5).TokenValue)
                            Else
                                If TOK.RequiredTokens.Count = 3 Then
                                    _DIM_AS(TOK.RequiredTokens(1).TokenValue, TOK.RequiredTokens(3).TokenValue)
                                Else
                                End If
                            End If
                        Case "ASSIGN_EQUALS"
                            _VAR_EQ_VALUE(TOK.RequiredTokens(0).TokenValue, TOK.RequiredTokens(2).TokenValue)
                        Case "_NEXT"
                            'GET AND iNCREMENT vALUE 
                            'UPDATE THE ENVIRONMENT VARIABLE
                            Dim X As Integer = GetValue(TOK.RequiredTokens(0).TokenValue)
                            RAM.UpdateVar(TOK.RequiredTokens(0).TokenValue, X + 1)
                            'Required to jump back to for....
                        Case "_FOR"



                    End Select
                Next
            Next

            Prog.Add("HALT")
            iCPU.LoadProgram(Prog)
            iCPU.RUN()
        End Sub
        ''' <summary>
        ''' returns the value if it is a var it is returned as a value
        ''' </summary>
        ''' <param name="Val"></param>
        ''' <returns></returns>
        Private Function GetValue(ByRef Val As String) As Integer
            Try
                Dim XVal As Integer = Integer.Parse(Val)
                Return XVal
            Catch ex As Exception
                'isvariable
                Dim XVal As Integer = Integer.Parse(RAM.GetVar(Val))
                Return XVal

            End Try

        End Function
        ''' <summary>
        ''' Sets type of variable
        ''' </summary>
        ''' <param name="iTEM"></param>
        ''' <param name="iTYPE"></param>
        Private Sub _DIM_AS(ByRef iTEM As String, ByRef iTYPE As String)
            Select Case iTYPE
                Case "INT"
                    Dim var As New Variable
                    var.iName = iTEM
                    var.iType = "INTEGER"
                    var.iValue = 0
                    iRAM.AddVar(var)
                Case "BOOLEAN"
                    Dim var As New Variable
                    var.iName = iTEM
                    var.iType = "BOOLEAN"
                    var.iValue = "FALSE"
                    iRAM.AddVar(var)
                Case "STRING"
                    Dim var As New Variable
                    var.iName = iTEM
                    var.iType = "STRING"
                    var.iValue = ""
                    iRAM.AddVar(var)
            End Select

        End Sub
        ''' <summary>
        ''' dims varibale as type and sets value
        ''' </summary>
        ''' <param name="iTEM"></param>
        ''' <param name="iTYPE"></param>
        ''' <param name="VALUE"></param>
        Private Sub _DIM_AS(ByRef iTEM As String, ByRef iTYPE As String, ByRef VALUE As String)
            Select Case iTYPE
                Case "INT"
                    Dim var As New Variable
                    var.iName = iTEM
                    var.iType = "INTEGER"
                    var.iValue = VALUE
                    iRAM.AddVar(var)
                Case "BOOLEAN"
                    Dim var As New Variable
                    var.iName = iTEM
                    var.iType = "BOOLEAN"
                    var.iValue = VALUE
                    iRAM.AddVar(var)
                Case "STRING"
                    Dim var As New Variable
                    var.iName = iTEM
                    var.iType = "STRING"
                    var.iValue = VALUE
                    iRAM.AddVar(var)
            End Select

        End Sub
        ''' <summary>
        ''' Sets var eq to value
        ''' </summary>
        ''' <param name="iTEM"></param>
        ''' <param name="VALUE"></param>
        Private Sub _VAR_EQ_VALUE(ByRef iTEM As String, ByRef VALUE As String)
            If iRAM.CheckVar(iTEM) = True Then
                iRAM.UpdateVar(iTEM, VALUE)

            Else
            End If
        End Sub
        ''' <summary>
        ''' Returns var value as string value
        ''' </summary>
        ''' <param name="iName"></param>
        ''' <returns></returns>
        Private Function GetVarValue(ByRef iName As String) As String
            Return iRAM.GetVar(iName)
        End Function
        Public Sub AddProgram(ByRef Program As List(Of AbstractSyntax))
            iProgram.Add(Program)
        End Sub
        Public Sub AddProgram(ByRef Statement As AbstractSyntax)
            Dim Prog As New List(Of AbstractSyntax)
            Prog.Add(Statement)
            iProgram.Add(Prog)
        End Sub
        Public Sub ClearProgram()
            iProgram = New List(Of List(Of AbstractSyntax))
        End Sub
        Public Sub SetProgram(ByRef ProgTree As List(Of List(Of AbstractSyntax)))
            Me.ClearProgram()
            iProgram = ProgTree
        End Sub
        Private Function _Binary_op(ByRef Left As Integer, ByRef Right As Integer, ByRef iOperator As String) As List(Of String)

            Dim PROGRAM As New List(Of String)
            Select Case iOperator
                Case "-"
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Left.ToString)
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Right.ToString)
                    PROGRAM.Add("SUB")
                    PROGRAM.Add("PRINT_M")

                Case "+"
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Left.ToString)
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Right.ToString)
                    PROGRAM.Add("ADD")
                    PROGRAM.Add("PRINT_M")

                Case "/"
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Left.ToString)
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Right.ToString)
                    PROGRAM.Add("DIV")
                    PROGRAM.Add("PRINT_M")

                Case "*"
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Left.ToString)
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Right.ToString)
                    PROGRAM.Add("MUL")
                    PROGRAM.Add("PRINT_M")
                Case ">"
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Left.ToString)
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Right.ToString)
                    PROGRAM.Add("IS_GT")
                    PROGRAM.Add("PRINT_M")

                Case "<"
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Left.ToString)
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Right.ToString)
                    PROGRAM.Add("IS_LT")
                    PROGRAM.Add("PRINT_M")

                Case ">="
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Left.ToString)
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Right.ToString)
                    PROGRAM.Add("IS_GTE")
                    PROGRAM.Add("PRINT_M")

                Case "<="
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Left.ToString)
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Right.ToString)
                    PROGRAM.Add("IS_LTE")
                    PROGRAM.Add("PRINT_M")

                Case "="
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Left.ToString)
                    PROGRAM.Add("PUSH")
                    PROGRAM.Add(Right.ToString)
                    PROGRAM.Add("IS_EQ")
                    PROGRAM.Add("PRINT_M")

            End Select
            Return PROGRAM
        End Function

        Private Function _print(ByRef Str As String) As List(Of String)

            Dim PROGRAM As New List(Of String)
            PROGRAM.Add("PUSH")
            PROGRAM.Add(Str.Replace("'", ""))
            PROGRAM.Add("PRINT_M")
            Return PROGRAM

        End Function
#Region "IF"
        ''' <summary>
        '''       If ["condition"] Then ["If-True"]  End
        ''' </summary>
        ''' <param name="_If">If ["condition"]</param>
        ''' <param name="_Then">Then ["If-True"]  End</param>
        Private Function _if_then(ByRef _If As List(Of String), ByRef _Then As List(Of String)) As List(Of String)
            Dim PROGRAM As New List(Of String)
            'ADD CONDITION
            PROGRAM.AddRange(_If)
            'JUMP TO END - IF FALSE
            PROGRAM.Add("JIF_F")
            PROGRAM.Add(_Then.Count)
            PROGRAM.AddRange(_Then)
            'END

            Return PROGRAM
        End Function
        ''' <summary>
        '''     If ["condition"] Then ["If-True"] ELSE ["If-False"] End
        ''' </summary>
        ''' <param name="_If">If ["condition"]</param>
        ''' <param name="_Then">Then ["If-True"]</param>
        ''' <param name="_Else">ELSE ["If-False"]</param>
        Private Function _if_then_else(ByRef _If As List(Of String), ByRef _Then As List(Of String), ByRef _Else As List(Of String)) As List(Of String)

            Dim PROGRAM As New List(Of String)
            'ADD CONDITION
            PROGRAM.AddRange(_If)
            'JUMP TO ELSE IF FALSE
            PROGRAM.Add("JIF_F")
            PROGRAM.Add(_If.Count + _Then.Count + 2)
            'THEN PART
            PROGRAM.AddRange(_Then)
            'JUMP TO END
            PROGRAM.Add("JMP")
            PROGRAM.Add(_If.Count + _Then.Count + 4 + _Else.Count)
            'ELSE PART
            PROGRAM.AddRange(_Else)
            'END
            Return PROGRAM
        End Function
#End Region
        Private Function for_value_to_value(ByRef Start As Integer, ByRef Finish As Integer)
            Dim PROGRAM As New List(Of String)
            PROGRAM.Add("PUSH")
            PROGRAM.Add(Start)
            PROGRAM.Add("STORE")
            PROGRAM.Add(0)
            PROGRAM.Add("PUSH")
            PROGRAM.Add(Finish)
            PROGRAM.Add("STORE")
            PROGRAM.Add(1)
            'Start Statments

            'BeginNext
            PROGRAM.Add("LOAD")
            PROGRAM.Add(1)
            PROGRAM.Add("LOAD")
            PROGRAM.Add(0)
            PROGRAM.Add("INCR")
            PROGRAM.Add("REMOVE")
            PROGRAM.Add(0)
            PROGRAM.Add("STORE")
            PROGRAM.Add(0)
            'LOOP
            PROGRAM.Add("JIF_LT")
            'LineBefore store
            PROGRAM.Add(8)
            'end of loop
            '-PROGRAM.Add("HALT")
            Return PROGRAM
        End Function


    End Class
    Public Enum instruction
        _halt
        _push
        _add
        _Sub
        _mul
        _div
        _Not
        _And
        _Or
        _pop
        _dup
        _is_eq
        _is_ge
        _is_gt
        _jmp
        _jif_t
        _jif_f
        _load
        _store
        _Call
        _ret
    End Enum

End Namespace
