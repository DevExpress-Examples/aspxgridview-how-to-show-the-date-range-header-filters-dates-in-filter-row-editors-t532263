# ASPxGridView - How to show the date range header filter’s dates in filter row editors


By default, ASPxGridView doesn't provide functionality to show start and end dates from the header date range filter in the filter row because only one date editor is displayed in this row. This example illustrates how to display these dates in custom date editors in the filter row once the header date range filter is applied and synchronize these editors' values with the date editors located in the header filter popup.<br><br>To accomplish this task, you can add two custom ASPxDateEditors in an appropriate column's FilterTemplate as demonstrated in the <a href="https://www.devexpress.com/Support/Center/p/E1990">Date Range Filtering in the Filter Row</a> example. Process the applied date range filter in the grid's CustomJSProperties event handler and pass the filter dates to the client side by using JSProperties. Then, set your custom ASPxDateEditor filter row editors in the client-side EndCallback even handler based on the passed JSProperties. You can apply the filter set by these custom filter row editors as demonstrated in the <a href="https://www.devexpress.com/Support/Center/p/E1990">Date Range Filtering in the Filter Row</a> example.

<br/>


