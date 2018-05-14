using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
using System.Threading.Tasks;


namespace milestone4
{
    class CurrentUser
    {
        private static CurrentUser instance;


        private User _thisuser;
        public User Thisuser
        {
            get
            {
                return _thisuser;
            }
            set
            {
                _thisuser = value;
            }
        }
        public static CurrentUser Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CurrentUser();
                }
                return instance;
            }
        }
    }
    class CurrentBusiness
    {
        private static CurrentBusiness instance;


        private DBBusinessElement _thisuser;
        public DBBusinessElement Thisbusiness
        {
            get
            {
                return _thisuser;
            }
            set
            {
                _thisuser = value;
            }
        }
        public static CurrentBusiness Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CurrentBusiness();
                }
                return instance;
            }
        }
    }
    class CurrentConnection
    {
        public CurrentConnection()
        {
            thisconn = new NpgsqlConnection(connectionstring);
            thisconn.Open();
            thiscmd = new NpgsqlCommand();
            thiscmd.Connection = thisconn;
        }
        private static CurrentConnection instance;
        private string connectionstring = "Host=localhost; Username=postgres; Password=password; Database=postgres";
        private NpgsqlConnection _thisconn;
        public NpgsqlConnection thisconn
        {
            get
            {
                return _thisconn;
            }
            set
            {
                _thisconn = value;
            }
        }
        private NpgsqlCommand _thiscmd;
        public NpgsqlCommand thiscmd
        {
            get { return _thiscmd; }
            set
            {
                _thiscmd = value;
            }
        }
        public string Command
        {
            get
            {
                return "The given given sql command";
            }
            set
            {
                if (value != string.Empty)
                {
                    thiscmd.CommandText = value;
                }

            }
        }
        public static CurrentConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CurrentConnection();
                }
                return instance;
            }
        }
    }
    
}
