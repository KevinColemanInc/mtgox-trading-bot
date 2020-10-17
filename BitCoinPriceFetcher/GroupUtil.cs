using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinTrader.Library.Business
{
    public enum GroupBy
    {        
        fifteenmin = 60*15,
        halfhour = 60*30,
        hour = 60 * 60,
        sixhour = 60*60*6,
        twelvehour = 60*60*12,
        day = 60 * 60 * 24,
        doublehour = 60 * 60 * 2,
        doubleday = 60 * 60 * 24 * 2,
        week = 60 * 60 * 24 * 7,
    }

    public static class GroupUtil
    {
        public static GroupBy GetGroupBy(string group)
        {
            Dictionary<string, GroupBy> groupDic = new Dictionary<string, GroupBy>();
            //groupDic.Add("second", GroupBy.second);
            groupDic.Add("halfhour", GroupBy.halfhour);
            groupDic.Add("hour", GroupBy.hour);
            groupDic.Add("day", GroupBy.day);
            groupDic.Add("doublehour", GroupBy.doublehour);
            groupDic.Add("doubleday", GroupBy.doubleday);
            groupDic.Add("week", GroupBy.week);
            groupDic.Add("fifteenmin", GroupBy.fifteenmin);
            groupDic.Add("sixhour", GroupBy.sixhour);
            groupDic.Add("twelvehour", GroupBy.twelvehour);

            return groupDic[group.Trim()];
        }
    }
}
