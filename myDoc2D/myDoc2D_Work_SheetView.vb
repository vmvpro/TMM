Imports System.Runtime.CompilerServices
Imports Kompas6API5, Kompas6Constants

Partial Public Class myDoc2D

    Shared countSheetView_ As Integer = 0

	Dim sheetView1 As cls_SheetView
    Dim listSheetView As List(Of cls_SheetView)

    Dim dictionarySheetView As Dictionary(Of String, cls_SheetView)


#Region "   Метод для добавления листа в документ   "



	''' <summary>
	''' Метод для добавления листа в документ. Имеются исключения, так если использовать метод в исполнении, не забывайте заключать его в блок Try...Catch
	''' </summary>
	''' <param name="sheetView1"></param>
	''' <remarks></remarks>
	Public Sub AddView(ByRef sheetView1 As cls_SheetView)
		For i As Integer = 0 To listSheetView.Count - 1
			If sheetView1.Name = listSheetView(i).Name Then
				If sheetView1.Ref = listSheetView(i).Ref Or sheetView1.Name = listSheetView(i).Name Then
					Throw New Exception("Ошибка! В документе можно использовать идентичное имя вида. В данном случае ошибка вызвана тем, что попытались добавить вид который с таким именем уже в документе существует.")
				End If
			End If
		Next

		If Not sheetView1.Ref = -1 Then
			Throw New Exception("Ошибка! Вид может пренадлежат только одному документу и добавление происходит один раз.")
		End If

		If sheetView1.Name Is Nothing Then
			Throw New Exception("Ошибка! Виду не присвоено имя в классе")
		End If

		Dim newSheetView As New cls_SheetView(Me)
		newSheetView.Name = sheetView1.Name
		newSheetView.X = sheetView1.X
		newSheetView.Y = sheetView1.Y
		newSheetView.AngleView = sheetView1.AngleView
		newSheetView.ScaleView = sheetView1.ScaleView
		newSheetView.ColorView = sheetView1.ColorView

		sheetView1 = newSheetView

        listSheetView.Add(sheetView1)
        dictionarySheetView.Add(sheetView1.Name, sheetView1)
		countSheetView_ += 1
	End Sub

	Public Sub AddView(ByRef sheetView1 As cls_Test_SheetView)
		For i As Integer = 0 To listSheetView.Count - 1
			If sheetView1.Name = listSheetView(i).Name Then
				If sheetView1.Ref = listSheetView(i).Ref Or sheetView1.Name = listSheetView(i).Name Then
					Throw New Exception("Ошибка! В документе можно использовать идентичное имя вида. В данном случае ошибка вызвана тем, что попытались добавить вид который с таким именем уже в документе существует.")
				End If
			End If
		Next

		If Not sheetView1.Ref = -1 Then
			Throw New Exception("Ошибка! Вид может пренадлежат только одному документу и добавление происходит один раз.")
		End If

		If sheetView1.Name Is Nothing Then
			Throw New Exception("Ошибка! Виду не присвоено имя в классе")
		End If

		Dim newSheetView As New cls_Test_SheetView(Me)
		newSheetView.Name = sheetView1.Name
		newSheetView.X = sheetView1.X
		newSheetView.Y = sheetView1.Y
		newSheetView.AngleView = sheetView1.AngleView
		newSheetView.ScaleView = sheetView1.ScaleView
		newSheetView.ColorView = sheetView1.ColorView

		sheetView1 = newSheetView

		listSheetView.Add(sheetView1)
		countSheetView_ += 1
	End Sub

#End Region

    Public Function ActiveSheet(sheetName As String) As cls_SheetView
        Try
            Return dictionarySheetView(sheetName)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Сделать текущим системный вид ( номер 0 )
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SystemSheet()
        doc2D.ksOpenView(0)
    End Sub


#Region "   Проверка на существование листа в документе   "

	''' <summary>
	''' Проверка на существование листа в документе (Возвращает True если лист есть) Проверка происходит по имени вида
	''' </summary>
	''' <param name="sheetView1"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Function ExistSheetViewName(sheetView1 As cls_SheetView) As Boolean
		Dim bool As Boolean

		Dim f As Object = listSheetView.AsEnumerable().Where(Function(sv) sv.Name = sheetView1.Name)

		Return bool = (f Is Nothing)

	End Function

#End Region

#Region "   Свойство - Количество листов в документе   "

    ''' <summary>
    ''' Свойство для чтения на количество листов в документе
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Count As Integer
        Get
            Return countSheetView_
        End Get
    End Property

#End Region
End Class
