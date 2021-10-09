Imports Kompas6API5, Kompas6Constants
Public Class frm_Shatun_9

    Dim kompas As KompasObject
    Dim ActiveDoc2D As ksDocument2D
    Dim DocumParam As ksDocumentParam
    Dim StandartSheet As ksStandartSheet
    Dim SheetPar As ksSheetPar
    Dim str As String

    Dim shatun_9 As cls_Shatun_9

    Dim cam_0000 As cls_Cam_0000

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            Dim Doc2D As New myDoc2D(ActiveDoc2D)

            shatun_9.Run()



        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
       
    End Sub

    Private Sub frm_Shatun_9_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kompas = kompasApp()
        loadForm(ActiveDoc2D, "ШаблонЧертежа")

        shatun_9 = New cls_Shatun_9(ActiveDoc2D)

        'povzun_6 = New cls_Povsun_6(ActiveDoc2D)
    End Sub
End Class