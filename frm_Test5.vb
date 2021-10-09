Imports Kompas6API5, Kompas6Constants
Imports DB
Imports System.Data.SQLite

Public Class frm_Test5
    Dim kompas As KompasObject
    Dim ActiveDoc2D As ksDocument2D
    Dim DocumParam As ksDocumentParam
    Dim StandartSheet As ksStandartSheet
    Dim SheetPar As ksSheetPar
    Dim str As String
    Dim iMathematic2D As Mathematic2D ' Интерфейс ksMathematic2D

    Dim doc2D As myDoc2D

    Private Sub frm_Test5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kompas = kompasApp()
        loadForm(ActiveDoc2D, "ШаблонЧертежа")

        iMathematic2D = kompas.GetMathematic2D()

        Dim s As String = "10,6707692307694;-103,491334342657"
        Dim ss() As String = s.Split(";")
        Dim d1 As String = ss(0)
        Dim d2 As String = ss(1)

        Dim dd1 As Double = Convert.ToDouble(d1)
        Dim dd2 As Double = Convert.ToDouble(d2)

        Debug.Print(dd1)
        Debug.Print(dd2)


        TextBox1.Text = s

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        doc2D = New myDoc2D(ActiveDoc2D)

        Dim A As PointDouble = New PointDouble("A", 30, 30)
        Dim B As PointDouble = New PointDouble("B", 40, 70)
        Dim C As PointDouble = New PointDouble("C", 70, 40)

        Dim P As PointDouble = New PointDouble("P", 80, 0)
        Dim angle As Double = 70


        Dim v1 As New cls_Test_SheetView("test", 100, 200)

        doc2D.AddView(v1)

        v1.Create()

        v1.Active()



        doc2D.myDrawPoint(A)
        doc2D.myDrawPoint(B)
        doc2D.myDrawPoint(C)

        doc2D.myDrawLineSeg(A, B)
        doc2D.myDrawLineSeg(B, C)
        doc2D.myDrawLineSeg(C, A)


        doc2D.myDrawLine(P, angle)








    End Sub





    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        doc2D = New myDoc2D(ActiveDoc2D)

        Dim T1 As PointDouble = New PointDouble("T1", 0, 0)
        'Dim T2 As PointDouble = New PointDouble("T2", 0, 100)
        Dim lenght As Double = 105
        Dim T2 As PointDouble = doc2D.myCalculateCordinatePoint(T1, lenght, 240)
        'Dim T2 As PointDouble = New PointDouble("T2", -45, -80)
        Dim B As PointDouble = New PointDouble("B", 20, -45)
        Dim BC As Double = 65

        'Dim P As PointDouble = New PointDouble("P", 80, 0)
        Dim angle As Double = doc2D.myCalculateAngleTwoPoint(T1, T2)


        Dim v1 As New cls_Test_SheetView("test", 100, 200)

        doc2D.AddView(v1)

        v1.Create()

        v1.Active()



        doc2D.myDrawPoint(T1)
        doc2D.myDrawPoint(T2)
        doc2D.myDrawPoint(B)

        doc2D.myDrawLineSeg(T1, T2)

        '90 - 0 180
        '65
        '215

        Dim C0 As PointDouble = IntersectLineSegArc(T1, T2, B, BC)
        doc2D.myDrawPoint(C0)

        doc2D.myDrawLineSeg(B, C0)

    End Sub
