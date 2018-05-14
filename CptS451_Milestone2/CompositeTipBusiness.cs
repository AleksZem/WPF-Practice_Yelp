using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace milestone4
{
    class CompositeTipBusiness
    {
        private Tip tip;
        private DBBusinessElement business;
        private User tipUser;
        public string tipUserName { get; private set; }
        public string businessName { get; private set; }
        public string businessCity { get; private set; }
        public string businessState { get; private set; }
        public string tipText { get; private set; }
        public string tipDate { get; private set; }
        public int tipLikes { get; private set; }

        public CompositeTipBusiness(User tipUser, Tip tip, DBBusinessElement business)
        {
            this.tip = tip;
            this.business = business;
            tipUserName = tipUser.uName;
            businessName = business.bName;
            businessCity = business.bCity;
            businessState = business.bState;
            tipText = tip.tip_Text;
        }


        public CompositeTipBusiness(NpgsqlDataReader DBReader, bool internalRead = false)
        {
            if (internalRead)
            {
                DBReader.Read();
            }
            tipUserName = DBReader["uName"] as string;
            tipText = DBReader["tip_text"] as string;
            businessName = DBReader["bname"] as string;
            tipDate = DBReader["tip_date"] as string;
            tipLikes = (int)DBReader["tip_likes"];
        }
    }
}
