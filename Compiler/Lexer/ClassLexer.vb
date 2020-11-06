Imports Newtonsoft.Json.JsonConvert

Imports AI_ZX81.GRAMMARS

Namespace Compiler


    ''' <summary>
    ''' Used to decode language based upon a Grammar Terms are detected and assigned.
    ''' Tokens are Produced on execution of the Class;
    ''' these can be transpiled and converted into Expression Nodes 
    ''' to be assembled in an Abstract Syntax Tree. 
    ''' The Abstract Tree can be executed or Statements reasssbled,
    ''' into a Target Language for execution or compiling to an output such as EXE/DLL.
    ''' </summary>
    Public Class ClassLexer
        Public Shared Function ENG_Lexer(ByRef Code As String) As List(Of Token)


            mGrammar = EnglishLanguageGrammar.SIMPLECONSTITUANTGRAMMAR
            mGrammar.AddRange(EnglishLanguageGrammar.SIMPLEPHRASEGRAMMAR)
            mGrammar.AddRange(EnglishLanguageGrammar.COMPLEXPHRASEGRAMMAR)
            mGrammar.AddRange(EnglishLanguageGrammar.SIMPLESENTENCEGRAMMAR)
            mGrammar.AddRange(EnglishLanguageGrammar.COMPLEXSENTENCEGRAMMAR)
            mGrammar.AddRange(EnglishLanguageGrammar.SIMPLECONCEPTNETPHRASEGRAMMAR)
            mGrammar.Add(EnglishLanguageGrammar.AddEOF)

            'TOKENIZE---------Into Characters ----------------------------
            '1-Split by char
            Dim CharTokens As List(Of String) = Tokenizer(Code)
            '----------------------------------------------------------
            Dim GramaticalStr As String = ""
            Dim TextStr As String = ""
            Dim NumericStr As String = ""
            Dim StringStr As String = ""
            Dim Current_Token As String = ""
            Dim CurrentState As New SearchState
            Dim DefinedTokens As New List(Of Token)
            CurrentState.State = 0
            CurrentState.CurrentOperation = ""
            For Each TokenChar In CharTokens
                'Expressions


                'Check Gramars
                If CheckGrammarTerms(TokenChar, GramaticalStr, CurrentState) = True Then
                    '-------------------------------------------------------
                    '1.
                    '-------------------------------------------------------
                    'Detect Grammar Terms
                    '-------------------------------------------------------
                    'Check Matches
                    'Get Match
                    Dim NewToken As New Token
                    NewToken.TokenRule = GetTokenRule(GramaticalStr, mGrammar)
                    NewToken.TokenValue = GramaticalStr
                    DefinedTokens.Add(NewToken)
                    GramaticalStr = ""

                Else
                    GramaticalStr &= TokenChar
                End If
                'DetectNewLine
                If CheckNewline(GramaticalStr, mGrammar) = True Then
                    Dim NewToken As New Token
                    NewToken.TokenRule = GetTokenRule(Current_Token, mGrammar)
                    NewToken.TokenValue = "<CR>"
                    GramaticalStr = ""
                    DefinedTokens.Add(NewToken)
                    TokenChar = ""
                Else
                End If
            Next
            Return DefinedTokens
        End Function
        '----------------------------------------------------------------------------------
        'Stage1
        'Main Text Parser Compiles text into token list ready for Abstract tree Generation
        '----------------------------------------------------------------------------------
        ''' <summary>
        '''  Returns Node ready for display in a tree view control
        ''' </summary>
        ''' <param name="Program"></param>
        ''' <returns></returns>
        Public Shared Function GetTokenExprTree(ByRef Program As List(Of List(Of Token))) As TreeNode
            Dim Main As New TreeNode
            Dim Count As Integer = 0
            For Each item In Program
                Count += 1
                Dim nde1 As New TreeNode
                nde1 = New TreeNode
                nde1.Text = "Expression" & Count
                For Each item2 In item
                    Dim nde As New TreeNode
                    If item2.TokenRule.TAGSTRING = "" Then
                    Else
                        nde.Text = "TAG: " & item2.TokenRule.TAGSTRING & " VALUE: " & item2.TokenValue
                        nde1.Nodes.Add(nde)
                    End If
                Next
                Main.Nodes.Add(nde1)
            Next
            Return Main
        End Function
        ''' <summary>
        '''  Returns Node ready for display in a tree view control
        ''' </summary>
        ''' <param name="Program"></param>
        ''' <returns></returns>
        Public Shared Function GetTokenExprTree(ByRef Program As AbstractTokenTree) As TreeNode
            'Root NOde
            Dim Main As New TreeNode
            Main.Text = "Program"
            Dim Count As Integer = 0
            Dim CurrentItemNode As New TreeNode
            For Each item In Program.Statments
                Count += 1

                CurrentItemNode = New TreeNode
                CurrentItemNode.Text = "Statement" & Count
                For Each ItemToken In item
                    Dim CurrentTokenNode As New TreeNode
                    If ItemToken.TokenRule.TAGSTRING = "" Then

                    Else
                        If ItemToken.CodeBlock IsNot Nothing Then

                            CurrentTokenNode.Nodes.Add(GetTokenExprTree(ItemToken.CodeBlock))

                        End If
                        CurrentTokenNode.Text = "TAG: " & ItemToken.TokenRule.TAGSTRING & " VALUE: " & ItemToken.TokenValue
                        CurrentItemNode.Nodes.Add(CurrentTokenNode)
                    End If


                Next
                Main.Nodes.Add(CurrentItemNode)
            Next
            Return Main
        End Function
