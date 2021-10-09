Imports Kompas6API5
Imports System.Threading.Thread
Imports System.IO

''' <summary>
''' Перечисление стиль отрисовки точки
''' </summary>
''' <remarks></remarks>
Enum enStylePoint
    ''' <summary>
    ''' Точка
    ''' </summary>
    ''' <remarks></remarks>
    Point = 0 ' 0 - точка

    ''' <summary>
    ''' Плюс
    ''' </summary>
    ''' <remarks></remarks>
    Plus = 1

    ''' <summary>
    ''' Крестик
    ''' </summary>
    ''' <remarks></remarks>
    Cross = 2

    ''' <summary>
    ''' Квадрат
    ''' </summary>
    ''' <remarks></remarks>
    Square = 3

    ''' <summary>
    ''' Треугольник
    ''' </summary>
    ''' <remarks></remarks>
    Triangle = 4

    ''' <summary>
    ''' Окружность
    ''' </summary>
    ''' <remarks></remarks>
    Circle = 5

    ''' <summary>
    ''' Звезда
    ''' </summary>
    ''' <remarks></remarks>
    Star = 6

    ''' <summary>
    ''' Конверт
    ''' </summary>
    ''' <remarks></remarks>
    Envelope = 7

    ''' <summary>
    ''' Плюс утолщенный
    ''' </summary>
    ''' <remarks></remarks>
    PlusBold = 8

End Enum

''' <summary>
''' Перечисление для угла
''' </summary>
''' <remarks></remarks>
Public Enum enAngle
    Angle0 = 0
    Angle90 = 90
    Angle180 = 180
    Angle270 = 270
