Public Class Form_DisplayHelpNotes

    Private Delegate Sub iClearText(ByRef mTextbox As RichTextBox, ByRef Message As String)

    Private Delegate Sub UpdateText(ByRef mTextbox As RichTextBox, ByRef Message As String)

    Private Delegate Sub UpdateTitle(ByRef mTextbox As Form, ByRef Message As String)

    Public Sub ClearText()
        _iClearText(Me.TextOut, "")
    End Sub

    Public Sub DisplayText(ByVal NewText As String)
        _UpdateText(Me.TextOut, NewText)
    End Sub

    Public Sub SetTitle(ByVal NewText As String)
        _UpdateTitle(Me, NewText)
    End Sub

    Private Sub _iClearText(ByRef mTextbox As RichTextBox, ByRef Message As String)
        If mTextbox.InvokeRequired = True Then
            'invoke delegate
            mTextbox.Invoke(New iClearText(AddressOf _iClearText), New Object() {mTextbox, Message})
        Else

            mTextbox.Clear()

        End If
    End Sub

    Private Sub _UpdateText(ByRef mTextbox As RichTextBox, ByRef Message As String)
        If mTextbox.InvokeRequired = True Then
            'invoke delegate
            mTextbox.Invoke(New UpdateText(AddressOf _UpdateText), New Object() {mTextbox, Message})
        Else

            mTextbox.AppendText(vbNewLine & Message)

        End If
    End Sub

    Private Sub _UpdateTitle(ByRef mTextbox As Form, ByRef Message As String)
        If mTextbox.InvokeRequired = True Then
            'invoke delegate
            mTextbox.Invoke(New UpdateTitle(AddressOf _UpdateTitle), New Object() {mTextbox, Message})
        Else

            mTextbox.Text = Message

        End If
    End Sub

    Private Sub Form_DisplayText_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
    End Sub


End Class