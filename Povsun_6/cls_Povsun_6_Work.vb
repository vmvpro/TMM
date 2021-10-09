Imports Kompas6API5, Kompas6Constants
Imports TMM.myDoc2D

Partial Public Class cls_Povsun_6

    Public Sub Run()
        Dim bool As Boolean = True
        Dim k As Integer = 3

        If bool Then
            MechanismConstruction(0)
            MechanismDraw(0, 2)

            MechanismConstruction(k)
            MechanismDraw(k, 1)

            Mechanism_Speed(k)
            Mechanism_SpeedDraw(k)

            Mechanism_Acceleration(k)
            Mechanism_AccelerationDraw(k)

            'Mechanism_Acceleration_(k)
            Exit Sub
        End If

        For i As Integer = 0 To 7
            MechanismConstruction(i)

            ' отрисовка толщины линий
            If i = k Then
                'толстая линия 
                MechanismDraw(i, 1)
            Else

                MechanismDraw(i, 2)
            End If

            Mechanism_Speed(i)
            Mechanism_SpeedDraw(i)

            If i = k Then
                Mechanism_Acceleration(i)
                Mechanism_AccelerationDraw(i)
            Else
                ' MechanismDraw(2)
            End If


        Next

        kompas = kompasApp()
        kompas.ksMessage("Построение закончено")

        'ActiveDoc2D.m()

        'shatun.Mechanism_Speed()

    End Sub

    Public Sub MechanismConstruction(numberPosition As Integer)
        'Dim doc2D As New myDoc2D(doc2D)
        mp_O = New PointDouble(110, 310)

        meh_Mechanism = New cls_SheetView("Mechanism " & numberPosition, 110, 310, , mashtab_ml_view)

        doc2D.AddView(meh_Mechanism)

        meh_Mechanism.Create()
        meh_Mechanism.Active()

        'doc2D.myDrawCrossingLines(mp_A)
        'doc2D.myDrawPoint(mp_A)


        ' Для определения крайнего положения механизма в точке C0 нужно определить координаты прямой на которой эта точка находится
        Dim line_CordinateLineC As New PointDouble(0, mp_A.Y - L_a)

        ' Поиск точки C происходит так.
        ' Из точки B проводим круг заданным радиусом и на пересечении прямой line_CordinateLineC 
        ' (Угол прямой line_CordinateLineC Определяем по тому как у нас размещена точка С)
        ' переменная myDoc2D.enLocation - это выбор (перечисление) и нужно определить в каком месте расположена точка C 
        ' В нашем механизме есть 8 вариантов: 4 из оси x, y и 4 еще - четверти координатных осей 
        mp_C0 = doc2D.myCalculateCordinatePoint(mp_A, L_AB + L_BC, line_CordinateLineC, 0, enLocation.quarter_270_360)

        ' Определяем угол для крайнего положения механизма из точки A и точки C0
        Dim angle_AC0 As Double = doc2D.myCalculateAngleTwoPoint(mp_A, mp_C0)

        ' Нахождение координаты точки B
        ' Определяем координату точки B на плане для положения, которое задано в переменной numberPosition
        'Dim numberPosition As Integer = 5

        ' Шаг перемещения механизма
        Dim step_ As Double = 45
        Dim angle_AB As Double = angle_AC0 + (numberPosition * step_)

        If angle_AB > 360 Then angle_AB -= 360

        mp_B = doc2D.myCalculateCordinatePoint(mp_A, L_AB, angle_AB)
        'doc2D.myDrawPoint(mp_B)
        'doc2D.myDrawLineSeg(mp_A, mp_B)

        mp_C = doc2D.myCalculateCordinatePoint(mp_B, L_BC, line_CordinateLineC, 0, myDoc2D.enLocation.quarter_270_360)
        'doc2D.myDrawPoint(mp_C)

        Dim angle_CB As Double = doc2D.myCalculateAngleTwoPoint(mp_C, mp_B)

        mp_D = doc2D.myCalculateCordinatePoint(mp_B, L_BD, angle_CB)

        mp_S2 = doc2D.myCalculateCordinateCentreSeg(mp_A, mp_B)
        mp_S3 = doc2D.myCalculateCordinateCentreSeg(mp_D, mp_C)
        mp_S4 = mp_C

        'doc2D.myDrawPoint(mp_D)

        'doc2D.myDrawLineSeg(mp_A, mp_B)
        'doc2D.myDrawLineSeg(mp_B, mp_C)
        'doc2D.myDrawLineSeg(mp_C, mp_D)

        'doc2D.myDrawTextIndex(mp_B, "B", "", numberPosition.ToString())
        'doc2D.myDrawTextIndex(mp_C, "C", "", numberPosition.ToString())
        'doc2D.myDrawTextIndex(mp_D, "D", "", numberPosition.ToString())

        'doc2D.myDrawText(mp_A, doc2D.myAbsoluteCordinatePoint(meh_Mechanism, mp_A).ToString())
        'doc2D.myDrawText(mp_B, mp_B.ToString)
        'doc2D.myDrawText(mp_C, mp_C.ToString)
        'doc2D.myDrawText(mp_D, mp_D.ToString)


        'doc2D.myDrawLineSeg(mp_A, mp_D, 4)

        meh_Mechanism.ExitView()

        'doc2D.myLineSegDraw(A, B)


        'doc2D.myMtrDelete()
    End Sub

    Public Sub MechanismDraw(numberPosition As Integer, style As Integer)
        Dim height As Double = 4

        meh_Mechanism.Active()

        If numberPosition = 0 Then
            doc2D.myDrawCrossingLines(mp_A)
            doc2D.myDrawPoint(mp_A, enStylePoint.Circle)
            doc2D.myDrawTextIndex(mp_A, "A", , , height)
        End If

        doc2D.myDrawPoint(mp_B, enStylePoint.Circle)
        doc2D.myDrawPoint(mp_C, enStylePoint.Circle)
        doc2D.myDrawPoint(mp_D, enStylePoint.Circle)

        doc2D.myDrawLineSeg(mp_A, mp_B, style)
        doc2D.myDrawLineSeg(mp_B, mp_C, style)
        doc2D.myDrawLineSeg(mp_B, mp_D, style)

        doc2D.myDrawTextIndex(mp_B, "B", "", numberPosition.ToString(), height)
        doc2D.myDrawTextIndex(mp_C, "C", "", numberPosition.ToString(), height)
        doc2D.myDrawTextIndex(mp_D, "D", "", numberPosition.ToString(), height)

        If style = 1 Then
            doc2D.myDrawTextIndex(mp_S2, "S2", "", numberPosition.ToString(), height)
            doc2D.myDrawTextIndex(mp_S3, "S3", "", numberPosition.ToString(), height)
            doc2D.myDrawTextIndex(mp_S4, "S4", "", numberPosition.ToString(), height)
        End If

        meh_Mechanism.ExitView()
    End Sub

    Public Sub Mechanism_Speed(numberPosition As Integer)
        'Dim doc2D As New myDoc2D(ActiveDoc2D)

        'doc2D.ksMtr(370, 235, 0, 1, 1)

        ' Создание вида для 5-положения
        meh_Speed = New cls_SheetView("v " & numberPosition, 200 + numberPosition * 100, 200, , mashtab_mv_view)

        doc2D.AddView(meh_Speed)
        'v3.Create(doc2D) 
        ' Делаем вид активным
        meh_Speed.Create()
        meh_Speed.Active()

        ' Задаем начальную точку P для построения плана скоростей
        vp_P = New PointDouble(0, 0)
        ' Определяем угол Pb - Функцией. Принимаем параметры угол между точкой А и B + 90 (перпендикуляр AB)
        Dim angle_Pb As Double = doc2D.myCalculateAngleTwoPoint(mp_A, mp_B) + 90

        ' Находим точку b на плане скоростей согласно принимаемых данных: точка P, Длина 70 и угол в переменной angle_Pb
        vp_b = doc2D.myCalculateCordinatePoint(vp_P, v_b, angle_Pb)

        ' Рисуем вектор Pb
        'doc2D.myDrawVector(vp_P, vp_b)

        ' Определение точки с на плане скоростей на пересечении перпендикуляров точок b и P плана скоростей 
        ' соответственно своих ланок на плане механизма
        Dim angle_bc As Double = doc2D.myCalculateAngleTwoPoint(mp_B, mp_C) + 90

        Dim angle_px As Double = doc2D.myCalculateAngleTwoPoint(vp_P, New PointDouble(100, 0))

        vp_c = doc2D.myCalculateCordinatePoint(vp_b, angle_bc, vp_P, angle_px)

        ' Рисуем вектор Pb
        'doc2D.myDrawVector(vp_b, vp_c)
        'doc2D.myDrawVector(vp_P, vp_c)

        ' Определение длины bc на плане скоростей
        v_bc = doc2D.myCalculateLenghtTwoPoint(vp_b, vp_c)

        ' Определение длины bd на плане скоростей  (математика все длины есть )
        v_bd = ml_BD / ml_BC * v_bc
        bd_v(numberPosition) = v_bd

        ' Нахождение координаты точки е на плане скоростей
        ' входные параметры: точка с, длина се и угол Pb

        ' Определяем угол направления вектора bc по точкам b и c

        Dim angle_bd = doc2D.myCalculateAngleTwoPoint(vp_c, vp_b)

        vp_d = doc2D.myCalculateCordinatePoint(vp_b, v_bd, angle_bd)

        ' Рисуем вектор bd
        'doc2D.myDrawVector(vp_b, vp_d)

        ' Соединяем с полюсом 
        'doc2D.myDrawVector(vp_P, vp_d)


        ' Определения координат s1, s2, s3, s4, s5
        vp_s2 = doc2D.myCalculateCordinateCentreSeg(vp_P, vp_b)
        vp_s3 = doc2D.myCalculateCordinateCentreSeg(vp_c, vp_d)
        vp_s4 = doc2D.myCalculateCordinateCentreSeg(vp_P, vp_c)

        ' Вычисление проекции на ось силы тяжести и угла вектора для МЗВ
        ps2_v(numberPosition) = doc2D.myCalculateLenghtProjectionY(vp_P, vp_s2, 0)
        ps3_v(numberPosition) = doc2D.myCalculateLenghtProjectionY(vp_P, vp_s3, 0)
        ps4_v(numberPosition) = doc2D.myCalculateLenghtProjectionY(vp_P, vp_s4, 0)

        ' Рисуем вектора для s2, s3, s4
        'doc2D.myDrawVector(vp_P, vp_s2)
        'doc2D.myDrawVector(vp_P, vp_s3)
        'doc2D.myDrawVector(vp_P, vp_s4)

        ' Простановка всех точек на плане скоростей
        'doc2D.myDrawText(vp_P, "P")
        'doc2D.myDrawText(vp_b, "b")
        'doc2D.myDrawText(vp_c, "c, s4")
        'doc2D.myDrawText(vp_d, "d")

        'doc2D.myDrawText(vp_s2, "s2")
        'doc2D.myDrawText(vp_s3, "s3")

        'doc2D.myDrawText(vp_s4, "s4")


        meh_Speed.ExitView()



        'doc2D.myMtrDelete()
    End Sub

    Public Sub Mechanism_SpeedDraw(numberPosition As Integer)
        meh_Speed.Active()

        doc2D.myDrawNumberMacro(vp_P, numberPosition.ToString(), meh_Speed.ScaleView)

        ' Рисуем вектор Pb
        doc2D.myDrawVector(vp_P, vp_b)

        ' Рисуем вектор Pb
        doc2D.myDrawVector(vp_b, vp_c)
        doc2D.myDrawVector(vp_P, vp_c)


        ' Рисуем вектор bd
        doc2D.myDrawVector(vp_b, vp_d)

        ' Соединяем с полюсом 
        doc2D.myDrawVector(vp_P, vp_d)

        ' Рисуем вектора для s2, s3, s4
        doc2D.myDrawVector(vp_P, vp_s2)
        doc2D.myDrawVector(vp_P, vp_s3)
        'doc2D.myDrawVector(vp_P, vp_s4)

        ' Простановка всех точек на плане скоростей
        doc2D.myDrawText(vp_P, "Pv")
        doc2D.myDrawText(vp_b, "b")
        doc2D.myDrawText(vp_c, "c, s4")
        doc2D.myDrawText(vp_d, "d")

        doc2D.myDrawText(vp_s2, "s2")
        doc2D.myDrawText(vp_s3, "s3")

        meh_Speed.ExitView()
    End Sub

    ''' <summary>
    ''' Работа ускорения
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Mechanism_Acceleration(numberPosition As Integer)

        'Dim doc2D As New myDoc2D(ActiveDoc2D)

        'doc2D.ksMtr(370, 235, 0, 1, 1)

        ' Создание вида для 5-положения
        meh_Acceleration = New cls_SheetView("a " & numberPosition, 300, 200, , mashtab_ma_view)

        doc2D.AddView(meh_Acceleration)
        'v3.Create(doc2D) 
        ' Делаем вид активным
        meh_Acceleration.Create()
        meh_Acceleration.Active()

        ' Задаем начальную точку P для построения плана ускорений
        ap_pi = New PointDouble(0, 0)
        ' Определяем угол Pb - Функцией. Принимаем параметры угол между точкой А и B (паралельность AB)
        Dim aAngle_Pb As Double = doc2D.myCalculateAngleTwoPoint(mp_B, mp_A)

        ' Находим точку b на плане скоростей согласно принимаемых данных: точка ap_P, Длина ab, угол паралельность AB
        ap_b = doc2D.myCalculateCordinatePoint(ap_pi, a_b, aAngle_Pb)

        ' Рисуем вектор Pb
        'doc2D.myDrawVector(ap_P, ap_b)

        ' Определяем длину вектора bn3 
        ' нормальне прискорення точки ap_c относительно точки ap_b

        ' Для определения точки ap_n3 на плане ускорения нужно определить
        ' угол из точки ap_b и длину вектора al_bn3
        a_n3 = (v_bc ^ 2) / L_BC
        Dim angleCB As Double = doc2D.myCalculateAngleTwoPoint(mp_C, mp_B)

        ap_n3 = doc2D.myCalculateCordinatePoint(ap_b, a_n3, angleCB)
        'doc2D.myDrawVector(ap_b, ap_n3)

        ' Определяем точку ap_c 
        ' Строится из точки ap_n3 перпендикуляр угла вектора al_bn3 и
        ' и из точки ap_P ось по X

        ap_c = doc2D.myCalculateCordinatePoint(ap_n3, a_n3 + 90, ap_pi, 0)
        'doc2D.myDrawVector(ap_b, ap_c)
        'doc2D.myDrawVector(ap_n3, ap_c)

        'doc2D.myDrawVector(ap_P, ap_c)

        a_bc = doc2D.myCalculateLenghtTwoPoint(ap_b, ap_c)
        bc_a(numberPosition) = a_bc

        ' Определяем точку длину al_cd для определения координаты точки ap_d и угол сd
        a_bd = L_BD / L_BC * a_bc

        Dim angle_bd As Double = doc2D.myCalculateAngleTwoPoint(ap_c, ap_b)

        ap_d = doc2D.myCalculateCordinatePoint(ap_b, a_bd, angle_bd)
        'doc2D.myDrawVector(ap_c, ap_d)

        ' Соединяем с полюсом 
        'doc2D.myDrawVector(ap_P, ap_d)


        ' Определения координат s1, s2, s3, s4, s5
        ap_s2 = doc2D.myCalculateCordinateCentreSeg(ap_pi, ap_b)
        ps2_a(numberPosition) = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_s2)

        ap_s3 = doc2D.myCalculateCordinateCentreSeg(ap_b, ap_d)
        ps3_a(numberPosition) = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_s3)

        ' s4 определяется в данном случае в точке c
        ap_s4 = ap_c
        ps4_a(numberPosition) = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_s4)


        ' Рисуем вектора для s2, s3, s4
        'doc2D.myDrawVector(ap_P, ap_s2)
        'doc2D.myDrawVector(ap_P, ap_s3)

        'doc2D.myDrawVector(vp_P, vp_s4)

        ' Простановка всех точек на плане скоростей
        'doc2D.myDrawText(ap_P, "P")
        'doc2D.myDrawText(ap_b, "b")
        'doc2D.myDrawText(ap_c, "c, s4")
        'doc2D.myDrawText(ap_d, "d")

        'doc2D.myDrawText(ap_s2, "s2")
        'doc2D.myDrawText(ap_s3, "s3")

        'doc2D.myDrawText(vp_s4, "s4")


        meh_Acceleration.ExitView()

    End Sub

	Public Sub Mechanism_AccelerationDraw(numberPosition As Integer)
		meh_Acceleration.Active()

		doc2D.myDrawNumberMacro(ap_pi, numberPosition.ToString(), meh_Acceleration.ScaleView)

		' Рисуем вектор Pb
		doc2D.myDrawVector(ap_pi, ap_b)

		doc2D.myDrawVector(ap_b, ap_n3)

		doc2D.myDrawVector(ap_b, ap_c)
		doc2D.myDrawVector(ap_n3, ap_c)

		doc2D.myDrawVector(ap_pi, ap_c)

		doc2D.myDrawVector(ap_b, ap_d)

		' Соединяем с полюсом 
		doc2D.myDrawVector(ap_pi, ap_d)

		' Рисуем вектора для s2, s3, s4
		doc2D.myDrawVector(ap_pi, ap_s2)
		doc2D.myDrawVector(ap_pi, ap_s3)

		'doc2D.myDrawVector(vp_P, vp_s4)

		' Простановка всех точек на плане скоростей
		doc2D.myDrawText(ap_pi, "Pa")
		doc2D.myDrawText(ap_b, "b")
		doc2D.myDrawText(ap_c, "c, s4")
		doc2D.myDrawText(ap_d, "d")
		doc2D.myDrawText(ap_n3, "n3")

		doc2D.myDrawText(ap_s2, "s2")
		doc2D.myDrawText(ap_s3, "s3")

		'doc2D.myDrawText(vp_s4, "s4")


		meh_Acceleration.ExitView()
	End Sub

    Sub PowerAnalysis()
        meh_Acceleration = New cls_SheetView("a " & numberPosition, 300, 200, , mashtab_ma_view)

        doc2D.AddView(meh_Acceleration)
        'v3.Create(doc2D) 
        ' Делаем вид активным
        meh_Acceleration.Create()
        meh_Acceleration.Active()

    End Sub

    Public Sub AbsoluteWork()
        Dim lenTest1 As Double = doc2D.myAbsoluteLenghtTwoPoint(meh_Mechanism, mp_A, mp_B)
        Dim newPoint1 As PointDouble = doc2D.myAbsoluteCordinatePoint(meh_Mechanism, mp_C)

        Dim lenTest2 As Double = doc2D.myAbsoluteLenghtTwoPoint(meh_Speed, vp_P, vp_b)
        Dim newPoint2 As PointDouble = doc2D.myAbsoluteCordinatePoint(meh_Speed, vp_P)
    End Sub

End Class
