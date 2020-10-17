using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinTrader.Library.Business
{
    public enum TradeAction
    {
        Sell,
        Buy,
        Hold
    }

    public static class ActionUtil
    {
            public static TradeAction GetTradeActionBy(string group)
            {
                Dictionary<string, TradeAction> tradeActionDic = new Dictionary<string, TradeAction>();
                tradeActionDic.Add("Sell", TradeAction.Sell);
                tradeActionDic.Add("Buy", TradeAction.Buy);
                tradeActionDic.Add("Hold", TradeAction.Hold);

                return tradeActionDic[group.Trim()];
            }
        
    }
}
