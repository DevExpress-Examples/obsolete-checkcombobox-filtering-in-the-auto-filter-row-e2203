using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using DevExpress.Data.Filtering;
using System.Collections.Generic;

public partial class _Default : System.Web.UI.Page 
{
    DataTemplate dateTemplate = null;
    Dictionary<object, string> criteriaValues = new Dictionary<object, string>();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void grid_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (e.Column.FieldName == "CategoryName" || e.Column.FieldName == "CategoryID")
        {
            ASPxDropDownEdit dde = e.Editor as ASPxDropDownEdit;
            dde.ClientSideEvents.CloseUp = String.Format("function (s, e) {{ ApplyFilter(s, {0})}}", e.Column.FieldName);
            dde.ClientSideEvents.TextChanged = String.Format("function(s,e) {{SynchronizeListBoxValues(s, e, cls{0});}}", e.Column.FieldName);
            dde.ClientSideEvents.DropDown = String.Format("function(s,e) {{SynchronizeListBoxValues(s, e, cls{0});}}", e.Column.FieldName);
            dde.ReadOnly = true;
        }
    }

    protected void grid_AutoFilterCellEditorCreate(object sender, ASPxGridViewEditorCreateEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (e.Column.FieldName == "CategoryName" || e.Column.FieldName == "CategoryID")
        {
            DropDownEditProperties dde = new DropDownEditProperties();
            dde.ClientInstanceName = e.Column.FieldName;

            dde.EnableClientSideAPI = true;

            dateTemplate = new DataTemplate(AccessDataSource1);

            dde.DropDownWindowTemplate = dateTemplate;
            e.EditorProperties = dde;
        }
    }
    protected void grid_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
    {
        if (e.Column.FieldName != "CategoryName" && e.Column.FieldName != "CategoryID")
            return;

        if (e.Kind == GridViewAutoFilterEventKind.CreateCriteria)
        {
            criteriaValues[e.Column.FieldName] = e.Value;
            Session["criteriaValues"] = criteriaValues;
        }
        if (e.Kind == GridViewAutoFilterEventKind.ExtractDisplayText)
        {
            criteriaValues = Session["criteriaValues"] as Dictionary<object, string>;
            e.Value = criteriaValues[e.Column.FieldName];
        }
        string[] values = e.Value.Split(';') as string[];

        string criteria = string.Empty;

        GroupOperator group = new GroupOperator();
        group.OperatorType = GroupOperatorType.Or;
        foreach (String value in values)
        {
            BinaryOperator op = new BinaryOperator(e.Column.FieldName, value, BinaryOperatorType.Equal);
            group.Operands.Add(op);
        }
        e.Criteria = group;
    }
}
