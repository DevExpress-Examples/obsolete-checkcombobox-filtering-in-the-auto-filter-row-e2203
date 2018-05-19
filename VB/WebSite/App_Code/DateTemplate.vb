Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxEditors


Public Class DataTemplate
	Implements ITemplate
	Private accessDS As AccessDataSource

	Public Sub New(ByVal accessDS As AccessDataSource)
		Me.accessDS = accessDS
	End Sub

	Public Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
		Dim dde As ASPxDropDownEdit = TryCast(container.NamingContainer.NamingContainer, ASPxDropDownEdit)
		Dim colName As String = dde.ClientInstanceName

		Dim listBox As New ASPxListBox()
		container.Controls.Add(listBox)

		listBox.ID = "listBox"
		listBox.Width = New Unit(100, UnitType.Percentage)
		listBox.ClientInstanceName = "cls" & colName
		listBox.SelectionMode = ListEditSelectionMode.CheckColumn
		listBox.ClientSideEvents.SelectedIndexChanged = String.Format("function(s,e) {{ OnListBoxSelectionChanged(s,e,{0}); }}", colName)

		Dim command As String = String.Format("SELECT DISTINCT [{0}] FROM [Categories]", colName)
		Dim ds As New AccessDataSource(accessDS.DataFile, command)

		Dim dv As DataView = TryCast(ds.Select(DataSourceSelectArguments.Empty), DataView)
		listBox.Items.Add("(Select all)")
		For i As Integer = 0 To dv.Count - 1
			listBox.Items.Add(dv(i)(0).ToString(),i)
		Next i

		Dim table As New Table()
		table.Width = New Unit(100, UnitType.Percentage)
		container.Controls.Add(table)
		Dim row As New TableRow()
		row.HorizontalAlign = HorizontalAlign.Right
		row.Width = New Unit(100, UnitType.Percentage)
		table.Rows.Add(row)

		Dim cell As New TableCell()
		cell.Width = New Unit(100, UnitType.Percentage)
		row.Cells.Add(cell)
		Dim btn As New ASPxButton()
		btn.AutoPostBack = False
		btn.Text = "Close"
		btn.ClientSideEvents.Click = String.Format("function(s, e){{ {0}.HideDropDown(); grid.AutoFilterByColumn('{0}', {0}.GetText()); }}", colName)
		cell.Controls.Add(btn)
	End Sub
End Class

