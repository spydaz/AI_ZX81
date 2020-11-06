Public Class ZX81_RAM
    Public Structure Variable
        Public iName As String
        Public iValue As String
        Public iType As String
    End Structure
    ''' <summary>
    ''' Currently only Variables can be stored
    ''' </summary>
    Public CurrentVars As List(Of Variable)
    Public Sub New()
        CurrentVars = New List(Of Variable)
    End Sub

    'Variables

    Public Sub UpdateVar(ByRef VarName As String, ByRef iVALUE As String)
        For Each item In CurrentVars
            If item.iName = VarName Then
                Dim NiTEM As Variable = item
                NiTEM.iValue = iVALUE
                CurrentVars.Remove(item)
                CurrentVars.Add(NiTEM)
                Exit For
            Else
            End If
        Next
    End Sub
    Public Function RemoveVar(ByRef Var As Variable)
        For Each item In CurrentVars
            If item.iName = Var.iName Then
                CurrentVars.Remove(item)
            End If
        Next
        Return Var
    End Function
    Public Sub AddVar(ByRef Var As Variable)
        If CheckVar(Var.iName) = False Then
            CurrentVars.Add(Var)
        End If
    End Sub
    Public Function CheckVar(ByRef VarName As String) As Boolean
        For Each item In CurrentVars
            If item.iName = VarName Then
                Return True
            End If
        Next
        CheckVar = False
    End Function
    Public Function GetVar(ByRef VarName As String) As String
        For Each item In CurrentVars
            If item.iName = VarName = True Then
                If item.iType = "BOOLEAN" Then
                    Select Case item.iValue
                        Case 0
                            Return "False"
                        Case 1
                            Return "True"
                        Case Else
                            Return item.iValue
                    End Select
                Else
                    Return item.iValue
                End If

            Else
            End If
        Next
        Return VarName
    End Function
End Class
