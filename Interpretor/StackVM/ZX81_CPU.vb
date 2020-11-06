Imports Newtonsoft.Json.JsonConvert
Imports System
Namespace STACK_VM
    ''' <summary>
    ''' SpydazWeb X86 Assembly language Virtual X86 Processor
    ''' </summary>
    Public Class ZX81_CPU
        Public GPU As New ZX81_GPU

#Region "CPU"
        ''' <summary>
        ''' Used to monitor the Program status ; 
        ''' If the program is being executed then the cpu must be running
        ''' the Property value can only be changed within the program
        ''' </summary>
        Public ReadOnly Property RunningState As Boolean
            Get
                If mRunningState = State.RUN Then
                    Return True
                Else
                    Return False
                End If

                Return mRunningState
            End Get
        End Property
        Private mRunningState As State = State.HALT
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
        Private ReadOnly Property GetInstructionAddress() As Integer
            Get
                Return InstructionAdrress
            End Get
        End Property
        Public ReadOnly Property Get_Instruction_Pointer_Position As Integer
            Get
                Return GetInstructionAddress
            End Get
        End Property
        ''' <summary>
        ''' Returns the current data in the stack
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Get_Current_Stack_Data As List(Of String)
            Get
                Get_Current_Stack_Data = New List(Of String)
                For Each item In CPU_CACHE
                    Get_Current_Stack_Data.Add(item.ToString)
                Next
            End Get
        End Property
        ''' <summary>
        ''' Returns the Current Cache (the stack)
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property View_C_P_U As Stack
            Get
                Return CPU_CACHE
            End Get
        End Property
        ''' <summary>
        ''' Returns the current object on top of the stack
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Get_Current_Stack_Item As Object
            Get
                Return Peek()
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
        Private R_A_M As New Stack
        ''' <summary>
        ''' Returns the Ram as a Stack of Stack Memeory frames;
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property View_R_A_M As Stack
            Get
                Return R_A_M
            End Get
        End Property
        Private WaitTime As Integer = 0


        ''' <summary>
        ''' Each Program can be considered to be a task or thread; 
        ''' A name should be assigned to the Process; 
        ''' Processes themselves can be stacked in a higher level processor,
        ''' allowing for paralel processing of code
        ''' This process allows for the initialization of the CPU; THe Prgram will still need to be loaded
        ''' </summary>
        ''' <param name="ThreadName"></param>
        Public Sub New(ByRef ThreadName As String)
            Me.PROCESS_NAME = ThreadName
            'Initializes a Stack for use (Memory for variables in code can be stored here)
            CurrentCache = New StackMemoryFrame(0)
        End Sub
        ''' <summary>
        ''' Load Program and Executes Code on CPU
        ''' </summary>
        ''' <param name="ThreadName">A name is required to Identify the Process</param>
        ''' <param name="Program"></param>
        Public Sub New(ByRef ThreadName As String, ByRef Program As List(Of String))
            Me.PROCESS_NAME = ThreadName
            'Initializes a Stack for use (Memory for variables in code can be stored here)
            CurrentCache = New StackMemoryFrame(0)
            LoadProgram(Program)
            RUN()
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
            mRunningState = State.RUN
            Do While (IsHalted = False)
                If IsWait = True Then
                    For I = 0 To WaitTime
                    Next
                    EXECUTE()
                    mRunningState = State.RUN
                Else
                    EXECUTE()
                End If
            Loop
        End Sub
        ''' <summary>
        ''' Checks the status of the cpu
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property IsHalted As Boolean
            Get
                If mRunningState = State.HALT = True Then
                    Return True
                Else
                    Return False
                End If
            End Get
        End Property
        Public ReadOnly Property IsWait As Boolean
            Get
                If RunningState = State.PAUSE Then
                    Return True
                Else
                    Return False
                End If
            End Get
        End Property
        ''' <summary>
        ''' Executes the next instruction in the Program
        ''' Each Instruction is fed individually to the decoder : 
        ''' The Execute cycle Checks the Current State to determine 
        ''' if to fetch the next instruction to be decoded;(or EXECUTED) -
        ''' The decoder contains the Chip logic
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
        ''' of the instructions at the decoder level : 
        ''' The Fetch Cycle Fetches the next Instruction in the Program to be executed:
        ''' It is fed to the decoder to be decoded and executed
        ''' </summary>
        ''' <returns></returns>
        Private Function Fetch() As Object
            'Check that it is not the end of the program
            If InstructionAdrress >= ProgramData.Count Then
                CPU_ERR = New VM_ERR("End of instruction list reached! No more instructions in Program data Error Invalid Address -FETCH(Missing HALT command)", Me)
                CPU_ERR.RaiseErr()
                'End of instruction list reached no more instructions in program data
                'HALT CPU!!
                Me.mRunningState = State.HALT
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
        ''' <summary>
        ''' Contains MainInstruction Set: Decode Cycle Decodes program instructions from the program 
        ''' the Insruction pointer points to the current Instruction being feed into the decoder: 
        ''' Important Note : the stack will always point to the data at top of the CPU CACHE (Which is Working Memory);
        '''                 THe memory frames being used are Extensions of this memeory and can be seen as registers, 
        '''                 itself being a memory stack (stack of memory frames)
        ''' </summary>
        ''' <param name="ProgramInstruction"></param>
        Public Sub DECODE(ByRef ProgramInstruction As Object)

            Select Case ProgramInstruction

