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
            Rule.COMPONENTSTRINGS.Add("Print")
            Rule.COMPONENTSTRINGS.Add("P.")
            RuleList.Add(Rule)

            Rule = New GrammarRule
            Rule.TAGSTRING = "_NEW_LINE"
            Rule.COMPONENTSTRINGS = New List(Of String)
            Rule.COMPONENTSTRINGS.Add(ControlChars.CrLf)
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
            Rule.COMPONENTSTRINGS.Add("BOOLEAN")
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
    ''' <summary>
    ''' Production Rules for Grammar
    ''' </summary>
    Public Class AST_Grammar


        ''' <summary>
        ''' Returns a list of Syntax Grammar 
        ''' If Phrase is matched then then it can be returned populated 
        ''' the NAME of the Syntax is the name of the relative function
        ''' tokens added to the syntax will be the parameters for the executor
        ''' or Code transpiler.
        ''' </summary>
        ''' <returns></returns>
        Public Function CreateGrammar() As List(Of AbstractSyntax)
            Dim RuleList As New List(Of AbstractSyntax)
            RuleList.Add(AddMathOperation)
            RuleList.Add(AddConditionalOperation)
            RuleList.Add(Add_DIM_AS_EQUALS_FUNCTION)
            RuleList.Add(Add_DIM_AS_FUNCTION)
            'Print
            RuleList.Add(Add_PRINT_STR_FUNCTION)
            RuleList.Add(Add_PRINT_BOOL_FUNCTION)
            RuleList.Add(Add_PRINT_INT_FUNCTION)

            'RuleList.Add(ADD_DO_WHILE_FUNCTION)
            'RuleList.Add(ADD_LOOP_FUNCTION)
            'RuleList.Add(ADD_WHILE_FUNCTION)
            'RuleList.Add(ADD_ELSE_FUNCTION)
            'RuleList.Add(ADD_END_IF_FUNCTION)
            'RuleList.Add(ADD_FOR_FUNCTION)
            'RuleList.Add(ADD_IF_THEN_FUNCTION)
            'RuleList.Add(ADD_NEXT_FUNCTION)
            Return RuleList
        End Function

#Region "Conditional/Math Operations"
        ''' <summary>
        ''' _ADD         -> var/number/float + var/number/float
        ''' _MINUS       -> var/number/float - var/number/float
        ''' _DIVIDE      -> var/number/float / var/number/float
        ''' _MULTIPLY    -> var/number/float * var/number/float
        ''' </summary>
        ''' <returns></returns>
        Public Function AddMathOperation() As AbstractSyntax
            Dim rule As New AbstractSyntax
            rule.SyntaxStatments = New List(Of List(Of String))
            Dim Statement As New List(Of String)
            rule.SyntaxName = "Math_Operation"
            'ADD
            Statement = New List(Of String)
            Statement.Add("_NUMBER")
            Statement.Add("_ADD")
            Statement.Add("_NUMBER")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_NUMBER")
            Statement.Add("_ADD")
            Statement.Add("_VARIABLE")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_VARIABLE")
            Statement.Add("_ADD")
            Statement.Add("_VARIABLE")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_VARIABLE")
            Statement.Add("_ADD")
            Statement.Add("_NUMBER")
            rule.SyntaxStatments.Add(Statement)

            'MINUS
            Statement = New List(Of String)
            Statement.Add("_NUMBER")
            Statement.Add("_MINUS")
            Statement.Add("_NUMBER")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_NUMBER")
            Statement.Add("_MINUS")
            Statement.Add("_VARIABLE")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_VARIABLE")
            Statement.Add("_MINUS")
            Statement.Add("_VARIABLE")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_VARIABLE")
            Statement.Add("_MINUS")
            Statement.Add("_NUMBER")
            rule.SyntaxStatments.Add(Statement)

            'DIVIDE
            Statement = New List(Of String)
            Statement.Add("_NUMBER")
            Statement.Add("_DIVIDE")
            Statement.Add("_NUMBER")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_NUMBER")
            Statement.Add("_DIVIDE")
            Statement.Add("_VARIABLE")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_VARIABLE")
            Statement.Add("_DIVIDE")
            Statement.Add("_VARIABLE")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_VARIABLE")
            Statement.Add("_DIVIDE")
            Statement.Add("_NUMBER")
            rule.SyntaxStatments.Add(Statement)

            'MULTIPLY
            Statement = New List(Of String)
            Statement.Add("_NUMBER")
            Statement.Add("_MULTIPLY")
            Statement.Add("_NUMBER")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_NUMBER")
            Statement.Add("_MULTIPLY")
            Statement.Add("_VARIABLE")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_VARIABLE")
            Statement.Add("_MULTIPLY")
            Statement.Add("_VARIABLE")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_VARIABLE")
            Statement.Add("_MULTIPLY")
            Statement.Add("_NUMBER")
            rule.SyntaxStatments.Add(Statement)


            Return rule

        End Function
        ''' <summary>
        ''' EQUALS / NOT        -> var/bool/number/float = var/bool/number/float
        ''' LESS THAN / NOT     -> var/number/float = var/number/float
        ''' GREATER THAN / NOT  -> var/number/float = var/number/float
        ''' </summary>
        ''' <returns></returns>
        Public Function AddConditionalOperation() As AbstractSyntax
            Dim rule As New AbstractSyntax
            rule.SyntaxStatments = New List(Of List(Of String))
            Dim Statement As New List(Of String)
            rule.SyntaxName = "Conditional_Operation"
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)

            'a = b
