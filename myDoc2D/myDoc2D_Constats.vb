Imports System.Runtime.CompilerServices
Imports Kompas6API5, Kompas6Constants

Partial Public Class myDoc2D

    ''' <summary>
    ''' Перечисление Режим симметрии
    ''' </summary>
    ''' <remarks></remarks>
    Enum enMode
        ''' <summary>
        ''' Вырезать
        ''' </summary>
        ''' <remarks></remarks>
        Cute = 0

        ''' <summary>
        ''' Копировать
        ''' </summary>
        ''' <remarks></remarks>
        Copy = 1
    End Enum

    ''' <summary>
    ''' Перечисление Размещения точки вокруг, которой необходимо определить вектор направления
    ''' </summary>
    ''' <remarks></remarks>
    Enum enPointPlacement
        ''' <summary>
        ''' Точка находится - слева
        ''' </summary>
        ''' <remarks></remarks>
        Left = -1

        ''' <summary>
        ''' Точка находится - справа
        ''' </summary>
        ''' <remarks></remarks>
        Right = 1
    End Enum
    Enum enLocation
        ''' <summary>
        ''' Условие на то, что расположение точки находится на оси 'x' Правее относительной точки 'A'
        ''' </summary>
        ''' <remarks></remarks>
        Horizontal_Right = 1

        ''' <summary>
        ''' Условие на то, что расположение точки находится на оси 'x' левее относительной точки 'A'
        ''' </summary>
        ''' <remarks></remarks>
        Horizontal_Left

        ''' <summary>
        ''' Условие на то, что расположение точки находится на оси 'y' выше относительной точки 'A'
        ''' </summary>
        ''' <remarks></remarks>
        Vertical_Up

        ''' <summary>
        ''' Условие на то, что расположение точки находится на оси 'y' ниже относительной точки 'A'
        ''' </summary>
        ''' <remarks></remarks>
        Vertical_Down

        ''' <summary>
        ''' Условие на то, что искомоя точка находится в четверти между 0 и 90
        ''' </summary>
        ''' <remarks></remarks>
        quarter_0_90

        ''' <summary>
        ''' Условие на то, что искомоя точка находится в четверть между 90 и 180
        ''' </summary>
        ''' <remarks></remarks>
        quarter_90_180

        ''' <summary>
        ''' Условие на то, что искомоя точка находится в четверть между 180 и 270
        ''' </summary>
        ''' <remarks></remarks>
        quarter_180_270

        ''' <summary>
        ''' Условие на то, что искомоя точка находится в четверть между 270 и 360
        ''' </summary>
        ''' <remarks></remarks>
        quarter_270_360


    End Enum

End Class
