using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web;


public class DataTemplate : ITemplate {
    AccessDataSource accessDS;

    public DataTemplate(AccessDataSource accessDS) {
        this.accessDS = accessDS;
    }

    public void InstantiateIn(Control container) {
        ASPxDropDownEdit dde = container.NamingContainer.NamingContainer as ASPxDropDownEdit;
        string colName = dde.ClientInstanceName;

        ASPxListBox listBox = new ASPxListBox();
        container.Controls.Add(listBox);

        listBox.ID = "listBox";
        listBox.Width = new Unit(100, UnitType.Percentage);
        listBox.ClientInstanceName = "cls" + colName;
        listBox.SelectionMode = ListEditSelectionMode.CheckColumn;
        listBox.ClientSideEvents.SelectedIndexChanged = string.Format("function(s,e) {{ OnListBoxSelectionChanged(s,e,{0}); }}", colName);

        string command = string.Format("SELECT DISTINCT [{0}] FROM [Categories]", colName);
        AccessDataSource ds = new AccessDataSource(accessDS.DataFile, command);

        DataView dv = ds.Select(DataSourceSelectArguments.Empty) as DataView;
        listBox.Items.Add("(Select all)");
        for (int i = 0; i < dv.Count; i++) {
            listBox.Items.Add(dv[i][0].ToString(),i );
        }

        Table table = new Table();
        table.Width = new Unit(100, UnitType.Percentage);
        container.Controls.Add(table);
        TableRow row = new TableRow();
        row.HorizontalAlign = HorizontalAlign.Right;
        row.Width = new Unit(100, UnitType.Percentage);
        table.Rows.Add(row);

        TableCell cell = new TableCell();
        cell.Width = new Unit(100, UnitType.Percentage);
        row.Cells.Add(cell);
        ASPxButton btn = new ASPxButton();
        btn.AutoPostBack = false;
        btn.Text = "Close";
        btn.ClientSideEvents.Click = string.Format("function(s, e){{ {0}.HideDropDown(); grid.AutoFilterByColumn('{0}', {0}.GetText()); }}", colName);
        cell.Controls.Add(btn);
    }
}

