Imports Kompas6API5, Kompas6Constants
Imports TMM.myDoc2D
Imports TMM.mdl_Function

Partial Public Class cls_Shatun_9

    Public Sub Run()

        'Me.doc2D = doc2D
        Dim bool As Boolean = True
        Dim k As Integer = 3

        If bool Then
            MechanismConstruction(k)
            MechanismDraw(k, 2)

            Mechanism_Speed(k)
            Mechanism_SpeedDraw(k)

            Mechanism_Acceleration()
            Mechanism_AccelerationDraw(k)


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
                Mechanism_Acceleration()
                Mechanism_AccelerationDraw(i)
            Else
                ' MechanismDraw(2)
            End If



        Next

        'shatun.Mechanism_Speed()

    End Sub


    Public Sub MechanismConstruction()

        mp_O = New PointDouble(130, 310)

        meh_Mechanism = New cls_SheetView("mehanizm", mp_O.X, mp_O.Y, , mashtab_ml_view)

        doc2D.AddView(meh_Mechanism)

        meh_Mechanism.Create()
        meh_Mechanism.Active()

        ' Для определения крайнего положения механизма в точке C0

        mp_C0 = doc2D.myCalculateCordinatePointCircleCircle(mp_A, L_AB + L_BC, mp_D, L_CD)
        Dim line_CordinateLineC As New PointDouble(0, mp_A.Y)

        ' Определяем угол для крайнего положения механизма из точки A и точки C0
        Dim angle_AC0 As Double = doc2D.myCalculateAngleTwoPoint(mp_A, mp_C0)

        ' Нахождение координаты точки B
        ' Определяем координату точки B на плане для положения, которое задано в переменной numberPosition
        'Dim numberPosition As Integer = 2

        ' Шаг перемещения механизма
        Dim step_ As Double = 45
        Dim angle_AB As Double = angle_AC0 + (numberPosition * step_)

        mp_B = doc2D.myCalculateCordinatePoint(mp_A, L_AB, angle_AB)
        'doc2D.myDrawPoint(mp_B)
        'doc2D.myDrawLineSeg(mp_A, mp_B)

        mp_C = doc2D.myCalculateCordinatePointCircleCircle(mp_B, L_BC, mp_D, L_CD)
        'doc2D.myDrawPoint(mp_C)

        Dim angle_BC As Double = doc2D.myCalculateAngleTwoPoint(mp_B, mp_C)
        mp_E = doc2D.myCalculateCordinatePoint(mp_C, L_CE, angle_BC)
        'doc2D.myDrawPoint(mp_E)

        'doc2D.myDrawLineSeg(mp_A, mp_B)
        'doc2D.myDrawLineSeg(mp_B, mp_C)
        'doc2D.myDrawLineSeg(mp_C, mp_D)
        'doc2D.myDrawLineSeg(mp_C, mp_E)

        'doc2D.myDrawText(mp_A, mp_A.ToStringScale(mashtab_ml))
        'doc2D.myDrawText(mp_B, mp_B.ToStringScale(mashtab_ml))
        'doc2D.myDrawText(mp_C, mp_C.ToStringScale(mashtab_ml))
        'doc2D.myDrawText(mp_D, mp_D.ToStringScale(mashtab_ml))
        'doc2D.myDrawText(mp_E, mp_E.ToStringScale(mashtab_ml))

        'doc2D.myDrawLineSeg(mp_A, mp_D, 4)

    End Sub

    Public Sub MechanismConstruction(numberPosition As Integer)

        mp_O = New PointDouble(130, 310)

        meh_Mechanism = New cls_SheetView("Mechanism " & numberPosition, mp_O.X, mp_O.Y, , mashtab_ml_view)

        doc2D.AddView(meh_Mechanism)

        meh_Mechanism.Create()
        meh_Mechanism.Active()

        ' Для определения крайнего положения механизма в точке C0

        mp_C0 = doc2D.myCalculateCordinatePointCircleCircle(mp_A, L_AB + L_BC, mp_D, L_CD)
        Dim line_CordinateLineC As New PointDouble(0, mp_A.Y)

        ' Определяем угол для крайнего положения механизма из точки A и точки C0
        Dim angle_AC0 As Double = doc2D.myCalculateAngleTwoPoint(mp_A, mp_C0)

        ' Нахождение координаты точки B
        ' Определяем координату точки B на плане для положения, которое задано в переменной numberPosition
        'Dim numberPosition As Integer = 2

        ' Шаг перемещения механизма
        Dim step_ As Double = 45
        Dim angle_AB As Double = angle_AC0 + (numberPosition * step_)

        mp_B = doc2D.myCalculateCordinatePoint(mp_A, L_AB, angle_AB)
        'doc2D.myDrawPoint(mp_B)
        'doc2D.myDrawLineSeg(mp_A, mp_B)

        mp_C = doc2D.myCalculateCordinatePointCircleCircle(mp_B, L_BC, mp_D, L_CD)
        'doc2D.myDrawPoint(mp_C)

        Dim angle_BC As Double = doc2D.myCalculateAngleTwoPoint(mp_B, mp_C)
        mp_E = doc2D.myCalculateCordinatePoint(mp_C, L_CE, angle_BC)

    End Sub

    Public Sub MechanismDraw(numberPosition As Integer, style As Integer)

        'doc2D.myDrawCrossingLines(mp_A)
        'doc2D.myDrawPoint(mp_A)

        'doc2D.myDrawCrossingLines(mp_D)
        'doc2D.myDrawPoint(mp_D)

        If numberPosition = 0 Then
            doc2D.myDrawPoint(mp_A, enStylePoint.Circle)
            doc2D.myDrawPoint(mp_D, enStylePoint.Circle)

            doc2D.myDrawTextIndex(mp_A, "A")
            doc2D.myDrawTextIndex(mp_D, "D")

            doc2D.myDrawLineSeg(mp_A, mp_D, 4)

        End If

        doc2D.myDrawPoint(mp_B, enStylePoint.Circle)
        doc2D.myDrawPoint(mp_C, enStylePoint.Circle)

        doc2D.myDrawPoint(mp_E, enStylePoint.Circle)

        doc2D.myDrawLineSeg(mp_A, mp_B, style)
        doc2D.myDrawLineSeg(mp_B, mp_C, style)
        doc2D.myDrawLineSeg(mp_C, mp_D, style)
        doc2D.myDrawLineSeg(mp_C, mp_E, style)


        doc2D.myDrawTextIndex(mp_B, "B", "", numberPosition.ToString())
        doc2D.myDrawTextIndex(mp_C, "C", "", numberPosition.ToString())

        doc2D.myDrawTextIndex(mp_E, "E", "", numberPosition.ToString())



    End Sub

    Public Sub Mechanism_Speed(numberPosition As Integer)


        ' Создание вида для 2-положения
        meh_Speed = New cls_SheetView("v " & numberPosition, 200 + numberPosition * 100, 200, , mashtab_mv_view)

        doc2D.AddView(meh_Speed)
        'v3.Create(doc2D) 
        ' Делаем вид активным
        meh_Speed.Create()
        meh_Speed.Active()

        ' Задаем начальную точку P для построения плана скоростей
        ' в данном механизме точка D будет совпадать с полюсом
        'vp_P = New PointDouble(0, 0)
        'vp_d = New PointDouble(0, 0)

        ' Определяем угол Pb - Функцией. Принимаем параметры угол между точкой А и B + 90 (перпендикуляр AB)
        Dim angle_Pb As Double = doc2D.myCalculateAngleTwoPoint(mp_A, mp_B) + 90


        ' Находим точку b на плане скоростей согласно принимаемых данных: точка P, Длина 70 и угол в переменной angle_Pb
        vp_b = doc2D.myCalculateCordinatePoint(vp_P, v_b, angle_Pb)

        ' Рисуем вектор Pb
        'doc2D.myDrawVector(vp_P, vp_b)

        ' Определение точки с на плане скоростей на пересечении перпендикуляров точок b и P плана скоростей 
        ' соответственно своих ланок на плане механизма
        Dim angle_bc As Double = doc2D.myCalculateAngleTwoPoint(mp_B, mp_C) + 90
        Dim angle_dc As Double = doc2D.myCalculateAngleTwoPoint(mp_D, mp_C) + 90

        vp_c = doc2D.myCalculateCordinatePoint(vp_b, angle_bc, vp_P, angle_dc)
        vl_cd = doc2D.myCalculateLenghtTwoPoint(vp_d, vp_c)

        ' Рисуем вектор Pb
        'doc2D.myDrawVector(vp_b, vp_c)
        'doc2D.myDrawVector(vp_P, vp_c)

        ' Определение длины bc на плане скоростей
        v_bc = doc2D.myCalculateLenghtTwoPoint(vp_b, vp_c)

        ' Определение длины ce на плане скоростей  (математика все длины есть )
        v_ce = L_CE / L_BC * v_bc

        ' Нахождение координаты точки е на плане скоростей
        ' входные параметры: точка с, длина се и угол Pb
        vp_e = doc2D.myCalculateCordinatePoint(vp_c, v_ce, angle_bc)

        ' Определяем угол направления вектора bc по точкам b и c

        Dim angle_ce = doc2D.myCalculateAngleTwoPoint(vp_b, vp_c)

        vp_e = doc2D.myCalculateCordinatePoint(vp_c, v_ce, angle_ce)

        ' Рисуем вектор ce
        'doc2D.myDrawVector(vp_c, vp_e)

        ' Соединяем с полюсом 
        'doc2D.myDrawVector(vp_P, vp_e)

        ' Определения координат s1, s2, s3, s4, s5
        vp_s2 = doc2D.myCalculateCordinateCentreSeg(vp_P, vp_b)
        vp_s3 = doc2D.myCalculateCordinateCentreSeg(vp_b, vp_e)
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
        'doc2D.myDrawText(vp_P, "P, d")
        'doc2D.myDrawText(vp_b, "b")
        'doc2D.myDrawText(vp_c, "c")
        'doc2D.myDrawText(vp_e, "e")

        'doc2D.myDrawText(vp_s2, "s2")
        'doc2D.myDrawText(vp_s3, "s3")
        'doc2D.myDrawText(vp_s4, "s4")


        'meh_Speed.ExitView()
    End Sub

    Public Sub Mechanism_SpeedDraw(numberPosition As Integer)

        doc2D.myDrawNumberMacro(vp_P, numberPosition.ToString(), meh_Speed.ScaleView)

        ' Рисуем вектор Pb
        doc2D.myDrawVector(vp_P, vp_b)


        ' Рисуем вектор c
        doc2D.myDrawVector(vp_b, vp_c)
        doc2D.myDrawVector(vp_P, vp_c)


        ' Рисуем вектор ce
        doc2D.myDrawVector(vp_c, vp_e)

        ' Соединяем с полюсом 
        doc2D.myDrawVector(vp_P, vp_e)

        ' Рисуем вектора для s2, s3, s4
        doc2D.myDrawVector(vp_P, vp_s2)
        doc2D.myDrawVector(vp_P, vp_s3)
        doc2D.myDrawVector(vp_P, vp_s4)

        ' Простановка всех точек на плане скоростей
        doc2D.myDrawText(vp_P, "Pv, d")
        doc2D.myDrawText(vp_b, "b")
        doc2D.myDrawText(vp_c, "c")
        doc2D.myDrawText(vp_e, "e")

        doc2D.myDrawText(vp_s2, "s2")
        doc2D.myDrawText(vp_s3, "s3")
        doc2D.myDrawText(vp_s4, "s4")


        meh_Speed.ExitView()
    End Sub

    Public Sub Mechanism_Speed_(numberPosition As Integer)

        ' Создание вида для 2-положения
        meh_Speed = New cls_SheetView("v " & numberPosition, 200 + (100 * numberPosition), 200, , mashtab_mv_view)

        doc2D.AddView(meh_Speed)
        'v3.Create(doc2D) 
        ' Делаем вид активным
        meh_Speed.Create()
        meh_Speed.Active()

        ' Задаем начальную точку P для построения плана скоростей
        ' в данном механизме точка D будет совпадать с полюсом
        'vp_P = New PointDouble(0, 0)
        'vp_d = New PointDouble(0, 0)

        ' Определяем угол Pb - Функцией. Принимаем параметры угол между точкой А и B + 90 (перпендикуляр AB)
        Dim angle_Pb As Double = doc2D.myCalculateAngleTwoPoint(mp_A, mp_B) + 90

        ' Находим точку b на плане скоростей согласно принимаемых данных: точка P, Длина 70 и угол в переменной angle_Pb
        vp_b = doc2D.myCalculateCordinatePoint(vp_P, v_b, angle_Pb)

        ' Рисуем вектор Pb
        'doc2D.myDrawVector(vp_P, vp_b)

        ' Определение точки с на плане скоростей на пересечении перпендикуляров точок b и P плана скоростей 
        ' соответственно своих ланок на плане механизма
        Dim angle_bc As Double = doc2D.myCalculateAngleTwoPoint(mp_B, mp_C) + 90
        Dim angle_dc As Double = doc2D.myCalculateAngleTwoPoint(mp_D, mp_C) + 90

        vp_c = doc2D.myCalculateCordinatePoint(vp_b, angle_bc, vp_P, angle_dc)
        vl_cd = doc2D.myCalculateLenghtTwoPoint(vp_d, vp_c)
        cd_v(numberPosition) = vl_cd

        ' Рисуем вектор Pb
        'doc2D.myDrawVector(vp_b, vp_c)
        'doc2D.myDrawVector(vp_P, vp_c)

        ' Определение длины bc на плане скоростей
        v_bc = doc2D.myCalculateLenghtTwoPoint(vp_b, vp_c)

        ' Определение длины ce на плане скоростей  (математика все длины есть )
        v_ce = L_CE / L_BC * v_bc

        ' Нахождение координаты точки е на плане скоростей
        ' входные параметры: точка с, длина се и угол Pb
        vp_e = doc2D.myCalculateCordinatePoint(vp_c, v_ce, angle_bc)

        ' Определяем угол направления вектора bc по точкам b и c

        Dim angle_ce = doc2D.myCalculateAngleTwoPoint(vp_b, vp_c)

        vp_e = doc2D.myCalculateCordinatePoint(vp_c, v_ce, angle_ce)

        ' Рисуем вектор ce
        'doc2D.myDrawVector(vp_c, vp_e)

        ' Соединяем с полюсом 
        'doc2D.myDrawVector(vp_P, vp_e)

        ' Определения координат s1, s2, s3, s4, s5
        vp_s2 = doc2D.myCalculateCordinateCentreSeg(vp_P, vp_b)
        vp_s3 = doc2D.myCalculateCordinateCentreSeg(vp_b, vp_e)
        vp_s4 = doc2D.myCalculateCordinateCentreSeg(vp_P, vp_c)

        ' Рисуем вектора для s2, s3, s4
        'doc2D.myDrawVector(vp_P, vp_s2)
        'doc2D.myDrawVector(vp_P, vp_s3)
        'doc2D.myDrawVector(vp_P, vp_s4)

        ' Простановка всех точек на плане скоростей
        'doc2D.myDrawText(vp_P, "P, d")
        'doc2D.myDrawText(vp_b, "b")
        'doc2D.myDrawText(vp_c, "c")
        'doc2D.myDrawText(vp_e, "e")

        'doc2D.myDrawText(vp_s2, "s2")
        'doc2D.myDrawText(vp_s3, "s3")
        'doc2D.myDrawText(vp_s4, "s4")


        'meh_Speed.ExitView()
    End Sub

    ''' <summary>
    ''' Работа ускорения
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Mechanism_Acceleration()


        ' Создание вида 
        meh_Acceleration = New cls_SheetView("a", 300, 200, , mashtab_ma_view)

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



        '===================================================================
        ' ------ 3 --------

        a_n3 = ((v_bc) ^ 2) / L_BC
        Dim angleCB As Double = doc2D.myCalculateAngleTwoPoint(mp_C, mp_B)

        ap_n3 = doc2D.myCalculateCordinatePoint(ap_b, a_n3, angleCB)

        Dim absoluteLenght_bn3 As Double = doc2D.myAbsoluteLenght(meh_Acceleration, a_n3)

        ' -----------------
        a_n4 = (vl_cd ^ 2) / L_CD
        Dim angleCD As Double = doc2D.myCalculateAngleTwoPoint(mp_C, mp_D)

        ap_n4 = doc2D.myCalculateCordinatePoint(ap_pi, a_n4, angleCD)

        Dim absoluteLenght_pn4 As Double = doc2D.myAbsoluteLenght(meh_Acceleration, a_n4)



        '------------------

        ' Определяем точку ap_c 
        ' Строится из точки ap_n3 перпендикуляр угла вектора al_bn3 и
        ' и из точки ap_P ось по X

        ap_c = doc2D.myCalculateCordinatePoint(ap_n3, angleCB + 90, ap_n4, angleCD + 90)


        a_bc = doc2D.myCalculateLenghtTwoPoint(ap_b, ap_c)
        bc_a(numberPosition) = a_bc

        a_cd = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_c)
        bc_a(numberPosition) = a_cd

        ' Определяем точку длину al_cd для определения координаты точки ap_d и угол сd
        a_ce = L_CE / L_BC * a_bc

        Dim angle_ce As Double = doc2D.myCalculateAngleTwoPoint(ap_b, ap_c)

        ap_e = doc2D.myCalculateCordinatePoint(ap_c, a_ce, angle_ce)

        ' Соединяем с полюсом 
        'doc2D.myDrawVector(ap_P, ap_d)

        ' Определения координат s1, s2, s3, s4, s5
        ap_s2 = doc2D.myCalculateCordinateCentreSeg(ap_pi, ap_b)
        ps2_a(numberPosition) = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_s2)

        ap_s3 = doc2D.myCalculateCordinateCentreSeg(ap_b, ap_e)
        ps3_a(numberPosition) = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_s3)

        ' s4 определяется в данном случае в точке c
        ap_s4 = doc2D.myCalculateCordinateCentreSeg(ap_pi, ap_c)
        ps4_a(numberPosition) = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_s4)

        ' Рисуем и определяем вектора для s2, s3, s4 

        'meh_Acceleration.ExitView()

        'meh_Acceleration.ExitView()




    End Sub

    Public Sub Mechanism_AccelerationDraw(numberPosition As Integer)

        doc2D.myDrawNumberMacro(ap_pi, numberPosition.ToString(), meh_Acceleration.ScaleView)

        ' Рисуем вектор Pb
        doc2D.myDrawVector(ap_pi, ap_b)

        Dim absoluteLenght_bn3 As Double = doc2D.myAbsoluteLenght(meh_Acceleration, a_n3)

        If absoluteLenght_bn3 < 7 Then
            doc2D.myDrawLineSeg(ap_b, ap_n3, 2)
        Else
            doc2D.myDrawVector(ap_b, ap_n3)
        End If

        Dim absoluteLenght_pn4 As Double = doc2D.myAbsoluteLenght(meh_Acceleration, a_n4)

        If absoluteLenght_pn4 < 7 Then
            doc2D.myDrawLineSeg(ap_pi, ap_n4, 2)
        Else
            doc2D.myDrawVector(ap_pi, ap_n4)
        End If

        '------------------
        ' Определяем точку ap_c 
        ' Строится из точки ap_n3 перпендикуляр угла вектора al_bn3 и
        ' и из точки ap_P ось по X

        doc2D.myDrawVector(ap_n3, ap_c)
        doc2D.myDrawVector(ap_n4, ap_c)

        doc2D.myDrawVector(ap_pi, ap_c)
        doc2D.myDrawVector(ap_b, ap_c)

        doc2D.myDrawVector(ap_pi, ap_e)

        doc2D.myDrawVector(ap_pi, ap_n3)
        doc2D.myDrawVector(ap_pi, ap_n4)


        doc2D.myDrawLineSeg(ap_c, ap_e, 2)

        ' Рисуем вектора для s2, s3, s4
        doc2D.myDrawVector(ap_pi, ap_s2)
        doc2D.myDrawVector(ap_pi, ap_s3)
        doc2D.myDrawVector(ap_pi, ap_s4)

        ' Простановка всех точек на плане ускорений скоростей
        doc2D.myDrawText(ap_pi, "Pa, d")
        doc2D.myDrawText(ap_b, "b")
        doc2D.myDrawText(ap_c, "c")
        doc2D.myDrawText(ap_e, "e")

        doc2D.myDrawText(ap_s2, "s2")
        doc2D.myDrawText(ap_s3, "s3")
        doc2D.myDrawText(ap_s4, "s4")

        meh_Acceleration.ExitView()

    End Sub

    Public Sub AbsoluteWork()
        Dim lenTest1 As Double = doc2D.myAbsoluteLenghtTwoPoint(meh_Mechanism, mp_A, mp_B)
        Dim newPoint1 As PointDouble = doc2D.myAbsoluteCordinatePoint(meh_Mechanism, mp_C)

        Dim lenTest2 As Double = doc2D.myAbsoluteLenghtTwoPoint(meh_Speed, vp_P, vp_b)
        Dim newPoint2 As PointDouble = doc2D.myAbsoluteCordinatePoint(meh_Speed, vp_P)
    End Sub

    Public Sub Mechanism_Acceleration_(numberPosition As Integer)

        ' Создание вида 
        meh_Acceleration = New cls_SheetView("a", 300, 200, , mashtab_ma_view)

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
        doc2D.myDrawVector(ap_pi, ap_b)

        '===================================================================
        ' ------ 3 --------

        a_n3 = ((v_bc) ^ 2) / L_BC
        Dim angleCB As Double = doc2D.myCalculateAngleTwoPoint(mp_C, mp_B)

        ap_n3 = doc2D.myCalculateCordinatePoint(ap_b, a_n3, angleCB)

        Dim absoluteLenght_bn3 As Double = doc2D.myAbsoluteLenght(meh_Acceleration, a_n3)

        If absoluteLenght_bn3 < 7 Then
            doc2D.myDrawLineSeg(ap_b, ap_n3, 2)
        Else
            doc2D.myDrawVector(ap_b, ap_n3)
        End If

        ' -----------------
        Dim al_pn4 As Double = (vl_cd ^ 2) / L_CD
        Dim angleCD As Double = doc2D.myCalculateAngleTwoPoint(mp_C, mp_D)

        Dim ap_n4 As PointDouble = doc2D.myCalculateCordinatePoint(ap_pi, al_pn4, angleCD)

        Dim absoluteLenght_pn4 As Double = doc2D.myAbsoluteLenght(meh_Acceleration, al_pn4)

        If absoluteLenght_pn4 < 7 Then
            doc2D.myDrawLineSeg(ap_pi, ap_n4, 2)
        Else
            doc2D.myDrawVector(ap_pi, ap_n4)
        End If

        '------------------

        ' Определяем точку ap_c 
        ' Строится из точки ap_n3 перпендикуляр угла вектора al_bn3 и
        ' и из точки ap_P ось по X

        ap_c = doc2D.myCalculateCordinatePoint(ap_n3, angleCB + 90, ap_n4, angleCD + 90)
        doc2D.myDrawVector(ap_n3, ap_c)
        doc2D.myDrawVector(ap_n4, ap_c)

        doc2D.myDrawVector(ap_pi, ap_c)
        doc2D.myDrawVector(ap_b, ap_c)

        a_bc = doc2D.myCalculateLenghtTwoPoint(ap_b, ap_c)
        bc_a(numberPosition) = a_bc

        ' Определяем точку длину al_cd для определения координаты точки ap_d и угол сd
        a_ce = L_CE / L_BC * a_bc

        Dim angle_ce As Double = doc2D.myCalculateAngleTwoPoint(ap_b, ap_c)

        ap_e = doc2D.myCalculateCordinatePoint(ap_c, a_ce, angle_ce)
        doc2D.myDrawVector(ap_pi, ap_e)

        doc2D.myDrawLineSeg(ap_c, ap_e, 2)

        ' Соединяем с полюсом 
        'doc2D.myDrawVector(ap_P, ap_d)

        ' Определения координат s1, s2, s3, s4, s5
        ap_s2 = doc2D.myCalculateCordinateCentreSeg(ap_pi, ap_b)
        ap_s3 = doc2D.myCalculateCordinateCentreSeg(ap_b, ap_e)
        ' s4 определяется в данном случае в точке c
        ap_s4 = doc2D.myCalculateCordinateCentreSeg(ap_pi, ap_c)


        ' Рисуем и определяем вектора для s2, s3, s4 
        doc2D.myDrawVector(ap_pi, ap_s2)
        ps2_a(numberPosition) = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_s2)

        doc2D.myDrawVector(ap_pi, ap_s3)
        ps3_a(numberPosition) = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_s3)

        doc2D.myDrawVector(ap_pi, ap_s4)
        ps4_a(numberPosition) = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_s4)

        ' Простановка всех точек на плане скоростей
        doc2D.myDrawText(ap_pi, "P, d")
        doc2D.myDrawText(ap_b, "b")
        doc2D.myDrawText(ap_c, "c")
        doc2D.myDrawText(ap_e, "e")

        doc2D.myDrawText(ap_s2, "s2")
        doc2D.myDrawText(ap_s3, "s3")
        doc2D.myDrawText(ap_s4, "s4")


        meh_Acceleration.ExitView()

    End Sub

End Class
