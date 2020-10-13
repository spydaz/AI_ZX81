# AI_ZX81 (Not really) ...

A Basic Experiment in Parser and Compilers and Stack VM . A basic stack based CPU with Assembly language and basic commands. 
A basic programming Languge Parsed to Tokens to e parsed to expressions to be compiled to assembly code to be executed on the virtual CPU... 
Also to be used to Parse English grammar to make abstract syntax trees. 


# Basic Program Langugae Grammar

BASIC FUNCTIONS 

   Print ""
   DIM $VARNAME$ AS INTEGER/BOOLEAN/STRING/FLOAT = TRUE/FALSE / 1.5 / $VARNAME$
   Conditional Operations () Output TRUE/FALSE
   IF () THEN {} ELSE {} END IF
   Basic Math Operators +-/*=<>
   Block Code {}
   DO WHILE () {} LOOP 
   FOR $VARNAME$ = START to FINISH {} NEXT $VARNAME$
   NEXT $VARNAME$


# Spydaz Virtual Machine Code Language ;
This language is used to execute code on the Stack VM.

PUSH / POP / LOAD / STORE / ADD/ MUL / SUB / IS_EQ / IS_GE / IS_LE / LT / GT / RET / JIF / JMP / DIV / DUP / AND / OR / NOT /HALT
 
 ALL commands must be on a single line and ending with the halt Command!

EG : Push 1 Push 2 add ret halt
the returned address should return the value 3



# Expected Outcomes :

 The ability to parse text to a set of rules as well as,
 create a set of production rules enabling for,
 the generation of a collection of Syntax Trees,
 for each statement or codeblock.

 This may be applied to a Language such as English to generate Syntax Trees based on Customized grammars. 
 This will further Extend the SpydazWeb AI SDK; 
 It is also noted that the ability to generate some form of Generic Code will also be potentially achieved. 
 It is my understanding that there is a simular functionality in microsoft roslyn and Experssions etc. 

 The Journey of Creation Reveals a lot about the microsoft compilr as well as the original basic/Interperter and compiler.
  

  The Language Creator : A Toy Programming Language

### Development Stages: 

Create a Lexer to tokenize the input based upon a grammar with defined rules
Create a Parser to parse Tokens into Expressions (Constant/Unary/Binary/Trinary)
Generate - Statements from Expressions 
Generate - List of Expressions and functions for inputted program
Create MasterList - AST Expressions for Functions and Types  > With Internal Execution as well as Regeneration of Visual basic syntax for Expressions 
 
Create A Compiler to ExecuteCode or Compile as Console Application or Generate ConvertedCode to Script. 
maybe generate machine code language and execute on a virtual cpu stack based or register based?


 
