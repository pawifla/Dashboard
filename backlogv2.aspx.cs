using SouthForkDamnDashboard.App_Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SouthForkDamnDashboard
{
    public partial class backlogv2 : System.Web.UI.Page
    {
        static int daysInMonth;
        static List<string> months = new List<string>() { "NONE", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        static int i;
        static int day = 1;
        static DateTime cDate = DateTime.Now;
        int maxYear = Convert.ToInt32(cDate.Year);
        
        static int beginYear = 2002;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                populateYears();
            }
        }
        public void populateYears()
        {
            ArrayList values = new ArrayList();
            int year = 2002;
            do
            {
                values.Add(new yearsData(year));
                year++;
            } while (year <= maxYear);
            ListView1.DataSource = values;
            ListView1.DataBind();
        }
        public class yearsData
        {
            private int year;
            public yearsData(int year)
            {
                this.year = year;
            }
            public int Year
            {
                get
                {
                    return year;
                }
            }
        }
        protected void All_Years_Click(object sender, EventArgs e)
        {
            List<DataClass> oneYearDates;
            //TODO: CREATE AN OUTER LOOP FOR YEARS, AND CREATE BACKLOG TITLES FOR EACH YEAR

            for (beginYear = 2002; beginYear <= maxYear; beginYear++)
            {
                HtmlGenericControl backLogTitle = new HtmlGenericControl("h2");
                backLogTitle.InnerText = $"Backlog for {beginYear}";
                backLogTitle.Style.Add("text-align", "center");
                title.Controls.Add(backLogTitle);
                //loops through the months in a year
                for (i = 1; i < 13; i++)
                {
                    DataAccessLayer dal = new DataAccessLayer();
                    oneYearDates = dal.getOneYear(i, beginYear);
                    daysInMonth = DateTime.DaysInMonth(beginYear, i);

                    Table monthTable = new Table();
                    monthTable.Style.Add("margin-left", "auto");
                    monthTable.Style.Add("margin-right", "auto;");
                    monthTable.Style.Add("width", "300px");
                    HtmlGenericControl Header = new HtmlGenericControl("h3");
                    Header.InnerHtml = months[i].ToString();
                    Header.Style.Add("text-align", "center");

                    //sets side of the page for table 
                    if (i % 2 == 0)
                    {
                        otherHiddenTables.Controls.Add(Header);
                        TableRow headRow = new TableRow();
                        headRow.Style.Add("font-weight", "bold");
                        headRow.Style.Add("text-align", "center");

                        TableCell dayTitle = new TableCell();
                        TableCell elevationTitle = new TableCell();
                        TableCell upstreamTitle = new TableCell();
                        TableCell downstreamTitle = new TableCell();

                        dayTitle.Text = "Day";
                        elevationTitle.Text = "Elevation";
                        upstreamTitle.Text = "Upstream";
                        downstreamTitle.Text = "Downstream";

                        headRow.Cells.Add(dayTitle);
                        headRow.Cells.Add(elevationTitle);
                        headRow.Cells.Add(upstreamTitle);
                        headRow.Cells.Add(downstreamTitle);

                        monthTable.Rows.Add(headRow);
                    }
                    else
                    {
                        hiddenTables.Controls.Add(Header);
                        TableRow headRow = new TableRow();
                        headRow.Style.Add("font-weight", "bold");
                        headRow.Style.Add("text-align", "center");
                        TableCell dayTitle = new TableCell();
                        TableCell elevationTitle = new TableCell();
                        TableCell upstreamTitle = new TableCell();
                        TableCell downstreamTitle = new TableCell();

                        dayTitle.Text = "Day";
                        elevationTitle.Text = "Elevation";
                        upstreamTitle.Text = "Upstream";
                        downstreamTitle.Text = "Downstream";

                        headRow.Cells.Add(dayTitle);
                        headRow.Cells.Add(elevationTitle);
                        headRow.Cells.Add(upstreamTitle);
                        headRow.Cells.Add(downstreamTitle);

                        monthTable.Rows.Add(headRow);

                    }

                    //this loop creates DataClass List for null value days
                    day = 1;
                    List<DataClass> nullDates = new List<DataClass>();
                    for (int date = 0; date < daysInMonth; date++)
                    {
                        DataClass nullDate = new DataClass();
                        try
                        {
                            //compares the day of month with the current day
                            //sets null values if dataDate(currDay) != dayOfMonth (day)
                            int currDay = Convert.ToInt32(oneYearDates[date].measDate);
                            if (currDay != day || date > oneYearDates.Count)
                            {
                                nullDate.measDate = day.ToString();
                                nullDate.elevation = 0;
                                nullDates.Add(nullDate);
                                date--;
                            }
                            //sets existing data
                            else
                            {
                                nullDate.measDate = oneYearDates[date].measDate;
                                nullDate.elevation = oneYearDates[date].elevation;
                                nullDate.upstream = oneYearDates[date].upstream;
                                nullDate.downstream = oneYearDates[date].downstream;
                                nullDates.Add(nullDate);
                            }
                            day++;
                        }//finds null days after the last data entry for the month
                        catch (ArgumentOutOfRangeException)
                        {
                            if (day <= daysInMonth && daysInMonth <= 31)
                            {
                                nullDate.measDate = day.ToString();
                                nullDate.elevation = 0;
                                nullDates.Add(nullDate);
                                day++;
                            }

                        }
                    }
                    //reads the dataclass lists and creates tables.
                    foreach (DataClass d in nullDates)
                    {
                        TableRow row = new TableRow();

                        TableCell dayOfMonth = new TableCell();
                        TableCell Elevation = new TableCell();
                        TableCell Upstream = new TableCell();
                        TableCell Downstream = new TableCell();
                        dayOfMonth.Style.Add("text-align", "center");
                        Elevation.Style.Add("text-align", "center");
                        Upstream.Style.Add("text-align", "center");
                        Downstream.Style.Add("text-align", "center");

                        dayOfMonth.Text = d.measDate;
                        Elevation.Text = d.elevation.ToString();
                        Upstream.Text = d.upstream.ToString();
                        Downstream.Text = d.downstream.ToString();
                        if (d.elevation == 0)
                        {
                            //sets string values for null data
                            Elevation.Text = "No Elev Data";
                            Upstream.Text = "   ";
                            Downstream.Text = "   ";
                        }

                        row.Cells.Add(dayOfMonth);
                        row.Cells.Add(Elevation);
                        row.Cells.Add(Upstream);
                        row.Cells.Add(Downstream);

                        monthTable.Rows.Add(row);
                        if (i % 2 == 0)
                        {
                            monthTable.Style.Add("height", "800px");

                            otherHiddenTables.Controls.Add(monthTable);
                        }
                        else
                        {
                            monthTable.Style.Add("height", "800px");

                            hiddenTables.Controls.Add(monthTable);
                        }
                    }

                }
            }

        }
        protected void Repeater1_ItemCommand(object source, ListViewCommandEventArgs e)
        {
            List<DataClass> oneYearDates;
            //GETS THE YEAR SELECTED FROM LIST VIEW
            DataKey key = ListView1.DataKeys[e.Item.DisplayIndex];
            int year = (int)key["year"];
            HtmlGenericControl backLogTitle = new HtmlGenericControl("h2");
            backLogTitle.InnerText = $"Backlog for {year}";
            backLogTitle.Style.Add("text-align", "center");
            title.Controls.Add(backLogTitle);

            for (i = 1; i < 13; i++)
            {
                    DataAccessLayer dal = new DataAccessLayer();
                    oneYearDates = dal.getOneYear(i, year);
                    daysInMonth = DateTime.DaysInMonth(year, i);

                    HtmlTable monthTable = new HtmlTable();
                    monthTable.Style.Add("margin-left", "auto");
                    monthTable.Style.Add("margin-right", "auto;");
                    monthTable.Style.Add("width", "300px");
                    HtmlGenericControl Header = new HtmlGenericControl("h3");
                    Header.InnerHtml = months[i].ToString();
                    Header.Style.Add("text-align", "center");

                    //sets side of the page for HtmlTable 
                    if (i % 2 == 0)
                    {
                        otherHiddenTables.Controls.Add(Header);
                        HtmlTableRow headRow = new HtmlTableRow();
                        headRow.Style.Add("font-weight", "bold");
                        headRow.Style.Add("text-align", "center");

                        HtmlTableCell dayTitle = new HtmlTableCell();
                        HtmlTableCell elevationTitle = new HtmlTableCell();
                        HtmlTableCell upstreamTitle = new HtmlTableCell();
                        HtmlTableCell downstreamTitle = new HtmlTableCell();

                        dayTitle.InnerText = "Day";
                        elevationTitle.InnerText = "Elevation";
                        upstreamTitle.InnerText = "Upstream";
                        downstreamTitle.InnerText = "Downstream";

                        headRow.Cells.Add(dayTitle);
                        headRow.Cells.Add(elevationTitle);
                        headRow.Cells.Add(upstreamTitle);
                        headRow.Cells.Add(downstreamTitle);
                        
                        monthTable.Rows.Add(headRow);
                    }
                    else
                    {
                        hiddenTables.Controls.Add(Header);
                        HtmlTableRow headRow = new HtmlTableRow();
                        headRow.Style.Add("font-weight", "bold");
                        headRow.Style.Add("text-align", "center");
                        HtmlTableCell dayTitle = new HtmlTableCell();
                        HtmlTableCell elevationTitle = new HtmlTableCell();
                        HtmlTableCell upstreamTitle = new HtmlTableCell();
                        HtmlTableCell downstreamTitle = new HtmlTableCell();

                        dayTitle.InnerText = "Day";
                        elevationTitle.InnerText = "Elevation";
                        upstreamTitle.InnerText = "Upstream";
                        downstreamTitle.InnerText = "Downstream";

                        headRow.Cells.Add(dayTitle);
                        headRow.Cells.Add(elevationTitle);
                        headRow.Cells.Add(upstreamTitle);
                        headRow.Cells.Add(downstreamTitle);

                        monthTable.Rows.Add(headRow);

                    }

                    //this loop creates DataClass List for null value days
                    day = 1;
                    List<DataClass> nullDates = new List<DataClass>();
                    for (int date = 0; date < daysInMonth; date++)
                    {
                    date = (day > daysInMonth ) ? day : date;
                        DataClass nullDate = new DataClass();
                        try
                        {
                            //compares the day of month with the current day
                            //sets null values if dataDate(currDay) != dayOfMonth (day)
                            int currDay = Convert.ToInt32(oneYearDates[date].measDate);
                            if (currDay != day || date > oneYearDates.Count)
                            {
                                nullDate.measDate = day.ToString();
                                nullDate.elevation = 0;
                                nullDates.Add(nullDate);
                                date--;
                            }
                            //sets existing data
                            else
                            {
                                nullDate.measDate = oneYearDates[date].measDate;
                                nullDate.elevation = oneYearDates[date].elevation;
                                nullDate.upstream = oneYearDates[date].upstream;
                                nullDate.downstream = oneYearDates[date].downstream;
                                nullDates.Add(nullDate);
                            }
                            day++;
                        }//finds null days after the last data entry for the month
                        catch (ArgumentOutOfRangeException)
                        {
                            if (day <= daysInMonth && daysInMonth <= 31)
                            {
                                nullDate.measDate = day.ToString();
                                nullDate.elevation = 0;
                                nullDates.Add(nullDate);
                            }
                                day++;

                        }
                    }
                    //reads the dataclass lists and creates tables.
                    foreach (DataClass d in nullDates)
                    {
                    //Set HtmlTable id to match javascript and replicate update function

                        HtmlTableRow row = new HtmlTableRow();
                        row.Attributes["ID"] = $"{year}.{i}.{d.measDate}";
                        HtmlTableCell dayOfMonth = new HtmlTableCell();
                        HtmlTableCell Elevation = new HtmlTableCell();
                        HtmlTableCell Upstream = new HtmlTableCell();
                        HtmlTableCell Downstream = new HtmlTableCell();
                        HtmlTableCell RefreshCell = new HtmlTableCell();
                        dayOfMonth.Style.Add("text-align", "center");
                        Elevation.Style.Add("text-align", "center");
                        Upstream.Style.Add("text-align", "center");
                        Downstream.Style.Add("text-align", "center");
                        

                        dayOfMonth.InnerText = d.measDate;
                        Elevation.InnerText = d.elevation.ToString();
                        Upstream.InnerText = d.upstream.ToString();
                        Downstream.InnerText = d.downstream.ToString();

                    if (d.elevation == 0)
                        Elevation.InnerText = "No Record";
                        row.Cells.Add(dayOfMonth);
                        row.Cells.Add(Elevation);
                        row.Cells.Add(Upstream);
                        row.Cells.Add(Downstream);
                    
                        monthTable.Rows.Add(row);
                        if (i % 2 == 0)
                        {
                        monthTable.Style.Add("height", "800px");

                        otherHiddenTables.Controls.Add(monthTable);
                        }
                        else
                        {
                        monthTable.Style.Add("height", "800px");

                        hiddenTables.Controls.Add(monthTable);
                        }
                    }
                nullDates.Clear();

            }
            
        }

        
    }
}