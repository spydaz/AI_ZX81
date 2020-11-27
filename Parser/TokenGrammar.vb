Public Module TokenGrammar
    'Grammar Rules



    Public Function CreateGrammar() As List(Of GrammarRule)
        Dim grammar As New List(Of GrammarRule)
        Dim Rule As New GrammarRule

        'NEWLINE
        Rule = New GrammarRule
        Rule.TagString = "_NEW_LINE"
        Rule.ComponentStrings = New List(Of String)
        Rule.ComponentStrings.Add(ControlChars.CrLf)

        Rule.ComponentStrings.Add(vbLf)
        grammar.Add(Rule)


        'List
        Rule = New GrammarRule
        Rule.TagString = "CALCULATION_BEGIN"
        Rule.ComponentStrings.Add("(")
        grammar.Add(Rule)
        Rule = New GrammarRule
        Rule.TagString = "CALCULATION_END"
        Rule.ComponentStrings.Add(")")
        grammar.Add(Rule)
        Rule = New GrammarRule
        Rule.TagString = "CODEBLOCK_BEGIN"
        Rule.ComponentStrings.Add("{")
        grammar.Add(Rule)
        Rule = New GrammarRule
        Rule.TagString = "CODEBLOCK_END"
        Rule.ComponentStrings.Add("}")
        grammar.Add(Rule)
        Rule = New GrammarRule
        Rule.TagString = "LIST_BEGIN"
        Rule.ComponentStrings.Add("[")
        grammar.Add(Rule)
        Rule = New GrammarRule
        Rule.TagString = "LIST_END"
        Rule.ComponentStrings.Add("]")
        grammar.Add(Rule)
        Rule = New GrammarRule
        Rule.TagString = "COMMA"
        Rule.ComponentStrings.Add(",")
        grammar.Add(Rule)


        'End of File marker
        Rule = New GrammarRule
        Rule.TagString = "END_OF_PROGRAM"
        Rule.ComponentStrings.Add("EOF")
        grammar.Add(Rule)
        'String Identifier
        Rule = New GrammarRule
        Rule.TagString = "QUOTE"
        'Double Quote
        Rule.ComponentStrings.Add(Chr(34))
        'Single Quote
        'Rule.ComponentStrings.Add(Chr(39))
        grammar.Add(Rule)
        'Alphabet
        grammar.Add(GETLETTERSRULE)
        'Numericals
        grammar.Add(GETNUMBERRULE)
        ''Operators
        grammar.Add(ADDFLOATPOINT)
        grammar.Add(GETMATHOPERATORS)
        grammar.Add(GETCONDITIONALOPERATORS)
        Rule = New GrammarRule
        Rule.TagString = "DIM"
        Rule.ComponentStrings.Add("DIM")
        grammar.Add(Rule)
        Rule = New GrammarRule
        Rule.TagString = "VARIABLE"
        Rule.ComponentStrings.Add("$")
        grammar.Add(Rule)
        Rule = New GrammarRule
        Rule.TagString = "WHITESPACE"
        Rule.ComponentStrings.Add(" ")
        grammar.Add(Rule)
        'PRINT
        Rule = New GrammarRule
        Rule.TagString = "PRINT"
        Rule.ComponentStrings.Add("PRINT")
        grammar.Add(Rule)
        'IF Then Else
        Rule = New GrammarRule
        Rule.TagString = "IF"
        Rule.ComponentStrings.Add("IF")
        grammar.Add(Rule)
        Rule = New GrammarRule
        Rule.TagString = "THEN"
        Rule.ComponentStrings.Add("THEN")
        grammar.Add(Rule)
        Rule = New GrammarRule
        Rule.TagString = "ELSE"
        Rule.ComponentStrings.Add("ELSE")
        grammar.Add(Rule)
        Rule = New GrammarRule
        Rule.TagString = "END"
        Rule.ComponentStrings.Add("END")
        grammar.Add(Rule)
        'Do while / DO Until LOOP
        Rule = New GrammarRule
        Rule.TagString = "WHILE"
        Rule.ComponentStrings.Add("WHILE")
        grammar.Add(Rule)
        Rule = New GrammarRule
        Rule.TagString = "DO"
        Rule.ComponentStrings.Add("DO")
        grammar.Add(Rule)
        Rule = New GrammarRule
        Rule.TagString = "UNTIL"
        Rule.ComponentStrings.Add("UNTIL")
        grammar.Add(Rule)
        Rule = New GrammarRule
        Rule.TagString = "LOOP"
        Rule.ComponentStrings.Add("LOOP")
        grammar.Add(Rule)
        'TRUE/FALSE
        Rule = New GrammarRule
        Rule.TagString = "TRUE"
        Rule.ComponentStrings.Add("TRUE")
        grammar.Add(Rule)
        Rule = New GrammarRule
        Rule.TagString = "FALSE"
        Rule.ComponentStrings.Add("FALSE")
        grammar.Add(Rule)
        'FOR/NEXT
        Rule = New GrammarRule
        Rule.TagString = "FOR"
        Rule.ComponentStrings.Add("FOR")
        grammar.Add(Rule)
        Rule = New GrammarRule
        Rule.TagString = "EACH"
        Rule.ComponentStrings.Add("EACH")
        grammar.Add(Rule)
        Rule = New GrammarRule
        Rule.TagString = "IN"
        Rule.ComponentStrings.Add("IN")
        grammar.Add(Rule)
        Rule = New GrammarRule
        Rule.TagString = "TO"
        Rule.ComponentStrings.Add("TO")
        grammar.Add(Rule)
        Rule = New GrammarRule
        Rule.TagString = "NEXT"
        Rule.ComponentStrings.Add("NEXT")
        grammar.Add(Rule)


        Rule = New GrammarRule
        Rule.TagString = "ASSIGN"
        Rule.ComponentStrings.Add("=")
        grammar.Add(Rule)



        Return grammar
    End Function
    Private Function GETNUMBERRULE() As GrammarRule
        Dim RULE = New GrammarRule
        RULE.TagString = "_NUMBER"
        RULE.ComponentStrings = New List(Of String)
        RULE.ComponentStrings.Add("1")
        RULE.ComponentStrings.Add("2")
        RULE.ComponentStrings.Add("3")
        RULE.ComponentStrings.Add("4")
        RULE.ComponentStrings.Add("5")
        RULE.ComponentStrings.Add("6")
        RULE.ComponentStrings.Add("7")
        RULE.ComponentStrings.Add("8")
        RULE.ComponentStrings.Add("9")
        RULE.ComponentStrings.Add("0")
        Return RULE
    End Function
    Private Function GETLETTERSRULE() As GrammarRule
        Dim RULE = New GrammarRule
        RULE.TagString = "_LETTER"
        RULE.ComponentStrings = New List(Of String)
        RULE.ComponentStrings.Add("A")
        RULE.ComponentStrings.Add("B")
        RULE.ComponentStrings.Add("C")
        RULE.ComponentStrings.Add("D")
        RULE.ComponentStrings.Add("E")
        RULE.ComponentStrings.Add("F")
        RULE.ComponentStrings.Add("G")
        RULE.ComponentStrings.Add("H")
        RULE.ComponentStrings.Add("I")
        RULE.ComponentStrings.Add("J")
        RULE.ComponentStrings.Add("K")
        RULE.ComponentStrings.Add("L")
        RULE.ComponentStrings.Add("M")
        RULE.ComponentStrings.Add("N")
        RULE.ComponentStrings.Add("O")
        RULE.ComponentStrings.Add("P")
        RULE.ComponentStrings.Add("Q")
        RULE.ComponentStrings.Add("R")
        RULE.ComponentStrings.Add("S")
        RULE.ComponentStrings.Add("T")
        RULE.ComponentStrings.Add("U")
        RULE.ComponentStrings.Add("V")
        RULE.ComponentStrings.Add("W")
        RULE.ComponentStrings.Add("X")
        RULE.ComponentStrings.Add("Y")
        RULE.ComponentStrings.Add("Z")
        Return RULE

    End Function
    Private Function GETCONDITIONALOPERATORS() As GrammarRule
        Dim RULE = New GrammarRule
        RULE.TagString = "_CONDITIONALOPERATORS"
        RULE.ComponentStrings = New List(Of String)
        RULE.ComponentStrings.Add("<")
        RULE.ComponentStrings.Add(">")
        Return RULE
    End Function
    Private Function GETMATHOPERATORS() As GrammarRule
        Dim RULE = New GrammarRule
        RULE.TagString = "_MATHOPERATORS"
        RULE.ComponentStrings.Add("+")
        RULE.ComponentStrings.Add("-")
        RULE.ComponentStrings.Add("/")
        RULE.ComponentStrings.Add("*")
        Return RULE
    End Function
    Private Function ADDFLOATPOINT() As GrammarRule
        Dim RULE = New GrammarRule
        RULE.TagString = "_DECIMAL_POINT"
        RULE.ComponentStrings = New List(Of String)
        RULE.ComponentStrings.Add(".")
        Return RULE
    End Function

End Module
