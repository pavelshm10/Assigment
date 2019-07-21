using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    public DataTable SearchResTbl
    {
        set { ViewState["_SearchResTb"] = value; }
        get { return (DataTable)ViewState["_SearchResTb"]; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindData();
    }

    private void BindData()
    {
        //init grid view
        SearchResTbl = null;
        SearchResTbl = new DataAcces("sp_topProducts", CommandType.StoredProcedure, null, Enums.ExecuteType.DataTable).ResultDataTable;
        GridView1.DataSource = SearchResTbl;
        GridView1.DataBind();

        //init drop downlist and min max values for store sales
        getDropList();
        colorMaxAndMin();
    }

    protected void filterByStore(object sender, EventArgs e)
    {
        SearchResTbl = null;
        if (DropDownList1.SelectedItem.Value == DropDownList1.Items[0].Value)
        {
            SearchResTbl = new DataAcces("sp_topProducts", CommandType.StoredProcedure, null, Enums.ExecuteType.DataTable).ResultDataTable;
        }
        else
        {
            SearchResTbl = new DataAcces("filterByStore'" + DropDownList1.SelectedItem.Value + "'", CommandType.StoredProcedure, null, Enums.ExecuteType.DataTable).ResultDataTable;
        }
        GridView1.DataSource = SearchResTbl;
        GridView1.DataBind();
        colorMaxAndMin();
    }

    private void getDropList()
    {
        SearchResTbl = new DataAcces("getAllStores", CommandType.StoredProcedure, null, Enums.ExecuteType.DataTable).ResultDataTable;
        DropDownList1.DataSource = SearchResTbl;
        DropDownList1.DataTextField = "store_desc";
        DropDownList1.DataValueField = "store_desc";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, new ListItem("--Select store to fiter the table--"));
    }

    private void colorMaxAndMin()
    {
        Boolean minFlag = false;
        Boolean maxFlag = false;
        string minMax = new DataAcces("colorMaxAndMin @min out,@max out", CommandType.StoredProcedure, null, Enums.ExecuteType.NonQuery).ResultScalar;    //ToString();
        string[] str = minMax.Split(' ');
        string min = str[0];
        string max = str[1];
        //Response.Write("<script>alert('" + GridView1.Rows.Count() + "');</script>");
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            string[] sales = GridView1.Rows[i].Cells[6].Text.Split('.');
            string salesIntger = sales[0];
            if (salesIntger == min && minFlag == false)
            {
                GridView1.Rows[i].Cells[6].BackColor = System.Drawing.Color.Red;
                minFlag = true;
            }
            if (salesIntger == max && maxFlag == false)
            {
                GridView1.Rows[i].Cells[6].BackColor = System.Drawing.Color.Green;
                maxFlag = true;
            }
            if (minFlag == true && maxFlag == true)
                break;
        }
    }

    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)
                ViewState["sortDirection"] = SortDirection.Ascending;

            return (SortDirection)ViewState["sortDirection"];
        }
        set { ViewState["sortDirection"] = value; }
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression;

        if (GridViewSortDirection == SortDirection.Ascending)
        {
            GridViewSortDirection = SortDirection.Descending;
            SortGridView(sortExpression, DESCENDING);
        }
        else
        {
            GridViewSortDirection = SortDirection.Ascending;
            SortGridView(sortExpression, ASCENDING);
        }
    }

    private void SortGridView(string sortExpression, string direction)
    {
        SearchResTbl = null;
        SearchResTbl = new DataAcces("sp_topProducts", CommandType.StoredProcedure, null, Enums.ExecuteType.DataTable).ResultDataTable;
        
        DataView dv = new DataView(SearchResTbl);
        dv.Sort = sortExpression + direction;

        GridView1.DataSource = dv;
        GridView1.DataBind();
        getDropList();
        colorMaxAndMin();
    }

}