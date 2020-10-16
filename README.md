# AI_ZX81 (Not really) ...

A Basic Experiment in Parser and Compilers and Stack VM . A basic stack based CPU with Assembly language and basic commands. 
A basic programming Languge Parsed to Tokens to e parsed to expressions to be compiled to assembly code to be executed on the virtual CPU... 
Also to be used to Parse English grammar to make abstract syntax trees. 


## Basic Program Language; 

Print ""

DIM $VARNAME$ AS INTEGER/BOOLEAN/STRING/FLOAT = TRUE/FALSE / 0.5 / $VARNAME$ / 1

Conditional Operations () Output TRUE/FALSE

IF () THEN {} ELSE {} END IF

Basic Math Operators +-/*=<>

Block Code {}

DO WHILE () {} LOOP 

FOR $VARNAME$ = START to FINISH {} NEXT $VARNAME$

NEXT $VARNAME$

## Spydaz Virtual Machine Code Language ;

This language is used to execute code on the Stack VM.
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

ALL commands must be on a single line and ending with the halt Command!

EG : Push 1 Push 2 add ret halt the returned address should return the value 3


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
  

 ## The Language Creator : A Toy Programming Language

### Development Stages: 

Create A Compiler to ExecuteCode or Compile as Console Application or Generate ConvertedCode to Script. 
maybe generate machine code language and execute on a virtual cpu stack based or register based???

Create a Lexer to tokenize the input based upon a grammar with defined rules
Create a Parser to parse Tokens into Abstract Syntax statments (Constant/Unary/Binary/Trinary)
Select - Correct Expressions for detected syntax load and execute?
Execute: Expressions to Assembly >  Functions : Decisions regarding evaluation of sub nodes before executing on stack vm simplfing the execution process
Execute - execute optimized tree on STACK Virtual machine

Curently _ CLose!!!


 
