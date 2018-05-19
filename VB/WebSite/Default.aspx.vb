Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Web
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Private dateTemplate As DataTemplate = Nothing
    Private criteriaValues As New Dictionary(Of Object, String)()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

    End Sub
    Protected Sub grid_AutoFilterCellEditorInitialize(ByVal sender As Object, ByVal e As ASPxGridViewEditorEventArgs)
        Dim grid As ASPxGridView = TryCast(sender, ASPxGridView)
        If e.Column.FieldName = "CategoryName" OrElse e.Column.FieldName = "CategoryID" Then
            Dim dde As ASPxDropDownEdit = TryCast(e.Editor, ASPxDropDownEdit)
            dde.ClientSideEvents.CloseUp = String.Format("function (s, e) {{ ApplyFilter(s, {0})}}", e.Column.FieldName)
            dde.ClientSideEvents.TextChanged = String.Format("function(s,e) {{SynchronizeListBoxValues(s, e, cls{0});}}", e.Column.FieldName)
            dde.ClientSideEvents.DropDown = String.Format("function(s,e) {{SynchronizeListBoxValues(s, e, cls{0});}}", e.Column.FieldName)
            dde.ReadOnly = True
        End If
    End Sub

    Protected Sub grid_AutoFilterCellEditorCreate(ByVal sender As Object, ByVal e As ASPxGridViewEditorCreateEventArgs)
        Dim grid As ASPxGridView = TryCast(sender, ASPxGridView)
        If e.Column.FieldName = "CategoryName" OrElse e.Column.FieldName = "CategoryID" Then
            Dim dde As New DropDownEditProperties()
            dde.ClientInstanceName = e.Column.FieldName

            dde.EnableClientSideAPI = True

            dateTemplate = New DataTemplate(AccessDataSource1)

            dde.DropDownWindowTemplate = dateTemplate
            e.EditorProperties = dde
        End If
    End Sub
    Protected Sub grid_ProcessColumnAutoFilter(ByVal sender As Object, ByVal e As ASPxGridViewAutoFilterEventArgs)
        If e.Column.FieldName <> "CategoryName" AndAlso e.Column.FieldName <> "CategoryID" Then
            Return
        End If

        If e.Kind = GridViewAutoFilterEventKind.CreateCriteria Then
            criteriaValues(e.Column.FieldName) = e.Value
            Session("criteriaValues") = criteriaValues
        End If
        If e.Kind = GridViewAutoFilterEventKind.ExtractDisplayText Then
            criteriaValues = TryCast(Session("criteriaValues"), Dictionary(Of Object, String))
            If criteriaValues IsNot Nothing Then
                e.Value = criteriaValues(e.Column.FieldName)
            End If
        End If
        Dim values() As String = TryCast(e.Value.Split(";"c), String())

        Dim criteria As String = String.Empty

        Dim group As New GroupOperator()
        group.OperatorType = GroupOperatorType.Or
        For Each value As String In values
            Dim op As New BinaryOperator(e.Column.FieldName, value, BinaryOperatorType.Equal)
            group.Operands.Add(op)
        Next value
        e.Criteria = group
    End Sub
End Class
