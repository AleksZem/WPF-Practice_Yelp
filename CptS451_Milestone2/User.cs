using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace milestone4
{
    class User
    {
        //private string uName;
        //private string uID;
        //private string[] uFriends;
        //private double uAverageStars;
        //private int uReviewCount;
        //private string uYelpSince;
        //private int uNumFans;
        //private string[,] uVotes;
        //private int[] uYearsElite;
        //private string[] uCompliments;
        //private string uType;

        public string uName { get; private set; }
        public string uID { get; private set; }
        public string[] uFriends { get; private set; }
        public double uAverageStars { get; private set; }
        public int uReviewCount { get; private set; }
        public string uYelpSince { get; private set; }
        public int uNumFans { get; private set; }
        public string[,] uVotes { get; private set; }
        public int[] uYearsElite { get; private set; }
        public string[] uCompliments { get; private set; }
        public string uType { get; private set; }
        public int uTotalVotes { get { return int.Parse(this.getVotes()[0,1]) + int.Parse(this.getVotes()[1, 1]) + int.Parse(this.getVotes()[2, 1]); } }

        public User(NpgsqlDataReader DBReader, bool internalRead = false)
        {
            if (internalRead)
            {
                DBReader.Read();
            }
            uID = DBReader.GetString(0);
            uName = DBReader.GetString(1);
            uType = DBReader.GetString(2);
            uFriends = DBReader["ufriends"] as String[];
            uAverageStars = (Double)DBReader["uavgstar"];
            uReviewCount = int.Parse(DBReader.GetString(5));
            uYelpSince = DBReader.GetString(6);
            uNumFans = int.Parse(DBReader.GetString(7));
            uVotes = DBReader["uvotes"] as String[,];
            uYearsElite = DBReader["uelite"] as int[];
            uCompliments = DBReader["ucompliments"] as string[];
        }

        public User(string ID, string Name, string[] Friends, double AverageStars, int ReviewCount, string YelpSince, 
            int NumFans, string[,] Votes, int[] YearsElite, string[] Compliments, string userType) {
            uName = Name;
            uID = ID;
            uFriends = Friends;
            uAverageStars = AverageStars;
            uReviewCount = ReviewCount;
            uYelpSince = YelpSince;
            uNumFans = NumFans;
            uVotes = Votes;
            uYearsElite = YearsElite;
            uCompliments = Compliments;
            uType = userType;
        }
        public string getName() { return uName; }
        public string getID() { return uID; }
        public string[] getFriends() { return uFriends; }
        public double getAverageStars() { return uAverageStars; }
        public int getReviewCount() { return uReviewCount; }
        public int getNumFans() { return uNumFans; }
        public string[,] getVotes()
        {
            if (uVotes != null)
            {
                return uVotes;
            }
            else
            {
                return new String[,] { {"","0"}, { "", "0" }, { "", "0" } };
            }
           
        }
        public int[] getYearsElite() { return uYearsElite; }
        public string[] getCompliments() { return uCompliments; }
        public string getType() { return uType; }
        public string getYelpSince() { return uYelpSince; }
    }
}
