' Примеры разработанны по книге 
' Норсеев С.А. - Разработка приложений под КОМПАС в Delphi - 2013.pdf
' Место размещение ACER - D:\Multimedia\Книги\Kompas 3D_Збірник удосконалення\


Imports Kompas6API5 ', 'Kompas6Constants, KGAXLib

Public Class Form1
    Dim kompas As Object
    Dim Document2D As ksDocument2D
    Dim DocumParam As ksDocumentParam
    Dim StandartSheet As ksStandartSheet
    Dim SheetPar As ksSheetPar
    Dim str As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Create()
        Dim s() As String = {"Тонкая", "Основная", "Осевая"}
        ComboBox1.Items.Clear()
        For i As Integer = 0 To 2
            ComboBox1.Items.Add(s(i))
        Next

        ComboBox1.SelectedIndex = 1

    End Sub

    'Dim kompas = CreateObject("KOMPAS.Application.5") ' Подключаемся к КОМПАСу

    Private Sub btn_CreateCompas_1(sender As Object, e As EventArgs) Handles btn_CreateCompas.Click
        Module1.Create()
    End Sub


    Private Sub btn_Ввод_данных1(sender As Object, e As EventArgs) Handles btn_Ввод_данных.Click
        'Document2D.ksCircle(10, 10, 10, 0)          'Построение окружности
        Document2D.ksLineSeg(150, 100, 100, 40, 1)     'Построении линии
        kompas.ksMessage("нарисовали")
    End Sub

    '--------------Закрытие программы------------------
    Protected Overrides Sub OnClosing(ByVal e As System.ComponentModel.CancelEventArgs)
        Dim Messege1 As Integer
        Messege1 = MsgBox(" Документ не будет сохранен! " + vbNewLine + _
        " Желаете выйти из программы?           ", MsgBoxStyle.YesNo, "Разработка")
        If Messege1 = vbYes Then
            'Document2D.ksCloseDocument()
            End
        Else
            e.Cancel = True
        End If
    End Sub

    ' Не работает
    Private Sub btn_Stamp_1(sender As Object, e As EventArgs) Handles btn_Stamp.Click
        Dim TextItemParam As ksTextItemParam
        Dim Stamp As ksStamp

        'kompas.GetParamStruct

        TextItemParam = kompas.GetParamStruct(31)   ' парметр GetParamStruct(31) выбирается из SDK
        Stamp = Document2D.GetStamp()
        Stamp.ksOpenStamp()

        '==========================================
        ' Заполнение основных надписей чертежа
        'For i As Integer = 1 To 200
        '    Stamp.ksColumnNumber(i)
        '    TextItemParam.s = i
        '    Stamp.ksTextLine(TextItemParam)
        'Next
        '==========================================

        Stamp.ksColumnNumber(1)
        TextItemParam.s = "Деталь"
        Stamp.ksTextLine(TextItemParam)

        Stamp.ksColumnNumber(110)
        TextItemParam.s = "Мельник В.В."
        Stamp.ksTextLine(TextItemParam)

        Stamp.ksCloseStamp()


    End Sub

    '========================================================================================== 
    ' ---------------------ГЛАВА 6 - СИСТЕМЫ КООРДИНАТ -----------------------------------
    ' Кнопка 1
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles btn_XY.Click
        Dim x, y As Double
        'Document2D.ksSheetToView(50, 100, x, y)
        Document2D.ksViewToSheet(x, y, 50, 100)
    End Sub

    ' Кнопка 2
    Private Sub Новый_вид_2(sender As Object, e As EventArgs) Handles Button1.Click
        Dim ViewParam As ksViewParam
        Dim ViewNumber As Integer

        ViewParam = kompas.GetParamStruct(8)        ' парметр GetParamStruct(8) выбирается из SDK - StructType2D -> ko_ViewParam -> ksViewParam, который относится к "Видам чертежа"

        ViewParam.name = "Главный вид"
        ViewParam.x = 100
        ViewParam.y = 190
        ViewNumber = 0

        Document2D.ksCreateSheetView(ViewParam, ViewNumber)
    End Sub

    '--------------------Выбор вида листа------------------------
    Dim bool As Boolean = True
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Select Case bool
            Case True
                Document2D.ksOpenView(1)
                bool = False
            Case Else
                Document2D.ksOpenView(0)
                bool = True
        End Select

        'Document2D.ksOpenView(1)
    End Sub
    '==========================================================================================

    '========================================================================================== 
    ' ---------------------ГЛАВА 7 - ТЕКСТОВАЯ НАДПИСЬ  -----------------------------------

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Document2D.ksText(100, 100, 0, 5, 0, 0, "Выводимый текст")


    End Sub
    '========================================================================================== 

    ' ---------------------Получить координаты точки пересечения двух прямых  ----------------
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim Mathematic2D As ksMathematic2D
        'Dim x, y As Double
        'Dim k As Integer
        Dim Ap As ksMathPointParam
        'Dim Ap As Double
        Document2D.ksLine(0, 150, 0.0)
        Document2D.ksLine(0.0, 0.0, 45)

        Ap = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam) 'StructType2DEnum.ko_MathPointParam)

        Mathematic2D = kompas.GetMathematic2D()
        'MessageBox.Show("Продолжить!")
        Mathematic2D.ksIntersectLinLin(0, 150, 0, 0, 0, 45, Ap) '    k, x, y)
        'MessageBox.Show("Продолжить!")
        Debug.WriteLine("" & Ap.x & " " & Ap.y)
        'MessageBox.Show("Продолжить!")
        Document2D.ksPoint(Ap.x, Ap.y, 0)

    End Sub

    '===================================================================================




    '===================================================================================




    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Form2.Show()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If TextBox1.Text = "" And TextBox2.Text = "" And TextBox3.Text = "" And TextBox4.Text = "" Then
            MsgBox("Введите значения в текстовые поля!" & vbNewLine & "Пустые поля не допускаются!!!")
        Else

            Document2D.ksLineSeg(TextBox1.Text, TextBox2.Text,
                                 TextBox3.Text, TextBox4.Text,
                                 ComboBox1.SelectedIndex)     'Построении линии
        End If
    End Sub
End Class


