﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Test2
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
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

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(260, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Подготовка"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(12, 58)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(260, 23)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Рисование 4-линий вокруг рамки"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(12, 103)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(260, 23)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "Создание нового вида"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(12, 132)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(260, 23)
        Me.Button4.TabIndex = 3
        Me.Button4.Text = "Button4"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(12, 170)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(260, 23)
        Me.Button5.TabIndex = 4
        Me.Button5.Text = "Button5"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(12, 243)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(260, 40)
        Me.Button6.TabIndex = 5
        Me.Button6.Text = "Создание временной системы координат"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(12, 302)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(260, 40)
        Me.Button7.TabIndex = 6
        Me.Button7.Text = "Найти пересечение двух окружностей"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(12, 361)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(260, 40)
        Me.Button8.TabIndex = 7
        Me.Button8.Text = "Пример пересечение двух окружностей"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.Location = New System.Drawing.Point(401, 12)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(260, 40)
        Me.Button9.TabIndex = 8
        Me.Button9.Text = "Пример пересечение двух окружностей"
        Me.Button9.UseVisualStyleBackColor = True
        '
        'frm_Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(777, 435)
        Me.Controls.Add(Me.Button9)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Name = "frm_Main"
        Me.Text = "frm_Main"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button9 As System.Windows.Forms.Button
End Class
