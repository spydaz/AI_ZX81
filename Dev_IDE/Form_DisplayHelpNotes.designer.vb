Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_DisplayHelpNotes
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_DisplayHelpNotes))
        Me.TextOut = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'TextOut
        '
        Me.TextOut.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.TextOut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextOut.Font = New System.Drawing.Font("Courier New", 10.15!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextOut.ForeColor = System.Drawing.SystemColors.InfoText
        Me.TextOut.Location = New System.Drawing.Point(0, 0)
        Me.TextOut.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TextOut.Name = "TextOut"
        Me.TextOut.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth
        Me.TextOut.Size = New System.Drawing.Size(1914, 623)
        Me.TextOut.TabIndex = 0
        Me.TextOut.Text = resources.GetString("TextOut.Text")
        '
        'Form_DisplayHelpNotes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1914, 623)
        Me.Controls.Add(Me.TextOut)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "Form_DisplayHelpNotes"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TextOut As RichTextBox
End Class