#Region "Propertys"
        Private mCurrentState As New SearchState
        Private ReadOnly Property CurrentState As SearchState
            Get
                Return mCurrentState
            End Get
        End Property
        Private zDefinedTokens As New List(Of Token)
        Public ReadOnly Property Defined As List(Of Token)
            Get
                Return zDefinedTokens
            End Get

        End Property
        Private Shared mGrammar As List(Of GrammarRule)
        Public ReadOnly Property CurrentGrammar As List(Of GrammarRule)
            Get
                Return mGrammar
            End Get
        End Property
#End Region
#Region "Tokenizer"
        'Tokenizer
        ''' <summary>
        ''' Returns Characters in String as list
        ''' </summary>
        ''' <param name="InputStr"></param>
        ''' <returns></returns>
        Public Shared Function Tokenizer(ByRef InputStr As String) As List(Of String)
            Tokenizer = New List(Of String)
            Dim Endstr As Integer = InputStr.Length
            For i = 1 To Endstr
                Tokenizer.Add(InputStr(i - 1))
            Next
        End Function
        ''' <summary>
        ''' Checks if String token Matches grammar tule
        ''' </summary>
        ''' <param name="CurrentToken"></param>
        ''' <param name="rule"></param>
        ''' <returns></returns>
        Public Shared Function CheckTokenByRule(ByRef CurrentToken As String, rule As GrammarRule) As Boolean
            CheckTokenByRule = False
            For Each item In rule.COMPONENTSTRINGS
                If UCase(item) = UCase(CurrentToken) Then
                    Return True
                End If
            Next
        End Function
        ''' <summary>
        ''' Checks current token to see if it is in TokenList
        ''' </summary>
        ''' <param name="Token"></param>
        ''' <returns></returns>
        Public Shared Function CheckCurrentToken(ByRef Token As String, ByRef iRuleList As List(Of GrammarRule)) As Boolean
            CheckCurrentToken = False
            For Each item In iRuleList
                For Each ITEM2 In item.COMPONENTSTRINGS
                    If UCase(ITEM2) Like UCase(Token) Then
                        Return True
                    End If
                Next
            Next
        End Function
        ''' <summary>
        ''' Checks current token to see if it is in TokenList.COMPONENTSTRINGS 
        ''' (all reserved terms which point to this term)  Allowing for multiple ways to ie: DIM/PUBLIC/PRIVATE
        ''' </summary>
        ''' <param name="Token"></param>
        ''' <returns></returns>
        Public Shared Function GetTokenRule(ByRef Token As String, ByRef iRuleList As List(Of GrammarRule)) As GrammarRule
            GetTokenRule = New GrammarRule
            For Each item In iRuleList
                For Each ITEM2 In item.COMPONENTSTRINGS
                    If UCase(ITEM2) = UCase(Token) Then
                        Return item
                    End If
                Next
            Next
        End Function
#End Region

