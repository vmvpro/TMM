Imports Kompas6API5, Kompas6Constants
Imports DB
Imports System.Data.SQLite
Imports System.Text.RegularExpressions

Public Class frm_Shatun_1

    Dim kompas As KompasObject
    Dim ActiveDoc2D As ksDocument2D
    Dim DocumParam As ksDocumentParam
    Dim StandartSheet As ksStandartSheet
    Dim SheetPar As ksSheetPar
    Dim str As String

    Dim cam_0000 As cls_Cam_0000
    Dim cam_K As cls_Cam_K

    Dim dt_student As DataTable
    Dim da_student As SQLiteDataAdapter

    Dim dtData As DataTable
    Dim daData As SQLiteDataAdapter

    Dim dt_z1 As DataTable
    Dim da_z1 As SQLiteDataAdapter

    Dim dt_lstn As DataTable
    Dim da_lstn As SQLiteDataAdapter

    Dim counter As Integer

    Dim drZD_Student As DataRow

    Dim row_student As DataRow

    Dim shatun_1 As cls_Shatun_1

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DataBase.LoadDataAdapterAndDataTable(TableName.z1, da_z1, dt_z1)
        DataBase.LoadDataAdapterAndDataTable(TableName.lstn, da_lstn, dt_lstn)
        DataBase.LoadDataAdapterAndDataTable(TableName.zd_student, da_student, dt_student)

        'Dim tran As SQLiteTransaction = DataBase.Connection().BeginTransaction()

        Dim name = txt_st_name.Text
        Dim year = Convert.ToInt64(txt_year.Text)
        Dim drDodatok2 As DataRow = StoredProcedure.TableDodatok2(name, year)
        Dim id_dodatok As Integer = drDodatok2("id")

        DataBase.BeginTransaction()


        Dim id_zd = Convert.ToInt64(cbo_id_zd.Text)

        '------------------------------------------------------
        Dim dr_lstn As DataRow = DataBase.Row(id_zd, Table.LSTN)
        Dim counter = dr_lstn("counter") + 1
        dr_lstn("counter") = counter

        Dim tableNameZD As String = dr_lstn("tableNameZD")

        da_lstn.Update(Table.LSTN)

        '------------------------------------------------------

        Dim dr_z1 As DataRow = dt_z1.NewRow
        dr_z1("id") = counter
        dr_z1("variant") = Convert.ToInt64(cbo_variant.Text)
        dt_z1.Rows.Add(dr_z1)
        da_z1.Update(dt_z1)

        '------------------------------------------------------
        Dim v As Integer = Convert.ToInt64(cbo_variant.Text)

        row_student = dt_student.NewRow()
        row_student("st_name") = txt_st_name.Text
        row_student("id_zd") = id_zd

        row_student("variant") = v
        row_student("year") = Convert.ToInt64(txt_year.Text)
        row_student("lstn") = counter
        row_student("id_dodatok2") = id_dodatok

        If txt_ml_AB.Text = "" Then txt_ml_AB.Text = 30
        row_student("ml_AB") = Convert.ToDecimal(txt_ml_AB.Text)

        If txt_vl_pb.Text = "" Then txt_vl_pb.Text = 60
        row_student("vl_pb") = Convert.ToDecimal(txt_vl_pb.Text)

        If txt_al_pib.Text = "" Then txt_al_pib.Text = 90
        row_student("al_pib") = Convert.ToDecimal(txt_al_pib.Text)

        drZD_Student = row_student

        dt_student.Rows.Add(drZD_Student)
        da_student.Update(dt_student)

        '------------------------------------------------------

        DataBase.TransactionCommit()

        '=================================================

        DataBase.LoadDataAdapterAndDataTable(tableNameZD, daData, dtData)
        Dim drData = DataBase.Row(v, tableNameZD)

        '--- Основной механизм ---
        txt_L_ab.Text = drData("L_ab")
        txt_L_ad.Text = drData("L_ad")
        txt_L_cd.Text = drData("L_cd")

        Dim L_bc As Decimal = drData("L_bc")
        txt_L_bc.Text = L_bc.ToString()

        Dim L_be As Decimal = drData("L_be")
        txt_L_be.Text = L_be.ToString()

        If (drData("m2").ToString() = "") Then
            Dim m2 As Decimal = 10 * Convert.ToDecimal(drData("L_ab"))
            drData("m2") = m2
            daData.Update(dtData)
        End If

        If (drData("m3").ToString() = "") Then
            Dim L_ce As Decimal = L_bc + L_be
            Dim m3 As Decimal = 12 * L_bc
            drData("m3") = m3
            daData.Update(dtData)
        End If

        If (drData("m4").ToString() = "") Then
            Dim m4 As Decimal = 15 * Convert.ToDecimal(drData("L_cd"))
            drData("m4") = m4
            daData.Update(dtData)
        End If

        txt_m2.Text = drData("m2").ToString()
        txt_m3.Text = drData("m3").ToString()
        txt_m4.Text = drData("m4").ToString()

        txt_M_M4.Text = drData("M_M4")

        txt_fi_v.Text = drData("fi_v")
        txt_fi_dv.Text = drData("fi_dv")
        txt_fi_pov.Text = drData("fi_pov")

        '--- Додаток 2 ---
        txt_w2.Text = drDodatok2("w2")
        txt_delta.Text = drDodatok2("delta")

        txt_dif.Text = drDodatok2("dif")
        txt_h.Text = drDodatok2("h")

        txt_zi.Text = drDodatok2("zi")
        txt_zm.Text = drDodatok2("zm")

        txt_zm.Text = drDodatok2("zm")

        Dim vf As String = drDodatok2("vf")
        Dim m_vf() As String = vf.Split(";")

        lst_vf.Items.Clear()
        For i = 0 To m_vf.Count - 1
            lst_vf.Items.Add(m_vf(i))
        Next

        txt_position_m.Text = drDodatok2("position_m")

        row_student("id_dodatok2") = Convert.ToInt64(drDodatok2("id"))

        DataBase.UpdateDataBase()

        'DataBase.TransactionCommit()

        Dim value = ComboBox1.SelectedValue
        loadStudents()
        ComboBox1.SelectedValue = value
    End Sub

    Sub loadStudents()
        ComboBox1.DataSource = Table.ZD_Student
        ComboBox1.DisplayMember = "st_name"
        ComboBox1.ValueMember = "id"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            'Dim Doc2D As New myDoc2D(ActiveDoc2D)

            shatun_1 = New cls_Shatun_1(drZD_Student)

            'MyBase.New(ActiveDoc2D)
            'doc2D = New myDoc2D(ActiveDoc2D)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        shatun_1 = New cls_Shatun_1(drZD_Student)
        Dim doc2D = New myDoc2D(ActiveDoc2D, "ШаблонЧертежа")

        shatun_1.Run(Convert.ToInt32(txt_nmberPosition.Text), doc2D)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        shatun_1.RunKompas()
    End Sub

    Private Sub frm_Shatun_1_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        'mdl_CreateDataContext.CreateDiagramEntity()

        Dim db_ As New DataBase

        cbo_id_zd.SelectedIndex = 1
        cbo_variant.SelectedIndex = 7

        txt_st_name.Text = "Винийчук А.А."

        loadStudents()
    End Sub
    Dim lstn As Integer
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            Dim selectedValue = ComboBox1.SelectedValue
            drZD_Student = DataBase.Row(Table.ZD_Student, "id", selectedValue)

            Dim id_zd = drZD_Student("id_zd")
            Dim v = drZD_Student("variant")
            lstn = drZD_Student("lstn")
            Dim id_dodatok2 = drZD_Student("id_dodatok2")

            Dim drLSTN = DataBase.Row(Table.LSTN, "id", id_zd)
            Dim tableNameZD = drLSTN("tableNameZD")
            Dim dtDataZd = DataBase.DictionaryDataTables(tableNameZD)

            Dim drDataZd = DataBase.Row(dtDataZd, v)

            Dim drDodatok2 = DataBase.Row(Table.Dodatok2, "id", id_dodatok2)

            txt_st_name.Text = drZD_Student("st_name")
            cbo_id_zd.SelectedIndex = drZD_Student("id_zd")
            cbo_variant.SelectedIndex = drZD_Student("variant")
            txt_year.Text = drZD_Student("year")

            txt_ml_AB.Text = drZD_Student("ml_AB")
            txt_vl_pb.Text = drZD_Student("vl_pb")
            txt_al_pib.Text = drZD_Student("al_pib")

            txt_L_ab.Text = drDataZd("L_ab")
            txt_L_ad.Text = drDataZd("L_ad")
            txt_L_cd.Text = drDataZd("L_cd")
            txt_L_bc.Text = drDataZd("L_bc")
            txt_L_be.Text = drDataZd("L_be")

            txt_m2.Text = drDataZd("m2")
            txt_m3.Text = drDataZd("m3")
            txt_m4.Text = drDataZd("m4")

            txt_M_M4.Text = drDataZd("M_M4")

            txt_fi_v.Text = drDataZd("fi_v")
            txt_fi_dv.Text = drDataZd("fi_dv")
            txt_fi_pov.Text = drDataZd("fi_pov")

            txt_w2.Text = drDodatok2("w2")
            txt_delta.Text = drDodatok2("delta")

            txt_dif.Text = drDodatok2("dif")
            txt_h.Text = drDodatok2("h")

            txt_zi.Text = drDodatok2("zi")
            txt_zm.Text = drDodatok2("zm")

            txt_zm.Text = drDodatok2("zm")

            Dim vf As String = drDodatok2("vf")
            Dim m_vf() As String = vf.Split(";")
            lst_vf.Items.Clear()
            For i = 0 To m_vf.Count - 1
                lst_vf.Items.Add(m_vf(i))
            Next

            txt_position_m.Text = drDodatok2("position_m")



            'MsgBox(ComboBox1.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click, btn_CreateDataBase.Click

        shatun_1 = New cls_Shatun_1(drZD_Student)
        shatun_1.CreateDataBase()

        
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

    End Sub

    Private Sub txt_nmberPosition_TextChanged(sender As Object, e As EventArgs) Handles txt_nmberPosition.TextChanged

    End Sub
End Class