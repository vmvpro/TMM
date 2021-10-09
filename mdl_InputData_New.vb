Imports Kompas6API5, Kompas6Constants

Module mdl_InputData
    Dim kompas As KompasObject
    Dim ActiveDoc2D As ksDocument2D
    Dim DocumParam As ksDocumentParam
    Dim StandartSheet As ksStandartSheet
    Dim SheetPar As ksSheetPar
    Dim str As String

    ' Входные данные
    Private Const planX As Double = 110
    Private Const planY As Double = 310

    Private sheetWidth = 594
    Private sheetHeight = 420

    ' Длины механизма в реальном размере
    Private L_AB As Double = 0.09
    Private L_BC As Double = 0.3
    Private L_BD As Double = 0.4
    Private L_CD As Double = L_BD - L_BC
    Private L_a As Double = 0.04

    ' Длина AB на плане принимаем = 36  
    'Private ml_AB As Double = 36.0

    'Определяем масштаб механизма
    'Private mashtab_ml As Double = L_AB / ml_AB

    ''' <summary>
    ''' Масштаб механизма для конкретного вида на чертеже
    ''' </summary>
    ''' <remarks></remarks>
    Private mashtab_ml_view As Double '= ml_AB / L_AB


    ' Длины механизма в масштабе
    Private ml_BC As Double '= L_BC / mashtab_ml
    Private ml_BD As Double '= L_BD / mashtab_ml
    Private ml_a As Double '= L_a / mashtab_ml

    Private ml_CD As Double '= L_CD / mashtab_ml

    ' Начальный угол механизма 
    ''' <summary>
    ''' Определяем угол для крайнего положения механизма
    ''' a / AB+BC
    ''' </summary>
    ''' <remarks></remarks>
    Private angle_AB_BC As Double = Math.Asin(L_a / (L_AB + L_BC)) * (180 / Math.PI)

    Private mp_A As PointDouble = New PointDouble(0, 0)
    Private mp_B As PointDouble
    ''' <summary>
    ''' Точка крайнего положения механизма
    ''' </summary>
    ''' <remarks></remarks>
    Private mp_B0 As PointDouble

    Private mp_C As PointDouble
    Private mp_D As PointDouble


    ' ================= НАЧАЛО - Входные данные для построения скоростей ===================
    'Dim vb_ As Double

    Dim w2 As Double = 34 ' угловая скорость по заданию
    Private vl_pb As Double = 100 ' на чертеже

    ' Определение скорости vb
    Private vb As Double = w2 * L_AB

    'Определяем масштаб механизма для скоростей
    Private mashtab_mv As Double = vb / vl_pb

    ''' <summary>
    ''' Масштаб для конкретного вида на чертеже
    ''' </summary>
    ''' <remarks></remarks>
    Private mashtab_mv_view As Double = vl_pb / vb


    ' ================= КОНЕЦ входных данных скоростей ===================

    ' Точки скоростей
    Private vp_P As PointDouble
    Private vp_b As PointDouble
    Private vp_c As PointDouble
    Private vp_e As PointDouble
    Private vp_d As PointDouble

    Private vp_s1 As PointDouble
    Private vp_s2 As PointDouble
    Private vp_s3 As PointDouble
    Private vp_s4 As PointDouble
    Private vp_s5 As PointDouble

    ' Длины скоростей

    Private vl_bc As Double
    Private vl_ce As Double
    Private vl_pc As Double
    Private vl_pe As Double

    Private vl_Ps1 As Double
    Private vl_Ps2 As Double
    Private vl_Ps3 As Double
    Private vl_Ps4 As Double
    Private vl_Ps5 As Double

    ' Работа план скоростей для 3-положения
    Private viewShetV3 As Integer = -1

    Sub Meh()
        ActiveDoc2D.ksMtr(planX, planY, 0, 1, 1)

        Dim doc2D As New myDoc2D(ActiveDoc2D)

        doc2D.myDrawCrossingLines(mp_A)
        doc2D.myDrawPoint(mp_A)

        ' Нахождение координаты точки B
        ' Определяем кординату точки B на плане для второго положения
        Dim n As Integer = 5
        Dim step_ As Double = 45

        Dim angle_AB As Double = angle_AB_BC + (n * step_)

        'mp_B = doc2D.myCalculateCordinatePoint(mp_A, ml_AB, angle_AB)
        mp_B = doc2D.myCalculateCordinatePoint(mp_A, 40, angle_AB)
        doc2D.myDrawPoint(mp_B)
        doc2D.myDrawLineSeg(mp_A, mp_B)



        'Dim angle_BC As Double = (Math.Asin((ml_a - ml_AB * Math.Sin(angle_AB * Math.PI / 180)) / 120)) * (180 / Math.PI)
        Dim angle_BC As Double = (Math.Asin((ml_a - 40 * Math.Sin(angle_AB * Math.PI / 180)) / 120)) * (180 / Math.PI)

        If angle_BC >= 0 Then
            angle_BC = angle_BC
        Else
            angle_BC = 360 + angle_BC
        End If

        'Public angle_BC As Double = Math.Asin((ml_a - L))

        ' Задание координаты точки прямой на которой находится точка C
        Dim mp_CordinateLineC As New PointDouble(0, mp_A.Y + ml_a)

        Dim mp_C_ = doc2D.myCalculateCordinatePoint(mp_B, ml_BC, mp_CordinateLineC, 0, myDoc2D.enLocation.quarter_0_90)

        mp_C = doc2D.myCalculateCordinatePoint(mp_B, ml_BC, angle_BC)
        doc2D.myDrawPoint(mp_C)

        mp_D = doc2D.myCalculateCordinatePoint(mp_C, ml_CD, angle_BC)
        doc2D.myDrawPoint(mp_D)

        doc2D.myDrawLineSeg(mp_A, mp_B)
        doc2D.myDrawLineSeg(mp_B, mp_C)
        doc2D.myDrawLineSeg(mp_C, mp_D)

        doc2D.myDrawText(mp_A, mp_A.ToString)
        doc2D.myDrawText(mp_B, mp_B.ToString, , -5)
        doc2D.myDrawText(mp_C, mp_C.ToString)
        doc2D.myDrawText(mp_D, mp_D.ToString)



        'doc2D.myDrawLineSeg(mp_A, mp_D, 4)



        'doc2D.myLineSegDraw(A, B)


        doc2D.myMtrDelete()
    End Sub

End Module
