Namespace GRAMMARS
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
            RuleList.Add(Add_DIM_AS_FUNCTION)
            RuleList.Add(Add_DIM_AS_EQUALS_FUNCTION)

            'Print

            RuleList.Add(Add_PRINT_STR_FUNCTION)
            RuleList.Add(Add_PRINT_BOOL_FUNCTION)
            RuleList.Add(Add_PRINT_INT_FUNCTION)
            RuleList.Add(Add_PRINT_VARIABLE_FUNCTION)
            RuleList.Add(Add_PRINT_FUNCTION)
            RuleList.Add(Add_ASSIGN_EQUALS_FUNCTION)
            RuleList.Add(ADD_DO_WHILE_FUNCTION)
            RuleList.Add(ADD_LOOP_FUNCTION)
            RuleList.Add(ADD_WHILE_FUNCTION)
            RuleList.Add(ADD_ELSE_FUNCTION)
            RuleList.Add(ADD_ELSE_END_IF_FUNCTION)
            RuleList.Add(ADD_END_IF_FUNCTION)
            RuleList.Add(ADD_FOR_FUNCTION)
            RuleList.Add(ADD_IF_FUNCTION)
            RuleList.Add(ADD_NEXT_FUNCTION)
            RuleList.Add(ADD_THEN_FUNCTION)
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
            Statement.Add("_NUMBER")
            Statement.Add("_EQUALS")
            Statement.Add("_BOOLEAN")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_NUMBER")
            Statement.Add("_EQUALS")
            Statement.Add("_VARIABLE")
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

        Public Function Add_ASSIGN_EQUALS_FUNCTION() As AbstractSyntax
            Dim rule As New AbstractSyntax
            rule.SyntaxStatments = New List(Of List(Of String))
            Dim Statement As New List(Of String)
            rule.SyntaxName = "ASSIGN_EQUALS"
            Statement = New List(Of String)
            Statement.Add("_VARIABLE")
            Statement.Add("_EQUALS")
            Statement.Add("_NUMBER")
            Rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_VARIABLE")
            Statement.Add("_EQUALS")
            Statement.Add("_STRING")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_VARIABLE")
            Statement.Add("_EQUALS")
            Statement.Add("_BOOLEAN")
            rule.SyntaxStatments.Add(Statement)
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
        Public Function Add_PRINT_FUNCTION() As AbstractSyntax
            Dim rule As New AbstractSyntax
            Dim Statement As New List(Of String)
            rule.SyntaxStatments = New List(Of List(Of String))
            rule.SyntaxName = "_PRINT"
            Statement = New List(Of String)
            Statement.Add("_PRINT")
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
        Public Function Add_PRINT_VARIABLE_FUNCTION() As AbstractSyntax
            Dim rule As New AbstractSyntax
            rule.SyntaxStatments = New List(Of List(Of String))
            Dim Statement As New List(Of String)
            rule.SyntaxName = "_PRINT_VARIABLE"
            Statement = New List(Of String)
            Statement.Add("_PRINT")
            Statement.Add("_VARIABLE")
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
            Statement = New List(Of String)
            Statement.Add("_DIM")
            Statement.Add("_VARIABLE")
            Statement.Add("_AS")
            Statement.Add("_STRING_TYPE")
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
            rule.SyntaxName = "_DIM_AS_EQ"
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
            Statement.Add("_BOOLEAN")
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

            Statement = New List(Of String)
            Statement.Add("_FOR")
            Statement.Add("_VARIABLE")
            Statement.Add("_EQUALS")
            Statement.Add("_NUMBER")
            Statement.Add("_TO")
            Statement.Add("_NUMBER")
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
            rule.SyntaxStatments.Add(Statement)
            Return rule

        End Function
        Public Function ADD_NEXT_FUNCTION()
            Dim rule As New AbstractSyntax
            Dim Statement As New List(Of String)
            rule.SyntaxStatments = New List(Of List(Of String))
            'DIM A AS INTEGER/BOOLEAN/STRING
            rule.SyntaxName = "_NEXT"
            Statement = New List(Of String)
            Statement.Add("_NEXT")
            Statement.Add("_VARIABLE")
            rule.SyntaxStatments.Add(Statement)
            Return rule

        End Function
#End Region
#Region "IF_THEN"
        Public Function ADD_IF_FUNCTION()
            Dim rule As New AbstractSyntax
            Dim Statement As New List(Of String)
            rule.SyntaxStatments = New List(Of List(Of String))
            'DIM A AS INTEGER/BOOLEAN/STRING
            rule.SyntaxName = "_IF"
            Statement = New List(Of String)
            Statement.Add("_IF")
            Statement.Add("Conditional_Operation")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_IF")
            rule.SyntaxStatments.Add(Statement)
            Return rule
        End Function
        Public Function ADD_THEN_FUNCTION()
            Dim rule As New AbstractSyntax
            Dim Statement As New List(Of String)
            rule.SyntaxStatments = New List(Of List(Of String))
            'DIM A AS INTEGER/BOOLEAN/STRING
            rule.SyntaxName = "_THEN"
            Statement = New List(Of String)
            Statement.Add("_THEN")
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
        Public Function ADD_ELSE_END_IF_FUNCTION()
            Dim rule As New AbstractSyntax
            rule.SyntaxStatments = New List(Of List(Of String))
            Dim Statement As New List(Of String)
            'DIM A AS INTEGER/BOOLEAN/STRING
            rule.SyntaxName = "_ELSE_END_IF"
            Statement = New List(Of String)
            Statement.Add("_ELSE")
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
            Statement.Add("Conditional_Operation")
            rule.SyntaxStatments.Add(Statement)
            Statement = New List(Of String)
            Statement.Add("_DO")
            Statement.Add("_WHILE")
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


