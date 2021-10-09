Imports System.Runtime.CompilerServices
Imports Kompas6API5, Kompas6Constants

Public Class cls_Segment
	Dim kompas As Kompas6API5.Application
	Dim activeDoc2D As ksDocument2D
	Dim doc2D As myDoc2D
	Dim ViewParam As ksViewParam

	Dim ref_ As Integer
	Dim segName_ As String

	Dim t1_ As PointDouble
    Dim t2_ As PointDouble
    Dim lenght_ As Double

	Public Sub New(doc2D As myDoc2D)
		Me.doc2D = doc2D
		t1_ = New PointDouble
		t2_ = New PointDouble
	End Sub

	Public Sub New(SegName As String, T1 As PointDouble, T2 As PointDouble)
		Me.doc2D = doc2D
		Me.segName_ = SegName
		Me.t1_ = T1
        Me.t2_ = T2
        Me.lenght_ = doc2D.myCalculateLenghtTwoPoint(t1_, t2_)
	End Sub

	Public Property Name() As String
		Get
			Return segName_
		End Get
		Set(value As String)
			segName_ = value
		End Set
	End Property

	Public Property T1() As PointDouble
		Get
			Return t1_
		End Get
		Set(value As PointDouble)
			t1_ = value
		End Set
	End Property

	Public Property T2() As PointDouble
		Get
			Return t2_
		End Get
		Set(value As PointDouble)
			t2_ = value
		End Set
	End Property

	Public ReadOnly Property Lenght() As Double
		Get
            Return lenght_
		End Get
	End Property

	'  Overloads Function ToString() As String
	''Return String.Format("{0}({1};{2})", SegName, Math.Round(T1_, 2), Math.Round(T2_, 2))
	'  End Function

End Class
