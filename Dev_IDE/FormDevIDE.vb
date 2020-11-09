Imports AI_ZX81.Compiler
Imports AI_ZX81.GRAMMARS
Imports AI_ZX81.STACK_VM
Imports Newtonsoft.Json.JsonConvert
Public Class FormDevIDE
    Public Sub Print(ByRef Userinput As String)
        Me.RichTextBoxDisplayOutput.Text = Userinput
    End Sub
    Public Sub CLS()
        Me.RichTextBoxDisplayOutput.Text = ""
    End Sub
    Public Function Input(ByRef Message As String) As String
        '    Default = "1"    ' Set default.
        ' Display message, title, and default value.
        Return InputBox(Message, "INPUT")
    End Function

    Private Sub TextBoxEnterStatments_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxEnterStatments.KeyDown
        If e.KeyCode = Keys.Enter Then
            RichTextBoxProgram.Text &= UCase(TextBoxEnterStatments.Text) & vbNewLine
            TextBoxEnterStatments.Clear()

        Else
        End If
    End Sub

    Private Sub ButtonCompile_Click(sender As Object, e As EventArgs) Handles ButtonCompile.Click
        LexPL(GetCode)
    End Sub
    Public Function GetCode() As String
        Dim text = RichTextBoxProgram.Text.Replace("  ", " ")
        Return UCase(text)
    End Function
    Public Sub DisplayOutput(ByRef OutputStr As String)
        RichTextBoxDisplayOutput.Text = OutputStr
    End Sub
    Public Sub DisplayError(ByRef ErrorStr As String)
        TextBoxErrorOutput.Text &= ErrorStr
    End Sub
    Private Sub ButtonInsertCode_Click(sender As Object, e As EventArgs) Handles ButtonInsertCode.Click
        If ComboBoxSyntaxHelp.SelectedItem IsNot Nothing Then
            TextBoxEnterStatments.Text &= ComboBoxSyntaxHelp.SelectedItem.ToString

        End If
    End Sub
    Public Sub LexPL(ByRef UserProgram As String)
        On Error Resume Next
#Region "PL_GRAMMAR"
        'Dim Pharser As New LanguageCreator
        Dim CodeBlock As String = GetCode()
        Dim CurrentTokens As List(Of Token) = ClassLexer.PL_Lexer(UCase(CodeBlock))
        If CurrentTokens.Count > 0 Then


            If CurrentTokens(CurrentTokens.Count - 1).TokenRule.TAGSTRING = "END OF FILE" Then
                AST.Nodes.Clear()
                ' AST.Nodes.Add(ClassLexer.GetTokenExprTree(ClassLexer.CollectStatements(CurrentTokens)))
                Dim Tokentree As AbstractTokenTree = New ClassLexer(UCase(CodeBlock), PL_Grammar.CreatePLGrammar, "PL").Abstract_Token_Tree
                AST.Nodes.Add(ClassLexer.GetTokenExprTree(Tokentree))
                DisplayError("Lexer Completed" & vbNewLine &
                    "Code Fully Tokenized Success" & vbNewLine &
                    "Token Tree Generated")
                DisplayOutput(CodeBlock)
            Else
                ' AST.Nodes.Add(ClassLexer.GetTokenExprTree(ClassLexer.CollectStatements(CurrentTokens)))
                DisplayError("Error detected in Tokening Process" & vbNewLine & "At Invalid token" &
                    vbNewLine & CurrentTokens(CurrentTokens.Count - 1).ToJson() & vbNewLine &
                    vbNewLine & "Token Tree Generated")

                DisplayOutput(CodeBlock)
            End If
        End If
