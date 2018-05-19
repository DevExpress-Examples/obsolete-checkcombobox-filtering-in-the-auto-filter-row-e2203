<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v9.3, Version=9.3.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v9.3, Version=9.3.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        var textSeparator = ";";

        function OnListBoxSelectionChanged(listBox, args, checkComboBox) {
            if (args.index == 0)
                args.isSelected ? listBox.SelectAll() : listBox.UnselectAll();
            UpdateSelectAllItemState(listBox);
            UpdateText(listBox, checkComboBox);
        }
        function UpdateSelectAllItemState(checkListBox) {
            IsAllSelected(checkListBox) ? checkListBox.SelectIndices([0]) : checkListBox.UnselectIndices([0]);
        }
        function IsAllSelected(checkListBox) {
            for (var i = 1; i < checkListBox.GetItemCount(); i++)
                if (!checkListBox.GetItem(i).selected)
                return false;
            return true;
        }
        function UpdateText(checkListBox, checkComboBox) {
            var selectedItems = checkListBox.GetSelectedItems();
            checkComboBox.SetText(GetSelectedItemsText(selectedItems));
        }
        function SynchronizeListBoxValues(dropDown, args, checkListBox) {
            checkListBox.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = GetValuesByTexts(texts, checkListBox);
            checkListBox.SelectValues(values);
            UpdateSelectAllItemState(checkListBox);
            UpdateText(checkListBox, dropDown);  // for remove non-existing texts
        }
        function GetSelectedItemsText(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                if (items[i].index != 0)
                texts.push(items[i].text);
            return texts.join(textSeparator);
        }
        function GetValuesByTexts(texts, checkListBox) {
            var actualValues = [];
            var value = "";
            for (var i = 0; i < texts.length; i++) {
                value = GetValueByText(texts[i], checkListBox);
                if (value != null)
                    actualValues.push(value);
            }
            return actualValues;
        }
        function GetValueByText(text, checkListBox) {
            for (var i = 0; i < checkListBox.GetItemCount(); i++)
                if (checkListBox.GetItem(i).text.toUpperCase() == text.toUpperCase())
                return checkListBox.GetItem(i).value;
            return null;
        }

        function ApplyFilter(dde, colName) {
            grid.AutoFilterByColumn(colName, colName.GetText());
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dx:ASPxGridView ID="grid" runat="server" 
            DataSourceID="AccessDataSource1" AutoGenerateColumns="False" 
            KeyFieldName="CategoryID" 
            onautofiltercelleditorinitialize="grid_AutoFilterCellEditorInitialize" 
            onautofiltercelleditorcreate="grid_AutoFilterCellEditorCreate" 
            onprocesscolumnautofilter="grid_ProcessColumnAutoFilter">
            <Columns>
                <dx:GridViewCommandColumn VisibleIndex="0">
                    <ClearFilterButton Visible="True">
                    </ClearFilterButton>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="CategoryID" ReadOnly="True" 
                    VisibleIndex="1">
                    <Settings ShowFilterRowMenu="False"  />
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CategoryName" VisibleIndex="2">
                    <Settings ShowFilterRowMenu="False"  />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="3">
                </dx:GridViewDataTextColumn>
            </Columns>
            <Settings ShowFilterRow="True" ShowFilterRowMenu="True"/>
        </dx:ASPxGridView>
        <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
            DataFile="~/App_Data/nwind.mdb" 
            SelectCommand="SELECT [CategoryID], [CategoryName], [Description] FROM [Categories]">
        </asp:AccessDataSource>
    
    </div>
    </form>
</body>
</html>
