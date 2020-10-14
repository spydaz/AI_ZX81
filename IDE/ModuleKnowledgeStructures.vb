Imports Newtonsoft.Json.JsonConvert


Public Module ModuleKnowledgeStructures
    Public ConditionalOperators() As String = {"_GREATER_THAN", "_LESS_THAN", "_EQUALS", "_NOT"}
    Public LogicalOperators() As String = {"_AND", "_OR", "_NAND", "_NOR", "_XOR"}
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
    ''' <summary>
    ''' Abstrax Syntax Token Uses the data stored and 
    ''' Assoicated Expression to execute the components 
    ''' from the Lexer Statment; 
    ''' These tokens can finally form the tree for the Program to be executed;
    ''' These tokens for the final "internal syntax"  
    ''' used by the binary/Uanary/Trinary Expressions
    ''' The data is stored in the token to be extracted for use by the expression within
    ''' </summary>
    Public Structure AbstractSyntax
        Public Expr As AbstractExpressions.Expression
        ''' <summary>
        ''' Name of Syntax item - Refers to operation
        ''' </summary>
        Public SyntaxName As String
        ''' <summary>
        ''' Tokens discovered
        ''' </summary>
        Public RequiredTokens As List(Of Token)
        ''' <summary>
        ''' List of posible statements
        ''' </summary>
        Public SyntaxStatments As List(Of List(Of String))
        ''' <summary>
        ''' Auto Populated by Items for each list a search string is retruned
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property SyntaxSearchStr As List(Of String)
            Get
                Return GetSearchPattern()
            End Get
        End Property
        ''' <summary>
        ''' Sets the Syntax string from the List of internal syntax str
        ''' </summary>
        ''' <returns></returns>
        Private Function GetSearchPattern() As List(Of String)
            Dim Statments As New List(Of String)

            Dim Str As String = ""
            For Each item In SyntaxStatments
                If item IsNot Nothing Then
                    Str = ""
                    For Each item2 In item
                        Str &= item2 & " "
                    Next
                    Statments.Add(Str)
                Else
                End If
            Next
            Return Statments
        End Function
    End Structure
    ''' <summary>
    ''' Used to Hold the PRogram decoded by the lexer into a single tree
    ''' Useally Produced by Lexing the Tokenlist into List of Lists after 
    ''' </summary>
    Public Structure AbstractTokenTree
        Public Token As Token
        Public Statments As List(Of List(Of Token))
    End Structure
    ''' <summary>
    ''' Used as a marker when searching for block data
    ''' </summary>
    Public Structure SearchState
        Public State As Integer
        Public CurrentOperation As String
    End Structure
    ''' <summary>
    ''' Used to contain detected tokens
    ''' </summary>
    Public Structure Token
        Public CodeBlock As List(Of List(Of Token))
        Public Function ToJson() As String
            Return SerializeObject(Me)
        End Function
        Private iDetectedType As TokenType
        ''' <summary>
        ''' Used to denote which token type was detected
        ''' Once The Token rule has been set the type should be populated
        ''' </summary>
        Public ReadOnly Property DetectedType As TokenType
            Get
                Return SetType()

            End Get

        End Property
        ''' <summary>
        ''' Value of token
        ''' </summary>
        Public TokenValue As String
        Private iTokenRule As GrammarRule
        ''' <summary>
        ''' Token Rule
        ''' </summary>
        Public Property TokenRule As GrammarRule
            Get
                Return iTokenRule
            End Get
            Set(value As GrammarRule)
                iTokenRule = value
                iDetectedType = SetType()
            End Set
        End Property
        Private Function SetType() As TokenType
            SetType = TokenType._null
            If TokenRule.TAGSTRING IsNot Nothing Then

                If TokenRule.TAGSTRING = "END OF FILE" Then
                    iDetectedType = TokenType._EOF
                Else
                    Try
                        SetType = [Enum].Parse(GetType(TokenType), TokenRule.TAGSTRING)
                    Catch ex As Exception
                        SetType = TokenType._null
                    End Try

                End If


            Else
            End If

        End Function
    End Structure
    ''' <summary>
    ''' USED as syntactic element 
    ''' </summary>
    Public Structure GrammarRule
        ''' <SUMMARY>
        ''' ADDS A NEW RULE TO THE LIST OF RULES OR GRAMMAR
        ''' </SUMMARY>
        ''' <PARAM NAME="CUSTOMRULES">GRAMMAR TO BE UPDATED</PARAM>
        ''' <PARAM NAME="NEWGRAMMARRULE">RULE TO BE ADDED</PARAM>
        ''' <RETURNS></RETURNS>
        Public Shared Function ADD(ByRef CUSTOMRULES As List(Of GrammarRule), ByRef NEWGRAMMARRULE As GrammarRule)
            Dim FOUND = False
            For Each ITEM In CUSTOMRULES
                If ITEM.TAGSTRING = NEWGRAMMARRULE.TAGSTRING Then
                    FOUND = True
                End If
            Next
            If FOUND = False Then
                CUSTOMRULES.Add(NEWGRAMMARRULE)
            Else
                CUSTOMRULES = UPDATE(CUSTOMRULES, NEWGRAMMARRULE)
            End If
            Return CUSTOMRULES
        End Function

        ''' <SUMMARY>
        ''' UPDATES RULE LIST WITH RULE /
        ''' FINDS THE RULE ASSOCIATED WITH THE TAG AND ADD NEW COMPONENT STRINGS TO TAG
        ''' </SUMMARY>
        ''' <PARAM NAME="CUSTOMRULES">GRAMMAR TO BE UPDATED</PARAM>
        ''' <PARAM NAME="NEWGRAMMARRULE">RULE TO BE ADDED</PARAM>
        ''' <RETURNS></RETURNS>
        Public Shared Function UPDATE(ByRef CUSTOMRULES As List(Of GrammarRule), ByRef NEWGRAMMARRULE As GrammarRule) As List(Of GrammarRule)
            Dim FOUND As Boolean = False
            For Each ITEM In CUSTOMRULES
                If ITEM.TAGSTRING = NEWGRAMMARRULE.TAGSTRING Then
                    FOUND = True
                    For Each COMPONENTSTR In ITEM.COMPONENTSTRINGS
                        ITEM.COMPONENTSTRINGS.Add(COMPONENTSTR)
                    Next
                End If

            Next
            If FOUND = False Then
                CUSTOMRULES = ADD(CUSTOMRULES, NEWGRAMMARRULE)
            End If
            Return CUSTOMRULES
        End Function

        ''' <summary>
        ''' returns a grammar rule to be used in Syntactic BNF GRAMMAR
        ''' </summary>
        ''' <param name="Tag">Tag to be used as term / phrase indicator ie "$N$"</param>
        ''' <param name="Terms">terms defined in tag "CAT,DOG"</param>
        ''' <returns></returns>
        Public Shared Function CREATE_RULE(ByRef Tag As String, ByRef Terms As List(Of String)) As GrammarRule
            Dim NEWGRAMMARRULE As New GrammarRule With {
        .COMPONENTSTRINGS = Terms,
        .TAGSTRING = Tag
    }

            CREATE_RULE = NEWGRAMMARRULE
        End Function

        ''' <summary>
        ''' Production rule to be used in the generation of the Binary function Associated with the Term/Function/Operation
        ''' </summary>
        Public ExecutionScript As String
        ''' <SUMMARY>
        ''' TAG TO BE FOUND: THESE ARE THE TAGS OR PARTS OF SPEECH TO BE ASSOCIATED WITH THE SUBSTRINGS DEFINED
        ''' </SUMMARY>
        Public TAGSTRING As String
        ''' <SUMMARY>
        ''' SEARCH COMPONENT STRINGS : THESE ARE THE SUBSTRINGS OF THE TAG TO BE FOUND
        ''' </SUMMARY>
        Public COMPONENTSTRINGS As List(Of String)
        Public Function ToJson() As String
            Return SerializeObject(Me)

        End Function
    End Structure

    ''' <summary>
    ''' SHOULD BE SHADOWED FOR CUSTOM TOKEN SELECTION
    ''' </summary>
    Public Enum TokenType
        _null
        _STRING_TYPE
        _CODEBLOCK
        _LISTBEGIN
        _LISTEND
        _CONDITIONAL_OPERATORS
        _MATHOPERATORS
        _MULTIPLICATIVES
        _ADDATIVES
        _EACH
        _DO
        _WHITE_SPACE
        _NEW_LINE
        _LETTER
        _EOF
        _NUMBER
        _OPERATION
        _LEFT_CODE_BRACKET
        _RIGHT_CODE_BRACKET
        _CODE_BLOCK
        _LEFTBRACKET
        _RIGHTBRACKET
        _DIM
        _ADD
        _MINUS
        _DIVIDE
        _MULTIPLY
        _EQUALS
        _GREATER_THAN
        _LESS_THAN
        _NOT
        _OR
        _AND
        _DECIMAL_POINT
        _FLOAT
        _INT
        _BOOLEAN
        _VARIABLE
        _ARRAY
        _STRING
        _PRINT
        _IF
        _THEN
        _ELSE
        _FOR
        _NEXT
        _TO
        _FUNCTION
        _CALL
        _END
        _LOOP
        _WHILE
        _TRUE
        _FALSE
        _AS
        _IN
        _ASSIGN
        _NOUN
        _VERB
        _ADVERB
        _ADJECTIVE
        _PREPOSITION
        _CONJUNCTION
        _QUESTWORD
        _INTERJECTION
        _PRONOUN
        _PRONOUN_NAME
        _PRONOUN_PLACE
        _TRAANSITIVE_VERB
        _INTRANSITIVE_VERB
        _QUANTIFIER
    End Enum




    <System.Runtime.CompilerServices.Extension()>
    Public Function ExtractLastChar(ByRef InputStr As String) As String

        ExtractLastChar = Right(InputStr, 1)
    End Function
    <System.Runtime.CompilerServices.Extension()>
    Public Function ExtractFirstChar(ByRef InputStr As String) As String
        ExtractFirstChar = Left(InputStr, 1)
    End Function
    ''' <summary>
    ''' Returns Characters in String as list
    ''' </summary>
    ''' <param name="InputStr"></param>
    ''' <returns></returns>
    <System.Runtime.CompilerServices.Extension()>
    Public Function Tokenizer(ByRef InputStr As String) As List(Of String)
        Tokenizer = New List(Of String)
        Dim Endstr As Integer = InputStr.Length
        For i = 1 To Endstr
            Tokenizer.Add(InputStr(i - 1))
        Next
    End Function
End Module
