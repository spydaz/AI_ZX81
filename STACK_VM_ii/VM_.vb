Imports Newtonsoft.Json.JsonConvert
Public Class VM_
#Region "CPU"
    ''' <summary>
    ''' Used to monitor the Program status ; 
    ''' If the program is being executed then the cpu must be running
    ''' the Property value can only be changed within the program
    ''' </summary>
    Public ReadOnly Property RunningState As Boolean
        Get
            Return mRunningState
        End Get
    End Property
    Private mRunningState As Boolean
    ''' <summary>
    ''' This is the cpu stack memory space; 
    ''' Items being interrogated will be placed in this memeory frame
    ''' calling functions will access this frame ; 
    ''' the cpu stack can be considered to be a bus; Functions are devices / 
    ''' or gate logic which is connected to the bus via the cpu; 
    ''' </summary>
    Private CPU_CACHE As New Stack
    ''' <summary>
    ''' Returns the Current position of the instruction Pointer 
    ''' in the Program being executed The instruction Pionet can be manipulated 
    ''' Jumping backwards and forwards in the program code.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property GetInstructionAddress() As Integer
        Get
            Return InstructionAdrress
        End Get
    End Property
    ''' <summary>
    ''' Used to pass the intensive error messaging required; 
    ''' </summary>
    Private CPU_ERR As VM_ERR
    ''' <summary>
    ''' Used to hold the Program being sent to the CPU
    ''' A list of objects has been chosen to allow for a Richer CPU
    ''' enabling for objects to be passed instead of strings; 
    ''' due to this being a compiler as well as a morenized CPU
    ''' converting strings to string or integers or booleans etc 
    ''' makes it much harder to create quick easy code;
    ''' the sender is expeected to understand the logic of the items in the program
    ''' the decoder only decodes bassed on what is expected; 
    ''' </summary>
    Private ProgramData As New List(Of Object)
    ''' <summary>
    ''' the InstructionAdrress is the current position in the program; 
    ''' it could be considered to be the line numbe
    ''' </summary>
    Private InstructionAdrress As Integer
    ''' <summary>
    ''' Name of current program or process running in CPU thread
    ''' </summary>
    Public PROCESS_NAME As String = ""
    ''' <summary>
    ''' Used for local memory frame
    ''' </summary>
    Private CurrentCache As StackMemoryFrame
    ''' <summary>
    ''' Used to Store memory frames (The Heap)
    ''' </summary>
    Public R_A_M As New Stack
    ''' <summary>
    ''' Each Program can be considered to be a task or thread; 
    ''' A name should be assigned to the Process; 
    ''' Processes themselves can be stacked in a higher level processor,
    ''' allowing for paralel processing of code
    ''' </summary>
    ''' <param name="ThreadName"></param>
    Public Sub New(ByRef ThreadName As String)
        Me.PROCESS_NAME = ThreadName
    End Sub
    Public Sub New(ByRef ThreadName As String, ByRef Program As List(Of String))
        Me.PROCESS_NAME = ThreadName
        LoadProgram(Program)
    End Sub
    ''' <summary>
    '''  Loads items in to the program cache; 
    '''  this has been added to allow for continuious running of the VM
    '''  the run/wait Command will be added to the assembler 
    '''  enabling for the pausing of the program and restarting of the program stack
    ''' </summary>
    ''' <param name="Prog"></param>
    Public Sub LoadProgram(ByRef Prog As List(Of String))
        ProgramData.AddRange(Prog)
        'Initializes a Stack for use (Memory for variables in code can be stored here)
        CurrentCache = New StackMemoryFrame(0)
    End Sub
    ''' <summary>
    '''  Loads items in to the program cache; 
    '''  this has been added to allow for continuious running of the VM
    '''  the run/wait Command will be added to the assembler 
    '''  enabling for the pausing of the program and restarting of the program stack
    ''' </summary>
    ''' <param name="Prog"></param>
    Public Sub LoadProgram(ByRef Prog As List(Of Object))
        ProgramData.AddRange(Prog)
        'Initializes a Stack for use (Memory for variables in code can be stored here)
        CurrentCache = New StackMemoryFrame(0)
    End Sub
    ''' <summary>
    ''' Begins eexecution of the instructions held in program data
    ''' </summary>
    Public Sub RUN()
        mRunningState = True
        Do While (IsHalted = False)
            EXECUTE()
        Loop
    End Sub
    ''' <summary>
    ''' Checks the status of the cpu
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IsHalted As Boolean
        Get
            If RunningState = False Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property
    ''' <summary>
    ''' Executes the next instruction in the Program
    ''' Each Instruction is fed individually to the decoder
    ''' </summary>
    Public Sub EXECUTE()
        'The HALT command is used to STOP THE CPU
        If IsHalted = False Then
            DECODE(Fetch)
        Else
            CPU_ERR = New VM_ERR("CPU HALTED", Me)
            CPU_ERR.RaiseErr()
            'ERR - state stopped
        End If
    End Sub
    ''' <summary>
    ''' Program Instructions can be boolean/String or integer 
    ''' so an object is assumed enabling for later classification
    ''' of the instructions at the decoder level
    ''' </summary>
    ''' <returns></returns>
    Private Function Fetch() As Object
        'Check that it is not the end of the program
        If InstructionAdrress >= ProgramData.Count Then
            CPU_ERR = New VM_ERR("End of instruction list reached! No more instructions in Program data Error Invalid Address -FETCH(Missing HALT command)", Me)
            CPU_ERR.RaiseErr()
            'End of instruction list reached no more instructions in program data
            'HALT CPU!!
            Me.mRunningState = False
            '
        Else
            'Each Instruction is considered to be a string 
            'or even a integer or boolean the string is the most universal
            Dim CurrentInstruction As Object = ProgramData(InstructionAdrress)
            'Move to next instruction
            InstructionAdrress += 1
            Return CurrentInstruction
        End If
        Return Nothing
    End Function

