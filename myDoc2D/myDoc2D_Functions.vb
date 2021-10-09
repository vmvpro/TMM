Imports System.Runtime.CompilerServices
Imports Kompas6API5, Kompas6Constants

Partial Public Class myDoc2D

#Region "Пример создания полилинии одной функцией (Только для кулачка)"
    Public Function myDrawPolylineCam(pd As PointDouble()) As Integer
        ' Пример создания полилинии одной функцией
        Dim par As Kompas6API5.PolylineParam ' Интерфейс ksPolylineParam
        par = kompas.GetParamStruct(Kompas6Constants.StructType2DEnum.ko_PolylineParam) ' Интерфейс параметров ломаной линии

        Dim pr As Kompas6API5.MathPointParam ' Интерфейс ksMathPointParam
        pr = kompas.GetParamStruct(Kompas6Constants.StructType2DEnum.ko_MathPointParam) ' Интерфейс параметров математической точки

        Dim arr As Kompas6API5.DynamicArray
        Dim p As Integer ' Интерфейс ksDynamicArray
        If Not par Is Nothing And Not pr Is Nothing Then ' Интерфейсы созданы
            par.Init() ' Инициализация
            pr.Init() ' Инициализация

            arr = par.GetpMathPoint ' Получить интерфейс динамического массив математических точек

            If Not arr Is Nothing Then ' Интерфейс получен

                Dim pp As PointDouble

                For i As Integer = 0 To pd.Length - 1

                    Try
                        pp = New PointDouble(pd(i).X, pd(i).Y)

                        pr.x = pp.X ' 1-я точка
                        pr.y = pp.Y
                        arr.ksAddArrayItem(-1, pr) ' Добавим точку в конец массива
                    Catch ex As Exception
                        Debug.Print(ex.Message)
                    End Try




                Next

                par.style = 1 ' Cтиль линии ( 1 - основная, 2 - тонкая, 3 - осевая, 4 - штриховая,
                ' 5 - волнистая, 6 - утолщенная, 7 - штрихпунктирная с двумя точками,
                ' 8 - осевая основная, 9 - штриховая основная, 10 - осевая толстая,
                ' 11 - тонкая, включаемая в штриховку )

                p = doc2D.ksPolylineByParam(par) ' Создание ломаной

                'doc2D.ksLightObj(p, 1) ' Подсветить полилинию
                'kompas.ksMessage("Полилиния")
                'doc2D.ksLightObj(p, 0) ' Снять выделение полилинии

                arr.ksDeleteArray() ' Удалить массив точек

                arr = Nothing
            End If

            par = Nothing

            pr = Nothing
        End If

        Return p

    End Function
#End Region

#Region "Пример создания полилинии одной функцией"
    Public Function myDrawPolyline(pd As PointDouble()) As Integer
        ' Пример создания полилинии одной функцией
        Dim par As Kompas6API5.PolylineParam ' Интерфейс ksPolylineParam
        par = kompas.GetParamStruct(Kompas6Constants.StructType2DEnum.ko_PolylineParam) ' Интерфейс параметров ломаной линии

        Dim pr As Kompas6API5.MathPointParam ' Интерфейс ksMathPointParam
        pr = kompas.GetParamStruct(Kompas6Constants.StructType2DEnum.ko_MathPointParam) ' Интерфейс параметров математической точки

        Dim arr As Kompas6API5.DynamicArray
        Dim p As Integer ' Интерфейс ksDynamicArray
        If Not par Is Nothing And Not pr Is Nothing Then ' Интерфейсы созданы
            par.Init() ' Инициализация
            pr.Init() ' Инициализация

            arr = par.GetpMathPoint ' Получить интерфейс динамического массив математических точек

            If Not arr Is Nothing Then ' Интерфейс получен

                Dim pp As PointDouble

                For i As Integer = 1 To pd.Length - 1
                    pp = New PointDouble(pd(i).X, pd(i).Y)

                    pr.x = pp.X ' 1-я точка
                    pr.y = pp.Y
                    arr.ksAddArrayItem(-1, pr) ' Добавим точку в конец массива
                Next

                par.style = 1 ' Cтиль линии ( 1 - основная, 2 - тонкая, 3 - осевая, 4 - штриховая,
                ' 5 - волнистая, 6 - утолщенная, 7 - штрихпунктирная с двумя точками,
                ' 8 - осевая основная, 9 - штриховая основная, 10 - осевая толстая,
                ' 11 - тонкая, включаемая в штриховку )

                p = doc2D.ksPolylineByParam(par) ' Создание ломаной

                'doc2D.ksLightObj(p, 1) ' Подсветить полилинию
                'kompas.ksMessage("Полилиния")
                'doc2D.ksLightObj(p, 0) ' Снять выделение полилинии

                arr.ksDeleteArray() ' Удалить массив точек

                arr = Nothing
            End If

            par = Nothing

            pr = Nothing
        End If

        Return p

    End Function
#End Region


#Region "   Метод для отрисовки вспомогательной прямой по двум точкам   "

    ''' <summary>
    ''' Метод для отрисовки вспомогательной прямой по двум точкам
    ''' </summary>
    ''' <param name="P1">Координата точки</param>
    ''' <param name="P2">Координата точки</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function myDrawLine(P1 As PointDouble, P2 As PointDouble) As Integer
        Dim newPointDouble As New PointDouble
        Dim angle As Double = myCalculateAngleTwoPoint(P1, P2)
        Dim Ap As ksMathPointParam

        Ap = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam) 'StructType2DEnum.ko_MathPointParam)

        Dim l1 As Integer = doc2D.ksLine(P1.X, P1.Y, angle)

        Return l1

    End Function

#End Region

#Region "   Метод для отрисовки вспомогательной прямой на чертеже в заданной точке и углом   "

    ''' <summary>
    ''' Метод для отрисовки вспомогательной прямой на чертеже в заданной точке и угла
    ''' </summary>
    ''' <param name="P1">Точка в которой начинается прямая</param>
    ''' <param name="angle">Угол прямой</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function myDrawLine(P1 As PointDouble, angle As Double) As Integer
        Dim newPointDouble As New PointDouble

        Dim Ap As ksMathPointParam

        Ap = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam) 'StructType2DEnum.ko_MathPointParam)

        Dim l1 As Integer = doc2D.ksLine(P1.X, P1.Y, angle)

        Return l1

    End Function

#End Region

