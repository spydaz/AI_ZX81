SpydazWeb Basic Programming Language :
	DESCRIPTION:
		A basic programming language designed in stages to be compiled and executed on a virtual machine :
		the language is translated to an assembly code to be executed on a Virtual Stack based CPU
		The process of tokeninzing the input and susequentially into an executable abstract syntax tree
		the tree generates the required assembly code and executes the code in a virtual environment.
	TYPES: 
		INT = [96 / 96.4]					: Integers and floating points are handled the same
		boolean  = [True/False]				: Standard True or False
		String = "HelloWorld"				: Strings must be surrounded in quotes
	PRINT_TO_CONSOLE : 
		Print "HelloWorld"				: Prints String Quotes Signify String Content
		Print 34						: Prints Integer or Floating Point
		Print [True/False]				: Prints a Boolean True
		Print $Variable$				: Prints a Variable Variables must be surrounded in Dollar signs
		CLS								: Clears the screen of the console
	VARIABLE_ASSIGNMENT:
		Dim $Variable$ as [INT / Boolean / string] = [98 / [True/False] / "HelloWorld" / $Variable$]	- The full assignment can be used
		Dim $Variable$ as [INT / Boolean / string]														- The Half Assigment will use Default values =  / 0 / "" / False 
		$variable$ = [98 / [True/False] / "HelloWorld" / $Variable$]									- Re-Assignment - Variable must be instantiated
	IF_THEN								: If Condition = true then ExecuteStatements (Condition must be a Boolean Expression)
		If (Condition) then				: The condition will evaluate to true allowing for the statments to be executed or the end of the statment will be realised
		[Statements]					: THe END IF signifys the end of the statement
		End if
	IF_THEN_ELSE						: If Condition = True then executes Statements else ExecuteStatements (Condition must be a Boolean Expression)
		If (Condition) then				: the Condition will be evaluated and the (THEN) first statements executed.if true or  
		[Statements_1]					: if false the second statements (ELSE) will be evaluated. THe END IF signifys the end of the statement
		Else
		[Statements_2]
		End if
	DO_WHILE_LOOP						: if Condition is true then execute the statements , 
		DO While (Condition)				: The LOOP signifys the end of the loop. (Condition must be a Boolean Expression) 
		[Statements_1]
		LOOP
	FOR_NEXT_LOOP :						: the indicator is used as a marker for the counter of the loop
		FOR [$I$] = Start to Finish			: a start value is given and a finish value is given
		[Statements_1]						: The statements sre executed ; 
		Next								: and the Next Marks the end of the LOOP

SpydazWeb Assembly Language : 
	Description :
		This assembly language is specific to this virtual processor 
		Enableing for the code to be executed on the cpu: 
		This is a Micro based instruction set (Misc)
	ZX81:
		_PUSH					: Pushes items on to the stack (top)
		_POP					: Pops items off the stack (top)
		_PEEK					: Views items on the stack (top)
		_WAIT					: Pauses execution of code
		_PAUSE					: Pauses execution of code
		_HALT					: HALT execution of code
		_RESUME					: Resumes execution of code
		_DUP					: Duplicates item on the stack (top)
		_JMP					: Jumps to location
		_JIF_T					: Jump if true
		_JIF_F					: Jump if False
		_JIF_EQ					: Jump if Equals
		_JIF_GT					: Jump if Greater than
		_JIF_LT					: Jump if Less than
		_LOAD					: Load Memory Address
		_STORE					: Store at memeory Address
		_REMOVE					: Removes item at memeory address so location can be free for replacement or updated item
		_CALL					: call location in memory
		_RET					: Return to location called
		_PRINT_M				: Prints to TextConsole Display
		_ADD					: Adds last two items on the stack
		_SUB					: subracts last two items on the stack
		_MUL					: multiplys last two items on the stack
		_DIV					: divides last two items on the stack
		_AND					: if both last two items on the stack are true
		_OR						: if either last two items on the stack are true
		_NOT					: if both last two items on the stack are not true
		_IS_EQ					: if both last two items on the stack are Equals
		_IS_GT					: both items are compared 
		_IS_GTE					: both items are compared 
		_IS_LT					: both items are compared 
		_IS_LTE					: both items are compared 
		_TO_POS					: number is sent to negative
		_TO_NEG					: number is sent to positive
		_INCR					: number is incremented by 1
		_DECR 					: number is Decremented by 2

