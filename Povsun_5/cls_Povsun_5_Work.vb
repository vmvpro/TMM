Imports Kompas6API5, Kompas6Constants
Imports TMM.myDoc2D
Imports System.Data.SQLite
Imports DB

Partial Public Class cls_Povsun_5


    Public Sub New()
        MyBase.New()
    End Sub

    Sub test(counter As Long, v As Integer)

        Dim db As New DataBase

        Dim k = drDadatok2("position_m")

        'counter = 17
        'Dim k = 3

        Dim dr_Mehanizm = StoredProcedure.RowZD(Table.Z5_Mehanizm, counter, v)
        MechanismDraw(dr_Mehanizm, 1)

        Dim dr_Speed = StoredProcedure.RowZD(Table.Z5_Speed, counter, v)
        Mechanism_SpeedDraw(dr_Speed)

        Dim dr_Acceleration = StoredProcedure.RowZD(Table.Z5_Acceleration, counter, v)
        Mechanism_AccelerationDraw(dr_Acceleration)

        Return

        'For i As Integer = 0 To 7

        '    If k = i Then
        '        Dim dr_Mehanizm = StoredProcedure.RowZD(Table.Z5_Mehanizm, counter, i)
        '        MechanismDraw(dr_Mehanizm, 1)
        '    Else
        '        Dim dr_Mehanizm = StoredProcedure.RowZD(Table.Z5_Mehanizm, counter, i)
        '        MechanismDraw(dr_Mehanizm, 2)
        '    End If

        '    Dim dr_Speed = StoredProcedure.RowZD(Table.Z3_Speed, counter, i)
        '    Mechanism_SpeedDraw(dr_Speed)

        'Next

        'Dim dr_Acceleration = StoredProcedure.RowZD(Table.Z3_Acceleration, counter, k)
        'Mechanism_AccelerationDraw(dr_Acceleration)
    End Sub

    Sub CreateOne()

        Dim i1 = 1
        Dim i3 = 3
        Dim i7 = 6

        Dim db As New DataBase

        DataBase.Connection().Open()


        'For i As Integer = 0 To 7

        Dim tran As SQLiteTransaction = DataBase.Connection().BeginTransaction()

        MechanismConstruction(tran, i1)
        Mechanism_Speed(tran, i1)
        Mechanism_Acceleration(tran, i1)

        'tran.Commit()
        'Return

        MechanismConstruction(tran, i3)
        Mechanism_Speed(tran, i3)
        Mechanism_Acceleration(tran, i3)

        MechanismConstruction(tran, i7)
        Mechanism_Speed(tran, i7)
        Mechanism_Acceleration(tran, i7)

        tran.Commit()
        'Next

        DataBase.Connection().Close()
        DataBase.UpdateDataBase()
    End Sub

    Public Sub DrawOne(numberPosition As Integer)


        Dim dr_Mehanizm = StoredProcedure.RowZD(Table.Z3_Mehanizm, counter, numberPosition)
        MechanismDraw(dr_Mehanizm, 1)

        dt_Speed = db.ReturnDataTable(TableName.z3_speed)
        Dim dr_Speed = StoredProcedure.RowZD(dt_Speed, counter, numberPosition)
        Mechanism_SpeedDraw(dr_Speed)

        dt_Acceleration = db.ReturnDataTable(TableName.z3_acceleration)
        Dim dr_Acceleration = StoredProcedure.RowZD(dt_Acceleration, counter, numberPosition)
        Mechanism_AccelerationDraw(dr_Acceleration)
    End Sub

    Public Sub RunKompas()
        doc2D = New myDoc2D(ActiveDoc2D, "ШаблонЧертежа")
    End Sub

    Public Sub Run()
        doc2D = New myDoc2D(ActiveDoc2D, "ШаблонЧертежа")
        'doc2D = New myDoc2D(ActiveDoc2D, "ШаблонМаховик")
        If doc2D Is Nothing Then Throw New Exception("Приложение компас не запущено")

        Dim k = drDadatok2("position_m")
        'test(1, 1)
        'CreateOne()
        'Return
        'Dim db As New DataBase

        DataBase.Connection().Open()

        For i As Integer = 0 To 7

            Dim tran As SQLiteTransaction = DataBase.Connection().BeginTransaction()

            MechanismConstruction(tran, i)
            Mechanism_Speed(tran, i)
            Mechanism_Acceleration(tran, i)

            tran.Commit()
        Next

        DataBase.Connection().Close()
        DataBase.UpdateDataBase()

        'For i As Integer = 0 To 7


        '    If i = k Then
        '        'толстая линия 
        '        MechanismDraw(i, 1)
        '        Mechanism_AccelerationDraw(i)
        '    Else
        '        MechanismDraw(i, 2)
        '    End If

        '    Mechanism_SpeedDraw(i)

        'Next
        'kompas = kompasApp()
        'kompas.ksMessage("Построение закончено")

        'ActiveDoc2D.m()

        'shatun.Mechanism_Speed()

    End Sub

    Public Sub MechanismConstruction(tran As SQLiteTransaction, numberPosition As Integer)
        ''Dim doc2D As New myDoc2D(doc2D)
        'mp_O = New PointDouble(110, 310)

        'meh_Mechanism = New cls_SheetView("Mechanism " & numberPosition, 110, 310, , mashtab_ml_view)

        'doc2D.AddView(meh_Mechanism)

        'meh_Mechanism.Create()
        'meh_Mechanism.Active()

        'doc2D.myDrawCrossingLines(mp_A)
        'doc2D.myDrawPoint(mp_A)

        '==================================================
        'DataBase.LoadDataAdapterAndDataTable(TableName.z3_mehanizm, da_Mehanizm, dt_Mehanizm)

        ' Dim dr_Mehanizm As DataRow = DataBase.Row(dt_Mehanizm, "id_z3", counter)


        'da_Mehanizm.SelectCommand.Transaction = tran


        '================== МЕХАНИЗМ  ===================
        ' Длина AB на плане принимаем = 36  

        DataBase.LoadDataAdapterAndDataTable(TableName.z5_mehanizm, da_Mehanizm, dt_Mehanizm)

        'For Each r As DataRow In dt_Mehanizm.Rows
        'Debug.Print(r(0))
        'Next

        da_Mehanizm.SelectCommand.Transaction = tran

        Dim dr_Mehanizm As DataRow = dt_Mehanizm.NewRow()
        dr_Mehanizm("id_zd") = counter

        'ml_AB = 50
        dr_Mehanizm("L_AB") = L_AB '36
        dr_Mehanizm("ml_AB") = ml_AB '36
        'da_Mehanizm.Update(dt_Mehanizm)

        'Определяем масштаб механизма
        mashtab_ml = L_AB / ml_AB
        dr_Mehanizm("mashtab_ml") = mashtab_ml

        ' Масштаб механизма для конкретного вида на чертеже
        mashtab_ml_view = ml_AB / L_AB
        dr_Mehanizm("mashtab_ml_view") = mashtab_ml_view

        dr_Mehanizm("L_BC") = L_BC
        ' Длины механизма в масштабе
        ml_BC = L_BC / mashtab_ml
        dr_Mehanizm("ml_BC") = ml_BC

        L_CD = 0.25 * L_BC
        ml_CD = L_CD / mashtab_ml
        dr_Mehanizm("ml_CD") = ml_CD

        L_BD = L_BC + L_CD
        dr_Mehanizm("L_BD") = L_BD
        ml_BD = L_BD / mashtab_ml
        dr_Mehanizm("ml_BD") = ml_BD

        mp_A = New PointDouble(0, 0)
        dr_Mehanizm("mp_A") = mp_A.ToStringPoint()


        'da_Mehanizm.Update(dt_Mehanizm)



        ' Для определения крайнего положения механизма в точке C0 нужно определить координаты прямой на которой эта точка находится
        Dim line_CordinateLineC As New PointDouble(0, mp_A.Y + L_AB + L_BD)


        ' Для определения крайнего положения механизма в точке C0 нужно определить координаты прямой на которой эта точка находится
        'Dim mp_C0 As New PointDouble(0, mp_A.Y + L_BD)
        'dr_Mehanizm("line_CordinateLineC") = line_CordinateLineC.ToString()    ' Не требуется
        mp_C0 = doc2D.myCalculateCordinatePoint(mp_A, L_AB + L_BC, line_CordinateLineC, 90, enLocation.Vertical_Up)

        ' Поиск точки C происходит так.
        ' Из точки B проводим круг заданным радиусом и на пересечении прямой line_CordinateLineC 
        ' (Угол прямой line_CordinateLineC Определяем по тому как у нас размещена точка С)
        ' переменная myDoc2D.enLocation - это выбор (перечисление) и нужно определить в каком месте расположена точка C 
        ' В нашем механизме есть 8 вариантов: 4 из оси x, y и 4 еще - четверти координатных осей 
        'MyBase.mp_C0 = doc2D.myCalculateCordinatePoint(mp_A, L_AB + L_BC, mp_C0, 0, enLocation.Vertical_Up)
        'dr_Mehanizm("mp_C0") = mp_C0.ToString()

        'da_Mehanizm.Update(dt_Mehanizm)

        ' Определяем угол для крайнего положения механизма из точки A и точки C0
        Dim angle_AC0 As Double = doc2D.myCalculateAngleTwoPoint(mp_A, mp_C0)
        'dr_Mehanizm("angle_AC0") = angle_AC0

        ' Нахождение координаты точки B
        ' Определяем координату точки B на плане для положения, которое задано в переменной numberPosition
        'Dim numberPosition As Integer = 5

        ' Шаг перемещения механизма
        Dim step_ As Double = 45 '+ 270
        'dr_Mehanizm("step_") = step_

        Dim angle_AB As Double = angle_AC0 + (numberPosition * step_)
        dr_Mehanizm("numberPosition") = numberPosition

        'If angle_AB > 360 Then angle_AB -= 360

        Do While angle_AB > 360
            angle_AB -= 360
        Loop

        dr_Mehanizm("angle_AB") = angle_AB

        mp_B = doc2D.myCalculateCordinatePoint(mp_A, L_AB, angle_AB)
        dr_Mehanizm("mp_B") = mp_B.ToStringPoint()

        'doc2D.myDrawPoint(mp_B)
        'doc2D.myDrawLineSeg(mp_A, mp_B)

        'mp_C = doc2D.myCalculateCordinatePoint(mp_B, L_BC, mp_C0, 0, myDoc2D.enLocation.Vertical_Up)

        'mp_C = doc2D.myCalculateCordinatePoint(

        'mp_C = doc2D.myCalculateCordinatePoint(mp_A, mp_C0, mp_B, L_BC)
        'dr_Mehanizm("mp_C") = mp_C.ToStringPoint()

        mp_C = doc2D.myCalculateCordinatePoint(mp_B, L_BC, line_CordinateLineC, enAngle.Angle90, myDoc2D.enLocation.Vertical_Up)
        dr_Mehanizm("mp_C") = mp_C.ToStringPoint()
        'doc2D.myDrawPoint(mp_C)

        Dim angle_BC As Double = doc2D.myCalculateAngleTwoPoint(mp_B, mp_C)
        dr_Mehanizm("angle_BC") = angle_BC

        mp_D = doc2D.myCalculateCordinatePoint(mp_C, L_CD, angle_BC)
        dr_Mehanizm("mp_D") = mp_D.ToStringPoint()
        'doc2D.myDrawPoint(mp_D)

        mp_S2 = doc2D.myCalculateCordinateCentreSeg(mp_A, mp_B)
        dr_Mehanizm("mp_S2") = mp_S2.ToStringPoint()

        mp_S3 = doc2D.myCalculateCordinateCentreSeg(mp_B, mp_D)
        dr_Mehanizm("mp_S3") = mp_S3.ToStringPoint()

        mp_S4 = mp_C
        dr_Mehanizm("mp_S4") = mp_S4.ToStringPoint()

        dt_Mehanizm.Rows.Add(dr_Mehanizm)

        da_Mehanizm.Update(dt_Mehanizm)

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

        'doc2D.myLineSegDraw(A, B)

        'doc2D.myMtrDelete()
    End Sub

    Public Sub MechanismDraw(rowMechanism As DataRow, style As Integer)
        'Dim doc2D As New myDoc2D(doc2D)

        numberPosition = rowMechanism("numberPosition")
        mashtab_ml_view = rowMechanism("mashtab_ml_view")

        mp_A = StringToPointDouble(rowMechanism("mp_A"))
        mp_B = StringToPointDouble(rowMechanism("mp_B"))
        mp_C = StringToPointDouble(rowMechanism("mp_C"))
        mp_D = StringToPointDouble(rowMechanism("mp_D"))

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
        'End If

        doc2D.myDrawPoint(mp_B, enStylePoint.Circle)
        doc2D.myDrawPoint(mp_C, enStylePoint.Circle)
        doc2D.myDrawPoint(mp_D, enStylePoint.Circle)

        doc2D.myDrawLineSeg(mp_A, mp_B, style)
        'doc2D.myDrawLineSeg(mp_B, mp_C, style)
        'doc2D.myDrawLineSeg(mp_C, mp_D, style)
        doc2D.myDrawLineSeg(mp_B, mp_D, style)

        doc2D.myDrawTextIndex(mp_B, "B", "", numberPosition.ToString(), height)
        doc2D.myDrawTextIndex(mp_C, "C", "", numberPosition.ToString(), height)
        doc2D.myDrawTextIndex(mp_D, "D", "", numberPosition.ToString(), height)

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

    Public Sub Mechanism_Speed(tran As SQLiteTransaction, numberPosition As Integer)
        'Dim doc2D As New myDoc2D(ActiveDoc2D)

        'doc2D.ksMtr(370, 235, 0, 1, 1)

        ' Создание вида для 5-положения
        'meh_Speed = New cls_SheetView("v " & numberPosition, 200 + numberPosition * 100, 200, , mashtab_mv_view)

        'doc2D.AddView(meh_Speed)
        ''v3.Create(doc2D) 
        '' Делаем вид активным
        'meh_Speed.Create()
        'meh_Speed.Active()

        'DataBase.LoadDataAdapterAndDataTable(TableName.z3_Speed, da_Speed, dt_Speed)

        'Dim dr_Speed As DataRow = DataBase.Row(dt_Speed, "id_z3", counter)
        'da_Speed.SelectCommand.Transaction = tran

        '================== СКОРОСТЬ ===================

        DataBase.LoadDataAdapterAndDataTable(TableName.z5_speed, da_Speed, dt_Speed)
        da_Speed.SelectCommand.Transaction = tran


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

        ' Задаем начальную точку P для построения плана скоростей
        vp_P = New PointDouble(0, 0)
        dr_Speed("vp_P") = vp_P.ToStringPoint()

        ' Определяем угол Pb - Функцией. Принимаем параметры угол между точкой А и B + 90 (перпендикуляр AB)
        Dim angle_Pb As Double = doc2D.myCalculateAngleTwoPoint(mp_A, mp_B, enAngle.Angle90)
        dr_Speed("angle_Pb") = angle_Pb

        ' Находим точку b на плане скоростей согласно принимаемых данных: точка P, Длина 70 и угол в переменной angle_Pb
        vp_b = doc2D.myCalculateCordinatePoint(vp_P, v_b, angle_Pb)
        dr_Speed("vp_b") = vp_b.ToStringPoint()

        ' Рисуем вектор Pb
        'doc2D.myDrawVector(vp_P, vp_b)

        ' Определение точки с на плане скоростей на пересечении перпендикуляров точок b и P плана скоростей 
        ' соответственно своих ланок на плане механизма
        Dim angle_bc As Double = doc2D.myCalculateAngleTwoPoint(mp_B, mp_C, enAngle.Angle90)
        dr_Speed("angle_bc") = angle_bc

        Dim angle_py As Double = doc2D.myCalculateAngleTwoPoint(New PointDouble(0, enAngle.Angle90), vp_P)
        dr_Speed("angle_py") = angle_py

        vp_c = doc2D.myCalculateCordinatePoint(vp_b, angle_bc, vp_P, angle_py)
        dr_Speed("vp_c") = vp_c.ToStringPoint()

        v_c = doc2D.myCalculateLenghtTwoPoint(vp_P, vp_c)
        vl_c = v_c / mashtab_mv
        dr_Speed("v_c") = v_c
        dr_Speed("vl_c") = vl_c

        ' Рисуем вектор Pb
        'doc2D.myDrawVector(vp_b, vp_c)
        'doc2D.myDrawVector(vp_P, vp_c)

        ' Определение длины bc на плане скоростей
        v_bc = doc2D.myCalculateLenghtTwoPoint(vp_b, vp_c)
        vl_bc = v_bc / mashtab_mv
        dr_Speed("v_bc") = v_bc
        dr_Speed("vl_bc") = vl_bc

        ' Определение длины cd на плане скоростей  (математика все длины есть )
        v_cd = ml_CD / ml_BC * v_bc
        vl_cd = v_cd / mashtab_mv
        dr_Speed("v_cd") = v_cd
        dr_Speed("vl_cd") = vl_cd
        cd_v(numberPosition) = v_cd

        ' Нахождение координаты точки е на плане скоростей
        ' входные параметры: точка с, длина се и угол Pb

        ' Определяем угол направления вектора bc по точкам b и c

        Dim angle_cd = doc2D.myCalculateAngleTwoPoint(vp_b, vp_c)
        dr_Speed("angle_cd") = angle_cd

        vp_d = doc2D.myCalculateCordinatePoint(vp_c, v_cd, angle_cd)
        dr_Speed("vp_d") = vp_d.ToStringPoint()

        v_d = doc2D.myCalculateLenghtTwoPoint(vp_P, vp_d)
        vl_d = v_d / mashtab_mv
        dr_Speed("v_d") = v_d
        dr_Speed("vl_d") = vl_d

        ' Рисуем вектор ce
        'doc2D.myDrawVector(vp_c, vp_d)

        ' Соединяем с полюсом 
        'doc2D.myDrawVector(vp_P, vp_d)


        ' Определения координат s1, s2, s3, s4, s5
        vp_s2 = doc2D.myCalculateCordinateCentreSeg(vp_P, vp_b)
        dr_Speed("vp_s2") = vp_s2.ToStringPoint()

        vp_s3 = doc2D.myCalculateCordinateCentreSeg(vp_b, vp_d)
        dr_Speed("vp_s3") = vp_s3.ToStringPoint()

        vp_s4 = vp_c

        'vp_s4 = doc2D.myCalculateCordinateCentreSeg(vp_P, vp_c)
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

        ps2_v(numberPosition) = doc2D.myCalculateLenghtProjectionY(vp_P, vp_s2, 0)
        ps3_v(numberPosition) = doc2D.myCalculateLenghtProjectionY(vp_P, vp_s3, 0)
        ps4_v(numberPosition) = doc2D.myCalculateLenghtProjectionY(vp_P, vp_s4, 0)

        dt_Speed.Rows.Add(dr_Speed)

        da_Speed.Update(dt_Speed)

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

        'meh_Speed.ExitView()

        'doc2D.myMtrDelete()
    End Sub

    Public Overloads Sub Mechanism_SpeedDraw(row As DataRow)

        numberPosition = row("numberPosition")
        mashtab_mv_view = row("mashtab_mv_view")

        vp_P = StringToPointDouble(row("vp_P"))
        vp_b = StringToPointDouble(row("vp_b"))
        vp_c = StringToPointDouble(row("vp_c"))
        vp_d = StringToPointDouble(row("vp_d"))

        vp_s2 = StringToPointDouble(row("vp_s2"))
        vp_s3 = StringToPointDouble(row("vp_s3"))
        'vp_s4 = StringToPointDouble(row("vp_s4"))

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


        ' Рисуем вектор ce
        doc2D.myDrawVector(vp_c, vp_d)

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

    Public Overloads Sub Mechanism_SpeedDraw(numberPosition As Integer)
        meh_Speed.Active()

        doc2D.myDrawNumberMacro(vp_P, numberPosition.ToString(), meh_Speed.ScaleView)

        ' Рисуем вектор Pb
        doc2D.myDrawVector(vp_P, vp_b)

        ' Рисуем вектор Pb
        doc2D.myDrawVector(vp_b, vp_c)
        doc2D.myDrawVector(vp_P, vp_c)


        ' Рисуем вектор ce
        doc2D.myDrawVector(vp_c, vp_d)

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
    Public Sub Mechanism_Acceleration(tran As SQLiteTransaction, numberPosition As Integer)

        'Dim doc2D As New myDoc2D(ActiveDoc2D)

        'doc2D.ksMtr(370, 235, 0, 1, 1)

        ' Создание вида для 5-положения
        'meh_Acceleration = New cls_SheetView("a " & numberPosition, 300, 200, , mashtab_ma_view)

        'doc2D.AddView(meh_Acceleration)
        ''v3.Create(doc2D) 
        '' Делаем вид активным
        'meh_Acceleration.Create()
        'meh_Acceleration.Active()

        'DataBase.LoadDataAdapterAndDataTable(TableName.z3_acceleration, da_Acceleration, dt_Acceleration)

        'Dim dr_Acceleration As DataRow = DataBase.Row(dt_Acceleration, "id_z3", counter)

        'da_Acceleration.SelectCommand.Transaction = tran

        ' ================= УСКОРЕНИЕ ===================

        DataBase.LoadDataAdapterAndDataTable(TableName.z5_acceleration, da_Acceleration, dt_Acceleration)
        da_Acceleration.SelectCommand.Transaction = tran

        Dim dr_Acceleration As DataRow = dt_Acceleration.NewRow()

        dr_Acceleration("id_zd") = counter

        dr_Acceleration("numberPosition") = numberPosition

        ' Длина вектора ускорения на чертеже
        'al_pib = 80 ' на чертеже
        dr_Acceleration("al_pb") = al_pib

        ' Определение ускорения ab
        a_b = w2 ^ 2 * L_AB
        dr_Acceleration("ab") = a_b

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

        ' Рисуем вектор Pb
        'doc2D.myDrawVector(ap_P, ap_b)

        ' Определяем длину вектора bn3 
        ' нормальне прискорення точки ap_c относительно точки ap_b

        ' Для определения точки ap_n3 на плане ускорения нужно определить
        ' угол из точки ap_b и длину вектора al_bn3
        a_n3 = (v_bc ^ 2) / L_BC
        al_n3 = a_n3 / mashtab_ma
        dr_Acceleration("a_bn3") = a_n3
        dr_Acceleration("al_bn3") = al_n3

        Dim angleCB As Double = doc2D.myCalculateAngleTwoPoint(mp_C, mp_B)
        dr_Acceleration("angleCB") = angleCB

        ap_n3 = doc2D.myCalculateCordinatePoint(ap_b, a_n3, angleCB)
        dr_Acceleration("ap_n3") = ap_n3.ToStringPoint()

        'doc2D.myDrawVector(ap_b, ap_n3)

        ' Определяем точку ap_c 
        ' Строится из точки ap_n3 перпендикуляр угла вектора al_bn3 и
        ' и из точки ap_P ось по X

        ap_c = doc2D.myCalculateCordinatePoint(ap_n3, angleCB + 90, ap_pi, 270)
        dr_Acceleration("ap_c") = ap_c.ToStringPoint()

        a_c = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_c)
        al_c = a_c / mashtab_ma
        dr_Acceleration("a_c") = a_c
        dr_Acceleration("al_c") = al_c

        'doc2D.myDrawVector(ap_b, ap_c)
        'doc2D.myDrawVector(ap_n3, ap_c)

        a_n3c = doc2D.myCalculateLenghtTwoPoint(ap_n3, ap_c)
        al_n3c = a_n3c / mashtab_ma
        dr_Acceleration("a_n3c") = a_n3c
        dr_Acceleration("al_n3c") = al_n3c

        'doc2D.myDrawVector(ap_P, ap_c)

        a_bc = doc2D.myCalculateLenghtTwoPoint(ap_b, ap_c)
        al_bc = a_bc / mashtab_ma
        dr_Acceleration("a_bc") = a_bc
        dr_Acceleration("al_bc") = al_bc
        bc_a(numberPosition) = a_bc

        ' Определяем точку длину al_cd для определения координаты точки ap_d и угол сd
        a_cd = L_CD / L_BC * a_bc
        al_cd = a_cd / mashtab_ma
        dr_Acceleration("a_cd") = a_cd
        dr_Acceleration("al_cd") = al_cd

        Dim angle_cd As Double = doc2D.myCalculateAngleTwoPoint(ap_b, ap_c)
        dr_Acceleration("angle_cd") = angle_cd

        ap_d = doc2D.myCalculateCordinatePoint(ap_c, a_cd, angle_cd)
        dr_Acceleration("ap_d") = ap_d.ToStringPoint()

        a_d = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_d)
        al_d = a_d / mashtab_ma
        dr_Acceleration("a_d") = a_d
        dr_Acceleration("al_d") = al_d

        'doc2D.myDrawVector(ap_c, ap_d)

        ' Соединяем с полюсом 
        'doc2D.myDrawVector(ap_P, ap_d)


        ' Определения координат s1, s2, s3, s4, s5
        ap_s2 = doc2D.myCalculateCordinateCentreSeg(ap_pi, ap_b)
        a_s2 = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_s2)
        al_s2 = a_s2 / mashtab_ma

        dr_Acceleration("ap_s2") = ap_s2.ToStringPoint()
        dr_Acceleration("a_s2") = a_s2
        dr_Acceleration("al_s2") = al_s2

        ps2_a(numberPosition) = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_s2)

        '--------------------------------------------------------------------

        ap_s3 = doc2D.myCalculateCordinateCentreSeg(ap_b, ap_d)
        a_s3 = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_s3)
        al_s3 = a_s3 / mashtab_ma

        dr_Acceleration("ap_s3") = ap_s3.ToStringPoint()
        dr_Acceleration("a_s3") = a_s3
        dr_Acceleration("al_s3") = al_s3

        ps3_a(numberPosition) = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_s3)

        '--------------------------------------------------------------------

        ' s4 определяется в данном случае в точке c
        ap_s4 = ap_c
        a_s4 = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_s4)
        al_s4 = a_s4 / mashtab_ma

        dr_Acceleration("ap_s4") = ap_s4.ToStringPoint()
        dr_Acceleration("a_s4") = a_s4
        dr_Acceleration("al_s4") = al_s4

        ps4_a(numberPosition) = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_s4)

        '---------------------------------------------------------------------

        dt_Acceleration.Rows.Add(dr_Acceleration)

        da_Acceleration.Update(dt_Acceleration)

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


        'meh_Acceleration.ExitView()

    End Sub

    Public Sub Mechanism_AccelerationDraw(row As DataRow)

        numberPosition = row("numberPosition")
        mashtab_ma_view = row("mashtab_ma_view")

        ap_pi = StringToPointDouble(row("ap_pi"))
        ap_b = StringToPointDouble(row("ap_b"))
        ap_c = StringToPointDouble(row("ap_c"))
        ap_n3 = StringToPointDouble(row("ap_n3"))
        ap_d = StringToPointDouble(row("ap_d"))

        ap_s2 = StringToPointDouble(row("ap_s2"))
        ap_s3 = StringToPointDouble(row("ap_s3"))

        meh_Acceleration = New cls_SheetView("a " & numberPosition, 300, 200, , mashtab_ma_view)

        doc2D.AddView(meh_Acceleration)
        'v3.Create(doc2D) 
        ' Делаем вид активным
        meh_Acceleration.Create()
        meh_Acceleration.Active()

        doc2D.myDrawNumberMacro(ap_pi, numberPosition.ToString(), meh_Acceleration.ScaleView)

        ' Рисуем вектор Pb
        doc2D.myDrawVector(ap_pi, ap_b)

        doc2D.myDrawVector(ap_b, ap_n3)

        doc2D.myDrawVector(ap_b, ap_c)
        doc2D.myDrawVector(ap_n3, ap_c)

        doc2D.myDrawVector(ap_pi, ap_c)

        doc2D.myDrawVector(ap_c, ap_d)

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

        doc2D.myDrawText(ap_s2, "s2")
        doc2D.myDrawText(ap_s3, "s3")

        'doc2D.myDrawText(ap_s4, "s4")


        meh_Acceleration.ExitView()
    End Sub

    Sub Mahovik()

    End Sub

    Sub PowerAnalysis()
        meh_PowerAnalysis_23 = New cls_SheetView("sa_23", 370, 300, , mashtab_ml_view)

        doc2D.AddView(meh_PowerAnalysis_23)
        'v3.Create(doc2D) 
        ' Делаем вид активным
        meh_PowerAnalysis_23.Create()
        meh_PowerAnalysis_23.Active()

        doc2D.myDrawLineSeg(mp_B, mp_D)

        doc2D.myDrawPoint(mp_B)
        doc2D.myDrawText(mp_B, "B")

        doc2D.myDrawPoint(mp_C)
        doc2D.myDrawText(mp_C, "C")

        doc2D.myDrawPoint(mp_D)
        doc2D.myDrawText(mp_D, "D")

        doc2D.myDrawPoint(mp_S3)
        doc2D.myDrawText(mp_S3, "S3")

        G2 = m2 * 9.81
        al_as2 = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_s2)
        Fi2 = m2 * al_as2

        G3 = m3 * 9.81
        al_as3 = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_s3)
        Fi3 = m3 * al_as3

        G4 = m4 * 9.81
        al_as4 = doc2D.myCalculateLenghtTwoPoint(ap_pi, ap_s4)
        Fi4 = m4 * al_as4

        J3 = (m3 * (L_BD ^ 2)) / 12

        ' Для определения углового ускорения E3 необходимо сначала определить:
        ' - абсолютную длину вектора между точками ap_n3 и ap_c (вектор aτ_cb) соответственно на плане ускорения
        ' - знак направления относительно точки B 
        ' После этих двух неизвестных, можно переходить для поиска E3

        ' Абсолютная длина вектора aτ_cb
        Dim angleVector_n3_c = doc2D.myCalculateAngleTwoPoint(ap_n3, ap_c)
        Dim sgn_E3 As Double = doc2D.myCalculateSGN(enPointPlacement.Left, angleVector_n3_c)
        e3 = a_n3c * sgn_E3 / L_BC

        M_M3 = -(J3 * e3)

        ' Основное уравнение 
        ' Rτ*BC + G3*h_G3 + Fu3*h_Fu3 + M_M3 = 0

        ' Уравнение и плечи строим относительно точки C

        Dim angle_Vector_G3 As Double = doc2D.myCalculateAngleTwoPoint(ap_s3, New PointDouble(ap_s3.X, ap_s3.Y - 50))
        Dim sgn_G3 As Double = doc2D.myCalculateSGN(enPointPlacement.Right, angle_Vector_G3)
        Dim h_G3 As Double = doc2D.myCalculateLenghtProjectionX(mp_S3, mp_C, 0)
        Dim mp_Vector_G3 As PointDouble
        mp_Vector_G3 = doc2D.myCalculateCordinatePoint(mp_S3, 15 / meh_Mechanism.ScaleView, angle_Vector_G3)
        doc2D.myDrawVector(mp_S3, mp_Vector_G3)

        Dim angle_Vector_Fu3 As Double = doc2D.myCalculateAngleTwoPoint(ap_s3, ap_pi)
        Dim sgn_Fu3 As Double = doc2D.myCalculateSGN(enPointPlacement.Right, angle_Vector_Fu3)
        Dim h_Fu3 As Double = doc2D.myCalculateLenghtProjectionX(mp_S3, mp_C, angle_Vector_Fu3 + 90)
        Dim mp_Vector_Fu3 As PointDouble
        mp_Vector_Fu3 = doc2D.myCalculateCordinatePoint(mp_S3, 15 / meh_Mechanism.ScaleView, angle_Vector_Fu3)
        doc2D.myDrawVector(mp_S3, mp_Vector_Fu3)

        Dim vector_len_G3 As Double = sgn_G3 * G3 * h_G3
        Dim vector_len_Fu3 As Double = sgn_Fu3 * Fi3 * h_Fu3

        Dim Rt As Double
        Rt = (vector_len_G3 + vector_len_Fu3 + M_M3) / L_BC


        meh_PowerAnalysis = New cls_SheetView("PowerAnalysis", 100, 50, , mashtab_msa_view)

        doc2D.AddView(meh_PowerAnalysis)
        'v3.Create(doc2D) 
        ' Делаем вид активным
        meh_PowerAnalysis.Create()
        meh_PowerAnalysis.Active()

        Dim sap_O As New PointDouble("O", 0, 0)

        ' Определить координаты точки Rt, зависит от самого значения - или +, от этого зависит и угол
        'Dim angle_Vector_Rt As Double = doc2D.myCalculateCordinatePoint(sap_O, Math.Abs(Rt), 20)

        Dim angle_BC As Double = doc2D.myCalculateAngleTwoPoint(mp_B, mp_C)
        If angle_BC > 180 Then angle_BC -= 180

        Dim angle_Vector_Rt As Double



        If Rt > 0 Then angle_Vector_Rt = 0



        meh_PowerAnalysis.ExitView()

    End Sub

    Public Sub Run_Mahovik()
        'doc2D = New myDoc2D(ActiveDoc2D, "ШаблонЧертежа")
        doc2D = New myDoc2D(ActiveDoc2D, "ШаблонМаховик")
        If doc2D Is Nothing Then Throw New Exception("Приложение компас не запущено")

        ''Dim doc2D As New myDoc2D(doc2D)
        'mp_O = New PointDouble(110, 310)

        'meh_Mechanism = New cls_SheetView("Mechanism " & numberPosition, 110, 310, , mashtab_ml_view)

        'doc2D.AddView(meh_Mechanism)

        'meh_Mechanism.Create()
        'meh_Mechanism.Active()

        'doc2D.myDrawCrossingLines(mp_A)
        'doc2D.myDrawPoint(mp_A)

        '==================================================
        'DataBase.LoadDataAdapterAndDataTable(TableName.z3_mehanizm, da_Mehanizm, dt_Mehanizm)

        ' Dim dr_Mehanizm As DataRow = DataBase.Row(dt_Mehanizm, "id_z3", counter)


        'da_Mehanizm.SelectCommand.Transaction = tran


        '================== МЕХАНИЗМ  ===================
        ' Длина AB на плане принимаем = 36  

        DataBase.LoadDataAdapterAndDataTable(TableName.z5_mehanizm, da_Mehanizm, dt_Mehanizm)

        'For Each r As DataRow In dt_Mehanizm.Rows
        'Debug.Print(r(0))
        'Next

        'da_Mehanizm.SelectCommand.Transaction = tran

        Dim dr_Mehanizm As DataRow = dt_Mehanizm.NewRow()
        dr_Mehanizm("id_zd") = counter

        'ml_AB = 50
        dr_Mehanizm("L_AB") = L_AB '36
        dr_Mehanizm("ml_AB") = ml_AB '36
        'da_Mehanizm.Update(dt_Mehanizm)

        'Определяем масштаб механизма
        mashtab_ml = L_AB / ml_AB
        dr_Mehanizm("mashtab_ml") = mashtab_ml

        ' Масштаб механизма для конкретного вида на чертеже
        mashtab_ml_view = ml_AB / L_AB
        dr_Mehanizm("mashtab_ml_view") = mashtab_ml_view

        dr_Mehanizm("L_BC") = L_BC
        ' Длины механизма в масштабе
        ml_BC = L_BC / mashtab_ml
        dr_Mehanizm("ml_BC") = ml_BC

        L_CD = 0.25 * L_BC
        ml_CD = L_CD / mashtab_ml
        dr_Mehanizm("ml_CD") = ml_CD

        L_BD = L_BC + L_CD
        dr_Mehanizm("L_BD") = L_BD
        ml_BD = L_BD / mashtab_ml
        dr_Mehanizm("ml_BD") = ml_BD

        mp_A = New PointDouble(0, 0)
        dr_Mehanizm("mp_A") = mp_A.ToStringPoint()


        'da_Mehanizm.Update(dt_Mehanizm)



        ' Для определения крайнего положения механизма в точке C0 нужно определить координаты прямой на которой эта точка находится
        Dim line_CordinateLineC As New PointDouble(0, mp_A.Y + L_AB + L_BD)


        ' Для определения крайнего положения механизма в точке C0 нужно определить координаты прямой на которой эта точка находится
        'Dim mp_C0 As New PointDouble(0, mp_A.Y + L_BD)
        'dr_Mehanizm("line_CordinateLineC") = line_CordinateLineC.ToString()    ' Не требуется
        mp_C0 = doc2D.myCalculateCordinatePoint(mp_A, L_AB + L_BC, line_CordinateLineC, 90, enLocation.Vertical_Up)

        ' Поиск точки C происходит так.
        ' Из точки B проводим круг заданным радиусом и на пересечении прямой line_CordinateLineC 
        ' (Угол прямой line_CordinateLineC Определяем по тому как у нас размещена точка С)
        ' переменная myDoc2D.enLocation - это выбор (перечисление) и нужно определить в каком месте расположена точка C 
        ' В нашем механизме есть 8 вариантов: 4 из оси x, y и 4 еще - четверти координатных осей 
        'MyBase.mp_C0 = doc2D.myCalculateCordinatePoint(mp_A, L_AB + L_BC, mp_C0, 0, enLocation.Vertical_Up)
        'dr_Mehanizm("mp_C0") = mp_C0.ToString()

        'da_Mehanizm.Update(dt_Mehanizm)

        ' Определяем угол для крайнего положения механизма из точки A и точки C0
        Dim angle_AC0 As Double = doc2D.myCalculateAngleTwoPoint(mp_A, mp_C0)
        'dr_Mehanizm("angle_AC0") = angle_AC0

        ' Нахождение координаты точки B
        ' Определяем координату точки B на плане для положения, которое задано в переменной numberPosition
        'Dim numberPosition As Integer = 5

        ' Шаг перемещения механизма
        Dim step_ As Double = 45 '+ 270
        'dr_Mehanizm("step_") = step_

        Dim angle_AB As Double = angle_AC0 + (numberPosition * step_)
        dr_Mehanizm("numberPosition") = numberPosition

        'If angle_AB > 360 Then angle_AB -= 360

        Do While angle_AB > 360
            angle_AB -= 360
        Loop

        dr_Mehanizm("angle_AB") = angle_AB

        mp_B = doc2D.myCalculateCordinatePoint(mp_A, L_AB, angle_AB)
        dr_Mehanizm("mp_B") = mp_B.ToStringPoint()

        'doc2D.myDrawPoint(mp_B)
        'doc2D.myDrawLineSeg(mp_A, mp_B)

        'mp_C = doc2D.myCalculateCordinatePoint(mp_B, L_BC, mp_C0, 0, myDoc2D.enLocation.Vertical_Up)

        'mp_C = doc2D.myCalculateCordinatePoint(

        'mp_C = doc2D.myCalculateCordinatePoint(mp_A, mp_C0, mp_B, L_BC)
        'dr_Mehanizm("mp_C") = mp_C.ToStringPoint()

        mp_C = doc2D.myCalculateCordinatePoint(mp_B, L_BC, line_CordinateLineC, enAngle.Angle90, myDoc2D.enLocation.Vertical_Up)
        dr_Mehanizm("mp_C") = mp_C.ToStringPoint()
        'doc2D.myDrawPoint(mp_C)

        Dim angle_BC As Double = doc2D.myCalculateAngleTwoPoint(mp_B, mp_C)
        dr_Mehanizm("angle_BC") = angle_BC

        mp_D = doc2D.myCalculateCordinatePoint(mp_C, L_CD, angle_BC)
        dr_Mehanizm("mp_D") = mp_D.ToStringPoint()
        'doc2D.myDrawPoint(mp_D)

        mp_S2 = doc2D.myCalculateCordinateCentreSeg(mp_A, mp_B)
        dr_Mehanizm("mp_S2") = mp_S2.ToStringPoint()

        mp_S3 = doc2D.myCalculateCordinateCentreSeg(mp_B, mp_D)
        dr_Mehanizm("mp_S3") = mp_S3.ToStringPoint()

        mp_S4 = mp_C
        dr_Mehanizm("mp_S4") = mp_S4.ToStringPoint()

        dt_Mehanizm.Rows.Add(dr_Mehanizm)

        da_Mehanizm.Update(dt_Mehanizm)

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

        'doc2D.myLineSegDraw(A, B)

        'doc2D.myMtrDelete()


    End Sub

    Public Sub AbsoluteWork()
        Dim lenTest1 As Double = doc2D.myAbsoluteLenghtTwoPoint(meh_Mechanism, mp_A, mp_B)
        Dim newPoint1 As PointDouble = doc2D.myAbsoluteCordinatePoint(meh_Mechanism, mp_C)

        Dim lenTest2 As Double = doc2D.myAbsoluteLenghtTwoPoint(meh_Speed, vp_P, vp_b)
        Dim newPoint2 As PointDouble = doc2D.myAbsoluteCordinatePoint(meh_Speed, vp_P)
    End Sub

End Class