#Region "   Метод для отрисовки точки на чертеже в заданной точке   "

    ''' <summary>
    ''' Метод для отрисовки точки на чертеже в заданной точке
    ''' </summary>
    ''' <param name="P"></param>
    ''' <remarks></remarks>
    Public Function myDrawPoint(P As PointDouble, Optional stylePoint As Integer = 0) As Integer
        Return doc2D.ksPoint(P.X, P.Y, stylePoint)
    End Function

#End Region

#Region "   Метод для отрисовки отрезка по объекту cls_Segment"
    ''' <summary>
    ''' Метод для отрисовки отрезка по объекту cls_Segment
    ''' </summary>
    ''' <param name="segment"></param>
    ''' <param name="style"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function myDrawLineSeg(segment As cls_Segment, Optional style As Integer = 1) As Integer
        Return doc2D.ksLineSeg(segment.T1.X, segment.T1.Y, segment.T2.X, segment.T2.Y, style)
    End Function
#End Region

#Region "   Метод для отрисовки отрезка по заданным точкам на чертеже   "
    ''' <summary>
    ''' Метод для отрисовки отрезка по двум точкам
    ''' </summary>
    ''' <param name="P1">точка 1</param>
    ''' <param name="P2">точка 2</param>
    ''' <param name="style"></param>
    ''' <remarks></remarks>
    Public Function myDrawLineSeg(P1 As PointDouble, P2 As PointDouble, Optional style As Integer = 1) As Integer
        Return doc2D.ksLineSeg(P1.X, P1.Y, P2.X, P2.Y, style)
    End Function
#End Region

#Region "   Метод для отрисовки вектора направления по стрелке   "

    ''' <summary>
    ''' Метод для отрисовки вектора направления по стрелке
    ''' </summary>
    ''' <param name="P1"></param>
    ''' <param name="P2"></param>
    ''' <param name="style"></param>
    ''' <remarks></remarks>
    Public Function myDrawVector(P1 As PointDouble, P2 As PointDouble, Optional style As Integer = 1) As Integer
        'Dim pArray As ksDynamicArray
        Dim iMathPointArray As ksDynamicArray
        Dim iMathPointParam As ksMathPointParam
        Dim iPolylineArray As ksDynamicArray

        Dim len As Double = doc2D.myCalculateLenghtTwoPoint(P1, P2)

        'If len < 6 Then
        ' doc2D.myDrawLineSeg(P1, P2, 2)
        'Exit Sub
        'End If

        Dim iLeaderParam As ksLeaderParam = kompas.GetParamStruct(StructType2DEnum.ko_LeaderParam)

        iLeaderParam.Init()
        iLeaderParam.around = 0
        iLeaderParam.arrowType = 2
        iLeaderParam.cText0 = 0
        iLeaderParam.cText1 = 0
        iLeaderParam.cText2 = 0
        iLeaderParam.cText3 = 0
        iLeaderParam.dirX = 1
        iLeaderParam.signType = 0
        iLeaderParam.x = P1.X
        iLeaderParam.y = P1.Y

        iPolylineArray = iLeaderParam.GetpPolyline

        '--------Эта часть повтряется для последующих точек-------------------
        iMathPointArray = kompas.GetDynamicArray(POINT_ARR)
        iMathPointParam = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam)
        iMathPointParam.Init()
        iMathPointParam.x = P2.X
        iMathPointParam.y = P2.Y
        iMathPointArray.ksAddArrayItem(-1, iMathPointParam)
        iPolylineArray.ksAddArrayItem(-1, iMathPointArray)
        ' Если добавлять точки то добавлятьь только этот код
        '---------------------------------------------------------------------


        Dim iTextLineArray As ksDynamicArray = iLeaderParam.GetpTextline

        iLeaderParam.SetpTextline(iTextLineArray)

        Return doc2D.ksLeader(iLeaderParam)
    End Function

#End Region

#Region "   Метод для отрисовки отрезка по относительной точке, длине и заданному углу   "

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="P1"></param>
    ''' <param name="lenght"></param>
    ''' <param name="angle"></param>
    ''' <param name="style"></param>
    ''' <remarks></remarks>
    Public Function myDrawLineSeg(P1 As PointDouble, lenght As Double, angle As Double, Optional style As Integer = 1) As PointDouble
        doc2D.myMtr(P1)

        Dim tmp_PointDouble As New PointDouble

        kompas = kompasApp()

        Dim Mathematic2D As ksMathematic2D
        Mathematic2D = kompas.GetMathematic2D()

        'Dim pArray As ksMathPointParam
        'pArray = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam)

        Dim pArray As ksDynamicArray
        pArray = kompas.GetDynamicArray(POINT_ARR)

        Dim par As ksMathPointParam = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam)

        Dim circle1 As ksCircleParam
        circle1 = kompas.GetParamStruct(StructType2DEnum.ko_CircleParam)
        circle1.xc = P1.X : circle1.yc = P1.Y : circle1.rad = lenght
        Dim c1 As Integer = doc2D.ksCircle(circle1.xc, circle1.yc, circle1.rad, 1)

        Dim line1 As ksLineParam
        line1 = kompas.GetParamStruct(StructType2DEnum.ko_LineParam)
        line1.x = P1.X : line1.y = P1.Y : line1.angle = angle
        Dim l1 As Integer = doc2D.ksLine(line1.x, line1.y, line1.angle)

        Mathematic2D.ksIntersectCurvCurv(c1, l1, pArray)

        doc2D.ksDeleteObj(c1)
        doc2D.ksDeleteObj(l1)

        pArray.ksGetArrayItem(0, par)

        doc2D.ksLineSeg(P1.X, P1.Y, par.x, par.y, style)

        tmp_PointDouble.X = par.x
        tmp_PointDouble.Y = par.y

        doc2D.myMtrDelete()

        Return tmp_PointDouble
    End Function

#End Region

