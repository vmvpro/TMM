Imports Kompas6API5, Kompas6Constants

Public Module Module1
    Dim kompas As Object
    Dim Document2D As ksDocument2D
    Dim DocumParam As ksDocumentParam
    Dim StandartSheet As ksStandartSheet
    Dim SheetPar As ksSheetPar
    Dim str As String

    Public Sub Create()

        If kompas Is Nothing Then
            Try
                'kompas = CreateObject("KOMPAS.Application.5") ' Подключаемся к КОМПАСу
                kompas = GetObject(, "KOMPAS.Application.5")


            Catch ex As Exception
                kompas = CreateObject("KOMPAS.Application.5") ' Подключаемся к КОМПАСу
            Finally
                DocumParam = kompas.GetParamStruct(StructType2DEnum.ko_DocumentParam)
                DocumParam.type = 1                     ' стандартный чертеж
                DocumParam.regime = 1                   ' видимый режим редактирования

                ' Формируем путь к библиотеке оформления
                str = kompas.ksSystemPath(0) & "\graphic.lyt"

                ' Получаем интерфейс параметров оформления
                'SheetPar.Init()
                SheetPar = DocumParam.GetLayoutParam()
                SheetPar.layoutName = str
                SheetPar.shtType = 1

                ' Получаем интерфейс параметров стандартного листа
                StandartSheet = SheetPar.GetSheetParam()
                StandartSheet.direct = True                 ' штамп вдоль длинной стороны
                StandartSheet.format = 3                    ' формат А3
                StandartSheet.multiply = 1                  ' масштаб

                ' Получаем интерфейс документа
                Document2D = kompas.Document2D



                ' Создаем документ

            End Try
        End If

        'Document2D.ksCreateDocument(DocumParam)
        Document2D.ksOpenDocument(Environment.CurrentDirectory & "\ШаблонЧертежа.cdw", False)
        'Document2D.ksSaveDocumentEx()

        kompas.Visible = True           ' делаем документ видимым

        ' запустить График и создать соответствующий ему объект
        'kompas = CreateObject("KOMPAS.Application.5") ' Подключаемся к КОМПАСу

        'kompas = GetObject(, "KOMPAS.Application.5")

        'If Not kompas Is Nothing Then
        '    kompas.Visible = True
        '    kompas.ActivateControllerAPI()
        'End If
        'Document2D.ksOpenDocument("d:\Doc\Загальне\Компас\ШаблонЧертежа.cdw", False)
        'Получаем интерфейс параметров документа

    End Sub

End Module