#Region "MAIN FUNCTION"
        Private mAbstract_Token_Tree As New AbstractTokenTree
        Public ReadOnly Property Abstract_Token_Tree As AbstractTokenTree
            Get
                Return mAbstract_Token_Tree
            End Get

        End Property
        ''' <summary>
        ''' Returns a list of tokens found in the text
        ''' </summary>
        ''' <param name="Code"></param>
        ''' <returns></returns>
        Public Shared Function PL_Lexer(ByRef Code As String) As List(Of Token)
            mGrammar = PL_Grammar.CreatePLGrammar
            'TOKENIZE---------Into Characters ----------------------------
            '1-Split by char
            Dim CharTokens As List(Of String) = Tokenizer(Code & vbCrLf & "EOF")
            '----------------------------------------------------------
            Dim GramaticalStr As String = ""
            Dim TextStr As String = ""
            Dim NumericStr As String = ""
            Dim StringStr As String = ""
            Dim Current_Token As String = ""
            Dim CurrentState As New SearchState
            Dim DefinedTokens As New List(Of Token)
            CurrentState.State = 0
            CurrentState.CurrentOperation = ""
            For Each TokenChar In CharTokens
                'Expressions

                'Numeric
                If CheckNumericExpressions(TokenChar, NumericStr, CurrentState) = True Then
                    'Detected expression - Current_Token updated

                Else
                    If CurrentState.CurrentOperation = "Expression End" Then

                        Dim NewToken As New Token
                        NewToken.TokenValue = NumericStr
                        'Create Token

                        Dim Rule As New GrammarRule

                        If NumericStr.Contains(".") Then
                            Rule.TAGSTRING = "_FLOAT"
                        Else
                            Rule.TAGSTRING = "_NUMBER"
                        End If


                        NewToken.TokenRule = Rule

                        If NumericStr <> "" Then
                            'AddToken
                            DefinedTokens.Add(NewToken)


                        End If
                        CurrentState.CurrentOperation = "Null"
                        NumericStr = ""

                        GramaticalStr = ""
                    Else
                    End If
                End If

                'String
                If CheckStringExpressions(TokenChar, StringStr, CurrentState) = True Then
                Else
                    If CurrentState.CurrentOperation = "String Expression End" Then

                        Dim NewToken As New Token
                        NewToken.TokenValue = StringStr
                        'Create Token
                        Dim Rule As New GrammarRule
                        Rule.TAGSTRING = "_STRING"
                        NewToken.TokenRule = Rule

                        If StringStr <> "" Then
                            'AddToken
                            DefinedTokens.Add(NewToken)


                        End If
                        CurrentState.CurrentOperation = "Null"
                        StringStr = ""
                        GramaticalStr = ""
                        TokenChar = ""
                    Else
                    End If
                End If

                'Text
                If CheckTextExpressions(TokenChar, TextStr, CurrentState) = True Then
                Else
                    If CurrentState.CurrentOperation = "Text Expression End" Then

                        Dim NewToken As New Token
                        NewToken.TokenValue = TextStr

                        'Create Token
                        Dim Rule As New GrammarRule
                        Rule.TAGSTRING = "_VARIABLE"
                        NewToken.TokenRule = Rule

                        If TextStr <> "" Then
                            'AddToken
                            DefinedTokens.Add(NewToken)

                        End If
                        CurrentState.CurrentOperation = "Null"
                        TextStr = ""
                        GramaticalStr = ""
                        TokenChar = ""
                    Else

                    End If

                End If

                'Check Gramars
                If CheckGrammarTerms(TokenChar, GramaticalStr, CurrentState) = True Then
                    '-------------------------------------------------------
                    '1.
                    '-------------------------------------------------------
                    'Detect Grammar Terms
                    '-------------------------------------------------------
                    'Check Matches
                    'Get Match
                    Dim NewToken As New Token
                    NewToken.TokenRule = GetTokenRule(GramaticalStr, mGrammar)
                    NewToken.TokenValue = GramaticalStr
                    DefinedTokens.Add(NewToken)
                    GramaticalStr = ""

                Else
                    GramaticalStr &= TokenChar
                End If
                'DetectNewLine
                'DetectNewLine
                If CheckNewline(GramaticalStr, mGrammar) = True Then
                    Dim NewToken As New Token
                    NewToken.TokenRule = GetTokenRule(Current_Token, mGrammar)
                    NewToken.TokenValue = "<CR>"
                    GramaticalStr = ""
                    DefinedTokens.Add(NewToken)
                    TokenChar = ""
                Else
                End If
            Next
            Return DefinedTokens
        End Function

        ''' <summary>
        ''' This class is to create lexical items from gramar rules 
        ''' THe MAIN Funcyion LEXER creates the Tokens Returning a set of tokens from the given CODE BLOCK
        ''' The tokens can be passed to a transpiler to Create an Advanced Token Node 
        ''' associated to the language being transpiled to.
        ''' </summary>
        Public Sub New(ByRef Codeblock As String, ByRef GRAM As List(Of GrammarRule), ByRef Lang As String)
            mGrammar = GRAM
            'Used to hold Prgram
            Dim Program As New AbstractTokenTree
            'Used for Calcluations
            Dim NewProgram As New AbstractTokenTree

            'PROGRAMMING LANGUAGE
            Select Case Lang
                Case "PL"
#Region "PL_Grammar"
                    '-----------------------------------------------
                    'STAGE : 1
                    'Create Lexical Tokens based on Grammar
                    Dim CurrentTokens As List(Of Token) = PL_Lexer(Codeblock)
                    '-----------------------------------------------
                    '-----------------------------------------------
                    'Parse 1
                    '-----------------------------------------------
                    Program.Statments = New List(Of List(Of Token))
                    'First Create Statements(list of Lists)
                    Program.Statments = CollectStatements(CurrentTokens)

                    ''-----------------------------------------------
                    ''Parse 2
                    ''-----------------------------------------------
                    'NewProgram = New AbstractTokenTree
                    'NewProgram.Statments = New List(Of List(Of Token))
                    ''Get Operations and Place in Codeblocks.Subtree
                    ''List of Tokens
                    'For Each item In Program.Statments
                    '    'Add List back into list of lists
                    '    NewProgram.Statments.Add(CollectOperationBlocks(item))
                    'Next
                    ''Reset Program to new tree
                    'Program = NewProgram

                    ''-----------------------------------------------
                    ''Parse 3
                    ''-----------------------------------------------
                    'NewProgram = New AbstractTokenTree
                    'NewProgram.Statments = New List(Of List(Of Token))
                    ''Get CodeBlocks and add to SubTrees
                    'For Each item In Program.Statments
                    '    'Add List back into list of lists
                    '    NewProgram.Statments.Add(CollectCodeBlocks(item))
                    'Next
                    ''Reset Program to new tree
                    'Program = NewProgram
                    ''-----------------------------------------------



                    '-----------------------------------------------
                    mAbstract_Token_Tree = Program
            '-----------------------------------------------
#End Region
                Case "ENG"
#Region "Eng - Constituants"
                    '-----------------------------------------------
                    'STAGE : 1
                    'Create Lexical Tokens based on Grammar
                    Dim CurrentTokens As List(Of Token) = ENG_Lexer(UCase(Codeblock))
                    '-----------------------------------------------
                    'Parse 1
                    '-----------------------------------------------
                    Program.Statments = New List(Of List(Of Token))
                    '  First Create Statements(list of Lists)
                    Program.Statments = CollectStatements(CurrentTokens)

                    '-----------------------------------------------
                    mAbstract_Token_Tree = Program
                    '-----------------------------------------------
#End Region
            End Select



        End Sub
#End Region
#Region "Expressions - All Gramar items will define to an Expression Type"
#Region "ConstantExpression - Literal Value"
#Region "Numeric Expressions"
        ''' <summary>
        ''' Used to check if NumericExpression Contains Non Numerics
        ''' </summary>
        ''' <param name="NumericExpression"></param>
        ''' <returns></returns>
        Private Shared Function CheckIfContainsMathOperator(ByRef NumericExpression As String) As Boolean
            CheckIfContainsMathOperator = False
            For Each item In PL_Grammar.GetMathOperators.COMPONENTSTRINGS
                If UCase(NumericExpression).Contains(item) = True Then
                    Return True
                End If
            Next
        End Function
        ''' <summary>
        ''' Checks if Expression String contians Comparison Operator
        ''' </summary>
        ''' <param name="NumericExpression"></param>
        ''' <returns></returns>
        Private Shared Function CheckIfContainsComparisonOperator(ByRef NumericExpression As String) As Boolean
            CheckIfContainsComparisonOperator = False
            For Each item In PL_Grammar.GetConditionalOperators.COMPONENTSTRINGS
                If UCase(NumericExpression).Contains(item) = True Then
                    Return True
                End If
            Next
        End Function
        ''' <summary>
        ''' Attempts to capture a numeric from a string
        ''' </summary>
        ''' <param name="CurrentChar"></param>
        ''' <returns></returns>
        Private Shared Function ExpressionNumericCapture(ByRef CurrentChar As String) As Boolean
            If CheckTokenByRule(CurrentChar, PL_Grammar.GetNumberRule) = True Or
            CheckTokenByRule(CurrentChar, PL_Grammar.AddFloatPoint) = True Then
                Return True
            Else
                Return False
            End If
        End Function
        ''' <summary>
        ''' Checks for part of a numeric string based upon current search state
        ''' </summary>
        ''' <param name="CurrentChar"></param>
        ''' <param name="CurrentToken">Currently accuilated token</param>
        ''' <param name="CurrState"></param>
        ''' <returns></returns>
        Private Shared Function CheckNumericExpressions(ByRef CurrentChar As String, ByRef CurrentToken As String, ByRef CurrState As SearchState) As Boolean
            CheckNumericExpressions = False
            If CurrState.CurrentOperation = "Expression Capture" Then
                If ExpressionNumericCapture(CurrentChar) = True Then
                    CurrentToken &= CurrentChar
                    Return True
                Else
                    CurrState.CurrentOperation = "Expression End"

                    Return False
                End If
            Else
                If CheckTokenByRule(CurrentChar, PL_Grammar.GetNumberRule) = True Then
                    CurrentToken &= CurrentChar

                    CurrState.CurrentOperation = "Expression Capture"
                    Return True
                Else
                End If

            End If
        End Function
#End Region
#Region "String Expressions"
        ''' <summary>
        ''' checks for string token based on the current state
        ''' </summary>
        ''' <param name="CurrentChar"></param>
        ''' <param name="CurrentToken">currently accured token</param>
        ''' <param name="CurrentState"></param>
        ''' <returns></returns>
        Private Shared Function CheckStringExpressions(ByRef CurrentChar As String, ByRef CurrentToken As String, ByRef CurrentState As SearchState) As Boolean
            CheckStringExpressions = False
            If CurrentState.CurrentOperation <> "Text Expression Capture" Then

                If CurrentState.CurrentOperation = "String Expression Capture" Then
                    If CurrentChar = Chr(34) <> True Then
                        CurrentToken &= CurrentChar
                        Return True
                    Else
                        CurrentState.CurrentOperation = "String Expression End"
                        CurrentToken &= "'"
                        Return False
                    End If
                Else
                    If CurrentChar = Chr(34) = True Then
                        CurrentToken &= "'"
                        CurrentChar = "'"
                        CurrentState.CurrentOperation = "String Expression Capture"
                        Return True
                    Else
                    End If

                End If
            End If

        End Function