#Region "   Метод для отрисовки текста в графическом документе   "
    ''' <summary>
    ''' Метод для отрисовки текста в графическом документе
    ''' </summary>
    ''' <param name="pointDouble"></param>
    ''' <param name="heightText"></param>
    ''' <param name="text"></param>
    ''' <param name="offsetX"></param>
    ''' <param name="offsetY"></param>
    ''' <remarks></remarks>
    Sub myDrawText(pointDouble As PointDouble, text As String, Optional heightText As Double = 5, Optional offsetX As Double = 0, Optional offsetY As Double = 0)
        doc2D.ksText(pointDouble.X + offsetX, pointDouble.Y + offsetY, 0, heightText, 0, 0, text)
    End Sub
#End Region

#Region "   Метод для отрисовки текста с воможностью проставления верхнего и нижнего индексов"

    ''' <summary>
    ''' Метод для отрисовки текста с воможностью проставления верхнего и нижнего индексов
    ''' </summary>
    ''' <param name="T1">Простановка точки</param>
    ''' <param name="text">Текст</param>
    ''' <param name="textIndexUp">Верхний индекс</param>
    ''' <param name="textIndexDown">Нижний индекс</param>
    ''' <remarks></remarks>
    Public Function myDrawTextIndex(T1 As PointDouble, text As String, Optional textIndexUp As String = "", Optional textIndexDown As String = "", Optional height As Double = 5) As Integer
        Dim iTextLineParam As ksTextLineParam
        Dim iTextItemArray As ksDynamicArray
        Dim iTextItemParam As ksTextItemParam
        Dim iTextItemFontParam As Object

        Dim iParagraphParam As ksParagraphParam = kompas.GetParamStruct(StructType2DEnum.ko_ParagraphParam)
        iParagraphParam.Init()
        iParagraphParam.x = T1.X
        iParagraphParam.y = T1.Y
        iParagraphParam.ang = 0.0
        iParagraphParam.height = 30
        iParagraphParam.width = 50
        iParagraphParam.hFormat = 0
        iParagraphParam.vFormat = 0
        iParagraphParam.style = 1

        doc2D.ksParagraph(iParagraphParam)

        iTextLineParam = kompas.GetParamStruct(StructType2DEnum.ko_TextLineParam)
        iTextLineParam.Init()
        iTextLineParam.style = 1

        iTextItemArray = kompas.GetDynamicArray(TEXT_ITEM_ARR)

        iTextItemParam = kompas.GetParamStruct(StructType2DEnum.ko_TextItemParam)
        iTextItemParam.Init()
        iTextItemParam.iSNumb = 0
        iTextItemParam.s = ""
        iTextItemParam.type = 0

        iTextItemFontParam = iTextItemParam.GetItemFont
        iTextItemFontParam.Init()
        iTextItemFontParam.bitVector = 4096
        iTextItemFontParam.color = 0
        iTextItemFontParam.fontName = "GOST type A"
        iTextItemFontParam.height = height
        iTextItemFontParam.ksu = 1.0

        iTextItemArray.ksAddArrayItem(-1, iTextItemParam)

        iTextItemParam = kompas.GetParamStruct(StructType2DEnum.ko_TextItemParam)
        iTextItemParam.Init()
        iTextItemParam.iSNumb = 0
        iTextItemParam.s = text
        iTextItemParam.type = 0

        iTextItemFontParam = iTextItemParam.GetItemFont()
        iTextItemFontParam.Init()
        iTextItemFontParam.bitVector = 18
        iTextItemFontParam.color = 0
        iTextItemFontParam.fontName = "GOST type A"
        iTextItemFontParam.height = height
        iTextItemFontParam.ksu = 1.0

        iTextItemArray.ksAddArrayItem(-1, iTextItemParam)

        If textIndexUp.Length > 0 Or textIndexDown.Length > 0 Then
            iTextItemParam = kompas.GetParamStruct(StructType2DEnum.ko_TextItemParam)
            iTextItemParam.Init()
            iTextItemParam.iSNumb = 2
            iTextItemParam.s = ""
            iTextItemParam.type = 6

            iTextItemFontParam = iTextItemParam.GetItemFont()
            iTextItemFontParam.Init()
            iTextItemFontParam.bitVector = 4
            iTextItemFontParam.color = 0
            iTextItemFontParam.fontName = "GOST type A"
            iTextItemFontParam.height = height * 0.66
            iTextItemFontParam.ksu = 1.0

            iTextItemArray.ksAddArrayItem(-1, iTextItemParam)


            If textIndexUp.Length > 0 Then
                iTextItemParam = kompas.GetParamStruct(StructType2DEnum.ko_TextItemParam)
                iTextItemParam.Init()
                iTextItemParam.iSNumb = 0
                iTextItemParam.s = textIndexUp
                iTextItemParam.type = 0

                iTextItemFontParam = iTextItemParam.GetItemFont()
                iTextItemFontParam.Init()
                iTextItemFontParam.bitVector = 0
                iTextItemFontParam.color = 0
                iTextItemFontParam.fontName = "GOST type A"
                iTextItemFontParam.height = height * 0.66
                iTextItemFontParam.ksu = 1.0

                iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
            End If

            If textIndexDown.Length > 0 Then
                iTextItemParam = kompas.GetParamStruct(StructType2DEnum.ko_TextItemParam)
                iTextItemParam.Init()
                iTextItemParam.iSNumb = 0
                iTextItemParam.s = textIndexDown
                iTextItemParam.type = 0

                iTextItemFontParam = iTextItemParam.GetItemFont()
                iTextItemFontParam.Init()
                iTextItemFontParam.bitVector = 5
                iTextItemFontParam.color = 0
                iTextItemFontParam.fontName = "GOST type A"
                iTextItemFontParam.height = height * 0.66
                iTextItemFontParam.ksu = 1.0

                iTextItemArray.ksAddArrayItem(-1, iTextItemParam)
            End If

        End If


        iTextLineParam.SetTextItemArr(iTextItemArray)
        doc2D.ksTextLine(iTextLineParam)

        Return doc2D.ksEndObj()
    End Function

#End Region

#Region "   Метод для отрисовки круга по заданным параметрам: точка и радиус   "
    ''' <summary>
    ''' Метод для отрисовки круга по заданным параметрам: точка и радиус
    ''' </summary>
    ''' <param name="P"></param>
    ''' <param name="radius"></param>
    ''' <param name="style"></param>
    ''' <remarks></remarks>
    Public Function myDrawCircle(P As PointDouble, radius As Double, Optional style As Integer = 1)
        Return doc2D.ksCircle(P.X, P.Y, radius, style)
    End Function
#End Region

#Region "   Метод для отрисовки кружка внутри которого номер для указания положения конкретного вида на чертеже   "

    ''' <summary>
    ''' Метод для отрисовки кружка внутри которого номер для указания положения конкретного вида на чертеже
    ''' </summary>
    ''' <param name="P1"></param>
    ''' <param name="text"></param>
    ''' <remarks></remarks>
    Public Function myDrawNumberMacro(P1 As PointDouble, text As String, Optional scale As Double = 1)
        'doc2D.myMtr(P1, 0, 1, 1)
        Dim x, y As Double
        If text.Length = 1 Then
            x = -2.8
            y = -2.55
        ElseIf text.Length = 2 Then
            x = -4.48
            y = -2.55
        Else
            x = 0
            y = 0
        End If
        doc2D.ksMacro(0)
        doc2D.ksCircle(P1.X, P1.Y, 5 / scale, 2)
        doc2D.ksText(P1.X + (x / scale), P1.Y + (y / scale), 0, 5, 0, 0, text)
        Return doc2D.ksEndObj()

        'doc2D.myMtrDelete()
    End Function

