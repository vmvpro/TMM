Public Class PointDouble

    Dim pointName As String = ""
    Dim dimX As Double
    Dim dimY As Double
    Dim style_ As Integer


    Public Sub New()
        pointName = ""
        dimX = 0
        dimY = 0
    End Sub

    Public Sub New(X As Double, Y As Double)
        Me.dimX = X
        Me.dimY = Y
    End Sub

    Public Sub New(pointName As String, X As Double, Y As Double)
        Me.pointName = pointName
        Me.dimX = X
        Me.dimY = Y
    End Sub

    Public Property Name() As String
        Get
            Return pointName
        End Get
        Set(value As String)
            pointName = value
        End Set
    End Property

    Public Property X() As Double
        Get
            Return dimX
        End Get
        Set(value As Double)
            dimX = value
        End Set
    End Property

    Public Property Y() As Double
        Get
            Return dimY
        End Get
        Set(value As Double)
            dimY = value
        End Set
    End Property

    Public Function ToStringPoint() As String
        'Return String.Format("{0}({1};{2})", pointName, Math.Round(dimX, 2), Math.Round(dimY, 2))
        Return String.Format("{0};{1}", dimX, dimY)
    End Function

    Overloads Function ToString() As String
        'Return String.Format("{0}({1};{2})", pointName, Math.Round(dimX, 2), Math.Round(dimY, 2))
        Return String.Format("{0}({1};{2})", pointName, Math.Round(dimX, 2), Math.Round(dimY, 2))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overloads Function ToStringScale(scale As Double) As String
        Return String.Format("{0}({1};{2})", pointName, Math.Round(dimX / scale, 2), Math.Round(dimY / scale, 2))
    End Function




End Class
