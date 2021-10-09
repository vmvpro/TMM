Imports Kompas6API5, Kompas6Constants
Imports TMM.myDoc2D

Partial Public Class cls_Test_SheetView
	Inherits cls_SheetView

	Dim vl_Pb As cls_Segment

	Public Sub New(doc2D As myDoc2D)
		MyBase.New(doc2D)

		Me.doc2D = doc2D
		refSheetView = -1
		ViewParam = doc2D.RefKompas.GetParamStruct(StructType2DEnum.ko_ViewParam)

		vl_Pb = New cls_Segment(doc2D)
	End Sub

	Public Sub New(name As String, x As Double, y As Double, Optional angle As Double = 0, Optional scale As Double = 1, Optional color As Double = 0)
		MyBase.New(name, x, y, angle, scale, color)
		vl_Pb = New cls_Segment(doc2D)

		refSheetView = -1
		'kompas = kompasApp()
		name_ = name
		x_ = x
		y_ = y
		angle_ = angle
		scale_ = scale
		color_ = color
	End Sub

	Public Property Pb As cls_Segment
		Get
			Return vl_Pb
		End Get
		Set(value As cls_Segment)
			vl_Pb = value
		End Set
	End Property


End Class
