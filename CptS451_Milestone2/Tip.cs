using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace milestone4
{
    class Tip
    {
        public Tip(string uID, string tip_Text, string bid, string tip_date, int tip_likes, int tip_id)
        {
            this.uID = uID;
            this.tip_Text = tip_Text;
            this.bid = bid;
            this.tip_date = tip_date;
            this.tip_likes = tip_likes;
            this.tip_id = tip_id;
        }

        public Tip(NpgsqlDataReader DBReader, bool internalRead = false)
        {
            if (internalRead)
            {
                DBReader.Read();
            }
            uID = DBReader["uid"] as string;
            tip_Text = DBReader["tip_text"] as string;
            bid = DBReader["bid"] as string;
            tip_date = DBReader["tip_date"] as string;
            tip_likes = (int)DBReader["tip_likes"];
            tip_id = (int)DBReader["tip_id"];
        }

        public string uID { get; private set; }
        public string tip_Text { get; private set; }
        public string bid { get; private set; }
        public string tip_date { get; private set; }
        public int tip_likes { get; private set; }
        public int tip_id { get; private set; }
    }

    class TipHolder
    {
        public string Uname
        {
            get; set;
        }
        public string Tip
        {
            get; set;
        }
    }
}
