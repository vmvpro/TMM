Imports Kompas6API5, Kompas6Constants, KGAXLib

Public Class Form2
    Dim kompas As Object
    Dim Document2D As ksDocument2D
    Dim DocumParam As ksDocumentParam
    Dim StandartSheet As ksStandartSheet
    Dim SheetPar As ksSheetPar
    Dim str As String

    '-------------------------Книга С++ и компьтерная графика---------------------------
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim obj As Object

        Document2D.ksPolyline(1)

        Document2D.ksPoint(30, 10, 0)
        Document2D.ksPoint(40, 30, 0)
        Document2D.ksPoint(50, 10, 0)
        Document2D.ksPoint(60, 50, 0)
        obj = Document2D.ksEndObj

        Document2D.ksLightObj(0, 2)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        'Dim pDoc As ksDocument2D

        With Document2D
            .ksLineSeg(-10, 10, 10, 10, 1)
            .ksLineSeg(-10, 0, -10, 10, 1)
            .ksLineSeg(10, 0, 10, 10, 1)
            .ksCircle(0, 0, 5, 1)
            .ksArcByPoint(0, 0, 10, -10, 0, 10, 0, 1, 1)
            .ksEndObj()
        End With
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        Document2D.ksMtr(30, 0, 0, 0.5, 0.5)

        With Document2D
            .ksLineSeg(-10, 10, 10, 10, 1)
            .ksLineSeg(-10, 0, -10, 10, 1)
            .ksLineSeg(10, 0, 10, 10, 1)
            .ksCircle(0, 0, 5, 1)
            .ksArcByPoint(0, 0, 10, -10, 0, 10, 0, 1, 1)
            .ksEndObj()
        End With

        Document2D.ksDeleteMtr()

        Document2D.ksMtr(60, 0, 30, 1, 1)

        With Document2D
            .ksLineSeg(-10, 10, 10, 10, 1)
            .ksLineSeg(-10, 0, -10, 10, 1)
            .ksLineSeg(10, 0, 10, 10, 1)
            .ksCircle(0, 0, 5, 1)
            .ksArcByPoint(0, 0, 10, -10, 0, 10, 0, 1, 1)
            .ksEndObj()
        End With

        Document2D.ksDeleteMtr()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Obj As Object
        Dim ObjType As Integer
        Dim pParms As Kompas6API5.ksCircleParam = kompas.GetParamStruct(20) 'StructType2DEnum.ko_CircleParam)

        Obj = Document2D.ksCircle(30, 20, 10, 1)
        'ldefin2d.TECHNICAL_DEMAND_PAR

        ObjType = Document2D.ksGetObjParam(Obj, pParms, 0) 'ksDrawingObjectParamTypeEnum.ksAllParam)

        pParms.rad = 20

        'Debug.WriteLine(ObjType)
        'Debug.WriteLine(pParms.xc)
        'Debug.WriteLine(pParms.yc)
        'Debug.WriteLine(pParms.rad)
        'Debug.WriteLine(pParms.style)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim rad As Integer = 10
        For i As Int16 = 1 To 5
            Document2D.ksCircle(0, 0, rad, 1)
            rad += 10
        Next
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim Obj As System.Int32

        Obj = Document2D.ksNewGroup(0)
        Document2D.ksLineSeg(20, 10, 20, 30, 1)
        Document2D.ksLineSeg(20, 30, 40, 30, 1)
        Document2D.ksLineSeg(40, 30, 40, 10, 1)
        Document2D.ksLineSeg(40, 10, 20, 10, 1)
        Document2D.ksEndGroup()



        Document2D.ksSymmetryObj(Obj, 40, 10, 40, 20, "1")

        kompas.ksMessageBoxResult()

    End Sub


    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim Obj As System.Int32
        Dim pDoc As ksDocument2D

        pDoc = kompas.ActiveDocument2D
        'Obj = Document2D.ksNewGroup(1)

        'Document2D.ksLineSeg(20, 10, 20, 30, 1)

        pDoc.ksLineSeg(20, 10, 20, 30, 1)

        Debug.WriteLine(Obj.GetType)

    End Sub


    '-------------ВЫХОД--------------'
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        End
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim pIter As ksIterator = kompas.GetIterator
        'If pIter = vbEmpty Then Exit Sub





    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

    End Sub
End Class