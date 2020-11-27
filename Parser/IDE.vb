Public Class IDE
    Private Parser As New TokenizerClass
    Private Sub ButtonParse_Click(sender As Object, e As EventArgs) Handles ButtonParseAST.Click

        'Display Output
        For Each item In Parser.Get_Abstract_Syntax_Token_TREE(ProgText)
            InputText.Text &= "Statement" & vbNewLine
            InputText.Text &= item.ToJson & vbNewLine
            'For Each item2 In item.Value
            '    InputText.Text &= item2.ToJson & vbNewLine
            'Next
        Next

    End Sub


    Public Sub ClearText()
        InputText.Clear()
    End Sub

    Public Function ProgText() As String
        Dim ProgStr As String = InputText.Text
        ClearText()
        Return ProgStr
    End Function

    Private Sub ButtonParseTokens_Click(sender As Object, e As EventArgs) Handles ButtonParseTokens.Click

        'Display Output
        For Each item In Parser.Get_Token_TREE(ProgText)
            InputText.Text &= "Statement" & vbNewLine
            'InputText.Text &= item.ToJson & vbNewLine
            For Each item2 In item
                InputText.Text &= item2.ToJson & vbNewLine
            Next
        Next
    End Sub
End Class