#End Region
    Public Sub DECODE(ByRef ProgramInstruction As Object)

        Select Case ProgramInstruction.ToString

#Region "Basic Assembly"
            Case "HALT"
                'Stop the CPU
                Me.mRunningState = False
            Case "PAUSE"
                ' A Wait time Maybe Neccasary
            Case "DUP"
                Try
                    If CPU_CACHE.Count >= 1 Then
                        'Get Current item on the stack
                        Dim n As String = Peek()
                        'Push another copy onto the stack
                        Push(n)
                    Else
                        Me.mRunningState = False
                        CPU_ERR = New VM_ERR("STACK ERROR : Stack Not intialized - DUP", Me)
                        CPU_ERR.RaiseErr()
                    End If
                Catch ex As Exception
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - DUP", Me)
                    CPU_ERR.RaiseErr()
                End Try
            Case "POP"
                Pop()
            Case "PUSH"
                'Push
                Push(Fetch)
            Case "JMP"
                ' "Should have the address after the JMP instruction"
                '' The word after the instruction will contain the address to jump to
                Dim address As Integer = Integer.Parse(Fetch().ToString)
                JUMP(address)
            Case "JIF_T"
                ' "Should have the address after the JIF instruction"
                '' The word after the instruction will contain the address to jump to
                Dim address As Integer = Integer.Parse(Fetch().ToString)
                JumpIf_TRUE(address)
            Case "JIF_F"
                ' "Should have the address after the JIF instruction"
                '' The word after the instruction will contain the address to jump to
                Dim address As Integer = Integer.Parse(Fetch().ToString)
                JumpIf_False(address)
            Case "LOAD"
                'lOADS A VARIABLE
                Dim varNumber As Integer = Integer.Parse(Fetch().ToString)
                CPU_CACHE.Push(GetCurrentFrame.GetVar(varNumber))
            Case "STORE"
                ' "Should have the variable number after the STORE instruction"
                Dim varNumber As Integer = Integer.Parse(Fetch().ToString)
                CheckStackHasAtLeastOneItem()
                CurrentCache.SetVar(varNumber, CPU_CACHE.Pop())
                R_A_M.Push(CurrentCache)
            Case "CALL"
                ' The word after the instruction will contain the function address
                Dim address As Integer = Integer.Parse(Fetch().ToString)
                CheckJumpAddress(address)
                R_A_M.Push(New StackMemoryFrame(InstructionAdrress))  '// Push a New stack frame on to the memory heap
                InstructionAdrress = address '                   // And jump!
            Case "RET"
                ' Pop the stack frame And return to the previous address from the memory heap
                CheckThereIsAReturnAddress()
                Dim returnAddress = GetCurrentFrame().GetReturnAddress()
                InstructionAdrress = returnAddress
                R_A_M.Pop()
