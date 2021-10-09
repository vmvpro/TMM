Imports DB



Public Class frm_Test7

    Private Sub frm_Test7_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'mdl_CreateDataContext.CreateDiagramEntity()

        'Dim db As New Main( 

        'Dim db = New Main(DataBase.Connection())

        't.DbContextTest()

        't.DbContextInsert()

        't.DbContextUpdate()

        't.DbContextInsert()
        't.DbContextDelete()

        t.StZd()



        Console.ReadLine()



    End Sub
End Class

Public Class t

    Public Shared Sub TestInsert()

    End Sub

    Public Shared Sub StZd()
        Dim dataBase As DataBase = New DataBase()
        Dim st As DataTable = dataBase.ReturnDataTable(TableName.zd_student)
        Dim zd As DataTable = dataBase.ReturnDataTable(TableName.zadania)
        Dim ss = st.AsEnumerable()
        Dim zz = zd.AsEnumerable()
        Dim linqStZd = From s In ss
                      From z In zz
                      Where s.Field(Of Long)("id_zd") = z.Field(Of Long)("id")
                      Select New With
                        {
                            Key .Id = s.Field(Of Long)("id"),
                            Key .Name = s.Field(Of String)("st_name"),
                            Key .ZdName = z.Field(Of String)("name")
                        }

        For Each row In linqStZd
            Debug.WriteLine(String.Format("{0} \t {1} \t {2}", row.Id, row.Name, row.ZdName))
        Next

    End Sub

    'Public Shared Sub DbContextShowData()
    '    Using db = New Main(DataBase.Connection())
    '        Dim st = db.ZDStudent
    '        Dim zd = db.ZAdaNiA
    '        'Dim pos = db.DiCPosition
    '        For Each z In zd
    '            Console.WriteLine(z.Name)
    '            For Each s In 
    '                Dim qqq = From p In db.ZDStudent Group p By __groupByKey1__ = s. Into g() Select New With {Key .NumProducts = g.Count()}
    '            Next
    '        Next

    '        'Dim emp = db.TBlEmployees
    '        'Dim dep = db.DiCDepartAmenTS
    '        'For Each d In dep
    '        '    Console.WriteLine(d.Name)
    '        '    For Each e In d.TBlEmployees
    '        '        Dim qqq = From p In db.TBlEmployees Group p By __groupByKey1__ = e.IDDepartment Into g() Select New With {Key .NumProducts = g.Count()}
    '        '    Next
    '        'Next
    '    End Using
    'End Sub

    Public Shared Sub DbContextTest()
        Dim main As Main = New Main(DataBase.Connection())
        Dim st = main.ZDStudent.ToArray
        Dim linq_st = (From s In st Where s.ID = 13 Select s).Single()
        'For Each row In linq_emp
        Debug.WriteLine(String.Format("{0}" & vbTab, linq_st.STName))
        'Next
    End Sub

    Public Shared Sub DbContextInsert()
        Using db = New Main(DataBase.Connection())
            'Dim st = db.ZDStudent
            Dim newSt = New ZDStudent With {.STName = "vvvvv", .ID = 27}
            db.ZDStudent.InsertOnSubmit(newSt)
            db.SubmitChanges()
        End Using
    End Sub

    Public Shared Sub DbContextUpdate()
        Using db = New Main(DataBase.Connection())
            Dim st As ZDStudent = db.ZDStudent.First(Function(c) c.ID = 22)
            st.ALPiB = 70
            db.SubmitChanges()
        End Using
    End Sub

    Public Shared Sub DbContextDelete()
        Using db = New Main(DataBase.Connection())
            Dim st As ZDStudent = db.ZDStudent.First(Function(c) c.ID = 27)
            db.ZDStudent.DeleteOnSubmit(st)
            db.SubmitChanges()
        End Using
    End Sub


End Class