#Region "EQUALS"
            Statement.Add("_BOOLEAN")
            Statement.Add("_EQUALS")
            Statement.Add("_BOOLEAN")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_BOOLEAN")
            Statement.Add("_EQUALS")
            Statement.Add("_VARIABLE")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_BOOLEAN")
            Statement.Add("_EQUALS")
            Statement.Add("_NUMBER")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_VARIABLE")
            Statement.Add("_EQUALS")
            Statement.Add("_BOOLEAN")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_NUMBER")
            Statement.Add("_EQUALS")
            Statement.Add("_BOOLEAN")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_NUMBER")
            Statement.Add("_EQUALS")
            Statement.Add("_VARIABLE")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_VARIABLE")
            Statement.Add("_EQUALS")
            Statement.Add("_NUMBER")
            rule.SyntaxStatments.Add(Statement)
#End Region
            'a != b
#Region "NOT EQUALS"
            Statement.Add("_BOOLEAN")
            Statement.Add("_NOT")
            Statement.Add("_EQUALS")
            Statement.Add("_BOOLEAN")
            Statement = New List(Of String)
            Statement.Add("_BOOLEAN")
            Statement.Add("_NOT")
            Statement.Add("_EQUALS")
            Statement.Add("_VARIABLE")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_BOOLEAN")
            Statement.Add("_NOT")
            Statement.Add("_EQUALS")
            Statement.Add("_NUMBER")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_VARIABLE")
            Statement.Add("_NOT")
            Statement.Add("_EQUALS")
            Statement.Add("_BOOLEAN")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_NUMBER")
            Statement.Add("_NOT")
            Statement.Add("_EQUALS")
            Statement.Add("_BOOLEAN")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_NUMBER")
            Statement.Add("_NOT")
            Statement.Add("_EQUALS")
            Statement.Add("_VARIABLE")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_VARIABLE")
            Statement.Add("_NOT")
            Statement.Add("_EQUALS")
            Statement.Add("_NUMBER")
            rule.SyntaxStatments.Add(Statement)
#End Region
            'a !> b
#Region "Not Greater than"
            'VAR _NOT > VAR
            'VAR _NOT > NUMBER
            'NUMBER _NOT > VAR
            'NUMBER _NOT > NUMBER
            Statement = New List(Of String)
            Statement.Add("_NUMBER")
            Statement.Add("_NOT")
            Statement.Add("_GREATER_THAN")
            Statement.Add("_VARIABLE")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_VARIABLE")
            Statement.Add("_NOT")
            Statement.Add("_GREATER_THAN")
            Statement.Add("_NUMBER")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_NUMBER")
            Statement.Add("_NOT")
            Statement.Add("_GREATER_THAN")
            Statement.Add("_NUMBER")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_VARIABLE")
            Statement.Add("_NOT")
            Statement.Add("_GREATER_THAN")
            Statement.Add("_VARIBLE")
            rule.SyntaxStatments.Add(Statement)
#End Region
            'a > b
#Region "Greater Than"

            'VAR > VAR
            'VAR > NUMBER
            'NUMBER > VAR
            'NUMBER > NUMBER


            Statement = New List(Of String)
            Statement.Add("_NUMBER")
            Statement.Add("_GREATER_THAN")
            Statement.Add("_VARIABLE")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_VARIABLE")
            Statement.Add("_GREATER_THAN")
            Statement.Add("_NUMBER")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_NUMBER")
            Statement.Add("_GREATER_THAN")
            Statement.Add("_NUMBER")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_VARIABLE")
            Statement.Add("_GREATER_THAN")
            Statement.Add("_VARIBLE")
            rule.SyntaxStatments.Add(Statement)
