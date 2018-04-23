Imports System.Web.Hosting
Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWeb
Imports DevExpress.Data.Filtering
Imports DevExpress.DataAccess.ConnectionParameters

Namespace ASPxDashboard_OlapFiltering
    Partial Public Class WebForm1
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

            Dim dashboard As New Dashboard()
            dashboard.LoadFromXml(HostingEnvironment.MapPath("~/App_Data/Dashboard.xml"))
            Dim pivot1 As PivotDashboardItem = CType(dashboard.Items(0), PivotDashboardItem)
            Dim olapDataSource As DashboardOlapDataSource = CType(dashboard.DataSources(0), DashboardOlapDataSource)

            Dim fieldYearName As String = "[Date].[Calendar].[Calendar Year]"
            Dim year2001 As String = "[Date].[Calendar].[Calendar Year].&[2001]"
            Dim year2002 As String = "[Date].[Calendar].[Calendar Year].&[2002]"

            Dim fieldCountryName As String = "[Customer].[Country].[Country]"
            Dim countryCanada As String = "[Customer].[Country].&[Canada]"

            Dim settings As New DynamicListLookUpSettings()
            settings.DataSource = olapDataSource
            settings.ValueMember = fieldCountryName
            Dim parameter1 As New DashboardParameter("Parameter1", GetType(String),
                                                     countryCanada, "Category", True, settings)
            dashboard.Parameters.Add(parameter1)

            Dim filterCriteria As CriteriaOperator = GroupOperator.And(
                New BinaryOperator(New OperandProperty(fieldCountryName),
                                   New OperandParameter(parameter1.Name),
                                   BinaryOperatorType.Equal),
                New NotOperator(New InOperator(New OperandProperty(fieldYearName),
                                               New ConstantValue() {New ConstantValue(year2001),
                                                                    New ConstantValue(year2002)})))

            pivot1.FilterString = filterCriteria.ToString()

            ASPxDashboard1.OpenDashboard(dashboard.SaveToXDocument())
        End Sub

        Protected Sub ASPxDashboard1_ConfigureDataConnection(ByVal sender As Object,
                                                             ByVal e As ConfigureDataConnectionWebEventArgs)
            If e.DataSourceName = "olapDataSource1" Then
                Dim olapParams As New OlapConnectionParameters()
                olapParams.ConnectionString = "provider=MSOLAP;" _
                                & ControlChars.CrLf &
                                "data source=http://demos.devexpress.com/Services/OLAP/msmdpump.dll;" _
                                & ControlChars.CrLf &
                                "initial catalog=Adventure Works DW Standard Edition;" _
                                & ControlChars.CrLf &
                                "cube name=Adventure Works;"
                e.ConnectionParameters = olapParams
            End If
        End Sub
    End Class
End Namespace