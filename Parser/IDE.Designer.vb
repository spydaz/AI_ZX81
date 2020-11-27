<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IDE
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.InputText = New System.Windows.Forms.RichTextBox()
        Me.ButtonParseAST = New System.Windows.Forms.Button()
        Me.ButtonParseTokens = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'InputText
        '
        Me.InputText.Dock = System.Windows.Forms.DockStyle.Top
        Me.InputText.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InputText.Location = New System.Drawing.Point(0, 0)
        Me.InputText.Name = "InputText"
        Me.InputText.Size = New System.Drawing.Size(800, 350)
        Me.InputText.TabIndex = 0
        Me.InputText.Text = ""
        '
        'ButtonParseAST
        '
        Me.ButtonParseAST.Location = New System.Drawing.Point(550, 356)
        Me.ButtonParseAST.Name = "ButtonParseAST"
        Me.ButtonParseAST.Size = New System.Drawing.Size(143, 23)
        Me.ButtonParseAST.TabIndex = 1
        Me.ButtonParseAST.Text = "PARSE AST"
        Me.ButtonParseAST.UseVisualStyleBackColor = True
        '
        'ButtonParseTokens
        '
        Me.ButtonParseTokens.Location = New System.Drawing.Point(550, 385)
        Me.ButtonParseTokens.Name = "ButtonParseTokens"
        Me.ButtonParseTokens.Size = New System.Drawing.Size(143, 23)
        Me.ButtonParseTokens.TabIndex = 1
        Me.ButtonParseTokens.Text = "PARSE Tokens"
        Me.ButtonParseTokens.UseVisualStyleBackColor = True
        '
        'IDE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.ButtonParseTokens)
        Me.Controls.Add(Me.ButtonParseAST)
        Me.Controls.Add(Me.InputText)
        Me.Name = "IDE"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents InputText As RichTextBox
    Friend WithEvents ButtonParseAST As Button
    Friend WithEvents ButtonParseTokens As Button
End Class