#End Region

            Case "PRINT"
                Peek()
                Dim frm As New Form_ZX81
                frm.Show()
                frm.Print(Peek)
#Region "Operations"
            Case "ADD"
                'ADD
                Try
                    If CPU_CACHE.Count >= 2 Then
                        Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                    Else
                        Me.mRunningState = False
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - ADD", Me)
                        CPU_ERR.RaiseErr()
                    End If
                Catch ex As Exception
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - ADD", Me)
                    CPU_ERR.RaiseErr()
                End Try
            Case "SUB"
                'SUB
                Try
                    If CPU_CACHE.Count >= 2 Then
                        Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                    Else
                        Me.mRunningState = False
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - SUB", Me)
                        CPU_ERR.RaiseErr()
                    End If
                Catch ex As Exception
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - SUB", Me)
                    CPU_ERR.RaiseErr()
                End Try
            Case "MUL"
                'MUL
                Try
                    If CPU_CACHE.Count >= 2 Then
                        Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                    Else
                        Me.mRunningState = False
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - MUL", Me)
                        CPU_ERR.RaiseErr()
                    End If
                Catch ex As Exception
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - MUL", Me)
                    CPU_ERR.RaiseErr()
                End Try
            Case "DIV"
                'DIV
                Try
                    If CPU_CACHE.Count >= 2 Then
                        Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                    Else
                        Me.mRunningState = False
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - DIV", Me)
                        CPU_ERR.RaiseErr()
                    End If
                Catch ex As Exception
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - DIV", Me)
                    CPU_ERR.RaiseErr()
                End Try
            Case "NOT"
                CheckStackHasAtLeastOneItem()
                Push(ToInt(NOT_ToBool(Pop())))
            Case "AND"
                Try
                    If CPU_CACHE.Count >= 2 Then
                        Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                    Else
                        Me.mRunningState = False
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - AND", Me)
                        CPU_ERR.RaiseErr()
                    End If
                Catch ex As Exception
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - AND", Me)
                    CPU_ERR.RaiseErr()
                End Try
            Case "OR"
                Try
                    If CPU_CACHE.Count >= 2 Then
                        Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                    Else
                        Me.mRunningState = False
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - OR", Me)
                        CPU_ERR.RaiseErr()
                    End If
                Catch ex As Exception
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - OR", Me)
                    CPU_ERR.RaiseErr()
                End Try
            Case "ISEQ"
                Try
                    If CPU_CACHE.Count >= 2 Then
                        Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                    Else
                        Me.mRunningState = False
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - ISEQ", Me)
                        CPU_ERR.RaiseErr()
                    End If
                Catch ex As Exception
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - OR", Me)
                    CPU_ERR.RaiseErr()
                End Try
            Case "IS_GTE"
                Try
                    If CPU_CACHE.Count >= 2 Then
                        Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                    Else
                        Me.mRunningState = False
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_GTE", Me)
                        CPU_ERR.RaiseErr()
                    End If
                Catch ex As Exception
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_GTE", Me)
                    CPU_ERR.RaiseErr()
                End Try
            Case "IS_GT"
                Try
                    If CPU_CACHE.Count >= 2 Then
                        Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                    Else
                        Me.mRunningState = False
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_GT", Me)
                        CPU_ERR.RaiseErr()
                    End If
                Catch ex As Exception
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_GT", Me)
                    CPU_ERR.RaiseErr()
                End Try
            Case "IS_LT"
                Try
                    If CPU_CACHE.Count >= 2 Then
                        Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                    Else
                        Me.mRunningState = False
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_LT", Me)
                        CPU_ERR.RaiseErr()
                    End If
                Catch ex As Exception
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_LT", Me)
                    CPU_ERR.RaiseErr()
                End Try
            Case "IS_LTE"
                Try
                    If CPU_CACHE.Count >= 2 Then
                        Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                    Else
                        Me.mRunningState = False
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_LTE", Me)
                        CPU_ERR.RaiseErr()
                    End If
                Catch ex As Exception
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_LTE", Me)
                    CPU_ERR.RaiseErr()
                End Try
