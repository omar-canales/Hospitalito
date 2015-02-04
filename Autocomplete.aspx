<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Autocomplete.aspx.cs" Inherits="Autocomplete" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <asp:TextBox ID="txtMovie" runat="server" AutoPostBack="True" ></asp:TextBox>
        <asp:AutoCompleteExtender
            ID="AutoCompleteExtender1"
            TargetControlID="txtMovie"
            MinimumPrefixLength="1"
            runat="server" 
            ServiceMethod="GetCompletionList2" 
            UseContextKey="True" />
        <hr />
        <asp:GridView ID="gvPers" runat="server"></asp:GridView>
        <asp:GridView ID="gvSele" runat="server"></asp:GridView>
    </form>
</body>
</html>