#End Region
            'a !< B
#Region "Not less than >"
            'VAR _NOT < VAR
            'VAR _NOT < NUMBER
            'NUMBER _NOT < VAR
            'NUMBER _NOT < NUMBER
            Statement = New List(Of String)
            Statement.Add("_NUMBER")
            Statement.Add("_NOT")
            Statement.Add("_LESS_THAN")
            Statement.Add("_VARIABLE")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_VARIABLE")
            Statement.Add("_NOT")
            Statement.Add("_LESS_THAN")
            Statement.Add("_NUMBER")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_NUMBER")
            Statement.Add("_NOT")
            Statement.Add("_LESS_THAN")
            Statement.Add("_NUMBER")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_VARIABLE")
            Statement.Add("_NOT")
            Statement.Add("_LESS_THAN")
            Statement.Add("_VARIBLE")
            rule.SyntaxStatments.Add(Statement)
#End Region
            'a < b
#Region "less than <"


            'VAR < VAR
            'VAR < NUMBER
            'NUMBER < VAR
            'NUMBER < NUMBER
            Statement = New List(Of String)
            Statement.Add("_NUMBER")
            Statement.Add("_LESS_THAN")
            Statement.Add("_VARIABLE")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_VARIABLE")
            Statement.Add("_LESS_THAN")
            Statement.Add("_NUMBER")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_NUMBER")
            Statement.Add("_LESS_THAN")
            Statement.Add("_NUMBER")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_VARIABLE")
            Statement.Add("_LESS_THAN")
            Statement.Add("_VARIBLE")
            rule.SyntaxStatments.Add(Statement)
#End Region

            Statement = New List(Of String)
            Return rule
        End Function

#End Region
#Region "Print"
        Public Function Add_PRINT_STR_FUNCTION() As AbstractSyntax
            Dim rule As New AbstractSyntax
            Dim Statement As New List(Of String)
            rule.SyntaxStatments = New List(Of List(Of String))
            rule.SyntaxName = "_PRINT_STR"
            Statement = New List(Of String)
            Statement.Add("_PRINT")
            Statement.Add("_STRING")
            rule.SyntaxStatments.Add(Statement)
            Return rule
        End Function
        Public Function Add_PRINT_BOOL_FUNCTION() As AbstractSyntax
            Dim rule As New AbstractSyntax
            rule.SyntaxStatments = New List(Of List(Of String))
            Dim Statement As New List(Of String)
            rule.SyntaxName = "_PRINT_BOOL"
            Statement = New List(Of String)
            Statement.Add("_PRINT")
            Statement.Add("_BOOLEAN")
            rule.SyntaxStatments.Add(Statement)
            Return rule
        End Function
        Public Function Add_PRINT_INT_FUNCTION() As AbstractSyntax
            Dim rule As New AbstractSyntax
            rule.SyntaxStatments = New List(Of List(Of String))
            Dim Statement As New List(Of String)
            rule.SyntaxName = "_PRINT_INT"
            Statement = New List(Of String)
            Statement.Add("_PRINT")
            Statement.Add("_NUMBER")
            rule.SyntaxStatments.Add(Statement)
            Return rule
        End Function
