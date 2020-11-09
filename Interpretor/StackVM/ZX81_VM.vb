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
        Dim LineNumber As Integer = 0
        ''' <summary>
        ''' Executes Program on CPU stack
        ''' </summary>
        Public Sub ExecuteProgram()
            Dim str As New List(Of String)
            Dim Prog As New List(Of String)
            Try

                For Each ITEM In Program
                    LineNumber += 1





                    Prog.AddRange(ExecuteInstruction(ITEM))

                Next
            Catch ex As Exception

            End Try
            Prog.Add("HALT")
            iCPU.LoadProgram(Prog)
            iCPU.RUN()
        End Sub

        Public Function ExecuteInstruction(ByRef Item As List(Of AbstractSyntax)) As List(Of String)
            Dim Prog As New List(Of String)
            Dim ItemCount As Integer = 0
            For Each TOK In Item

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
                        Prog.AddRange(_print(iRAM.GetVar(TOK.RequiredTokens(1).TokenValue)))
                    Case "Math_Operation"
                        'Token (1) = Operator
                        If TOK.RequiredTokens.Count = 3 Then
                            Dim iOperator As String = TOK.RequiredTokens(1).TokenValue
                            Prog.AddRange(_Binary_op(GetValue(TOK.RequiredTokens(0).TokenValue), GetValue(TOK.RequiredTokens(2).TokenValue), iOperator))
                        End If
                    Case "Conditional_Operation"
                        'Token (1) = Operator
                        If TOK.RequiredTokens.Count = 3 Then
                            Dim iOperator As String = TOK.RequiredTokens(1).TokenValue
                            Prog.AddRange(_Binary_op(GetValue(TOK.RequiredTokens(0).TokenValue), GetValue(TOK.RequiredTokens(2).TokenValue), iOperator))
                        End If
                    Case "_DIM_AS"
                        'DIm,var,as,type
                        If TOK.RequiredTokens.Count = 4 Then
                            _DIM_AS(TOK.RequiredTokens(1).TokenValue, TOK.RequiredTokens(3).TokenValue)
                        Else
                        End If

                    Case "_DIM_AS_EQ"
                        'DIm,Var,as,type,=,value
                        If TOK.RequiredTokens.Count = 6 Then
                            _DIM_AS(TOK.RequiredTokens(1).TokenValue, TOK.RequiredTokens(3).TokenValue, TOK.RequiredTokens(5).TokenValue)
                        Else
                            If TOK.RequiredTokens.Count = 3 Then
                                _DIM_AS(TOK.RequiredTokens(1).TokenValue, TOK.RequiredTokens(3).TokenValue)
                            Else
                            End If
                        End If
                    Case "ASSIGN_EQUALS"
                        'var,=,value(int,Bool,String)
                        _VAR_EQ_VALUE(TOK.RequiredTokens(0).TokenValue, TOK.RequiredTokens(2).TokenValue)

                    Case "_FOR"
                        'For,Var,=,start,to,finish
                        If TOK.RequiredTokens.Count = 6 Then
                            _for(TOK.RequiredTokens(1).TokenValue, GetValue(TOK.RequiredTokens(3).TokenValue), GetValue(TOK.RequiredTokens(5).TokenValue))
                        End If

                    Case "_NEXT"
                        'Nex,Var
                        If TOK.RequiredTokens.Count = 2 Then
                            Dim Ivar = TOK.RequiredTokens(1).TokenValue
                            Do While (_next(Ivar)) = True
                                Prog.AddRange(ExecuteInstruction(Program(LineNumber - 2)))
                            Loop
                        Else
                        End If

                End Select

            Next
            Return Prog
        End Function

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
        Private Function _CheckCondition(ByRef Left As Integer, ByRef Right As Integer, ByRef iOperator As String) As Boolean

            Select Case iOperator

                Case ">"
                    If Left > Right Then
                        Return True
                    Else
                        Return False
                    End If


                Case "<"
                    If Left < Right Then
                        Return True
                    Else
                        Return False
                    End If

                Case ">="
                    If Left >= Right Then
                        Return True
                    Else
                        Return False
                    End If

                Case "<="
                    If Left <= Right Then
                        Return True
                    Else
                        Return False
                    End If

                Case "="
                    If Left = Right Then
                        Return True
                    Else
                        Return False
                    End If
                Case Else
                    Return False
            End Select

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
        Public Sub _for(ByRef iVar As String, ByRef iStart As Integer, ByRef ifinish As Integer)
            Dim Temp As New Variable
            Temp.iType = "INTEGER"
            Temp.iName = iVar
            Temp.iValue = Integer.Parse(iStart)
            iRAM.AddVar(Temp)
            Dim Fin As New Variable
            Fin.iName = "FOR_END_" & iVar
            Fin.iValue = Integer.Parse(ifinish)
            Fin.iType = "INTEGER"
            iRAM.AddVar(Fin)
        End Sub
        ''' <summary>
        ''' Executes next Cmd Increment by 1 if not greater than target
        ''' return true if loop is required
        ''' </summary>
        ''' <param name="Ivar"></param>
        ''' <returns>True If loop is Required</returns>
        Public Function _next(ByRef Ivar As String) As Boolean
            Dim fin As String = "FOR_END_" & Ivar
            Dim finval As Integer = Integer.Parse(iRAM.GetVar(fin))
            Dim Ival As Integer = Integer.Parse(iRAM.GetVar(Ivar))
            If Integer.Parse(iRAM.GetVar(Ivar)) < Integer.Parse(iRAM.GetVar(fin)) Then
                iRAM.UpdateVar(Ivar, Ival + 1)
                Return True
                'REPEAT CODE-BLOCK(codeblock required)
            Else
                'Loop is not required
                Return False
            End If
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
