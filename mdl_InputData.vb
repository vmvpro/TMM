Imports Kompas6API5, Kompas6Constants

Module mdl_InputData
    ' Входные данные
    Public Const planX As Double = 145
    Public Const planY As Double = 270

    Public sheetWidth = 594
    Public sheetHeight = 420

    ' Длины механизма в реальном размере


    ' Длины механизма в масштабе
    Public ml_AB As Double = 30.0
    Public ml_BC As Double = 80.0
    Public ml_CD As Double = 60.0
    Public ml_AD As Double = 70.0
    Public ml_CE As Double = 12.0
    Public ml_BE As Double = 92.0

    ' Начальный угол механизма
    Public angleDegrees As Double = 60

    Public mp_A As PointDouble = New PointDouble(0, 0)
    Public mp_B As PointDouble
    ''' <summary>
    ''' Точка крайнего положения механизма
    ''' </summary>
    ''' <remarks></remarks>
    Public mp_C0 As PointDouble

    Public mp_C As PointDouble
    Public mp_D As PointDouble = New PointDouble(70, 0)
    Public mp_E As PointDouble

    ' Точки скоростей
	Public vp_P As PointDouble
	Public vp_b As PointDouble
	Public vp_c As PointDouble
	Public vp_e As PointDouble
	Public vp_d As PointDouble

	Public vp_s1 As PointDouble
	Public vp_s2 As PointDouble
	Public vp_s3 As PointDouble
	Public vp_s4 As PointDouble
	Public vp_s5 As PointDouble

	' Длины скоростей
	Public vl_pb As Double
	Public vl_bc As Double
	Public vl_ce As Double
	Public vl_pc As Double
	Public vl_pe As Double

	Public vl_Ps1 As Double
	Public vl_Ps2 As Double
	Public vl_Ps3 As Double
	Public vl_Ps4 As Double
	Public vl_Ps5 As Double

    ' Работа план скоростей для 3-положения
    Public viewShetV3 As Integer = -1

End Module