#End Region
#Region "DIM"
        Public Function Add_DIM_AS_FUNCTION() As AbstractSyntax
            Dim rule As New AbstractSyntax
            Dim Statement As New List(Of String)
            rule.SyntaxStatments = New List(Of List(Of String))
            'DIM A AS INTEGER/BOOLEAN/STRING
            rule.SyntaxName = "_DIM_AS"
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_DIM")
            Statement.Add("_VARIABLE")
            Statement.Add("_AS")
            Statement.Add("_STRING")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_DIM")
            Statement.Add("_VARIABLE")
            Statement.Add("_AS")
            Statement.Add("_BOOLEAN")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_DIM")
            Statement.Add("_VARIABLE")
            Statement.Add("_AS")
            Statement.Add("_INT")
            rule.SyntaxStatments.Add(Statement)
            Return rule
        End Function
        Public Function Add_DIM_AS_EQUALS_FUNCTION() As AbstractSyntax
            Dim rule As New AbstractSyntax
            Dim Statement As New List(Of String)
            rule.SyntaxStatments = New List(Of List(Of String))
            'DIM A AS INTEGER/BOOLEAN/STRING = NUMBER/TRUE/FALSE/STRING
            rule.SyntaxName = "_DIM_AS"
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_DIM")
            Statement.Add("_VARIABLE")
            Statement.Add("_AS")
            Statement.Add("_STRING_TYPE")
            Statement.Add("_EQUALS")
            Statement.Add("_STRING")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_DIM")
            Statement.Add("_VARIABLE")
            Statement.Add("_AS")
            Statement.Add("_BOOLEAN")
            Statement.Add("_EQUALS")
            Statement.Add("_TRUE")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_DIM")
            Statement.Add("_VARIABLE")
            Statement.Add("_AS")
            Statement.Add("_BOOLEAN")
            Statement.Add("_EQUALS")
            Statement.Add("_FALSE")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_DIM")
            Statement.Add("_VARIABLE")
            Statement.Add("_AS")
            Statement.Add("_INT")
            Statement.Add("_EQUALS")
            Statement.Add("_NUMBER")
            rule.SyntaxStatments.Add(Statement)
            Return rule
        End Function
#End Region
#Region "FOR"
        Public Function ADD_FOR_FUNCTION()
            Dim rule As New AbstractSyntax
            Dim Statement As New List(Of String)
            rule.SyntaxStatments = New List(Of List(Of String))
            'DIM A AS INTEGER/BOOLEAN/STRING
            rule.SyntaxName = "_FOR"
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_FOR")
            Statement.Add("_VARIABLE")
            Statement.Add("_EQUALS")
            Statement.Add("_NUMBER")
            Statement.Add("_TO")
            Statement.Add("_NUMBER")
            Statement.Add("_LEFT_CODE_BRACKET")
            Statement.Add("*")
            Statement.Add("_RIGHT_CODE_BRACKET")
            Statement.Add("_NEXT")
            Statement.Add("_VARIABLE")
            rule.SyntaxStatments.Add(Statement)
            Return rule

        End Function
        Public Function ADD_NEXT_FUNCTION()
            Dim rule As New AbstractSyntax
            Dim Statement As New List(Of String)
            rule.SyntaxStatments = New List(Of List(Of String))
            'DIM A AS INTEGER/BOOLEAN/STRING
            rule.SyntaxName = "_NEXT"
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_NEXT")
            Statement.Add("_VARIABLE")
            Return rule

        End Function
#End Region
#Region "IF_THEN"
        Public Function ADD_IF_THEN_FUNCTION()
            Dim rule As New AbstractSyntax
            Dim Statement As New List(Of String)
            rule.SyntaxStatments = New List(Of List(Of String))
            'DIM A AS INTEGER/BOOLEAN/STRING
            rule.SyntaxName = "_IF"
            Statement = New List(Of String)
            Statement.Add("_IF")
            Statement.Add("_LEFT_BRACKET")
            Statement.Add("*")
            Statement.Add("_RIGHT_BRACKET")
            Statement.Add("_THEN")
            Statement.Add("_LEFT_CODE_BRACKET")
            Statement.Add("*")
            Statement.Add("_RIGHT_CODE_BRACKET")
            Statement.Add("_END")
            Statement.Add("_IF")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_IF")
            Statement.Add("_LEFT_BRACKET")
            Statement.Add("*")
            Statement.Add("_RIGHT_BRACKET")
            Statement.Add("_THEN")
            Statement.Add("_LEFT_CODE_BRACKET")
            Statement.Add("*")
            Statement.Add("_RIGHT_CODE_BRACKET")
            Statement.Add("_ELSE")
            Statement.Add("_LEFT_CODE_BRACKET")
            Statement.Add("*")
            Statement.Add("_RIGHT_CODE_BRACKET")
            Statement.Add("_END")
            Statement.Add("_IF")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_IF")
            Statement.Add("_LEFT_BRACKET")
            Statement.Add("*")
            Statement.Add("_RIGHT_BRACKET")
            Statement.Add("_THEN")
            Statement.Add("_LEFT_CODE_BRACKET")
            Statement.Add("*")
            Statement.Add("_RIGHT_CODE_BRACKET")
            Statement.Add("_END")
            Statement.Add("_IF")
            rule.SyntaxStatments.Add(Statement)
            Return rule
        End Function
        Public Function ADD_ELSE_FUNCTION()
            Dim rule As New AbstractSyntax
            rule.SyntaxStatments = New List(Of List(Of String))
            Dim Statement As New List(Of String)
            'DIM A AS INTEGER/BOOLEAN/STRING
            rule.SyntaxName = "_ELSE"
            Statement = New List(Of String)
            Statement.Add("_ELSE")
            Statement.Add("_LEFT_CODE_BRACKET")
            Statement.Add("*")
            Statement.Add("_RIGHT_CODE_BRACKET")
            Statement.Add("_END")
            Statement.Add("_IF")
            rule.SyntaxStatments.Add(Statement)
            Return rule
        End Function
        Public Function ADD_END_IF_FUNCTION()
            Dim rule As New AbstractSyntax
            rule.SyntaxStatments = New List(Of List(Of String))
            Dim Statement As New List(Of String)
            'DIM A AS INTEGER/BOOLEAN/STRING
            rule.SyntaxName = "_END_IF"
            Statement = New List(Of String)
            Statement.Add("_END")
            Statement.Add("_IF")
            rule.SyntaxStatments.Add(Statement)
            Return rule
        End Function
        Public Function ADD_LOOP_FUNCTION()
            Dim rule As New AbstractSyntax
            rule.SyntaxStatments = New List(Of List(Of String))
            Dim Statement As New List(Of String)
            'DIM A AS INTEGER/BOOLEAN/STRING
            rule.SyntaxName = "_LOOP"
            Statement = New List(Of String)
            Statement.Add("_LOOP")
            rule.SyntaxStatments.Add(Statement)
            Return rule
        End Function

