Imports Kompas6API5, Kompas6Constants

Public Class cls_Cam_K
    Inherits cls_InputDataCam

    Public Sub New(ActiveDoc2D As ksDocument2D)
        MyBase.New(ActiveDoc2D)
        doc2D = New myDoc2D(ActiveDoc2D)
        Initialization()
    End Sub

    Sub Initialization()


        S = 30
        leftDistance = 40 + S

        upDistance = 370
        downDistance = 35

        AngleDistance = 45
        AngleDistanccePlacing = 30
        AngleRotation = 75

        h = 60

        angleLamda = 60

        mp_O = New PointDouble("O", leftDistance, 320)

        step_Diagram = 10


        'Определяем масштаб механизма для скоростей
        'mashtab_mv = vb / vl_pb

        ' Масштаб для конкретного вида на чертеже
        'mashtab_mv_view = vl_pb / vb

    End Sub

    Public Sub Diagram1_Construction()

        'Dim doc2D As New myDoc2D(doc2D)

        table1 = diagramTableExcel("AngleDistance_K")

        Dim dd As Double = ck(1)
        Dim angleRadian_Distance As Double = (AngleDistance * Math.PI / 180)

        Dim angleRadian_Rotation As Double = (AngleRotation * Math.PI / 180)

        'For i As Integer = 1 To table1.Rows.Count
        '    S_a(i) = ((ck(i) * (S / 1000)) / angleRadian)
        'Next

        Dim angleRadian As Double

        If angleRadian_Distance > angleRadian_Rotation Then
            angleRadian = angleRadian_Rotation
        Else
            angleRadian = angleRadian_Distance
        End If

        m = h / ((ck(1) * (S / 1000)) / (angleRadian ^ 2))


        'mashtab_diagram1_view = table1

        diagram1_SheetView = New cls_SheetView("Diagram1 ", mp_O.X, mp_O.Y)

        doc2D.AddView(diagram1_SheetView)

        diagram1_SheetView.Create()
        diagram1_SheetView.Active()

        Dim line_CordinateLine As New PointDouble(0, 0)

        'doc2D.myDrawPoint(line_CordinateLine)
        'doc2D.myDrawLine(New PointDouble(0, 0), 0)
        'doc2D.myDrawLine(New PointDouble(0, 0), 90)

        Dim AngleSum As Double = AngleDistance + AngleDistanccePlacing + AngleRotation
        doc2D.myDrawLineSeg(New PointDouble(0, 0), AngleSum, 0)



        Dim p_AngleDistance(11) As Double
        'For i As Integer = 0 To AngleDistance Step 10
        'doc2D.myDrawLine(New PointDouble(i, 0), 90)
        'Next

        Dim p_AngleRotation(10) As Double

        Dim step1 As Double = AngleDistance / 10

        Dim k_ As Integer = 0

        Dim tmpSum1 As Double = 0

        For i As Integer = 0 To AngleDistance Step step1

            p_AngleDistance(k_) = tmpSum1
            tmpSum1 += step1

            doc2D.myDrawLineSeg(New PointDouble(p_AngleDistance(k_), h + 10), New PointDouble(p_AngleDistance(k_), -250), 2)
            k_ += 1
            'doc2D.myDrawLine(New PointDouble(i, 0), 90)
            'doc2D.myDrawLineSeg(New PointDouble(i / 2, (h + 15) / 2), upDistance - downDistance, 270, 2)
        Next
        'p_AngleDistance(k_) = AngleDistance

        Dim step2 As Double = AngleRotation / 10

        k_ = 0

        Dim tmpSum2 As Double
        tmpSum2 = AngleDistance + AngleDistanccePlacing
        For i As Integer = 0 To AngleRotation + step2 Step step2

            p_AngleRotation(k_) = tmpSum2
            tmpSum2 += step2

            doc2D.myDrawLineSeg(New PointDouble(p_AngleRotation(k_), h + 10), New PointDouble(p_AngleRotation(k_), -250), 2)
            k_ += 1

            'doc2D.myDrawLine(New PointDouble(i, 0), 90)

            'doc2D.myDrawLineSeg(New PointDouble(p_AngleRotation(k_), (h + 15) / 2), New PointDouble(p_AngleRotation(k_), -200), 2)
            'tmpSum -= step2
        Next
        'p_AngleRotation(k_) = AngleSum

        Dim pd_s1(table1.Rows.Count - 1) As PointDouble
        Dim pd_s2(table1.Rows.Count - 1) As PointDouble

        For i As Integer = 0 To pd_s1.Length - 1
            Dim y1 As Double = ((ck(i + 1) * (S / 1000)) / angleRadian_Distance ^ 2) * m
            Dim tmp_ As New PointDouble(p_AngleDistance(i), y1)
            Si_1(i) = y1
            pd_s1(i) = tmp_
        Next

        doc2D.myDrawPolylineCam(pd_s1)

        'Si_1(1) = ((ck(1) * (S / 1000)) / angleRadian_Distance ^ 2) * m
        'Si_1(11) = ((ck(11) * (S / 1000)) / angleRadian_Distance ^ 2) * m


        'Dim p_Distance1 As New PointDouble(p_AngleDistance(0), Si_1(1))
        'Dim p_Distance2 As New PointDouble(p_AngleDistance(10), Si_1(11))

        'doc2D.myDrawPoint(p_Distance1)
        'doc2D.myDrawPoint(p_Distance2)

        'doc2D.myDrawLineSeg(p_Distance1, p_Distance2)

        'Si_2(1) = ((ck(1) * (S / 1000)) / angleRadian_Rotation ^ 2) * m
        'Si_2(11) = ((ck(11) * (S / 1000)) / angleRadian_Rotation ^ 2) * m

        'Dim p_Rotation1 As New PointDouble(p_AngleRotation(10), Si_2(1))
        'Dim p_Rotation2 As New PointDouble(p_AngleRotation(0), Si_2(11))

        'doc2D.myDrawPoint(p_Rotation1)
        'doc2D.myDrawPoint(p_Rotation2)

        'doc2D.myDrawLineSeg(p_Rotation1, p_Rotation2)

        For i As Integer = 0 To pd_s2.Length - 1
            Dim y2 As Double = ((ck2(i + 1) * (S / 1000)) / angleRadian_Rotation ^ 2) * m
            Dim tmp_ As New PointDouble(p_AngleRotation(i), y2)
            Si_2(i) = y2
            pd_s2(i) = tmp_
        Next

        doc2D.myDrawPolylineCam(pd_s2)


        Dim s1_max As Double = Math.Abs(Si_1.Max())
        Dim s1_min As Double = Math.Abs(Si_1.Min())

        Dim s2_max As Double = Math.Abs(Si_2.Max())
        Dim s2_min As Double = Math.Abs(Si_2.Min())

        Smax = New Double() {s1_max, s1_min, s2_max, s2_min}.Max


        '----------------------------------------------
        '----------Определение второй диаграммы------------
        Dim st As Double

        diagram2_SheetView = New cls_SheetView("Diagram2 ", leftDistance, 200)


        doc2D.AddView(diagram2_SheetView)

        diagram2_SheetView.Create()
        diagram2_SheetView.Active()

        Dim p_Diagram2 As New PointDouble(0, 0)

        'doc2D.myDrawLine(p_Diagram2, 0)
        doc2D.myDrawLineSeg(New PointDouble(0, 0), AngleSum, 0)


        For i As Integer = 1 To table1.Rows.Count
            S_bk1(i) = ((bk(i) * (S / 1000)) / angleRadian_Distance) * m
        Next


        st = 0
        'Dim step1 As Double = AngleDistance / 10


        Dim pd1(table1.Rows.Count) As PointDouble

        For i As Integer = 1 To table1.Rows.Count

            pd1(i) = New PointDouble(st, S_bk1(i))
            st += step1
            'doc2D.myDrawPoint(pd(i))
        Next

        doc2D.myDrawPolyline(pd1)

        For i As Integer = 1 To table1.Rows.Count
            S_bk2(i) = ((bk(i) * (S / 1000)) / angleRadian_Rotation) * m
        Next

        'Dim step2 As Double = AngleRotation / 10
        st = AngleSum - AngleRotation

        Dim pd2(table1.Rows.Count) As PointDouble

        For i As Integer = 1 To table1.Rows.Count

            pd2(i) = New PointDouble(st, S_bk2(i) * (-1))
            st += step2
            'doc2D.myDrawPoint(pd(i))
        Next

        doc2D.myDrawPolyline(pd2)

        '------------------------------------------------
        '----------Определение третей диаграммы----------

        diagram3_SheetView = New cls_SheetView("Diagram3 ", leftDistance, 120)


        doc2D.AddView(diagram3_SheetView)

        diagram3_SheetView.Create()
        diagram3_SheetView.Active()

        Dim p_Diagram3 As New PointDouble(0, 0)

        'doc2D.myDrawLine(p_Diagram3, 0)
        doc2D.myDrawLineSeg(New PointDouble(0, 0), AngleSum, 0)


        For i As Integer = 1 To table1.Rows.Count
            S_ak1(i) = ((ak1(i) * (S / 1000))) * m
        Next


        st = 0
        Dim step3 As Double = AngleDistance / 10



        Dim pd1_3(table1.Rows.Count) As PointDouble
        ReDim Si1_len(table1.Rows.Count)

        Dim p1_Line As New PointDouble

        For i As Integer = 1 To table1.Rows.Count

            If i = table1.Rows.Count Then
                p1_Line.X = st
                p1_Line.Y = S_ak1(i)
            End If

            pd1_3(i) = New PointDouble(p_AngleDistance(i - 1), S_ak1(i))
            Si1_len(i) = doc2D.myCalculateLenghtProjectionY(mp_O0, pd1_3(i), 0)

            st += step3
            'doc2D.myDrawPoint(pd(i))ee
        Next

        doc2D.myDrawPolyline(pd1_3)

        '------------------------------------------
        ReDim Si2_len(table1.Rows.Count)

        For i As Integer = 1 To table1.Rows.Count
            S_ak2(i) = ((ak2(i) * (S / 1000))) * m
        Next

        Dim step4 As Double = AngleRotation / 10
        st = AngleSum - AngleRotation

        Dim pd2_3(table1.Rows.Count) As PointDouble

        Dim p2_Line As New PointDouble

        For i As Integer = 1 To table1.Rows.Count

            If i = 1 Then
                p2_Line.X = st
                p2_Line.Y = S_ak2(i)
            End If

            pd2_3(i) = New PointDouble(p_AngleRotation(i - 1), S_ak2(i))
            Si2_len(i) = doc2D.myCalculateLenghtProjectionY(mp_O0, pd2_3(i), 0)
            st += step4
            'doc2D.myDrawPoint(pd(i))
        Next

        doc2D.myDrawPolyline(pd2_3)



        doc2D.myDrawLineSeg(p1_Line, p2_Line)

        '------------------------------------------------
        '---------- Построение кулачка ----------


        Dim pd1_4(table1.Rows.Count) As PointDouble
        Dim pd2_4(table1.Rows.Count) As PointDouble

        cam_SheetView = New cls_SheetView("Cam ", 415, 225)


        doc2D.AddView(cam_SheetView)

        cam_SheetView.Create()
        cam_SheetView.Active()

        Dim p_Cam As New PointDouble(0, 0)

        Dim lamda As Double = 90 - angleLamda
        r0 = ((Smax / 1000) / Math.Tan(lamda * Math.PI / 180)) - ((S / 1000) / 2)

        Dim r0_mm = r0 * 1000

        doc2D.myDrawCircle(mp_O0, r0_mm)

        doc2D.myDrawLine(mp_O0, 0)
        doc2D.myDrawLine(mp_O0, 90)

        ReDim pointsCam(AngleSum / step_Diagram)

        'For i As Integer = 0 

        ' 90 - 80 - 20 - 70 

        Dim angle_CAM As Double = 90


        Dim k As Integer = 0

        Dim tmp1 As Double = AngleDistance
        For i As Integer = 0 To AngleDistance Step step1

            'tmp1 -= step1


            'If angle_CAM < 0 Then
            'angle_CAM += 360 - step1
            'Else

            'End If

            pd1_4(k) = doc2D.myCalculateCordinatePoint(mp_O0, r0_mm + Si1_len(k), angle_CAM)



            doc2D.myDrawPoint(pd1_4(k), 5)
            'doc2D.myDrawLine(mp_O0, angle_CAM)
            angle_CAM -= step1
            k += 1
            'pointsCam(k) = New 

        Next

        k = 0
        angle_CAM = -AngleDistanccePlacing + step1
        Dim tmp2 As Double = AngleDistance + AngleDistanccePlacing
        For i As Integer = 0 To AngleRotation + step2 Step step2

            'tmp1 -= step1
            pd2_4(k) = doc2D.myCalculateCordinatePoint(mp_O0, r0_mm + Si2_len(k + 1), angle_CAM)

            doc2D.myDrawPoint(pd2_4(k), 5)
            'doc2D.myDrawLine(mp_O0, angle_CAM)
            angle_CAM -= step2

            k += 1
            'pointsCam(k) = New 

        Next

        doc2D.myDrawPolylineCam(pd1_4)
        doc2D.myDrawLineSeg(pd1_4(11), pd2_4(0))
        doc2D.myDrawPolylineCam(pd2_4)

        'doc2D.myDrawLine(mp_O0, 80)

        Dim point As PointDouble = pointsCam(0)

        cam_SheetView.ExitView()

    End Sub



End Class
