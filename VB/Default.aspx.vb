Imports Microsoft.VisualBasic
Imports DevExpress.Data.Filtering
Imports DevExpress.Web
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		grid.Columns("BirthDate").FilterTemplate = New DateSelector()
	End Sub

	Public Class DateSelector
		Implements ITemplate
		Public Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
			Dim lbl As New ASPxLabel()
			lbl.ID = "lblFrom"
			lbl.Text = "From:"
			container.Controls.Add(lbl)

			Dim dateFrom As New ASPxDateEdit()
			dateFrom.ID = "dateFrom"
			dateFrom.ClientInstanceName = "dFrom"
			dateFrom.Date = New DateTime(1950, 1, 1)
			container.Controls.Add(dateFrom)

			lbl = New ASPxLabel()
			lbl.ID = "lblTo"
			lbl.Text = "To:"
			container.Controls.Add(lbl)

			Dim dateTo As New ASPxDateEdit()
			dateTo.ID = "dateTo"
			dateTo.ClientInstanceName = "dTo"
			dateTo.Date = New DateTime(1960, 11, 30)
			container.Controls.Add(dateTo)

			Dim btn As New ASPxButton()
			btn.ID = "btnApply"
			btn.Text = "Apply"
			btn.AutoPostBack = False
			btn.ClientSideEvents.Click = "function (s, e) { ApplyFilter( dFrom, dTo ); }"
			container.Controls.Add(btn)
		End Sub
	End Class
	Protected Sub grid_CustomJSProperties(ByVal sender As Object, ByVal e As ASPxGridViewClientJSPropertiesEventArgs)
	  Dim grid = TryCast(sender, ASPxGridView)
	  Dim filterExpression = (CType(grid.Columns("BirthDate"), GridViewDataColumn)).FilterExpression
	  If (Not String.IsNullOrEmpty(filterExpression)) Then
		Dim co = CType(CriteriaOperator.Parse(filterExpression), GroupOperator)
		Dim start As New DateTime()
		Dim [end] As New DateTime()
		For Each op As BinaryOperator In co.Operands
			Dim val As OperandValue = CType(op.RightOperand, OperandValue)
			If op.OperatorType = BinaryOperatorType.GreaterOrEqual Then
				start = CDate(val.Value)
			ElseIf op.OperatorType = BinaryOperatorType.Less Then
				[end] = (CDate(val.Value)).AddDays(-1)
			ElseIf op.OperatorType = BinaryOperatorType.LessOrEqual Then
				[end] = CDate(val.Value)
			End If
		Next op
		Dim dates = New Dates()
		dates.DateFrom = start
		dates.DateTo = [end]
		e.Properties("cpDates") = dates
	  End If

	End Sub
	Protected Sub grid_ProcessColumnAutoFilter(ByVal sender As Object, ByVal e As ASPxGridViewAutoFilterEventArgs)
		If e.Value = "|" Then
			Return
		End If
		If e.Column.FieldName = "BirthDate" Then
			If e.Kind = GridViewAutoFilterEventKind.CreateCriteria Then
				Dim dates() As String = e.Value.Split("|"c)
				Session("BirthDateFilterText") = dates(0) & " - " & dates(1)
				Dim dateFrom As DateTime = Convert.ToDateTime(dates(0)), dateTo As DateTime = Convert.ToDateTime(dates(1))
				e.Criteria = (New OperandProperty("BirthDate") >= dateFrom) And (New OperandProperty("BirthDate") < dateTo.AddDays(1))
			ElseIf e.Kind = GridViewAutoFilterEventKind.ExtractDisplayText Then
				If Session("BirthDateFilterText") IsNot Nothing Then
				  e.Value = Session("BirthDateFilterText").ToString()
				End If
			End If
		End If
	End Sub
End Class

Public Class Dates
	Private privateDateFrom As DateTime
	Public Property DateFrom() As DateTime
		Get
			Return privateDateFrom
		End Get
		Set(ByVal value As DateTime)
			privateDateFrom = value
		End Set
	End Property
	Private privateDateTo As DateTime
	Public Property DateTo() As DateTime
		Get
			Return privateDateTo
		End Get
		Set(ByVal value As DateTime)
			privateDateTo = value
		End Set
	End Property

End Class