Imports DB

Public Class frm_DataBaseTest

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim db As New DataBase

        Dim counter As Long = 21
        Dim v As Long = 3

        Dim dt As DataTable

        Dim dt_Mehanizm = db.ReturnDataTable(TableName.z3_mehanizm)

        dt = StoredProcedure.TableZD(dt_Mehanizm, 21)

        Dim dr_Mehanizm = DataBase.Row(dt, "numberPosition", v)
        Debug.Print(dr_Mehanizm("id"))

    End Sub
End Class