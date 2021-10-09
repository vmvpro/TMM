Imports System.IO

Public Module mdl_CreateDataContext

    Sub Test2()

        Dim di As New DirectoryInfo(Environment.CurrentDirectory)
        Dim contextName As String = "DbContext"

        '-------------------------------------------------------
        Dim fileNameDBML = Path.Combine(di.FullName, contextName, ".dbml")
        Dim fiDBML As New FileInfo(fileNameDBML)

        If (fiDBML.Exists) Then
            Debug.WriteLine("Ok")
        End If

        '--------------------------------------------------------
        Dim fileNameCS = Path.Combine(di.FullName, contextName, ".cs")
        Dim fiCS As New FileInfo(fileNameCS)

        If (fiCS.Exists) Then
            Debug.WriteLine("Ok")
        End If


        'Dim path = di.Parent.Parent.Parent.FullName '& "\DB\TMM.db"



        '	fiCopy.Delete();

    End Sub

    Sub CreateDiagramEntity()

        'Test2()
        Dim contextName As String = "DbContext"

        Dim startInfo_ As New ProcessStartInfo

        Dim pathDbMetal = "c:\Tools\DbMetal.exe"

        Dim di As New DirectoryInfo(Environment.CurrentDirectory)
        'Dim path = di.Parent.Parent.Parent.FullName & "\DataBase\TMM.db"
        'C#
        Dim path = "D:\Doc\Work\MS Visual Studio\ItTestWork\DataBase\ItTestWork.db"

        Dim argument1 = " /provider:Sqlite /conn " & Chr(34) & "Data Source = " & path & Chr(34) & " /dbml:" & contextName & ".dbml"
        Dim argument2 = " /code:" & contextName & ".cs " & contextName & ".dbml"

        'startInfo = New ProcessStartInfo'; //New Proccess
        startInfo_.FileName = pathDbMetal

        startInfo_.Arguments = String.Format("{0}", argument1)
        Process.Start(startInfo_)

        startInfo_.Arguments = String.Format("{0}", argument2)
        Process.Start(startInfo_)
    End Sub

    Sub test()
        '    string localMachine = @"C:\TempDataBase";
        Dim localMachine = "C:\TempDataBase"

        'DirectoryInfo di = new DirectoryInfo(localMachine);
        Dim di As New DirectoryInfo(localMachine)

        '    If (!di.Exists) Then
        '	Directory.CreateDirectory(localMachine);

        If (Not di.Exists) Then
            Directory.CreateDirectory(localMachine)
        End If

        'string fileName = @"z:\023 Цех23\ТБ\_ОБЩАЯ ПАПКА\База данных ТБ Ц23\BD\ClientDB.accdb";
        Dim fileName = "z:\023 Цех23\ТБ\_ОБЩАЯ ПАПКА\База данных ТБ Ц23\BD\ClientDB.accdb"

        'FileInfo fi = new FileInfo(fileName);

        'FileInfo fiCopy = new FileInfo(localMachine + "\\" + fi.Name);

        '//File.Copy(Environment.CurrentDirectory + "\\Resources\\MSACCESS.EXE", localMachine + "\\MSACCESS.EXE", true);
        '//File.Copy(Environment.CurrentDirectory + "\\Resources\\База данных (Сервер).lnk", localMachine + "\\База данных (Сервер).lnk", true);

        'If (fiCopy.Exists) Then

        'End If
        '	fiCopy.Delete();

        'File.Copy(fi.FullName, fiCopy.FullName, true);

        'Process.Start(fiCopy.FullName);

        'this.Close();
    End Sub

End Module
