Imports Newtonsoft.Json.JsonConvert

Public Module Ext
    <System.Runtime.CompilerServices.Extension()>
    Public Function SplitAtNewLine(input As String) As IEnumerable(Of String)
        Return input.Split({Environment.NewLine}, StringSplitOptions.None)
    End Function
    <System.Runtime.CompilerServices.Extension()>
    Public Function ExtractLastChar(ByRef InputStr As String) As String
        ExtractLastChar = Right(InputStr, 1)
    End Function
    <System.Runtime.CompilerServices.Extension()>
    Public Function ExtractFirstChar(ByRef InputStr As String) As String
        ExtractFirstChar = Left(InputStr, 1)
    End Function
    ''' <summary>
    ''' Rule for tagging text
    ''' </summary>
    Public Class GrammarRule
        Public Function ToJson() As String
            Return SerializeObject(Me)
        End Function
        Public ComponentStrings As List(Of String)
        Public TagString As String
        Public Sub New()
            ComponentStrings = New List(Of String)
        End Sub
    End Class
    ''' <summary>
    ''' AbstractSyntax Basic TOKEN
    ''' </summary>
    Public Structure Token
        Public Function ToJson() As String
            Return SerializeObject(Me)
        End Function
        Public Name As String
        Public Value As String
    End Structure

    Public Structure AbstractSyntaxToken
        Public Function ToJson() As String
            Return SerializeObject(Me)
        End Function
        Public Name As String
        Public Value As List(Of Token)
    End Structure
End Module
