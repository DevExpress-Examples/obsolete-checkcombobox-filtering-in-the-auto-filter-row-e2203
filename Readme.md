<!-- default file list -->
*Files to look at*:

* [Solution.sln](./CS/Solution.sln) (VB: [Solution.sln](./VB/Solution.sln))
* [DateTemplate.cs](./CS/WebSite/App_Code/DateTemplate.cs) (VB: [DateTemplate.vb](./VB/WebSite/App_Code/DateTemplate.vb))
* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))
<!-- default file list end -->
# OBSOLETE - CheckComboBox filtering in the Auto Filter Row
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/e2203)**
<!-- run online end -->


<p><strong>UPDATED #2:</strong><br><br>Please note that <strong>starting with version v13.1</strong>, ASPxGridView provides the column's <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebGridViewColumn_FilterTemplatetopic">FilterTemplate</a> that allows using ASPxGridLookup as a multi-select AutoFilterRow editor. So, if you need to accomplish this task using the <strong>AutoFilterRow</strong> <a href="https://documentation.devexpress.com/#AspNet/CustomDocument3716">filter type</a> (i.e., not the built-in multi-select <strong>Header Filter</strong>), consider using the approach illustrated in the <a href="https://www.devexpress.com/Support/Center/p/E4521">E4521: ASPxGridView - How to change a default editor to ASPxGridLookup in FilterRow via FilterTemplate</a> example instead.<br><br><strong>UPDATED #1:</strong><br><br></p>
<p>Please note that <a href="https://www.devexpress.com/Support/Center/p/S90968">starting with version v12.1</a>, ASPxGridView <strong>supports such a feature out-of-the-box</strong>. Please refer tot he <a href="https://documentation.devexpress.com/#AspNet/CustomDocument4022">Header Filter</a> topic for details. For older versions, you can use the following solution.</p>
<p><br>This example demonstrates how to introduce a multi-select dropdown list, based upon column values, in the Auto Filter Row editor of a column.<br> </p>
<p>1. There are two columns using an <a href="http://documentation.devexpress.com/#AspNet/clsDevExpressWebASPxEditorsASPxDropDownEdittopic">ASPxDropDownEdit</a> editor with a checked <a href="http://documentation.devexpress.com/#AspNet/clsDevExpressWebASPxEditorsASPxListBoxtopic">ASPxListBox</a> in its dropdown template.</p>
<p>The DropDownEditProperties object should be instantiated in the <a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewASPxGridView_AutoFilterCellEditorCreatetopic">ASPxGridView.AutoFilterCellEditorCreate</a> event handler as usual, but its adjustment should be performed in the <a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewASPxGridView_AutoFilterCellEditorInitializetopic">ASPxGridView.AutoFilterCellEditorInitialize</a> event handler.</p>
<p>2. When selecting/unselecting an item in the listbox, the corresponding element is added to the GridView's a filter condition with the <a href="http://documentation.devexpress.com/#CoreLibraries/DevExpressDataFilteringGroupOperatorTypeEnumtopic">GroupOperatorType.Or</a> condition.</p>
<p><strong>See Also:</strong><br> <a href="https://www.devexpress.com/Support/Center/p/E1990">Date range filtering in the Filter Row</a><br> <a href="https://www.devexpress.com/Support/Center/p/E1950">ASPxGridView - Date auto filter</a><br> <a href="https://www.devexpress.com/Support/Center/p/E2250">How to implement a CheckComboBox editor in the ASPxGridView</a></p>

<br/>


