Imports Kompas6API5, Kompas6Constants
Imports TMM.myDoc2D
Imports System.Data.SQLite
Imports DB

Partial Public Class cls_Shatun_1

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub RunKompas()
        doc2D = New myDoc2D(ActiveDoc2D, "ШаблонЧертежа")
    End Sub

    Public Sub Run(k As Integer, doc2D As myDoc2D)
        'doc2D = New myDoc2D(ActiveDoc2D, "ШаблонЧертежа")

        If doc2D Is Nothing Then Throw New Exception("Приложение компас не запущено")

        'Dim k = drDadatok2("position_m")



        MechanismConstruction(k)
        Dim dr_Mehanizm = StoredProcedure.RowZD(Table.Z1_Mehanizm, counter, k)
        MechanismDraw(dr_Mehanizm, 1)

        Mechanism_Speed(k)
        Dim dr_Speed = StoredProcedure.RowZD(Table.Z1_Speed, counter, k)
        Mechanism_SpeedDraw(dr_Speed)

        Mechanism_Acceleration(k)
        Dim dr_Acceleration = StoredProcedure.RowZD(Table.Z1_Acceleration, counter, k)
        Mechanism_AccelerationDraw(dr_Acceleration)

        'Mechanism_Speed(k)
        'Mechanism_SpeedDraw(k)

        'Mechanism_Acceleration(5)
        'Mechanism_AccelerationDraw(k)

    End Sub

    Public Sub CreateDataBase()

        doc2D = New myDoc2D(ActiveDoc2D, "ШаблонЧертежа")

        'If doc2D Is Nothing Then Throw New Exception("Приложение компас не запущено")

        'Dim k = drDadatok2("position_m")

        For i As Integer = 0 To 7
            DataBase.BeginTransaction()

            MechanismConstruction(i)
            Mechanism_Speed(i)
            Mechanism_Acceleration(i)

            DataBase.TransactionCommit()
        Next

        DataBase.UpdateDataBase()
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

        DataBase.LoadDataAdapterAndDataTable(TableName.z1_mehanizm, da_Mehanizm, dt_Mehanizm)

        'da_Mehanizm.SelectCommand.Transaction = tran

        Dim dr_Mehanizm As DataRow = dt_Mehanizm.NewRow()
        dr_Mehanizm("id_zd") = counter

        'ml_AB = 50
        dr_Mehanizm("ml_AB") = ml_AB '36

        'Определяем масштаб механизма
        mashtab_ml = L_AB / ml_AB
        dr_Mehanizm("mashtab_ml") = mashtab_ml

        ' Масштаб механизма для конкретного вида на чертеже
        mashtab_ml_view = ml_AB / L_AB
        dr_Mehanizm("mashtab_ml_view") = mashtab_ml_view

        ' Длины механизма в масштабе
        ml_AD = L_AD / mashtab_ml
        dr_Mehanizm("ml_AD") = ml_AD

        ml_CD = L_CD / mashtab_ml
        dr_Mehanizm("ml_CD") = ml_CD

        ml_BC = L_BC / mashtab_ml
        dr_Mehanizm("ml_BC") = ml_BC

        ml_BE = L_BE / mashtab_ml
        dr_Mehanizm("ml_BE") = ml_BE

        ml_CE = L_CE / mashtab_ml
        dr_Mehanizm("ml_CE") = ml_CE


        mp_A = New PointDouble(0, 0)
        dr_Mehanizm("mp_A") = mp_A.ToStringPoint()

        mp_D = New PointDouble(0.28, 0)
        dr_Mehanizm("mp_D") = mp_D.ToStringPoint()


        '===================================================================

        'mp_O = New PointDouble(130, 310)
        'meh_Mechanism = New cls_SheetView("Mechanism " & numberPosition, mp_O.X, mp_O.Y, , mashtab_ml_view)

        'doc2D.AddView(meh_Mechanism)

        'meh_Mechanism.Create()
        'meh_Mechanism.Active()

        ' Для определения крайнего положения механизма в точке C0
        mp_C0 = doc2D.myCalculateCordinatePointCircleCircle(mp_A, L_AB + L_BC, mp_D, L_CD)
        'Dim line_CordinateLineC As New PointDouble(0, mp_A.Y)

        ' Определяем угол для крайнего положения механизма из точки A и точки C0
        Dim angle_AC0 As Double = doc2D.myCalculateAngleTwoPoint(mp_A, mp_C0)

        ' Нахождение координаты точки B
        ' Определяем координату точки B на плане для положения, которое задано в переменной numberPosition
        'Dim numberPosition As Integer = 2

        ' Шаг перемещения механизма
        Dim step_ As Double = 45 + 270
        Dim angle_AB As Double = angle_AC0 + (numberPosition * step_)
        dr_Mehanizm("numberPosition") = numberPosition

        Do While angle_AB > 360
            angle_AB -= 360
        Loop

        dr_Mehanizm("angle_AB") = angle_AB

        'doc2D.myDrawPoint(mp_B)
        'doc2D.myDrawLineSeg(mp_A, mp_B)
        mp_B = doc2D.myCalculateCordinatePoint(mp_A, L_AB, angle_AB)
        dr_Mehanizm("mp_B") = mp_B.ToStringPoint()

        dr_Mehanizm("mp_D") = mp_D.ToStringPoint()

        mp_C = doc2D.myCalculateCordinatePointCircleCircle(mp_B, L_BC, mp_D, L_CD)
        dr_Mehanizm("mp_C") = mp_C.ToStringPoint()

        Dim angle_BC As Double = doc2D.myCalculateAngleTwoPoint(mp_B, mp_C)
        dr_Mehanizm("angle_BC") = angle_BC

        Dim angle_DC As Double = doc2D.myCalculateAngleTwoPoint(mp_D, mp_C)
        dr_Mehanizm("angle_DC") = angle_DC

        Dim angle_BE As Double = doc2D.myCalculateAngleTwoPoint(mp_C, mp_B)
        mp_E = doc2D.myCalculateCordinatePoint(mp_B, L_BE, angle_BE)
        dr_Mehanizm("mp_E") = mp_E.ToStringPoint()

        mp_S2 = doc2D.myCalculateCordinateCentreSeg(mp_A, mp_B)
        dr_Mehanizm("mp_S2") = mp_S2.ToStringPoint()

        mp_S3 = doc2D.myCalculateCordinateCentreSeg(mp_C, mp_E)
        dr_Mehanizm("mp_S3") = mp_S3.ToStringPoint()

        mp_S4 = doc2D.myCalculateCordinateCentreSeg(mp_D, mp_C)
        dr_Mehanizm("mp_S4") = mp_S4.ToStringPoint()

        dt_Mehanizm.Rows.Add(dr_Mehanizm)

        da_Mehanizm.Update(dt_Mehanizm)
    End Sub

    Public Sub MechanismDraw(rowMechanism As DataRow, style As Integer)
        'Dim doc2D As New myDoc2D(doc2D)

        numberPosition = rowMechanism("numberPosition")
        mashtab_ml_view = rowMechanism("mashtab_ml_view")

        mp_A = StringToPointDouble(rowMechanism("mp_A"))
        mp_B = StringToPointDouble(rowMechanism("mp_B"))
        mp_C = StringToPointDouble(rowMechanism("mp_C"))
        mp_D = StringToPointDouble(rowMechanism("mp_D"))
        mp_E = StringToPointDouble(rowMechanism("mp_E"))

        mp_S2 = StringToPointDouble(rowMechanism("mp_S2"))
        mp_S3 = StringToPointDouble(rowMechanism("mp_S3"))
        mp_S4 = StringToPointDouble(rowMechanism("mp_S4"))

        mp_O = New PointDouble(110, 310)

        meh_Mechanism = New cls_SheetView("Mechanism " & numberPosition, 110, 310, , mashtab_ml_view)

        doc2D.AddView(meh_Mechanism)

        meh_Mechanism.Create()
        meh_Mechanism.Active()

        Dim height As Double = 4

        'If numberPosition = 0 Then
        doc2D.myDrawCrossingLines(mp_A)

        doc2D.myDrawPoint(mp_A, enStylePoint.Circle)
        doc2D.myDrawTextIndex(mp_A, "A", , , height)

        doc2D.myDrawPoint(mp_D, enStylePoint.Circle)
        doc2D.myDrawTextIndex(mp_D, "D", , , height)

        'End If

        doc2D.myDrawPoint(mp_B, enStylePoint.Circle)
        doc2D.myDrawPoint(mp_C, enStylePoint.Circle)
        doc2D.myDrawPoint(mp_D, enStylePoint.Circle)
        doc2D.myDrawPoint(mp_E, enStylePoint.Circle)

        doc2D.myDrawLineSeg(mp_A, mp_B, style)
        doc2D.myDrawLineSeg(mp_D, mp_C, style)
        doc2D.myDrawLineSeg(mp_B, mp_C, style)
        doc2D.myDrawLineSeg(mp_B, mp_E, style)


        doc2D.myDrawTextIndex(mp_B, "B", "", numberPosition.ToString(), height)
        doc2D.myDrawTextIndex(mp_C, "C", "", numberPosition.ToString(), height)
        'doc2D.myDrawTextIndex(mp_D, "D", "", numberPosition.ToString(), height)
        doc2D.myDrawTextIndex(mp_E, "E", "", numberPosition.ToString(), height)

        If style = 1 Then
            doc2D.myDrawPoint(mp_S2, enStylePoint.Point)
            doc2D.myDrawTextIndex(mp_S2, "S2", "", numberPosition.ToString(), height)

            doc2D.myDrawPoint(mp_S3, enStylePoint.Point)
            doc2D.myDrawTextIndex(mp_S3, "S3", "", numberPosition.ToString(), height)

            doc2D.myDrawPoint(mp_S4, enStylePoint.Point)
            doc2D.myDrawTextIndex(mp_S4, "S4", "", numberPosition.ToString(), height)
        End If

        meh_Mechanism.ExitView()
    End Sub

    Public Sub MechanismDraw_(numberPosition As Integer, style As Integer)

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

        DataBase.LoadDataAdapterAndDataTable(TableName.z1_speed, da_Speed, dt_Speed)
        Dim dr_Speed As DataRow = dt_Speed.NewRow()

        dr_Speed("id_zd") = counter
        dr_Speed("numberPosition") = numberPosition

        'vl_pb = 70 ' на чертеже
        dr_Speed("vl_pb") = vl_pb


        ' Определение скорости vb
        v_b = w2 * L_AB
        dr_Speed("v_b") = v_b

        'Определяем масштаб механизма для скоростей
        mashtab_mv = v_b / vl_pb
        dr_Speed("mashtab_mv") = mashtab_mv

        ' Масштаб для конкретного вида на чертеже для скоростей
        mashtab_mv_view = vl_pb / v_b
        dr_Speed("mashtab_mv_view") = mashtab_mv_view

        ' Задаем начальную точку P и d для построения плана скоростей
        vp_P = New PointDouble(0, 0)
        dr_Speed("vp_P") = vp_P.ToStringPoint()

        vp_d = New PointDouble(L_AD, 0)
        dr_Speed("vp_d") = vp_d.ToStringPoint()

        ' Определяем угол Pb - Функцией. Принимаем параметры угол между точкой А и B + 90 (перпендикуляр AB)
        Dim angle_Pb As Double = doc2D.myCalculateAngleTwoPoint(mp_A, mp_B, enAngle.Angle270)
        dr_Speed("angle_Pb") = angle_Pb

        ' Находим точку b на плане скоростей согласно принимаемых данных: точка P, Длина 70 и угол в переменной angle_Pb
        vp_b = doc2D.myCalculateCordinatePoint(vp_P, v_b, angle_Pb)
        dr_Speed("vp_b") = vp_b.ToStringPoint()

        ' Рисуем вектор Pb
        'doc2D.myDrawVector(vp_P, vp_b)

        ' Определение точки с на плане скоростей на пересечении перпендикуляров точок b и P плана скоростей 
        ' соответственно своих ланок на плане механизма
        Dim angle_bc As Double = doc2D.myCalculateAngleTwoPoint(mp_B, mp_C, enAngle.Angle270)
        dr_Speed("angle_bc") = angle_bc
        Dim angle_dc As Double = doc2D.myCalculateAngleTwoPoint(mp_D, mp_C, enAngle.Angle270)
        dr_Speed("angle_dc") = angle_dc

        vp_c = doc2D.myCalculateCordinatePoint(vp_b, angle_bc, vp_P, angle_dc)
        dr_Speed("vp_c") = vp_c.ToStringPoint()

        v_c = doc2D.myCalculateLenghtTwoPoint(vp_P, vp_c)
        vl_c = v_c / mashtab_mv
        dr_Speed("v_c") = v_c
        dr_Speed("vl_c") = vl_c

        v_dc = v_c
        vl_dc = vl_c
        dr_Speed("v_dc") = v_dc
        dr_Speed("vl_dc") = vl_dc

        ' Определение длины bc на плане скоростей
        v_bc = doc2D.myCalculateLenghtTwoPoint(vp_b, vp_c)
        vl_bc = v_bc / mashtab_mv
        dr_Speed("v_bc") = v_bc
        dr_Speed("vl_bc") = vl_bc

        ' Определение длины ce на плане скоростей  (математика все длины есть )
        v_be = (v_bc / L_BC) * L_BE
        vl_be = v_be / mashtab_mv
        dr_Speed("v_be") = v_be
        dr_Speed("vl_be") = vl_be

        ' Нахождение координаты точки е на плане скоростей
        ' входные параметры: точка с, длина се и угол Pb
        Dim angle_be = doc2D.myCalculateAngleTwoPoint(vp_c, vp_b)
        vp_e = doc2D.myCalculateCordinatePoint(vp_b, v_be, angle_be)
        dr_Speed("vp_e") = vp_e.ToStringPoint()

        v_e = doc2D.myCalculateLenghtTwoPoint(vp_P, vp_e)
        vl_e = v_c / mashtab_mv
        dr_Speed("v_e") = v_e
        dr_Speed("vl_e") = vl_e

        ' Определения координат s1, s2, s3, s4, s5
        vp_s2 = doc2D.myCalculateCordinateCentreSeg(vp_P, vp_b)
        dr_Speed("vp_s2") = vp_s2.ToStringPoint()

        vp_s3 = doc2D.myCalculateCordinateCentreSeg(vp_c, vp_e)
        dr_Speed("vp_s3") = vp_s3.ToStringPoint()

        vp_s4 = doc2D.myCalculateCordinateCentreSeg(vp_P, vp_c)
        dr_Speed("vp_s4") = vp_s4.ToStringPoint()

        ' Вычисление проекции на ось силы тяжести и угла вектора для МЗВ

        '/ mashtab_mv

        v_s2 = doc2D.myCalculateLenghtTwoPoint(vp_P, vp_s2)
        vl_s2 = v_s2 / mashtab_mv
        dr_Speed("v_s2") = v_s2
        dr_Speed("vl_s2") = vl_s2

        v_s3 = doc2D.myCalculateLenghtTwoPoint(vp_P, vp_s3)
        vl_s3 = v_s3 / mashtab_mv
        dr_Speed("v_s3") = v_s3
        dr_Speed("vl_s3") = vl_s3

        v_s4 = doc2D.myCalculateLenghtTwoPoint(vp_P, vp_s4)
        vl_s4 = v_s4 / mashtab_mv
        dr_Speed("v_s4") = v_s4
        dr_Speed("vl_s4") = vl_s4

        'ps2_v(numberPosition) = doc2D.myCalculateLenghtProjectionY(vp_P, vp_s2, 0)
        'ps3_v(numberPosition) = doc2D.myCalculateLenghtProjectionY(vp_P, vp_s3, 0)
        'ps4_v(numberPosition) = doc2D.myCalculateLenghtProjectionY(vp_P, vp_s4, 0)

        dt_Speed.Rows.Add(dr_Speed)

        da_Speed.Update(dt_Speed)




    End Sub

    Public Overloads Sub Mechanism_SpeedDraw(row As DataRow)

        numberPosition = row("numberPosition")
        mashtab_mv_view = row("mashtab_mv_view")

        vp_P = StringToPointDouble(row("vp_P"))
        vp_b = StringToPointDouble(row("vp_b"))
        vp_c = StringToPointDouble(row("vp_c"))
        vp_d = StringToPointDouble(row("vp_d"))
        vp_e = StringToPointDouble(row("vp_e"))

        vp_s2 = StringToPointDouble(row("vp_s2"))
        vp_s3 = StringToPointDouble(row("vp_s3"))
        vp_s4 = StringToPointDouble(row("vp_s4"))

        meh_Speed = New cls_SheetView("v " & numberPosition, 200 + numberPosition * 100, 200, , mashtab_mv_view)

        doc2D.AddView(meh_Speed)
        'v3.Create(doc2D) 
        ' Делаем вид активным
        meh_Speed.Create()
        meh_Speed.Active()

        'meh_Speed.Active()

        doc2D.myDrawNumberMacro(vp_P, numberPosition, meh_Speed.ScaleView)

        ' Рисуем вектор Pb
        doc2D.myDrawVector(vp_P, vp_b)

        ' Рисуем вектор Pb
        doc2D.myDrawVector(vp_b, vp_c)
        doc2D.myDrawVector(vp_P, vp_c)
        doc2D.myDrawVector(vp_P, vp_e)

        doc2D.myDrawVector(vp_b, vp_e)


        ' Рисуем вектор ce
        'doc2D.myDrawVector(vp_c, vp_d)

        ' Соединяем с полюсом 
        'doc2D.myDrawVector(vp_P, vp_d)

        ' Рисуем вектора для s2, s3, s4
        doc2D.myDrawVector(vp_P, vp_s2)
        doc2D.myDrawVector(vp_P, vp_s3)
        doc2D.myDrawVector(vp_P, vp_s4)

        ' Простановка всех точек на плане скоростей
        doc2D.myDrawText(vp_P, "Pv")
        doc2D.myDrawText(vp_b, "b")
        doc2D.myDrawText(vp_c, "c")
        doc2D.myDrawText(vp_d, "d")
        doc2D.myDrawText(vp_e, "e")

        doc2D.myDrawText(vp_s2, "s2")
        doc2D.myDrawText(vp_s3, "s3")
        doc2D.myDrawText(vp_s4, "s4")

        meh_Speed.ExitView()
    End Sub

    Public Sub Mechanism_SpeedDraw_(numberPosition As Integer)

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
        vl_dc = doc2D.myCalculateLenghtTwoPoint(vp_d, vp_c)
        cd_v(numberPosition) = vl_dc

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

    Public Sub Mechanism_Acceleration(numberPosition As Integer)


        ' ================= УСКОРЕНИЕ ===================

        DataBase.LoadDataAdapterAndDataTable(TableName.z1_speed, da_Speed, dt_Speed)
        Dim dr_Speed = StoredProcedure.RowZD(Table.Z1_Speed, counter, numberPosition)

        v_bc = dr_Speed("v_bc")
        v_dc = dr_Speed("v_dc")


        DataBase.LoadDataAdapterAndDataTable(TableName.z1_acceleration, da_Acceleration, dt_Acceleration)
        'da_Acceleration.SelectCommand.Transaction = tran

        Dim dr_Acceleration As DataRow = dt_Acceleration.NewRow()

        dr_Acceleration("id_zd") = counter

        dr_Acceleration("numberPosition") = numberPosition

        ' Длина вектора ускорения на чертеже
        'al_pib = 80 ' на чертеже
        dr_Acceleration("al_pb") = al_pib

        ' Определение ускорения ab
        a_b = w2 ^ 2 * L_AB
        dr_Acceleration("a_b") = a_b

        'Определяем масштаб механизма для скоростей
        mashtab_ma = a_b / al_pib
        dr_Acceleration("mashtab_ma") = mashtab_ma

        ' Масштаб для конкретного вида на чертеже ускорения
        mashtab_ma_view = al_pib / a_b
        dr_Acceleration("mashtab_ma_view") = mashtab_ma_view

        ' Задаем начальную точку P для построения плана ускорений
        ap_pi = New PointDouble(0, 0)
        dr_Acceleration("ap_pi") = ap_pi.ToStringPoint()

        ' Определяем угол Pb - Функцией. Принимаем параметры угол между точкой А и B (паралельность AB)
        Dim angle_Pb As Double = doc2D.myCalculateAngleTwoPoint(mp_B, mp_A)
        dr_Acceleration("angle_Pb") = angle_Pb

        ' Находим точку b на плане скоростей согласно принимаемых данных: точка ap_P, Длина ab, угол паралельность AB
        ap_b = doc2D.myCalculateCordinatePoint(ap_pi, a_b, angle_Pb)
        dr_Acceleration("ap_b") = ap_b.ToStringPoint()

        '-----------------------------------------------------------
        ' Для определения точки ap_n3 на плане ускорения нужно определить
        ' угол из точки ap_b и длину вектора al_bn3
        a_n3 = (v_bc ^ 2) / L_BC
        al_n3 = a_n3 / mashtab_ma
        dr_Acceleration("a_n3") = a_n3
        dr_Acceleration("al_n3") = al_n3

        Dim angleCB As Double = doc2D.myCalculateAngleTwoPoint(mp_C, mp_B)
        dr_Acceleration("angleCB") = angleCB

        ap_n3 = doc2D.myCalculateCordinatePoint(ap_b, a_n3, angleCB)
        dr_Acceleration("ap_n3") = ap_n3.ToStringPoint()

        '----------------------------------------------------------------
        a_n4 = (v_dc ^ 2) / L_CD
        al_n4 = a_n4 / mashtab_ma
        dr_Acceleration("a_n4") = a_n4
        dr_Acceleration("al_n4") = al_n4
        Dim angleCD As Double = doc2D.myCalculateAngleTwoPoint(mp_C, mp_D)

        ap_n4 = doc2D.myCalculateCordinatePoint(ap_pi, a_n4, angleCD)
        dr_Acceleration("ap_n4") = ap_n4.ToStringPoint()

        ' Определяем точку ap_c 
        ' Строится из точки ap_n3 перпендикуляр угла вектора al_bn3 и
        ' и из точки ap_P ось по X

        ap_c = doc2D.myCalculateCordinatePoint(ap_n3, angleCB + 270, ap_n4, angleCD + 270)
        dr_Acceleration("ap_c") = ap_c.ToStringPoint()

        a_c = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_c)
        al_c = a_c / mashtab_ma
        dr_Acceleration("a_c") = a_c
        dr_Acceleration("al_c") = al_c

        a_bc = doc2D.myCalculateLenghtTwoPoint(ap_b, ap_c)
        al_bc = a_bc / mashtab_ma
        dr_Acceleration("a_bc") = a_bc
        dr_Acceleration("al_bc") = al_bc

        a_c = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_c)
        al_c = a_c / mashtab_ma
        dr_Acceleration("a_c") = a_c
        dr_Acceleration("al_c") = al_c

        a_dc = a_c
        al_dc = al_c
        dr_Acceleration("a_dc") = a_dc
        dr_Acceleration("al_dc") = al_dc

        ' Определяем точку длину al_cd для определения координаты точки ap_d и угол сd
        a_be = L_BE / L_BC * a_bc
        al_be = a_be / mashtab_ma
        dr_Acceleration("a_be") = a_be
        dr_Acceleration("al_be") = al_be

        Dim angle_cb As Double = doc2D.myCalculateAngleTwoPoint(ap_c, ap_b)
        ap_e = doc2D.myCalculateCordinatePoint(ap_b, a_be, angle_cb)
        dr_Acceleration("ap_e") = ap_e.ToStringPoint()

        a_e = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_e)
        al_e = a_e / mashtab_ma
        dr_Acceleration("a_e") = a_e
        dr_Acceleration("al_e") = al_e

        a_de = a_e
        al_de = al_e
        dr_Acceleration("a_de") = a_de
        dr_Acceleration("al_de") = al_de

        Dim angle_dc As Double = doc2D.myCalculateAngleTwoPoint(ap_pi, ap_c)
        dr_Acceleration("angle_dc") = angle_dc

        '--------------------------------------------------


        ' Определения координат s1, s2, s3, s4, s5
        ap_s2 = doc2D.myCalculateCordinateCentreSeg(ap_pi, ap_b)
        a_s2 = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_s2)
        al_s2 = a_s2 / mashtab_ma

        dr_Acceleration("ap_s2") = ap_s2.ToStringPoint()
        dr_Acceleration("a_s2") = a_s2
        dr_Acceleration("al_s2") = al_s2

        '--------------------------------------------------------------------

        ap_s3 = doc2D.myCalculateCordinateCentreSeg(ap_c, ap_e)
        a_s3 = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_s3)
        al_s3 = a_s3 / mashtab_ma

        dr_Acceleration("ap_s3") = ap_s3.ToStringPoint()
        dr_Acceleration("a_s3") = a_s3
        dr_Acceleration("al_s3") = al_s3

        '--------------------------------------------------------------------

        ' s4 определяется в данном случае в точке c
        ap_s4 = doc2D.myCalculateCordinateCentreSeg(ap_pi, ap_c)
        a_s4 = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_s4)
        al_s4 = a_s4 / mashtab_ma

        dr_Acceleration("ap_s4") = ap_s4.ToStringPoint()
        dr_Acceleration("a_s4") = a_s4
        dr_Acceleration("al_s4") = al_s4

        ps4_a(numberPosition) = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_s4)

        '---------------------------------------------------------------------

        dt_Acceleration.Rows.Add(dr_Acceleration)

        da_Acceleration.Update(dt_Acceleration)

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

    Public Sub Mechanism_AccelerationDraw(row As DataRow)
        numberPosition = row("numberPosition")
        mashtab_ma_view = row("mashtab_ma_view")

        ap_pi = StringToPointDouble(row("ap_pi"))
        ap_b = StringToPointDouble(row("ap_b"))
        ap_c = StringToPointDouble(row("ap_c"))
        ap_n3 = StringToPointDouble(row("ap_n3"))
        ap_n4 = StringToPointDouble(row("ap_n4"))

        ap_e = StringToPointDouble(row("ap_e"))

        ap_s2 = StringToPointDouble(row("ap_s2"))
        ap_s3 = StringToPointDouble(row("ap_s3"))
        ap_s4 = StringToPointDouble(row("ap_s4"))

        a_n3 = row("a_n3")
        a_n4 = row("a_n4")

        meh_Acceleration = New cls_SheetView("a " & numberPosition, 300, 200, , mashtab_ma_view)

        doc2D.AddView(meh_Acceleration)
        'v3.Create(doc2D) 
        ' Делаем вид активным
        meh_Acceleration.Create()
        meh_Acceleration.Active()


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
        '========================================

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


        'doc2D.myDrawLineSeg(ap_e, ap_c)

        doc2D.myDrawVector(ap_b, ap_e)

        ' Рисуем вектора для s2, s3, s4
        doc2D.myDrawVector(ap_pi, ap_s2)
        doc2D.myDrawVector(ap_pi, ap_s3)
        doc2D.myDrawVector(ap_pi, ap_s4)

        ' Простановка всех точек на плане ускорений скоростей
        doc2D.myDrawText(ap_pi, "Pa, d")
        doc2D.myDrawText(ap_b, "b")
        doc2D.myDrawText(ap_c, "c")
        doc2D.myDrawText(ap_e, "e")

        doc2D.myDrawText(ap_n3, "n3")
        doc2D.myDrawText(ap_n4, "n4")

        doc2D.myDrawText(ap_s2, "s2")
        doc2D.myDrawText(ap_s3, "s3")
        doc2D.myDrawText(ap_s4, "s4")

        '========================================
        'doc2D.myDrawVector(ap_b, ap_n3)

        'doc2D.myDrawVector(ap_b, ap_c)
        'doc2D.myDrawVector(ap_n3, ap_c)

        'doc2D.myDrawVector(ap_pi, ap_c)

        ''doc2D.myDrawVector(ap_c, ap_d)

        '' Соединяем с полюсом 
        ''doc2D.myDrawVector(ap_pi, ap_d)

        '' Рисуем вектора для s2, s3, s4
        'doc2D.myDrawVector(ap_pi, ap_s2)
        'doc2D.myDrawVector(ap_pi, ap_s3)

        ''doc2D.myDrawVector(vp_P, vp_s4)

        '' Простановка всех точек на плане скоростей
        'doc2D.myDrawText(ap_pi, "Pa")
        'doc2D.myDrawText(ap_b, "b")
        'doc2D.myDrawText(ap_c, "c, s4")
        ''doc2D.myDrawText(ap_d, "d")

        'doc2D.myDrawText(ap_s2, "s2")
        'doc2D.myDrawText(ap_s3, "s3")

        ''doc2D.myDrawText(ap_s4, "s4")


        meh_Acceleration.ExitView()
    End Sub

    Public Sub Mechanism_AccelerationDraw_(numberPosition As Integer)

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

        Dim ap_n4 As PointDouble = doc2D.myCalculateCordinatePoint(ap_pi, a_n4, angleCD)

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
