Imports Kompas6API5, Kompas6Constants
Imports DB
Imports System.Data.SQLite

Public Class cls_InputData
    Protected kompas As KompasObject
    Protected ActiveDoc2D As ksDocument2D
    Protected DocumParam As ksDocumentParam
    Protected StandartSheet As ksStandartSheet
    Protected SheetPar As ksSheetPar
    Protected str As String
    Protected doc2D As myDoc2D

    
    ''' <summary>
    ''' Строка с входными данными передаваемая и формы (у каждого задания своя форма)
    ''' </summary>
    ''' <remarks></remarks>
    Protected drZD_Student As DataRow

    ''' <summary>
    ''' Строка входных данных определена по заданию у студента
    ''' </summary>
    ''' <remarks></remarks>
    Protected drData As DataRow

    ''' <summary>
    ''' Строка с входными данными передаваемая и формы (у каждого задания своя форма)
    ''' </summary>
    ''' <remarks></remarks>
    Protected drDadatok2 As DataRow

    ''' <summary>
    ''' Класс DataBase при инициализации класса
    ''' </summary>
    ''' <remarks></remarks>
    Protected db As DataBase

    ''' <summary>
    ''' Таблица для механизма
    ''' </summary>
    ''' <remarks></remarks>
    Protected dt_Mehanizm As DataTable

    ''' <summary>
    ''' Адаптер для механизма
    ''' </summary>
    ''' <remarks></remarks>
    Protected da_Mehanizm As SQLiteDataAdapter

    ''' <summary>
    ''' Таблица для механизма скоростей
    ''' </summary>
    ''' <remarks></remarks>
    Protected dt_Speed As DataTable

    ''' <summary>
    ''' Таблица для механизма скоростей
    ''' </summary>
    ''' <remarks></remarks>
    Protected da_Speed As SQLiteDataAdapter

    ''' <summary>
    ''' Таблица для механизма ускорения
    ''' </summary>
    ''' <remarks></remarks>
    Protected dt_Acceleration As DataTable

    ''' <summary>
    ''' Адаптер для механизма ускорения
    ''' </summary>
    ''' <remarks></remarks>
    Protected da_Acceleration As SQLiteDataAdapter


    Public Sub New()
        db = New DataBase()

        dt_Mehanizm = DataBase.DictionaryDataTables(TableName.z3_mehanizm)


    End Sub

    Public Sub New(ActiveDoc2D As ksDocument2D)
        doc2D = New myDoc2D(ActiveDoc2D)
        db = New DataBase()
    End Sub

    ''' <summary>
    ''' Счетчик текущей сесии записи в базе данных (по однму ключу с механизмом, скоростью и ускорением)
    ''' </summary>
    ''' <remarks></remarks>
    Protected counter As Integer

    ''' <summary>
    ''' Переменная 
    ''' </summary>
    ''' <remarks></remarks>
    Protected numberPosition As Integer

    ' Входные данные
    ' Положение начальной точки, где строится наш механизм на чертеже 
    ' задается в произвольном выборе

    ''' <summary>
    ''' Координата начальной точки механизма на чертеже задается в произвольном выборе
    ''' </summary>
    ''' <remarks></remarks>
    Protected mp_O As PointDouble
    'Protected Const planX As Double = 110
    'Protected Const planY As Double = 310

    Protected sheetWidth = 594
    Protected sheetHeight = 420

    ''' <summary>
    ''' Вариант механизма по заданию задается в конструкторе каждого механизма (общая такая переменная)
    ''' </summary>
    ''' <remarks></remarks>
    Protected v As Integer

    ' Длины механизма в реальном размере
    Protected L_AB As Double
    Protected L_BC As Double
    Protected L_BD As Double
    Protected L_CD As Double
    Protected L_AD As Double
    Protected L_CE As Double
    Protected L_BE As Double

    Protected L_a As Double

    ''' <summary>
    ''' Длина ланка AB на плане принимаем
    ''' </summary>
    ''' <remarks></remarks>
    Protected ml_AB As Double

    'Определяем масштаб механизма
    Protected mashtab_ml As Double

    ''' <summary>
    ''' Масштаб механизма для конкретного вида на чертеже
    ''' </summary>
    ''' <remarks></remarks>
    Protected mashtab_ml_view As Double

    ''' <summary>
    ''' Вид механизма
    ''' </summary>
    ''' <remarks></remarks>
    Protected meh_Mechanism As cls_SheetView

    ' Длины механизма в масштабе
    Protected ml_BC As Double
    Protected ml_BD As Double
    Protected ml_CD As Double
    Protected ml_BE As Double
    Protected ml_a As Double

    Protected ml_AD As Double
    Protected ml_CE As Double

    '' Начальный угол механизма 
    '''' <summary>
    '''' Определяем угол для крайнего положения механизма
    '''' a / AB+BC
    '''' </summary>
    '''' <remarks></remarks>
    'Protected angle_B0 As Double = Math.Asin(L_a / (L_AB + L_BC)) * (180 / Math.PI)
    ' = doc2D.myCalculateCordinatePoint(mp_A, L_AB + L_BC, mp_CordinateLineC, 0, myDoc2D.enLocation.quarter_270_360)
    'Protected angle_B0 As Double = Math.Asin(L_a / (L_AB + L_BC)) * (180 / Math.PI)

    Protected mp_A As PointDouble
    Protected mp_B As PointDouble

    ''' <summary>
    ''' Точка крайнего положения механизма
    ''' </summary>
    ''' <remarks></remarks>
    Protected mp_B0 As PointDouble

    ''' <summary>
    ''' Точка крайнего положения механизма
    ''' </summary>
    ''' <remarks></remarks>
    Protected mp_C0 As PointDouble

    Protected mp_C As PointDouble
    Protected mp_D As PointDouble
    Protected mp_E As PointDouble

    Protected mp_S1 As PointDouble
    Protected mp_S2 As PointDouble
    Protected mp_S3 As PointDouble
    Protected mp_S4 As PointDouble
    Protected mp_S5 As PointDouble

    ' ================= НАЧАЛО - Входные данные для построения скоростей ===================
    'Dim vb_ As Double
    ''' <summary>
    ''' Создание вида для скоростей
    ''' </summary>
    ''' <remarks></remarks>
    Protected meh_Speed As cls_SheetView

    ''' <summary>
    ''' Длина скорости pb которую мы принимаем самовольно на черетеже (По умолчанию была = 60 мм)
    ''' </summary>
    ''' <remarks></remarks>
    Protected vl_pb As Double


    ''' <summary>
    ''' Определение скорости vb
    ''' </summary>
    ''' <remarks></remarks>
    Protected v_b As Double


    ''' <summary>
    ''' Определение масштаба механизма для скоростей
    ''' </summary>
    ''' <remarks></remarks>
    Protected mashtab_mv As Double

    ''' <summary>
    ''' Масштаб для конкретного вида на чертеже для скоростей
    ''' </summary>
    ''' <remarks></remarks>
    Protected mashtab_mv_view As Double


    ' ================= КОНЕЦ входных данных скоростей ===================

    ' Точки скоростей
    ''' <summary>
    ''' Точка скорости
    ''' </summary>
    ''' <remarks></remarks>
    Protected vp_P As PointDouble

    ''' <summary>
    ''' Точка скорости
    ''' </summary>
    ''' <remarks></remarks>
    Protected vp_b As PointDouble

    ''' <summary>
    ''' Точка скорости
    ''' </summary>
    ''' <remarks></remarks>
    Protected vp_c As PointDouble

    ''' <summary>
    ''' Точка скорости
    ''' </summary>
    ''' <remarks></remarks>
    Protected vp_e As PointDouble

    ''' <summary>
    ''' Точка скорости
    ''' </summary>
    ''' <remarks></remarks>
    Protected vp_d As PointDouble

    ''' <summary>
    ''' Точка скорости s 
    ''' </summary>
    ''' <remarks></remarks>
    Protected vp_s1 As PointDouble

    ''' <summary>
    ''' Точка скорости s 
    ''' </summary>
    ''' <remarks></remarks>
    Protected vp_s2 As PointDouble

    ''' <summary>
    ''' Точка скорости s 
    ''' </summary>
    ''' <remarks></remarks>
    Protected vp_s3 As PointDouble

    ''' <summary>
    ''' Точка скорости s 
    ''' </summary>
    ''' <remarks></remarks>
    Protected vp_s4 As PointDouble

    ''' <summary>
    ''' Точка скорости s 
    ''' </summary>
    ''' <remarks></remarks>
    Protected vp_s5 As PointDouble

    ' Длины скоростей

    ''' <summary>
    ''' Натуральная скорость направления механизма
    ''' </summary>
    ''' <remarks></remarks>
    Protected v_bc As Double

    ''' <summary>
    ''' Скорость в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected vl_bc As Double

    '---------------------------------------

    ''' <summary>
    ''' Натуральная скорость направления механизма
    ''' </summary>
    ''' <remarks></remarks>
    Protected v_dc As Double

    ''' <summary>
    ''' Скорость в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected vl_dc As Double

    '---------------------------------------
    ''' <summary>
    ''' Натуральная скорость направления механизма
    ''' </summary>
    ''' <remarks></remarks>
    Protected v_be As Double

    ''' <summary>
    ''' Скорость в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected vl_be As Double

    '---------------------------------------

    ''' <summary>
    ''' Натуральная скорость направления механизма
    ''' </summary>
    ''' <remarks></remarks>
    Protected v_c As Double

    ''' <summary>
    ''' Скорость в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected vl_c As Double

    '---------------------------------------

    ''' <summary>
    ''' Натуральная скорость направления механизма
    ''' </summary>
    ''' <remarks></remarks>
    Protected v_e As Double

    ''' <summary>
    ''' Скорость в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected vl_e As Double

    '---------------------------------------

    ''' <summary>
    ''' Натуральная скорость направления механизма
    ''' </summary>
    ''' <remarks></remarks>
    Protected v_ce As Double

    ''' <summary>
    ''' Скорость в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected vl_ce As Double

    '---------------------------------------

    ''' <summary>
    ''' Натуральная скорость направления механизма
    ''' </summary>
    ''' <remarks></remarks>
    Protected v_pc As Double

    ''' <summary>
    ''' Скорость в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected vl_pc As Double

    '---------------------------------------

    ''' <summary>
    ''' Натуральная скорость направления механизма
    ''' </summary>
    ''' <remarks></remarks>
    Protected v_pe As Double

    ''' <summary>
    ''' Скорость в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected vl_pe As Double


    '---------------------------------------

    ''' <summary>
    ''' Натуральная скорость направления механизма
    ''' </summary>
    ''' <remarks></remarks>
    Protected v_cd As Double

    ''' <summary>
    ''' Скорость в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected vl_cd As Double

    '---------------------------------------

    ''' <summary>
    ''' Натуральная скорость направления механизма
    ''' </summary>
    ''' <remarks></remarks>
    Protected v_bd As Double

    ''' <summary>
    ''' Скорость в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected vl_bd As Double

    '---------------------------------------

    ''' <summary>
    ''' Натуральная скорость направления механизма
    ''' </summary>
    ''' <remarks></remarks>
    Protected v_d As Double

    ''' <summary>
    ''' Скорость в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected vl_d As Double

    '---------------------------------------

    ''' <summary>
    ''' Натуральная скорость ps направления механизма 
    ''' </summary>
    ''' <remarks></remarks>
    Protected v_s1 As Double

    ''' <summary>
    ''' Скорость в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected vl_s1 As Double

    '---------------------------------------

    ''' <summary>
    ''' Натуральная скорость ps направления механизма 
    ''' </summary>
    ''' <remarks></remarks>
    Protected v_s2 As Double

    ''' <summary>
    ''' Скорость в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected vl_s2 As Double

    '---------------------------------------

    ''' <summary>
    ''' Натуральная скорость ps направления механизма 
    ''' </summary>
    ''' <remarks></remarks>
    Protected v_s3 As Double

    ''' <summary>
    ''' Скорость в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected vl_s3 As Double

    '---------------------------------------

    ''' <summary>
    ''' Натуральная скорость ps направления механизма 
    ''' </summary>
    ''' <remarks></remarks>
    Protected v_s4 As Double

    ''' <summary>
    ''' Скорость в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected vl_s4 As Double

    '---------------------------------------

    ''' <summary>
    ''' Натуральная скорость ps направления механизма 
    ''' </summary>
    ''' <remarks></remarks>
    Protected v_s5 As Double

    ''' <summary>
    ''' Скорость в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected vl_s5 As Double

    '---------------------------------------

    ' ================= НАЧАЛО - Входные данные для построения ускорений ===================
    'Dim vb_ As Double
    ''' <summary>
    ''' Создание вида для Ускорения
    ''' </summary>
    ''' <remarks></remarks>
    Protected meh_Acceleration As cls_SheetView

    ''' <summary>
    ''' Длина вектора ускорения на чертеже
    ''' </summary>
    ''' <remarks></remarks>
    Protected al_pib As Double ' на чертеже

    ' Определение ускорения ab
    ''' <summary>
    ''' ab = натуральная величина ускорения точки b
    ''' </summary>
    ''' <remarks></remarks>
    Protected a_b As Double

    'Определяем масштаб механизма для скоростей
    Protected mashtab_ma As Double

    ''' <summary>
    ''' Масштаб для конкретного вида на чертеже ускорения
    ''' </summary>
    ''' <remarks></remarks>
    Protected mashtab_ma_view As Double


    ' ================= КОНЕЦ входных данных скоростей ===================

    ' Точки ускорений

    ''' <summary>
    ''' Точка ускорения
    ''' </summary>
    ''' <remarks></remarks>
    Protected ap_pi As PointDouble

    ''' <summary>
    ''' Точка ускорения
    ''' </summary>
    ''' <remarks></remarks>
    Protected ap_b As PointDouble

    ''' <summary>
    ''' точка нормальне прискорення ланки L_BC  
    ''' </summary>
    ''' <remarks></remarks>
    Protected ap_n3 As PointDouble

    ''' <summary>
    ''' точка нормальне прискорення ланки L_BC  
    ''' </summary>
    ''' <remarks></remarks>
    Protected ap_n4 As PointDouble



    Protected ap_c As PointDouble

    Protected ap_d As PointDouble
    Protected ap_e As PointDouble

    Protected ap_s2 As PointDouble
    Protected ap_s3 As PointDouble
    Protected ap_s4 As PointDouble

    ' Длины ускорений

    ''' <summary>
    ''' Натуральная величина ускорения bc
    ''' </summary>
    ''' <remarks></remarks>
    Protected a_bc As Double

    ''' <summary>
    ''' Ускорение в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected al_bc As Double

    '---------------------------------------

    ''' <summary>
    ''' Натуральная величина ускорения bc
    ''' </summary>
    ''' <remarks></remarks>
    Protected a_dc As Double

    ''' <summary>
    ''' Ускорение в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected al_dc As Double

    '---------------------------------------

    ''' <summary>
    ''' Натуральная величина ускорения bc
    ''' </summary>
    ''' <remarks></remarks>
    Protected a_e As Double

    ''' <summary>
    ''' Ускорение в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected al_e As Double

    '---------------------------------------

    ''' <summary>
    ''' Натуральная величина ускорения bc
    ''' </summary>
    ''' <remarks></remarks>
    Protected a_de As Double

    ''' <summary>
    ''' Ускорение в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected al_de As Double

    '---------------------------------------

    ''' <summary>
    ''' Натуральная величина ускорения bc
    ''' </summary>
    ''' <remarks></remarks>
    Protected a_c As Double

    ''' <summary>
    ''' Ускорение в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected al_c As Double

    '---------------------------------------

    ''' <summary>
    ''' Натуральная величина ускорения з точки b и n3 
    ''' </summary>
    ''' <remarks></remarks>
    Protected a_n3 As Double

    ''' <summary>
    ''' Ускорение в масштабе з точки b и n3 
    ''' </summary>
    ''' <remarks></remarks>
    Protected al_n3 As Double

    '---------------------------------------

    Protected a_n4 As Double
    Protected al_n4 As Double


    ''' <summary>
    ''' Натуральная величина ускорения з точки n3 и c
    ''' </summary>
    ''' <remarks></remarks>
    Protected a_n3c As Double

    ''' <summary>
    ''' Ускорение в масштабе з точки n3 и c
    ''' </summary>
    ''' <remarks></remarks>
    Protected al_n3c As Double

    '----------------------------------------



    Protected absoluteLenght_bn3 As Double

    Protected absoluteLenght_pn4 As Double

    ''' <summary>
    ''' Натуральная величина ускорения cd
    ''' </summary>
    ''' <remarks></remarks>
    Protected a_cd As Double

    ''' <summary>
    ''' Ускорение в масштабе 
    ''' </summary>
    ''' <remarks></remarks>
    Protected al_cd As Double

    '---------------------------------------------

    ''' <summary>
    ''' Натуральная величина ускорения pc
    ''' </summary>
    ''' <remarks></remarks>
    Protected a_pc As Double

    ''' <summary>
    ''' Ускорение в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected al_pc As Double

    '----------------------------------------------

    ''' <summary>
    ''' Натуральная величина ускорения 
    ''' </summary>
    ''' <remarks></remarks>
    Protected a_pd As Double

    ''' <summary>
    ''' Ускорение в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected al_pd As Double

    '---------------------------------------------

    ''' <summary>
    ''' Натуральная величина ускорения 
    ''' </summary>
    ''' <remarks></remarks>
    Protected a_ce As Double

    ''' <summary>
    ''' Ускорение в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected al_ce As Double

    '-----------------------------------------------

    ''' <summary>
    ''' Натуральная величина ускорения 
    ''' </summary>
    ''' <remarks></remarks>
    Protected a_be As Double

    ''' <summary>
    ''' Ускорение в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected al_be As Double

    '-----------------------------------------------


    ''' <summary>
    ''' Натуральная величина ускорения 
    ''' </summary>
    ''' <remarks></remarks>
    Protected a_bd As Double

    ''' <summary>
    ''' Ускорение в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected al_bd As Double

    '-----------------------------------------------

    ''' <summary>
    ''' Натуральная величина ускорения 
    ''' </summary>
    ''' <remarks></remarks>
    Protected a_d As Double

    ''' <summary>
    ''' Ускорение в масштабе
    ''' </summary>
    ''' <remarks></remarks>
    Protected al_d As Double

    '----------------------------------------------

    Protected a_s2 As Double
    Protected al_s2 As Double

    Protected a_s3 As Double
    Protected al_s3 As Double

    Protected a_s4 As Double
    Protected al_s4 As Double

    '-------------------------------------------------------------
    Protected m2 As Double
    Protected m3 As Double
    Protected m4 As Double
    Protected m5 As Double

    ''' <summary>
    ''' Сила по заданию
    ''' </summary>
    ''' <remarks></remarks>
    Protected F_Fc As Double

    ''' <summary>
    ''' Момент на ланке 
    ''' </summary>
    ''' <remarks></remarks>
    Protected M_M3 As Double

    ''' <summary>
    ''' Момент по заданию
    ''' </summary>
    ''' <remarks></remarks>
    Protected M_M4 As Double

    ''' <summary>
    ''' Угловая скорость по заданию
    ''' </summary>
    ''' <remarks></remarks>
    Protected w2 As Double

    ''' <summary>
    ''' Угловоя скорость
    ''' </summary>
    ''' <remarks></remarks>
    Protected w3 As Double

    ''' <summary>
    ''' Угловоя скорость
    ''' </summary>
    ''' <remarks></remarks>
    Protected w4 As Double

    Protected delta As Double

    '---------------------------------------------------------------

    'Dim vb_ As Double
    ''' <summary>
    ''' Создание вида для силового анализа 
    ''' </summary>
    ''' <remarks></remarks>
    Protected meh_PowerAnalysis_23 As cls_SheetView
    Protected meh_PowerAnalysis_12 As cls_SheetView

    Protected meh_PowerAnalysis As cls_SheetView

    ''' <summary>
    ''' Масштаб для конкретного вида на чертеже ускорения
    ''' </summary>
    ''' <remarks></remarks>
    Protected mashtab_msa_view As Double

    Protected mashtab_msa As Double

    Protected sal_Fc As Double ' на чертеже
    Protected sal_M4 As Double ' на чертеже

    Protected G2 As Double
    Protected G3 As Double
    Protected G4 As Double
    Protected G5 As Double

    ''' <summary>
    ''' Момент инерции
    ''' </summary>
    ''' <remarks></remarks>
    Protected J2 As Double

    ''' <summary>
    ''' Момент инерции
    ''' </summary>
    ''' <remarks></remarks>
    Protected J3 As Double

    ''' <summary>
    ''' Момент инерции
    ''' </summary>
    ''' <remarks></remarks>
    Protected J4 As Double

    ''' <summary>
    ''' Момент инерции
    ''' </summary>
    ''' <remarks></remarks>
    Protected J5 As Double


    ''' <summary>
    ''' Угловое ускорение
    ''' </summary>
    ''' <remarks></remarks>
    Protected e2 As Double

    ''' <summary>
    ''' Угловое ускорение
    ''' </summary>
    ''' <remarks></remarks>
    Protected e3 As Double

    ''' <summary>
    ''' Угловое ускорение
    ''' </summary>
    ''' <remarks></remarks>
    Protected e4 As Double

    ''' <summary>
    ''' Сила инерции: m2 * as2 (Переменная заменена с Ф2)
    ''' </summary>
    ''' <remarks></remarks>
    Protected Fi2 As Double

    ''' <summary>
    ''' Сила инерции: m3 * as3 (Переменная заменена с Ф3)
    ''' </summary>
    ''' <remarks></remarks>
    Protected Fi3 As Double

    ''' <summary>
    ''' Сила инерции: m4 * as4 (Переменная заменена с Ф4)
    ''' </summary>
    ''' <remarks></remarks>
    Protected Fi4 As Double

    ''' <summary>
    ''' Сила инерции: m5 * as5 (Переменная заменена с Ф5)
    ''' </summary>
    ''' <remarks></remarks>
    Protected Fi5 As Double

    ''' <summary>
    ''' Длина растояние на плане ускорения от точки π до s2. Длина сразу берется в масштабе вида
    ''' </summary>
    ''' <remarks></remarks>
    Protected al_as2 As Double

    ''' <summary>
    ''' Длина растояние на плане ускорения от точки π до s3. Длина сразу берется в масштабе вида
    ''' </summary>
    ''' <remarks></remarks>
    Protected al_as3 As Double

    ''' <summary>
    ''' Длина растояние на плане ускорения от точки π до s4. Длина сразу берется в масштабе вида
    ''' </summary>
    ''' <remarks></remarks>
    Protected al_as4 As Double

    ''' <summary>
    ''' Размерность массива для механизма = 8
    ''' </summary>
    ''' <remarks></remarks>
    Protected n_ As Double = 8

    ''' <summary>
    ''' Проекция  ps2 штрих для Мзв
    ''' </summary>
    ''' <remarks></remarks>
    Protected ps2_v(n_) As Double

    ''' <summary>
    ''' Проекция  ps3 штрих для Мзв
    ''' </summary>
    ''' <remarks></remarks>
    Protected ps3_v(n_) As Double

    ''' <summary>
    ''' Проекция  ps4 штрих для Мзв
    ''' </summary>
    ''' <remarks></remarks>
    Protected ps4_v(n_) As Double

    ''' <summary>
    ''' Проекция  ps5 штрих для Мзв
    ''' </summary>
    ''' <remarks></remarks>
    Protected ps5_v(n_) As Double

    ''' <summary>
    ''' длина-массив cd штрих для Мзв
    ''' </summary>
    ''' <remarks></remarks>
    Protected cd_v(n_) As Double

    ''' <summary>
    ''' длина-массив cd штрих для Мзв
    ''' </summary>
    ''' <remarks></remarks>
    Protected bd_v(n_) As Double


    ''' <summary>
    ''' Сила зведенного момента
    ''' </summary>
    ''' <remarks></remarks>
    Protected Mzv(n_) As Double

    '---------------------------------------

    ''' <summary>
    ''' Размер cb для Iзв
    ''' </summary>
    ''' <remarks></remarks>
    Protected bc_a(n_) As Double

    ''' <summary>
    ''' Размер dc для Iзв
    ''' </summary>
    ''' <remarks></remarks>
    Protected cd_a(n_) As Double

    ''' <summary>
    ''' Размер  ps2 для Iзв
    ''' </summary>
    ''' <remarks></remarks>
    Protected ps2_a(n_) As Double

    ''' <summary>
    ''' Размер  ps3 для Iзв
    ''' </summary>
    ''' <remarks></remarks>
    Protected ps3_a(n_) As Double

    ''' <summary>
    ''' Размер  ps4 для Iзв
    ''' </summary>
    ''' <remarks></remarks>
    Protected ps4_a(n_) As Double

    ''' <summary>
    ''' Размер  ps5 для Iзв
    ''' </summary>
    ''' <remarks></remarks>
    Protected ps5_a(n_) As Double

    ''' <summary>
    ''' Сила инерциия
    ''' </summary>
    ''' <remarks></remarks>
    Protected Izv(n_) As Double

    Sub CreateTable_Mehanizm()
        Dim col As New DataColumn("")


    End Sub

End Class
