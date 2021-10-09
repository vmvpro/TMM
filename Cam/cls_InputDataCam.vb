Imports Kompas6API5, Kompas6Constants
Imports Excel = Microsoft.Office.Interop.Excel

Public Class cls_InputDataCam
    Protected kompas As KompasObject
    Protected ActiveDoc2D As ksDocument2D
    Protected DocumParam As ksDocumentParam
    Protected StandartSheet As ksStandartSheet
    Protected SheetPar As ksSheetPar
    Protected str As String
    Protected doc2D As myDoc2D

    Public Sub New()

    End Sub

    Public Sub New(ActiveDoc2D As ksDocument2D)

        doc2D = New myDoc2D(ActiveDoc2D)
    End Sub

    Protected mp_O0 As New PointDouble(0, 0)

    Protected mp_O As PointDouble



    ''' <summary>
    ''' Ширина всего листа
    ''' </summary>
    ''' <remarks></remarks>
    Protected sheetWidth = 594

    ''' <summary>
    ''' Высота всего листа
    ''' </summary>
    ''' <remarks></remarks>
    Protected sheetHeight = 420

    ''' <summary>
    ''' Отступ слева с учетом расстояния S согласно задания
    ''' </summary>
    ''' <remarks></remarks>
    Protected leftDistance As Double

    ''' <summary>
    ''' Верхняя координата для прорссовки графика
    ''' </summary>
    ''' <remarks></remarks>
    Protected upDistance As Double

    ''' <summary>
    ''' Нижняя координата для прорссовки графика
    ''' </summary>
    ''' <remarks></remarks>
    Protected downDistance As Double

    ''' <summary>
    ''' Шаг диаграммы всего рисунка и углы кулачка
    ''' </summary>
    ''' <remarks></remarks>
    Protected step_Diagram As Double

    ''' <summary>
    ''' Высота первой диаграммы и по которой будет выстроен масштаб
    ''' </summary>
    ''' <remarks></remarks>
    Protected h As Double

    ''' <summary>
    ''' Масштаб всех диаграмм по выбранной высоте
    ''' </summary>
    ''' <remarks></remarks>
    Protected m As Double

    ''' <summary>
    ''' Угол лямда согласно задания
    ''' </summary>
    ''' <remarks></remarks>
    Protected angleLamda As Double

    ''' <summary>
    ''' Радиус кулачка
    ''' </summary>
    ''' <remarks></remarks>
    Protected r0 As Double

    ''' <summary>
    ''' Кут вiддалення
    ''' </summary>
    ''' <remarks></remarks>
    Protected AngleDistance As Double

    ''' <summary>
    ''' Кут дальнього вистою
    ''' </summary>
    ''' <remarks></remarks>
    Protected AngleDistanccePlacing As Double

    ''' <summary>
    ''' Кут повороту
    ''' </summary>
    ''' <remarks></remarks>
    Protected AngleRotation As Double

    ''' <summary>
    ''' Начальная точка для S = 30
    ''' </summary>
    ''' <remarks></remarks>
    Protected S As Double

    ''' <summary>
    ''' Точка Smax по модулю Si
    ''' </summary>
    ''' <remarks></remarks>
    Protected Smax As Double

    ''' <summary>
    ''' Диаграмма S1 - S с двумя штрихами
    ''' </summary>
    ''' <remarks></remarks>
    Protected Si_1() As Double

    ''' <summary>
    ''' Диаграмма S2 - S с двумя штрихами
    ''' </summary>
    ''' <remarks></remarks>
    Protected Si_2() As Double

    ''' <summary>
    ''' Диаграмма 2 - S с одним штрихом
    ''' </summary>
    ''' <remarks></remarks>
    Protected S_v() As Double

    ''' <summary>
    ''' Коефициент k1 (Excel)
    ''' </summary>
    ''' <remarks></remarks>
    Protected k1() As Double

    ''' <summary>
    ''' Коефициент k2 (Excel)
    ''' </summary>
    ''' <remarks></remarks>
    Protected k2() As Double

    ''' <summary>
    ''' Прискорення (Excel)
    ''' </summary>
    ''' <remarks></remarks>
    Protected ak1() As Double

    ''' <summary>
    ''' Прискорення (Excel)
    ''' </summary>
    ''' <remarks></remarks>
    Protected ak2() As Double

    ''' <summary>
    ''' Диаграмма S_ak1
    ''' </summary>
    ''' <remarks></remarks>
    Protected S_ak1() As Double

    ''' <summary>
    ''' Длина перемещений точек на диаграмме 3 (Первый график)
    ''' </summary>
    ''' <remarks></remarks>
    Protected Si1_len() As Double

    ''' <summary>
    ''' Диаграмма S_ak2
    ''' </summary>
    ''' <remarks></remarks>
    Protected S_ak2() As Double

    ''' <summary>
    ''' Длина перемещений точек на диаграмме 3 (Второй график)
    ''' </summary>
    ''' <remarks></remarks>
    Protected Si2_len() As Double

    ''' <summary>
    ''' Позицiйний iнварiант швидкостi (Excel)
    ''' </summary>
    ''' <remarks></remarks>
    Protected bk() As Double

    ''' <summary>
    ''' Диаграмма S_bk1
    ''' </summary>
    ''' <remarks></remarks>
    Protected S_bk1() As Double

    ''' <summary>
    ''' Диаграмма S_bk2
    ''' </summary>
    ''' <remarks></remarks>
    Protected S_bk2() As Double

    ''' <summary>
    ''' Позицiйний iнварiант прискорення (Excel)
    ''' </summary>
    ''' <remarks></remarks>
    Protected ck() As Double

    ''' <summary>
    ''' Позицiйний iнварiант прискорення (Excel)
    ''' </summary>
    ''' <remarks></remarks>
    Protected ck2() As Double

    ''' <summary>
    ''' Точки кулачка
    ''' </summary>
    ''' <remarks></remarks>
    Protected pointsCam() As PointDouble


    ''' <summary>
    ''' Высота
    ''' </summary>
    ''' <remarks></remarks>
    Protected h_ As Double

    ''' <summary>
    ''' Диаграмма 1
    ''' </summary>
    ''' <remarks></remarks>
    Protected diagram1_SheetView As cls_SheetView

    ''' <summary>
    ''' Диаграмма 2
    ''' </summary>
    ''' <remarks></remarks>
    Protected diagram2_SheetView As cls_SheetView

    ''' <summary>
    ''' Диаграмма 3
    ''' </summary>
    ''' <remarks></remarks>
    Protected diagram3_SheetView As cls_SheetView

    ''' <summary>
    ''' Вид кулачка
    ''' </summary>
    ''' <remarks></remarks>
    Protected cam_SheetView As cls_SheetView

    ''' <summary>
    ''' Определяем масштаб первой диаграммы  
    ''' </summary>
    ''' <remarks></remarks>
    Protected mashtab_diagram1 As Double

    ''' <summary>
    ''' Масштаб диаграммы 1 вида на чертеже
    ''' </summary>
    ''' <remarks></remarks>
    Protected mashtab_diagram1_view As Double

    ''' <summary>
    ''' Nf,kbwf
    ''' </summary>
    ''' <remarks></remarks>
    Protected table1 As DataTable

    Protected Function diagramTableExcel(rangeName As String) As DataTable

        Dim objExcel As Excel.Application

        Try
            objExcel = GetObject(, "Excel.Application")
        Catch ex As Exception
            objExcel = CreateObject("Excel.Application") ' Подключаемся к Excel
        End Try

        Dim bookExcel As Excel.Workbook =
            objExcel.Workbooks.Open(Environment.CurrentDirectory & "\DiagramCam.xlsm")
        'objExcel.Visible = True

        Dim sheet As Excel.Worksheet = bookExcel.ActiveSheet
        Dim rng As Excel.Range = objExcel.Range(rangeName)

        Dim tbl As New DataTable
        tbl.Columns.Add("k1", GetType(Double))
        tbl.Columns.Add("k2", GetType(Double))
        tbl.Columns.Add("ak1", GetType(Double))
        tbl.Columns.Add("ak2", GetType(Double))
        tbl.Columns.Add("bk", GetType(Double))
        tbl.Columns.Add("ck", GetType(Double))
        tbl.Columns.Add("ck2", GetType(Double))

        Dim count As Integer = rng.Rows.Count

        ReDim k1(count)
        ReDim k2(count)
        ReDim ak1(count)
        ReDim ak2(count)
        ReDim bk(count)
        ReDim ck(count)

        ReDim ck2(count)


        ReDim Si_1(count)
        ReDim Si_2(count)

        ReDim S_bk1(count)
        ReDim S_bk2(count)

        ReDim S_ak1(count)
        ReDim S_ak2(count)

        ReDim S_v(count)

        'ak() As Double
        'bk() As Double
        'ck() As Double

        Dim valueCol1 As Double
        Dim valueCol2 As Double
        Dim valueCol3 As Double
        Dim valueCol4 As Double
        Dim valueCol5 As Double
        Dim valueCol6 As Double
        Dim valueCol7 As Double

        For i As Integer = 1 To rng.Rows.Count

            k1(i) = Math.Round(rng.Cells(i, 1).Value, 4)
            k2(i) = Math.Round(rng.Cells(i, 2).Value, 4)
            ak1(i) = Math.Round(rng.Cells(i, 3).Value, 4)
            ak2(i) = Math.Round(rng.Cells(i, 4).Value, 4)
            bk(i) = Math.Round(rng.Cells(i, 5).Value, 4)
            ck(i) = Math.Round(rng.Cells(i, 6).Value, 4)

            ck2(i) = Math.Round(rng.Cells(i, 7).Value, 4)

            tbl.Rows.Add(valueCol1, valueCol2, valueCol3, valueCol4, valueCol5, valueCol6, valueCol7)

        Next

        bookExcel.Close()

        If objExcel.Workbooks.Count = 0 Then
            objExcel.Quit()
        End If



        Return tbl

    End Function




End Class
