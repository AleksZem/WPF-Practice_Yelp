using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using milestone4;
using Npgsql;

namespace CptS451_Milestone2
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        public void PopulatefromBusiness(NpgsqlDataReader reader = null)
        {
            if (reader != null)
            {
                while (reader.Read())
                {

                    BusinessInfoGroupBox.Header = reader["bname"].ToString();
                    TextBlock_BusinessName.Text = reader["bname"].ToString();
                    TextBlock_Address.Text = reader["baddress"].ToString();
                    TextBlock_AvgStars.Text = reader["bavgstars"].ToString();
                    TextBlock_City.Text = reader["bcity"].ToString();
                    TextBlock_NumCheckins.Text = reader["numCheckIns"].ToString();
                    TextBlock_NumReviews.Text = reader["brevcount"].ToString();
                    TextBlock_State.Text = reader["bstate"].ToString();
                    DataGridTextColumn day = new DataGridTextColumn();
                    DataGridTextColumn open = new DataGridTextColumn();
                    DataGridTextColumn close = new DataGridTextColumn();
                    day.Header = "Day";
                    open.Header = "Open";
                    close.Header = "Close";
                    day.Binding = new Binding("Day");
                    open.Binding = new Binding("Open");
                    close.Binding = new Binding("Close");
                    DataGrid_Hours.Columns.Add(day);
                    DataGrid_Hours.Columns.Add(open);
                    DataGrid_Hours.Columns.Add(close);
                    var bhours1 = reader["bhours"];
                    string[,] bhours = bhours1 as string[,];
                    if (bhours != null)
                    {
                        for (int i = 0; i < bhours.GetLength(0); i++)
                        {
                            DataGrid_Hours.Items.Add(new Hours()
                            {
                                Day = bhours[i, 0].ToString(),
                                Open = bhours[i, 1],
                                Close = bhours[i, 2]
                            });

                        }

                    }
                    else
                    {

                    }

                }
            }


        }
        public class Hours
        {
            public string Day { get; set; }
            public string Open { get; set; }
            public string Close { get; set; }

        }


    }
}
