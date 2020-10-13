Public Class Environment_Memory
    ''' <summary>
    ''' Currently only Variables can be stored
    ''' </summary>
    Public CurrentVars As List(Of AbstractExpressions.UnaryExpression)
    Public Sub New()
        CurrentVars = New List(Of AbstractExpressions.UnaryExpression)
    End Sub


    'Variables

    Public Function UpdateVar(ByRef Var As AbstractExpressions.UnaryExpression) As AbstractExpressions.UnaryExpression
        For Each item In CurrentVars
            If item.GetName = Var.GetName Then
                Var.iValue = item.GetValue
                Return Var

            End If
        Next
        Return Var
    End Function
    Public Function RemoveVar(ByRef Var As AbstractExpressions.UnaryExpression)
        For Each item In CurrentVars
            If item.GetName = Var.GetName Then
                CurrentVars.Remove(item)
            End If
        Next
        Return Var
    End Function
    Public Sub AddVar(ByRef Var As AbstractExpressions.UnaryExpression)
        If CheckVar(Var.GetName) = False Then
            CurrentVars.Add(Var)
        End If
    End Sub
    Public Function CheckVar(ByRef VarName As String) As Boolean
        For Each item In CurrentVars
            If item.GetName = VarName Then
                Return True
            End If
        Next
        CheckVar = False
    End Function
    Public Function GetVar(ByRef VarName As String) As String
        For Each item In CurrentVars
            If item.GetName = VarName Then
                Return item.iValue
            End If
        Next
        Return Nothing
    End Function
End Class
