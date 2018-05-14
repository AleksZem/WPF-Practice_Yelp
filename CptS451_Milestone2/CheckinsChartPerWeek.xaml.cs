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
using Npgsql;

namespace CptS451_Milestone2
{
    /// <summary>
    /// Interaction logic for CheckinsChartPerWeek.xaml
    /// </summary>
    public partial class CheckinsChartPerWeek : Window
    {
        public CheckinsChartPerWeek(string typeofchart, string bname,NpgsqlDataReader reader = null)
        {   
            InitializeComponent();
            List<KeyValuePair<string, int>> myChartData = new List<KeyValuePair<string, int>>();
            CheckinsGroupBox.Header = bname;
            switch (typeofchart)
            {
                case ("weekly"):
                    if (reader != null)
                    {
                        try
                        {
                            Chart1.Title = "Number of Checkings Per Day of Week";
                            CheckinWindow.Title = "Checkings Per Day of Week";
                            CheckinsData.Title = "#Checkings Per Day of Week";
                            reader.Read();
                            int mondayHour = (reader["monday"] as int[]).Sum();
                            int tuesdayHour = (reader["tuesday"] as int[]).Sum();
                            int wednesdayHour = (reader["wednesday"] as int[]).Sum();
                            int thursdayHour = (reader["thursday"] as int[]).Sum();
                            int fridayHour = (reader["friday"] as int[]).Sum();
                            int saturdayHour = (reader["saturday"] as int[]).Sum();
                            int sundayHour = (reader["sunday"] as int[]).Sum();

                            myChartData.Add(new KeyValuePair<string, int>("Monday", mondayHour));
                            myChartData.Add(new KeyValuePair<string, int>("Tuesday", tuesdayHour));
                            myChartData.Add(new KeyValuePair<string, int>("Wednesday", wednesdayHour));
                            myChartData.Add(new KeyValuePair<string, int>("Thursday", thursdayHour));
                            myChartData.Add(new KeyValuePair<string, int>("Friday", fridayHour));
                            myChartData.Add(new KeyValuePair<string, int>("Saturday", saturdayHour));
                            myChartData.Add(new KeyValuePair<string, int>("Sunday", sundayHour));
                        }
                        catch
                        {

                        }
                    }
                    break;
                case ("zips"):
                    Chart1.Title = "Number of Businesses Per Zip";
                    CheckinWindow.Title = "Businesses Per Zip";
                    CheckinsData.Title = "#Businesses Per Zip";
                    while (reader.Read())
                    {
                        myChartData.Add(new KeyValuePair<string, int>(reader.GetString(0), reader.GetInt32(1)));
                    }
                    break;
                case ("daily"):
                    reader.Read();
                    Chart1.Title = "Number of Checkins per Time of Day";
                    CheckinWindow.Title = "Checkins per Time of Day";
                    CheckinsData.Title = "#Checkins per Time of Day";
                    int mondayTime1 = (reader[bname] as int[])[0];
                    int mondayTime2 = (reader[bname] as int[])[1];
                    int mondayTime3 = (reader[bname] as int[])[2];
                    int mondayTime4 = (reader[bname] as int[])[3];
                    myChartData.Add(new KeyValuePair<string, int>("6am-12pm", mondayTime1));
                    myChartData.Add(new KeyValuePair<string, int>("12pm-5pm", mondayTime2));
                    myChartData.Add(new KeyValuePair<string, int>("5pm-11pm", mondayTime3));
                    myChartData.Add(new KeyValuePair<string, int>("11pm-6am", mondayTime4));
                    break;
            }
            Chart1.DataContext = myChartData;

        }
    }
}
