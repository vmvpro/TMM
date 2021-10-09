Imports Kompas6API5, Kompas6Constants
Imports DB
Imports System.Data.SQLite

Public Class cls_Povsun_5
    Inherits cls_InputData

    ''' <summary>
    ''' Конструктор: принимает ссылку на новый чертеж и v-порядковый номер в таблице lstn
    ''' </summary>
    ''' <param name="ActiveDoc2D">Ссылка на чертеж .*cdw</param>
    ''' <param name="v">Номер задания или варианта</param>
    ''' <remarks></remarks>
    Public Sub New(ActiveDoc2D As ksDocument2D, v As Integer)
        MyBase.New(ActiveDoc2D)
        doc2D = New myDoc2D(ActiveDoc2D)
        'Initialization(v)
    End Sub

    Public Sub New(drZD_Student_ As DataRow)
        'MyBase.New(ActiveDoc2D)
        'doc2D = New myDoc2D(ActiveDoc2D)


        Initialization(drZD_Student_)


    End Sub

    ' Входные данные
    ' Положение начальной точки, где строится наш механизм на чертеже 
    ' задается в произвольном выборе

    Sub Initialization(drZD_Student_ As DataRow)

        drZD_Student = drZD_Student_

        drData = StoredProcedure.RowZD(drZD_Student)
        drDadatok2 = Table.Dodatok2.RowID(drZD_Student("id_dodatok2"))

        Dim dt_z As DataTable = Nothing
        Dim da_z As SQLiteDataAdapter = Nothing

        'Dim dt_lstn As DataTable
        'Dim da_lstn As SQLiteDataAdapter

        DataBase.LoadDataAdapterAndDataTable(TableName.z5, da_z, dt_z)
        'DataBase.LoadDataAdapterAndDataTable(TableName.lstn, da_lstn, dt_lstn)

        'DataBase.Connection().Open()

        'Dim tran As SQLiteTransaction = DataBase.Connection().BeginTransaction()

        'da_lstn.SelectCommand.Transaction = tran

        'Dim dr_lstn As DataRow = DataBase.Row(3, TableName.lstn)

        'counter = dr_lstn("counter") + 1
        'dr_lstn("counter") = counter
        'da_lstn.Update(dt_lstn)

        'da_z3.SelectCommand.Transaction = tran

        'Dim dr_z3 As DataRow = dt_z3.NewRow
        'dr_z3("id") = counter
        'dr_z3("variant") = v
        'dt_z3.Rows.Add(dr_z3)
        'da_z3.Update(dt_z3)

        'tran.Commit()


        'Me.v = v

        'Dim row As DataRow = DataBase.Row(v, TableName.z3_data)

        ' Длины механизма в реальном размере
        counter = drZD_Student("lstn")

        L_AB = drData("L_ab")
        L_BC = drData("L_bc")

        L_CD = 0.25 * L_BC 'L_BD - L_BC

        L_BD = L_BC + L_CD

        'L_a = drData("L_a")

        ml_AB = drZD_Student("ml_AB")
        vl_pb = drZD_Student("vl_pb")
        al_pib = drZD_Student("al_pib")

        w2 = drDadatok2("w2") '34 ' угловая скорость по заданию

        'L_AB = 0.09
        'L_BC = 0.3
        'L_BD = 0.4
        'L_CD = L_BD - L_BC
        'L_a = 0.04


        '================== МЕХАНИЗМ  ===================
        ' Длина AB на плане принимаем = 36  

        'DataBase.LoadDataAdapterAndDataTable(TableName.z3_mehanizm, da_Mehanizm, dt_Mehanizm)
        'da_Mehanizm.SelectCommand.Transaction = tran

        'Dim dr_Mehanizm As DataRow = dt_Mehanizm.NewRow()
        'dr_Mehanizm("id_z3") = counter



        'dr_Mehanizm("ml_AB") = 36
        'da_Mehanizm.Update(dt_Mehanizm)

        'Определяем масштаб механизма
        'mashtab_ml = L_AB / ml_AB
        'dr_Mehanizm("mashtab_ml") = mashtab_ml

        ' Масштаб механизма для конкретного вида на чертеже
        'mashtab_ml_view = ml_AB / L_AB
        'dr_Mehanizm("mashtab_ml_view") = mashtab_ml_view

        ' Длины механизма в масштабе
        'ml_BC = L_BC / mashtab_ml
        'dr_Mehanizm("ml_BC") = ml_BC

        'ml_BD = L_BD / mashtab_ml
        'dr_Mehanizm("ml_BD") = ml_BD

        'ml_CD = L_CD / mashtab_ml
        'dr_Mehanizm("ml_CD") = ml_CD

        'ml_a = L_a / mashtab_ml
        'dr_Mehanizm("ml_a") = ml_a

        'mp_A = New PointDouble(0, 0)
        'dr_Mehanizm("mp_A") = mp_A.ToString()

        'dt_Mehanizm.Rows.Add(dr_Mehanizm)
        'da_Mehanizm.Update(dt_Mehanizm)

        '================== СКОРОСТЬ ===================

        'DataBase.LoadDataAdapterAndDataTable(TableName.z3_Speed, da_Speed, dt_Speed)
        'da_Speed.SelectCommand.Transaction = tran
        'w2 = row("w2") '34 ' угловая скорость по заданию

        'Dim dr_Speed As DataRow = dt_Speed.NewRow()

        'dr_Speed("id_z3") = counter

        'vl_pb = 100 ' на чертеже
        'dr_Speed("vl_pb") = vl_pb


        ' Определение скорости vb
        'vb = w2 * L_AB
        'dr_Speed("vl_pb") = vb

        'Определяем масштаб механизма для скоростей
        'mashtab_mv = vb / vl_pb
        'dr_Speed("mashtab_mv") = mashtab_mv

        ' Масштаб для конкретного вида на чертеже для скоростей
        'mashtab_mv_view = vl_pb / vb
        'dr_Speed("mashtab_mv_view") = mashtab_mv_view

        'dt_Speed.Rows.Add(dr_Speed)
        'da_Speed.Update(dt_Speed)

        ' ================= УСКОРЕНИЕ ===================

        'DataBase.LoadDataAdapterAndDataTable(TableName.z3_acceleration, da_Acceleration, dt_Acceleration)
        'da_Acceleration.SelectCommand.Transaction = tran

        'Dim dr_Acceleration As DataRow = dt_Acceleration.NewRow()

        'dr_Acceleration("id_z3") = counter

        ' Длина вектора ускорения на чертеже
        'al_pb = 104 ' на чертеже
        'dr_Acceleration("al_pb") = al_pb

        ' Определение ускорения ab
        'ab = w2 ^ 2 * L_AB
        'dr_Acceleration("ab") = ab

        'Определяем масштаб механизма для скоростей
        'mashtab_ma = ab / al_pb
        'dr_Acceleration("mashtab_ma") = mashtab_ma

        ' Масштаб для конкретного вида на чертеже ускорения
        'mashtab_ma_view = al_pb / ab
        'dr_Acceleration("mashtab_ma_view") = mashtab_ma_view

        'dt_Acceleration.Rows.Add(dr_Acceleration)
        'da_Acceleration.Update(dt_Acceleration)

        'tran.Commit()

        'DataBase.Connection().Close()

        'DataBase.UpdateDataBase()

        ' ================= СИЛОВОЙ АНАЛИЗ ===================

        m2 = drData("m2")
        m3 = drData("m3")
        m4 = drData("m4")

        F_Fc = drData("Fc")

        sal_Fc = 100 ' на чертеже

        mashtab_msa = F_Fc / sal_Fc

        mashtab_msa_view = sal_Fc / F_Fc


    End Sub

End Class
