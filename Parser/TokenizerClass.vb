Public Class TokenizerClass

    ''' <summary>
    ''' Returns Characters in String as list
    ''' </summary>
    ''' <param name="InputStr"></param>
    ''' <returns></returns>
    Private Function Tokenizer(ByRef InputStr As String) As List(Of String)
        Tokenizer = New List(Of String)
        Dim Endstr As Integer = InputStr.Length
        For i = 1 To Endstr
            Tokenizer.Add(InputStr(i - 1))
        Next
    End Function
    Private Function Tokenize(ByRef Str As String) As List(Of String)
        Dim CharTokens As List(Of String) = Tokenizer(UCase(Str & " "))
        CharTokens.Add("EOF")
        Return CharTokens
    End Function
    ''' <summary>
    ''' If Token returned is nothing then no token has been found
    ''' </summary>
    ''' <param name="Tok">Current Str Character</param>
    ''' <returns></returns>
    Private Function GetToken(ByRef Tok As String, ByRef CurrentGramamr As List(Of GrammarRule)) As Token
        Dim NewTok As New Token
        Dim value As String = ""
        GetToken = Nothing



        For Each item In CurrentGramamr
                For Each Component In item.ComponentStrings

                    If Tok = Component Then
                        NewTok.Name = item.TagString
                        NewTok.Value = Tok
                        Return NewTok
                    Else
                    End If

                    'Next Component
                Next
                'Next grammar item
            Next


    End Function
    ''' <summary>
    ''' Returns an AST for the given text
    ''' </summary>
    ''' <param name="ProgramStr"></param>
    ''' <returns></returns>
    Private Function GetAST(ByRef ProgramStr As String) As List(Of Token)
        Dim ProGrammar As List(Of GrammarRule) = CreateGrammar()
        Dim StrTokens As List(Of String) = Tokenize(UCase(ProgramStr))
        Dim AST As New List(Of Token)
        Dim TokenStr As String = ""
        For Each item In StrTokens
            Dim TokenAdded As Boolean = False
            'Check Token
            Dim tok As Token = GetToken(item, ProGrammar)

            'CAPTURE INTEGER
            If CheckToken(tok) = "INTEGER" And BeginCapture = False And CapturingNum = False Then
                CapturingNum = True
                BeginCapture = True
                TokenStr &= item
            Else
                If BeginCapture = True And CapturingNum = True And CheckToken(tok) <> "INTEGER" Then
                    BeginCapture = False
                    CapturingNum = False

                    tok.Name = "INTEGER"
                    tok.Value = TokenStr
                    TokenStr = ""
                    AST.Add(tok)
                    TokenAdded = True
                End If

                If CapturingNum = True Then
                    TokenStr &= item
                End If
            End If
            'CAPTURE STRING
            If CheckToken(tok) = "QUOTE" And BeginCapture = False And CapturingStr = False Then
                CapturingStr = True
                BeginCapture = True
            Else
                If BeginCapture = True And CapturingStr = True And CheckToken(tok) = "QUOTE" Then
                    ' TokenStr &= item
                    BeginCapture = False
                    CapturingStr = False

                    tok.Name = "STRING"
                    tok.Value = TokenStr
                    TokenStr = ""
                    AST.Add(tok)
                    TokenAdded = True
                Else
                End If
                If CapturingStr = True Then
                    TokenStr &= item
                End If
            End If
            'CAPTURE DEFINED TERMS
            If CheckToken(tok) = "_LETTER" And BeginCapture = False And CapturingTerm = False Then
                'begin capture
                CapturingTerm = True
                BeginCapture = True
                TokenStr &= item
            Else
                'end capture
                If BeginCapture = True And CapturingTerm = True And tok.Name <> "_LETTER" Then
                    BeginCapture = False
                    CapturingTerm = False
                    ' TokenStr &= item
                    tok = GetToken(TokenStr, ProGrammar)
                    TokenStr = ""
                    If tok.Name IsNot Nothing Then
                        AST.Add(tok)
                        TokenAdded = True
                    Else

                    End If
                Else

                End If
                'capturing
                If BeginCapture = True And CapturingTerm = True Then
                    TokenStr &= item
                End If
            End If
            'CAPTURE VARIABLE
            If CheckToken(tok) = "VARIABLE" And BeginCapture = False And CapturingVar = False Then
                CapturingVar = True
                BeginCapture = True

            Else
                If BeginCapture = True And CapturingVar = True And CheckToken(tok) <> "VARIABLE" Then
                    BeginCapture = False
                    CapturingVar = False
                    tok.Name = "VARIABLE"
                    tok.Value = TokenStr
                    TokenStr = ""
                    TokenAdded = True
                    AST.Add(tok)

                Else
                End If
                If CapturingVar = True Then
                    TokenStr &= item
                End If
            End If


            If tok.Name = "_NEWLINE" Then
                AST.Add(tok)
                TokenAdded = True
            End If

            If tok.Name IsNot Nothing And TokenAdded = False And
                BeginCapture = False And
                CheckToken(tok) = "NULL" Then
                AST.Add(tok)
            Else
                ' TokenStr &= item
            End If
        Next
        ' AST = RemoveNewline(AST)
        Return RemoveTag(AST)
    End Function
    ''' <summary>
    ''' Each line of Text can be considered to be a statement 
    ''' </summary>
    ''' <param name="Statements">Array of statements</param>
    ''' <returns>Fully Populated AST TREE</returns>
    Private Function ParseStatementsGetAST(ByRef Statements() As String) As List(Of AbstractSyntaxToken)
        Dim AST As New List(Of List(Of Token))
        Dim Tree As New List(Of AbstractSyntaxToken)

        For Each item In Statements
            Dim SubAST As List(Of Token) = GetAST(item)
            If SubAST.Count > 0 Then
                Dim ASToken As New AbstractSyntaxToken
                ASToken.Name = "Expression"
                ASToken.Value = SubAST
                Tree.Add(ASToken)
                AST.Add(SubAST)
            End If
        Next
        Return Tree
    End Function
    Private Function ParseStatementsGetTokens(ByRef Statements() As String) As List(Of List(Of Token))
        Dim AST As New List(Of List(Of Token))


        For Each item In Statements
            Dim SubAST As List(Of Token) = GetAST(item)
            If SubAST.Count > 0 Then

                AST.Add(SubAST)
            End If
        Next
        Return AST
    End Function
    ''' <summary>
    ''' Spilts the Text in to statements based on line endings 
    ''' 
    ''' </summary>
    ''' <param name="Str"></param>
    ''' <returns></returns>
    Private Function GetStatements(ByRef Str As String) As String()
        ' Dim Statments() = InputText.Lines
        Dim Statments() = Str.Split(vbLf)
        Return Statments
    End Function
    Public Function Get_Abstract_Syntax_Token_TREE(ByRef Prog As String) As List(Of AbstractSyntaxToken)
        Return ParseStatementsGetAST(GetStatements(Prog))
    End Function
    Public Function Get_Token_TREE(ByRef Prog As String) As List(Of List(Of Token))
        Return ParseStatementsGetTokens(GetStatements(Prog))
    End Function
    Private Function RemoveTag(ByRef TokLst As List(Of Token)) As List(Of Token)
        Dim lst As New List(Of Token)
        For Each tok In TokLst
            If tok.Value = "EOF" Or tok.Name = "WHITESPACE" Then
            Else
                lst.Add(tok)
            End If
        Next
        Return lst
    End Function
    Private BeginCapture As Boolean = False
    Private CapturingStr As Boolean = False
    Private CapturingNum As Boolean = False
    Private CapturingVar As Boolean = False
    Private CapturingTerm As Boolean = False
    Private CapturingLst As Boolean = False
    Private Function CheckToken(ByRef Tok As Token)
        Select Case Tok.Name
            Case "_NUMBER"
                Return "INTEGER"
            Case "QUOTE"
                Return "QUOTE"
            Case "_LETTER"
                If BeginCapture = True And CapturingStr = True Then
                    Return "STRING"
                Else
                End If
                If BeginCapture = True And CapturingVar = True Then
                    Return "VARIABLE"
                Else
                End If
                Return "_LETTER"
            Case "VARIABLE"
                Return "VARIABLE"
            Case "NULL"
        End Select
        Return "NULL"
    End Function
End Class
