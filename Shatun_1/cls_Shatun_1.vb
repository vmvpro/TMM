Imports Kompas6API5, Kompas6Constants
Imports DB
Imports System.Data.SQLite

Public Class cls_Shatun_1
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

        Dim dt_z1 As DataTable = Nothing
        Dim da_z1 As SQLiteDataAdapter = Nothing

        'Dim dt_lstn As DataTable
        'Dim da_lstn As SQLiteDataAdapter

        DataBase.LoadDataAdapterAndDataTable(TableName.z1, da_z1, dt_z1)

        counter = drZD_Student("lstn")

        L_AB = drData("L_ab")
        L_AD = drData("L_ad")
        L_CD = drData("L_cd")
        L_BC = drData("L_bc")
        L_BE = drData("L_be")
        L_CE = L_BC - L_BE

        ml_AB = drZD_Student("ml_AB")
        vl_pb = drZD_Student("vl_pb")
        al_pib = drZD_Student("al_pib")

        w2 = drDadatok2("w2") '34 ' угловая скорость по заданию

        '' Длины механизма в реальном размере
        'L_AB = 0.15
        'L_BC = 0.4
        'L_CD = 0.3
        'L_AD = 0.35
        'L_CE = 0.15 * L_BC

        '' Длина AB на плане принимаем = 36  
        ''ml_AB = 36.0

        ''Определяем масштаб механизма
        'mashtab_ml = L_AB / ml_AB

        '' Масштаб механизма для конкретного вида на чертеже
        'mashtab_ml_view = ml_AB / L_AB

        '' Длины механизма в масштабе
        'ml_BC = L_BC / mashtab_ml
        'ml_CD = L_CD / mashtab_ml
        'ml_AD = L_AD / mashtab_ml
        'ml_CE = L_CE / mashtab_ml

        'mp_A = New PointDouble(0, 0)
        'mp_D = New PointDouble(L_AD, 0)

        '' ================= НАЧАЛО - Входные данные для построения скоростей ===================

        'vp_P = New PointDouble(0, 0)
        'vp_d = New PointDouble(0, 0)

        'w2 = 34 ' угловая скорость по заданию
        'vl_pb = 100 ' на чертеже

        '' Определение скорости vb
        'v_b = w2 * L_AB

        ''Определяем масштаб механизма для скоростей
        'mashtab_mv = v_b / vl_pb

        '' Масштаб для конкретного вида на чертеже
        'mashtab_mv_view = vl_pb / v_b

        '' Длина вектора ускорения на чертеже
        'al_pib = 80 ' на чертеже

        '' Определение ускорения ab
        'a_b = w2 ^ 2 * L_AB

        ''Определяем масштаб механизма для скоростей
        'mashtab_ma = a_b / al_pib

        '' Масштаб для конкретного вида на чертеже ускорения
        'mashtab_ma_view = al_pib / a_b

        ' ================= СИЛОВОЙ АНАЛИЗ ===================


        'm2 = 10 * L_AB ' drData("m2")
        m2 = drData("m2")

        'm3 = 12 * L_CE ' drData("m3")
        m3 = drData("m3")

        'm4 = 15 * L_CD ' drData("m4")
        m4 = drData("m4")

        'drData("m2") = m2
        'drData("m2") = m3
        'drData("m2") = m4


        M_M4 = drData("M_M4")

        sal_M4 = 100 ' на чертеже

        mashtab_msa = M_M4 / sal_M4

        mashtab_msa_view = sal_M4 / M_M4

    End Sub


End Class