#Region "Basic Assembly"
                Case "WAIT"
                    WaitTime = Integer.Parse(Fetch().ToString)
                    mRunningState = State.PAUSE
                Case "HALT"
                    'Stop the CPU
                    Me.mRunningState = State.HALT
                Case "PAUSE"
                    WaitTime = Integer.Parse(Fetch().ToString)
                    mRunningState = State.PAUSE
                Case "RESUME"
                    If mRunningState = State.PAUSE Then
                        mRunningState = State.RUN
                        RUN()
                    End If
                ' A Wait time Maybe Neccasary
                Case "DUP"
                    Try
                        If CPU_CACHE.Count >= 1 Then
                            'Get Current item on the stack
                            Dim n As String = Peek()
                            'Push another copy onto the stack
                            Push(n)
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("STACK ERROR : Stack Not intialized - DUP", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - DUP", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "POP"
                    CheckStackHasAtLeastOneItem()
                    Pop()
                Case "PUSH"
                    'Push
                    Push(Fetch)
                Case "JMP"
                    CheckStackHasAtLeastOneItem()
                    ' "Should have the address after the JMP instruction"
                    '' The word after the instruction will contain the address to jump to
                    Dim address As Integer = Integer.Parse(Fetch().ToString)
                    JUMP(address)
                Case "JIF_T"
                    CheckStackHasAtLeastOneItem()
                    ' "Should have the address after the JIF instruction"
                    '' The word after the instruction will contain the address to jump to
                    Dim address As Integer = Integer.Parse(Fetch().ToString)
                    JumpIf_TRUE(address)
                Case "JIF_F"
                    CheckStackHasAtLeastOneItem()
                    ' "Should have the address after the JIF instruction"
                    '' The word after the instruction will contain the address to jump to
                    Dim address As Integer = Integer.Parse(Fetch().ToString)
                    JumpIf_False(address)
                Case "LOAD"
                    CheckStackHasAtLeastOneItem()
                    'lOADS A VARIABLE
                    Dim varNumber As Integer = Integer.Parse(Fetch().ToString)
                    CPU_CACHE.Push(GetCurrentFrame.GetVar(varNumber))
                Case "REMOVE"
                    'lOADS A VARIABLE
                    Dim varNumber As Integer = Integer.Parse(Fetch().ToString)
                    GetCurrentFrame.RemoveVar(varNumber)
                Case "STORE"
                    ' "Should have the variable number after the STORE instruction"
                    Dim varNumber As Integer = Integer.Parse(Fetch().ToString)
                    CheckStackHasAtLeastOneItem()
                    CurrentCache.SetVar(varNumber, CPU_CACHE.Peek())
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
#Region "PRINT"
                ' PRINT TO MONITOR
                Case "PRINT_M"
                    GPU.ConsolePrint(Pop)

                Case "CLS"
                    GPU.Console_CLS()
                ' PRINT TO CONSOLE
                Case "PRINT_C"
                    '  System.Console.WriteLine("------------ ZX 81 ----------" & vbNewLine & Peek())
                    System.Console.ReadKey()
#End Region
#Region "Operations"
                Case "ADD"
                    'ADD
                    Try
                        If CPU_CACHE.Count >= 2 Then
                            Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - ADD", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - ADD", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "SUB"
                    'SUB
                    Try
                        If CPU_CACHE.Count >= 2 Then
                            Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - SUB", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - SUB", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "MUL"
                    'MUL
                    Try
                        If CPU_CACHE.Count >= 2 Then
                            Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - MUL", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - MUL", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "DIV"
                    'DIV
                    Try
                        If CPU_CACHE.Count >= 2 Then
                            Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - DIV", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
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
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - AND", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - AND", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "OR"
                    Try
                        If CPU_CACHE.Count >= 2 Then
                            Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - OR", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - OR", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "IS_EQ"
                    Try
                        If CPU_CACHE.Count >= 2 Then
                            Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - ISEQ", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - OR", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "IS_GTE"
                    Try
                        If CPU_CACHE.Count >= 2 Then
                            Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_GTE", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_GTE", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "IS_GT"
                    Try
                        If CPU_CACHE.Count >= 2 Then
                            Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_GT", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_GT", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "IS_LT"
                    Try
                        If CPU_CACHE.Count >= 2 Then
                            Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_LT", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_LT", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "IS_LTE"
                    Try
                        If CPU_CACHE.Count >= 2 Then
                            Push(BINARYOP(ProgramInstruction, Integer.Parse(Pop()), Integer.Parse(Pop())))
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_LTE", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - IS_LTE", Me)
                        CPU_ERR.RaiseErr()
                    End Try
