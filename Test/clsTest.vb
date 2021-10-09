Imports Kompas6API5, Kompas6Constants
Public Class clsTest
    Dim kompas As KompasObject
    Dim ActiveDoc2D As ksDocument2D
    Dim DocumParam As ksDocumentParam
    Dim StandartSheet As ksStandartSheet
    Dim SheetPar As ksSheetPar
    Dim str As String
    Dim doc2D As myDoc2D

    Public Sub New(ActiveDoc2D As ksDocument2D)

        doc2D = New myDoc2D(ActiveDoc2D)
    End Sub

    ''' <summary>
    ''' Переменная 
    ''' </summary>
    ''' <remarks></remarks>
    Dim numberPosition As Integer

    ' Входные данные
    ' Положение начальной точки, где строится наш механизм на чертеже 
    ' задается в произвольном выборе

    ''' <summary>
    ''' Координата начальной точки механизма на чертеже задается в произвольном выборе
    ''' </summary>
    ''' <remarks></remarks>
    Private mp_O As PointDouble
    'Private Const planX As Double = 110
    'Private Const planY As Double = 310

    Private sheetWidth = 594
    Private sheetHeight = 420



    ' Длины механизма в реальном размере
    Private L_AB As Double = 0.09
    Private L_BC As Double = 0.3
    Private L_BD As Double = 0.4
    Private L_CD As Double = L_BD - L_BC
    Private L_a As Double = 0.04

    ' Длина AB на плане принимаем = 36  
    Private ml_AB As Double '= 36.0

    'Определяем масштаб механизма
    Private mashtab_ml As Double '= L_AB / ml_AB

    ''' <summary>
    ''' Масштаб механизма для конкретного вида на чертеже
    ''' </summary>
    ''' <remarks></remarks>
    Private mashtab_ml_view As Double '= ml_AB / L_AB

    ''' <summary>
    ''' Вид механизма
    ''' </summary>
    ''' <remarks></remarks>
	Dim meh_SheetView As cls_SheetView

	' Длины механизма в масштабе
	Private ml_BC As Double = L_BC / mashtab_ml
	Private ml_BD As Double = L_BD / mashtab_ml
	Private ml_a As Double = L_a / mashtab_ml

	Private ml_CD As Double = L_CD / mashtab_ml

	'' Начальный угол механизма 
	'''' <summary>
	'''' Определяем угол для крайнего положения механизма
	'''' a / AB+BC
	'''' </summary>
	'''' <remarks></remarks>
	'Private angle_B0 As Double = Math.Asin(L_a / (L_AB + L_BC)) * (180 / Math.PI)
	' = doc2D.myCalculateCordinatePoint(mp_A, L_AB + L_BC, mp_CordinateLineC, 0, myDoc2D.enLocation.quarter_270_360)
	'Private angle_B0 As Double = Math.Asin(L_a / (L_AB + L_BC)) * (180 / Math.PI)

	Private mp_A As PointDouble = New PointDouble(0, 0)
	Private mp_B As PointDouble
	''' <summary>
	''' Точка крайнего положения механизма
	''' </summary>
	''' <remarks></remarks>
	Private mp_C0 As PointDouble

	Private mp_C As PointDouble
	Private mp_D As PointDouble


	' ================= НАЧАЛО - Входные данные для построения скоростей ===================
	'Dim vb_ As Double
	''' <summary>
	''' Создание вида для скоростей
	''' </summary>
	''' <remarks></remarks>
	Dim mehSpeed_SheetView As cls_SheetView

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


	' ================= НАЧАЛО - Входные данные для построения ускорений ===================
	'Dim vb_ As Double
	''' <summary>
	''' Создание вида для Ускорения
	''' </summary>
	''' <remarks></remarks>
	Dim mehAcceleration_SheetView As cls_SheetView

    ''' <summary>
    ''' Длина вектора ускорения на чертеже
    ''' </summary>
    ''' <remarks></remarks>
    Private al_pb As Double = 104 ' на чертеже

    ' Определение ускорения ab
    ''' <summary>
    ''' ab = натуральная величина ускорения точки b
    ''' </summary>
    ''' <remarks></remarks>
    Private ab As Double = w2 ^ 2 * L_AB

    'Определяем масштаб механизма для скоростей
    Private mashtab_ma As Double = ab / al_pb

    ''' <summary>
    ''' Масштаб для конкретного вида на чертеже ускорения
    ''' </summary>
    ''' <remarks></remarks>
    Private mashtab_ma_view As Double = al_pb / ab


    ' ================= КОНЕЦ входных данных скоростей ===================

    ' Точки ускорений
    Private ap_P As PointDouble
    Private ap_b As PointDouble

    ''' <summary>
    ''' точка нормальне прискорення ланки L_BC  
    ''' </summary>
    ''' <remarks></remarks>
    Private ap_n3 As PointDouble

    Private ap_c As PointDouble

    Private ap_d As PointDouble

    Private ap_s2 As PointDouble
    Private ap_s3 As PointDouble
    Private ap_s4 As PointDouble

    ' Длины ускорений

    Private al_bc As Double

    ''' <summary>
    ''' Длина вектора з точки b и n3 
    ''' </summary>
    ''' <remarks></remarks>
    Private al_bn3 As Double

    ''' <summary>
    ''' Длина вектора з точки n3 и c
    ''' </summary>
    ''' <remarks></remarks>
    Private al_n3c As Double

    Private al_cd As Double

    Private al_pc As Double
    Private al_pd As Double

    Private al_ps2 As Double
    Private al_Ps3 As Double
    Private al_Ps4 As Double


End Class
