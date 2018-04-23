using System;
using System.Web.Hosting;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.Data.Filtering;
using DevExpress.DataAccess.ConnectionParameters;

namespace ASPxDashboard_OlapFiltering {
    public partial class WebForm1 : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

            Dashboard dashboard = new Dashboard();
            dashboard.LoadFromXml(HostingEnvironment.MapPath(@"~/App_Data/Dashboard.xml"));
            PivotDashboardItem pivot1 = (PivotDashboardItem)dashboard.Items[0];
            DashboardOlapDataSource olapDataSource = (DashboardOlapDataSource)dashboard.DataSources[0];

            string fieldYearName = "[Date].[Calendar].[Calendar Year]";
            string year2001 = "[Date].[Calendar].[Calendar Year].&[2001]";
            string year2002 = "[Date].[Calendar].[Calendar Year].&[2002]";

            string fieldCountryName = "[Customer].[Country].[Country]";
            string countryCanada = "[Customer].[Country].&[Canada]";

            DynamicListLookUpSettings settings = new DynamicListLookUpSettings();
            settings.DataSource = olapDataSource;
            settings.ValueMember = fieldCountryName;
            DashboardParameter parameter1 = new DashboardParameter("Parameter1",
                typeof(string), countryCanada, "Category", true, settings);
            dashboard.Parameters.Add(parameter1);

            CriteriaOperator filterCriteria = GroupOperator.And(
                new BinaryOperator(new OperandProperty(fieldCountryName),
                                   new OperandParameter(parameter1.Name),
                                   BinaryOperatorType.Equal),
                new NotOperator(new InOperator(new OperandProperty(fieldYearName),
                                               new ConstantValue[] { new ConstantValue(year2001),
                                                                     new ConstantValue(year2002) 
                                                                   })));

            pivot1.FilterString = filterCriteria.ToString();

            ASPxDashboard1.OpenDashboard(dashboard.SaveToXDocument());            
        }

        protected void ASPxDashboard1_ConfigureDataConnection(object sender, ConfigureDataConnectionWebEventArgs e) {
            if (e.DataSourceName == "olapDataSource1") {
                OlapConnectionParameters olapParams = new OlapConnectionParameters();
                olapParams.ConnectionString = @"Provider=MSOLAP;
                                        Data Source=https://demos.devexpress.com/Services/OLAP/msmdpump.dll;  
                                        Initial catalog=Adventure Works DW Standard Edition;
                                        Cube name=Adventure Works;
                                        Query Timeout=100;";
                e.ConnectionParameters = olapParams;
            }
        }
    }
}