#Region " "



    ''' <summary>
    ''' Вычисление координаты точки на пересечении дуги заданной начальной точкой и прямой заданной двумя точками"
    ''' </summary>
    ''' <param name="T1"></param>
    ''' <param name="T2"></param>
    ''' <param name="B"></param>
    ''' <param name="radius"></param>
    ''' <returns></returns>
    Public Function IntersectLineSegArc(T1 As PointDouble, T2 As PointDouble, B As PointDouble, radius As Double) As PointDouble
        Dim iDynamicArray As Kompas6API5.DynamicArray ' Интерфейс ksDynamicArray
        ' Создать интерфейс динамического массива математических точек
        iDynamicArray = kompas.GetDynamicArray(POINT_ARR)

        Dim a1 As Double
        Dim a2 As Double
        If Not iDynamicArray Is Nothing Then ' Массив создан
            Dim line As Integer = ActiveDoc2D.ksLineSeg(T1.X, T1.Y, T2.X, T2.Y, 1) ' Отрисовка отрезка
            'ActiveDoc2D.ksArcByPoint(50, 40, 20, 30, 40, 70, 40, 1, 1) ' Отрисовка дуги по центру и конечным точкам
            'Dim arc As Integer = ActiveDoc2D.ksArcByPoint(B.X, B.Y, radius, 30, 40, 70, 40, 1, 1) ' Отрисовка дуги по центру и 

            'a1 = iMathematic2D.ksAngle(50, 40, 30, 40) ' Начальный угол дуги
            'a2 = iMathematic2D.ksAngle(50, 40, 70, 40) ' Конечный угол дуги

            ' Получить координаты точек пересечения отрезка и дуги
            ' Первая точка отрезка (0, 40), Вторая точка отрезка (100, 40),
            ' Центр дуги (50, 40), Радиус дуги 20
            'iMathematic2D.ksIntersectLinSArc(0, 40, 100, 40, 50, 40, 20, a1, a2, 1, iDynamicArray)

            Dim angle As Double = doc2D.myCalculateAngleTwoPoint(T1, T2)

            Dim angle1 As Double = angle - 90
            Dim angle2 As Double = angle + 90

            'If (angle >= 90 And angle < 275) Then
            '    angle1 = angle - 90
            '    angle1 = angle + 90
            '    'ElseIf (angle < 90 And angle >= 275) Then
            '    '    angle1 = 360 - angle
            '    '    angle2 = angle + 90
            'End If


            iMathematic2D.ksIntersectLinSArc(T1.X, T1.Y, T2.X, T2.Y, B.X, B.Y, radius, angle1, angle2, 1, iDynamicArray)

            Return DrawPointByArray(iDynamicArray) ' Отрисовка точек пересечения

            'Return iDynamicArray.ksDeleteArray() ' Удаление массива

            'Return New PointDouble(

        End If
    End Function

    Public Function DrawPointByArray(ByRef iDynamicArray As Object) As PointDouble
        Dim i As Object
        Dim iMathPointParam As Kompas6API5.MathPointParam ' Интерфейс ksMathPointParam
        If Not iDynamicArray Is Nothing Then
            ' Создать интерфейс параметров математической точки
            iMathPointParam = kompas.GetParamStruct(Kompas6Constants.StructType2DEnum.ko_MathPointParam)

            If Not iMathPointParam Is Nothing Then ' Интерфейс создан

                For i = 0 To iDynamicArray.ksGetArrayCount - 1 ' Выдать параметры точек в присланном массиве
                    iDynamicArray.ksGetArrayItem(i, iMathPointParam) ' Параметры текущей точки
                    ' Нарисовать точку в документе
                    'ActiveDoc2D.ksPoint(iMathPointParam.x, iMathPointParam.y, 5)
                    ' Выдать сообщение с координатами нарисованной точки
                    'kompas.ksMessage("x = " & iMathPointParam.x & " y = " & iMathPointParam.y)

                    Return New PointDouble(iMathPointParam.x, iMathPointParam.y)
                Next  ' Следующая точка
            End If
        End If
    End Function

#End Region
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim dt As DataTable
        Dim da As SQLiteDataAdapter
        DataBase.LoadDataAdapterAndDataTable(TableName.z3_acceleration, da, dt)

        Dim rows() As DataRow = dt.Select("id_z3 = 21")

        For Each row As DataRow In rows
            row.Delete()
        Next

        da.Update(dt)


    End Sub
End Class