<%@ Page Title="" Language="C#" MasterPageFile="~/BitcoinCommand.Master" AutoEventWireup="true" CodeBehind="AdviceSummary.aspx.cs" Inherits="CommandCenter.AdviceSummary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Repeater ID="rpAdviceList" Runat="server" EnableViewState="False">
	<HeaderTemplate>
		<div class="row-fluid show-grid">
			<div class="span3">
				Algorithm
			</div>
            <div class="span1">
				Group
			</div>
            <div class="span3">
				Time
			</div>
       		<div class="span1">
				Action
			</div>
			<div class="span1">
				Price
			</div>
			<div class="span1">
				Confidence
			</div>
		</div>
	</HeaderTemplate>
    <ItemTemplate>
		<div class="row-fluid show-grid">
			<div class="span3">
				<%# DataBinder.Eval(Container.DataItem, "AlgorithmName") %>
			</div>
			<div class="span1">
				<%# DataBinder.Eval(Container.DataItem, "Group") %>
			</div>
			<div class="span3">
				<%# DataBinder.Eval(Container.DataItem, "DTime") %>
			</div>
			<div class="span1">
				<%# DataBinder.Eval(Container.DataItem, "Action") %>
			</div>
			<div class="span1">
				<%# DataBinder.Eval(Container.DataItem, "Price", "{0:N5}") %>
			</div>
			<div class="span1">
				<%# DataBinder.Eval(Container.DataItem, "Confidence", "{0:N5}") %>
			</div>
		</div>
    </ItemTemplate>
</asp:Repeater>
</asp:Content>
