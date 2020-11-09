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
            Return CleanTree(ast_statments)
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



            My_VM.SetProgram(POPULATED_TREE)
            'Program will need to be Parsed again for codeblocks (If then else,End If)), (for next), (while,LOOP)
            My_VM.ExecuteProgram()
        End Sub
        Public Function CleanTree(ByRef POPULATED_TREE As List(Of List(Of AbstractSyntax)))
            CleanTree = New List(Of List(Of AbstractSyntax))
            For Each item In POPULATED_TREE
                CleanTree.Add(RemoveEmptySyntax(item))
            Next
        End Function
        Public Function ParseTree(ByRef POPULATED_TREE As List(Of List(Of AbstractSyntax))) As List(Of List(Of AbstractSyntax))
            POPULATED_TREE = CleanTree(ParseFOR_NEXT(POPULATED_TREE))
            POPULATED_TREE = CleanTree(ParseIF_ENDIF(POPULATED_TREE))
            POPULATED_TREE = CleanTree(ParseWHILE_LOOP(POPULATED_TREE))
            Return CleanTree(POPULATED_TREE)
        End Function

        Public Function ParseFOR_NEXT(ByRef POPULATED_TREE As List(Of List(Of AbstractSyntax))) As List(Of List(Of AbstractSyntax))
            '   POPULATED_TREE = CleanTree(POPULATED_TREE)
            Try


                'At this time there is only 1 ProgramList
                POPULATED_TREE = CleanTree(CheckFOR_NEXT(POPULATED_TREE.Item(0)))
                Dim Count_ As Integer = 1
                Dim Cnt_ As Integer = 0
                'Starts with 3 parts
                Do Until (Cnt_ = Count_)
                    Count_ = POPULATED_TREE.Count
                    Dim Last = POPULATED_TREE.Item(POPULATED_TREE.Count - 1)
                    POPULATED_TREE.RemoveAt(POPULATED_TREE.Count - 1)
                    POPULATED_TREE.AddRange(CleanTree(CheckFOR_NEXT(Last)))
                    Cnt_ = POPULATED_TREE.Count
                Loop
            Catch ex As Exception

            End Try
            Return POPULATED_TREE
        End Function
        Public Function ParseIF_ENDIF(ByRef POPULATED_TREE As List(Of List(Of AbstractSyntax))) As List(Of List(Of AbstractSyntax))
            '   POPULATED_TREE = CleanTree(POPULATED_TREE)
            'At this time there is only 1 ProgramList
            Try


                Dim nPOPULATED_TREE As New List(Of List(Of AbstractSyntax))
                For Each item In POPULATED_TREE
                    nPOPULATED_TREE.AddRange(CleanTree(CheckIF_ENDIF(item)))
                Next
                POPULATED_TREE = nPOPULATED_TREE

                Dim Count_ As Integer = 1
                Dim Cnt_ As Integer = 0
                'Starts with 3 parts
                Do Until (Cnt_ = Count_)
                    Count_ = POPULATED_TREE.Count
                    Dim Last = POPULATED_TREE.Item(POPULATED_TREE.Count - 1)
                    POPULATED_TREE.RemoveAt(POPULATED_TREE.Count - 1)
                    POPULATED_TREE.AddRange(CleanTree(CheckIF_ENDIF(Last)))
                    Cnt_ = POPULATED_TREE.Count
                Loop
            Catch ex As Exception

            End Try
            Return POPULATED_TREE
        End Function
        Public Function ParseWHILE_LOOP(ByRef POPULATED_TREE As List(Of List(Of AbstractSyntax))) As List(Of List(Of AbstractSyntax))
            Try


                '   POPULATED_TREE = CleanTree(POPULATED_TREE)
                'At this time there is only 1 ProgramList
                Dim nPOPULATED_TREE As New List(Of List(Of AbstractSyntax))
                For Each item In POPULATED_TREE
                    'nPOPULATED_TREE.Add(item)
                    nPOPULATED_TREE.AddRange(CleanTree(CheckWHILE_LOOP(item)))
                Next
                POPULATED_TREE = nPOPULATED_TREE

                Dim Count_ As Integer = 1
                Dim Cnt_ As Integer = 0
                'Starts with 3 parts
                Do Until (Cnt_ = Count_)
                    Count_ = POPULATED_TREE.Count
                    Dim Last = POPULATED_TREE.Item(POPULATED_TREE.Count - 1)
                    POPULATED_TREE.RemoveAt(POPULATED_TREE.Count - 1)
                    POPULATED_TREE.AddRange(CleanTree(CheckWHILE_LOOP(Last)))
                    Cnt_ = POPULATED_TREE.Count
                Loop
            Catch ex As Exception

            End Try
            Return POPULATED_TREE
        End Function
        Private Function CheckWHILE_LOOP(ByRef POPULATED_TREE As List(Of AbstractSyntax)) As List(Of List(Of AbstractSyntax))
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
                If TOK.SyntaxName = "_WHILE" Then
                    detected = True
                    Capturing = True
                    Found = Position
                End If
                If Capturing = True Then
                    Prog.Add(TOK)
                End If
                If TOK.SyntaxName = "_LOOP" Then
                    Capturing = False

                    Finished = True
                End If

                Position += 1
                If Finished = True And Capturing = False Then

                    'Remove a range of items from a list, starting at index 0, for a count of 1)
                    'This will remove index 0, and 1!
                    'Removes the detected Collection
                    'Grab before
                    Pop_Tree_begin = POPULATED_TREE.GetRange(0, Found)
                    'Grab Loop
                    Pop_Tree_Prog = Prog
                    'Grab_End

                    Try
                        Pop_Tree_end = POPULATED_TREE.GetRange(Found + Prog.Count, (POPULATED_TREE.Count) - (Found + Prog.Count))
                        'End was captured
                        Pop_Tree.Add(Pop_Tree_begin)
                        Dim First As New List(Of AbstractSyntax)
                        First.Add(Pop_Tree_Prog(0))
                        First.Add(Pop_Tree_Prog(1))
                        Dim Last As New List(Of AbstractSyntax)
                        Last.Add(Pop_Tree_Prog(Pop_Tree_Prog.Count - 1))
                        Pop_Tree_Prog.RemoveAt(0)
                        Pop_Tree_Prog.RemoveAt(0)
                        Pop_Tree_Prog.RemoveAt(Pop_Tree_Prog.Count - 1)
                        Pop_Tree.Add(First)
                        Pop_Tree.Add(Pop_Tree_Prog)
                        Pop_Tree.Add(Last)
                        Pop_Tree.Add(Pop_Tree_end)

                        Finished = False
                        Return Pop_Tree
                    Catch ex As Exception
                        'Prog was end
                        Pop_Tree.Add(Pop_Tree_begin)
                        Dim First As New List(Of AbstractSyntax)
                        First.Add(Pop_Tree_Prog(0))
                        First.Add(Pop_Tree_Prog(1))
                        Dim Last As New List(Of AbstractSyntax)
                        Last.Add(Pop_Tree_Prog(Pop_Tree_Prog.Count - 1))
                        Pop_Tree_Prog.RemoveAt(0)
                        Pop_Tree_Prog.RemoveAt(0)
                        Pop_Tree_Prog.RemoveAt(Pop_Tree_Prog.Count - 1)
                        Pop_Tree.Add(First)
                        Pop_Tree.Add(Pop_Tree_Prog)
                        Pop_Tree.Add(Last)
                        Finished = True
                        '  Return Pop_Tree
                    End Try
                    Found = 0
                End If
            Next
            If detected = False Then
                Pop_Tree = New List(Of List(Of AbstractSyntax))
                Pop_Tree.Add(POPULATED_TREE)

            End If
            Return Pop_Tree

        End Function
        Private Function CheckIF_ENDIF(ByRef POPULATED_TREE As List(Of AbstractSyntax)) As List(Of List(Of AbstractSyntax))
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
                If TOK.SyntaxName = "_IF" Then
                    detected = True
                    Capturing = True
                    Found = Position
                End If
                If Capturing = True Then
                    Prog.Add(TOK)
                End If
                If TOK.SyntaxName = "_END_IF" Then
                    Capturing = False

                    Finished = True
                End If

                Position += 1
                If Finished = True And Capturing = False Then

                    'Remove a range of items from a list, starting at index 0, for a count of 1)
                    'This will remove index 0, and 1!
                    'Removes the detected Collection
                    'Grab before
                    Pop_Tree_begin = POPULATED_TREE.GetRange(0, Found)
                    'Grab Loop
                    Pop_Tree_Prog = Prog
                    'Grab_End

                    Try
                        Pop_Tree_end = POPULATED_TREE.GetRange(Found + Prog.Count, (POPULATED_TREE.Count) - (Found + Prog.Count))
                        'End was captured
                        Pop_Tree.Add(Pop_Tree_begin)
                        Dim First As New List(Of AbstractSyntax)
                        'If 
                        First.Add(Pop_Tree_Prog(0))
                        'Conditional
                        First.Add(Pop_Tree_Prog(1))
                        Dim Last As New List(Of AbstractSyntax)
                        'End if
                        Last.Add(Pop_Tree_Prog(Pop_Tree_Prog.Count - 1))
                        'If 
                        Pop_Tree_Prog.RemoveAt(0)
                        'COnditional
                        Pop_Tree_Prog.RemoveAt(0)
                        'end if
                        Pop_Tree_Prog.RemoveAt(Pop_Tree_Prog.Count - 1)
                        'IF CONDITIONAL
                        Pop_Tree.Add(First)
                        'THEN ELSE
                        Pop_Tree.AddRange(CleanTree(CheckTHEN_ELSE(Pop_Tree_Prog)))
                        'END IF
                        Pop_Tree.Add(Last)
                        'REST
                        Pop_Tree.Add(Pop_Tree_end)
                        Finished = False
                        Return Pop_Tree
                    Catch ex As Exception
                        'Prog was end
                        Pop_Tree.Add(Pop_Tree_begin)
                        Dim First As New List(Of AbstractSyntax)
                        First.Add(Pop_Tree_Prog(0))
                        First.Add(Pop_Tree_Prog(1))
                        Dim Last As New List(Of AbstractSyntax)
                        Last.Add(Pop_Tree_Prog(Pop_Tree_Prog.Count - 1))
                        Pop_Tree_Prog.RemoveAt(0)
                        Pop_Tree_Prog.RemoveAt(0)
                        Pop_Tree_Prog.RemoveAt(Pop_Tree_Prog.Count - 1)
                        'IF CONDITIONAL
                        Pop_Tree.Add(First)
                        'THEN ELSE
                        Pop_Tree.AddRange(CleanTree(CheckTHEN_ELSE(Pop_Tree_Prog)))
                        'END IF
                        Pop_Tree.Add(Last)

                        Finished = True
                        '  Return Pop_Tree
                    End Try
                    Found = 0
                End If
            Next
            If detected = False Then
                Pop_Tree = New List(Of List(Of AbstractSyntax))
                Pop_Tree.Add(POPULATED_TREE)

            End If
            Return CleanTree(Pop_Tree)

        End Function
        Private Function CheckTHEN_ELSE(ByRef POPULATED_TREE As List(Of AbstractSyntax)) As List(Of List(Of AbstractSyntax))
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
                If TOK.SyntaxName = "_THEN" Then
                    detected = True
                    Capturing = True
                    Found = Position
                End If
                If Capturing = True Then
                    Prog.Add(TOK)
                End If
                If TOK.SyntaxName = "_ELSE" Then
                    Capturing = False

                    Finished = True
                End If

                Position += 1
                If Finished = True And Capturing = False Then

                    'Remove a range of items from a list, starting at index 0, for a count of 1)
                    'This will remove index 0, and 1!
                    'Removes the detected Collection
                    'Grab before
                    Pop_Tree_begin = POPULATED_TREE.GetRange(0, Found)
                    'Grab Loop
                    Pop_Tree_Prog = Prog
                    'Grab_End

                    Try
                        Pop_Tree_end = POPULATED_TREE.GetRange(Found + Prog.Count, (POPULATED_TREE.Count) - (Found + Prog.Count))
                        'End was captured
                        Pop_Tree.Add(Pop_Tree_begin)
                        Dim First As New List(Of AbstractSyntax)
                        First.Add(Pop_Tree_Prog(0))
                        Dim Last As New List(Of AbstractSyntax)
                        Last.Add(Pop_Tree_Prog(Pop_Tree_Prog.Count - 1))
                        Pop_Tree_Prog.RemoveAt(0)
                        Pop_Tree_Prog.RemoveAt(Pop_Tree_Prog.Count - 1)
                        Pop_Tree.Add(First)
                        Pop_Tree.Add(Pop_Tree_Prog)
                        Pop_Tree.Add(Last)
                        Pop_Tree.Add(Pop_Tree_end)
                        Finished = False
                        Return Pop_Tree
                    Catch ex As Exception
                        'Prog was end
                        Pop_Tree.Add(Pop_Tree_begin)
                        Dim First As New List(Of AbstractSyntax)
                        First.Add(Pop_Tree_Prog(0))
                        Dim Last As New List(Of AbstractSyntax)
                        Last.Add(Pop_Tree_Prog(Pop_Tree_Prog.Count - 1))
                        Pop_Tree_Prog.RemoveAt(0)
                        Pop_Tree_Prog.RemoveAt(Pop_Tree_Prog.Count - 1)
                        Pop_Tree.Add(First)
                        Pop_Tree.Add(Pop_Tree_Prog)
                        Pop_Tree.Add(Last)
                        Finished = True
                        '  Return Pop_Tree
                    End Try
                    Found = 0
                End If
            Next
            If detected = False Then
                Pop_Tree = New List(Of List(Of AbstractSyntax))
                Pop_Tree.Add(POPULATED_TREE)

            End If
            Return Pop_Tree

        End Function

        Public Function RemoveEmptySyntax(ByRef lst As List(Of AbstractSyntax)) As List(Of AbstractSyntax)
            Dim NEwLst As New List(Of AbstractSyntax)
            For Each item In lst
                If item.RequiredTokens.Count >= 1 Then
                    NEwLst.Add(item)
                End If
            Next
            Return NEwLst
        End Function
        Private Function CheckFOR_NEXT(ByRef POPULATED_TREE As List(Of AbstractSyntax)) As List(Of List(Of AbstractSyntax))
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
                If Finished = True And Capturing = False Then

                    'Remove a range of items from a list, starting at index 0, for a count of 1)
                    'This will remove index 0, and 1!
                    'Removes the detected Collection
                    'Grab before
                    Pop_Tree_begin = POPULATED_TREE.GetRange(0, Found)
                    'Grab Loop
                    Pop_Tree_Prog = Prog
                    'Grab_End

                    Try
                        Pop_Tree_end = POPULATED_TREE.GetRange(Found + Prog.Count, (POPULATED_TREE.Count) - (Found + Prog.Count))
                        'End was captured
                        Pop_Tree.Add(Pop_Tree_begin)
                        Dim First As New List(Of AbstractSyntax)
                        First.Add(Pop_Tree_Prog(0))
                        Dim Last As New List(Of AbstractSyntax)
                        Last.Add(Pop_Tree_Prog(Pop_Tree_Prog.Count - 1))
                        Pop_Tree_Prog.RemoveAt(0)
                        Pop_Tree_Prog.RemoveAt(Pop_Tree_Prog.Count - 1)
                        Pop_Tree.Add(First)
                        Pop_Tree.Add(Pop_Tree_Prog)
                        Pop_Tree.Add(Last)
                        Pop_Tree.Add(Pop_Tree_end)
                        Finished = False
                        Return Pop_Tree
                    Catch ex As Exception
                        'Prog was end
                        Pop_Tree.Add(Pop_Tree_begin)
                        Dim First As New List(Of AbstractSyntax)
                        First.Add(Pop_Tree_Prog(0))
                        Dim Last As New List(Of AbstractSyntax)
                        Last.Add(Pop_Tree_Prog(Pop_Tree_Prog.Count - 1))
                        Pop_Tree_Prog.RemoveAt(0)
                        Pop_Tree_Prog.RemoveAt(Pop_Tree_Prog.Count - 1)
                        Pop_Tree.Add(First)
                        Pop_Tree.Add(Pop_Tree_Prog)
                        Pop_Tree.Add(Last)
                        Finished = True
                        '  Return Pop_Tree
                    End Try
                    Found = 0
                End If
            Next
            If detected = False Then
                Pop_Tree = New List(Of List(Of AbstractSyntax))
                Pop_Tree.Add(POPULATED_TREE)

            End If
            Return Pop_Tree

        End Function
    End Class
End Namespace
