Imports System.Runtime.CompilerServices
Imports Kompas6API5, Kompas6Constants

Partial Public Class myDoc2D
    Dim kompas As Kompas6API5.Application
    Dim doc2D As ksDocument2D
    Dim ViewParam As ksViewParam

    Public Sub New(doc2D As ksDocument2D)
        kompas = kompasApp()
        Me.doc2D = doc2D
        listSheetView = New List(Of cls_SheetView)()
        dictionarySheetView = New Dictionary(Of String, cls_SheetView)()
    End Sub

    Public Sub New(doc2D As ksDocument2D, chertegName As String)
        kompas = kompasApp()
        Me.doc2D = doc2D
        listSheetView = New List(Of cls_SheetView)()
        dictionarySheetView = New Dictionary(Of String, cls_SheetView)()

        loadForm(Me.doc2D, chertegName)
    End Sub



    Private Function kompasApp() As Kompas6API5.Application

        Try
            kompas = GetObject(, "KOMPAS.Application.5")
        Catch ex As Exception
            kompas = CreateObject("KOMPAS.Application.5") ' Подключаемся к КОМПАСу
        End Try

        Return kompas

    End Function

    Public ReadOnly Property RefDoc As ksDocument2D
        Get
            Return doc2D
        End Get
    End Property

    Public ReadOnly Property RefKompas As Kompas6API5.Application
        Get
            Return kompas
        End Get
    End Property





End Class