#End Region
#Region "POSITIVE VS NEGATIVE"
                Case "TO_POS"
                    If CPU_CACHE.Count >= 1 Then
                        Push(ToPositive(Integer.Parse(Pop)))
                    Else
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid arguments - POS", Me)
                        CPU_ERR.RaiseErr()
                    End If
                Case "TO_NEG"
                    If CPU_CACHE.Count >= 1 Then
                        Push(ToNegative(Integer.Parse(Pop)))
                    Else
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid arguments - NEG", Me)
                        CPU_ERR.RaiseErr()
                    End If
#End Region
#Region "Extended JmpCmds"
                Case "JIF_GT"
                    Try
                        If CPU_CACHE.Count >= 3 Then
                            Dim address As Integer = Integer.Parse(Fetch().ToString)
                            Push(BINARYOP("IS_GT", Integer.Parse(Pop()), Integer.Parse(Pop())))
                            JumpIf_TRUE(address)
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid arguments - JIF_GT", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - JIF_GT", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "JIF_LT"
                    Try
                        If CPU_CACHE.Count >= 3 Then
                            Dim address As Integer = Integer.Parse(Fetch().ToString)
                            Push(BINARYOP("IS_LT", Integer.Parse(Pop()), Integer.Parse(Pop())))
                            JumpIf_TRUE(address)
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid arguments - JIF_LT", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - JIF_LT", Me)
                        CPU_ERR.RaiseErr()
                    End Try
                Case "JIF_EQ"
                    Try
                        If CPU_CACHE.Count >= 3 Then
                            Dim address As Integer = Integer.Parse(Fetch().ToString)
                            Push(BINARYOP("IS_EQ", Integer.Parse(Pop()), Integer.Parse(Pop())))
                            JumpIf_TRUE(address)
                        Else
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Error Decoding Invalid arguments - JIF_EQ", Me)
                            CPU_ERR.RaiseErr()
                        End If
                    Catch ex As Exception
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction - JIF_EQ", Me)
                        CPU_ERR.RaiseErr()
                    End Try