#End Region
#Region "DO_WHILE"
        Public Function ADD_DO_WHILE_FUNCTION()
            Dim rule As New AbstractSyntax
            Dim Statement As New List(Of String)
            rule.SyntaxStatments = New List(Of List(Of String))
            'DIM A AS INTEGER/BOOLEAN/STRING
            rule.SyntaxName = "_DO_WHILE"

            Statement = New List(Of String)
            Statement.Add("_DO")
            Statement.Add("_WHILE")
            Statement.Add("_LEFT_BRACKET")
            Statement.Add("_RIGHT_BRACKET")
            Statement.Add("_LEFT_CODE_BRACKET")
            Statement.Add("_RIGHT_CODE_BRACKET")
            Statement.Add("_LOOP")
            rule.SyntaxStatments.Add(Statement)
            Return rule
        End Function
        Public Function ADD_WHILE_FUNCTION()
            Dim rule As New AbstractSyntax
            rule.SyntaxStatments = New List(Of List(Of String))
            Dim Statement As New List(Of String)
            'DIM A AS INTEGER/BOOLEAN/STRING
            rule.SyntaxName = "_WHILE"
            Statement.Add("_WHILE")
            Statement.Add("_LEFT_BRACKET")
            Statement.Add("_RIGHT_BRACKET")
            Statement.Add("_LEFT_CODE_BRACKET")
            Statement.Add("_RIGHT_CODE_BRACKET")
            Statement.Add("_LOOP")
            rule.SyntaxStatments.Add(Statement)
            Return rule
        End Function

#End Region
#Region "Operations"
        Public Function ADD_CODEBLOCK_FUNCTION()
            Dim rule As New AbstractSyntax
            Dim Statement As New List(Of String)
            rule.SyntaxStatments = New List(Of List(Of String))
            'DIM A AS INTEGER/BOOLEAN/STRING
            rule.SyntaxName = "_CODEBLOCK"

            Statement = New List(Of String)
            Statement.Add("_LEFT_CODE_BRACKET")
            Statement.Add("*")
            Statement.Add("_RIGHT_CODE_BRACKET")
            rule.SyntaxStatments.Add(Statement)
            Return rule
        End Function
        Public Function ADD_OPERATION_FUNCTION()
            Dim rule As New AbstractSyntax
            Dim Statement As New List(Of String)
            rule.SyntaxStatments = New List(Of List(Of String))
            'DIM A AS INTEGER/BOOLEAN/STRING
            rule.SyntaxName = "_OPERATION"

            Statement = New List(Of String)
            Statement.Add("_LEFT_BRACKET")
            Statement.Add("*")
            Statement.Add("_RIGHT_BRACKET")
            rule.SyntaxStatments.Add(Statement)
            Return rule
        End Function
#End Region
    End Class



End Namespace
