Namespace STACK_VM
    ''' <summary>
    ''' Memory frame for Variables 
    ''' </summary>
    Public Class StackMemoryFrame
        Public Structure Var
            Public Value As Integer
            Public VarNumber As String
        End Structure
        Public ReturnAddress As Integer
        Public Variables As List(Of Var)

        Public Sub New(ByRef ReturnAddress As Integer)
            ReturnAddress = ReturnAddress
            Variables = New List(Of Var)
        End Sub
        Public Function GetReturnAddress() As Integer

            Return ReturnAddress
        End Function
        Public Function GetVar(ByRef VarNumber As String) As Integer
            For Each item In Variables
                If item.VarNumber = VarNumber Then
                    Return item.Value

                End If
            Next
            Return 0
        End Function
        Public Sub SetVar(ByRef VarNumber As String, ByRef value As Integer)
            Dim item As New Var
            item.VarNumber = VarNumber
            item.Value = value

            Variables.Add(item)
        End Sub
    End Class
End Namespace

