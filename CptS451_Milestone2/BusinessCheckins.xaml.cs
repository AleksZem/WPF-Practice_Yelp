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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }
        public void PopulateFromReader(string type,string bname,NpgsqlDataReader reader = null)
        {
            if (reader != null)
            {
                CheckinWindow.Title = type;
                CheckinGroupBox.Header = bname;
                switch (type)
                {
                    case ("Checkins"):
                        break;
                    case ("Tips"):

                        DataGridTextColumn uname = new DataGridTextColumn();
                        DataGridTextColumn tip = new DataGridTextColumn();
                        uname.Header = "User Name";
                        tip.Header = "Tip";
                        uname.Binding = new Binding("Uname");
                        tip.Binding = new Binding("Tip");
                        CheckinDataGrid.Columns.Add(uname);
                        CheckinDataGrid.Columns.Add(tip);


                        while (reader.Read())
                        {
                            CheckinDataGrid.Items.Add(new TipHolder() { Uname = reader["uname"].ToString(), Tip = reader["tip_text"].ToString() });
                        }


                        break;

                }
            }

        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            return;
        }
        public class TipHolder
        {
            public string Uname
            {
                get;set;
            }
            public string Tip
            {
                get;set;
            }
        }
    }
}