#End Region
#Region "INCREMENT/DECREMENT"
                Case "INCR"
                    CheckStackHasAtLeastOneItem()
                    Push(Integer.Parse(Pop) + 1)
                Case "DECR"
                    CheckStackHasAtLeastOneItem()
                    Push(Integer.Parse(Pop) - 1)
#End Region

                Case Else
                    Me.mRunningState = State.HALT
                    CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction", Me)
                    CPU_ERR.RaiseErr()

            End Select

        End Sub
#End Region
#Region "functions required by cpu and assembly language"
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
                    Me.mRunningState = State.HALT
                    CPU_ERR = New VM_ERR(String.Format("Invalid jump address %d at %d", address, GetInstructionAddress), Me)
                    CPU_ERR.RaiseErr()
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Me.mRunningState = State.HALT
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
                    Me.mRunningState = State.HALT
                    CPU_ERR = New VM_ERR(String.Format("Invalid RET instruction: no current function call %d", GetInstructionAddress), Me)
                    CPU_ERR.RaiseErr()
                    Return False
                End If
            Catch ex As Exception
                Me.mRunningState = State.HALT
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
            If R_A_M.Count > 0 Then
                Return R_A_M.Pop()
            Else
                Return Nothing
                Me.mRunningState = State.HALT
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
                If CPU_CACHE.Count > 0 Then
                    Return CPU_CACHE.Peek().ToString
                Else
                    mRunningState = State.HALT
                    Return "NULL"
                End If
            Catch ex As Exception
                mRunningState = State.HALT
                CPU_ERR = New VM_ERR("NULL POINTER CPU HALTED", Me)
                CPU_ERR.RaiseErr()

                Return "NULL"
            End Try
        End Function
        Private Function BINARYOP(ByRef INSTRUCTION As String, LEFT As Integer, RIGHT As Integer) As String
            If INSTRUCTION IsNot Nothing Then


                Select Case INSTRUCTION
                    Case "IS_EQ"
                        Try
                            Return ToBool(ToInt((ToBool(LEFT) = ToBool(RIGHT))))
                        Catch ex As Exception
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Invalid Operation - isEQ", Me)
                            CPU_ERR.RaiseErr()
                        End Try
                    Case "IS_GT"
                        Try
                            Return ToBool(ToInt((ToBool(LEFT) < ToBool(RIGHT))))
                        Catch ex As Exception
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Invalid Operation - isGT", Me)
                            CPU_ERR.RaiseErr()
                        End Try
                    Case "IS_GTE"
                        Try
                            Return ToBool(ToInt((ToBool(LEFT) <= ToBool(RIGHT))))
                        Catch ex As Exception
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Invalid Operation isGTE", Me)
                            CPU_ERR.RaiseErr()
                        End Try
                    Case "IS_LT"
                        Try
                            Return ToBool(ToInt((ToBool(LEFT) > ToBool(RIGHT))))

                        Catch ex As Exception
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Invalid Operation isLT", Me)
                            CPU_ERR.RaiseErr()
                        End Try
                    Case "IS_LE"
                        Try
                            Return ToBool(ToInt((ToBool(LEFT) >= ToBool(RIGHT))))
                        Catch ex As Exception
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Invalid Operation isLTE", Me)
                            CPU_ERR.RaiseErr()
                        End Try
                    Case "ADD"
                        Try
                            Return LEFT + RIGHT
                        Catch ex As Exception
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Invalid Operation -add", Me)
                            CPU_ERR.RaiseErr()
                        End Try
                    Case "SUB"
                        Try
                            Return RIGHT - LEFT
                        Catch ex As Exception
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Invalid Operation -sub", Me)
                            CPU_ERR.RaiseErr()
                        End Try
                    Case "MUL"
                        Try
                            Return RIGHT * LEFT
                        Catch ex As Exception
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Invalid Operation -mul", Me)
                            CPU_ERR.RaiseErr()
                        End Try
                    Case "DIV"
                        Try
                            Return RIGHT / LEFT
                        Catch ex As Exception
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Invalid Operation -div", Me)
                            CPU_ERR.RaiseErr()
                        End Try
                    Case "OR"
                        Try
                            Return ToInt((ToBool(LEFT) Or ToBool(RIGHT)))
                        Catch ex As Exception
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Invalid Operation -or", Me)
                            CPU_ERR.RaiseErr()
                        End Try
                    Case "AND"
                        Try
                            Return ToBool(ToInt((ToBool(LEFT) And ToBool(RIGHT))))
                        Catch ex As Exception
                            Me.mRunningState = State.HALT
                            CPU_ERR = New VM_ERR("Invalid Operation -and", Me)
                            CPU_ERR.RaiseErr()
                        End Try
                    Case "NOT"
                        CheckStackHasAtLeastOneItem()
                        Push(ToInt(NOT_ToBool(Pop())))
                    Case Else
                        Me.mRunningState = State.HALT
                        CPU_ERR = New VM_ERR("Invalid Operation -not", Me)
                        CPU_ERR.RaiseErr()
                End Select


            Else
                Me.mRunningState = State.HALT
            End If

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
                mRunningState = State.HALT
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
                    mRunningState = State.HALT
                    Return "NULL"
                End If
            Catch ex As Exception
                CPU_ERR = New VM_ERR("STACK ERROR - NULL POINTER CPU HALTED -pop", Me)
                CPU_ERR.RaiseErr()
                mRunningState = State.HALT
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
#End Region
#Region "CPU _ INTERNAL _ Components"
        Private Enum State
            RUN
            HALT
            PAUSE
        End Enum
        ''' <summary>
        ''' Memory frame for Variables
        ''' </summary>
        Public Class StackMemoryFrame
            Public Structure Var
                Public Value As String
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
            Public Function GetVar(ByRef VarNumber As String) As String
                For Each item In Variables
                    If item.VarNumber = VarNumber Then
                        Return item.Value
                    End If
                Next
                Return 0
            End Function
            Public Sub SetVar(ByRef VarNumber As String, ByRef value As String)
                Dim item As New Var
                Dim added As Boolean = False
                item.VarNumber = VarNumber
                item.Value = value
                For Each ITM In Variables
                    If ITM.VarNumber = VarNumber Then
                        ITM.Value = value
                    End If
                Next
                Variables.Add(item)

            End Sub
            Public Sub RemoveVar(ByRef VarNumber As String)
                For Each item In Variables
                    If item.VarNumber = VarNumber = True Then
                        Variables.Remove(item)
                        Exit For
                    Else
                    End If
                Next
            End Sub
        End Class
        Public Class VM_ERR
            Private ErrorStr As String = ""
            Private frm As New FormDisplayConsole
            Private CpuCurrentState As ZX81_CPU

            Public Sub New(ByRef Err As String, ByVal CPUSTATE As ZX81_CPU)
                ErrorStr = Err
                CpuCurrentState = CPUSTATE
            End Sub
            Public Sub RaiseErr()
                If frm Is Nothing Then
                    frm = New FormDisplayConsole
                    frm.Show()
                    frm.Print(ErrorStr & vbNewLine & CpuCurrentState.GetStackData())
                Else
                    frm.Show()
                    frm.Print(ErrorStr & vbNewLine & CpuCurrentState.GetStackData())
                End If

            End Sub




        End Class
#End Region
        ''' <summary>
        ''' COMMANDS FOR ASSEMBLY LANGUAGE FOR THIS CPU
        ''' SPYDAZWEB_VM_X86
        ''' </summary>
        Public Enum VM_x86_Cmds
            _NULL
            _REMOVE
            _RESUME
            _PUSH
            _PULL
            _PEEK
            _WAIT
            _PAUSE
            _HALT
            _DUP
            _JMP
            _JIF_T
            _JIF_F
            _JIF_EQ
            _JIF_GT
            _JIF_LT
            _LOAD
            _STORE
            _CALL
            _RET
            _PRINT_M
            _PRINT_C
            _ADD
            _SUB
            _MUL
            _DIV
            _AND
            _OR
            _NOT
            _IS_EQ
            _IS_GT
            _IS_GTE
            _IS_LT
            _IS_LTE
            _TO_POS
            _TO_NEG
            _INCR
            _DECR
        End Enum
    End Class


End Namespace
