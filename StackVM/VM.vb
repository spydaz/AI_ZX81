''' <summary>
''' Virtual Machine
''' </summary>
Public Class VM
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
    Public ReadOnly Property RAM As Environment_Memory
        Get
            Return iRAM
        End Get
    End Property
    Private iRAM As Environment_Memory
    Public Sub New(ByRef iName As String)
        Me.iname = iName
        iRAM = New Environment_Memory
        iCPU = New STACK_VM.ZX81_CPU(Name)
        iProgram = New List(Of List(Of AbstractSyntax))
    End Sub
    ''' <summary>
    ''' Executes Program on CPU
    ''' </summary>
    Public Sub ExecuteProgram()
        Dim Prog As New List(Of String)
        For Each ITEM In Program
            For Each TOK In ITEM
                Select Case TOK.SyntaxName
                    Case "_PRINT_STR"
                        Prog.AddRange(_print(TOK.RequiredTokens(1).TokenValue))
                    Case "_PRINT_INT"
                        Prog.AddRange(_print(TOK.RequiredTokens(1).TokenValue))
                End Select
            Next
        Next

        Prog.Add("HALT")
        iCPU.LoadProgram(Prog)
        iCPU.RUN()
    End Sub
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
    Private Function _for_I_start_To_finish(ByRef Start As Integer, ByRef Finish As Integer, ByRef _loop As List(Of String)) As List(Of String)

        Dim PROGRAM As New List(Of String)
        PROGRAM.Add("PUSH")
        PROGRAM.Add(Start)
        PROGRAM.Add("STORE")
        PROGRAM.Add(1)
        PROGRAM.Add("PUSH")
        PROGRAM.Add(Finish)
        PROGRAM.Add("STORE")
        PROGRAM.Add(2) '(COMPARISION)
        PROGRAM.Add("LOAD")
        PROGRAM.Add(1)
        PROGRAM.Add("LOAD")
        PROGRAM.Add(2)
        PROGRAM.Add("JIF_GTE")
        PROGRAM.Add(15)
        PROGRAM.Add("HALT") 'END lOOP
        PROGRAM.AddRange(_loop)
        PROGRAM.Add("LOAD") ' (LOAD FOR INCREMENT)
        PROGRAM.Add("1")
        PROGRAM.Add("PUSH")
        PROGRAM.Add("1")
        PROGRAM.Add("ADD")
        PROGRAM.Add("STORE")
        PROGRAM.Add("1")
        PROGRAM.Add("JMP") '(BACK TO COMPARISON)
        PROGRAM.Add("14")
        Return PROGRAM
    End Function
End Class


