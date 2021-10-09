Imports Kompas6API5, Kompas6Constants
Partial Public Class clsTest

    Public Sub Mehanizm()
        'Dim doc2D As New myDoc2D(doc2D)
        mp_O = New PointDouble(110, 310)

		meh_SheetView = New cls_SheetView("mehanizm", 110, 310, , mashtab_ml_view)

		doc2D.AddView(meh_SheetView)

		meh_SheetView.Create()
		meh_SheetView.Active()

        doc2D.myDrawCrossingLines(mp_A)
		doc2D.myDrawPoint(mp_A)


		' Для определения крайнего положения механизма в точке C0 нужно определить прямую на которой эта точка находится
		Dim line_CordinateLineC As New PointDouble(0, mp_A.Y + L_a)

		mp_C0 = doc2D.myCalculateCordinatePoint(mp_A, L_AB + L_BC, line_CordinateLineC, 0, myDoc2D.enLocation.quarter_0_90)

		' Определяем угол для крайнего положения механизма из точки A и точки C0
		Dim angle_AC0 As Double = doc2D.myCalculateAngleTwoPoint(mp_A, mp_C0)

		' Нахождение координаты точки B
		' Определяем координату точки B на плане для положения, которое задано в переменной numberPosition
		Dim numberPosition As Integer = 5
		Dim step_ As Double = 45
		Dim angle_AB As Double = angle_AC0 + (numberPosition * step_)

		mp_B = doc2D.myCalculateCordinatePoint(mp_A, L_AB, angle_AB)
		doc2D.myDrawPoint(mp_B)
		doc2D.myDrawLineSeg(mp_A, mp_B)

		mp_C = doc2D.myCalculateCordinatePoint(mp_B, L_BC, line_CordinateLineC, 0, myDoc2D.enLocation.quarter_0_90)
		doc2D.myDrawPoint(mp_C)

		Dim angle_BC As Double = doc2D.myCalculateAngleTwoPoint(mp_B, mp_C)

		mp_D = doc2D.myCalculateCordinatePoint(mp_C, L_CD, angle_BC)
		doc2D.myDrawPoint(mp_D)

		doc2D.myDrawLineSeg(mp_A, mp_B)
		doc2D.myDrawLineSeg(mp_B, mp_C)
		doc2D.myDrawLineSeg(mp_C, mp_D)

		doc2D.myDrawText(mp_A, doc2D.myAbsoluteCordinatePoint(meh_SheetView, mp_A).ToString())
		doc2D.myDrawText(mp_B, mp_B.ToString)
		doc2D.myDrawText(mp_C, mp_C.ToString)
		doc2D.myDrawText(mp_D, mp_D.ToString)

		'doc2D.myDrawLineSeg(mp_A, mp_D, 4)

		'doc2D.myLineSegDraw(A, B)

		'doc2D.myMtrDelete()
	End Sub

	Public Sub mehanizm_speed()
		'Dim doc2D As New myDoc2D(ActiveDoc2D)

		'doc2D.ksMtr(370, 235, 0, 1, 1)

		' Создание вида для 5-положения
		mehSpeed_SheetView = New cls_SheetView("v5", 100, 200, , mashtab_mv_view)

		doc2D.AddView(mehSpeed_SheetView)
		'v3.Create(doc2D) 
		' Делаем вид активным
		mehSpeed_SheetView.Create()
		mehSpeed_SheetView.Active()

		' Задаем начальную точку P для построения плана скоростей
		vp_P = New PointDouble(0, 0)
		' Определяем угол Pb - Функцией. Принимаем параметры угол между точкой А и B + 90 (перпендикуляр AB)
		Dim angle_Pb As Double = doc2D.myCalculateAngleTwoPoint(mp_A, mp_B) + 90

		' Находим точку b на плане скоростей согласно принимаемых данных: точка P, Длина 70, угол перпендикуляр AB
		vp_b = doc2D.myCalculateCordinatePoint(vp_P, vb, angle_Pb)

		' Рисуем вектор Pb
		doc2D.myDrawVector(vp_P, vp_b)

		' Определение точки с на плане скоростей на пересечении перпендикуляров точок b и P плана скоростей 
		' соответственно своих ланок на плане механизма
		Dim angle_bc As Double = doc2D.myCalculateAngleTwoPoint(mp_B, mp_C) + 90

		Dim angle_px As Double = doc2D.myCalculateAngleTwoPoint(vp_P, New PointDouble(100, 0))

		vp_c = doc2D.myCalculateCordinatePoint(vp_b, angle_bc, vp_P, angle_px)

		' Рисуем вектор Pb
		doc2D.myDrawVector(vp_b, vp_c)
		doc2D.myDrawVector(vp_P, vp_c)

		' Определение длины bc на плане скоростей
		vl_bc = doc2D.myCalculateLenghtTwoPoint(vp_b, vp_c)

		' Определение длины ce на плане скоростей  (математика все длины есть )
		vl_ce = ml_CD / ml_BC * vl_bc

		' Нахождение координаты точки е на плане скоростей
		' входные параметры: точка с, длина се и угол Pb

		' Определяем угол направления вектора bc по точкам b и c

		Dim angle_ce = doc2D.myCalculateAngleTwoPoint(vp_b, vp_c)

		vp_d = doc2D.myCalculateCordinatePoint(vp_c, vl_ce, angle_ce)

		' Рисуем вектор ce
		doc2D.myDrawVector(vp_c, vp_d)

		' Соединяем с полюсом 
		doc2D.myDrawVector(vp_P, vp_d)


		' Определения координат s1, s2, s3, s4, s5
		vp_s2 = doc2D.myCalculateCordinateCentreSeg(vp_P, vp_b)
		vp_s3 = doc2D.myCalculateCordinateCentreSeg(vp_b, vp_d)
		'vp_s4 = doc2D.myCalculateCordinateCentreSeg(vp_c, vp_c)

		' Рисуем вектора для s2, s3, s4
		doc2D.myDrawVector(vp_P, vp_s2)
		doc2D.myDrawVector(vp_P, vp_s3)
		'doc2D.myDrawVector(vp_P, vp_s4)

		' Простановка всех точек на плане скоростей
		doc2D.myDrawText(vp_P, "P")
		doc2D.myDrawText(vp_b, "b")
		doc2D.myDrawText(vp_c, "c, s4")
		doc2D.myDrawText(vp_d, "d")

		doc2D.myDrawText(vp_s2, "s2")
		doc2D.myDrawText(vp_s3, "s3")

		'doc2D.myDrawText(vp_s4, "s4")


		mehSpeed_SheetView.ExitView()



		'doc2D.myMtrDelete()
	End Sub

	''' <summary>
	''' Работа ускорения
	''' </summary>
	''' <remarks></remarks>
	Public Sub acceleration()

		'Dim doc2D As New myDoc2D(ActiveDoc2D)

		'doc2D.ksMtr(370, 235, 0, 1, 1)

		' Создание вида для 5-положения
		mehAcceleration_SheetView = New cls_SheetView("a", 300, 200, , mashtab_ma_view)

		doc2D.AddView(mehAcceleration_SheetView)
		'v3.Create(doc2D) 
		' Делаем вид активным
		mehAcceleration_SheetView.Create()
		mehAcceleration_SheetView.Active()

		' Задаем начальную точку P для построения плана ускорений
		ap_P = New PointDouble(0, 0)
		' Определяем угол Pb - Функцией. Принимаем параметры угол между точкой А и B (паралельность AB)
		Dim aAngle_Pb As Double = doc2D.myCalculateAngleTwoPoint(mp_B, mp_A)

		' Находим точку b на плане скоростей согласно принимаемых данных: точка ap_P, Длина ab, угол паралельность AB
		ap_b = doc2D.myCalculateCordinatePoint(ap_P, ab, aAngle_Pb)

		' Рисуем вектор Pb
		doc2D.myDrawVector(ap_P, ap_b)

		' Определяем длину вектора bn3 
		' нормальне прискорення точки ap_c относительно точки ap_b

		' Для определения точки ap_n3 на плане ускорения нужно определить
		' угол из точки ap_b и длину вектора al_bn3
		al_bn3 = (vl_bc ^ 2) / L_BC
		Dim angleCB As Double = doc2D.myCalculateAngleTwoPoint(mp_C, mp_B)

		ap_n3 = doc2D.myCalculateCordinatePoint(ap_b, al_bn3, angleCB)
		doc2D.myDrawVector(ap_b, ap_n3)

		' Определяем точку ap_c 
		' Строится из точки ap_n3 перпендикуляр угла вектора al_bn3 и
		' и из точки ap_P ось по X

		ap_c = doc2D.myCalculateCordinatePoint(ap_n3, al_bn3 + 90, ap_P, 0)
		doc2D.myDrawVector(ap_b, ap_c)
		doc2D.myDrawVector(ap_n3, ap_c)

		doc2D.myDrawVector(ap_P, ap_c)

		al_bc = doc2D.myCalculateLenghtTwoPoint(ap_b, ap_c)

		' Определяем точку длину al_cd для определения координаты точки ap_d и угол сd
		al_cd = L_CD / L_BC * al_bc

		Dim angle_cd As Double = doc2D.myCalculateAngleTwoPoint(ap_b, ap_c)

		ap_d = doc2D.myCalculateCordinatePoint(ap_c, al_cd, angle_cd)
		doc2D.myDrawVector(ap_c, ap_d)

		' Соединяем с полюсом 
		doc2D.myDrawVector(ap_P, ap_d)


		' Определения координат s1, s2, s3, s4, s5
		ap_s2 = doc2D.myCalculateCordinateCentreSeg(ap_P, ap_b)
		ap_s3 = doc2D.myCalculateCordinateCentreSeg(ap_b, ap_d)
		' s4 определяется в данном случае в точке c
		'ap_s4 = doc2D.myCalculateCordinateCentreSeg(vp_c, vp_c)


		' Рисуем вектора для s2, s3, s4
		doc2D.myDrawVector(ap_P, ap_s2)
		doc2D.myDrawVector(ap_P, ap_s3)

		'doc2D.myDrawVector(vp_P, vp_s4)

		' Простановка всех точек на плане скоростей
		doc2D.myDrawText(ap_P, "P")
		doc2D.myDrawText(ap_b, "b")
		doc2D.myDrawText(ap_c, "c, s4")
		doc2D.myDrawText(ap_d, "d")

		doc2D.myDrawText(ap_s2, "s2")
		doc2D.myDrawText(ap_s3, "s3")

		'doc2D.myDrawText(vp_s4, "s4")


		mehAcceleration_SheetView.ExitView()

	End Sub

    Public Sub AbsoluteWork()
        Dim lenTest1 As Double = doc2D.myAbsoluteLenghtTwoPoint(meh_SheetView, mp_A, mp_B)
        Dim newPoint1 As PointDouble = doc2D.myAbsoluteCordinatePoint(meh_SheetView, mp_C)

        Dim lenTest2 As Double = doc2D.myAbsoluteLenghtTwoPoint(mehSpeed_SheetView, vp_P, vp_b)
        Dim newPoint2 As PointDouble = doc2D.myAbsoluteCordinatePoint(mehSpeed_SheetView, vp_P)
    End Sub

End Class
