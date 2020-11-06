Imports AI_ZX81.GRAMMARS
Imports AI_ZX81.STACK_VM

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
                Dim x = ASTgramm.CreateGrammar()
                For Each item2 In ASTgramm.CreateGrammar()
                    Dim Abstrac As New AbstractSyntax
                    Abstrac = GetSyntax(item, item2)
                    If Abstrac.RequiredTokens IsNot Nothing Then

                        AST_statement.Add(Abstrac)

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

        Public Sub executeON_CPU(ByRef VM As ZX81_VM, ByRef POPULATED_TREE As List(Of List(Of AbstractSyntax)))
            Dim My_VM As ZX81_VM = VM
            Dim NewCount = 0
            Dim OldCount = 1
            Dim OrgiginalProgramLength = POPULATED_TREE.Count
            'IF Counts are the same then 
            Do Until (NewCount = OldCount)
                ParseNext(OldCount, NewCount, POPULATED_TREE)
                'not right!!!!!
            Loop


            My_VM.SetProgram(POPULATED_TREE)
            'Program will need to be Parsed again for codeblocks (If then else,End If)), (for next), (while,LOOP)
            My_VM.ExecuteProgram()
        End Sub

        Public Function ParseNext(ByRef Original As Integer, ByRef newCount As Integer, ByRef Populated_tree As List(Of List(Of AbstractSyntax))) As List(Of List(Of AbstractSyntax))
            Original = Populated_tree.Count
            Dim Last As List(Of AbstractSyntax) = Populated_tree(Populated_tree.Count - 1)
            Populated_tree = CheckFOR_NEXT(RemoveEmptySyntax(Last))
            newCount = Populated_tree.Count
            Return Populated_tree

        End Function

        Public Function RemoveEmptySyntax(ByRef lst As List(Of AbstractSyntax)) As List(Of AbstractSyntax)
            Dim NEwLst As New List(Of AbstractSyntax)
            For Each item In lst
                If item.RequiredTokens.Count > 0 Then
                    NEwLst.Add(item)
                End If
            Next
            Return NEwLst
        End Function
        Public Function CheckFOR_NEXT(ByRef POPULATED_TREE As List(Of AbstractSyntax)) As List(Of List(Of AbstractSyntax))
            Dim Prog As New List(Of AbstractSyntax)
            Dim Pop_Tree_begin As New List(Of AbstractSyntax)
            Dim Pop_Tree_end As New List(Of AbstractSyntax)
            Dim Pop_Tree_Prog As New List(Of AbstractSyntax)
            Dim Pop_Tree As New List(Of List(Of AbstractSyntax))
            Dim Capturing As Boolean = False
            Dim detected As Boolean = False
            Dim Finished As Boolean = False
            Dim Position = 0
            Dim Found As Integer = 0


            For Each TOK In POPULATED_TREE
                If TOK.SyntaxName = "_FOR" Then
                    detected = True
                    Capturing = True
                    Found = Position
                End If
                If Capturing = True Then
                    Prog.Add(TOK)
                End If
                If TOK.SyntaxName = "_NEXT" Then
                    Capturing = False

                    Finished = True
                End If

                Position += 1
                If Finished = True Then

                    'Remove a range of items from a list, starting at index 0, for a count of 1)
                    'This will remove index 0, and 1!
                    'Removes the detected Collection
                    'Grab before
                    Pop_Tree_begin = POPULATED_TREE.GetRange(0, Found)
                    'Grab Loop
                    Pop_Tree_Prog = Prog
                    'Grab_End

                    Try
                        Pop_Tree_end = POPULATED_TREE.GetRange(Found + Prog.Count, POPULATED_TREE.Count)
                        'End was captured
                        Pop_Tree.Add(Pop_Tree_begin)
                        Pop_Tree.Add(Pop_Tree_Prog)
                        Pop_Tree.Add(Pop_Tree_end)
                    Catch ex As Exception
                        'Prog was end
                        Pop_Tree.Add(Pop_Tree_begin)
                        Pop_Tree.Add(Pop_Tree_Prog)
                    End Try
                    Found = 0
                End If
            Next
            Return Pop_Tree
        End Function
    End Class
End Namespace