#End Region

#Region "   Отрисовка пересечения вспомогательных прямых относительно заданной точке   "
    ''' <summary>
    ''' Пересечение вспомогательных прямых относительно заданной точки 
    ''' </summary>
    ''' <param name="P"></param>
    ''' <remarks></remarks>
    Sub myDrawCrossingLines(P As PointDouble)
        doc2D.ksLine(P.X, P.Y, 0)
        doc2D.ksLine(P.X, P.Y, 90)
    End Sub
#End Region


#Region "   Вычисление координаты точки на пересечении перпендикуляра произвольной точки к отрезку заданному по двум точкам   "

    ''' <summary>
    ''' Вычисление координаты точки на пересечении перпендикуляра произвольной точки к отрезку заданному по двум точкам
    ''' </summary>
    ''' <param name="arbitraryX">Произвольная точка</param>
    ''' <param name="P1">Первая координата отрезка</param>
    ''' <param name="P2">Вторая координата отрезка</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Function myCalculateCordinatePoint(arbitraryX As PointDouble, P1 As PointDouble, P2 As PointDouble) As PointDouble

        ' Точка пересечения отрезка и перпендикуляра
        Dim x As Double
        Dim y As Double

        Dim newPointDouble As New PointDouble
        Dim Mathematic2D As ksMathematic2D
        'Dim x, y As Double
        'Dim k As Integer
        Dim Ap As ksMathPointParam
        'Dim Ap As Double

        'Document2D.ksLine(P1.X, P1.Y, angle1)
        'Document2D.ksLine(0.0, 0.0, 45)

        Ap = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam) 'StructType2DEnum.ko_MathPointParam)

        Mathematic2D = kompas.GetMathematic2D()

        Mathematic2D.ksPerpendicular(arbitraryX.X, arbitraryX.Y, P1.X, P1.Y, P2.X, P2.Y, x, y) '

        newPointDouble.X = x
        newPointDouble.Y = y

        Return newPointDouble

    End Function

#End Region

#Region "   Вычисление координаты точки на пересечении вспомогательных прямых в заданных точках и углов прямых "

    ''' <summary>
    ''' Вычисление координаты точки на пересечении вспомогательных прямых в заданных точках и углов прямых
    ''' </summary>
    ''' <param name="P1"></param>
    ''' <param name="angle1"></param>
    ''' <param name="P2"></param>
    ''' <param name="angle2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Function myCalculateCordinatePoint(P1 As PointDouble, angle1 As Double, P2 As PointDouble, angle2 As Double) As PointDouble
        Dim newPointDouble As New PointDouble
        Dim Mathematic2D As ksMathematic2D
        'Dim x, y As Double
        'Dim k As Integer
        Dim Ap As ksMathPointParam
        'Dim Ap As Double

        'Document2D.ksLine(P1.X, P1.Y, angle1)
        'Document2D.ksLine(0.0, 0.0, 45)

        Ap = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam) 'StructType2DEnum.ko_MathPointParam)

        Mathematic2D = kompas.GetMathematic2D()
        'MessageBox.Show("Продолжить!")
        Mathematic2D.ksIntersectLinLin(P1.X, P1.Y, angle1, P2.X, P2.Y, angle2, Ap) '    k, x, y)
        'MessageBox.Show("Продолжить!")

        newPointDouble.X = Ap.x
        newPointDouble.Y = Ap.y

        Return newPointDouble

        'MessageBox.Show("Продолжить!")
        'Document2D.ksPoint(Ap.x, Ap.y, 0)

    End Function

#End Region

#Region "   Вычисление координаты точки относительно заданной точке, длины, угла   "
    ''' <summary>
    ''' Вычисление координаты точки относительно заданной точке, длины, угла
    ''' </summary>
    ''' <param name="P"></param>
    ''' <param name="lenght"></param>
    ''' <param name="angleDegrees"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Function myCalculateCordinatePoint(P As PointDouble, lenght As Double, angleDegrees As Double) As PointDouble
        Dim tmp_PointDouble As New PointDouble

        kompas = kompasApp()

        Dim Mathematic2D As ksMathematic2D
        Mathematic2D = kompas.GetMathematic2D()

        'Dim pArray As ksMathPointParam
        'pArray = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam)

        Dim pArray As ksDynamicArray
        pArray = kompas.GetDynamicArray(POINT_ARR)

        Dim par As ksMathPointParam = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam)

        Dim circle1 As ksCircleParam
        circle1 = kompas.GetParamStruct(StructType2DEnum.ko_CircleParam)
        circle1.xc = P.X : circle1.yc = P.Y : circle1.rad = lenght
        Dim c1 As Integer = doc2D.ksCircle(circle1.xc, circle1.yc, circle1.rad, 6)

        Dim line1 As ksLineParam
        line1 = kompas.GetParamStruct(StructType2DEnum.ko_LineParam)
        line1.x = P.X : line1.y = P.Y : line1.angle = angleDegrees
        Dim l1 As Integer = doc2D.ksLine(line1.x, line1.y, line1.angle)

        Mathematic2D.ksIntersectCurvCurv(c1, l1, pArray)

        doc2D.ksDeleteObj(c1)
        doc2D.ksDeleteObj(l1)

        pArray.ksGetArrayItem(0, par)


        tmp_PointDouble.X = par.x
        tmp_PointDouble.Y = par.y

        Return tmp_PointDouble


    End Function
#End Region

