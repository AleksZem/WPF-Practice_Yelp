using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Microsoft.Maps.MapControl.WPF;
using Npgsql;
using CptS451_Milestone2;



namespace milestone4
{
     
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public string connectionSQL()
        //{
        //    return "Host=localhost; Username=postgres; Password=password; Database=postgres"; //Input relevent username, password, database name
        //}
        /// <summary>
        /// User
        /// </summary>
        /// 

        #region Start Up Functions
        public MainWindow()
        {

            InitializeComponent();
            PopulateStateComboBox();
            PopulateStateDailySelectionBox();
            InitializeDataGrid();
            InitializeFriendsDataGrid();
            InitializeTipsOfFriendsDataGrid();
            InitializeTipsOfUserDataGrid();
            InitializeTipsOfBusinessDataGrid();
            InitializeCheckinsOfBusinessDataGrid();
            CurrentUser currentUser = CurrentUser.Instance;
            CurrentBusiness currentBusiness = CurrentBusiness.Instance;
            CurrentConnection currentConnection = CurrentConnection.Instance;
            SetCurrentUserTextBox.Text = "Enter UserName";
            SetCurrentBusinessTextBox.Text = "Enter Business Name";
        }
        private void PopulateStateComboBox()
        {
            CurrentConnection.Instance.Command = "SELECT distinct bstate FROM yelp_businesses ORDER BY bstate";
            using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
            {
                while (reader.Read()) StateComboBox.Items.Add(reader.GetString(0));
            }
        }
        //using (var conn = new NpgsqlConnection(connectionSQL()))
        //{
        //    conn.Open();
        //    using (var cmd = new NpgsqlCommand())
        //    {
        //        cmd.Connection = conn;
        //        cmd.CommandText = "SELECT distinct bstate FROM yelp_businesses ORDER BY bstate";
        //        using (var reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read()) StateComboBox.Items.Add(reader.GetString(0));
        //        }
        //    }
        //    conn.Close();
        //}
        //}
        private void PopulateStateDailySelectionBox()
        {
            DailyCheckinDayListBox.Items.Add("Monday");
            DailyCheckinDayListBox.Items.Add("Tuesday");
            DailyCheckinDayListBox.Items.Add("Wednesday");
            DailyCheckinDayListBox.Items.Add("Thursday");
            DailyCheckinDayListBox.Items.Add("Friday");
            DailyCheckinDayListBox.Items.Add("Saturday");
            DailyCheckinDayListBox.Items.Add("Sunday");
        }
        private void InitializeDataGrid()
        {
            DataGridTextColumn bNameCol = new DataGridTextColumn();
            DataGridTextColumn bAddressCol = new DataGridTextColumn();
            DataGridTextColumn bAvgStars = new DataGridTextColumn();
            DataGridTextColumn bReviewCount = new DataGridTextColumn();
            DataGridTextColumn bIsOpen = new DataGridTextColumn();
            bNameCol.Header = "Name";
            bAddressCol.Header = "Address";
            bAvgStars.Header = "Average Stars";
            bReviewCount.Header = "Review Count";
            bIsOpen.Header = "In Business?";
            bNameCol.Binding = new Binding("bName");
            bAddressCol.Binding = new Binding("bAddress");
            bAvgStars.Binding = new Binding("bAvgStars");
            bReviewCount.Binding = new Binding("bReviewCount");
            bIsOpen.Binding = new Binding("bIsOpen");
            BusinessesDataGrid.Columns.Add(bNameCol);
            BusinessesDataGrid.Columns.Add(bAddressCol);
            BusinessesDataGrid.Columns.Add(bAvgStars);
            BusinessesDataGrid.Columns.Add(bReviewCount);
            BusinessesDataGrid.Columns.Add(bIsOpen);

        }
        private void InitializeFriendsDataGrid()
        {
            DataGridTextColumn FriendNameCol = new DataGridTextColumn();
            DataGridTextColumn FriendAvgStarsCol = new DataGridTextColumn();
            DataGridTextColumn FreindYelpSinceCol = new DataGridTextColumn();
            DataGridTextColumn FriendsVotesCol = new DataGridTextColumn();
            DataGridTextColumn FriendFansCol = new DataGridTextColumn();
            FriendNameCol.Header = "Name";
            FriendAvgStarsCol.Header = "Avg Stars";
            FreindYelpSinceCol.Header = "Yelper Since";
            FriendsVotesCol.Header = "Votes";
            FriendFansCol.Header = "Fans";
            FriendNameCol.Binding = new Binding("uName");
            FriendAvgStarsCol.Binding = new Binding("uAverageStars");
            FreindYelpSinceCol.Binding = new Binding("uYelpSince");
            FriendsVotesCol.Binding = new Binding("uTotalVotes");
            FriendFansCol.Binding = new Binding("uNumFans");
            FriendsDataGrid.Columns.Add(FriendNameCol);
            FriendsDataGrid.Columns.Add(FriendAvgStarsCol);
            FriendsDataGrid.Columns.Add(FreindYelpSinceCol);
            FriendsDataGrid.Columns.Add(FriendsVotesCol);
            FriendsDataGrid.Columns.Add(FriendFansCol);
        }
        private void InitializeTipsOfFriendsDataGrid()
        {
            DataGridTextColumn FriendNameCol = new DataGridTextColumn();
            DataGridTextColumn BusinessNameCol = new DataGridTextColumn();
            DataGridTextColumn BusinessStateCol = new DataGridTextColumn();
            DataGridTextColumn BusinessCityCol = new DataGridTextColumn();
            DataGridTextColumn TipTextCol = new DataGridTextColumn();
            FriendNameCol.Header = "Name";
            BusinessNameCol.Header = "Business";
            BusinessStateCol.Header = "State";
            BusinessCityCol.Header = "City";
            TipTextCol.Header = "Tip";
            FriendNameCol.Binding = new Binding("tipUserName");
            BusinessNameCol.Binding = new Binding("businessName");
            BusinessStateCol.Binding = new Binding("businessState");
            BusinessCityCol.Binding = new Binding("businessCity");
            TipTextCol.Binding = new Binding("tipText");
            TipsOfFriendsDataGrid.Columns.Add(FriendNameCol);
            TipsOfFriendsDataGrid.Columns.Add(BusinessNameCol);
            TipsOfFriendsDataGrid.Columns.Add(BusinessStateCol);
            TipsOfFriendsDataGrid.Columns.Add(BusinessCityCol);
            TipsOfFriendsDataGrid.Columns.Add(TipTextCol);
        }
        private void InitializeTipsOfUserDataGrid()
        {
            DataGridTextColumn BusinessNameCol = new DataGridTextColumn();
            DataGridTextColumn TipTextCol = new DataGridTextColumn();
            DataGridTextColumn TipLikesCol = new DataGridTextColumn();
            BusinessNameCol.Header = "Business";
            TipTextCol.Header = "Tip";
            TipLikesCol.Header = "Likes";
            BusinessNameCol.Binding = new Binding("businessName");
            TipTextCol.Binding = new Binding("tipText");
            TipLikesCol.Binding = new Binding("tipLikes");
            TipsOfUserDataGrid.Columns.Add(BusinessNameCol);
            TipsOfUserDataGrid.Columns.Add(TipTextCol);
            TipsOfUserDataGrid.Columns.Add(TipLikesCol);
        }
        private void InitializeTipsOfBusinessDataGrid()
        {
            DataGridTextColumn uname = new DataGridTextColumn();
            DataGridTextColumn tip = new DataGridTextColumn();
            uname.Header = "User Name";
            tip.Header = "Tip";
            uname.Binding = new Binding("Uname");
            tip.Binding = new Binding("Tip");
            TipsOfBusinessDataGrid.Columns.Add(uname);
            TipsOfBusinessDataGrid.Columns.Add(tip);
        }
        private void InitializeCheckinsOfBusinessDataGrid()
        {
            DataGridTextColumn day = new DataGridTextColumn();
            DataGridTextColumn morning = new DataGridTextColumn();
            DataGridTextColumn noon = new DataGridTextColumn();
            DataGridTextColumn evening = new DataGridTextColumn();
            DataGridTextColumn night = new DataGridTextColumn();
            day.Header = "Day of Week";
            morning.Header = "Morning";
            noon.Header = "Noon";
            evening.Header = "Evening";
            night.Header = "Night";
            day.Binding = new Binding("day");
            morning.Binding = new Binding("morning");
            noon.Binding = new Binding("noon");
            evening.Binding = new Binding("evening");
            night.Binding = new Binding("night");
            CheckinsOfBusinessDataGrid.Columns.Add(day);
            CheckinsOfBusinessDataGrid.Columns.Add(morning);
            CheckinsOfBusinessDataGrid.Columns.Add(noon);
            CheckinsOfBusinessDataGrid.Columns.Add(evening);
            CheckinsOfBusinessDataGrid.Columns.Add(night);



        }
        #endregion

