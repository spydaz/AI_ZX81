Imports Newtonsoft.Json.JsonConvert
Namespace STACK_VM
    Public Class ZX81_CPU
        Public CpuStack As New Stack
        Public InstructionPointer As Integer
        Private InstructionAdrress As Integer
        Public STATE As Boolean = False
        Public ProgramData As List(Of String)
        Public CPU_ERR As ZX81_ERR
        Public localCache As New Stack
        Public CurrentCache As StackMemoryFrame
        Public Sub New(ByRef Instuctions As List(Of String))


            If Instuctions.Count > 1 Then
                CurrentCache = New StackMemoryFrame(0)
                '  localCache.Push(CurrentCache)
                ProgramData = Instuctions

            Else
                Me.STATE = False
                ProgramData = New List(Of String)
                CPU_ERR = New ZX81_ERR("Raise Error No instruction set", Me)
                CPU_ERR.RaiseErr()
                Me.STATE = False
            End If

        End Sub
        Private Function GetStack() As Stack
            Return CpuStack
        End Function
        Private Sub Push(ByRef Value As String)
            CpuStack.Push(Value)
        End Sub
        Private Function Pop() As String
            Return CpuStack.Pop()
        End Function
        ''' <summary>
        ''' REQUIRED TO SEE IN SIDE CURRENT POINTER LOCATION
        ''' </summary>
        ''' <returns></returns>
        Public Function Peek() As String

            Try
                Return CpuStack.Peek()
            Catch ex As Exception
                CPU_ERR = New ZX81_ERR("NULL POINTER CPU HALTED", Me)
                CPU_ERR.RaiseErr()
                STATE = False
                Return "NULL"
            End Try



        End Function


        'CPU SET: 
        Public ReadOnly Property GetInstructionAddress() As Integer
            Get
                Return InstructionAdrress
            End Get
        End Property
        Public Sub Run()
            STATE = True
            Do While (Ishalted = False)
                Step_forward()
            Loop
        End Sub
        Private Sub Step_forward()
            If Ishalted = False Then
                Decode(Fetch)
            Else
                CPU_ERR = New ZX81_ERR("CPU HALTED", Me)
                CPU_ERR.RaiseErr()
                'ERR - state stopped
            End If
        End Sub
        Private Function Fetch() As String



            If InstructionAdrress >= ProgramData.Count Then
                CPU_ERR = New ZX81_ERR("Raise Error Invalid Address -FETCH", Me)
                CPU_ERR.RaiseErr()
                Me.STATE = False
                '
            Else
                Dim Next_Wrd As String = ProgramData(InstructionAdrress)
                InstructionAdrress += 1
                Return Next_Wrd
            End If

            Return Nothing
        End Function
        Public ReadOnly Property Ishalted As Boolean
            Get
                If STATE = False Then
                    Return True
                Else
                    Return False
                End If
            End Get
        End Property
        Public Sub Decode(ByRef Instruct As String)
            'Instruction Set
            Select Case Instruct
                Case "NOT"
                    CheckStackHasAtLeastOneItem()
                    CpuStack.Push(ToInt(NOT_ToBool(CpuStack.Pop())))
                Case "DUP"
                    Try
                        If CpuStack.Count >= 1 Then
                            Dim n As String = Peek()
                            Push(n)
                        Else
                            Me.STATE = False
                            CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - DUP", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - DUP", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "HALT"
                    'HALT:
                    STATE = False
                Case "POP"
                    Try
                        If CpuStack.Count >= 1 Then
                            Pop()

                        Else
                            Me.STATE = False
                            CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - pop", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - POP", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "PUSH"
                    'Push
                    Push(Fetch)
                Case "ADD"
                    'ADD
                    Try
                        If CpuStack.Count >= 2 Then
                            Push(BINARYOP(Instruct, Integer.Parse(CpuStack.Pop()), Integer.Parse(CpuStack.Pop())))
                        Else
                            Me.STATE = False
                            CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - ADD", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - ADD", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "SUB"
                    'SUB
                    Try
                        If CpuStack.Count >= 2 Then
                            Push(BINARYOP(Instruct, Integer.Parse(CpuStack.Pop()), Integer.Parse(CpuStack.Pop())))
                        Else
                            Me.STATE = False
                            CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - SUB", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - SUB", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "MUL"
                    'MUL
                    Try
                        If CpuStack.Count >= 2 Then
                            Push(BINARYOP(Instruct, Integer.Parse(CpuStack.Pop()), Integer.Parse(CpuStack.Pop())))
                        Else
                            Me.STATE = False
                            CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - MUL", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - MUL", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "DIV"
                    'DIV
                    Try
                        If CpuStack.Count >= 2 Then
                            Push(BINARYOP(Instruct, Integer.Parse(CpuStack.Pop()), Integer.Parse(CpuStack.Pop())))
                        Else
                            Me.STATE = False
                            CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - DIV", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - DIV", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "AND"
                    Try
                        If CpuStack.Count >= 2 Then
                            Push(BINARYOP(Instruct, Integer.Parse(CpuStack.Pop()), Integer.Parse(CpuStack.Pop())))
                        Else
                            Me.STATE = False
                            CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - AND", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - AND", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "OR"
                    Try
                        If CpuStack.Count >= 2 Then
                            Push(BINARYOP(Instruct, Integer.Parse(CpuStack.Pop()), Integer.Parse(CpuStack.Pop())))
                        Else
                            Me.STATE = False
                            CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - OR", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - OR", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "ISEQ"
                    Try
                        If CpuStack.Count >= 2 Then
                            Push(BINARYOP(Instruct, Integer.Parse(CpuStack.Pop()), Integer.Parse(CpuStack.Pop())))
                        Else
                            Me.STATE = False
                            CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - ISEQ", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - OR", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "IS_GTE"
                    Try
                        If CpuStack.Count >= 2 Then
                            Push(BINARYOP(Instruct, Integer.Parse(CpuStack.Pop()), Integer.Parse(CpuStack.Pop())))
                        Else
                            Me.STATE = False
                            CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - IS_GTE", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - IS_GTE", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "IS_GT"
                    Try
                        If CpuStack.Count >= 2 Then
                            Push(BINARYOP(Instruct, Integer.Parse(CpuStack.Pop()), Integer.Parse(CpuStack.Pop())))
                        Else
                            Me.STATE = False
                            CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - IS_GT", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - IS_GT", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "IS_LT"
                    Try
                        If CpuStack.Count >= 2 Then
                            Push(BINARYOP(Instruct, Integer.Parse(CpuStack.Pop()), Integer.Parse(CpuStack.Pop())))
                        Else
                            Me.STATE = False
                            CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - IS_LT", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - IS_LT", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "IS_LTE"
                    Try
                        If CpuStack.Count >= 2 Then
                            Push(BINARYOP(Instruct, Integer.Parse(CpuStack.Pop()), Integer.Parse(CpuStack.Pop())))
                        Else
                            Me.STATE = False
                            CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - IS_LTE", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction - IS_LTE", Me)
                        CPU_ERR.RaiseErr()
                    End Try

                Case "JMP"
                    ' "Should have the address after the JMP instruction"
                    '' The word after the instruction will contain the address to jump to
                    Dim address As String = Fetch()
                    If CheckJumpAddress(Integer.Parse(address)) = True Then
                        InstructionAdrress = address
                    End If

                Case "JIF_T"
                    ' "Should have the address after the JIF instruction"
                    '' The word after the instruction will contain the address to jump to
                    Dim address As String = Fetch()
                    If CheckJumpAddress(Integer.Parse(address)) = True And CheckStackHasAtLeastOneItem() = True Then
                        If (ToBool(CpuStack.Pop())) Then
                            InstructionAdrress = address
                        Else
                        End If
                    Else
                    End If
                Case "JIF_F"
                    ' "Should have the address after the JIF instruction"
                    '' The word after the instruction will contain the address to jump to
                    Dim address As String = Fetch()
                    If CheckJumpAddress(Integer.Parse(address)) = True And CheckStackHasAtLeastOneItem() = True Then
                        If (NOT_ToBool(CpuStack.Pop())) Then
                            InstructionAdrress = address
                        Else
                        End If
                    Else
                    End If
                Case "LOAD"
                    'lOADS A VARIABLE
                    Dim varNumber As String = Fetch()
                    CpuStack.Push(GetCurrentFrame.GetVar(varNumber))
                Case "STORE"
                    ' "Should have the variable number after the STORE instruction"
                    Dim varNumber As String = Fetch()
                    CheckStackHasAtLeastOneItem()
                    CurrentCache.SetVar(varNumber, CpuStack.Pop())
                    localCache.Push(CurrentCache)
                Case "CALL"
                    ' The word after the instruction will contain the function address
                    Dim address As String = Fetch()
                    CheckJumpAddress(address)
                    localCache.Push(New StackMemoryFrame(InstructionAdrress))  '// Push a New stack frame
                    InstructionAdrress = address '                   // And jump!
                Case "RET"
                    ' Pop the stack frame And return to the previous address
                    CheckThereIsAReturnAddress()
                    Dim returnAddress = GetCurrentFrame().GetReturnAddress()
                    InstructionAdrress = returnAddress
                    localCache.Pop()
                Case "PRINT"
                    Peek()
                    Dim frm As New Form_ZX81
                    frm.Show()
                    frm.Print(Peek)
                Case Else
                    Me.STATE = False
                    CPU_ERR = New ZX81_ERR("Error Decoding Invalid Instruction", Me)
                    CPU_ERR.RaiseErr()
            End Select

        End Sub
        Public Function GetCurrentFrame() As StackMemoryFrame
            If localCache IsNot Nothing Then
                Return localCache.Peek()
            Else
                Return Nothing
                Me.STATE = False
                CPU_ERR = New ZX81_ERR("Error Decoding STACK MEMORY FRAME - GetCurrentFrame", Me)
                CPU_ERR.RaiseErr()
            End If

        End Function

        Public Function GetStackData() As String
            Dim Str As String = ""
            Str = ToJson(Me)
            Return Str
        End Function
        Private Function ToJson(ByRef OBJ As Object) As String
            Return SerializeObject(OBJ)

        End Function
        Private Function CheckStackHasAtLeastOneItem() As Boolean
            If CpuStack.Count >= 1 Then
                Return True
            Else
                Return False
            End If
        End Function
        Private Function BINARYOP(ByRef INSTRUCTION As String, LEFT As Integer, RIGHT As Integer) As String
            Select Case INSTRUCTION
                Case "IS_EQ"
                    Try
                        Return ToInt((ToBool(LEFT) = ToBool(RIGHT)))
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Invalid Operation", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "IS_GT"
                    Try
                        Return ToInt((ToBool(LEFT) > ToBool(RIGHT)))
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Invalid Operation", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "IS_GTE"
                    Try
                        Return ToInt((ToBool(LEFT) >= ToBool(RIGHT)))
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Invalid Operation", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "IS_LT"
                    Try
                        Return ToInt((ToBool(LEFT) < ToBool(RIGHT)))
                        Return LEFT + RIGHT
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Invalid Operation", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "IS_LE"
                    Try
                        Return ToInt((ToBool(LEFT) <= ToBool(RIGHT)))
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Invalid Operation", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "ADD"
                    Try
                        Return LEFT + RIGHT
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Invalid Operation", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "SUB"
                    Try
                        Return LEFT - RIGHT
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Invalid Operation", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "MUL"
                    Try
                        Return LEFT * RIGHT
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Invalid Operation", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "DIV"
                    Try
                        Return LEFT / RIGHT
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Invalid Operation", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "OR"
                    Try
                        Return ToInt((ToBool(LEFT) Or ToBool(RIGHT)))
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Invalid Operation", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "AND"
                    Try
                        Return ToInt((ToBool(LEFT) And ToBool(RIGHT)))
                    Catch ex As Exception
                        Me.STATE = False
                        CPU_ERR = New ZX81_ERR("Invalid Operation", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "NOT"

                Case Else
                    Me.STATE = False
                    CPU_ERR = New ZX81_ERR("Invalid Operation", Me)
                    CPU_ERR.RaiseErr()
                    Return "NULL"
            End Select

            Me.STATE = False
            CPU_ERR = New ZX81_ERR("Invalid Operation", Me)
            CPU_ERR.RaiseErr()
            Return "NULL"
        End Function
        Private Function ToInt(ByRef Bool As Boolean) As String
            If Bool = False Then
                Return 0
            Else
                Return 1
            End If
        End Function
        Private Function ToBool(ByRef Val As Integer) As Boolean
            If Val = 1 Then
                Return True
            Else
                Return False
            End If
        End Function
        Private Function NOT_ToBool(ByRef Val As Integer) As Integer
            If Val = 1 Then
                Return 0
            Else
                Return 1
            End If
        End Function
        Private Function CheckJumpAddress(ByRef address As Integer) As Boolean

            Try
                If address < 0 Or address >= ProgramData.Count Then
                    Me.STATE = False
                    CPU_ERR = New ZX81_ERR(String.Format("Invalid jump address %d at %d", address, GetInstructionAddress), Me)
                    CPU_ERR.RaiseErr()
                    Return False

                Else
                    Return True
                End If
            Catch ex As Exception
                Me.STATE = False
                CPU_ERR = New ZX81_ERR(String.Format("Invalid jump address %d at %d", address, GetInstructionAddress), Me)
                CPU_ERR.RaiseErr()
                Return False
            End Try

        End Function
        Private Function CheckThereIsAReturnAddress() As Boolean


            Try
                If (localCache.Count >= 1) Then
                    Return True
                Else

                    Me.STATE = False
                    CPU_ERR = New ZX81_ERR(String.Format("Invalid RET instruction: no current function call %d", GetInstructionAddress), Me)
                    CPU_ERR.RaiseErr()
                    Return False
                End If
            Catch ex As Exception
                Me.STATE = False
                CPU_ERR = New ZX81_ERR(String.Format("Invalid RET instruction: no current function call %d", GetInstructionAddress), Me)
                CPU_ERR.RaiseErr()
                Return False
            End Try


        End Function
    End Class
    Public Class ZX81_ERR
        Private ErrorStr As String = ""
        Private frm As New Form_ZX81
        Private CpuCurrentState As ZX81_CPU

        Public Sub New(ByRef Err As String, ByVal CPUSTATE As ZX81_CPU)
            ErrorStr = Err
            CpuCurrentState = CPUSTATE
        End Sub
        Public Sub RaiseErr()
            If frm Is Nothing Then
                frm = New Form_ZX81
                frm.Show()
                frm.Print(ErrorStr & vbNewLine & CpuCurrentState.GetStackData())
            Else
                frm.Show()
                frm.Print(ErrorStr & vbNewLine & CpuCurrentState.GetStackData())
            End If

        End Sub




    End Class
End Namespace
Namespace GRAMMARS
    Public Class CPU_GRAMMAR
        Public Shared Function CreatePLGrammar() As List(Of GrammarRule)
            Dim Rule As New GrammarRule
            Dim RuleList As New List(Of GrammarRule)
            Dim Quote As String = "'"
            'Spaces
            Rule.TAGSTRING = "_WHITE_SPACE"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add(" ")
            'Print Command
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_halt"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("HALT")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_push"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("PUSH")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_pop"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("POP")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_add"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("ADD")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_sub"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("SUB")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_div"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("DIV")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_mul"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("MUL")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_and"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("AND")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_or"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("OR")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_not"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("NOT")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_dup"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("DUP")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_is_eq"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("IS_EQ")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_is_ge"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("IS_GE")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_is_le"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("IS_LE")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_lt"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("LT")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_gt"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("GT")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_jmp"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("JMP")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_jif_t"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("JIF_T")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_jif_f"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("JIF_F")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_load"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("LOAD")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_store"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("STORE")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_ret"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("RET")
            RuleList.Add(Rule)
            Return RuleList
        End Function

        '    grammar Sbvm;

        '// A program Is a sequence of lines
        'program: line*;

        '// A line Is either a label, Or an instruction, followed by a newline
        'line: (label | instruction | emptyLine) NEWLINE;

        'emptyLine: ;

        '// Labels are simply identifiers, followed by colons
        'label: IDENTIFIER ':';

        '// An instruction can be of many kinds
        'instruction: halt |
        '             push |
        '             add |
        '             Sub |
        '             mul |
        '             div |
        '             Not |
        '             And |
        '             Or |
        '             pop |
        '             dup |
        '             iseq |
        '             isge |
        '             isgt |
        '             jmp |
        '             jif |
        '             load |
        '             store |
        '             Call |
        '             ret
        '             ;
        'halt: 'HALT';
        'push: 'PUSH' NUMBER;
        'add: 'ADD';
        'Sub 'SUB';
        'mul: 'MUL';
        'div: 'DIV';
        'Not 'NOT';
        'And 'AND';
        'Or 'OR';
        'pop: 'POP';
        'dup: 'DUP';
        'iseq: 'ISEQ';
        'isge: 'ISGE';
        'isgt: 'ISGT';
        'jmp: 'JMP' IDENTIFIER;
        'jif: 'JIF' IDENTIFIER;
        'load: 'LOAD' NUMBER;
        'store: 'STORE' NUMBER;
        '        Call 'CALL' IDENTIFIER;
        'ret: 'RET';


        'IDENTIFIER: [a-zA-Z][a-zA-Z0-9_]*;
        'NUMBER: [0-9]+;
        'NEWLINE: '\r'? '\n';

        '// Skip all whitespaces
        'WHITESPACE: [ \t]+ -> skip;

        '// Skip comments
        'COMMENT: '//' ~('\r' | '\n')* -> skip;

    End Class
End Namespace
