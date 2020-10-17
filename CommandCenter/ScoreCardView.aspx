<%@ Page Title="" Language="C#" MasterPageFile="~/BitcoinCommand.Master" AutoEventWireup="true" CodeBehind="ScoreCardView.aspx.cs" Inherits="CommandCenter.ScoreCardView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center><h2>Score Card View</h2></center>
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="span1"></div>
            <div class="span11">
                <div class="row-fluid">
                    <div class="span2">

                    </div>
                    <div class="span1">
                        <h2>Min</h2>
                    </div>
                    <div class="span1">
                        <h2>Hour</h2>
                    </div>
                    <div class="span1">
                        <h2>2Hour</h2>
                    </div>
                    <div class="span1">
                        <h2>Day</h2>
                    </div>
                    <div class="span1">
                        <h2>2Day</h2>
                    </div>
                    <div class="span1">
                        <h2>Week</h2>
                    </div>
                </div>
                <div class="row-fluid show-grid">
                    <div class="span2">
                        <b>Total Net Profit</b>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTNPMin" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTNPHour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTNP2Hour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTNPDay" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTNP2Day" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTNPWeek" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row-fluid show-grid">
                    <div class="span2">
                        <b>Number of Trades</b>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblNumberOfTradesMin" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblNumberOfTradesHour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblNumberOfTrades2Hour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblNumberOfTradesDay" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblNumberOfTrades2Day" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblNumberOfTradesWeek" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row-fluid show-grid">
                    <div class="span2">
                        Average Profit Per Trade
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTotalAverageProfitPerTradeMin" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTotalAverageProfitPerTradeHour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTotalAverageProfitPerTrade2Hour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTotalAverageProfitPerTradeDay" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTotalAverageProfitPerTrade2Day" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTotalAverageProfitPerTradeWeek" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row-fluid show-grid">
                    <div class="span2">
                        Average Profit % Per Trade
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAveragePrcntProfitPerTradeMin" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAveragePrcntProfitPerTradeHour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAveragePrcntProfitPerTrade2Hour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAveragePrcntProfitPerTradeDay" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAveragePrcntProfitPerTrade2Day" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAveragePrcntProfitPerTradeWeek" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row-fluid show-grid">
                    <div class="span2">
                        <b>Profitable Trades</b>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblProfTradesMin" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblProfTradesHour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblProfTrades2Hour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblProfTradesDay" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblProfTrades2Day" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblProfTradesWeek" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row-fluid show-grid">
                    <div class="span2">
                        Win Rate
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblWinRateMin" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblWinRateHour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblWinRate2Hour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblWinRateDay" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblWinRate2Day" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblWinRateWeek" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row-fluid show-grid">
                    <div class="span2">
                        Total Gross Profit
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTotalGrossProfitMin" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTotalGrossProfitHour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTotalGrossProfit2Hour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTotalGrossProfitDay" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTotalGrossProfit2Day" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTotalGrossProfitWeek" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row-fluid show-grid">
                    <div class="span2">
                        Average Profit Per Trade
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageProfitPerTradeMin" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageProfitPerTradeHour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageProfitPerTrade2Hour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageProfitPerTradeDay" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageProfitPerTrade2Day" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageProfitPerTradeWeek" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row-fluid show-grid">
                    <div class="span2">
                        Average Profit % Per Trade
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageProfitPrcntPerTradeMin" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageProfitPrcntPerTradeHour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageProfitPrcntPerTrade2Hour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageProfitPrcntPerTradeDay" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageProfitPrcntPerTrade2Day" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageProfitPrcntPerTradeWeek" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row-fluid show-grid">
                    <div class="span2">
                        Max Consecutive Winners
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaxConsecutiveWinnersMin" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaxConsecutiveWinnersHour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaxConsecutiveWinners2Hour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaxConsecutiveWinnersDay" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaxConsecutiveWinners2Day" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaxConsecutiveWinnersWeek" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row-fluid show-grid">
                    <div class="span2">
                        <b>Unprofitable Trades</b>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblUnprofitableTradesMin" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblUnprofitableTradesHour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblUnprofitableTrades2Hour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblUnprofitableTradesDay" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblUnprofitableTrades2Day" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblUnprofitableTradesWeek" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row-fluid show-grid">
                    <div class="span2">
                        Loss Rate
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblLossRateMin" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblLossRateHour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblLossRate2Hour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblLossRateDay" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblLossRate2Day" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblLossRateWeek" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row-fluid show-grid">
                    <div class="span2">
                        Total Gross Loss
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTotalGrossLossMin" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTotalGrossLossHour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTotalGrossLoss2Hour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTotalGrossLossDay" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTotalGrossLoss2Day" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblTotalGrossLossWeek" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row-fluid show-grid">
                    <div class="span2">
                        Average Loss Per Trade
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageLossPerTradeMin" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageLossPerTradeHour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageLossPerTrade2Hour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageLossPerTradeDay" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageLossPerTrade2Day" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageLossPerTradeWeek" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row-fluid show-grid">
                    <div class="span2">
                        Average Loss % Per Trade
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageLossPrcntPerTradeMin" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageLossPrcntPerTradeHour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageLossPrcntPerTrade2Hour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageLossPrcntPerTradeDay" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageLossPrcntPerTrade2Day" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblAverageLossPrcntPerTradeWeek" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row-fluid show-grid">
                    <div class="span2">
                        Max Consecutive Losses
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaxConsecutiveLossesMin" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaxConsecutiveLossesHour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaxConsecutiveLosses2Hour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaxConsecutiveLossesDay" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaxConsecutiveLosses2Day" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaxConsecutiveLossesWeek" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row-fluid show-grid">
                    <div class="span2">
                        Maximum Drawdown
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaxDrawDownMin" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaxDrawDownHour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaxDrawDown2Hour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaxDrawDownDay" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaxDrawDown2Day" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaxDrawDownWeek" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row-fluid show-grid">
                    <div class="span2">
                        Maximum Drawdown Date
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaximumDrawdownDateMin" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaximumDrawdownDateHour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaximumDrawdownDate2Hour" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaximumDrawdownDateDay" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaximumDrawdownDate2Day" runat="server"></asp:Label>
                    </div>
                    <div class="span1">
                        <asp:Label ID="lblMaximumDrawdownDateWeek" runat="server"></asp:Label>
                    </div>
                </div>
                
            </div>
        </div>
    </div>
</asp:Content>
