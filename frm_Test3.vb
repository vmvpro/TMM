Imports Kompas6API5, Kompas6Constants
Imports TMM.myDoc2D
Imports System.Data.SQLite
Imports DB


Public Class frm_Test3

    Dim kompas As KompasObject
    Dim ActiveDoc2D As ksDocument2D
    Dim DocumParam As ksDocumentParam
    Dim StandartSheet As ksStandartSheet
    Dim SheetPar As ksSheetPar
    Dim str As String

    Protected doc2D As myDoc2D


    Private Sub frm_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadForm(ActiveDoc2D, "ШаблонЧертежа")
        doc2D = New myDoc2D(ActiveDoc2D)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        loadForm(ActiveDoc2D, "ШаблонЧертежа")
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        'Dim Doc2D As New myDoc2D(ActiveDoc2D)

        Dim mp_O = New PointDouble(110, 310)
        ActiveDoc2D.myDrawPoint(mp_O)

        Dim meh_Mechanism = New cls_SheetView("Test 1", 110, 310)

        doc2D.AddView(meh_Mechanism)

        meh_Mechanism.Create()
        meh_Mechanism.Active()

        Dim A As PointDouble = New PointDouble("A", 30, 30)
        Dim B As PointDouble = New PointDouble("B", 40, 70)
        Dim C As PointDouble = New PointDouble("C", 70, 40)

        Dim D As PointDouble = New PointDouble("D", 50, 50)

        Dim P1 As PointDouble = New PointDouble("P", 80, 0)
        Dim angle As Double = 70

        doc2D.myDrawPoint(A)
        doc2D.myDrawPoint(B)
        doc2D.myDrawPoint(C)

        doc2D.myDrawLineSeg(A, B)
        doc2D.myDrawLineSeg(B, C)
        doc2D.myDrawLineSeg(C, A)

        meh_Mechanism.ExitView()

        Dim meh_Mechanism2 = New cls_SheetView("Test 2", 110, 150)

        doc2D.AddView(meh_Mechanism2)

        meh_Mechanism2.Create()
        meh_Mechanism2.Active()

        doc2D.myDrawPoint(A)
        doc2D.myDrawPoint(B)
        doc2D.myDrawPoint(C)

        doc2D.myDrawLineSeg(A, B)
        doc2D.myDrawLineSeg(B, C)
        doc2D.myDrawLineSeg(C, A)

        meh_Mechanism2.ExitView()

        meh_Mechanism.Active()
        doc2D.myDrawPoint(D)
        meh_Mechanism.ExitView()

        doc2D.ActiveSheet("Test 2").Active()

        doc2D.myDrawPoint(D)

        meh_Mechanism.ExitView()

        'Dim viewTemp As cls_SheetView = meh_Mechanism2.Ref

        'viewTemp.

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'doc2D.ksMtr(planX, planY, 0, 1, 1)

        'doc2D.myCrossingLines(mp_A)
        'doc2D.myDrawPoint(mp_A)

        'ksCrossingLines(doc2D, mp_D)
        'doc2D.myDrawPoint(mp_D)

        ' Нахождение координаты точки B
        'mp_B = doc2D.myCalculateCordinatePoint(mp_A, ml_AB, angleDegrees)
        'doc2D.myDrawPoint(mp_B)

        ' Нахождение координаты точки C
        'mp_C = doc2D.myCalculateCordinatePointCircleCircle(mp_B, ml_BC, mp_D, ml_CD)
        'doc2D.myDrawPoint(mp_C)

        ' Нахождение координаты точки E
        'Dim vBC As Double = doc2D.myCalculateAngleTwoPoint(mp_B, mp_C)
        'mp_E = doc2D.myCalculateCordinatePoint(mp_C, ml_CE, vBC)

        'doc2D.myDrawLineSeg(mp_A, mp_B)
        'doc2D.myDrawLineSeg(mp_B, mp_C)
        'doc2D.myDrawLineSeg(mp_C, mp_D)
        'doc2D.myDrawLineSeg(mp_C, mp_E)

        'doc2D.myDrawText(mp_A, mp_A.ToString)
        'doc2D.myDrawText(mp_B, mp_B.ToString, , -5)
        'doc2D.myDrawText(mp_C, mp_C.ToString)
        'doc2D.myDrawText(mp_D, mp_D.ToString)
        'doc2D.myDrawText(mp_E, mp_E.ToString, , +5)



        'doc2D.myDrawLineSeg(mp_A, mp_D, 4)



        'doc2D.myLineSegDraw(A, B)


        ActiveDoc2D.ksDeleteMtr()
    End Sub




    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'doc2D.ksMtr(planX, planY, 0, 1, 1)
        '
        Dim Mathematic2D As ksMathematic2D
        Mathematic2D = kompas.GetMathematic2D()

        'Dim pArray As ksMathPointParam
        'pArray = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam)

        Dim pArray As ksDynamicArray
        pArray = kompas.GetDynamicArray(POINT_ARR)

        Dim par As ksMathPointParam = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam)

        Dim circle1 As ksCircleParam
        circle1 = kompas.GetParamStruct(StructType2DEnum.ko_CircleParam)
        circle1.xc = 0 : circle1.yc = 0 : circle1.rad = 70
        Dim c1 As Integer = ActiveDoc2D.ksCircle(circle1.xc, circle1.yc, circle1.rad, 1)

        Dim circle2 As ksCircleParam
        circle2 = kompas.GetParamStruct(StructType2DEnum.ko_CircleParam)
        circle2.xc = 50 : circle2.yc = 50 : circle2.rad = 50
        Dim c2 As Integer = ActiveDoc2D.ksCircle(circle2.xc, circle2.yc, circle2.rad, 1)

        Mathematic2D.ksIntersectCurvCurv(c1, c2, pArray)

        'Document2D.ksDeleteObj(c2)

        pArray.ksGetArrayItem(0, par)
        ActiveDoc2D.ksPoint(par.x, par.y, 0)

        pArray.ksGetArrayType()

        pArray.ksGetArrayItem(1, par)
        ActiveDoc2D.ksPoint(par.x, par.y, 0)

        ActiveDoc2D.ksDeleteMtr()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ActiveDoc2D.ksMtr(145, 270, 0, 1, 1)


        ActiveDoc2D.ksDeleteMtr()
    End Sub


    Function dddd() As PointDouble
        Dim tmp_PointDouble As PointDouble
        Dim list As List(Of PointDouble) = New List(Of PointDouble)


        Dim Mathematic2D As ksMathematic2D
        Mathematic2D = kompas.GetMathematic2D()

        'Dim pArray As ksMathPointParam
        'pArray = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam)

        Dim pArray As ksDynamicArray
        pArray = kompas.GetDynamicArray(POINT_ARR)

        Dim par As ksMathPointParam = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam)

        Dim circle1 As ksCircleParam
        circle1 = kompas.GetParamStruct(StructType2DEnum.ko_CircleParam)
        circle1.xc = 0 : circle1.yc = 0 : circle1.rad = 70
        Dim c1 As Integer = ActiveDoc2D.ksCircle(circle1.xc, circle1.yc, circle1.rad, 1)

        Dim circle2 As ksCircleParam
        circle2 = kompas.GetParamStruct(StructType2DEnum.ko_CircleParam)
        circle2.xc = 50 : circle2.yc = 50 : circle2.rad = 50
        Dim c2 As Integer = ActiveDoc2D.ksCircle(circle2.xc, circle2.yc, circle2.rad, 1)

        Mathematic2D.ksIntersectCurvCurv(c1, c2, pArray)

        For i As Integer = 0 To pArray.ksGetArrayCount
            pArray.ksGetArrayItem(i, par)
            tmp_PointDouble = New PointDouble(par.x, par.y)
            list.Add(tmp_PointDouble)
        Next

        If list(0).Y >= list(1).Y Then
            tmp_PointDouble = New PointDouble(list(1).X, list(1).Y)
        Else
            tmp_PointDouble = New PointDouble(list(0).X, list(0).Y)
        End If

        Return tmp_PointDouble

    End Function


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'doc2D.ksMtr(planX, planY, 0, 1, 1)

        'Dim angle As Double = doc2D.myCalculateAngleTwoPoint(mp_A, mp_B)



        ActiveDoc2D.ksDeleteMtr()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        'doc2D.ksMtr(planX, planY, 0, 1, 1)

        'Dim lenght As Double = doc2D.myCalculateLenghtTwoPoint(mp_A, mp_B)

        ActiveDoc2D.ksDeleteMtr()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        'doc2D.ksMtr(planX, planY, 0, 1, 1)

        'Dim C0 As PointDouble = doc2D.myCalculateCordinatePointCircleCircle(mp_A, ml_AB + ml_BC, mp_D, ml_CD)

        'doc2D.myDrawPoint(C0)

        ActiveDoc2D.ksDeleteMtr()

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim XAB As Double
        Dim XAC As Double
        Dim sumAngle As Double

        kompas = kompasApp()
        ActiveDoc2D = kompas.ActiveDocument2D

        Dim A As PointDouble = New PointDouble(0, 0)
        ActiveDoc2D.myDrawPoint(A)

        Dim B As PointDouble = New PointDouble(59.59, 50)
        ActiveDoc2D.myDrawPoint(B)

        Dim C As PointDouble = New PointDouble(93.3, -25)
        ActiveDoc2D.myDrawPoint(C)

        XAB = ActiveDoc2D.myCalculateAngleTwoPoint(A, B)
        XAC = ActiveDoc2D.myCalculateAngleTwoPoint(A, C)

        sumAngle = XAB + XAC


        'XAB = doc2D.myAngleTwoPoint()

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        preparation()



        ActiveDoc2D.myMtrDelete()
    End Sub

    ''' <summary>
    ''' Подготовка начальные координаты
    ''' </summary>
    ''' <remarks></remarks>
    Sub preparation()
        ActiveDoc2D.myDrawPoint(New PointDouble(0, 0))
        ActiveDoc2D.myDrawPoint(New PointDouble(594, 420))

        Dim tmpPointDouble As New PointDouble(145, 270)
        ActiveDoc2D.myMtr(tmpPointDouble)
    End Sub

    ''' <summary>
    ''' Функция на получения крайних положений механизма
    ''' </summary>
    ''' <remarks></remarks>
    Sub Func0()
        ' Нахождение угла крайнего положения механизма
        ' Функция на получение точки C0
        'pC0 = doc2D.myCordinatePointCircleCircle(pA, lAB + lBC, pD, lCD)
        'm_pC0.Name = "C0"
        'doc2D.myPointDraw(m_pC0)
        'doc2D.myText(m_pC0, 7, m_pC0.ToString())

        'Dim fi20 As Double = doc2D.myAngleTwoPoint(m_pA, m_pC0)

        ''Dim B0 As PointDouble = doc2D.myLineSegDraw(pA, lAB, fi20)
        'B0.Name = "B0"
        'doc2D.myPointDraw(B0)
    End Sub


    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim mp_O = New PointDouble(110, 310)
        ActiveDoc2D.myDrawPoint(mp_O)

        

        Dim counter = 23
        Dim v = 3

        'Dim dt_Mehanizm = StoredProcedure.Zadanie(TableName.z3_mehanizm, counter)
        'Dim dr_Mehanizm = DataBase.Row(TableName.z3_mehanizm, "numberPosition", 3)

        'Dim dt_Speed = StoredProcedure.Zadanie(TableName.z3_Speed, counter)
        'Dim dr_Speed = DataBase.Row(dt_Speed, "numberPosition", v)

        'Dim dt_Acceleration = StoredProcedure.Zadanie(TableName.z3_acceleration, counter)
        'Dim dr_Acceleration = DataBase.Row(dt_Acceleration, "numberPosition", v)

        Dim L_ab = 0.09
        Dim ml_AB = 45

        Dim L_bc = 0.3
        Dim ml_BC = 150

        Dim mashtab_ml = 0.002
        Dim mashtab_ml_view = 500

        Dim meh_Mechanism1 = New cls_SheetView("Test 1", 110, 310, 0, 400)
        Dim meh_Mechanism2 = New cls_SheetView("Test 2", 110, 150, 0, 500)

        doc2D.AddView(meh_Mechanism1)
        doc2D.AddView(meh_Mechanism2)

        meh_Mechanism1.Create()
        meh_Mechanism2.Create()

        meh_Mechanism1.Active()

        Dim A As PointDouble = New PointDouble("A", 0, 0)
        doc2D.myDrawPoint(A)
        Dim B = doc2D.myCalculateCordinatePoint(A, L_ab, 30)
        doc2D.myDrawLineSeg(A, B)

        meh_Mechanism2.Active()
        doc2D.myDrawPoint(A)
        B = doc2D.myCalculateCordinatePoint(A, L_ab, 30)
        doc2D.myDrawLineSeg(A, B)

        doc2D.SystemSheet()


        'doc2D.s()

        'Dim B As PointDouble = New PointDouble("B", 40, 70)
        'Dim C As PointDouble = New PointDouble("C", 70, 40)


    End Sub
End Class