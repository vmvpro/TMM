Imports Kompas6API5, Kompas6Constants

Public Class frm_Test2

    Dim kompas As Kompas6API5.Application
    Dim Document2D As ksDocument2D

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        loadForm()
    End Sub

    Sub loadForm()
        Try
            kompas = kompasApp()
            'kompas.Visible = False
            Document2D = kompas.Document2D
            Document2D.ksOpenDocument(Environment.CurrentDirectory & "\ШаблонЧертежа.cdw", False)
            Document2D.ksSaveDocumentEx(Environment.CurrentDirectory & "\ШаблонЧертежа (1).cdw", 1)
            Document2D.ksCloseDocument()

            Document2D = kompas.Document2D
            Document2D.ksOpenDocument(Environment.CurrentDirectory & "\ШаблонЧертежа (1).cdw", False)

            'kompas.Visible = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Document2D.ksLine(0, 0, 0)
        Document2D.ksLine(0, 0, 90)
        Document2D.ksLine(0, 420, 0)
        Document2D.ksLine(594, 0, 90)

    End Sub

    Dim view1 As Integer
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim ViewParam As ksViewParam
        Dim ViewParam2 As ksViewParam
        Dim ViewNumber As Integer

        ViewParam = kompas.GetParamStruct(StructType2DEnum.ko_ViewParam)        ' парметр GetParamStruct(8) выбирается из SDK - StructType2D -> ko_ViewParam -> ksViewParam, который относится к "Видам чертежа"
        ViewParam2 = kompas.GetParamStruct(StructType2DEnum.ko_ViewParam)        ' парметр GetParamStruct(8) выбирается из SDK - StructType2D -> ko_ViewParam -> ksViewParam, который относится к "Видам чертежа"

        ViewParam.name = "Главный вид"
        ViewParam.x = 100
        ViewParam.y = 190
        'ViewNumber = 0

        'ViewParam.

        Document2D.ksCreateSheetView(ViewParam, ViewNumber)
        Document2D.ksOpenView(0)


    End Sub

    Dim bool As Boolean = True
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Select Case bool
            Case True
                Document2D.ksOpenView(1)
                bool = False
            Case Else
                Document2D.ksOpenView(0)
                bool = True
        End Select
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Document2D.ksMtr(20, 20, 45, 1, 1)
        'Document2D.ksCircle(20, 50, 50, 1)
    End Sub


    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        '============================================
        Dim Mathematic2D As ksMathematic2D
        Dim Ap As ksMathPointParam
        Ap = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam) 'StructType2DEnum.ko_MathPointParam)
        Mathematic2D = kompas.GetMathematic2D()
        Mathematic2D.ksIntersectCirLin(0, 0, 30, 0, 0, 130, Ap) '    k, x, y)
        '============================================

        Document2D.ksMtr(145, 270, 0, 1, 1)

        Step1()
        'mp_B = kompas.cordinatePoint(Document2D, mp_A, ml_AB, angleDegrees)
        'Document2D.myDrawPoint(mp_B)

        'Document2D.myDrawLineSeg(mp_A, mp_B)



        Document2D.ksDeleteMtr()
    End Sub

    Sub Step1()
        'Document2D.myCrossingLines(mp_A)
        'Document2D.myDrawPoint(mp_A)

        'ksCrossingLines(Document2D, mp_D)
        'Document2D.myDrawPoint(mp_D)
    End Sub

    Sub test1()

    End Sub

    Private Sub frm_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadForm()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Document2D.ksMtr(145, 270, 0, 1, 1)

        'Document2D.myDrawCircle(mp_B, 80)
        'Document2D.myDrawCircle(mp_D, 60)

        '============================================


        Document2D.ksDeleteMtr()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Document2D.ksMtr(145, 270, 0, 1, 1)
        Dim Mathematic2D As ksMathematic2D
        Mathematic2D = kompas.GetMathematic2D()

        Dim tan As ksTAN

        tan = kompas.GetParamStruct(StructType2DEnum.ko_TAN)


        Mathematic2D.ksTanCircleCircle(100, 100, 10, 150, 150, 20, tan)

        Document2D.ksLineSeg(tan.x1(2), tan.y1(2), tan.x2(2), tan.y2(2), 1)
        'tan.x1(2), tan.y1(2),  



        Document2D.ksDeleteMtr()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Document2D.ksMtr(145, 270, 0, 1, 1)

        Dim Mathematic2D As ksMathematic2D
        Mathematic2D = kompas.GetMathematic2D()

        'Dim pArray As ksMathPointParam
        'pArray = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam)

        Dim pArray As ksDynamicArray
        pArray = kompas.GetDynamicArray(POINT_ARR)

        Dim par As ksMathPointParam = kompas.GetParamStruct(StructType2DEnum.ko_MathPointParam)

        Dim circle1 As ksCircleParam
        circle1 = kompas.GetParamStruct(StructType2DEnum.ko_CircleParam)
        circle1.xc = 0 : circle1.yc = 0 : circle1.rad = 70
        Dim c1 As Integer = Document2D.ksCircle(circle1.xc, circle1.yc, circle1.rad, 1)

        Dim circle2 As ksCircleParam
        circle2 = kompas.GetParamStruct(StructType2DEnum.ko_CircleParam)
        circle2.xc = 50 : circle2.yc = 50 : circle2.rad = 50
        Dim c2 As Integer = Document2D.ksCircle(circle2.xc, circle2.yc, circle2.rad, 1)

        Mathematic2D.ksIntersectCurvCurv(c1, c2, pArray)



        pArray.ksGetArrayItem(0, par)
        Document2D.ksPoint(par.x, par.y, 0)

        pArray.ksGetArrayType()

        pArray.ksGetArrayItem(1, par)
        Document2D.ksPoint(par.x, par.y, 0)

        Document2D.ksDeleteMtr()
    End Sub
End Class