#End Region
            Case "POS"
                If CPU_CACHE.Count >= 1 Then
                    Push(ToPositive(Integer.Parse(Pop)))
                Else
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - POS", Me)
                    CPU_ERR.RaiseErr()
                End If
            Case "NEG"
                If CPU_CACHE.Count >= 1 Then
                    Push(ToNegative(Integer.Parse(Pop)))
                Else
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - NEG", Me)
                    CPU_ERR.RaiseErr()
                End If
            Case Else
                Me.mRunningState = False
                CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction", Me)
                CPU_ERR.RaiseErr()

        End Select

    End Sub

#Region "Handle Boolean"
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
#End Region

#Region "CPU _ Components"
    ''' <summary>
    ''' Memory frame for Variables
    ''' </summary>
    Public Class StackMemoryFrame
        Public Structure Var
            Public Value As Integer
            Public VarNumber As String
        End Structure
        Public ReturnAddress As Integer
        Public Variables As List(Of Var)

        Public Sub New(ByRef ReturnAddress As Integer)
            ReturnAddress = ReturnAddress
            Variables = New List(Of Var)
        End Sub
        Public Function GetReturnAddress() As Integer

            Return ReturnAddress
        End Function
        Public Function GetVar(ByRef VarNumber As String) As Integer
            For Each item In Variables
                If item.VarNumber = VarNumber Then
                    Return item.Value

                End If
            Next
            Return 0
        End Function
        Public Sub SetVar(ByRef VarNumber As String, ByRef value As Integer)
            Dim item As New Var
            item.VarNumber = VarNumber
            item.Value = value

            Variables.Add(item)
        End Sub
    End Class

    Public Class VM_ERR
        Private ErrorStr As String = ""
        Private frm As New Form_ZX81
        Private CpuCurrentState As VM_

        Public Sub New(ByRef Err As String, ByVal CPUSTATE As VM_)
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
#End Region

#Region "Functional Parts"
    ''' <summary>
    ''' Checks if there is a jump address available
    ''' </summary>
    ''' <param name="address"></param>
    ''' <returns></returns>
    Private Function CheckJumpAddress(ByRef address As Integer) As Boolean
        Try
            'Check if it is in range
            If address < 0 Or address >= ProgramData.Count Then
                'Not in range
                Me.mRunningState = False
                CPU_ERR = New VM_ERR(String.Format("Invalid jump address %d at %d", address, GetInstructionAddress), Me)
                CPU_ERR.RaiseErr()
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Me.mRunningState = False
            CPU_ERR = New VM_ERR(String.Format("Invalid jump address %d at %d", address, GetInstructionAddress), Me)
            CPU_ERR.RaiseErr()
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Function used by the internal functions to check if there is a return address
    ''' </summary>
    ''' <returns></returns>
    Private Function CheckThereIsAReturnAddress() As Boolean
        Try
            If (R_A_M.Count >= 1) Then
                Return True
            Else
                Me.mRunningState = False
                CPU_ERR = New VM_ERR(String.Format("Invalid RET instruction: no current function call %d", GetInstructionAddress), Me)
                CPU_ERR.RaiseErr()
                Return False
            End If
        Catch ex As Exception
            Me.mRunningState = False
            CPU_ERR = New VM_ERR(String.Format("Invalid RET instruction: no current function call %d", GetInstructionAddress), Me)
            CPU_ERR.RaiseErr()
            Return False
        End Try
    End Function
    ''' <summary>
    ''' RAM is a STACK MEMORY - Here we can take a look at the stack item
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCurrentFrame() As StackMemoryFrame
        If R_A_M IsNot Nothing Then
            Return R_A_M.Peek()
        Else
            Return Nothing
            Me.mRunningState = False
            CPU_ERR = New VM_ERR("Error Decoding STACK MEMORY FRAME - GetCurrentFrame", Me)
            CPU_ERR.RaiseErr()
        End If
    End Function
    ''' <summary>
    ''' Outputs stack data for verbose output
    ''' </summary>
    ''' <returns></returns>
    Public Function GetStackData() As String
        Dim Str As String = ""
        Str = ToJson(Me)
        Return Str
    End Function
    Private Function ToJson(ByRef OBJ As Object) As String
        Return SerializeObject(OBJ)
    End Function
