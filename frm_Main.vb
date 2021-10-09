Imports Kompas6API5, Kompas6Constants
Imports DB

Public Class frm_Main
    Dim kompas As KompasObject
    Dim ActiveDoc2D As ksDocument2D
    Dim DocumParam As ksDocumentParam
    Dim StandartSheet As ksStandartSheet
    Dim SheetPar As ksSheetPar
    Dim str As String

    Dim povzun_3 As cls_Povsun_3
    Dim povzun_6 As cls_Povsun_6
    Dim shatun_9 As cls_Shatun_9

    Dim cam_0000 As cls_Cam_0000
    Dim cam_K As cls_Cam_K

    Private Sub frm_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Dim ds As DataBase

        Dim db As New DataBase

        'Dim dt As DataTable = DataBase.DictionaryDataTables(TableName.z3_data)
        ''Dim row As DataRow = DataBase.Row(1, TableName.z3_data)

        'For i As Integer = 0 To dt.Columns.Count - 1
        ' Debug.Write(row(i).ToString() & Space(2))
        'Next
        '

        'For Each r As DataRow In dt.Rows
        ' Debug.Print(r(0).ToString())
        'Next



        'Return
        kompas = kompasApp()
        loadForm(ActiveDoc2D, "ШаблонЧертежа")

        'loadForm(ActiveDoc2D, "Cam_0000")

        'cam_0000 = New cls_Cam_0000(ActiveDoc2D)

        'cam_K = New cls_Cam_K(ActiveDoc2D)


        povzun_3 = New cls_Povsun_3(ActiveDoc2D, 1)

        'shatun_9 = New cls_Shatun_9(ActiveDoc2D)

        'povzun_6 = New cls_Povsun_6(ActiveDoc2D)

    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            Dim Doc2D As New myDoc2D(ActiveDoc2D)

            'shatun_9.Run()

            povzun_3.Run()

            'povzun_6.Run()

            ' shatun.Run()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        'povzun.Mehanizm()
        'povzun.mehanizm_speed()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'povzun.acceleration()
        'povzun.AbsoluteWork()
    End Sub





    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        ActiveDoc2D.ksMtr(370, 235, 0, 1, 1)
        Dim gr As Integer
        Dim p As Integer

        'vp_P = New PointDouble(0, 0)
        'vp_b = ActiveDoc2D.myCalculateCordinatePoint(vp_P, 70, ActiveDoc2D.myCalculateAngleTwoPoint(mp_A, mp_B) + 90)

        'vp_c = ActiveDoc2D.myCalculateCordinatePoint(vp_b, ActiveDoc2D.myCalculateAngleTwoPoint(mp_B, mp_C) + 90,
        '                                              vp_P, ActiveDoc2D.myCalculateAngleTwoPoint(mp_D, mp_C) + 90)
        ' Создание вида для 3-положения
        gr = ActiveDoc2D.ksNewGroup(0)

        'ActiveDoc2D.myDrawVector(vp_P, vp_b)
        'ActiveDoc2D.myDrawVector(vp_b, vp_c)
        'ActiveDoc2D.myDrawVector(vp_P, vp_c)
        '
        ActiveDoc2D.ksEndGroup() ' Завершить создание группы объектов

        If ActiveDoc2D.ksSaveGroup(gr, "gr3") = 0 Then
            Exit Sub ' Если сохранить не удалось - выход из процедуры
        End If

        ActiveDoc2D.myMtrDelete()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        'Dim gr1 As Integer
        'gr1 = doc2D.ksGetGroup("gr3") ' Получить указатель на именованную группу

        'doc2D.ksMoveObj(gr1, -100, 0)	' Сдвинули первую группу на -100 ММ
        Dim doc2D As New myDoc2D(ActiveDoc2D)
        Dim v1 As cls_SheetView = New cls_SheetView("v1", 150, 150)

        doc2D.AddView(v1)
        v1.Create()
        v1.Active()
        ActiveDoc2D.myDrawPoint(New PointDouble(0, 0))
        v1.ExitView()

        ActiveDoc2D.ksMoveObj(v1.Ref, 50, 50)
        ActiveDoc2D.ksRotateObj(v1.Ref, 50, 50, 45)

        'doc2D.ksRotateObj()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        'If viewShetV3 = -1 Then
        ' viewShetV3 = ActiveDoc2D.mySheetViewCreate("v3", 370, 235)
        'End If


        'ActiveDoc2D.ksGetViewReference(viewShetV3)

        'Dim number As Integer = ActiveDoc2D.ksGetViewNumber(viewShetV3)



        'doc2D.ksOpenView(number)

        'Dim ViewParam As ksViewParam
        'ViewParam = kompas.GetParamStruct(StructType2DEnum.ko_ViewParam)


    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'ActiveDoc2D.ksOpenView(0)
        'ActiveDoc2D.ksDeleteObj(viewShetV3)


    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim doc2D As New myDoc2D(ActiveDoc2D)

        Dim A As PointDouble = New PointDouble("A", 22, 8)
        Dim B As PointDouble = New PointDouble("B", 8, 38)
        Dim C As PointDouble = New PointDouble("C", 44, 35)

        Dim sheetView1 As New cls_SheetView("v1", 100, 235, 30)
        Dim sheetView2 As New cls_SheetView("v2", 100, 235, , 2)
        Dim sheetView3 As New cls_SheetView("v3", 300, 235)

        'sheetView1.Create(doc2D)
        sheetView2.Create()
        sheetView3.Create()

        'sheetView1.Active()
        'doc2D.myLineSegDraw(A, B)
        'doc2D.myLineSegDraw(C, B)
        'doc2D.myLineSegDraw(A, C)

        sheetView2.Active()
        ActiveDoc2D.myDrawLineSeg(A, B)
        ActiveDoc2D.myDrawLineSeg(C, B)
        ActiveDoc2D.myDrawLineSeg(A, C)

        sheetView3.Active()
        ActiveDoc2D.myDrawLineSeg(A, B)
        ActiveDoc2D.myDrawLineSeg(C, B)
        ActiveDoc2D.myDrawLineSeg(A, C)


    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        'ActiveDoc2D.ksMtr(planX, planY, 0, 1, 1)

        Dim A As PointDouble = New PointDouble("A", 22, 8)
        Dim B As PointDouble = New PointDouble("B", 8, 38)

        ActiveDoc2D.myDrawPoint(A)
        ActiveDoc2D.myDrawPoint(B)
        ActiveDoc2D.myDrawLineSeg(A, B)


        Dim C As PointDouble = ActiveDoc2D.myCalculateCordinateCentreSeg(A, B)

        ActiveDoc2D.myDrawPoint(C)

        ActiveDoc2D.myMtrDelete()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        'ActiveDoc2D.ksMtr(planX, planY, 0, 1, 1)

        Dim A As PointDouble = New PointDouble("A", 90, 70)
        Dim B As PointDouble = New PointDouble("B", 30, 20)
        Dim C As PointDouble = New PointDouble("C", 160, 40)

        ActiveDoc2D.myDrawPoint(A)
        ActiveDoc2D.myDrawPoint(B)
        ActiveDoc2D.myDrawPoint(C)

        ActiveDoc2D.myDrawLineSeg(A, B)
        ActiveDoc2D.myDrawLineSeg(A, C)
        ActiveDoc2D.myDrawLineSeg(C, B)

        ActiveDoc2D.myDrawText(A, A.ToString())
        ActiveDoc2D.myDrawText(B, B.ToString())
        ActiveDoc2D.myDrawText(C, C.ToString())


        Dim angleSeg_BA As Double = ActiveDoc2D.myCalculateAngleTwoPoint(B, A)

        Dim angleSeg_AC As Double = ActiveDoc2D.myCalculateAngleTwoPoint(A, C)

        Debug.Print(angleSeg_BA)
        Debug.Print(angleSeg_AC)

        Dim tmpAngle1 = ActiveDoc2D.myCalculateAngleTwoSegment(B, A, C)
        Debug.Print(tmpAngle1)

        Dim tmpAngle2 = ActiveDoc2D.myCalculateAngleTwoSegment(A, B, C)
        Debug.Print(tmpAngle2)

        Dim tmpAngle3 = ActiveDoc2D.myCalculateAngleTwoSegment(A, C, B)
        Debug.Print(tmpAngle3)






        'Dim angleBAC As Double = doc


        'Dim C As PointDouble = doc2D.myCordinateCentreSeg(A, B)

        'doc2D.myPointDraw(C)

        ActiveDoc2D.myMtrDelete()
    End Sub


    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        'doc2D.ksMtr(planX, planY, 0, 1, 1)

        Dim A As PointDouble = New PointDouble("A", 90, 70)
        Dim B As PointDouble = New PointDouble("B", 30, 20)
        Dim C As PointDouble = New PointDouble("C", 160, 40)


        Dim angleSeg_BA As Double = ActiveDoc2D.myCalculateAngleTwoPoint(B, A)
        Debug.Print(angleSeg_BA)

        Dim angleSeg_AB As Double = ActiveDoc2D.myCalculateAngleTwoPoint(A, B)
        Debug.Print(angleSeg_AB)

        Dim angleSeg_AC As Double = ActiveDoc2D.myCalculateAngleTwoPoint(A, C)

        Debug.Print(angleSeg_BA)
        Debug.Print(angleSeg_AC)

        Dim tmpAngle1 = ActiveDoc2D.myCalculateAngleTwoSegment(B, A, C)
        Debug.Print(tmpAngle1)

        Dim tmpAngle2 = ActiveDoc2D.myCalculateAngleTwoSegment(A, B, C)
        Debug.Print(tmpAngle2)

        Dim tmpAngle3 = ActiveDoc2D.myCalculateAngleTwoSegment(A, C, B)
        Debug.Print(tmpAngle3)






        'Dim angleBAC As Double = doc


        'Dim C As PointDouble = doc2D.myCordinateCentreSeg(A, B)

        'doc2D.myPointDraw(C)

        ' doc2D.myMtrDelete()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click



    End Sub



    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click

        Dim par As Kompas6API5.ViewParam ' Интерфейс ksViewParam
        ' Структура параметров вида
        par = kompas.GetParamStruct(Kompas6Constants.StructType2DEnum.ko_ViewParam)

        Dim number As Integer
        Dim v As Integer
        Dim gr As Integer
        Dim p As Integer
        Dim var As Kompas6API5.LtVariant ' Интерфейс ksLtVariant
        If Not par Is Nothing Then ' Интерфейс создан

            number = 5 ' Номер вида

            par.Init() ' Инициализация
            par.x = 10 ' Точка привязки вида
            par.y = 20
            par.scale_ = 0.5 ' Масштаб вида
            par.angle = 45 ' Угол поворота вида
            par.color = RGB(10, 20, 10) ' Цвет вида в активном состоянии
            par.state = stACTIVE ' Состояние вида
            par.name = "user view" ' Имя вида

            v = ActiveDoc2D.ksCreateSheetView(par, number) ' Создадим вид с номером 5, масштабом 0.5, под углом 45 гр.
            number = ActiveDoc2D.ksGetViewNumber(v)   ' Номер созданного вида
            kompas.ksMessage("создали вид: ref = " & v & " number = " & number)

            ' Создание группы объектов, type - тип группы ( 0 - определяет модельный, 1 - временный )
            gr = ActiveDoc2D.ksNewGroup(0)
            ActiveDoc2D.ksLineSeg(20, 10, 20, 30, 1)
            ActiveDoc2D.ksLineSeg(20, 30, 40, 30, 1)
            ActiveDoc2D.ksLineSeg(40, 30, 40, 10, 1)
            ActiveDoc2D.ksLineSeg(40, 10, 20, 10, 1)
            ActiveDoc2D.ksEndGroup() ' Завершить создание группы объектов

            ActiveDoc2D.ksAddObjGroup(gr, v) ' Добавим вид в группу
            kompas.ksMessage("добавили вид в группу")
            '    Kompas.ksMessageBoxResult

            p = ActiveDoc2D.ksLineSeg(10, 10, 30, 30, 0) ' Нарисуем отрезок
            ActiveDoc2D.ksAddObjGroup(gr, p) ' И добавим его в группу

            kompas.ksMessage("добавили эл в группу")
            '    Kompas.ksMessageBoxResult

            ActiveDoc2D.ksRotateObj(gr, 0, 0, -45) ' Повернуть группу на -45 градусов вокруг точки ( 0, 0 )

            par.Init() ' Инициализация
            ActiveDoc2D.ksGetObjParam(v, par, ALLPARAM)   ' Получить параметры видa

            kompas.ksMessage("x =" & par.x & " y = " & par.y & " angl = " & par.angle & " name = " & par.name & " st = " & par.state)

            par.Init()
            par.name = "new"

            Dim sss As Integer = ActiveDoc2D.ksSetObjParam(v, par, VIEW_ALLPARAM)




            ActiveDoc2D.ksOpenView(0) ' Сделать текущим системный вид ( номер 0 )

            ' состояние  вида  : только чтение
            ' Интерфейс для хранения данных некоторого типа
            var = kompas.GetParamStruct(Kompas6Constants.StructType2DEnum.ko_LtVariant)

            If Not var Is Nothing Then ' Интерфейс создан

                var.Init() ' Инициализация
                var.intVal = stREADONLY ' Изменить состояние вида ( только чтение )
                ActiveDoc2D.ksSetObjParam(v, var, VIEW_LAYER_STATE)
                'UPGRADE_NOTE: Object var may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                var = Nothing
            End If

            'UPGRADE_NOTE: Object par may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            par = Nothing
        End If
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim doc2D As New myDoc2D(ActiveDoc2D)

        Dim A As New PointDouble("A", 40, 10)
        Dim B As New PointDouble("B", 80, 50)
        Dim C As New PointDouble("C", 20, 60)

        Dim v1 As New cls_SheetView("v1", 100, 200, )
        Dim v2 As New cls_SheetView("v2", 200, 200, , 2)
        Dim v3 As New cls_SheetView("v3", 200, 200, , 250)

        Dim l As Double

        doc2D.AddView(v1)
        doc2D.AddView(v2)

        v1.Create()
        v2.Create()

        v2.Active()

        v1.Active()
        doc2D.myDrawLineSeg(A, B)
        doc2D.myDrawLineSeg(B, C)
        doc2D.myDrawLineSeg(C, A)
        l = doc2D.myCalculateLenghtTwoPoint(A, B)

        v2.Active()
        doc2D.myDrawLineSeg(A, B)
        doc2D.myDrawLineSeg(B, C)
        doc2D.myDrawLineSeg(C, A)
        l = doc2D.myCalculateLenghtTwoPoint(A, B)

        v2.ExitView()

        Dim lenTest1 As Double = myAbsoluteLenghtTwoPoint(v2, A, B)
        Dim newPoint1 As PointDouble = myAbsoluteCordinatePoint(v1, A)
        doc2D.myDrawPoint(newPoint1)

        Dim newPoint2 As PointDouble = myAbsoluteCordinatePoint(v2, A)
        'doc2D.myDrawPoint(newPoint2)
        doc2D.myDrawPoint(newPoint2)


        v2.ScaleView = 1

        v2.X = -50
        v2.Y = -100

        newPoint2 = myAbsoluteCordinatePoint(v2, A)
        doc2D.myDrawPoint(newPoint2)
        ''v2.AngleView = 60



        'doc2D.ksRotateObj(v2.Ref, v2.X, v2.Y, 30)

        ''doc2D.ksMtr(0, 0, 0, 0.5, 0.5)
        ''doc2D.ksTransformObj(v2.Ref)
        ''doc2D.myMtrDelete()

        'Dim AB As Double = 0.1
        'v3.Create(doc2D)
        'v3.Active()

        'doc2D.myLineSegDraw(New PointDouble(0, 0), AB, 30)

        'v3.ExitView()



    End Sub

    Function myAbsoluteLenghtTwoPoint(sheetView1 As cls_SheetView, T1 As PointDouble, T2 As PointDouble) As Double
        Dim len As Double
        len = ActiveDoc2D.myCalculateLenghtTwoPoint(T1, T2)

        Return len * sheetView1.ScaleView
    End Function

    Function myAbsoluteCordinatePoint(sheetView1 As cls_SheetView, T1 As PointDouble) As PointDouble
        Dim newPoint As New PointDouble

        newPoint.X = sheetView1.X + (T1.X * sheetView1.ScaleView)
        newPoint.Y = sheetView1.Y + (T1.Y * sheetView1.ScaleView)

        Return newPoint
    End Function

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Dim iParagraphParam As ksParagraphParam = kompas.GetParamStruct(StructType2DEnum.ko_ParagraphParam)
        iParagraphParam.Init()
        iParagraphParam.x = 130.0
        iParagraphParam.y = 143.37947147215
        iParagraphParam.ang = 0.0
        iParagraphParam.height = 30
        iParagraphParam.width = 50
        iParagraphParam.hFormat = 0
        iParagraphParam.vFormat = 0
        iParagraphParam.style = 1

        ActiveDoc2D.ksParagraph(iParagraphParam)

        Dim iTextLineParam As ksTextLineParam = kompas.GetParamStruct(StructType2DEnum.ko_TextLineParam)
        iTextLineParam.Init()
        iTextLineParam.style = 1

        Dim iTextItemArray As ksDynamicArray = kompas.GetDynamicArray(TEXT_ITEM_ARR)

        Dim iTextItemParam As ksTextItemParam = kompas.GetParamStruct(StructType2DEnum.ko_TextItemParam)
        iTextItemParam.Init()
        iTextItemParam.iSNumb = 0
        iTextItemParam.s = ""
        iTextItemParam.type = 0

        Dim iTextItemFontParam = iTextItemParam.GetItemFont
        iTextItemFontParam.Init()
        iTextItemFontParam.bitVector = 4096
        iTextItemFontParam.color = 0
        iTextItemFontParam.fontName = "GOST type A"
        iTextItemFontParam.height = 5.0
        iTextItemFontParam.ksu = 1.0

        iTextItemArray.ksAddArrayItem(-1, iTextItemParam)

        iTextItemParam = kompas.GetParamStruct(StructType2DEnum.ko_TextItemParam)
        iTextItemParam.Init()
        iTextItemParam.iSNumb = 2
        iTextItemParam.s = ""
        iTextItemParam.type = 17

        iTextItemFontParam = iTextItemParam.GetItemFont()
        iTextItemFontParam.Init()
        iTextItemFontParam.bitVector = 17
        iTextItemFontParam.color = 0
        iTextItemFontParam.fontName = "GOST type A"
        iTextItemFontParam.height = 5.0
        iTextItemFontParam.ksu = 1.0

        iTextItemArray.ksAddArrayItem(-1, iTextItemParam)

        iTextItemParam = kompas.GetParamStruct(StructType2DEnum.ko_TextItemParam)
        iTextItemParam.Init()
        iTextItemParam.iSNumb = 0
        iTextItemParam.s = "Text"
        iTextItemParam.type = 0

        iTextItemFontParam = iTextItemParam.GetItemFont()
        iTextItemFontParam.Init()
        iTextItemFontParam.bitVector = 18
        iTextItemFontParam.color = 0
        iTextItemFontParam.fontName = "GOST type A"
        iTextItemFontParam.height = 5.0
        iTextItemFontParam.ksu = 1.0

        iTextItemArray.ksAddArrayItem(-1, iTextItemParam)

        iTextItemParam = kompas.GetParamStruct(StructType2DEnum.ko_TextItemParam)
        iTextItemParam.Init()
        iTextItemParam.iSNumb = 2
        iTextItemParam.s = ""
        iTextItemParam.type = 6

        iTextItemFontParam = iTextItemParam.GetItemFont()
        iTextItemFontParam.Init()
        iTextItemFontParam.bitVector = 4
        iTextItemFontParam.color = 0
        iTextItemFontParam.fontName = "GOST type A"
        iTextItemFontParam.height = 3.33333325386
        iTextItemFontParam.ksu = 1.0

        iTextItemArray.ksAddArrayItem(-1, iTextItemParam)

        iTextItemParam = kompas.GetParamStruct(StructType2DEnum.ko_TextItemParam)
        iTextItemParam.Init()
        iTextItemParam.iSNumb = 0
        iTextItemParam.s = "v2"
        iTextItemParam.type = 0

        iTextItemFontParam = iTextItemParam.GetItemFont()
        iTextItemFontParam.Init()
        iTextItemFontParam.bitVector = 0
        iTextItemFontParam.color = 0
        iTextItemFontParam.fontName = "GOST type A"
        iTextItemFontParam.height = 3.33333325386
        iTextItemFontParam.ksu = 1.0

        iTextItemArray.ksAddArrayItem(-1, iTextItemParam)

        iTextItemParam = kompas.GetParamStruct(StructType2DEnum.ko_TextItemParam)
        iTextItemParam.Init()
        iTextItemParam.iSNumb = 0
        iTextItemParam.s = "v1"
        iTextItemParam.type = 0

        iTextItemFontParam = iTextItemParam.GetItemFont()
        iTextItemFontParam.Init()
        iTextItemFontParam.bitVector = 5
        iTextItemFontParam.color = 0
        iTextItemFontParam.fontName = "GOST type A"
        iTextItemFontParam.height = 3.33333325386
        iTextItemFontParam.ksu = 1.0

        iTextItemArray.ksAddArrayItem(-1, iTextItemParam)

        iTextItemParam = kompas.GetParamStruct(StructType2DEnum.ko_TextItemParam)
        iTextItemParam.Init()
        iTextItemParam.iSNumb = 0
        iTextItemParam.s = " v3"
        iTextItemParam.type = 0

        iTextItemFontParam = iTextItemParam.GetItemFont()
        iTextItemFontParam.Init()
        iTextItemFontParam.bitVector = 6
        iTextItemFontParam.color = 0
        iTextItemFontParam.fontName = "GOST type A"
        iTextItemFontParam.height = 5.0
        iTextItemFontParam.ksu = 1.0

        iTextItemArray.ksAddArrayItem(-1, iTextItemParam)

        iTextLineParam.SetTextItemArr(iTextItemArray)
        ActiveDoc2D.ksTextLine(iTextLineParam)

        Dim obj As Integer = ActiveDoc2D.ksEndObj()

    End Sub

    Sub Text_()
        Dim iParagraphParam As ksParagraphParam = kompas.GetParamStruct(StructType2DEnum.ko_ParagraphParam)
        iParagraphParam.Init()
        iParagraphParam.x = 130.0
        iParagraphParam.y = 143.37947147215
        iParagraphParam.ang = 0.0
        iParagraphParam.height = 30
        iParagraphParam.width = 50
        iParagraphParam.hFormat = 0
        iParagraphParam.vFormat = 0
        iParagraphParam.style = 1

        ActiveDoc2D.ksParagraph(iParagraphParam)

        Dim iTextLineParam As ksTextLineParam = kompas.GetParamStruct(StructType2DEnum.ko_TextLineParam)
        iTextLineParam.Init()
        iTextLineParam.style = 1

        Dim iTextItemArray As ksDynamicArray = kompas.GetDynamicArray(TEXT_ITEM_ARR)

        Dim iTextItemParam As ksTextItemParam = kompas.GetParamStruct(StructType2DEnum.ko_TextItemParam)
        iTextItemParam.Init()
        iTextItemParam.iSNumb = 0
        iTextItemParam.s = ""
        iTextItemParam.type = 0

        Dim iTextItemFontParam = iTextItemParam.GetItemFont
        iTextItemFontParam.Init()
        iTextItemFontParam.bitVector = 4096
        iTextItemFontParam.color = 0
        iTextItemFontParam.fontName = "GOST type A"
        iTextItemFontParam.height = 5.0
        iTextItemFontParam.ksu = 1.0

        iTextItemArray.ksAddArrayItem(-1, iTextItemParam)

        iTextItemParam = kompas.GetParamStruct(StructType2DEnum.ko_TextItemParam)
        iTextItemParam.Init()
        iTextItemParam.iSNumb = 0
        iTextItemParam.s = "Text"
        iTextItemParam.type = 0

        iTextItemFontParam = iTextItemParam.GetItemFont()
        iTextItemFontParam.Init()
        iTextItemFontParam.bitVector = 18
        iTextItemFontParam.color = 0
        iTextItemFontParam.fontName = "GOST type A"
        iTextItemFontParam.height = 5.0
        iTextItemFontParam.ksu = 1.0

        iTextItemArray.ksAddArrayItem(-1, iTextItemParam)

        iTextItemParam = kompas.GetParamStruct(StructType2DEnum.ko_TextItemParam)
        iTextItemParam.Init()
        iTextItemParam.iSNumb = 2
        iTextItemParam.s = ""
        iTextItemParam.type = 6

        iTextItemFontParam = iTextItemParam.GetItemFont()
        iTextItemFontParam.Init()
        iTextItemFontParam.bitVector = 4
        iTextItemFontParam.color = 0
        iTextItemFontParam.fontName = "GOST type A"
        iTextItemFontParam.height = 3.33333325386
        iTextItemFontParam.ksu = 1.0

        iTextItemArray.ksAddArrayItem(-1, iTextItemParam)

        iTextItemParam = kompas.GetParamStruct(StructType2DEnum.ko_TextItemParam)
        iTextItemParam.Init()
        iTextItemParam.iSNumb = 0
        iTextItemParam.s = "v2"
        iTextItemParam.type = 0

        iTextItemFontParam = iTextItemParam.GetItemFont()
        iTextItemFontParam.Init()
        iTextItemFontParam.bitVector = 0
        iTextItemFontParam.color = 0
        iTextItemFontParam.fontName = "GOST type A"
        iTextItemFontParam.height = 3.33333325386
        iTextItemFontParam.ksu = 1.0

        iTextItemArray.ksAddArrayItem(-1, iTextItemParam)

        iTextLineParam.SetTextItemArr(iTextItemArray)
        ActiveDoc2D.ksTextLine(iTextLineParam)

        Dim obj As Integer = ActiveDoc2D.ksEndObj()
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Dim iParagraphParam As ksParagraphParam = kompas.GetParamStruct(StructType2DEnum.ko_ParagraphParam)
        Dim iTextLineParam As ksTextLineParam = kompas.GetParamStruct(StructType2DEnum.ko_TextLineParam)
        Dim iTextItemArray As ksDynamicArray = kompas.GetDynamicArray(TEXT_ITEM_ARR)
        Dim iTextItemParam As ksTextItemParam = kompas.GetParamStruct(StructType2DEnum.ko_TextItemParam)
        Dim iTextItemFontParam = iTextItemParam.GetItemFont


        iParagraphParam.Init()
        iParagraphParam.x = 130.0
        iParagraphParam.y = 143.37947147215
        iParagraphParam.ang = 0.0
        iParagraphParam.height = 30
        iParagraphParam.width = 50
        iParagraphParam.hFormat = 0
        iParagraphParam.vFormat = 0
        iParagraphParam.style = 1

        ActiveDoc2D.ksParagraph(iParagraphParam)

        iTextLineParam.Init()
        iTextLineParam.style = 1


        'iTextItemParam.Init()
        'iTextItemParam.iSNumb = 0
        'iTextItemParam.s = ""
        'iTextItemParam.type = 0


        'iTextItemFontParam.Init()
        'iTextItemFontParam.bitVector = 4096
        'iTextItemFontParam.color = 0
        'iTextItemFontParam.fontName = "GOST type A"
        'iTextItemFontParam.height = 5.0
        'iTextItemFontParam.ksu = 1.0

        'iTextItemArray.ksAddArrayItem(-1, iTextItemParam)

        iTextItemParam.Init()
        iTextItemParam.iSNumb = 2
        iTextItemParam.s = ""
        iTextItemParam.type = 17

        iTextItemFontParam = iTextItemParam.GetItemFont()
        iTextItemFontParam.Init()
        iTextItemFontParam.bitVector = 17
        iTextItemFontParam.color = 0
        iTextItemFontParam.fontName = "GOST type A"
        iTextItemFontParam.height = 5.0
        iTextItemFontParam.ksu = 1.0

        iTextItemArray.ksAddArrayItem(-1, iTextItemParam)

        iTextLineParam.SetTextItemArr(iTextItemArray)
        ActiveDoc2D.ksTextLine(iTextLineParam)

        Dim obj As Integer = ActiveDoc2D.ksEndObj()
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Dim doc2D As New myDoc2D(ActiveDoc2D)


        Try
            Dim v1 As New cls_SheetView("v1", 100, 200)
            Dim v2 As New cls_SheetView("v1", 100, 200)

            doc2D.AddView(v1)

            'Dim bool As Boolean = doc2D.ExistSheetViewName(v1)

            v1.Create()
            v1.Active()

            Dim A As New PointDouble("A", 40, 10)
            Dim B As New PointDouble("B", 80, 50)
            Dim C As New PointDouble("C", 20, 60)

            v1.ExitView()

            doc2D.AddView(v2)

            v2.Active()

            'Dim v2 As New SheetView("v2", 200, 200, , 2)
            Dim v3 As New cls_SheetView("v3", 200, 200, , 250)

            'Dim l As Double


            'v2.Create(doc2D)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click

    End Sub


    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        Try
            Dim t1 As New clsTest(ActiveDoc2D)
            t1.test2()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        'Text_()
        Text_2(New PointDouble(50, 50), "B", "3", "2")

        Text_2(New PointDouble(80, 50), "A")

        Text_2(New PointDouble(120, 50), "C", "3")

        Text_2(New PointDouble(160, 50), "D", , "2")
    End Sub

    Sub Text_2(T1 As PointDouble, Text As String, Optional TextIndexUp As String = "", Optional TextIndexDown As String = "")
        Dim iTextLineParam As ksTextLineParam
        Dim iTextItemArray As ksDynamicArray
        Dim iTextItemParam As ksTextItemParam
        Dim iTextItemFontParam As Object

        Dim iParagraphParam As ksParagraphParam = kompas.GetParamStruct(StructType2DEnum.ko_ParagraphParam)
        iParagraphParam.Init()
        iParagraphParam.x = T1.X
        iParagraphParam.y = T1.Y
        iParagraphParam.ang = 0.0
        iParagraphParam.height = 30
        iParagraphParam.width = 50
        iParagraphParam.hFormat = 0
        iParagraphParam.vFormat = 0
        iParagraphParam.style = 1

        ActiveDoc2D.ksParagraph(iParagraphParam)

        iTextLineParam = kompas.GetParamStruct(StructType2DEnum.ko_TextLineParam)
        iTextLineParam.Init()
        iTextLineParam.style = 1

        iTextItemArray = kompas.GetDynamicArray(TEXT_ITEM_ARR)

        iTextItemParam = kompas.GetParamStruct(StructType2DEnum.ko_TextItemParam)
        iTextItemParam.Init()
        iTextItemParam.iSNumb = 0
        iTextItemParam.s = ""
        iTextItemParam.type = 0

        iTextItemFontParam = iTextItemParam.GetItemFont
        iTextItemFontParam.Init()
        iTextItemFontParam.bitVector = 4096
        iTextItemFontParam.color = 0
        iTextItemFontParam.fontName = "GOST type A"
        iTextItemFontParam.height = 5.0
        iTextItemFontParam.ksu = 1.0

        iTextItemArray.ksAddArrayItem(-1, iTextItemParam)

        iTextItemParam = kompas.GetParamStruct(StructType2DEnum.ko_TextItemParam)
        iTextItemParam.Init()
        iTextItemParam.iSNumb = 0
        iTextItemParam.s = Text
        iTextItemParam.type = 0

        iTextItemFontParam = iTextItemParam.GetItemFont()
        iTextItemFontParam.Init()
        iTextItemFontParam.bitVector = 18
        iTextItemFontParam.color = 0
        iTextItemFontParam.fontName = "GOST type A"
        iTextItemFontParam.height = 5.0
        iTextItemFontParam.ksu = 1.0

        iTextItemArray.ksAddArrayItem(-1, iTextItemParam)

        If TextIndexUp.Length > 0 Or TextIndexDown.Length > 0 Then
            iTextItemParam = kompas.GetParamStruct(StructType2DEnum.ko_TextItemParam)
            iTextItemParam.Init()
            iTextItemParam.iSNumb = 2
            iTextItemParam.s = ""
            iTextItemParam.type = 6

            iTextItemFontParam = iTextItemParam.GetItemFont()
            iTextItemFontParam.Init()
            iTextItemFontParam.bitVector = 4
            iTextItemFontParam.color = 0
            iTextItemFontParam.fontName = "GOST type A"
            iTextItemFontParam.height = 3.33333325386
            iTextItemFontParam.ksu = 1.0

            iTextItemArray.ksAddArrayItem(-1, iTextItemParam)


            If TextIndexUp.Length > 0 Then
                iTextItemParam = kompas.GetParamStruct(StructType2DEnum.ko_TextItemParam)
                iTextItemParam.Init()
                iTextItemParam.iSNumb = 0
                iTextItemParam.s = TextIndexUp
                iTextItemParam.type = 0

                iTextItemFontParam = iTextItemParam.GetItemFont()
                iTextItemFontParam.Init()
                iTextItemFontParam.bitVector = 0
                iTextItemFontParam.color = 0
                iTextItemFontParam.fontName = "GOST type A"
                iTextItemFontParam.height = 3.33333325386
                iTextItemFontParam.ksu = 1.0

                iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
            End If

            If TextIndexDown.Length > 0 Then
                iTextItemParam = kompas.GetParamStruct(StructType2DEnum.ko_TextItemParam)
                iTextItemParam.Init()
                iTextItemParam.iSNumb = 0
                iTextItemParam.s = TextIndexDown
                iTextItemParam.type = 0

                iTextItemFontParam = iTextItemParam.GetItemFont()
                iTextItemFontParam.Init()
                iTextItemFontParam.bitVector = 5
                iTextItemFontParam.color = 0
                iTextItemFontParam.fontName = "GOST type A"
                iTextItemFontParam.height = 3.33333325386
                iTextItemFontParam.ksu = 1.0

                iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
            End If

        End If


        iTextLineParam.SetTextItemArr(iTextItemArray)
        ActiveDoc2D.ksTextLine(iTextLineParam)

        Dim obj As Integer = ActiveDoc2D.ksEndObj()
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        ActiveDoc2D.ksMacro(0)
        ActiveDoc2D.ksCircle(50, 50, 5, 1)
        ActiveDoc2D.ksText(48, 48, 0, 5, 0, 0, "5")
        ActiveDoc2D.ksEndObj()

    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        Dim doc2D As New myDoc2D(ActiveDoc2D)


        Try
            Dim v1 As New cls_SheetView("v1", 100, 200)

            doc2D.AddView(v1)

            'Dim bool As Boolean = doc2D.ExistSheetViewName(v1)

            v1.Create()
            v1.Active()
            ' -------------------------------------------------

            Dim angleLine As Double = 40


            Dim A As New PointDouble("A", -30, -30)
            Dim B As New PointDouble("B", 65, 25)
            'Dim C As New PointDouble("C", 20, 60)

            doc2D.myDrawPoint(A)
            doc2D.myDrawPoint(B)

            doc2D.myDrawLineSeg(A, B)

            Dim C As PointDouble = doc2D.myCalculateCordinatePoint(A, angleLine, B, 90 + angleLine)

            doc2D.myDrawPoint(C)

            doc2D.myDrawLineSeg(A, C, 4)

            Dim lenX As Double = doc2D.myCalculateLenghtProjectionX(A, B, angleLine)
            Dim lenY As Double = doc2D.myCalculateLenghtProjectionY(A, B, angleLine)

            v1.ExitView()


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click

        cam_0000.Run()

        'cam_K.Run()

    End Sub
End Class