End Enum
Module mdl_Function
    Dim kompas As KompasObject

    Dim copyFile As Integer = 0

    ''' <summary>
    ''' Перевод строкового значения точки в базе данных в тип PointDouble (Принимает строковое значение)
    ''' </summary>
    ''' <param name="ToStringPoint">Строковое значение в базе данных</param>
    ''' <returns>PointDouble</returns>
    ''' <remarks></remarks>
    Public Function StringToPointDouble(ToStringPoint As String) As PointDouble
        Dim ss() As String = ToStringPoint.Split(";")
        Dim d1 As String = ss(0)
        Dim d2 As String = ss(1)
        Return New PointDouble(Convert.ToDouble(d1), Convert.ToDouble(d2))
    End Function

    Function kompasApp() As Kompas6API5.Application

		If Environment.UserName = "tz023111" Then
			Try
				kompas = GetObject(, "KOMPAS.Application.5")
			Catch ex As Exception

				'kompas = GetObject(Environment.CurrentDirectory & "\ШаблонЧертежа.cdw", "KOMPAS.Application.5")

				Dim kompasProcess As ProcessStartInfo = New ProcessStartInfo("c:\Program Files\ASCON\KOMPAS-3D V15\Bin\KOMPAS.exe")
				Process.Start(kompasProcess).WaitForExit(10000)
				Sleep(10000)



				kompas = GetObject(, "KOMPAS.Application.5")
			End Try

			Return kompas
		End If

        Try
			kompas = GetObject(, "KOMPAS.Application.5")
        Catch ex As Exception
			kompas = CreateObject("KOMPAS.Application.5") ' Подключаемся к КОМПАСу
        End Try

        Return kompas

    End Function

    Sub loadForm(ByRef doc2D As Document2D, fileName As String)
        Do
            Dim name As String = Environment.CurrentDirectory & "\" & fileName & " (" & copyFile & ").cdw"
            If Not File.Exists(name) Then
                File.Delete(name)
                Exit Do
            End If
            copyFile += 1
        Loop

        Try
            kompas = kompasApp()
            'kompas.Visible = False
            doc2D = kompas.Document2D
            doc2D.ksOpenDocument(Environment.CurrentDirectory & "\" & fileName & ".cdw", True)
            doc2D.ksSaveDocumentEx(Environment.CurrentDirectory & "\" & fileName & " (" & copyFile & ").cdw", 1)
            doc2D.ksCloseDocument()

            doc2D = kompas.Document2D
            doc2D.ksOpenDocument(Environment.CurrentDirectory & "\" & fileName & " (" & copyFile & ").cdw", False)

            kompas.Visible = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Метод перечечения прямых в заданной точке
    ''' </summary>
    ''' <remarks></remarks>
    Sub ksCrossingLines(Document2D As ksDocument2D, P As PointDouble)
        Document2D.ksLine(P.X, P.Y, 0)
        Document2D.ksLine(P.X, P.Y, 90)
    End Sub


    ''' <summary>
    ''' Вычисление координаты точки по заданной точке, длины, угла
    ''' </summary>
    ''' <param name="P"></param>
    ''' <param name="lenght"></param>
    ''' <param name="angleDegrees"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function cordinatePoint(P As PointDouble, lenght As Double, angleDegrees As Double) As PointDouble
        Dim tmpPoint As New PointDouble
        Dim angleRadian As Double = (Math.PI / 180) * angleDegrees

        tmpPoint.X = P.X + lenght * Math.Cos(angleRadian)
        tmpPoint.Y = P.Y + lenght * Math.Sin(angleRadian)

        Return tmpPoint

    End Function

    Function cordinatePoint(kompas As Kompas6API5.Application, P As PointDouble, lenght As Double, angleDegrees As Double) As PointDouble
        Dim tmpPoint As New PointDouble

        Dim Mathematic2D As ksMathematic2D
        Dim Ap As ksMathPointParam
        Ap = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam) 'StructType2DEnum.ko_MathPointParam)
        Mathematic2D = kompas.GetMathematic2D()
        Mathematic2D.ksIntersectCirLin(P.X, P.Y, lenght, P.X, P.Y, angleDegrees, Ap) '    k, x, y)

        tmpPoint.X = Ap.x
        tmpPoint.Y = Ap.y

        Return tmpPoint

    End Function

    Function myCreateSheetView_(Document2D As Document2D, name As String, x As Double, y As Double) As Integer
        Dim refSheetView As Integer
        Dim ViewParam As ksViewParam
        Dim ViewNumber As Integer

        ViewParam = kompas.GetParamStruct(StructType2DEnum.ko_ViewParam)        ' парметр GetParamStruct(8) выбирается из SDK - StructType2D -> ko_ViewParam -> ksViewParam, который относится к "Видам чертежа"

        ViewParam.name = name
        ViewParam.x = 370
        ViewParam.y = 235
        ViewNumber = Document2D.ksNewViewNumber

        refSheetView = Document2D.ksCreateSheetView(ViewParam, ViewNumber)

        Document2D.ksOpenView(0)

        Return refSheetView

        'doc2D.ksOpenView(0)
        'doc2D.ksDeleteObj(refSheetView)
    End Function

    Sub bbb()
        Dim pArray As ksDynamicArray
        pArray = kompas.GetDynamicArray(POINT_ARR)

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
        iLeaderParam.x = 225.63995853586
        iLeaderParam.y = 268.01042312921




    End Sub

    Sub DrawLeader(ByRef doc As Object)
        Dim ilead As Object ' ksLeaderParam
        ilead = kompas.GetParamStruct(StructType2DEnum.ko_LeaderParam)

        Dim itLinePar As Object ' ksTextLineParam
        itLinePar = kompas.GetParamStruct(Kompas6Constants.StructType2DEnum.ko_TextLineParam)

        Dim iItemPar As Object ' ksTextItemParam
        iItemPar = kompas.GetParamStruct(Kompas6Constants.StructType2DEnum.ko_TextItemParam)

        Dim itFont As Object ' ksTextItemFont
        itFont = iItemPar.GetItemFont

        Dim itMathPoint As Object ' ksMathPointParam
        itMathPoint = kompas.GetParamStruct(Kompas6Constants.StructType2DEnum.ko_MathPointParam)

        Dim iptext As Object
        Dim iTextItemArr As Object
        Dim ipPolyLin As Object
        Dim ipMathPoint As Object
        Dim obj As Integer ' ksDynamicArray
        If Not ilead Is Nothing And Not itLinePar Is Nothing And Not iItemPar Is Nothing And Not itFont Is Nothing And Not itMathPoint Is Nothing Then
            ilead.Init()
            itLinePar.Init()
            iItemPar.Init()
            itFont.Init()
            itMathPoint.Init()

            itFont.SetBitVectorValue(NEW_LINE, True)
            itLinePar.Style = 0

            iptext = ilead.GetpTextline

            iTextItemArr = itLinePar.GetTextItemArr
            iItemPar.s = "1"
            iTextItemArr.ksAddArrayItem(-1, iItemPar)
            iptext.ksAddArrayItem(-1, itLinePar)

            iTextItemArr.ksClearArray()
            iItemPar.s = "2"
            iTextItemArr.ksAddArrayItem(-1, iItemPar)
            iptext.ksAddArrayItem(-1, itLinePar)

            iTextItemArr.ksClearArray()
            iItemPar.s = "3"
            iTextItemArr.ksAddArrayItem(-1, iItemPar)
            iptext.ksAddArrayItem(-1, itLinePar)

            ipPolyLin = ilead.GetpPolyline
            ipMathPoint = kompas.GetDynamicArray(POINT_ARR)
            If Not ipMathPoint Is Nothing And Not ipPolyLin Is Nothing Then
                itMathPoint.x = 10
                itMathPoint.y = 10

                ipMathPoint.ksAddArrayItem(-1, itMathPoint)
                ipPolyLin.ksAddArrayItem(-1, ipMathPoint)

                itMathPoint.x = 30
                itMathPoint.y = 10
                ipMathPoint.ksClearArray()
                ipMathPoint.ksAddArrayItem(-1, itMathPoint)
                ipPolyLin.ksAddArrayItem(-1, ipMathPoint)

                ilead.SetpPolyline(ipPolyLin)
            End If

            ' заполним параметры
            ilead.x = 50 ' координаты базовой точки ( начало полки )
            ilead.y = 50
            ilead.arrowType = 1 ' тип стрелки
            ilead.dirX = 1 ' направление полки по X (1 -вправо -1-влево)
            ilead.signType = 0 ' тип знака
            ilead.around = 0 ' знак обработки по контуру 0-выключен 1- включен
            ilead.cText0 = 1 ' количество строк текста над полкой 0- текст отсутствует
            ilead.cText1 = 1 ' количество строк текста под полкой 0- текст отсутствует
            ilead.cText2 = 0 ' количество строк текста над ножкой (не более 1 строки)0- текст отсутствует
            ilead.cText3 = 1 ' количество строк текста под ножкой (не более 1 строки)0- текст отсутствует

            obj = doc.ksLeader(ilead)
            If obj Then
                doc.ksGetObjParam(obj, ilead, ALLPARAM)
                ilead.x = 100
                kompas.ksMessage("Поменяем параметры")
                doc.ksSetObjParam(obj, ilead, ALLPARAM)
            End If
        End If
    End Sub
    Sub DrawPosLeader(ByRef doc As Object)
        Dim ilead As Object ' ksPosLeaderParam

        ilead = kompas.GetParamStruct(Kompas6Constants.StructType2DEnum.ko_PosLeaderParam)
        Dim itLinePar As Object ' ksTextLineParam

        itLinePar = kompas.GetParamStruct(Kompas6Constants.StructType2DEnum.ko_TextLineParam)
        Dim iItemPar As Object ' ksTextItemParam
        iItemPar = kompas.GetParamStruct(Kompas6Constants.StructType2DEnum.ko_TextItemParam)
        Dim itFont As Object ' ksTextItemFont
        itFont = iItemPar.GetItemFont()

        Dim itMathPoint As Object ' ksMathPointParam
        itMathPoint = kompas.GetParamStruct(Kompas6Constants.StructType2DEnum.ko_MathPointParam)

        Dim iptext As Object
        Dim iTextItemArr As Object
        Dim ipPolyLin As Object
        Dim ipMathPoint As Object
        Dim obj As Integer ' ksDynamicArray
        If Not ilead Is Nothing And Not itLinePar Is Nothing And Not iItemPar Is Nothing And Not itFont Is Nothing And Not itMathPoint Is Nothing Then
            ilead.Init()
            itLinePar.Init()
            iItemPar.Init()
            itFont.Init()
            itMathPoint.Init()
            itFont.SetBitVectorValue(NEW_LINE, True)
            itLinePar.Style = 0
            iptext = ilead.GetpTextline()
            iTextItemArr = itLinePar.GetTextItemArr()

            iItemPar.s = "1"
            iTextItemArr.ksAddArrayItem(-1, iItemPar)
            iptext.ksAddArrayItem(-1, itLinePar)

            ipPolyLin = ilead.GetpPolyline
            ipMathPoint = kompas.GetDynamicArray(POINT_ARR)

            If Not ipPolyLin Is Nothing And Not ipMathPoint Is Nothing Then
                itMathPoint.x = 10
                itMathPoint.y = 10

                ipMathPoint.ksAddArrayItem(-1, itMathPoint)
                ipPolyLin.ksAddArrayItem(-1, ipMathPoint)

                itMathPoint.x = 30
                itMathPoint.y = 10
                ipMathPoint.ksClearArray()
                ipMathPoint.ksAddArrayItem(-1, itMathPoint)
                ipPolyLin.ksAddArrayItem(-1, ipMathPoint)

                ilead.SetpPolyline(ipPolyLin)
            End If

            ' заполним параметры
            ilead.x = 50 ' координаты базовой точки ( начало полки )
            ilead.y = 50
            ilead.arrowType = 1
            ilead.dirX = -1

            obj = doc.ksPositionLeader(ilead)
            If obj Then
                doc.ksGetObjParam(obj, ilead, ALLPARAM)
                ilead.x = 100
                '.ksMessage("Поменяем параметры")
                doc.ksSetObjParam(obj, ilead, ALLPARAM)
            End If
        End If
    End Sub

    '====================================================================================================================
    '==================================ФУНКЦИИ ИЗЖИЛИ СЕБЯ===============================================================
    Function mySheetViewCreate(Document2D As ksDocument2D, name As String, x As Double, y As Double, Optional angle As Double = 0, Optional scale As Double = 1) As Integer
        Dim refSheetView As Integer
        Dim ViewParam As ksViewParam
        Dim ViewNumber As Integer

        ViewParam = kompas.GetParamStruct(StructType2DEnum.ko_ViewParam)        ' парметр GetParamStruct(8) выбирается из SDK - StructType2D -> ko_ViewParam -> ksViewParam, который относится к "Видам чертежа"

        ViewParam.name = name
        ViewParam.x = 370
        ViewParam.y = 235
        ViewParam.scale_ = 1
        ViewParam.angle = 0
        ViewNumber = 1

        refSheetView = Document2D.ksCreateSheetView(ViewParam, ViewNumber)

        Document2D.ksOpenView(0)

        Return refSheetView
    End Function

    Function mySheetViewRename(Document2D As ksDocument2D, sheetView As Integer, name As String, x As Double, y As Double, Optional angle As Double = 0, Optional scale As Double = 1) As Integer

        Document2D.ksDeleteObj(sheetView)

        Dim refSheetView As Integer
        Dim ViewParam As ksViewParam
        Dim ViewNumber As Integer

        ViewParam = kompas.GetParamStruct(StructType2DEnum.ko_ViewParam)        ' парметр GetParamStruct(8) выбирается из SDK - StructType2D -> ko_ViewParam -> ksViewParam, который относится к "Видам чертежа"

        ViewParam.name = name
        ViewParam.x = 370
        ViewParam.y = 235
        ViewParam.scale_ = 1
        ViewParam.angle = 0
        ViewNumber = 1

        refSheetView = Document2D.ksCreateSheetView(ViewParam, ViewNumber)

        Document2D.ksOpenView(0)

        Return refSheetView
    End Function

End Module
