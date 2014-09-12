<%@ Page Title="" Language="C#" MasterPageFile="~/SitePopup.Master" AutoEventWireup="true" CodeBehind="ConsultaProducto.aspx.cs" Inherits="CRUZDELSUR.UI.Web.GestionCarga.ConsultaProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    Descripcion
    <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Buscar" />
    <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False" 
        onrowcommand="gvProductos_RowCommand" Width="100%">
        <Columns>
            <asp:BoundField DataField="CODIGO_PRODUCTO" HeaderText="ID" />
            <asp:BoundField DataField="NOMBRE" HeaderText="Producto" />
            <asp:BoundField DataField="Precio" HeaderText="Precio" />            
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkSeleccionar" runat="server" 
                        CommandArgument='<%# Eval("CODIGO_PRODUCTO")%>' CommandName="Seleccionar">Seleccionar</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