#End Region

#Region "Operational Functions"
    ''' <summary>
    ''' REQUIRED TO SEE IN-SIDE CURRENT POINTER LOCATION
    ''' ----------Public For Testing Purposes-----------
    ''' </summary>
    ''' <returns></returns>
    Public Function Peek() As String
        Try
            Return CPU_CACHE.Peek().ToString
        Catch ex As Exception
            CPU_ERR = New VM_ERR("NULL POINTER CPU HALTED", Me)
            CPU_ERR.RaiseErr()
            mRunningState = False
            Return "NULL"
        End Try
    End Function
    Private Function BINARYOP(ByRef INSTRUCTION As String, LEFT As Integer, RIGHT As Integer) As String
        Select Case INSTRUCTION
            Case "IS_EQ"
                Try
                    Return ToInt((ToBool(LEFT) = ToBool(RIGHT)))
                Catch ex As Exception
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Invalid Operation - isEQ", Me)
                    CPU_ERR.RaiseErr()
                End Try
            Case "IS_GT"
                Try
                    Return ToInt((ToBool(LEFT) > ToBool(RIGHT)))
                Catch ex As Exception
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Invalid Operation - isGT", Me)
                    CPU_ERR.RaiseErr()
                End Try
            Case "IS_GTE"
                Try
                    Return ToInt((ToBool(LEFT) >= ToBool(RIGHT)))
                Catch ex As Exception
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Invalid Operation isGTE", Me)
                    CPU_ERR.RaiseErr()
                End Try
            Case "IS_LT"
                Try
                    Return ToInt((ToBool(LEFT) < ToBool(RIGHT)))
                    Return LEFT + RIGHT
                Catch ex As Exception
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Invalid Operation isLT", Me)
                    CPU_ERR.RaiseErr()
                End Try
            Case "IS_LE"
                Try
                    Return ToInt((ToBool(LEFT) <= ToBool(RIGHT)))
                Catch ex As Exception
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Invalid Operation isLTE", Me)
                    CPU_ERR.RaiseErr()
                End Try
            Case "ADD"
                Try
                    Return LEFT + RIGHT
                Catch ex As Exception
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Invalid Operation -add", Me)
                    CPU_ERR.RaiseErr()
                End Try
            Case "SUB"
                Try
                    Return LEFT - RIGHT
                Catch ex As Exception
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Invalid Operation -sub", Me)
                    CPU_ERR.RaiseErr()
                End Try
            Case "MUL"
                Try
                    Return LEFT * RIGHT
                Catch ex As Exception
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Invalid Operation -mul", Me)
                    CPU_ERR.RaiseErr()
                End Try
            Case "DIV"
                Try
                    Return LEFT / RIGHT
                Catch ex As Exception
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Invalid Operation -div", Me)
                    CPU_ERR.RaiseErr()
                End Try
            Case "OR"
                Try
                    Return ToInt((ToBool(LEFT) Or ToBool(RIGHT)))
                Catch ex As Exception
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Invalid Operation -or", Me)
                    CPU_ERR.RaiseErr()
                End Try
            Case "AND"
                Try
                    Return ToInt((ToBool(LEFT) And ToBool(RIGHT)))
                Catch ex As Exception
                    Me.mRunningState = False
                    CPU_ERR = New VM_ERR("Invalid Operation -and", Me)
                    CPU_ERR.RaiseErr()
                End Try
            Case "NOT"

            Case Else
                Me.mRunningState = False
                CPU_ERR = New VM_ERR("Invalid Operation -not", Me)
                CPU_ERR.RaiseErr()
        End Select

        Me.mRunningState = False
        CPU_ERR = New VM_ERR("Invalid Operation -BinaryOp", Me)
        CPU_ERR.RaiseErr()
        Return "NULL"
    End Function
    Private Function CheckStackHasAtLeastOneItem(ByRef Current As Stack) As Boolean
        If Current.Count >= 1 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function CheckStackHasAtLeastOneItem() As Boolean
        If CPU_CACHE.Count >= 1 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function CheckRamHasAtLeastOneItem() As Boolean
        If CPU_CACHE.Count >= 1 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub JumpIf_TRUE(ByRef Address As Integer)
        If CheckJumpAddress(Address) = True And CheckStackHasAtLeastOneItem() = True Then
            If (ToBool(Pop)) Then
                InstructionAdrress = Address
            Else
            End If
        Else
        End If
    End Sub
    Private Sub JUMP(ByRef Address As Integer)
        If CheckJumpAddress(Address) = True Then
            InstructionAdrress = Address
        End If

    End Sub
    Private Sub JumpIf_False(ByRef Address As Integer)
        If CheckJumpAddress(Address) = True And CheckStackHasAtLeastOneItem() = True Then
            If (NOT_ToBool(Pop)) Then
                InstructionAdrress = Address
            Else
            End If
        Else
        End If
    End Sub
    ''' <summary>
    ''' Puts a value on the cpu stack to be available to funcitons
    ''' </summary>
    ''' <param name="Value"></param>
    Private Sub Push(ByRef Value As Object)
        Try
            CPU_CACHE.Push(Value)
        Catch ex As Exception
            CPU_ERR = New VM_ERR("STACK ERROR - CPU HALTED -push", Me)
            CPU_ERR.RaiseErr()
            mRunningState = False
        End Try
    End Sub
    ''' <summary>
    ''' Pops a value of the cpu_Stack (current workspace)
    ''' </summary>
    ''' <returns></returns>
    Private Function Pop() As Object
        Try
            If CPU_CACHE.Count >= 1 Then
                Return CPU_CACHE.Pop()
            Else
                CPU_ERR = New VM_ERR("STACK ERROR - NULL POINTER CPU HALTED -pop", Me)
                CPU_ERR.RaiseErr()
                mRunningState = False
                Return "NULL"
            End If
        Catch ex As Exception
            CPU_ERR = New VM_ERR("STACK ERROR - NULL POINTER CPU HALTED -pop", Me)
            CPU_ERR.RaiseErr()
            mRunningState = False
            Return "NULL"
        End Try
        Return "NULL"
    End Function
#End Region

    Private Function ToPositive(Number As Integer)
        Return Math.Abs(Number)
    End Function

    Private Function ToNegative(Number As Integer)
        Return Math.Abs(Number) * -1
    End Function

End Class