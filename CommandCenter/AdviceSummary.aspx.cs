using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BitcoinTrader.Library.Business;

namespace CommandCenter
{
    public partial class AdviceSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Advice> advices = Advice.GetAllRecentAdvices();
            rpAdviceList.DataSource = advices;
            rpAdviceList.DataBind();
        }
    }
}