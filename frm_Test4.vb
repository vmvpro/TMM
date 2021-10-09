Imports Kompas6API5, Kompas6Constants

Public Class frm_Test4
    Dim kompas As KompasObject
    Dim ActiveDoc2D As ksDocument2D
    Dim DocumParam As ksDocumentParam
    Dim StandartSheet As ksStandartSheet
    Dim SheetPar As ksSheetPar
    Dim str As String

    Dim doc2D As myDoc2D

    Private Sub frm_Test4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kompas = kompasApp()
        loadForm(ActiveDoc2D, "ШаблонЧертежа")
    End Sub
	Dim v1 As New cls_SheetView("v1", 100, 200)

	Dim A As New PointDouble("A", 40, 10)
	Dim B As New PointDouble("B", 80, 50)
	Dim C As New PointDouble("C", 20, 60)

	Dim listPoint As New List(Of Integer)
	Dim listLineSeg As New List(Of Integer)

	Dim refObjectSelect As Integer
	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        test_1()

        test0()
        test1()
        test2()

		'WalkGroup(ActiveDoc2D)
        'test4()

		'PolyLineArray()

		'DrawNurbs(ActiveDoc2D)
        test5_Point()
        test5_LineSeg()

        'test7()

	End Sub

	Sub test7()
		doc2D = New myDoc2D(ActiveDoc2D)

		Dim v5 As New cls_Test_SheetView("test", 100, 200)

		doc2D.AddView(v5)
		v5.Create()

		v5.X = 50
		v5.Y = 10

		v5.Active()

		Dim pb As New cls_Segment(doc2D)

		pb.T1 = A
		pb.T1 = B

		v5.Pb = pb

		doc2D.myDrawLineSeg(v5.Pb)

		Debug.WriteLine(v5.Pb.Lenght)

	End Sub

    Dim P As PointDouble
    Sub test_1()
        Dim pRegInfo As ksRequestInfo = kompas.GetParamStruct(Kompas6Constants.StructType2DEnum.ko_RequestInfo)
        pRegInfo.commandsString = "Укажите объект"

        P = New PointDouble()

        ActiveDoc2D.ksCursor(pRegInfo, P.X, P.Y, Nothing)

        Debug.Print(P.ToString)

    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged

        Dim par As Kompas6API5.LineSegParam ' Интерфейс ksLineSegParam

        par = kompas.GetParamStruct(Kompas6Constants.StructType2DEnum.ko_LineSegParam)


        Dim t As Short



        ActiveDoc2D.ksLightObj(refObjectSelect, 0)

        refObjectSelect = listLineSeg(ListBox2.SelectedIndex)

        t = ActiveDoc2D.ksGetObjParam(refObjectSelect, par, ALLPARAM) ' Получить параметры отрезка

        lbl_p1.Text = par.x1 & "; " & par.y1
        lbl_p2.Text = par.x2 & "; " & par.y2
        lbl_angle.Text = doc2D.myCalculateAngleTwoPoint(New PointDouble(par.x1, par.y1), New PointDouble(par.x2, par.y2))


        ActiveDoc2D.ksLightObj(refObjectSelect, 1)
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ActiveDoc2D.ksLightObj(refObjectSelect, 0) ' подсветим группу
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ActiveDoc2D.ksLightObj(refObjectSelect, 0) ' подсветим группу
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

        refObjectSelect = listPoint(ListBox1.SelectedIndex)

        ActiveDoc2D.ksLightObj(refObjectSelect, 1) ' подсветим группу

    End Sub

    Sub test5_LineSeg()

        Dim iIterPoint As ksIterator = kompas.GetIterator
        iIterPoint.ksCreateIterator(LINESEG_OBJ, v1.Ref)

        Dim count As Short = 1
        Dim refObject As Integer

        If iIterPoint.reference Then
            refObject = iIterPoint.ksMoveIterator("F")
            If refObject Then
                Do
                    ListBox2.Items.Add("Линия " & count)
                    listLineSeg.Add(refObject)
                    'ActiveDoc2D.ksLightObj(refObject, 1) ' подсветим группу
                    count = count + 1

                    'ActiveDoc2D.ksLightObj(refObject, 0) ' снимем подсветку
                    refObject = iIterPoint.ksMoveIterator("N")

                Loop Until refObject = 0
                count = 0
            End If

        End If

    End Sub

    Sub test5_Point()

        Dim iIterPoint As ksIterator = kompas.GetIterator
        iIterPoint.ksCreateIterator(POINT_OBJ, v1.Ref)

        Dim count As Short = 1
        Dim refObject As Integer

        If iIterPoint.reference Then
            refObject = iIterPoint.ksMoveIterator("F")
            If refObject Then
                Do
                    ListBox1.Items.Add("точка " & count)
                    listPoint.Add(refObject)
                    'ActiveDoc2D.ksLightObj(refObject, 1) ' подсветим группу
                    count = count + 1

                    'ActiveDoc2D.ksLightObj(refObject, 0) ' снимем подсветку
                    refObject = iIterPoint.ksMoveIterator("N")

                Loop Until refObject = 0
                count = 0
            End If

        End If

    End Sub

    Sub test5()
        Dim iIter As ksIterator = kompas.GetIterator
        iIter.ksCreateIterator(ALL_OBJ, v1.Ref)

        Dim iIterPoint As ksIterator = kompas.GetIterator
        iIterPoint.ksCreateIterator(POINT_OBJ, v1.Ref)

        Dim iIterLineSeg As ksIterator = kompas.GetIterator
        iIterLineSeg.ksCreateIterator(LINESEG_OBJ, v1.Ref)

        Dim count As Short = 0


        Dim refObject As Integer


        If iIter.reference Then
            refObject = iIter.ksMoveIterator("F")
            If refObject Then
                Do
                    ActiveDoc2D.ksLightObj(refObject, 1) ' подсветим группу
                    count = count + 1

                    ActiveDoc2D.ksLightObj(refObject, 0) ' снимем подсветку
                    refObject = iIter.ksMoveIterator("N")
                Loop Until refObject = 0
            End If

        End If

    End Sub

    Sub DrawNurbs(ByRef doc As Kompas6API5.Document2D)
        Dim par As Kompas6API5.NurbsPointParam ' Интерфейс ksNurbsPointParam
        par = Kompas.GetParamStruct(Kompas6Constants.StructType2DEnum.ko_NurbsPointParam) ' Интерфейс параметров точки кривой NURBS

        Dim p As Integer
        If Not par Is Nothing Then ' Интерфейс получен
            par.Init() ' Инициализация

            ' Построить Nurbs сплайн  как составной объект
            doc.ksNurbs(3, 0, 1)
            ' Точки входящие в сплайн
            par.x = 0 ' Координаты 1-ой точки
            par.y = 0
            par.weight = 1 ' Вес 1-ой точки (должен быть больше нуля)
            doc.ksNurbsPoint(par) ' Добавить точку в сплайн
            par.x = 20 ' Координаты 2-ой точки
            par.y = 20
            par.weight = 1 ' Вес 2-ой точки (должен быть больше нуля)
            doc.ksNurbsPoint(par) ' Добавить точку в сплайн
            par.x = 50 ' Координаты 3-ой точки
            par.y = 10
            par.weight = 1 ' Вес 3-ой точки (должен быть больше нуля)
            doc.ksNurbsPoint(par) ' Добавить точку в сплайн
            par.x = 70 ' Координаты 4-ой точки
            par.y = 20
            par.weight = 1 ' Вес 4-ой точки (должен быть больше нуля)
            doc.ksNurbsPoint(par) ' Добавить точку в сплайн
            par.x = 100 ' Координаты 5-ой точки
            par.y = 0
            par.weight = 1 ' Вес 5-ой точки (должен быть больше нуля)
            doc.ksNurbsPoint(par) ' Добавить точку в сплайн
            par.x = 50 ' Координаты 6-ой точки
            par.y = -50
            par.weight = 1 ' Вес 6-ой точки (должен быть больше нуля)
            doc.ksNurbsPoint(par) ' Добавить точку в сплайн
            p = doc.ksEndObj ' Функция EndObj возвращает указатель на созданный объект сплайн

            doc.ksLightObj(p, 1) ' Подсветить сплайн
            Kompas.ksMessage("NURBS")
            doc.ksLightObj(p, 0) ' Снять выделение сплайна

            par = Nothing
        End If
    End Sub

    ' Массив полилиний это массив массивов математических точек

    Sub PolyLineArray()


        Dim pPoly As Kompas6API5.DynamicArray ' Интерфейс ksDynamicArray
        ' Массив полилиний - массивы математических точек
        pPoly = kompas.GetDynamicArray(POLYLINE_ARR)

        Dim par As Kompas6API5.MathPointParam ' Интерфейс ksMathPointParam
        ' Структура параметров математической точки
        par = kompas.GetParamStruct(Kompas6Constants.StructType2DEnum.ko_MathPointParam)

        Dim arr As Kompas6API5.DynamicArray ' Интерфейс ksDynamicArray
        ' Массив для хранения математических точек
        arr = kompas.GetDynamicArray(POINT_ARR)

        ' Интерфейсы созданы
        Dim count As Short
        Dim i As Short
        Dim count1 As Short
        Dim j As Short
        Dim arr2 As Kompas6API5.DynamicArray ' Интерфейс ksDynamicArray

        If (Not pPoly Is Nothing) And (Not par Is Nothing) And (Not arr Is Nothing) Then
            par.x = 10 ' Наполнить массив математических точек
            par.y = 10
            arr.ksAddArrayItem(-1, par) ' Добавим 1-ю точку, элемент добавляется в конец массива

            par.x = 100
            par.y = 100
            arr.ksAddArrayItem(-1, par) ' Добавим 2-ю точку, элемент добавляется в конец массива

            par.x = 1000
            par.y = 1000
            arr.ksAddArrayItem(-1, par) ' Добавим 3-ю точку, элемент добавляется в конец массива

            ' Добавим 1-й массив математических точек в массив полилиний, элемент добавляется в конец массива
            pPoly.ksAddArrayItem(-1, arr)

            ' Очистили массив математических точек, чтобы использовать для создания 2-й полилинии
            arr.ksClearArray()

            par.x = 20 ' Наполнить массив математических точек
            par.y = 20
            arr.ksAddArrayItem(-1, par) ' Добавим 1-ю точку, элемент добавляется в конец массива

            par.x = 200
            par.y = 200
            arr.ksAddArrayItem(-1, par) ' Добавим 2-ю точку, элемент добавляется в конец массива

            par.x = 2000
            par.y = 2000
            arr.ksAddArrayItem(-1, par) ' Добавим 3-ю точку, элемент добавляется в конец массива

            ' Добавим 2-й массив математических точек в массив полилиний, элемент добавляется в конец массива
            pPoly.ksAddArrayItem(-1, arr)

            ' Очистили массив математических точек, чтобы использовать для создания 3-й полилинии
            arr.ksClearArray()

            par.x = 30 ' Наполнить массив математических точек
            par.y = 30
            arr.ksAddArrayItem(-1, par) ' Добавим 1-ю точку, элемент добавляется в конец массива

            par.x = 300
            par.y = 300
            arr.ksAddArrayItem(-1, par) ' Добавим 2-ю точку, элемент добавляется в конец массива

            par.x = 3000
            par.y = 3000
            arr.ksAddArrayItem(-1, par) ' Добавим 3-ю точку, элемент добавляется в конец массива

            ' Добавим 3-й массив математических точек в массив полилиний, элемент добавляется в конец массива
            pPoly.ksAddArrayItem(-1, arr)

            count = pPoly.ksGetArrayCount() ' Количество элементов в массиве полилиний
            'Kompas.ksMessage("count = " & count) ' Вывести количество элементов в массиве полилиний

            ' Просмотрим массив полилиний
            ' Цикл по полилиниям - массивы математических точек
            For i = 0 To count - 1
                ' Получить значение элемента массива
                pPoly.ksGetArrayItem(i, arr)

                ' Количество элементов в массиве математических точек
                count1 = arr.ksGetArrayCount

                ' Цикл по полилинии - массиву математических точек
                For j = 0 To count1 - 1
                    ' Получить значение элемента массива
                    arr.ksGetArrayItem(j, par)
                    'Kompas.ksMessage("i = " & i & " j = " & j & " x = " & par.x & " y = " & par.y)
                Next j
            Next i

            ' Заменим у второго элемента массива полилиний( массива математических точек ) первый и второй элемент
            par.x = 50
            par.y = 50
            arr.ksSetArrayItem(1, par) ' Второй элемент

            par.x = 500
            par.y = 500
            arr.ksSetArrayItem(0, par) ' Первый элемент

            ' Установить значение элемента динамического массива математических точек
            pPoly.ksSetArrayItem(1, arr)

            count = pPoly.ksGetArrayCount() ' Количество элементов в массиве полилиний

            ' создадим массив точек
            ' Массив для хранения математических точек
            arr2 = kompas.GetDynamicArray(POINT_ARR)

            If Not arr2 Is Nothing Then ' Интерфейс создан

                ' Просмотрим массив полилиний
                ' Цикл по полилиниям - массивы математических точек
                For i = 0 To count - 1
                    pPoly.ksGetArrayItem(i, arr2) ' Получить значение элемента массива

                    ' Цикл по полилинии - массиву математических точек
                    For j = 0 To arr2.ksGetArrayCount() - 1 ' Количество элементов в массиве математических точек
                        arr2.ksGetArrayItem(j, par) ' Получить значение элемента массива
                        'Kompas.ksMessage("j = " & j & " x = " & par.x & " y = " & par.y)
                    Next j
                Next i

                arr2.ksDeleteArray() ' Массив для хранения математических точек
            End If

            kompas.ksMessageBoxResult() ' Результат выполнения

            ' Удалить динамические массивы
            arr.ksDeleteArray() ' Массив для хранения математических точек
            pPoly.ksDeleteArray() ' Массив полилиний - массивы математических точек
        End If
    End Sub
    Sub test4()
        Dim iIter As ksIterator = kompas.GetIterator

        Dim iIterPoint As ksIterator = kompas.GetIterator

        Dim iIterLineSeg As ksIterator = kompas.GetIterator

        Dim count As Short = 0

        iIter.ksCreateIterator(ALL_OBJ, v1.Ref)
        Dim refObject As Integer


        If iIter.reference Then
            refObject = iIter.ksMoveIterator("F")
            If refObject Then
                Do
                    ActiveDoc2D.ksLightObj(refObject, 1) ' подсветим группу
                    count = count + 1

                    ActiveDoc2D.ksLightObj(refObject, 0) ' снимем подсветку
                    refObject = iIter.ksMoveIterator("N")
                Loop Until refObject = 0
            End If

        End If

    End Sub


    Sub WalkGroup(ByRef doc As Object) ' хождение по именнованным и рабочим группам
        Dim pNameGrp As Integer
        Dim count As Short
        count = 0
        Dim iIter As Object ' ksIterator
        iIter = kompas.GetIterator

        iIter.ksCreateIterator(NAME_GROUP_OBJ, 0) ' создадим итератор для движения по именнованным группам в документе
        If iIter.reference Then
            pNameGrp = iIter.ksMoveIterator("F")
            If pNameGrp Then
                Do
                    doc.ksLightObj(pNameGrp, 1) ' подсветим группу
                    count = count + 1
                    kompas.ksMessage("номер = " & count)
                    doc.ksLightObj(pNameGrp, 0) ' снимем подсветку
                    pNameGrp = iIter.ksMoveIterator("N")
                Loop Until pNameGrp = 0
            End If
        End If
        iIter.ksDeleteIterator()
        ' все именнованные группы ложатся в массив рабочих групп
        doc.ksNewGroup(0)
        doc.ksCircle(30, 30, 20, 1)
        doc.ksCircle(30, 30, 10, 1)
        doc.ksHatch(0, 45, 2, 0, 0, 0)
        doc.ksCircle(30, 30, 20, 1)
        doc.ksCircle(30, 30, 10, 1)
        doc.ksEndObj()
        doc.ksEndGroup()

        ' создать итератор по рабочим группам
        count = 0
        Dim pWorkGrp As Integer
        iIter.ksCreateIterator(WORK_GROUP_OBJ, 0) ' создадим итератор для движения по именнованным группам в документе
        If iIter.reference Then
            pWorkGrp = iIter.ksMoveIterator("F")
            If pWorkGrp Then
                Do
                    doc.ksLightObj(pWorkGrp, 1) ' подсветим группу
                    count = count + 1
                    kompas.ksMessage("номер = " & count)
                    doc.ksLightObj(pWorkGrp, 0) ' снимем подсветку
                    pWorkGrp = iIter.ksMoveIterator("N")
                Loop Until pWorkGrp = 0
            End If
        End If
    End Sub
    Private Sub test3()
        

    End Sub

    Private Sub test2()
        v1.Active()

        doc2D.myDrawLineSeg(A, C)

        doc2D.myDrawPoint(A)
        doc2D.myDrawPoint(B)
        doc2D.myDrawPoint(C)

        'v1.ExitView()

    End Sub

    Sub test1()

        Dim angleAB As Double = doc2D.myCalculateAngleTwoPoint(A, B)

        doc2D.myDrawLineSeg(A, B)

        doc2D.myDrawLineSeg(B, C)

        v1.ExitView()

    End Sub

    Sub test0()
        doc2D = New myDoc2D(ActiveDoc2D)

        doc2D.AddView(v1)
        v1.Create()

        v1.X = P.X
        v1.Y = P.Y

        v1.Active()
    End Sub

    
    
    
    
    
End Class