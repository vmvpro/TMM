Partial Public Class clsTest



    Public Sub test1()

        ' Подготовка к черчению
        ' Сначала создадим новый вид на 
        ' для построения нового чертежа создадим новый 'Вид'

        ' команда на создание нового вида на чертеже
        ' принимает 4-параметра: имя вида, координата х, координата y, угол поворота, масштаб
		Dim v1 As New cls_SheetView("v1", 100, 200, 0, 1) ' 

		' Выполним команду: дабавим лист в документ
		doc2D.AddView(v1)

		' Выполним команду: создать вид
		v1.Create()

		' Выполнить команду: сделать лист активным
		v1.Active()

		' Подготовка выполнена
		'----------------------------------------------------------------------------------

		' Таким образом объявляем точки с которыми будем работать
		Dim A As PointDouble				' объявляем точку А
		A = New PointDouble("A", 40, 10)	' присваиваем точке А данные: имя точки, координата х, координата y

		Dim B As New PointDouble("B", 80, 50)	'точка B  можно и так сразу объявить и присоить значения
		Dim C As New PointDouble("C", 20, 60)	'точка C

		Dim l As Double		' объявляем переменную: длина 

		' Отрисуем все точки
		doc2D.myDrawPoint(A)
		doc2D.myDrawPoint(B)
		doc2D.myDrawPoint(C)

		' Отрисуем отрезки по точкам
		doc2D.myDrawLineSeg(A, B)
		doc2D.myDrawLineSeg(B, C)
		doc2D.myDrawLineSeg(C, A)

		' Определить длину отрезка по точкам
		l = doc2D.myCalculateLenghtTwoPoint(A, B)

		' Определить длину отрезка по точкам
		l = doc2D.myCalculateLenghtTwoPoint(A, B)

		' Команда выход из вида
		v1.ExitView()


		' Реальная длина отрезка на чертеже
		Dim lenTest1 As Double = doc2D.myAbsoluteLenghtTwoPoint(v1, A, B)

		' Реальное размещение точки на чертеже
		Dim newPoint1 As PointDouble = doc2D.myAbsoluteCordinatePoint(v1, A)



	End Sub

	Public Sub test2()

		' Подготовка к черчению
		' Сначала создадим новый вид на 
		' для построения нового чертежа создадим новый 'Вид'

		' команда на создание нового вида на чертеже
		' принимает 4-параметра: имя вида, координата х, координата y, угол поворота, масштаб
		Dim v1 As New cls_SheetView("v1", 100, 200, 0, 1) ' 

		' Выполним команду: дабавим лист в документ
		doc2D.AddView(v1)

		' Выполним команду: создать вид
		v1.Create()

		' Выполнить команду: сделать лист активным
		v1.Active()

		' Подготовка выполнена
		'----------------------------------------------------------------------------------

		' Таким образом объявляем точки с которыми будем работать
		Dim A As PointDouble				' объявляем точку А
		A = New PointDouble("A", 40, 10)	' присваиваем точке А данные: имя точки, координата х, координата y

		Dim B As New PointDouble("B", 80, 50)	'точка B  можно и так сразу объявить и присоить значения

		Dim lineC As New PointDouble("lineC", 0, -20)

		'Dim C As PointDouble = doc2D.myCalculateCordinatePoint(B,210,lineC,

		Dim l As Double		' объявляем переменную: длина 

		' Отрисуем все точки
		doc2D.myDrawPoint(A)
		doc2D.myDrawPoint(B)


		' Отрисуем отрезки по точкам
		doc2D.myDrawLineSeg(A, B)



		' Определить длину отрезка по точкам
		l = doc2D.myCalculateLenghtTwoPoint(A, B)

		' Определить длину отрезка по точкам
		l = doc2D.myCalculateLenghtTwoPoint(A, B)

		' Команда выход из вида
		v1.ExitView()


		' Реальная длина отрезка на чертеже
		Dim lenTest1 As Double = doc2D.myAbsoluteLenghtTwoPoint(v1, A, B)

		' Реальное размещение точки на чертеже
		Dim newPoint1 As PointDouble = doc2D.myAbsoluteCordinatePoint(v1, A)



	End Sub
End Class