#End Region
    End Sub
    Public Sub LexEnglish(ByRef UserProgram As String)

        'Dim Pharser As New LanguageCreator
        Dim CodeBlock As String = GetCode()
        Dim CurrentTokens As List(Of Token) = ClassLexer.ENG_Lexer(UCase(CodeBlock))
        If CurrentTokens IsNot Nothing Then
            If CurrentTokens.Count > 0 Then
                If CurrentTokens(CurrentTokens.Count - 1).TokenRule.TAGSTRING = "END OF FILE" Then
                    DisplayError("Lexer Completed" & vbNewLine &
                    "Code Fully Tokenized Success" & vbNewLine &
                    "Token Tree Generated")
                    AST.Nodes.Clear()
                    Dim Lexer As New ClassLexer(UCase(CodeBlock), EnglishLanguageGrammar.SIMPLECONSTITUANTGRAMMAR, "ENG")
                    AST.Nodes.Add(ClassLexer.GetTokenExprTree(Lexer.Abstract_Token_Tree))
                    DisplayOutput(CodeBlock)
                Else
                    AST.Nodes.Clear()
                    AST.Nodes.Add(ClassLexer.GetTokenExprTree(ClassLexer.CollectStatements(CurrentTokens)))
                    DisplayError("Error detected in Tokening Process" & vbNewLine & "At Invalid token" &
                    vbNewLine & CurrentTokens(CurrentTokens.Count - 1).ToJson() & vbNewLine &
                    vbNewLine & "Token Tree Generated")
                    DisplayOutput(CodeBlock)
                End If
            Else
                DisplayError("At no valid tokens")
            End If
        End If
    End Sub
    Public Function ToJson(ByRef OBJ As Object) As String
        Return SerializeObject(OBJ)
    End Function

    Private Sub ButtonExecute_Click(sender As Object, e As EventArgs) Handles ButtonExecute.Click
        LexEnglish(GetCode)
    End Sub
    Dim CPU As ZX81_CPU
    Private Sub ButtonExecuteCpuCode_Click(sender As Object, e As EventArgs) Handles ButtonExecuteCpuCode.Click
        CLEAR()

        Dim PROG() As String = Split(RichTextBoxProgram.Text.Replace(vbCrLf, " "), " ")
        Dim InstructionLst As New List(Of String)
        Dim ROOT As New TreeNode
        ROOT.Text = "ASSEMBLY_CODE"
        Dim Count As Integer = 0
        For Each item In PROG
            Count += 1
            If item <> "" Then
                Dim NDE As New TreeNode
                NDE.Text = Count & ": " & item
                ROOT.Nodes.Add(NDE)
                InstructionLst.Add(item)
            End If
        Next
        'cpu - START

        'Dim CPU As ZX81_CPU = New ZX81_CPU(InstructionLst)
        'CPU.Run()
        Dim CPU As ZX81_CPU = New ZX81_CPU("Test", InstructionLst)
        DisplayOutput("CURRENT POINTER = " & CPU.Get_Instruction_Pointer_Position & vbNewLine & "CONTAINED DATA = " & CPU.Peek)
        AST.Nodes.Add(ROOT)
    End Sub

    Private Sub ButtonHelp_Click(sender As Object, e As EventArgs) Handles ButtonHelp.Click
        Dim frm As New Form_DisplayHelpNotes
        frm.Show()

    End Sub

    Private Sub ButtonParseTree_Click(sender As Object, e As EventArgs) Handles ButtonParseTree.Click
        CLEAR()
        Dim StatCount As Integer = 0
        Dim Errr As Boolean = False
        Dim CurrentTokens As List(Of Token) = ClassLexer.PL_Lexer(UCase(UCase(GetCode)))
        If CurrentTokens IsNot Nothing Then
            If CurrentTokens.Count > 0 Then
                Dim Tokentree As AbstractTokenTree = New ClassLexer(UCase(GetCode), PL_Grammar.CreatePLGrammar, "PL").Abstract_Token_Tree

                AST.Nodes.Clear()
                AST.Nodes.Add(ClassLexer.GetTokenExprTree(Tokentree))
                Dim tree = Parser.CleanTree(Parser.GetParseAST_Tree(Tokentree))
                tree = Parser.ParseTree(tree)
                For Each DefinedSyntax In Parser.CleanTree(tree)
                    StatCount += 1
                    If DefinedSyntax IsNot Nothing Then
                        Dim DefinedSyntaxNDE As New TreeNode
                        DefinedSyntaxNDE.Text = "Syntax -" & StatCount


                        For Each AbstractSyntaxDefinedToken In DefinedSyntax
                            If AbstractSyntaxDefinedToken IsNot Nothing Then


                                Dim AbstractSyntaxDefinedTokenNDE As New TreeNode
                                AbstractSyntaxDefinedTokenNDE.Text = AbstractSyntaxDefinedToken.SyntaxName
                                RichTextBoxDisplayOutput.Text &= AbstractSyntaxDefinedToken.SyntaxName & vbNewLine
                                If AbstractSyntaxDefinedToken.RequiredTokens IsNot Nothing Then

                                    For Each tok In AbstractSyntaxDefinedToken.RequiredTokens
                                        Dim tokNDE As New TreeNode
                                        tokNDE.Text = AbstractSyntaxDefinedToken.SyntaxName & " Value = " & tok.TokenValue
                                        AbstractSyntaxDefinedTokenNDE.Nodes.Add(tokNDE)
                                    Next


                                End If
                                DefinedSyntaxNDE.Nodes.Add(AbstractSyntaxDefinedTokenNDE)
                            Else
                                'skip
                            End If
                        Next

                        AST.Nodes.Add(DefinedSyntaxNDE)
                    Else
                        Errr = True
                        ' DisplayError("Error" & ToJson(item) & ")" & vbNewLine)
                    End If


                Next
                DisplayError("Parse Completed" & vbNewLine &
                             "Abstract Token Tree Generated" & vbNewLine &
                             "Number of CodeBlocks defined =" & StatCount)
                'add test --------------------
            Else
                Errr = True
                DisplayError("No tokens detected" & vbNewLine)
            End If
        Else
            Errr = True
            DisplayError("Parse NOT Completed" & vbNewLine & "Abstract Token NOT Tree Generated" & vbNewLine)
        End If
        If Errr = True Then
            DisplayError("TOKENS NOT FULLY DEFINED - NO SYNTAX DEFINED" & vbNewLine)
        End If
    End Sub

    Private Sub ButtonClearTree_Click(sender As Object, e As EventArgs) Handles ButtonClearTree.Click
        cLEAR()
    End Sub
    Public Sub CLEAR()
        AST.Nodes.Clear()
        TextBoxErrorOutput.Clear()
        TextBoxEnterStatments.Clear()
        RichTextBoxDisplayOutput.Clear()
    End Sub
    Dim Parser As New ClassParser
    Private Sub ButtonExecuteTree_Click(sender As Object, e As EventArgs) Handles ButtonExecuteTree.Click
        CLEAR()
        Dim Errr As Boolean = False
        Dim CurrentTokens As List(Of Token) = ClassLexer.PL_Lexer(UCase(UCase(GetCode)))
        If CurrentTokens IsNot Nothing Then
            If CurrentTokens.Count > 0 Then
                Dim Tokentree As AbstractTokenTree = New ClassLexer(UCase(GetCode), PL_Grammar.CreatePLGrammar, "PL").Abstract_Token_Tree

                AST.Nodes.Clear()
                AST.Nodes.Add(ClassLexer.GetTokenExprTree(Tokentree))
                Dim tree = Parser.GetParseAST_Tree(Tokentree)
                For Each DefinedSyntax In tree
                    If DefinedSyntax IsNot Nothing Then
                        Dim DefinedSyntaxNDE As New TreeNode
                        DefinedSyntaxNDE.Text = "Syntax"


                        For Each AbstractSyntaxDefinedToken In DefinedSyntax
                            Dim AbstractSyntaxDefinedTokenNDE As New TreeNode
                            AbstractSyntaxDefinedTokenNDE.Text = AbstractSyntaxDefinedToken.SyntaxName
                            RichTextBoxDisplayOutput.Text &= AbstractSyntaxDefinedToken.SyntaxName & vbNewLine
                            If AbstractSyntaxDefinedToken.RequiredTokens IsNot Nothing Then

                                For Each tok In AbstractSyntaxDefinedToken.RequiredTokens
                                    Dim tokNDE As New TreeNode
                                    tokNDE.Text = AbstractSyntaxDefinedToken.SyntaxName & " Value = " & tok.TokenValue
                                    AbstractSyntaxDefinedTokenNDE.Nodes.Add(tokNDE)
                                Next


                            End If
                            DefinedSyntaxNDE.Nodes.Add(AbstractSyntaxDefinedTokenNDE)
                        Next

                        AST.Nodes.Add(DefinedSyntaxNDE)
                    Else
                        Errr = True
                        ' DisplayError("Error" & ToJson(item) & ")" & vbNewLine)
                    End If

                Next
                DisplayError("Parse Completed" & vbNewLine & "Abstract Token Tree Generated" & vbNewLine)
                'add test --------------------
                RunTree(tree)
            Else
                Errr = True
                DisplayError("No tokens detected" & vbNewLine)
            End If
        Else
            Errr = True
            DisplayError("Parse NOT Completed" & vbNewLine & "Abstract Token NOT Tree Generated" & vbNewLine)
        End If
        If Errr = True Then
            DisplayError("TOKENS NOT FULLY DEFINED - NO SYNTAX DEFINED" & vbNewLine)
        End If
    End Sub


    Public Sub RunTree(ByRef tree As List(Of List(Of AbstractSyntax)))

        'Add Test --------------------
        Dim vm As New ZX81_VM("IDE")
        tree = Parser.ParseTree(tree)
        Parser.executeON_CPU(vm, tree)
    End Sub
End Class