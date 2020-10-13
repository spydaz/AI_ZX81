<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormDevIDE
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormDevIDE))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.RichTextBoxDisplayOutput = New System.Windows.Forms.RichTextBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.AST = New System.Windows.Forms.TreeView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.RichTextBoxProgram = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TextBoxErrorOutput = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.TextBoxEnterStatments = New System.Windows.Forms.TextBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.ButtonExecuteCpuCode = New System.Windows.Forms.Button()
        Me.ButtonExecute = New System.Windows.Forms.Button()
        Me.ButtonCompile = New System.Windows.Forms.Button()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.ButtonInsertCode = New System.Windows.Forms.Button()
        Me.ComboBoxSyntaxHelp = New System.Windows.Forms.ComboBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Black
        Me.GroupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox1.Controls.Add(Me.GroupBox6)
        Me.GroupBox1.Controls.Add(Me.GroupBox5)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Font = New System.Drawing.Font("Comic Sans MS", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(1540, 400)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "SpydazWeb AI Programming Language Editor"
        '
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.Color.Black
        Me.GroupBox6.BackgroundImage = Global.AI_ZX81.My.Resources.Resources.Dell_UltraSharp_27
        Me.GroupBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox6.Controls.Add(Me.RichTextBoxDisplayOutput)
        Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox6.ForeColor = System.Drawing.Color.Lime
        Me.GroupBox6.Location = New System.Drawing.Point(718, 21)
        Me.GroupBox6.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox6.Size = New System.Drawing.Size(319, 377)
        Me.GroupBox6.TabIndex = 2
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Program Output"
        '
        'RichTextBoxDisplayOutput
        '
        Me.RichTextBoxDisplayOutput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBoxDisplayOutput.BackColor = System.Drawing.Color.Gainsboro
        Me.RichTextBoxDisplayOutput.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBoxDisplayOutput.Location = New System.Drawing.Point(7, 22)
        Me.RichTextBoxDisplayOutput.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.RichTextBoxDisplayOutput.Name = "RichTextBoxDisplayOutput"
        Me.RichTextBoxDisplayOutput.Size = New System.Drawing.Size(306, 249)
        Me.RichTextBoxDisplayOutput.TabIndex = 0
        Me.RichTextBoxDisplayOutput.Text = ""
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Black
        Me.GroupBox5.Controls.Add(Me.AST)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Right
        Me.GroupBox5.Font = New System.Drawing.Font("Comic Sans MS", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.Color.Aqua
        Me.GroupBox5.Location = New System.Drawing.Point(1037, 21)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox5.Size = New System.Drawing.Size(500, 377)
        Me.GroupBox5.TabIndex = 1
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Abstract Syntax Tree"
        '
        'AST
        '
        Me.AST.BackColor = System.Drawing.SystemColors.Info
        Me.AST.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AST.Font = New System.Drawing.Font("Comic Sans MS", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AST.Location = New System.Drawing.Point(3, 21)
        Me.AST.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.AST.Name = "AST"
        Me.AST.Size = New System.Drawing.Size(494, 354)
        Me.AST.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Black
        Me.GroupBox2.Controls.Add(Me.RichTextBoxProgram)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox2.Font = New System.Drawing.Font("Comic Sans MS", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Yellow
        Me.GroupBox2.Location = New System.Drawing.Point(3, 21)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox2.Size = New System.Drawing.Size(715, 377)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Program"
        '
        'RichTextBoxProgram
        '
        Me.RichTextBoxProgram.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBoxProgram.Location = New System.Drawing.Point(3, 21)
        Me.RichTextBoxProgram.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.RichTextBoxProgram.Multiline = True
        Me.RichTextBoxProgram.Name = "RichTextBoxProgram"
        Me.RichTextBoxProgram.Size = New System.Drawing.Size(709, 354)
        Me.RichTextBoxProgram.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Black
        Me.GroupBox3.Controls.Add(Me.TextBoxErrorOutput)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox3.Font = New System.Drawing.Font("Comic Sans MS", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.Lime
        Me.GroupBox3.Location = New System.Drawing.Point(0, 598)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox3.Size = New System.Drawing.Size(1540, 228)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Error Output"
        '
        'TextBoxErrorOutput
        '
        Me.TextBoxErrorOutput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxErrorOutput.Font = New System.Drawing.Font("Consolas", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxErrorOutput.Location = New System.Drawing.Point(3, 21)
        Me.TextBoxErrorOutput.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TextBoxErrorOutput.Multiline = True
        Me.TextBoxErrorOutput.Name = "TextBoxErrorOutput"
        Me.TextBoxErrorOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBoxErrorOutput.Size = New System.Drawing.Size(1534, 205)
        Me.TextBoxErrorOutput.TabIndex = 0
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.Black
        Me.GroupBox4.Controls.Add(Me.TextBoxEnterStatments)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox4.Font = New System.Drawing.Font("Comic Sans MS", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.ForeColor = System.Drawing.Color.Aqua
        Me.GroupBox4.Location = New System.Drawing.Point(0, 400)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox4.Size = New System.Drawing.Size(1540, 81)
        Me.GroupBox4.TabIndex = 2
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Enter Statements"
        '
        'TextBoxEnterStatments
        '
        Me.TextBoxEnterStatments.BackColor = System.Drawing.SystemColors.Info
        Me.TextBoxEnterStatments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxEnterStatments.Font = New System.Drawing.Font("Comic Sans MS", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxEnterStatments.Location = New System.Drawing.Point(3, 21)
        Me.TextBoxEnterStatments.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TextBoxEnterStatments.Name = "TextBoxEnterStatments"
        Me.TextBoxEnterStatments.Size = New System.Drawing.Size(1534, 40)
        Me.TextBoxEnterStatments.TabIndex = 0
        '
        'GroupBox7
        '
        Me.GroupBox7.BackColor = System.Drawing.Color.Black
        Me.GroupBox7.Controls.Add(Me.ButtonExecuteCpuCode)
        Me.GroupBox7.Controls.Add(Me.ButtonExecute)
        Me.GroupBox7.Controls.Add(Me.ButtonCompile)
        Me.GroupBox7.Controls.Add(Me.GroupBox8)
        Me.GroupBox7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox7.Font = New System.Drawing.Font("Comic Sans MS", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox7.ForeColor = System.Drawing.Color.White
        Me.GroupBox7.Location = New System.Drawing.Point(0, 481)
        Me.GroupBox7.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox7.MinimumSize = New System.Drawing.Size(1559, 101)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox7.Size = New System.Drawing.Size(1559, 117)
        Me.GroupBox7.TabIndex = 3
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Tools"
        '
        'ButtonExecuteCpuCode
        '
        Me.ButtonExecuteCpuCode.BackColor = System.Drawing.Color.PeachPuff
        Me.ButtonExecuteCpuCode.ForeColor = System.Drawing.Color.Black
        Me.ButtonExecuteCpuCode.Location = New System.Drawing.Point(1426, 21)
        Me.ButtonExecuteCpuCode.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ButtonExecuteCpuCode.Name = "ButtonExecuteCpuCode"
        Me.ButtonExecuteCpuCode.Size = New System.Drawing.Size(103, 82)
        Me.ButtonExecuteCpuCode.TabIndex = 2
        Me.ButtonExecuteCpuCode.Text = "Execute on Virtual CPU"
        Me.ButtonExecuteCpuCode.UseVisualStyleBackColor = False
        '
        'ButtonExecute
        '
        Me.ButtonExecute.BackColor = System.Drawing.Color.Black
        Me.ButtonExecute.Location = New System.Drawing.Point(1261, 64)
        Me.ButtonExecute.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ButtonExecute.Name = "ButtonExecute"
        Me.ButtonExecute.Size = New System.Drawing.Size(159, 38)
        Me.ButtonExecute.TabIndex = 0
        Me.ButtonExecute.Text = "Lex English"
        Me.ButtonExecute.UseVisualStyleBackColor = False
        '
        'ButtonCompile
        '
        Me.ButtonCompile.BackColor = System.Drawing.Color.Black
        Me.ButtonCompile.Location = New System.Drawing.Point(1261, 22)
        Me.ButtonCompile.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ButtonCompile.Name = "ButtonCompile"
        Me.ButtonCompile.Size = New System.Drawing.Size(159, 38)
        Me.ButtonCompile.TabIndex = 0
        Me.ButtonCompile.Text = "Lex ProgramLogic"
        Me.ButtonCompile.UseVisualStyleBackColor = False
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.ButtonInsertCode)
        Me.GroupBox8.Controls.Add(Me.ComboBoxSyntaxHelp)
        Me.GroupBox8.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox8.ForeColor = System.Drawing.Color.Aqua
        Me.GroupBox8.Location = New System.Drawing.Point(3, 21)
        Me.GroupBox8.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox8.Size = New System.Drawing.Size(595, 94)
        Me.GroupBox8.TabIndex = 1
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Select Syntax"
        '
        'ButtonInsertCode
        '
        Me.ButtonInsertCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonInsertCode.BackColor = System.Drawing.Color.Black
        Me.ButtonInsertCode.Location = New System.Drawing.Point(491, 53)
        Me.ButtonInsertCode.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ButtonInsertCode.Name = "ButtonInsertCode"
        Me.ButtonInsertCode.Size = New System.Drawing.Size(99, 38)
        Me.ButtonInsertCode.TabIndex = 1
        Me.ButtonInsertCode.Text = "Insert"
        Me.ButtonInsertCode.UseVisualStyleBackColor = False
        '
        'ComboBoxSyntaxHelp
        '
        Me.ComboBoxSyntaxHelp.AutoCompleteCustomSource.AddRange(New String() {"DIM $VAR$ AS STRING", "DIM $VAR$ AS INT", "DIM $VAR$ AS BOOLEAN", "DIM $VAR$ AS STRING = ""HELLO WORLD""", "DIM $VAR$ AS INT = 0", "DIM $VAR$ AS BOOLEAN = TRUE", "DIM $VAR$ AS BOOLEAN = FALSE", "PRINT $VAR$", "PRINT 1", "PRINT ""HELLO WORLD""", "IF  ( $VAR$ > $VAR$  )THEN ", "{ }", "( )", "ELSE", "END IF", "FOR $VAR$ = 1 to 10 ", "NEXT $VAR$", "DO WHILE ", "LOOP"})
        Me.ComboBoxSyntaxHelp.BackColor = System.Drawing.Color.MistyRose
        Me.ComboBoxSyntaxHelp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBoxSyntaxHelp.FormattingEnabled = True
        Me.ComboBoxSyntaxHelp.Items.AddRange(New Object() {"Print ""Hello World""", "DIM $VAR$ AS STRING = """"", "DIM $VAR$ AS BOOLEAN = TRUE", "DIM $VAR$ AS INT = 0", "FOR $VAR$ = 1 TO 10", "{ }", "NEXT $VAR$", "IF ( $VAR$ = TRUE) THEN ", "{ }", "ELSE", "{ }", "END IF", "DO WHILE ( $VAR$ = TRUE )", "{ }", "loop", "PUSH", "ADD", "HALT", "MUL", "DIV", "SUB", "PUSH 1 NOT HALT - testUnaryNotTrue - RESPONSE = 0", "PUSH 0 NOT HALT - testUnaryNotFalse - RESPONSE = 1", "PUSH 1 PUSH 1 AND HALT testAndTrueTrue - RESPONSE = 1", "PUSH 1 PUSH 0 OR, HALT  - testOrTrueFalse - RESPONSE = 1", "PUSH, 42, POP, HALT - testPop - RESPONSE - NULL / 0", "PUSH, 42, DUP, HALT - testDup - RESPONSE 42", "JMP 3 HALT JMP 2  - testUnconditionalJump - RESPONSE = 3", "PUSH 1 JIF 5 POP PUSH 0 JIF 4 HALT - testConditionalJump - RESPONSE = 10", "PUSH 42 STORE 0 HALT - testStoreVariable - RESPONSE = 42", "PUSH 42 STORE 0 LOAD 0 HALT - testStoreAndLoadVariable"})
        Me.ComboBoxSyntaxHelp.Location = New System.Drawing.Point(3, 21)
        Me.ComboBoxSyntaxHelp.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ComboBoxSyntaxHelp.Name = "ComboBoxSyntaxHelp"
        Me.ComboBoxSyntaxHelp.Size = New System.Drawing.Size(589, 27)
        Me.ComboBoxSyntaxHelp.TabIndex = 0
        Me.ComboBoxSyntaxHelp.Tag = "Some Tests"
        '
        'FormDevIDE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1540, 826)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MinimumSize = New System.Drawing.Size(1533, 820)
        Me.Name = "FormDevIDE"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Development IDE"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents TextBoxErrorOutput As TextBox
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents AST As TreeView
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents TextBoxEnterStatments As TextBox
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents RichTextBoxDisplayOutput As RichTextBox
    Friend WithEvents ButtonExecute As Button
    Friend WithEvents ButtonCompile As Button
    Friend WithEvents GroupBox8 As GroupBox
    Friend WithEvents ComboBoxSyntaxHelp As ComboBox
    Friend WithEvents RichTextBoxProgram As TextBox
    Friend WithEvents ButtonExecuteCpuCode As Button
    Friend WithEvents ButtonInsertCode As Button
End Class
