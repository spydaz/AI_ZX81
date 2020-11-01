Imports Newtonsoft.Json.JsonConvert


Public Module ModuleKnowledgeStructures
    Public ConditionalOperators() As String = {"_GREATER_THAN", "_LESS_THAN", "_EQUALS", "_NOT"}
    Public LogicalOperators() As String = {"_AND", "_OR", "_NAND", "_NOR", "_XOR"}



#Region "Parser"
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
        Public Expr As Compiler.Expression
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
            If SyntaxStatments IsNot Nothing Then


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
            End If
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
#End Region
#Region "Lexer"
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
        _null = 0
        _STRING_TYPE = 1
        _CODEBLOCK = 2
        _LISTBEGIN = 3
        _LISTEND = 4
        _CONDITIONAL_OPERATORS = 5
        _MATHOPERATORS = 6
        _MULTIPLICATIVES = 7
        _ADDATIVES = 8
        _EACH = 9
        _DO = 10
        _WHITE_SPACE = 11
        _NEW_LINE = 12
        _LETTER = 13
        _EOF = 14
        _NUMBER = 15
        _OPERATION = 16
        _LEFT_CODE_BRACKET = 17
        _RIGHT_CODE_BRACKET = 18
        _CODE_BLOCK = 19
        _LEFTBRACKET = 20
        _RIGHTBRACKET = 21
        _DIM = 22
        _ADD = 23
        _MINUS = 24
        _DIVIDE = 25
        _MULTIPLY = 26
        _EQUALS = 27
        _GREATER_THAN = 28
        _LESS_THAN = 29
        _NOT = 30
        _OR = 31
        _AND = 32
        _DECIMAL_POINT = 33
        _FLOAT = 34
        _INT = 35
        _BOOLEAN = 36
        _VARIABLE = 37
        _ARRAY = 38
        _STRING = 39
        _PRINT = 40
        _IF = 41
        _THEN = 42
        _ELSE = 43
        _FOR = 44
        _NEXT = 45
        _TO = 46
        _FUNCTION = 47
        _CALL = 48
        _END = 49
        _LOOP = 50
        _WHILE = 51
        _TRUE = 52
        _FALSE = 53
        _AS = 54
        _IN = 55
        _ASSIGN = 56
        _NOUN = 57
        _VERB = 58
        _ADVERB = 59
        _ADJECTIVE = 60
        _PREPOSITION = 61
        _CONJUNCTION = 62
        _QUESTWORD = 63
        _INTERJECTION = 64
        _PRONOUN = 65
        _PRONOUN_NAME = 66
        _PRONOUN_PLACE = 67
        _TRAANSITIVE_VERB = 68
        _INTRANSITIVE_VERB = 69
        _QUANTIFIER = 70
    End Enum
#End Region


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