#Region "   Вычисление координаты точки по заданным двум окружностям (1 - Y2=>Y1 | -1 )   "
    ''' <summary>
    ''' Вычисление координаты точки по заданным двум окружностям (1 - Y2=>Y1 | -1 )
    ''' </summary>
    ''' <param name="P1"></param>
    ''' <param name="R1"></param>
    ''' <param name="P2"></param>
    ''' <param name="R2"></param>
    ''' <param name="horizontalAxis"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function myCalculateCordinatePointCircleCircle(P1 As PointDouble, R1 As Double, P2 As PointDouble, R2 As Double, Optional horizontalAxis As Integer = 1) As PointDouble
        Dim tmp_PointDouble As PointDouble = Nothing
        Dim list As List(Of PointDouble) = New List(Of PointDouble)

        kompas = kompasApp()

        Dim Mathematic2D As ksMathematic2D
        Mathematic2D = kompas.GetMathematic2D()

        'Dim pArray As ksMathPointParam
        'pArray = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam)

        Dim pArray As ksDynamicArray
        pArray = kompas.GetDynamicArray(POINT_ARR)

        Dim par As ksMathPointParam = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam)

        Dim circle1 As ksCircleParam
        circle1 = kompas.GetParamStruct(StructType2DEnum.ko_CircleParam)
        circle1.xc = P1.X : circle1.yc = P1.Y : circle1.rad = R1
        Dim c1 As Integer = doc2D.ksCircle(circle1.xc, circle1.yc, circle1.rad, 6)

        Dim circle2 As ksCircleParam
        circle2 = kompas.GetParamStruct(StructType2DEnum.ko_CircleParam)
        circle2.xc = P2.X : circle2.yc = P2.Y : circle2.rad = R2
        Dim c2 As Integer = doc2D.ksCircle(circle2.xc, circle2.yc, circle2.rad, 6)

        Mathematic2D.ksIntersectCurvCurv(c1, c2, pArray)
        doc2D.ksDeleteObj(c1)
        doc2D.ksDeleteObj(c2)

        For i As Integer = 0 To pArray.ksGetArrayCount - 1
            pArray.ksGetArrayItem(i, par)
            tmp_PointDouble = New PointDouble(par.x, par.y)
            list.Add(tmp_PointDouble)
        Next

        If horizontalAxis = 1 Then tmp_PointDouble = New PointDouble(list(1).X, list(1).Y)
        If horizontalAxis = -1 Then tmp_PointDouble = New PointDouble(list(0).X, list(0).Y)

        If horizontalAxis > 0 Then
            tmp_PointDouble = New PointDouble(list(1).X, list(1).Y)

        Else

        End If


        If horizontalAxis > 0 Then
            If list(1).Y >= list(0).Y Then
                tmp_PointDouble = New PointDouble(list(1).X, list(1).Y)
            Else
                tmp_PointDouble = New PointDouble(list(0).X, list(0).Y)
            End If
        ElseIf horizontalAxis < 0 Then
            If list(0).Y >= list(1).Y Then
                tmp_PointDouble = New PointDouble(list(1).X, list(1).Y)
            Else
                tmp_PointDouble = New PointDouble(list(0).X, list(0).Y)
            End If
        End If

        Return tmp_PointDouble


    End Function
#End Region

    ''' <summary>
    ''' Вычисление координаты точки на пересечении дуги и заданным углом прямой "
    ''' </summary>
    ''' <param name="B"></param>
    ''' <param name="radius"></param>
    ''' <returns></returns>
    Public Overloads Function myCalculateCordinatePoint(angleLine As Double, B As PointDouble, radius As Double) As PointDouble
        '90

        If (Math.Round(B.X, 8) = 0) Then
            'If (B.Y > 0) Then
            Return New PointDouble(0, B.Y + radius)
            'End If
        End If
        Dim T1 As PointDouble = New PointDouble(0, 0)

        Dim T2 As PointDouble = doc2D.myCalculateCordinatePoint(T1, radius, angleLine)

        kompas = kompasApp()
        'loadForm(ActiveDoc2D, "ШаблонЧертежа")
        Dim Mathematic2D As ksMathematic2D
        Mathematic2D = kompas.GetMathematic2D()

        Dim iDynamicArray As Kompas6API5.DynamicArray ' Интерфейс ksDynamicArray
        ' Создать интерфейс динамического массива математических точек
        iDynamicArray = kompas.GetDynamicArray(POINT_ARR)

        If Not iDynamicArray Is Nothing Then ' Массив создан
            Dim line As Integer = doc2D.ksLineSeg(T1.X, T1.Y, T2.X, T2.Y, 1) ' Отрисовка отрезка
            'ActiveDoc2D.ksArcByPoint(50, 40, 20, 30, 40, 70, 40, 1, 1) ' Отрисовка дуги по центру и конечным точкам
            'Dim arc As Integer = ActiveDoc2D.ksArcByPoint(B.X, B.Y, radius, 30, 40, 70, 40, 1, 1) ' Отрисовка дуги по центру и 

            'a1 = iMathematic2D.ksAngle(50, 40, 30, 40) ' Начальный угол дуги
            'a2 = iMathematic2D.ksAngle(50, 40, 70, 40) ' Конечный угол дуги

            ' Получить координаты точек пересечения отрезка и дуги
            ' Первая точка отрезка (0, 40), Вторая точка отрезка (100, 40),
            ' Центр дуги (50, 40), Радиус дуги 20
            'iMathematic2D.ksIntersectLinSArc(0, 40, 100, 40, 50, 40, 20, a1, a2, 1, iDynamicArray)

            Dim angle As Double = doc2D.myCalculateAngleTwoPoint(T1, T2)

            Dim angle1 As Double = angle - 90
            Dim angle2 As Double = angle + 90

            'If (angle >= 90 And angle < 275) Then
            '    angle1 = angle - 90
            '    angle1 = angle + 90
            '    'ElseIf (angle < 90 And angle >= 275) Then
            '    '    angle1 = 360 - angle
            '    '    angle2 = angle + 90
            'End If


            Mathematic2D.ksIntersectLinSArc(T1.X, T1.Y, 0, T2.Y, B.X, B.Y, radius, angle1, angle2, 1, iDynamicArray)

            Return DrawPointByArray(iDynamicArray) ' Отрисовка точек пересечения

            'Return iDynamicArray.ksDeleteArray() ' Удаление массива

            'Return New PointDouble(

        End If
    End Function

    ''' <summary>
    ''' Вычисление координаты точки на пересечении дуги заданной начальной точкой и прямой заданной двумя точками"
    ''' </summary>
    ''' <param name="T1"></param>
    ''' <param name="T2"></param>
    ''' <param name="B"></param>
    ''' <param name="radius"></param>
    ''' <returns></returns>
    Public Overloads Function myCalculateCordinatePoint(T1 As PointDouble, T2 As PointDouble, B As PointDouble, radius As Double) As PointDouble
        If (Math.Round(B.X, 8) = 0) Then
            'If (B.Y > 0) Then
            Return New PointDouble(0, B.Y + radius)
            'End If
        End If

        kompas = kompasApp()
        'loadForm(ActiveDoc2D, "ШаблонЧертежа")
        Dim Mathematic2D As ksMathematic2D
        Mathematic2D = kompas.GetMathematic2D()

        Dim iDynamicArray As Kompas6API5.DynamicArray ' Интерфейс ksDynamicArray
        ' Создать интерфейс динамического массива математических точек
        iDynamicArray = kompas.GetDynamicArray(POINT_ARR)

        If Not iDynamicArray Is Nothing Then ' Массив создан
            Dim line As Integer = doc2D.ksLineSeg(T1.X, T1.Y, T2.X, T2.Y, 1) ' Отрисовка отрезка
            'ActiveDoc2D.ksArcByPoint(50, 40, 20, 30, 40, 70, 40, 1, 1) ' Отрисовка дуги по центру и конечным точкам
            'Dim arc As Integer = ActiveDoc2D.ksArcByPoint(B.X, B.Y, radius, 30, 40, 70, 40, 1, 1) ' Отрисовка дуги по центру и 

            'a1 = iMathematic2D.ksAngle(50, 40, 30, 40) ' Начальный угол дуги
            'a2 = iMathematic2D.ksAngle(50, 40, 70, 40) ' Конечный угол дуги

            ' Получить координаты точек пересечения отрезка и дуги
            ' Первая точка отрезка (0, 40), Вторая точка отрезка (100, 40),
            ' Центр дуги (50, 40), Радиус дуги 20
            'iMathematic2D.ksIntersectLinSArc(0, 40, 100, 40, 50, 40, 20, a1, a2, 1, iDynamicArray)

            Dim angle As Double = doc2D.myCalculateAngleTwoPoint(T1, T2)

            Dim angle1 As Double = angle - 90
            Dim angle2 As Double = angle + 90

            'If (angle >= 90 And angle < 275) Then
            '    angle1 = angle - 90
            '    angle1 = angle + 90
            '    'ElseIf (angle < 90 And angle >= 275) Then
            '    '    angle1 = 360 - angle
            '    '    angle2 = angle + 90
            'End If


            'doc2D.ksIntersectLinSArc(T1.X, T1.Y, T2.X, T2.Y, B.X, B.Y, radius, angle1, angle2, 1, iDynamicArray)

            Mathematic2D.ksIntersectLinSArc(T1.X, T1.Y, T2.X, T2.Y, B.X, B.Y, radius, 0, 360, 1, iDynamicArray)

            Return DrawPointByArray(iDynamicArray) ' Отрисовка точек пересечения

            'Return iDynamicArray.ksDeleteArray() ' Удаление массива

            'Return New PointDouble(

        End If
    End Function

    Public Function DrawPointByArray(ByRef iDynamicArray As Object) As PointDouble
        Dim i As Object
        Dim iMathPointParam As Kompas6API5.MathPointParam ' Интерфейс ksMathPointParam
        If Not iDynamicArray Is Nothing Then
            ' Создать интерфейс параметров математической точки
            iMathPointParam = kompas.GetParamStruct(Kompas6Constants.StructType2DEnum.ko_MathPointParam)

            If Not iMathPointParam Is Nothing Then ' Интерфейс создан

                For i = 0 To iDynamicArray.ksGetArrayCount - 1 ' Выдать параметры точек в присланном массиве
                    iDynamicArray.ksGetArrayItem(i, iMathPointParam) ' Параметры текущей точки
                    ' Нарисовать точку в документе
                    'ActiveDoc2D.ksPoint(iMathPointParam.x, iMathPointParam.y, 5)
                    ' Выдать сообщение с координатами нарисованной точки
                    'kompas.ksMessage("x =И " & iMathPointParam.x & " y = " & iMathPointParam.y)
                    Dim x1 = iMathPointParam.x
                    Dim y1 = iMathPointParam.y

                    'Return New PointDouble(iMathPointParam.x, iMathPointParam.y)
                Next  ' Следующая точка
            End If
        End If

        Return Nothing
    End Function