#End Region
#End Region
#Region "Parameter Expression - Require an Assignment - "
#Region "TextExpressions"
        ''' <summary>
        ''' Detects text expressions based on the current state and accumalated token
        ''' </summary>
        ''' <param name="CurrentChar"></param>
        ''' <param name="CurrentToken"></param>
        ''' <param name="CurrState"></param>
        ''' <returns></returns>
        Private Shared Function CheckTextExpressions(ByRef CurrentChar As String, ByRef CurrentToken As String, ByRef CurrState As SearchState) As Boolean
            CheckTextExpressions = False
            If CurrState.CurrentOperation <> "String Expression Capture" Then

                If CurrState.CurrentOperation = "Text Expression Capture" Then
                    If CurrentChar = Chr(36) <> True Then
                        CurrentToken &= CurrentChar
                        Return True
                    Else
                        CurrState.CurrentOperation = "Text Expression End"
                        CurrentToken &= "$"
                        Return False
                    End If
                Else
                    If CurrentChar = Chr(36) = True Then
                        CurrentToken &= "$"
                        CurrState.CurrentOperation = "Text Expression Capture"
                        Return True
                    Else
                    End If

                End If
            End If

        End Function
#End Region
#End Region
        Private Shared Function CheckNewline(ByRef CurrentToken As String, ByRef RuleList As List(Of GrammarRule)) As Boolean
            CheckNewline = False
            'Check Token if matches rule
            If CurrentToken = vbCrLf & vbLf Then
                Return True

            End If

            If GetTokenRule(CurrentToken, RuleList).TAGSTRING = "_NEW_LINE" = True Then
                CurrentToken = "<CR>"
                Return True
            End If


        End Function
        ''' <summary>
        ''' Checks for terms in Grammar based on the 
        ''' current state and accualated token
        ''' </summary>
        ''' <param name="CurrentChar"></param>
        ''' <param name="CurrentToken"></param>
        ''' <param name="CurrentState"></param>
        ''' <returns></returns>
        Private Shared Function CheckGrammarTerms(ByRef CurrentChar As String, ByRef CurrentToken As String, ByRef CurrentState As SearchState) As Boolean
            CheckGrammarTerms = False
            Dim Expr As String = CurrentToken & CurrentChar

            If CheckCurrentToken(Expr, mGrammar) = True Then
                CurrentToken = Expr
                Dim rule As GrammarRule = GetTokenRule(CurrentToken, mGrammar)
                If rule.TAGSTRING <> "_NEW_LINE" Then
                    Return True

                Else
                    Return False

                End If

            End If


        End Function
#End Region
#Region "TOKEN PARSER"
        ''' <summary>
        ''' Collects tokens into statements by newline 
        ''' </summary>
        ''' <param name="DefinedTokens"></param>
        ''' <returns></returns>
        Public Shared Function CollectStatements(ByRef DefinedTokens As List(Of Token)) As List(Of List(Of Token))
            'Current Statment Being built
            Dim Statement As New List(Of Token)
            'Output List
            Dim Statments As List(Of List(Of Token)) = New List(Of List(Of Token))
            'Removes whiteSpaces for Easier recognition of Tokens 
            DefinedTokens = RemoveWhiteSpace(DefinedTokens)
            'A Program is a List of Statements
            For Each item In DefinedTokens
                'Statements are defined by the newline char. 
                'Although statements will be recognized later 
                'they maybe issuse with non correctly formed expressions
                If item.TokenValue = "<CR>" Then

                    'NewLine detected Add Current Statment to List of statments (Program)
                    Statments.Add(Statement)
                    'Being a NEw Statement
                    Statement = New List(Of Token)
                Else
                    'Adds Current Token To Satament
                    Statement.Add(item)
                End If
            Next

            Return Statments
        End Function
        ''' <summary>
        ''' Collects tokens into codeBLocks by COnvert to a single token containing the code cblock
        ''' Should be executes on a per statment basis not on a program basis
        ''' </summary>
        ''' <param name="DefinedTokens"></param>
        ''' <returns></returns>
        Public Shared Function CollectCodeBlocks(ByRef DefinedTokens As List(Of Token)) As List(Of Token)
            Dim Statements As New List(Of Token)
            Dim NewToke As New Token
            Dim NewStatment As New List(Of Token)
            'To be effective whitespace can be removed
            DefinedTokens = RemoveWhiteSpace(DefinedTokens)
            Dim Captureing As String = "False"
            'Statements are Lists of tokens
            For Each item In DefinedTokens
                'Begin Detect - IF detected Set Capture to true
                If item.TokenRule.TAGSTRING = "_LEFT_CODE_BRACKET" Then
                    Captureing = "True"
                    NewStatment = New List(Of Token)
                    'Update statment as the start char is can be added 
                    NewStatment.Add(item)
                    'add the Start operation to original statment for positional marker; 
                    'If detected later Then a codeblock will be sought for the next marker named codeblock
                    'If codeblock list is empty then null can be returned
                    Statements.Add(item)
                Else
                    If Captureing = "True" Then


                        If item.TokenRule.TAGSTRING = "_RIGHT_CODE_BRACKET" Then
                            Captureing = "false"
                            'Create Token
                            NewToke = New Token
                            'Set Rule to Generic CODEBLOCK
                            'USed to identify later
                            NewToke.TokenValue = "CODEBLOCK"

                            'create A rule for CodeBlock
                            Dim newrule As New GrammarRule
                            newrule.COMPONENTSTRINGS = New List(Of String)
                            newrule.COMPONENTSTRINGS.Add("{")
                            newrule.COMPONENTSTRINGS.Add("}")
                            'this can be used to identify later
                            newrule.TAGSTRING = "_CODE_BLOCK"
                            NewToke.TokenRule = newrule
                            'Add Collcted Tokens for block to Statment
                            NewToke.CodeBlock = New List(Of List(Of Token))
                            NewToke.CodeBlock.Add(NewStatment)
                            Statements.Add(NewToke)


                        Else
                            'Must be capturing 
                            'Update statement (if its end char it can be added) signinfying the end of the block
                            NewStatment.Add(item)
                        End If

                        'Token Captured form previous IF
                    Else

                    End If
                    If Captureing = False Then
                        'rebuild original statment 
                        Statements.Add(item)
                    Else

                        ' if true then code block capture is in effect
                    End If
                End If







            Next

            Return Statements
        End Function
        ''' <summary>
        ''' Collects Operation BLocks and add them to thier sub Codeblock
        ''' </summary>
        ''' <param name="DefinedTokens"></param>
        ''' <returns></returns>
        Public Shared Function CollectOperationBlocks(ByRef DefinedTokens As List(Of Token)) As List(Of Token)
            Dim Statements As New List(Of Token)
            Dim NewToke As New Token
            Dim NewStatment As New List(Of Token)
            'To be effective whitespace can be removed
            DefinedTokens = RemoveWhiteSpace(DefinedTokens)
            Dim Captureing As String = "False"
            'Statements are Lists of tokens
            For Each item In DefinedTokens
                'Begin Detect - IF detected Set Capture to true
                If item.TokenRule.TAGSTRING = "_LEFTBRACKET" Then
                    Captureing = "True"
                    NewStatment = New List(Of Token)
                    'Update statment as the start char is can be added 
                    NewStatment.Add(item)
                    'add the Start operation to original statment for positional marker; 
                    'If detected later Then a codeblock will be sought for the next marker named codeblock
                    'If codeblock list is empty then null can be returned
                    Statements.Add(item)
                Else
                    If Captureing = "True" Then


                        If item.TokenRule.TAGSTRING = "_RIGHTBRACKET" Then
                            Captureing = "false"
                            'Create Token
                            NewToke = New Token
                            'Set Rule to Generic CODEBLOCK
                            'USed to identify later
                            NewToke.TokenValue = "OperationBlock"

                            'create A rule for CodeBlock
                            Dim newrule As New GrammarRule
                            newrule.COMPONENTSTRINGS = New List(Of String)
                            newrule.COMPONENTSTRINGS.Add("(")
                            newrule.COMPONENTSTRINGS.Add(")")
                            'this can be used to identify later
                            newrule.TAGSTRING = "_OPERATION"
                            NewToke.TokenRule = newrule
                            NewToke.CodeBlock = New List(Of List(Of Token))
                            'Add Collcted Tokens for block to Statment
                            NewToke.CodeBlock.Add(NewStatment)
                            Statements.Add(NewToke)


                        Else
                            'Must be capturing 
                            'Update statement (if its end char it can be added) signinfying the end of the block
                            NewStatment.Add(item)
                        End If

                        'Token Captured form previous IF
                    Else

                    End If
                    If Captureing = False Then
                        'rebuild original statment 
                        Statements.Add(item)
                    Else

                        ' if true then code block capture is in effect
                    End If
                End If

            Next

            Return Statements
        End Function
        ''' <summary>
        ''' Attempts to detect if codeblock in token list
        ''' </summary>
        ''' <param name="DefinedTokens"></param>
        ''' <returns></returns>
        Public Shared Function DetectCodeBlock(ByRef DefinedTokens As List(Of Token)) As Boolean
            DetectCodeBlock = False
            For Each item In DefinedTokens
                If item.TokenValue = "{" Or item.TokenValue = "}" Then
                    Return True
                Else

                End If
            Next
        End Function
        ''' <summary>
        ''' Attempts to detect operators in the token list
        ''' </summary>
        ''' <param name="DefinedTokens"></param>
        ''' <returns></returns>
        Public Shared Function DetectOperationBlock(ByRef DefinedTokens As List(Of Token)) As Boolean
            DetectOperationBlock = False
            For Each item In DefinedTokens
                If item.TokenValue = "(" Or item.TokenValue = ")" Then
                    Return True
                Else

                End If
            Next
        End Function
        ''' <summary>
        ''' Removes whitespace tokens from the token list
        ''' </summary>
        ''' <param name="DefinedTokens"></param>
        ''' <returns></returns>
        Public Shared Function RemoveWhiteSpace(ByRef DefinedTokens As List(Of Token)) As List(Of Token)
            RemoveWhiteSpace = New List(Of Token)

            For Each item In DefinedTokens
                If item.TokenRule.TAGSTRING = "_WHITE_SPACE" Then

                Else
                    RemoveWhiteSpace.Add(item)
                End If
            Next
        End Function
#End Region
    End Class


End Namespace

