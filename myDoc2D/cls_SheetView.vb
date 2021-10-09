Imports Kompas6API5, Kompas6Constants
Public Class cls_SheetView
	'Implements ksViewParam
	Protected refSheetView As Integer

	Protected ViewParam As ksViewParam
	Protected ViewNumber As Integer


	Protected kompas As Kompas6API5.Application
	Protected doc2D As myDoc2D
	Protected ksDoc2D As ksDocument2D

	Protected x_ As Double = 0
	Protected y_ As Double = 0
	Protected state_ As UShort = 0

	Protected name_ As String = Nothing
	Protected angle_ As Double
	Protected scale_ As Double
	Protected color_ As Integer

	Public Sub New(doc2D As myDoc2D)

		'kompas = kompasApp()
		Me.doc2D = doc2D

		refSheetView = -1

		ViewParam = doc2D.RefKompas.GetParamStruct(StructType2DEnum.ko_ViewParam)

	End Sub

	Public Sub New(name As String, x As Double, y As Double, Optional angle As Double = 0, Optional scale As Double = 1, Optional color As Double = 0)
		refSheetView = -1
		'kompas = kompasApp()
		name_ = name
		x_ = x
		y_ = y
		angle_ = angle
		scale_ = scale
		color_ = color

		'ViewParam = kompas.GetParamStruct(StructType2DEnum.ko_ViewParam)
	End Sub

	Public Overloads ReadOnly Property Ref As Integer
		Get
			Return refSheetView
		End Get

	End Property

	Private Function Create(Document2D As ksDocument2D) As Integer

		If refSheetView = -1 Then
			doc2D = Document2D
			ViewParam.Init()
			ViewParam.name = name_
			ViewParam.x = x_
			ViewParam.y = y_
			ViewParam.scale_ = scale_
			ViewParam.color = color_
			ViewParam.angle = angle_

			ViewNumber = doc2D.RefDoc.ksNewViewNumber
			refSheetView = doc2D.RefDoc.ksCreateSheetView(ViewParam, ViewNumber)
		End If

		doc2D.RefDoc.ksOpenView(0)

		Return refSheetView
	End Function

	Public Function Create() As Integer
		Try
			If refSheetView = -1 Then

				ViewParam.Init()
				ViewParam.name = name_
				ViewParam.x = x_
				ViewParam.y = y_
				ViewParam.scale_ = scale_
				ViewParam.color = color_
				ViewParam.angle = angle_

				ViewNumber = doc2D.RefDoc.ksNewViewNumber
				refSheetView = doc2D.RefDoc.ksCreateSheetView(ViewParam, ViewNumber)
			End If

			doc2D.RefDoc.ksOpenView(0)

			Return refSheetView
		Catch ex As Exception

			MessageBox.Show("Ошибка! При этой ошибке требуется сначала добавить вид на лист")
			Throw New Exception(ex.Message)
		End Try

	End Function

	Private Function Create(Document2D As myDoc2D) As Integer

		If Document2D.ExistSheetViewName(Me) Then
			Throw New Exception("Ошибка! Сначала, что бы создать лист требуется его добавить к документу")
		End If

		If refSheetView = -1 Then
			doc2D = Document2D.RefDoc
			ViewParam.Init()
			ViewParam.name = name_
			ViewParam.x = x_
			ViewParam.y = y_
			ViewParam.scale_ = scale_
			ViewParam.color = color_
			ViewParam.angle = angle_

			ViewNumber = doc2D.RefDoc.ksNewViewNumber
			refSheetView = doc2D.RefDoc.ksCreateSheetView(ViewParam, ViewNumber)
		End If

		doc2D.RefDoc.ksOpenView(0)

		Return refSheetView
	End Function

	Public Sub Delete()

		If Not refSheetView = -1 Then
			doc2D.RefDoc.ksOpenView(0)

			doc2D.RefDoc.ksDeleteObj(refSheetView)

			refSheetView = -1
		End If

	End Sub



	Public Overloads Property StateView As UShort
		Get
			Return state_
		End Get
		Set(value As UShort)
			doc2D.RefDoc.ksDeleteObj(refSheetView)
			refSheetView = -1
			state_ = value

			Create(doc2D)

		End Set
	End Property


	Public Overloads Property X As Double
		Get
			Return x_
		End Get
		Set(value As Double)
			Dim tmp_coordinate As Double = x_
			Dim s As Double
			x_ = value

			s = value - tmp_coordinate

			doc2D.RefDoc.ksMoveObj(refSheetView, s, 0)

		End Set
	End Property

	Public Overloads Property Y As Double
		Get
			Return y_
		End Get
		Set(value As Double)
			Dim tmp_coordinate As Double = y_
			Dim s As Double
			y_ = value

			s = value - tmp_coordinate

			doc2D.RefDoc.ksMoveObj(refSheetView, 0, s)

		End Set
	End Property


	Public Overloads Property ScaleView As Double
		Get
			Return scale_
		End Get
		Set(value As Double)
			Dim tmp_scale As Double = value / scale_

			doc2D.RefDoc.ksMtr(0, 0, 0, tmp_scale, tmp_scale)
			doc2D.RefDoc.ksTransformObj(refSheetView)
			doc2D.myMtrDelete()
			scale_ = value
		End Set
	End Property



	Public Overloads Property AngleView As Double
		Get
			Return angle_
		End Get
		Set(value As Double)
			angle_ = value
			doc2D.RefDoc.ksRotateObj(refSheetView, 0, 0, angle_)
		End Set
	End Property


	Public Overloads Property ColorView As Double
		Get
			Return color_
		End Get
		Set(value As Double)
			color_ = value
		End Set
	End Property



	Public Overloads Property Name As String
		Get
			Return name_
		End Get
		Set(value As String)
			name_ = value
		End Set
	End Property

	Public Sub Active()
		If refSheetView = -1 Then
			Throw New Exception("Ошибка! Сначала, что бы сделать лист активным его требуется создать! ")
		End If

		If Not doc2D.ExistSheetViewName(Me) Then
			Throw New Exception("Ошибка! Сначала, что бы сделать лист активным требуется его добавить к документу")
		End If

		doc2D.RefDoc.ksOpenView(ViewNumber)
	End Sub


	Function kompasApp() As Kompas6API5.Application

		Try
			kompas = GetObject(, "KOMPAS.Application.5")
		Catch ex As Exception
			kompas = CreateObject("KOMPAS.Application.5") ' Подключаемся к КОМПАСу
		End Try

		Return kompas

	End Function

	Public Sub ExitView()
		doc2D.RefDoc.ksOpenView(0)
	End Sub





End Class