#Region "   Вычисление угла в градусах по двум точкам   "
    ''' <summary>
    ''' Вычисление угла в градусах по двум точкам 
    ''' </summary>
    ''' <param name="P1"></param>
    ''' <param name="P2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function myCalculateAngleTwoPoint(P1 As PointDouble, P2 As PointDouble, Optional myAngle As enAngle = enAngle.Angle0) As Double
        Dim angle As Double

        kompas = kompasApp()

        Dim Mathematic2D As ksMathematic2D
        Mathematic2D = kompas.GetMathematic2D()

        angle = Mathematic2D.ksAngle(P1.X, P1.Y, P2.X, P2.Y) + Double.Parse(myAngle)

        If angle > 360 Then angle -= 360

        Return angle

    End Function
#End Region

#Region "   Вычисление угла в градусах по двум отрезкам   "
    ''' <summary>
    ''' Вычисление угла в градусах по двум отрезкам 
    ''' </summary>
    ''' <param name="P1"></param>
    ''' <param name="P2"></param>
    ''' <param name="P3"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function myCalculateAngleTwoSegment(P1 As PointDouble, P2 As PointDouble, P3 As PointDouble) As Double

        Dim angleSeg_P1P2 As Double = doc2D.myCalculateAngleTwoPoint(P1, P2)
        Dim angle1 As Double = Angle(angleSeg_P1P2)

        Dim angleSeg_P2P3 As Double = doc2D.myCalculateAngleTwoPoint(P2, P3)
        Dim angle2 As Double = Angle(angleSeg_P2P3)

        Return Math.Abs(angle1 - angle2)

    End Function
#End Region

#Region "   Вычисление длины по двум точкам   "
    ''' <summary>
    ''' Вычисление длины по двум точкам 
    ''' </summary>
    ''' <param name="P1"></param>
    ''' <param name="P2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function myCalculateLenghtTwoPoint(P1 As PointDouble, P2 As PointDouble) As Double
        Dim lenght As Double

        kompas = kompasApp()

        Dim Mathematic2D As ksMathematic2D
        Mathematic2D = kompas.GetMathematic2D()

        lenght = Mathematic2D.ksDistancePntPnt(P1.X, P1.Y, P2.X, P2.Y)


        Return lenght

    End Function
#End Region

