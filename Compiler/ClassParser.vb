Imports AI_ZX81.GRAMMARS

'Production _ rules
' The Parse / Generator is used to manage statements created by the Tokenizer / Lexer
Namespace Compiler


    ''' <summary>
    ''' The Parse / Generator is used to manage statements created by the Tokenizer / Lexer
    ''' the Lexical statments are used to generate Abstract tokens 
    ''' Which are associated to Specific Expressions 
    ''' Which can be used to execute code to produce executable results 
    ''' or generate code for another environment or language to be executed on the platform.
    ''' An Abstract Syntax Grammar should be created from all required comands 
    ''' with thier associated expressions loaded
    ''' </summary>
    Public Class ClassParser
        ''' <summary>
        ''' Returns Abstract Syntax tree If Fails then syntax is incorrect not matching any known function
        ''' </summary>
        ''' <param name="AST"></param>
        ''' <returns></returns>
        Public Function GetParseAST_Tree(ByRef AST As AbstractTokenTree) As List(Of List(Of AbstractSyntax))
            Dim AST_statement As New List(Of AbstractSyntax)
            Dim ASTgramm As New AST_Grammar
            Dim ast_statments As New List(Of List(Of AbstractSyntax))
            For Each item In AST.Statments

                For Each item2 In ASTgramm.CreateGrammar()
                    Dim Abstrac As New AbstractSyntax
                    Abstrac = GetSyntax(item, item2)
                    If Abstrac.RequiredTokens IsNot Nothing Then
                        If Abstrac.RequiredTokens.Count > 1 Then
                            AST_statement.Add(Abstrac)
                        Else

                        End If
                    Else
                    End If
                Next


            Next
            If AST_statement.Count > 0 Then
                ast_statments.Add(AST_statement)
            End If
            Return ast_statments
        End Function
        ''' <summary>
        ''' Given as statment (Token Statment) 
        ''' Return an Abstract Syntax Token (Used for execution)
        ''' </summary>
        ''' <param name="DefinedTokens">Statment From Lexer</param>
        ''' <param name="KnownSyntax">Known Syntax Item(Parser Gramar)</param>
        ''' <returns></returns>
        Public Function GetSyntax(ByRef DefinedTokens As List(Of Token),
                              ByRef KnownSyntax As AbstractSyntax) As AbstractSyntax
            Dim NewSyntax As New AbstractSyntax
            Dim CompList As New List(Of String)
            'Build Comparison list
            For Each item In DefinedTokens
                CompList.Add(item.DetectedType.ToString)
            Next
            'Search each statement in Search Syntaxes
            For Each item In KnownSyntax.SyntaxStatments
                'Compare list
                If CompList.SequenceEqual(item) = True Then
                    NewSyntax = KnownSyntax
                    NewSyntax.RequiredTokens = DefinedTokens
                    ' Returns syntax detected with tokens attached
                    'For later generator

                    Return NewSyntax
                Else
                End If
            Next

            Return Nothing
        End Function
    End Class
End Namespace
