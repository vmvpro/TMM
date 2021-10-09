Imports System.Runtime.CompilerServices
Imports Kompas6API5, Kompas6Constants



Module mdl_ExtensionsKompas

    Dim kompas As Kompas6API5.Application

    ''' <summary>
    ''' Метод для отрисовки точки на чертеже в заданной точки
    ''' </summary>
    ''' <param name="Document2D"></param>
    ''' <param name="P"></param>
    ''' <remarks></remarks>
	<Extension> _
	Sub myDrawPoint(Document2D As ksDocument2D, P As PointDouble, Optional stylePoint As Integer = 0)
		Document2D.ksPoint(P.X, P.Y, stylePoint)
	End Sub

    ''' <summary>
    ''' Метод для отрисовки отрезка по двум точкам
    ''' </summary>
    ''' <param name="Document2D"></param>
    ''' <param name="P1">точка 1</param>
    ''' <param name="P2">точка 2</param>
    ''' <param name="style"></param>
    ''' <remarks></remarks>
    <Extension> _
    Sub myDrawLineSeg(Document2D As ksDocument2D, P1 As PointDouble, P2 As PointDouble, Optional style As Integer = 1)
        Document2D.ksLineSeg(P1.X, P1.Y, P2.X, P2.Y, style)
    End Sub

    ''' <summary>
    ''' Метод для отрисовки вектора направления по стрелке
    ''' </summary>
    ''' <param name="Document2D"></param>
    ''' <param name="P1"></param>
    ''' <param name="P2"></param>
    ''' <param name="style"></param>
    ''' <remarks></remarks>
    <Extension> _
    Sub myDrawVector(Document2D As ksDocument2D, P1 As PointDouble, P2 As PointDouble, Optional style As Integer = 1)
        'Dim pArray As ksDynamicArray
        Dim iMathPointArray As ksDynamicArray
        Dim iMathPointParam As ksMathPointParam
        Dim iPolylineArray As ksDynamicArray

        Dim len As Double = Document2D.myCalculateLenghtTwoPoint(P1, P2)

        'If len < 6 Then
        'Document2D.myDrawLineSeg(P1, P2, 2)
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

        Dim obj As Object = Document2D.ksLeader(iLeaderParam)
    End Sub


    ''' <summary>
    ''' Метод для отрисовки отрезка по относительной точке, длине и заданному углу
    ''' </summary>
    ''' <param name="Document2D"></param>
    ''' <param name="P1"></param>
    ''' <param name="lenght"></param>
    ''' <param name="angle"></param>
    ''' <param name="style"></param>
    ''' <remarks></remarks>
    <Extension> _
    Function myDrawLineSeg(Document2D As ksDocument2D, P1 As PointDouble, lenght As Double, angle As Double, Optional style As Integer = 1) As PointDouble
        Document2D.myMtr(P1)

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
        Dim c1 As Integer = Document2D.ksCircle(circle1.xc, circle1.yc, circle1.rad, 1)

        Dim line1 As ksLineParam
        line1 = kompas.GetParamStruct(StructType2DEnum.ko_LineParam)
        line1.x = P1.X : line1.y = P1.Y : line1.angle = angle
        Dim l1 As Integer = Document2D.ksLine(line1.x, line1.y, line1.angle)

        Mathematic2D.ksIntersectCurvCurv(c1, l1, pArray)

        Document2D.ksDeleteObj(c1)
        Document2D.ksDeleteObj(l1)

        pArray.ksGetArrayItem(0, par)

        Document2D.ksLineSeg(P1.X, P1.Y, par.x, par.y, style)

        tmp_PointDouble.X = par.x
        tmp_PointDouble.Y = par.y

        Document2D.myMtrDelete()

        Return tmp_PointDouble
    End Function

    ''' <summary>
    ''' Пересечение вспомогательных прямых относительно заданной точки 
    ''' </summary>
    ''' <param name="Document2D"></param>
    ''' <param name="P"></param>
    ''' <remarks></remarks>
    <Extension> _
    Sub myCrossingLines(Document2D As ksDocument2D, P As PointDouble)
        Document2D.ksLine(P.X, P.Y, 0)
        Document2D.ksLine(P.X, P.Y, 90)
    End Sub

    ''' <summary>
    ''' Метод для отрисовки круга по заданным парметрам: точка и радиус
    ''' </summary>
    ''' <param name="Document2D"></param>
    ''' <param name="P"></param>
    ''' <param name="radius"></param>
    ''' <param name="style"></param>
    ''' <remarks></remarks>
    <Extension> _
    Sub myDrawCircle(Document2D As ksDocument2D, P As PointDouble, radius As Double, Optional style As Integer = 1)
        Document2D.ksCircle(P.X, P.Y, radius, style)
    End Sub

    ''' <summary>
    ''' Вычисление координаты точки относительно заданной точке, длины, угла
    ''' </summary>
    ''' <param name="Document2D"></param>
    ''' <param name="P"></param>
    ''' <param name="lenght"></param>
    ''' <param name="angleDegrees"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension> _
    Function myCalculateCordinatePoint(Document2D As ksDocument2D, P As PointDouble, lenght As Double, angleDegrees As Double) As PointDouble
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
        Dim c1 As Integer = Document2D.ksCircle(circle1.xc, circle1.yc, circle1.rad, 6)

        Dim line1 As ksLineParam
        line1 = kompas.GetParamStruct(StructType2DEnum.ko_LineParam)
        line1.x = P.X : line1.y = P.Y : line1.angle = angleDegrees
        Dim l1 As Integer = Document2D.ksLine(line1.x, line1.y, line1.angle)

        Mathematic2D.ksIntersectCurvCurv(c1, l1, pArray)

        Document2D.ksDeleteObj(c1)
        Document2D.ksDeleteObj(l1)

        pArray.ksGetArrayItem(0, par)


        tmp_PointDouble.X = par.x
        tmp_PointDouble.Y = par.y

        Return tmp_PointDouble


    End Function


    ''' <summary>
    ''' Вычисление координаты точки по заданным двум окружностям (1 - Y2=>Y1 | -1 )
    ''' </summary>
    ''' <param name="Document2D"></param>
    ''' <param name="P1"></param>
    ''' <param name="R1"></param>
    ''' <param name="P2"></param>
    ''' <param name="R2"></param>
    ''' <param name="horizontalAxis"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension> _
    Function myCalculateCordinatePointCircleCircle(Document2D As ksDocument2D, P1 As PointDouble, R1 As Double, P2 As PointDouble, R2 As Double, Optional horizontalAxis As Integer = 1) As PointDouble
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
        Dim c1 As Integer = Document2D.ksCircle(circle1.xc, circle1.yc, circle1.rad, 6)

        Dim circle2 As ksCircleParam
        circle2 = kompas.GetParamStruct(StructType2DEnum.ko_CircleParam)
        circle2.xc = P2.X : circle2.yc = P2.Y : circle2.rad = R2
        Dim c2 As Integer = Document2D.ksCircle(circle2.xc, circle2.yc, circle2.rad, 6)

        Mathematic2D.ksIntersectCurvCurv(c1, c2, pArray)
        Document2D.ksDeleteObj(c1)
        Document2D.ksDeleteObj(c2)

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


    ''' <summary>
    ''' Вычисление угла в градусах по двум точкам 
    ''' </summary>
    ''' <param name="Document2D"></param>
    ''' <param name="P1"></param>
    ''' <param name="P2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension> _
    Function myCalculateAngleTwoPoint(Document2D As ksDocument2D, P1 As PointDouble, P2 As PointDouble, Optional myAngle As enAngle = enAngle.Angle0) As Double
        Dim angle As Double

        kompas = kompasApp()

        Dim Mathematic2D As ksMathematic2D
        Mathematic2D = kompas.GetMathematic2D()


        angle = Mathematic2D.ksAngle(P1.X, P1.Y, P2.X, P2.Y) + Double.Parse(enAngle.Angle0)

        Return angle

    End Function

    ''' <summary>
    ''' Вычисление угла в градусах по двум отрезкам 
    ''' </summary>
    ''' <param name="Document2D"></param>
    ''' <param name="P1"></param>
    ''' <param name="P2"></param>
    ''' <param name="P3"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension> _
    Function myCalculateAngleTwoSegment(Document2D As ksDocument2D, P1 As PointDouble, P2 As PointDouble, P3 As PointDouble) As Double

        Dim angleSeg_P1P2 As Double = Document2D.myCalculateAngleTwoPoint(P1, P2)
        Dim angle1 As Double = Angle(angleSeg_P1P2)

        Dim angleSeg_P2P3 As Double = Document2D.myCalculateAngleTwoPoint(P2, P3)
        Dim angle2 As Double = Angle(angleSeg_P2P3)

        Return Math.Abs(angle1 - angle2)

    End Function


    ''' <summary>
    ''' Вычисление длины по двум точкам 
    ''' </summary>
    ''' <param name="Document2D"></param>
    ''' <param name="P1"></param>
    ''' <param name="P2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension> _
    Function myCalculateLenghtTwoPoint(Document2D As ksDocument2D, P1 As PointDouble, P2 As PointDouble) As Double
        Dim lenght As Double

        kompas = kompasApp()

        Dim Mathematic2D As ksMathematic2D
        Mathematic2D = kompas.GetMathematic2D()

        lenght = Mathematic2D.ksDistancePntPnt(P1.X, P1.Y, P2.X, P2.Y)


        Return lenght

    End Function


    ''' <summary>
    ''' Определение временной локальной системы координат по заданной точке
    ''' </summary>
    ''' <param name="Document2D"></param>
    ''' <param name="P1"></param>
    ''' <param name="angle"></param>
    ''' <param name="mX1"></param>
    ''' <param name="mY1"></param>
    ''' <remarks></remarks>
    <Extension> _
    Sub myMtr(Document2D As ksDocument2D, P1 As PointDouble, Optional angle As Double = 0, Optional mX1 As Double = 1, Optional mY1 As Double = 1)
        Document2D.ksMtr(P1.X, P1.Y, angle, mX1, mY1)

    End Sub

    ''' <summary>
    ''' Удаление временной локальной системы координат на чертеже
    ''' </summary>
    ''' <param name="Document2D"></param>
    ''' <remarks></remarks>
    <Extension> _
    Sub myMtrDelete(Document2D As ksDocument2D)
        Document2D.ksDeleteMtr()

    End Sub

    ''' <summary>
    ''' Создать текст в графическом документе
    ''' </summary>
    ''' <param name="Document2D"></param>
    ''' <param name="pointDouble"></param>
    ''' <param name="heightText"></param>
    ''' <param name="text"></param>
    ''' <param name="offsetX"></param>
    ''' <param name="offsetY"></param>
    ''' <remarks></remarks>
    <Extension> _
    Sub myDrawText(Document2D As ksDocument2D, pointDouble As PointDouble, text As String, Optional heightText As Double = 5, Optional offsetX As Double = 0, Optional offsetY As Double = 0)
        Document2D.ksText(pointDouble.X + offsetX, pointDouble.Y + offsetY, 0, heightText, 0, 0, text)
    End Sub



    ''' <summary>
    ''' Вычисление координаты центра заданного отрезка отрезка
    ''' </summary>
    ''' <param name="Document2D"></param>
    ''' <param name="P1"></param>
    ''' <param name="P2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension> _
    Function myCalculateCordinateCentreSeg(Document2D As ksDocument2D, P1 As PointDouble, P2 As PointDouble) As PointDouble
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

        Dim len As Double = Document2D.myCalculateLenghtTwoPoint(P1, P2)

        Dim obj As Object = Mathematic2D.ksIntersectLinSCir(P1.X, P1.Y, P2.X, P2.Y, P1.X, P1.Y, len / 2, pArray)

        pArray.ksGetArrayItem(0, par)

        tmp_PointDouble.X = par.x
        tmp_PointDouble.Y = par.y

        Return tmp_PointDouble


    End Function

    Function Angle(angleValue As Double) As Double
        Dim tmpAngle As Double = 0

        If angleValue >= 0 And angleValue < 180 Then
            tmpAngle = angleValue
        End If

        If angleValue >= 180 And angleValue < 360 Then
            tmpAngle = angleValue - 180
        End If

        Return tmpAngle

    End Function

    ''' <summary>
    ''' Вычисление координаты точки на пересечении вспомогательных прямых в заданных точках и углов прямых
    ''' </summary>
    ''' <param name="Document2D"></param>
    ''' <param name="P1"></param>
    ''' <param name="angle1"></param>
    ''' <param name="P2"></param>
    ''' <param name="angle2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension> _
    Function myCalculateCordinatePoint(Document2D As ksDocument2D, P1 As PointDouble, angle1 As Double, P2 As PointDouble, angle2 As Double) As PointDouble
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



End Module
