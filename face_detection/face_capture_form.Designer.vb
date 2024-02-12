<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.browse_img = New System.Windows.Forms.Button()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.load_data = New System.Windows.Forms.Button()
        Me.PB_Gallery1 = New System.Windows.Forms.PictureBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.ComboBoxcameralist = New System.Windows.Forms.ComboBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.ComboBox14 = New System.Windows.Forms.ComboBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PB_Gallery1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.BackColor = System.Drawing.Color.Gainsboro
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Location = New System.Drawing.Point(176, 55)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(366, 295)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'browse_img
        '
        Me.browse_img.Location = New System.Drawing.Point(176, 18)
        Me.browse_img.Name = "browse_img"
        Me.browse_img.Size = New System.Drawing.Size(148, 33)
        Me.browse_img.TabIndex = 2
        Me.browse_img.Text = "Browse"
        Me.browse_img.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox2.Location = New System.Drawing.Point(309, 354)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(91, 88)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 6
        Me.PictureBox2.TabStop = False
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2})
        Me.DataGridView1.Location = New System.Drawing.Point(559, 42)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.Size = New System.Drawing.Size(359, 396)
        Me.DataGridView1.TabIndex = 14
        '
        'Column1
        '
        Me.Column1.HeaderText = "MyID"
        Me.Column1.Name = "Column1"
        '
        'Column2
        '
        Me.Column2.HeaderText = "Image"
        Me.Column2.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
        Me.Column2.Name = "Column2"
        '
        'load_data
        '
        Me.load_data.Location = New System.Drawing.Point(559, 4)
        Me.load_data.Name = "load_data"
        Me.load_data.Size = New System.Drawing.Size(153, 34)
        Me.load_data.TabIndex = 15
        Me.load_data.Text = "Load Data from DB"
        Me.load_data.UseVisualStyleBackColor = True
        '
        'PB_Gallery1
        '
        Me.PB_Gallery1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PB_Gallery1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PB_Gallery1.Location = New System.Drawing.Point(924, 42)
        Me.PB_Gallery1.Name = "PB_Gallery1"
        Me.PB_Gallery1.Size = New System.Drawing.Size(189, 180)
        Me.PB_Gallery1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PB_Gallery1.TabIndex = 16
        Me.PB_Gallery1.TabStop = False
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(924, 405)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(189, 33)
        Me.Button4.TabIndex = 2
        Me.Button4.Text = "Find Match"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(6, 54)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(83, 13)
        Me.Label28.TabIndex = 29
        Me.Label28.Text = "Select Webcam"
        '
        'ComboBoxcameralist
        '
        Me.ComboBoxcameralist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxcameralist.FormattingEnabled = True
        Me.ComboBoxcameralist.Location = New System.Drawing.Point(9, 70)
        Me.ComboBoxcameralist.Name = "ComboBoxcameralist"
        Me.ComboBoxcameralist.Size = New System.Drawing.Size(158, 21)
        Me.ComboBoxcameralist.TabIndex = 28
        '
        'Button5
        '
        Me.Button5.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.Location = New System.Drawing.Point(9, 92)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(158, 27)
        Me.Button5.TabIndex = 27
        Me.Button5.Text = "Connect"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(9, 121)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(59, 13)
        Me.Label29.TabIndex = 31
        Me.Label29.Text = "Frame Size"
        '
        'ComboBox14
        '
        Me.ComboBox14.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox14.FormattingEnabled = True
        Me.ComboBox14.Location = New System.Drawing.Point(9, 137)
        Me.ComboBox14.Name = "ComboBox14"
        Me.ComboBox14.Size = New System.Drawing.Size(158, 21)
        Me.ComboBox14.TabIndex = 30
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1125, 450)
        Me.Controls.Add(Me.Label29)
        Me.Controls.Add(Me.ComboBox14)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.ComboBoxcameralist)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.PB_Gallery1)
        Me.Controls.Add(Me.load_data)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.browse_img)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PB_Gallery1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents browse_img As Button
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents load_data As System.Windows.Forms.Button
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents PB_Gallery1 As System.Windows.Forms.PictureBox
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxcameralist As System.Windows.Forms.ComboBox
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents ComboBox14 As System.Windows.Forms.ComboBox
End Class
