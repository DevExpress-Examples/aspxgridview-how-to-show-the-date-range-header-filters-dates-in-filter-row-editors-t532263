using DevExpress.Data.Filtering;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        grid.Columns["BirthDate"].FilterTemplate = new DateSelector();
    }

    public class DateSelector : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            ASPxLabel lbl = new ASPxLabel();
            lbl.ID = "lblFrom";
            lbl.Text = "From:";
            container.Controls.Add(lbl);

            ASPxDateEdit dateFrom = new ASPxDateEdit();
            dateFrom.ID = "dateFrom";
            dateFrom.ClientInstanceName = "dFrom";
            dateFrom.Date = new DateTime(1950, 1, 1);
            container.Controls.Add(dateFrom);

            lbl = new ASPxLabel();
            lbl.ID = "lblTo";
            lbl.Text = "To:";
            container.Controls.Add(lbl);

            ASPxDateEdit dateTo = new ASPxDateEdit();
            dateTo.ID = "dateTo";
            dateTo.ClientInstanceName = "dTo";
            dateTo.Date = new DateTime(1960, 11, 30);
            container.Controls.Add(dateTo);

            ASPxButton btn = new ASPxButton();
            btn.ID = "btnApply";
            btn.Text = "Apply";
            btn.AutoPostBack = false;
            btn.ClientSideEvents.Click = "function (s, e) { ApplyFilter( dFrom, dTo ); }";
            container.Controls.Add(btn);
        }
    }
    protected void grid_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
      var grid = sender as ASPxGridView;
      var filterExpression = ((GridViewDataColumn)grid.Columns["BirthDate"]).FilterExpression;        
      if(!string.IsNullOrEmpty(filterExpression)) {
        var co = (GroupOperator)CriteriaOperator.Parse(filterExpression);
        DateTime start = new DateTime();
        DateTime end = new DateTime();
        foreach(BinaryOperator op in co.Operands) {
            OperandValue val = (OperandValue)op.RightOperand;
            if(op.OperatorType == BinaryOperatorType.GreaterOrEqual)
                start = (DateTime)val.Value;
            else if (op.OperatorType == BinaryOperatorType.Less)
                end = ((DateTime)val.Value).AddDays(-1);
            else if (op.OperatorType == BinaryOperatorType.LessOrEqual)
                end = (DateTime)val.Value;
        }
        var dates = new Dates();
        dates.DateFrom = start;
        dates.DateTo = end;
        e.Properties["cpDates"] = dates;
    }
 
    }
    protected void grid_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
    {
        if(e.Value == "|")
            return;
        if(e.Column.FieldName == "BirthDate") {
            if(e.Kind == GridViewAutoFilterEventKind.CreateCriteria) {
                String[] dates = e.Value.Split('|');
                Session["BirthDateFilterText"] = dates[0] + " - " + dates[1];
                DateTime dateFrom = Convert.ToDateTime(dates[0]), dateTo = Convert.ToDateTime(dates[1]);
                e.Criteria = (new OperandProperty("BirthDate") >= dateFrom) & (new OperandProperty("BirthDate") < dateTo.AddDays(1));
            } else if(e.Kind == GridViewAutoFilterEventKind.ExtractDisplayText) {
                if (Session["BirthDateFilterText"]!=null)
                  e.Value = Session["BirthDateFilterText"].ToString();
            }
        }
    }
}

public class Dates
{
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }

}