        #region Log In/Out Buttons
        private void Select_User_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectUserIDListBox.SelectedItems == null || SelectUserIDListBox.Items.IsEmpty)
            {
                return;
            }
            else
            {
                SetCurrentUserTextBox.IsReadOnly = true;
                SelectUserIDListBox.IsEnabled = false;
                string selectedUserID = SelectUserIDListBox.SelectedItem.ToString();
                //TODO: MAKE SURE USER SELECTS A UID BEFORE CLICKING THIS, CANT JUST TYPE IN A NAME AND HIT THIS BUTTON

                CurrentConnection.Instance.Command = "SELECT * FROM yelp_users WHERE uid = '" + selectedUserID + "'";
                using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
                {
                    reader.Read();
                    CurrentUser.Instance.Thisuser = new User(reader);
                }

                PopulateUserInformation(CurrentUser.Instance.Thisuser);
                SetCurrentUserTextBox.Text = CurrentUser.Instance.Thisuser.uName;
                FriendsDataGrid.Items.Clear();
                TipsOfFriendsDataGrid.Items.Clear();
                PopulateFriends(CurrentUser.Instance.Thisuser);
                PopulateUserTips(CurrentUser.Instance.Thisuser);
            }
        }
        private void Logout_User_Button_Click(object sender, RoutedEventArgs e)
        {
            LogoutOfCurrentUser();
        }
        public void LogoutOfCurrentUser()
        {
            CurrentUser.Instance.Thisuser = null;
            SetCurrentUserTextBox.IsReadOnly = false;
            SelectUserIDListBox.IsEnabled = true;
            SetCurrentUserTextBox.Text = "";
            UserNameTextBox.Text = "";
            StarsTextBox.Text = "";
            FansTextBox.Text = "";
            YelperSinceTextBox.Text = "";
            VotesRatingsTextBox.Text = "";
            FunnyRatingsTextBox.Text = "";
            CoolRatingsTextBox.Text = "";
            UsefulRatingsTextBox.Text = "";
            FriendsDataGrid.Items.Clear();
            TipsOfFriendsDataGrid.Items.Clear();

            TipsOfUserDataGrid.Items.Clear();
            SetCurrentUserTextBox.Text = "Enter UserName";
        }
        private void Logout_Business_Button_Click(object sender, RoutedEventArgs e)
        {
            LogoutBusinessInformation();
        }
        private void LogoutBusinessInformation()
        {
            CurrentBusiness.Instance.Thisbusiness = null;
            SetCurrentBusinessTextBox.IsReadOnly = false;
            SelectBusinessIDListBox.IsEnabled = true;
            SetCurrentBusinessTextBox.Text = "";
            BusinessNameTextBox.Text = "";
            BusinessStarsTextBox.Text = "";
            BusinessNumRevsTextBox.Text = "";
            BusinessOpenTextBox.Text = "";
            BusinessAddressTextBox.Text = "";
            BusinessStateTextBox.Text = "";
            BusinessCityTextBox.Text = "";
            TipsOfBusinessDataGrid.Items.Clear();
            CheckinsOfBusinessDataGrid.Items.Clear();
        }
        private void Select_Business_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectBusinessIDListBox.SelectedItems == null || SelectBusinessIDListBox.Items.IsEmpty)
            {
                return;
            }
            else
            {
                SetCurrentBusinessTextBox.IsReadOnly = true;
                SelectBusinessIDListBox.IsEnabled = false;
                string selectedBusinessID = SelectBusinessIDListBox.SelectedItem.ToString();
                //TODO: MAKE SURE USER SELECTS A UID BEFORE CLICKING THIS, CANT JUST TYPE IN A NAME AND HIT THIS BUTTON

                CurrentConnection.Instance.thiscmd.CommandText = "SELECT * FROM yelp_businesses WHERE bid = '" + selectedBusinessID + "'";
                using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
                {
                    reader.Read();
                    CurrentBusiness.Instance.Thisbusiness = new DBBusinessElement(reader);
                }
                SetCurrentBusinessTextBox.Text = CurrentBusiness.Instance.Thisbusiness.bName;
                PopulateBusinessInformation(CurrentBusiness.Instance.Thisbusiness);
                TipsOfBusinessDataGrid.Items.Clear();
                CheckinsOfBusinessDataGrid.Items.Clear();
                PopulateBusinessTipsandCheckins(CurrentBusiness.Instance.Thisbusiness);
            }
        }
        #endregion

        #region Populate Functions

        private void PopulateUserTips(User currentUser)
        {
            List<CompositeTipBusiness> tips = new List<CompositeTipBusiness>();
            CurrentConnection.Instance.thiscmd.CommandText = "SELECT * FROM yelp_businesses as t0,yelp_tips as t1, Yelp_users as t2 WHERE t1.uid = t2.uid and t1.bid = t0.bid and t1.uid = '" + CurrentUser.Instance.Thisuser.uID + "'";
            using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tips.Add(new CompositeTipBusiness(reader));
                }
            }
            foreach (CompositeTipBusiness tip in tips)
            {
                TipsOfUserDataGrid.Items.Add(tip);
            }

        }

        private void PopulateFriends(User currentUser)
        {
            List<User> friends = new List<User>();
            foreach (string friendID in currentUser.getFriends())
            {
                CurrentConnection.Instance.thiscmd.CommandText = "SELECT * FROM yelp_users WHERE uid = '" + friendID + "'";
                using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
                {
                    reader.Read();
                    friends.Add(new User(reader));
                }
            }
            foreach (User friend in friends)
            {
                FriendsDataGrid.Items.Add(friend);
            }
            PopulateFriendsTips(friends);
        }

        private void PopulateFriendsTips(List<User> friends)
        {
            if (friends.Count < 1)
            {
                return;
            }
            List<CompositeTipBusiness> compositeTips = new List<CompositeTipBusiness>();
            foreach (User friendID in friends)
            {
                DBBusinessElement dBBusinessElement;
                List<Tip> tips = new List<Tip>();
                CurrentConnection.Instance.thiscmd.CommandText = "SELECT * FROM yelp_tips WHERE uid = '" + friendID.uID + "'";
                using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tips.Add(new Tip(reader));
                    }
                }
                foreach (Tip tip in tips)
                {
                    CurrentConnection.Instance.thiscmd.CommandText = "SELECT * FROM yelp_businesses WHERE bid = '" + tip.bid + "'";
                    using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
                    {
                        reader.Read();
                        dBBusinessElement = new DBBusinessElement(reader);
                        compositeTips.Add(new CompositeTipBusiness(friendID, tip, dBBusinessElement));
                    }
                }
            }
            foreach (CompositeTipBusiness tip in compositeTips)
            {
                TipsOfFriendsDataGrid.Items.Add(tip);
            }
        }

        private void PopulateUserInformation(User currentUser)
        {
            UserNameTextBox.Text = currentUser.getName();
            StarsTextBox.Text = currentUser.getAverageStars().ToString();
            FansTextBox.Text = currentUser.getNumFans().ToString();
            YelperSinceTextBox.Text = currentUser.getYelpSince();
            int votes = 0;
            votes = votes + int.Parse(currentUser.getVotes()[1, 1]) +
                int.Parse(currentUser.getVotes()[0, 1]) +
                int.Parse(currentUser.getVotes()[2, 1]);
            VotesRatingsTextBox.Text = votes.ToString();
            FunnyRatingsTextBox.Text = currentUser.getVotes()[1, 1];
            CoolRatingsTextBox.Text = currentUser.getVotes()[0, 1];
            UsefulRatingsTextBox.Text = currentUser.getVotes()[2, 1];

        }

        private void PopulateBusinessTipsandCheckins(DBBusinessElement currentBusiness)
        {
            CurrentConnection.Instance.thiscmd.CommandText = "SELECT uname,tip_text FROM Yelp_tips as t1 , Yelp_users as t2 , Yelp_businesses as t3 " +
                                        "WHERE t1.uid = t2.uid and t1.bid = t3.bid and t3.bid = '" +
                                        currentBusiness.bId + "'";
            using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    TipsOfBusinessDataGrid.Items.Add(new TipHolder() { Uname = reader["uname"].ToString(), Tip = reader["tip_text"].ToString() });
                }
            }
            CurrentConnection.Instance.thiscmd.CommandText = "SELECT * FROM Yelp_checkins as t1 " +
                                "WHERE t1.bid = '" +
                                currentBusiness.bId + "'";
            using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    CheckinsOfBusinessDataGrid.Items.Add(new CheckinHolder("Sunday", reader["sunday"] as int[]));
                    CheckinsOfBusinessDataGrid.Items.Add(new CheckinHolder("Monday", reader["monday"] as int[]));
                    CheckinsOfBusinessDataGrid.Items.Add(new CheckinHolder("Tuesday", reader["tuesday"] as int[]));
                    CheckinsOfBusinessDataGrid.Items.Add(new CheckinHolder("Wednesday", reader["wednesday"] as int[]));
                    CheckinsOfBusinessDataGrid.Items.Add(new CheckinHolder("Thursday", reader["thursday"] as int[]));
                    CheckinsOfBusinessDataGrid.Items.Add(new CheckinHolder("Friday", reader["friday"] as int[]));
                    CheckinsOfBusinessDataGrid.Items.Add(new CheckinHolder("Saturday", reader["saturday"] as int[]));
                }
            }
        }

        private void PopulateBusinessInformation(DBBusinessElement currentbusiness)
        {
            BusinessNameTextBox.Text = currentbusiness.bName;
            BusinessStarsTextBox.Text = currentbusiness.bAvgStars.ToString();
            BusinessNumRevsTextBox.Text = currentbusiness.bRevCount.ToString();
            BusinessOpenTextBox.Text = currentbusiness.bOpen.ToString();
            BusinessAddressTextBox.Text = currentbusiness.bAddress;
            BusinessStateTextBox.Text = currentbusiness.bState;
            BusinessCityTextBox.Text = currentbusiness.bCity;
        }


        #endregion

        #region Map Manipulation
        private void AddPinToMapFromBusiness(Business selectedbusiness, int zoomlevel)
        {
            BusinessMap.Center = new Location(selectedbusiness.blat, selectedbusiness.blong);
            Pushpin storepin = new Pushpin();
            storepin.Location = new Location(selectedbusiness.blat, selectedbusiness.blong);
            storepin.ToolTip = selectedbusiness.bName + "\nStar Rating: " + selectedbusiness.bAvgStars.ToString();
            BusinessMap.Children.Add(storepin);
            BusinessMap.ZoomLevel = zoomlevel;
        }

        #endregion

        #region Button Clicks
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            //if (null == CategoriesListBox.SelectedItem) return;
            if (StateComboBox.SelectedItem == null || CitiesListBox.SelectedItem == null)
            {
                MessageBox.Show("Please Select a state and a city", "No City Selected", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            BusinessesDataGrid.Items.Clear();
            BusinessMap.Children.Clear();
            //if (FilterCategoriesListBox.Items.IsEmpty)
            StringBuilder searchquery = new StringBuilder();
            if (ZipCodesListBox.SelectedItem == null)
            {
                CurrentConnection.Instance.thiscmd.CommandText = "SELECT * FROM yelp_businesses WHERE bstate='" + StateComboBox.SelectedItem.ToString() +
               "' AND " + "bcity = '" + CitiesListBox.SelectedItem.ToString() + "'";
            }
            else if (FilterCategoriesListBox.Items.IsEmpty)
            {
                CurrentConnection.Instance.thiscmd.CommandText = "SELECT * FROM yelp_businesses WHERE bstate='" + StateComboBox.SelectedItem.ToString() +
               "' AND " + "bcity = '" + CitiesListBox.SelectedItem.ToString() + "' AND baddress LIKE '%" + ZipCodesListBox.SelectedItem.ToString() +
               "'";
            }
            else
            {
                var categoriesString = String.Join(",", (FilterCategoriesListBox.Items.OfType<string>().ToList()).ToArray());
                CurrentConnection.Instance.thiscmd.CommandText = "SELECT * FROM yelp_businesses WHERE bstate='" + StateComboBox.SelectedItem.ToString() +
               "' AND " + "bcity = '" + CitiesListBox.SelectedItem.ToString() + "' AND baddress LIKE '%" + ZipCodesListBox.SelectedItem.ToString() +
               "' AND '{" + categoriesString + "}' && (bcats)";
            }
            using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Business business = new Business
                    {
                        bName = reader.GetString(1),
                        bAddress = reader.GetString(2),
                        bAvgStars = reader["bavgstars"].ToString(),
                        bReviewCount = reader.GetString(8),
                        bIsOpen = reader["bopen"].ToString(),
                        blat = (double)reader["blat"],
                        blong = (double)reader["blong"],
                        bid = reader["bid"].ToString()
                    };
                    BusinessesDataGrid.Items.Add(business);
                    AddPinToMapFromBusiness(business, 12);



                }
            }

        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (CategoriesListBox.SelectedItem != null)
            {
                bool itemAlreadyExists = false;
                foreach (var item in FilterCategoriesListBox.Items)
                {
                    if (item.ToString() == CategoriesListBox.SelectedItem.ToString())
                    {
                        itemAlreadyExists = true;
                        break;
                    }
                }
                if (!itemAlreadyExists)
                {
                    FilterCategoriesListBox.Items.Add(CategoriesListBox.SelectedItem.ToString());
                }
            }
        }

        private void RemoveCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (FilterCategoriesListBox.SelectedItem != null)
            {
                FilterCategoriesListBox.Items.Remove(FilterCategoriesListBox.SelectedItem);
            }
        }

        private void AddTipToBusinessButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckForLoggedIn("user"))
            {
                return;
            }
            if (CheckinSelectedBusinessTextbox.Text == "")
            {
                MessageBox.Show("Please Select a Business before you can add tips", "No Business Selected", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (CheckinEditTextbox.Text.Replace(" ", string.Empty) == string.Empty)
            {
                MessageBox.Show("Please Enter a Tip before you can add tips", "No Tip Text", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (BusinessesDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please Select a Business before you can add tips", "No Business Selected", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            Business selectedbusiness = (Business)BusinessesDataGrid.SelectedItems[0];
            CurrentConnection.Instance.thiscmd.CommandText = "INSERT INTO yelp_tips(uid,tip_text,bid,type)" +
                        "VALUES('" + CurrentUser.Instance.Thisuser.uID + "'," +
                        "'" + ScrubString(CheckinEditTextbox.Text) + "'," +
                        "'" + selectedbusiness.bid + "','tip')";
            using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
            {
                MessageBox.Show("Successfully added tip for business " + selectedbusiness.bName, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            CheckinEditTextbox.Text = "";
        }

        private void CheckInToBusinessButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckForLoggedIn("User"))
            {
                return;
            }
            if (CheckinSelectedBusinessTextbox.Text == "" || BusinessesDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please Select a Business before you can checkin", "No Business Selected", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            Business selectedbusiness = (Business)BusinessesDataGrid.SelectedItems[0];
            Random generator = new Random();
            int day = (generator.Next(0, Int32.MaxValue) % 7); 
            int time = (generator.Next(0, Int32.MaxValue) % 4) + 1; //sql is indexed by 1
            //this is gunna get ugly
            switch (day) {
                case (0):
                    CurrentConnection.Instance.thiscmd.CommandText = "update yelp_checkins " +
                "set monday[" + time.ToString() + "] = ( " +
                "select monday[" + time.ToString() + "] " +
                "from yelp_checkins " +
                "where bid = '" + selectedbusiness.bid + "') + 1 " +
                "where bid = '" + selectedbusiness.bid + "'";
                    break;
                case (1):
                    CurrentConnection.Instance.thiscmd.CommandText = "update yelp_checkins " +
                "set tuesday[" + time.ToString() + "] = (" +
                "select tuesday[" + time.ToString() + "] " +
                "from yelp_checkins " +
                "where bid = '" + selectedbusiness.bid + "') + 1 " +
                "where bid = '" + selectedbusiness.bid + "'";
                    break;
                case (2):
                    CurrentConnection.Instance.thiscmd.CommandText = "update yelp_checkins " +
                "set wednesday[" + time.ToString() + "] = (" +
                "select wednesday[" + time.ToString() + "] " +
                "from yelp_checkins " +
                "where bid = '" + selectedbusiness.bid + "') + 1 " +
                "where bid = '" + selectedbusiness.bid + "'";
                    break;
                case (3):
                    CurrentConnection.Instance.thiscmd.CommandText = "update yelp_checkins " +
                "set thursday[" + time.ToString() + "] = (" +
                "select thursday[" + time.ToString() + "] " +
                "from yelp_checkins " +
                "where bid = '" + selectedbusiness.bid + "') + 1 " +
                "where bid = '" + selectedbusiness.bid + "'";
                    break;
                case (4):
                    CurrentConnection.Instance.thiscmd.CommandText = "update yelp_checkins " +
                "set friday[" + time.ToString() + "] = (" +
                "select friday[" + time.ToString() + "] " +
                "from yelp_checkins " +
                "where bid = '" + selectedbusiness.bid + "') + 1 " +
                "where bid = '" + selectedbusiness.bid + "'";
                    break;
                case (5):
                    CurrentConnection.Instance.thiscmd.CommandText = "update yelp_checkins " +
                "set saturday[" + time.ToString() + "] = (" +
                "select saturday[" + time.ToString() + "] " +
                "from yelp_checkins " +
                "where bid = '" + selectedbusiness.bid + "') + 1 " +
                "where bid = '" + selectedbusiness.bid + "'";
                    break;
                case (6):
                    CurrentConnection.Instance.thiscmd.CommandText = "update yelp_checkins " +
                "set sunday[" + time.ToString() + "] = (" +
                "select sunday[" + time.ToString() + "] " +
                "from yelp_checkins " +
                "where bid = '" + selectedbusiness.bid + "') + 1 " +
                "where bid = '" + selectedbusiness.bid + "'";
                    break;
            }
            
            using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
            {
                MessageBox.Show("Successfully added checked in to business " + selectedbusiness.bName, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void ShowCityStatistics_Click(object sender, RoutedEventArgs e)
        {
            if (StateComboBox.SelectedItem == null || CitiesListBox.SelectedItem == null)
            {
                MessageBox.Show("Please Select a City", "No City Selected", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            CurrentConnection.Instance.thiscmd.CommandText = "Select sub.zip, count(yelp_businesses.bid) FROM (SELECT Distinct Right(baddress, 5) as zip, bid FROM yelp_businesses Where bcity = '" + CitiesListBox.SelectedItem.ToString() + "') " +
                        "as sub, yelp_businesses WHERE sub.bid = yelp_businesses.bid GROUP BY sub.zip;";
            using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
            {
                CheckinsChartPerWeek businessinfopage = new CheckinsChartPerWeek("zips", CitiesListBox.SelectedItem.ToString(), reader);
                businessinfopage.Show();
            }
        }

        private void ShowDailyCheckinsButton_Click(object sender, RoutedEventArgs e)
        {
            if (BusinessesDataGrid.SelectedItem == null || DailyCheckinDayListBox.SelectedItem == null)
            {
                MessageBox.Show("Please Select a Business and a Day of the Week", "No Business or Day Selected", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            }
            else
            {

                Business selectedbusiness = (Business)BusinessesDataGrid.SelectedItems[0];
                string dayofweek = DailyCheckinDayListBox.SelectedItem.ToString();
                CurrentConnection.Instance.thiscmd.CommandText = "select * " +
                            "From yelp_businesses,yelp_checkins " +
                            "WHERE yelp_checkins.bid = yelp_businesses.bid " +
                            "AND yelp_businesses.bid = '" + selectedbusiness.bid + "'";
                using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
                {
                    CheckinsChartPerWeek businessinfopage = new CheckinsChartPerWeek("daily", dayofweek, reader);
                    businessinfopage.Show();
                }
            }
        }

        private void ShowCheckinsButton_Click(object sender, RoutedEventArgs e)
        {
            if (BusinessesDataGrid.SelectedItem != null)
            {
                Business selectedbusiness = (Business)BusinessesDataGrid.SelectedItems[0];
                CurrentConnection.Instance.thiscmd.CommandText = "select * From yelp_businesses,yelp_checkins WHERE yelp_checkins.bid = yelp_businesses.bid AND yelp_businesses.bid = '" +
                                            selectedbusiness.bid + "'";
                using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
                {
                    CheckinsChartPerWeek businessinfopage = new CheckinsChartPerWeek("weekly", selectedbusiness.bName, reader);

                    businessinfopage.Show();
                }
            }
            else
            {

                MessageBox.Show("Please Select a Business", "No Business Selected", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void ShowTipsButton_Click(object sender, RoutedEventArgs e)
        {
            if (BusinessesDataGrid.SelectedItem != null)
            {
                Business selectedbusiness = (Business)BusinessesDataGrid.SelectedItems[0];
                CurrentConnection.Instance.thiscmd.CommandText = "SELECT uname,tip_text FROM Yelp_tips as t1 , Yelp_users as t2 , Yelp_businesses as t3 " +
                                            "WHERE t1.uid = t2.uid and t1.bid = t3.bid and t3.bid = '" +
                                            selectedbusiness.bid + "'";
                using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
                {
                    Window2 businessinfopage = new Window2();
                    businessinfopage.PopulateFromReader("Tips", selectedbusiness.bName, reader);
                    businessinfopage.Show();
                }
            }
            else
            {
                MessageBox.Show("Please Select a Business", "No Business Selected", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void ShowBusinessStatistics_Click(object sender, RoutedEventArgs e)
        {
            if (BusinessesDataGrid.SelectedItem != null)
            {
                Business selectedbusiness = (Business)BusinessesDataGrid.SelectedItems[0];
                CurrentConnection.Instance.thiscmd.CommandText = "SELECT * FROM Yelp_businesses WHERE bid = '" +
                             selectedbusiness.bid + "'";
                using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
                {
                    Window1 businessinfopage = new Window1();
                    businessinfopage.PopulatefromBusiness(reader);
                    businessinfopage.Show();
                }
            }
            else
            {
                MessageBox.Show("Please Select a Business", "No Business Selected", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void RemoveFriendButton_Click(object sender, RoutedEventArgs e)
        {
            if (FriendsDataGrid.SelectedItems.Count != 0)
            {
                User selectedfriend = (User)FriendsDataGrid.SelectedItems[0];
                CurrentConnection.Instance.thiscmd.CommandText = "UPDATE yelp_users " +
                            "set ufriends = " +
                            "(Select array_remove" +
                            "((Select ufriends from Yelp_users " +
                            "Where uid = '" + CurrentUser.Instance.Thisuser.getID() + "')," +
                            "'" + selectedfriend.getID().ToString() + "'))";
                UpdateRemoveFriend();

            }
        }

        private void ShowLoggedInCheckinsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckForLoggedIn("business")) return;
            if (CurrentBusiness.Instance.Thisbusiness != null)
            {

                CurrentConnection.Instance.thiscmd.CommandText = "select * From yelp_businesses,yelp_checkins WHERE yelp_checkins.bid = yelp_businesses.bid AND yelp_businesses.bid = '" +
                                            CurrentBusiness.Instance.Thisbusiness.bId + "'";
                using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
                {
                    CheckinsChartPerWeek businessinfopage = new CheckinsChartPerWeek("weekly", CurrentBusiness.Instance.Thisbusiness.bName, reader);

                    businessinfopage.Show();
                }
            }
        }

        private void ShowLoggedInTipsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckForLoggedIn("business")) { return; }
            CurrentConnection.Instance.thiscmd.CommandText = "SELECT uname,tip_text FROM Yelp_tips as t1 , Yelp_users as t2 , Yelp_businesses as t3 " +
                    "WHERE t1.uid = t2.uid and t1.bid = t3.bid and t3.bid = '" +
                    CurrentBusiness.Instance.Thisbusiness.bId + "'";
            using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
            {
                Window2 businessinfopage = new Window2();
                businessinfopage.PopulateFromReader("Tips", CurrentBusiness.Instance.Thisbusiness.bName, reader);
                businessinfopage.Show();
            }
        }

        private void ShowLoggedInBusinessStatistics_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckForLoggedIn("business")) return;

            CurrentConnection.Instance.thiscmd.CommandText = "SELECT * FROM Yelp_businesses WHERE bid = '" +
                         CurrentBusiness.Instance.Thisbusiness.bId + "'";
            using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
            {
                Window1 businessinfopage = new Window1();
                businessinfopage.PopulatefromBusiness(reader);
                businessinfopage.Show();
            }
        }

        private void ShowLoggedInCityStatistics_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckForLoggedIn("business")) return;


            CurrentConnection.Instance.thiscmd.CommandText = "Select sub.zip, count(yelp_businesses.bid) FROM (SELECT Distinct Right(baddress, 5) as zip, bid FROM yelp_businesses Where bcity = '" + CurrentBusiness.Instance.Thisbusiness.bCity + "') " +
                        "as sub, yelp_businesses WHERE sub.bid = yelp_businesses.bid GROUP BY sub.zip;";
            using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
            {
                CheckinsChartPerWeek businessinfopage = new CheckinsChartPerWeek("zips", CurrentBusiness.Instance.Thisbusiness.bCity, reader);
                businessinfopage.Show();
            }
        }
        private void ClearPinsbutton_Click(object sender, RoutedEventArgs e)
        {
            BusinessMap.Children.Clear();
        }

        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            StateComboBox.SelectedItem = null;
            CitiesListBox.Items.Clear();
            ZipCodesListBox.Items.Clear();
            CategoriesListBox.Items.Clear();
            FilterCategoriesListBox.Items.Clear();
            BusinessMap.Children.Clear();
            //DayOfTheWeekComboBox.SelectedItem = null;
            //StartTimeComboBox.SelectedItem = null;
            //EndTimeComboBox.SelectedItem = null;

        }
        #endregion

        #region Update
        private void UpdateCurrentUser()
        {
            CurrentConnection.Instance.thiscmd.CommandText = "SELECT * FROM yelp_users WHERE uid = '" + CurrentUser.Instance.Thisuser.getID() + "'";
            using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
            {
                reader.Read();
                CurrentUser.Instance.Thisuser = new User(reader);
            }

        }

        private void UpdateRemoveFriend()
        {
            UpdateCurrentUser();
            PopulateUserInformation(CurrentUser.Instance.Thisuser);
            FriendsDataGrid.Items.Clear();
            TipsOfFriendsDataGrid.Items.Clear();
            PopulateFriends(CurrentUser.Instance.Thisuser);
        }
        #endregion

        #region Text Changed
        private void SetCurrentUserTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CurrentUser.Instance.Thisuser != null)
            {
                if (SetCurrentUserTextBox.Text == CurrentUser.Instance.Thisuser.uName) { return; }
            }
            if (String.IsNullOrEmpty(SetCurrentUserTextBox.Text))
            {
                SelectUserIDListBox.Items.Clear();
                return;

            }
            else
            {
                SelectUserIDListBox.Items.Clear();
                List<string> UserIDs = getUserIDsWithName(SetCurrentUserTextBox.Text);
                foreach (string userID in UserIDs)
                {
                    SelectUserIDListBox.Items.Add(userID);
                }
            }
        }
        private void SetCurrentBusinessTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CurrentBusiness.Instance.Thisbusiness != null)
            {
                if (SetCurrentBusinessTextBox.Text == CurrentBusiness.Instance.Thisbusiness.bName) { return; }
            }
            if (String.IsNullOrEmpty(SetCurrentBusinessTextBox.Text))
            {
                SelectBusinessIDListBox.Items.Clear();
                return;

            }
            else
            {
                SelectBusinessIDListBox.Items.Clear();
                List<string> BusinessIDs = getBusinessIDsWithName(SetCurrentBusinessTextBox.Text);
                foreach (string businessID in BusinessIDs)
                {
                    SelectBusinessIDListBox.Items.Add(businessID);
                }
            }
        }

        #endregion

        #region Get Ids
        private List<string> getUserIDsWithName(string name)
        {
            List<string> tempUserIDList = new List<string>();
            CurrentConnection.Instance.thiscmd.CommandText = "SELECT uid FROM yelp_users WHERE uname like '" + ScrubString(name) + "%'";
            using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
            {
                while (reader.Read()) tempUserIDList.Add(reader.GetString(0));
            }
            return tempUserIDList;
        }

        private List<string> getBusinessIDsWithName(string name)
        {
            List<string> tempUserIDList = new List<string>();
            CurrentConnection.Instance.thiscmd.CommandText = "SELECT bid FROM yelp_businesses WHERE bname like '" + ScrubString(name) + "%'";
            using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
            {
                while (reader.Read()) tempUserIDList.Add(reader.GetString(0));
            }
            return tempUserIDList;
        }
        #endregion

        #region selection changed
        private void BusinessesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BusinessMap.Children.Clear();
            CheckinSelectedBusinessTextbox.Text = "";
            if (BusinessesDataGrid.Items.IsEmpty != true)
            {
                Business selectedbusiness = (Business)BusinessesDataGrid.SelectedItem;
                AddPinToMapFromBusiness(selectedbusiness, 17);
                CheckinSelectedBusinessTextbox.Text = selectedbusiness.bName;
            }
        }

        private void TipsOfFriendsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            return;
        }

        private void SelectUserIDListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            return;
        }

        private void StateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StateComboBox.SelectedItem == null) { return; }
            BusinessesDataGrid.Items.Clear();
            CitiesListBox.Items.Clear();
            ZipCodesListBox.Items.Clear();
            CurrentConnection.Instance.Command = "SELECT distinct bcity FROM yelp_businesses WHERE bstate='" + StateComboBox.SelectedItem.ToString() + "' ORDER BY bcity";
            using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    CitiesListBox.Items.Add(reader.GetString(0));
                }
            }
        }

        private void CitiesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BusinessesDataGrid.Items.Clear();
            ZipCodesListBox.Items.Clear();
            if (CitiesListBox.Items.IsEmpty) return;
            HashSet<String> zipSet = new HashSet<string>();
            CurrentConnection.Instance.thiscmd.CommandText = "SELECT baddress FROM yelp_businesses WHERE bstate='" + StateComboBox.SelectedItem.ToString() + "' AND " +
                        "bcity = '" + CitiesListBox.SelectedItem.ToString() + "'";
            using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    String address = reader.GetString(0);
                    zipSet.Add(address.Substring(address.Length - 5));
                }
            }
            foreach (string zip in zipSet)
            {
                ZipCodesListBox.Items.Add(zip);
            }
        }

        private void ZipCodesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BusinessesDataGrid.Items.Clear();
            CategoriesListBox.Items.Clear();
            if (ZipCodesListBox.Items.IsEmpty) return;
            HashSet<String> Categories = new HashSet<string>();
            CurrentConnection.Instance.thiscmd.CommandText = "SELECT bcats FROM yelp_businesses WHERE bstate='" + StateComboBox.SelectedItem.ToString() + "' AND " +
                        "bcity = '" + CitiesListBox.SelectedItem.ToString() + "' AND baddress LIKE '%" + ZipCodesListBox.SelectedItem.ToString() + "'";
            using (var reader = CurrentConnection.Instance.thiscmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    String[] DBcategories = reader["bcats"] as String[];
                    foreach (string cat in DBcategories)
                    {
                        Categories.Add(cat);
                    }
                }
            }
            foreach (string cat in Categories)
            {
                CategoriesListBox.Items.Add(cat);
            }
        }

        //private void DayOfTheWeekComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (DayOfTheWeekComboBox.SelectedItem == null) return;
        //    ValidatedTimeRange();
        //}

        //private void StartTimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (StartTimeComboBox.SelectedItem == null) return;
        //    ValidatedTimeRange();
        //}

        //private void EndTimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (EndTimeComboBox.SelectedItem == null) return;
        //    ValidatedTimeRange();
        //}

        private void SelectBusinessIDListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            return;
        }

        private void TipsOfBusinessDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            return;
        }


        #endregion

        #region Validation Functions
        //private bool ValidatedTimeRange()
        //{
        //    if (DayOfTheWeekComboBox.SelectedItem != null && StartTimeComboBox.SelectedItem != null && EndTimeComboBox.SelectedItem != null)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        private bool CheckForLoggedIn(string type)
        {
            switch (type)
            {
                case ("business"):
                    if (CurrentBusiness.Instance.Thisbusiness == null)
                    {
                        MessageBox.Show("Please Login To a Business", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error); return false;
                    }
                    break;
                case ("user"):
                    if (CurrentUser.Instance.Thisuser == null)
                    {
                        MessageBox.Show("Please Login To a User", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error); return false;
                    }
                    break;

            }
            return true;
        }

        #endregion

        #region Mouse Events
        private void SetCurrentUserTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (SetCurrentUserTextBox.Text == "Enter UserName")
            {
                SetCurrentUserTextBox.Text = "";
            }
        }

        private void SetCurrentUserTextBox_MouseLeave(object sender, MouseEventArgs e)
        {

            if (SetCurrentUserTextBox.Text == "" && SetCurrentUserTextBox.IsSelectionActive == false)
            {
                SetCurrentUserTextBox.Text = "Enter UserName";
            }
        }

        private void SelectUserIDListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Select_User_Button_Click(sender,e);
            return;
        }







        private void SetCurrentBusinessTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (SetCurrentBusinessTextBox.Text == "Enter Business Name")
            {
                SetCurrentBusinessTextBox.Text = "";
            }
        }

        private void SetCurrentBusinessTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (SetCurrentBusinessTextBox.Text == "" && SetCurrentBusinessTextBox.IsSelectionActive == false)
            {
                SetCurrentBusinessTextBox.Text = "Enter Business Name";
            }
        }


        private void SelectBusinessIDListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            return;
        }
        #endregion

        /// <summary>
        /// Not Necessary at this point but keeping around in case we need to revert
        /// </summary>

        #region Various Functions
        private string ScrubString(string str)
        {
            return str.Replace("'", "''");
        }
        private void FilterDatagridByTime(string day, string startTime, string endTime)
        {
            throw new NotImplementedException();
        }

        private void KeyPressHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (true == String.IsNullOrEmpty(SetCurrentUserTextBox.Text))
                {
                    return;
                }
                else
                {
                    SelectUserIDListBox.Items.Clear();
                    List<string> UserIDs = getUserIDsWithName(SetCurrentUserTextBox.Text);
                    foreach (string userID in UserIDs)
                    {
                        SelectUserIDListBox.Items.Add(userID);
                    }
                }
            }
        }


        public class ThreadConnection
        {
            public NpgsqlCommand cmd { get; set; }
            public NpgsqlConnection conn { get; set; }

        }

        
        #endregion

        /// <summary>
        /// Business Search by Area
        /// </summary>
        
    }
}

