Namespace GRAMMARS


    ''' <summary>
    ''' Used by the Lexer to Detect and create tokens 
    ''' Which form statments to be used by a Parser 
    ''' </summary>
    Public Class PL_Grammar
#Region "SpydazWeb Tokenizer - Grammar"
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
            Rule.TAGSTRING = "_PRINT"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("PRINT")
            Rule.COMPONENTSTRINGS.Add("P.")
            RuleList.Add(Rule)

            Rule = New GrammarRule
            Rule.TAGSTRING = "_NEW_LINE"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add(ControlChars.CrLf)
            RuleList.Add(Rule)

            Rule = New GrammarRule
            Rule.TAGSTRING = "_BOOLEAN"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("BOOLEAN")
            RuleList.Add(Rule)

            'RuleList.Add(AddListBeginPoint)
            'RuleList.Add(AddListEndPoint)
            RuleList = AddMathOperators(RuleList)
            RuleList.Add(AddOperationEndPoint)
            RuleList.Add(AddOperationBeginPoint)
            RuleList = AddMathOperators(RuleList)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_DIM"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("DIM")
            RuleList.Add(Rule)
            'If/Then/ELSE
            Rule = New GrammarRule
            Rule.TAGSTRING = "_IF"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("IF")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_THEN"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("THEN")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_ELSE"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("ELSE")
            RuleList.Add(Rule)
            'For/Next / to/Each
            Rule = New GrammarRule
            Rule.TAGSTRING = "_FOR"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("FOR")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_NEXT"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("NEXT")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_TO"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("TO")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_EACH"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("EACH")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_END"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("END")
            RuleList.Add(Rule)
            RuleList.Add(GetStartCodeBlockRule)
            RuleList.Add(GetEndCodeBlockRule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_LOOP"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("LOOP")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_WHILE"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("WHILE")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_DO"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("DO")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_BOOLEAN"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("TRUE")
            Rule.COMPONENTSTRINGS.Add("FALSE")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_STRING_TYPE"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("STRING")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_INT"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("INT")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_TRUE"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("TRUE")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_FALSE"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("FALSE")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_ARRAY"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("ARRAY")
            Rule.COMPONENTSTRINGS.Add("ARR")
            RuleList.Add(Rule)
            Rule = New GrammarRule
            Rule.TAGSTRING = "_AS"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("AS")
            RuleList.Add(Rule)
            'Spaces
            Rule = New GrammarRule
            Rule.TAGSTRING = "END OF FILE"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("EOF")
            RuleList.Add(AddLessThan)
            RuleList.Add(AddGreaterThan)
            RuleList.Add(Rule)

            Return RuleList
        End Function
        Public Shared Function AddNumbers(ByRef Rulelist As List(Of GrammarRule)) As List(Of GrammarRule)
            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "_NUMBER"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("1")
            Rule.COMPONENTSTRINGS.Add("2")
            Rule.COMPONENTSTRINGS.Add("3")
            Rule.COMPONENTSTRINGS.Add("4")
            Rule.COMPONENTSTRINGS.Add("5")
            Rule.COMPONENTSTRINGS.Add("6")
            Rule.COMPONENTSTRINGS.Add("7")
            Rule.COMPONENTSTRINGS.Add("8")
            Rule.COMPONENTSTRINGS.Add("9")
            Rule.COMPONENTSTRINGS.Add("0")
            Rulelist.Add(Rule)
            Return Rulelist
        End Function
        Public Shared Function GetNumberRule() As GrammarRule
            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "_NUMBER"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("1")
            Rule.COMPONENTSTRINGS.Add("2")
            Rule.COMPONENTSTRINGS.Add("3")
            Rule.COMPONENTSTRINGS.Add("4")
            Rule.COMPONENTSTRINGS.Add("5")
            Rule.COMPONENTSTRINGS.Add("6")
            Rule.COMPONENTSTRINGS.Add("7")
            Rule.COMPONENTSTRINGS.Add("8")
            Rule.COMPONENTSTRINGS.Add("9")
            Rule.COMPONENTSTRINGS.Add("0")

            Return Rule
        End Function
        Public Shared Function GetStartCodeBlockRule() As GrammarRule
            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "_LEFT_CODE_BRACKET"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("{")
            Return Rule
        End Function
        Public Shared Function GetEndCodeBlockRule() As GrammarRule
            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "_RIGHT_CODE_BRACKET"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("}")
            Return Rule
        End Function
        Public Shared Function GetLettersRule() As GrammarRule
            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "_LETTER"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("A")
            Rule.COMPONENTSTRINGS.Add("B")
            Rule.COMPONENTSTRINGS.Add("C")
            Rule.COMPONENTSTRINGS.Add("D")
            Rule.COMPONENTSTRINGS.Add("E")
            Rule.COMPONENTSTRINGS.Add("F")
            Rule.COMPONENTSTRINGS.Add("G")
            Rule.COMPONENTSTRINGS.Add("H")
            Rule.COMPONENTSTRINGS.Add("I")
            Rule.COMPONENTSTRINGS.Add("J")
            Rule.COMPONENTSTRINGS.Add("K")
            Rule.COMPONENTSTRINGS.Add("L")
            Rule.COMPONENTSTRINGS.Add("M")
            Rule.COMPONENTSTRINGS.Add("M")
            Rule.COMPONENTSTRINGS.Add("O")
            Rule.COMPONENTSTRINGS.Add("P")
            Rule.COMPONENTSTRINGS.Add("Q")
            Rule.COMPONENTSTRINGS.Add("R")
            Rule.COMPONENTSTRINGS.Add("S")
            Rule.COMPONENTSTRINGS.Add("T")
            Rule.COMPONENTSTRINGS.Add("U")
            Rule.COMPONENTSTRINGS.Add("V")
            Rule.COMPONENTSTRINGS.Add("W")
            Rule.COMPONENTSTRINGS.Add("X")
            Rule.COMPONENTSTRINGS.Add("Y")
            Rule.COMPONENTSTRINGS.Add("Z")
            Return Rule




        End Function
        Public Shared Function AddLetters(ByRef Rulelist As List(Of GrammarRule)) As List(Of GrammarRule)
            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "_LETTER"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("A")
            Rule.COMPONENTSTRINGS.Add("B")
            Rule.COMPONENTSTRINGS.Add("C")
            Rule.COMPONENTSTRINGS.Add("D")
            Rule.COMPONENTSTRINGS.Add("E")
            Rule.COMPONENTSTRINGS.Add("F")
            Rule.COMPONENTSTRINGS.Add("G")
            Rule.COMPONENTSTRINGS.Add("H")
            Rule.COMPONENTSTRINGS.Add("I")
            Rule.COMPONENTSTRINGS.Add("J")
            Rule.COMPONENTSTRINGS.Add("K")
            Rule.COMPONENTSTRINGS.Add("L")
            Rule.COMPONENTSTRINGS.Add("M")
            Rule.COMPONENTSTRINGS.Add("N")
            Rule.COMPONENTSTRINGS.Add("O")
            Rule.COMPONENTSTRINGS.Add("P")
            Rule.COMPONENTSTRINGS.Add("Q")
            Rule.COMPONENTSTRINGS.Add("R")
            Rule.COMPONENTSTRINGS.Add("S")
            Rule.COMPONENTSTRINGS.Add("T")
            Rule.COMPONENTSTRINGS.Add("U")
            Rule.COMPONENTSTRINGS.Add("V")
            Rule.COMPONENTSTRINGS.Add("W")
            Rule.COMPONENTSTRINGS.Add("X")
            Rule.COMPONENTSTRINGS.Add("Y")
            Rule.COMPONENTSTRINGS.Add("Z")
            Rulelist.Add(Rule)


            Return Rulelist

        End Function
        Public Shared Function AddMathOperators(ByRef Rulelist As List(Of GrammarRule)) As List(Of GrammarRule)
            Rulelist.Add(ADD_DIVIDE)
            Rulelist.Add(ADD_MULTIPLY)
            Rulelist.Add(ADD_PLUS)
            Rulelist.Add(ADD_MINUS)
            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "_EQUALS"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("=")
            Rulelist.Add(Rule)


            Return Rulelist
        End Function


        Public Shared Function ADD_PLUS() As GrammarRule

            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "_ADD"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("+")
            Return Rule
        End Function
        Public Shared Function ADD_MINUS() As GrammarRule

            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "_MINUS"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("-")

            Return Rule
        End Function
        Public Shared Function ADD_MULTIPLY() As GrammarRule

            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "_MULTIPLY"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("*")

            Return Rule
        End Function
        Public Shared Function ADD_DIVIDE() As GrammarRule

            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "_DIVIDE"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("/")

            Return Rule
        End Function
        Public Shared Function GetMathOperators() As GrammarRule
            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "_MathOperators"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("+")
            Rule.COMPONENTSTRINGS.Add("-")
            Rule.COMPONENTSTRINGS.Add("/")
            Rule.COMPONENTSTRINGS.Add("*")
            Return Rule
        End Function
        Public Shared Function GetConditionalOperators() As GrammarRule
            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "_ConditionalOperators"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("<>")
            Rule.COMPONENTSTRINGS.Add("<=")
            Rule.COMPONENTSTRINGS.Add(">=")
            Return Rule
        End Function


        Public Shared Function AddGreaterThan() As GrammarRule
            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "_GREATER_THAN"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add(">")

            Return Rule
        End Function
        Public Shared Function AddLessThan() As GrammarRule
            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "_LESS_THAN"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("<")

            Return Rule
        End Function

        Public Shared Function AddNOT() As GrammarRule
            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "_NOT"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("!")

            Return Rule
        End Function

        Public Shared Function AddOperationBeginPoint() As GrammarRule
            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "_LEFTBRACKET"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("(")
            Return Rule
        End Function
        Public Shared Function AddOperationEndPoint() As GrammarRule
            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "_RIGHTBRACKET"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add(")")
            Return Rule
        End Function
        Public Shared Function AddListBeginPoint() As GrammarRule
            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "_ListBegin"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("[")
            Return Rule
        End Function
        Public Shared Function AddListEndPoint() As GrammarRule
            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "_ListEnd"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add("]")
            Return Rule
        End Function
        Public Shared Function AddFloatPoint() As GrammarRule
            Dim Rule = New GrammarRule
            Rule.TAGSTRING = "_DECIMAL_POINT"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add(".")
            Return Rule
        End Function
#End Region
    End Class


End Namespace
