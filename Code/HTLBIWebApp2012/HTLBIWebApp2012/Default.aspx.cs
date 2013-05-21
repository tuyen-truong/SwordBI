using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTLBIWebApp2012
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			String strConnectionString = "Data Source=(local);Initial Catalog=SwordBI_SSAS";
			MdxExecuter exec = new MdxExecuter(strConnectionString);

			String query = @"WITH 
	SET [Customer CodeFilterSet] AS FILTER([ARDimCustomer].[Customer Code].Levels(0).MEMBERS, [ARDimCustomer].[Customer Code].CURRENTMEMBER.NAME > ""1"")
	MEMBER [Measures].[Doc TotalSUM1] AS COALESCEEMPTY([Measures].[Doc Total],0)
	MEMBER [Measures].[Gros ProfitSUM2] AS COALESCEEMPTY([Measures].[Gros Profit],0)
	MEMBER [Measures].[Gros Profit FCSUM3] AS COALESCEEMPTY([Measures].[Gros Profit FC],0)
	MEMBER [Measures].[Line TotalSUM4] AS COALESCEEMPTY([Measures].[Line Total],0)
	MEMBER [Measures].[QuantitySUM5] AS COALESCEEMPTY([Measures].[Quantity],0)
SELECT 
{
	[Customer CodeFilterSet]*
	[ARDimCustomer].[Customer Address].Levels(0).MEMBERS*
	[ARDimCustomer].[Customer Contact Person].Levels(0).MEMBERS*
	[ARDimItem].[FirmCode].Levels(0).MEMBERS*
	[ARDimItem].[FirmName].Levels(0).MEMBERS*
	[ARDimItem].[ItemCode].Levels(0).MEMBERS*
	[ARDimItem].[ItemName].Levels(0).MEMBERS*
	[ARDimItem].[ItemGroupName].Levels(0).MEMBERS
} 
ON ROWS,

{
	[Measures].[Doc TotalSUM1], 
	[Measures].[Gros ProfitSUM2], 
	[Measures].[Gros Profit FCSUM3], 
	[Measures].[Line TotalSUM4], 
	[Measures].[QuantitySUM5]
} ON COLUMNS
 FROM [ARCube2]
";
			
			ASPxGridView1.DataSource = exec.ExecuteReader(query);
			ASPxGridView1.DataBind();

		}
	}
}