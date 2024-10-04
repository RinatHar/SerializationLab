<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PointForm
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
        Me.BtnCreate = New System.Windows.Forms.Button()
        Me.ListBox = New System.Windows.Forms.ListBox()
        Me.BtnSort = New System.Windows.Forms.Button()
        Me.BtnSerialize = New System.Windows.Forms.Button()
        Me.BtnDeserialize = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtnCreate
        '
        Me.BtnCreate.Location = New System.Drawing.Point(36, 376)
        Me.BtnCreate.Name = "BtnCreate"
        Me.BtnCreate.Size = New System.Drawing.Size(110, 68)
        Me.BtnCreate.TabIndex = 0
        Me.BtnCreate.Text = "Create"
        Me.BtnCreate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnCreate.UseVisualStyleBackColor = True
        '
        'ListBox
        '
        Me.ListBox.FormattingEnabled = True
        Me.ListBox.Location = New System.Drawing.Point(36, 23)
        Me.ListBox.Name = "ListBox"
        Me.ListBox.Size = New System.Drawing.Size(458, 316)
        Me.ListBox.TabIndex = 5
        '
        'BtnSort
        '
        Me.BtnSort.Location = New System.Drawing.Point(152, 376)
        Me.BtnSort.Name = "BtnSort"
        Me.BtnSort.Size = New System.Drawing.Size(110, 68)
        Me.BtnSort.TabIndex = 6
        Me.BtnSort.Text = "Sort"
        Me.BtnSort.UseVisualStyleBackColor = True
        '
        'BtnSerialize
        '
        Me.BtnSerialize.Location = New System.Drawing.Point(268, 376)
        Me.BtnSerialize.Name = "BtnSerialize"
        Me.BtnSerialize.Size = New System.Drawing.Size(110, 68)
        Me.BtnSerialize.TabIndex = 7
        Me.BtnSerialize.Text = "Serialize"
        Me.BtnSerialize.UseVisualStyleBackColor = True
        '
        'BtnDeserialize
        '
        Me.BtnDeserialize.Location = New System.Drawing.Point(384, 376)
        Me.BtnDeserialize.Name = "BtnDeserialize"
        Me.BtnDeserialize.Size = New System.Drawing.Size(110, 68)
        Me.BtnDeserialize.TabIndex = 8
        Me.BtnDeserialize.Text = "Deserialize"
        Me.BtnDeserialize.UseVisualStyleBackColor = True
        '
        'PointForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(509, 480)
        Me.Controls.Add(Me.BtnDeserialize)
        Me.Controls.Add(Me.BtnSerialize)
        Me.Controls.Add(Me.BtnSort)
        Me.Controls.Add(Me.ListBox)
        Me.Controls.Add(Me.BtnCreate)
        Me.Name = "PointForm"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ListBox As ListBox
    Friend WithEvents BtnSort As Button
    Friend WithEvents BtnSerialize As Button
    Friend WithEvents BtnDeserialize As Button
    Friend WithEvents BtnCreate As Button
End Class