#Region "   Вычисление координаты центра отрезка   "

    ''' <summary>
    ''' Вычисление координаты центра отрезка
    ''' </summary>
    ''' <param name="P1"></param>
    ''' <param name="P2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function myCalculateCordinateCentreSeg(P1 As PointDouble, P2 As PointDouble) As PointDouble
        Dim tmp_PointDouble As New PointDouble
        Dim list As List(Of PointDouble) = New List(Of PointDouble)

        kompas = kompasApp()

        Dim Mathematic2D As ksMathematic2D
        Mathematic2D = kompas.GetMathematic2D()

        'Dim pArray As ksMathPointParam
        'pArray = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam)

        Dim pArray As ksDynamicArray
        pArray = kompas.GetDynamicArray(POINT_ARR)

        Dim par As ksMathPointParam = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam)

        Dim len As Double = doc2D.myCalculateLenghtTwoPoint(P1, P2)

        Dim obj As Object = Mathematic2D.ksIntersectLinSCir(P1.X, P1.Y, P2.X, P2.Y, P1.X, P1.Y, len / 2, pArray)

        pArray.ksGetArrayItem(0, par)

        tmp_PointDouble.X = par.x
        tmp_PointDouble.Y = par.y

        Return tmp_PointDouble


    End Function
#End Region

#Region "   Вычисление координаты точки пересечения окружности и прямой в заданной координате с условием определения расположения искомой точки (условие задается перечислением 'enLocation'"

    ''' <summary>
    ''' Вычисление координаты точки пересечения окружности и прямой в заданной координате с условием определения расположения искомой точки (условие задается перечислением 'enLocation'
    ''' </summary>
    ''' <param name="P1">Координата точки круга</param>
    ''' <param name="radius">Радиус круга</param>
    ''' <param name="P2">Координата прямой</param>
    ''' <param name="angleDegrees">Угол прямой</param>
    ''' <param name="enLocation_">Перечисление выбора условия расположения искомой точки</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Function myCalculateCordinatePoint(P1 As PointDouble, radius As Double, P2 As PointDouble, angleDegrees As Double, enLocation_ As enLocation) As PointDouble
        myMtr(P1)

        Dim tmp_PointDouble As New PointDouble

        Try
            Dim list As List(Of PointDouble) = New List(Of PointDouble)

            kompas = kompasApp()

            Dim Mathematic2D As ksMathematic2D
            Mathematic2D = kompas.GetMathematic2D()

            'Dim pArray As ksMathPointParam
            'pArray = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam)

            Dim pArray As ksDynamicArray
            pArray = kompas.GetDynamicArray(POINT_ARR)

            Dim par As ksMathPointParam = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam)

            Dim c1 As Integer = doc2D.ksCircle(P1.X, P1.Y, radius, 6)

            Dim l1 As Integer = doc2D.ksLine(P2.X, P2.Y, angleDegrees)

            Mathematic2D.ksIntersectCurvCurv(c1, l1, pArray)

            doc2D.ksDeleteObj(c1)
            doc2D.ksDeleteObj(l1)

            For i As Integer = 0 To pArray.ksGetArrayCount - 1
                pArray.ksGetArrayItem(i, par)
                tmp_PointDouble = New PointDouble(par.x, par.y)
                list.Add(tmp_PointDouble)
            Next

            Select Case enLocation_
                Case enLocation.Horizontal_Right
                    For i As Integer = 0 To list.Count
                        If list(i).X > 0 Then
                            tmp_PointDouble = New PointDouble(list(i).X, 0)
                            Exit Select
                        End If
                    Next

                    Throw New Exception("Скорее всего направильно выбрано перечисление enLocation, что в свою очередь означает искомоя точка находится вне пределах заданной переменной")

                Case enLocation.Horizontal_Left
                    For i As Integer = 0 To list.Count
                        If list(i).X < 0 Then
                            tmp_PointDouble = New PointDouble(list(i).X, 0)
                            Exit Select
                        End If
                    Next

                    Throw New Exception("Скорее всего направильно выбрано перечисление enLocation, что в свою очередь означает искомоя точка находится вне пределах заданной переменной")

                Case enLocation.Vertical_Up
                    For i As Integer = 0 To list.Count
                        If list(i).Y > 0 Then
                            tmp_PointDouble = New PointDouble(0, list(i).Y)
                            Exit Select
                        End If
                    Next

                    Throw New Exception("Скорее всего направильно выбрано перечисление enLocation, что в свою очередь означает искомоя точка находится вне пределах заданной переменной")

                Case enLocation.Vertical_Down
                    For i As Integer = 0 To list.Count
                        If list(i).Y < 0 Then
                            tmp_PointDouble = New PointDouble(0, list(i).Y)
                            Exit Select
                        End If
                    Next

                    Throw New Exception("Скорее всего направильно выбрано перечисление enLocation, что в свою очередь означает искомоя точка находится вне пределах заданной переменной")

                Case enLocation.quarter_0_90
                    For i As Integer = 0 To list.Count
                        If list(i).X > 0 And list(i).Y > 0 Then
                            tmp_PointDouble = New PointDouble(list(i).X, list(i).Y)
                            Exit Select
                        End If
                    Next

                    Throw New Exception("Скорее всего направильно выбрано перечисление enLocation, что в свою очередь означает искомоя точка находится вне пределах заданной переменной")

                Case enLocation.quarter_90_180
                    For i As Integer = 0 To list.Count
                        If list(i).X < 0 And list(i).Y > 0 Then
                            tmp_PointDouble = New PointDouble(list(i).X, list(i).Y)
                            Exit Select
                        End If
                    Next

                    Throw New Exception("Скорее всего направильно выбрано перечисление enLocation, что в свою очередь означает искомоя точка находится вне пределах заданной переменной")

                Case enLocation.quarter_180_270
                    For i As Integer = 0 To list.Count
                        If list(i).X < 0 And list(i).Y < 0 Then
                            tmp_PointDouble = New PointDouble(list(i).X, list(i).Y)
                            Exit Select
                        End If
                    Next
                    Throw New Exception("Скорее всего направильно выбрано перечисление enLocation, что в свою очередь означает искомоя точка находится вне пределах заданной переменной")

                Case enLocation.quarter_270_360
                    For i As Integer = 0 To list.Count
                        If list(i).X > 0 And list(i).Y < 0 Then
                            tmp_PointDouble = New PointDouble(list(i).X, list(i).Y)
                            Exit Select
                        End If
                    Next
                    Throw New Exception("Скорее всего направильно выбрано перечисление enLocation, что в свою очередь означает искомоя точка находится вне пределах заданной переменной")
            End Select
        Catch ex As Exception
            myMtrDelete()
            'MessageBox.Show(ex.Message & vbNewLine & "Скорее всего направильно выбрана перечисление enLocation, что в свою очередь означает искомоя точка находится вне пределах заданной переменной")
            Dim s As String = ex.Message & vbNewLine & vbNewLine & "Скорее всего неправильно выбрано перечисление enLocation, и это означает, что искомоя точка находится вне пределах заданной переменной"
            Throw New Exception(s)
        End Try


        myMtrDelete()

        Return tmp_PointDouble


    End Function
#End Region

    ''' <summary>
    ''' Вычисление длины проекции между двумя точками относительно заданной прямой под углом На прямую X
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function myCalculateLenghtProjectionX(P1 As PointDouble, P2 As PointDouble, angleValue As Double) As Double

        ' Сначала определяем коорднату точки проекции
        Dim angle_ As Double = Angle(angleValue)

        Dim X As PointDouble
        X = myCalculateCordinatePoint(P1, 0 + angle_, P2, 90 + angle_)

        Dim len As Double = myCalculateLenghtTwoPoint(P1, X)

        Return len
    End Function

    ''' <summary>
    ''' Вычисление длины проекции между двумя точками относительно заданной прямой под углом на прямую Y
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function myCalculateLenghtProjectionY(P1 As PointDouble, P2 As PointDouble, angleValue As Double) As Double

        ' Сначала определяем коорднату точки проекции
        Dim angle_ As Double = Angle(angleValue)

        Dim Y As PointDouble
        Y = myCalculateCordinatePoint(P1, 0 + angle_, P2, 90 + angle_)

        Dim len As Double = myCalculateLenghtTwoPoint(P2, Y)


        Return len
    End Function


    Function locationPoint(list As List(Of Double)) As PointDouble
        Dim tmpPointDouble As New PointDouble(0, 0)



        Return tmpPointDouble

    End Function




#Region "   Создание временной локальной системы координат по заданной точке   "
    ''' <summary>
    ''' Создание временной локальной системы координат по заданной точке
    ''' </summary>
    ''' <param name="P1"></param>
    ''' <param name="angle"></param>
    ''' <param name="mX1"></param>
    ''' <param name="mY1"></param>
    ''' <remarks></remarks>
    Sub myMtr(P1 As PointDouble, Optional angle As Double = 0, Optional mX1 As Double = 1, Optional mY1 As Double = 1)
        doc2D.ksMtr(P1.X, P1.Y, angle, mX1, mY1)

    End Sub
#End Region

#Region "   Удаление временной локальной системы координат на чертеже   "
    ''' <summary>
    ''' Удаление временной локальной системы координат на чертеже
    ''' </summary>
    ''' <remarks></remarks>
    Sub myMtrDelete()
        doc2D.ksDeleteMtr()

    End Sub
#End Region


#Region "    Вычисление знака относительно точки вокруг, которой необходимо определить вектор направления   "

    ''' <summary>
    ''' Вычисление знака относительно точки вокруг, которой необходимо определить вектор направления 
    ''' Варианты (точка слева направление угла вектора : угол = 0...180 направление против часовой стрелки будет знак +: угол = 180...360 за часовой будет - )
    ''' и наоборот относительно размещения точки справа
    ''' </summary>
    ''' <param name="pointPlacement">размещение точки вокруг которой крутим на правление вектора слева или справа</param>
    ''' <param name="angle">Угол вектора в основном определяется по двум координатам</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function myCalculateSGN(pointPlacement As enPointPlacement, angle As Double)

        Dim sgn_ As Double = 0

        If angle >= 0 And angle < 180 Then
            sgn_ = -1 * pointPlacement
        End If

        If angle >= 180 And angle < 360 Then
            sgn_ = 1 * pointPlacement
        End If

        Return sgn_
    End Function

