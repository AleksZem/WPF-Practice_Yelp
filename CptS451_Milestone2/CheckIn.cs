using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace milestone4
{
    class CheckinHolder
    {
        public string day { get; private set; }
        public int morning { get; private set; }
        public int noon { get; private set; }
        public int evening { get; private set; }
        public int night { get; set; }
        public CheckinHolder(string column, int[] data)
        {
            day = column;
            if (data.Length == 4)
            {
                morning = data[0];
                noon = data[1];
                evening = data[2];
                night = data[3];
            }
        }

    }
    class CheckIn
    {
        public CheckIn(string bid, int[] monday, int[] tuesday, int[] wednesday, int[] thursday, int[] friday, int[] saturday, int[] sunday)
        {
            this.bid = bid;
            this.monday = monday;
            this.tuesday = tuesday;
            this.wednesday = wednesday;
            this.thursday = thursday;
            this.friday = friday;
            this.saturday = saturday;
            this.sunday = sunday;
        }

        public CheckIn(NpgsqlDataReader DBReader, bool internalRead = false)
        {
            if (internalRead)
            {
                DBReader.Read();
            }
            bid = DBReader["bid"] as string;
            monday = DBReader["monday"] as int[];
            tuesday = DBReader["tuesday"] as int[];
            wednesday = DBReader["wednesday"] as int[];
            thursday = DBReader["thursday"] as int[];
            friday = DBReader["friday"] as int[];
            saturday = DBReader["saturday"] as int[];
            sunday = DBReader["sunday"] as int[];
        }

        public string bid { get; private set; }
        public int[] monday { get; private set; }
        public int[] tuesday { get; private set; }
        public int[] wednesday { get; private set; }
        public int[] thursday { get; private set; }
        public int[] friday { get; private set; }
        public int[] saturday { get; private set; }
        public int[] sunday { get; private set; }

    }
}
