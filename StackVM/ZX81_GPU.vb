Imports Newtonsoft.Json.JsonConvert
Namespace STACK_VM
    Public Class ZX81_GPU
        Private iMonitorConsole As FormDisplayConsole

        Public Sub New()
            iMonitorConsole = New FormDisplayConsole
        End Sub

        Public Sub ConsolePrint(ByRef Str As String)
            If iMonitorConsole.Visible = False Then
                iMonitorConsole.Show()
            Else
            End If
            iMonitorConsole.Print(Str)
        End Sub
        Public Sub Console_CLS()
            If iMonitorConsole.Visible = False Then
                iMonitorConsole.Show()
            Else
            End If
            iMonitorConsole.CLS()
        End Sub
    End Class
End Namespace