#End Region

    Private Function Angle(angleValue As Double) As Double
        Dim tmpAngle As Double = 0

        If angleValue > 360 Then angleValue -= 360

        Do While angleValue > 360
            angleValue -= 360
            tmpAngle = angleValue
        Loop

        If angleValue >= 0 And angleValue < 180 Then
            tmpAngle = angleValue
        End If

        If angleValue >= 180 And angleValue < 360 Then
            tmpAngle = angleValue - 180
        End If

        Return tmpAngle

    End Function


#Region "    Абсолютная длина по двум точкам прямой выбранного вида   "

    ''' <summary>
    ''' Абсолютная длина прямой выбранного вида по двум точкам
    ''' </summary>
    ''' <param name="sheetView1"></param>
    ''' <param name="T1"></param>
    ''' <param name="T2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function myAbsoluteLenghtTwoPoint(sheetView1 As cls_SheetView, T1 As PointDouble, T2 As PointDouble) As Double
        Dim len As Double
        len = myCalculateLenghtTwoPoint(T1, T2)

        Return len * sheetView1.ScaleView
    End Function

#End Region

#Region "    Абсолютная длина прямой выбранного вида (аргумент длина вида - double)   "

    ''' <summary>
    ''' Абсолютная длина прямой выбранного вида (аргумент длина вида - double)
    ''' </summary>
    ''' <param name="sheetView1">Вид для которого нужно узнать абсолютную длину чертежа</param>
    ''' <param name="lenght">На плане вида длина переменной для которой нужно узнать абсолютную длину</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function myAbsoluteLenght(sheetView1 As cls_SheetView, lenght As Double) As Double

        Return lenght * sheetView1.ScaleView
    End Function

#End Region

#Region "    Абсолютная координата точки взята на конкретном виде относительно листа чертежа"

    ''' <summary>
    ''' Координата точки взята на конкретном виде оотносительно листа черетежа
    ''' </summary>
    ''' <param name="sheetView1"></param>
    ''' <param name="T1"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function myAbsoluteCordinatePoint(sheetView1 As cls_SheetView, T1 As PointDouble) As PointDouble
        Dim newPoint As New PointDouble

        newPoint.X = sheetView1.X + (T1.X * sheetView1.ScaleView)
        newPoint.Y = sheetView1.Y + (T1.Y * sheetView1.ScaleView)

        Return newPoint
    End Function

#End Region

#Region "    Работа с группой   "

    ''' <summary>
    ''' Создание новой группы
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GroupNew() As Integer
        Return doc2D.ksNewGroup(0)
    End Function


    ''' <summary>
    ''' Завершение операция - группа
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GroupEnd() As Integer
        Return doc2D.ksEndGroup()
    End Function

#End Region

#Region "    Метод для отрисовки объекта симетрично заданной прямой определенная точкой и углом   "

    ''' <summary>
    ''' Метод для отрисовки объекта симетрично заданной прямой определенная точкой и углом
    ''' </summary>
    ''' <param name="int_group">Ссылка на объект </param>
    ''' <param name="P1">Точка в которой задается симметрия </param>
    ''' <param name="angle">Угол прямой</param>
    ''' <param name="enMode">Параметр определяет как будет произведена симметрия </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function myDrawSymmetryObject(int_group As Integer, P1 As PointDouble, angle As Double, enMode As enMode) As Integer

        Dim P2 As PointDouble = myCalculateCordinatePoint(P1, 10, angle)

        If enMode = enMode.Copy Then
            Return doc2D.ksSymmetryObj(int_group, P1.X, P1.Y, P2.X, P2.Y, "1")
        ElseIf enMode = enMode.Cute Then
            Return doc2D.ksSymmetryObj(int_group, P1.X, P1.Y, P2.X, P2.Y, "0")
        End If

        Return 0

    End Function

#End Region


End Class
