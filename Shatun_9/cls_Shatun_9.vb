Imports Kompas6API5, Kompas6Constants
Public Class cls_Shatun_9
    Inherits cls_InputData

    Public Sub New(ActiveDoc2D As ksDocument2D)
        MyBase.New(ActiveDoc2D)
        doc2D = New myDoc2D(ActiveDoc2D)
        Initialization()
    End Sub

    ' Входные данные
    ' Положение начальной точки, где строится наш механизм на чертеже 
    ' задается в произвольном выборе

    Sub Initialization()
        ' Длины механизма в реальном размере
        L_AB = 0.15
        L_BC = 0.4
        L_CD = 0.3
        L_AD = 0.35
        L_CE = 0.15 * L_BC

        ' Длина AB на плане принимаем = 36  
        'ml_AB = 36.0

        'Определяем масштаб механизма
        mashtab_ml = L_AB / ml_AB

        ' Масштаб механизма для конкретного вида на чертеже
        mashtab_ml_view = ml_AB / L_AB

        ' Длины механизма в масштабе
        ml_BC = L_BC / mashtab_ml
        ml_CD = L_CD / mashtab_ml
        ml_AD = L_AD / mashtab_ml
        ml_CE = L_CE / mashtab_ml

        mp_A = New PointDouble(0, 0)
        mp_D = New PointDouble(L_AD, 0)

        ' ================= НАЧАЛО - Входные данные для построения скоростей ===================

        vp_P = New PointDouble(0, 0)
        vp_d = New PointDouble(0, 0)

        w2 = 34 ' угловая скорость по заданию
        vl_pb = 100 ' на чертеже

        ' Определение скорости vb
        v_b = w2 * L_AB

        'Определяем масштаб механизма для скоростей
        mashtab_mv = v_b / vl_pb

        ' Масштаб для конкретного вида на чертеже
        mashtab_mv_view = vl_pb / v_b

        ' Длина вектора ускорения на чертеже
        al_pib = 80 ' на чертеже

        ' Определение ускорения ab
        a_b = w2 ^ 2 * L_AB

        'Определяем масштаб механизма для скоростей
        mashtab_ma = a_b / al_pib

        ' Масштаб для конкретного вида на чертеже ускорения
        mashtab_ma_view = al_pib / a_b

    End Sub


End Class
