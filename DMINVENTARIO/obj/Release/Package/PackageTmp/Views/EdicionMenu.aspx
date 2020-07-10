<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EdicionMenu.aspx.cs" Inherits="DMINVENTARIO.Views.EdicionMenu" %>
<%@ Register assembly="DevExpress.Web.v18.1, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<script>
		function ROL(s, e) {
			debugger;
			document.getElementById("MainContent_ObtenerRolMenu").click();
		}
		function SUBMENU(s, e) {
			debugger;
			document.getElementById("MainContent_BtnObtenerSubmenu").click();
		}
	</script>
	<br />
	<h1>Menu Rol</h1>
	<br />
	<div class="row">
            <div class="form-group">
                <label for="lblDepot" class="control-label col-sm-1">Rol</label>
                <div class="col-sm-3">
					<dx:ASPxGridLookup ID="ASPxGridLookupRol" runat="server" OnInit="ASPxGridLookupRol_Init" CssClass="form-control" AutoGenerateColumns="False" KeyFieldName="ID_ROL" Theme="Office2010Black">
					<GridViewProperties>
					<SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True"></SettingsBehavior>

					<EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
					</GridViewProperties>
						<Columns>
							<dx:GridViewDataTextColumn Caption="ID" FieldName="ID_ROL" VisibleIndex="0">
							</dx:GridViewDataTextColumn>
							<dx:GridViewDataTextColumn Caption="DESCRIPCION" FieldName="ROL" VisibleIndex="1">
							</dx:GridViewDataTextColumn>
						</Columns>
						<ClientSideEvents ValueChanged="ROL" />
					</dx:ASPxGridLookup>
					<asp:Button runat="server" style="display:none" ID="ObtenerRolMenu" OnClick="ObtenerRolMenu_Click" />
				</div>
                <label for="lblDepot" class="control-label col-sm-1">Menu</label>
				<div class="col-sm-3">
					<dx:ASPxGridLookup ID="ASPxGridLookupMenu" runat="server" OnInit="ASPxGridLookupMenu_Init" AutoGenerateColumns="False" KeyFieldName="ID_MENU" CssClass="form-control" Theme="Office2010Black">
					<GridViewProperties>
					<SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True"></SettingsBehavior>

					<EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
					</GridViewProperties>
						<Columns>
							<dx:GridViewDataTextColumn Caption="ID" FieldName="ID_MENU" VisibleIndex="0">
							</dx:GridViewDataTextColumn>
							<dx:GridViewDataTextColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" VisibleIndex="1">
							</dx:GridViewDataTextColumn>
						</Columns>
						<%--<ClientSideEvents ValueChanged="SUBMENU" />--%>
					</dx:ASPxGridLookup>
				</div>
                <%--<label for="lblDepartment" class="control-label col-sm-1 ">Submenu</label>
                <div class="col-sm-3">
					<dx:ASPxGridLookup ID="ASPxGridLookupSubMenu" runat="server" OnInit="ASPxGridLookupSubMenu_Init" KeyFieldName="ID_SUBMENU" CssClass="form-control" Theme="Office2010Black">
					<GridViewProperties>
					<SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True"></SettingsBehavior>

					<EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
					</GridViewProperties>
						<Columns>
							<dx:GridViewDataTextColumn Caption="ID" FieldName="ID_SUBMENU" VisibleIndex="0">
							</dx:GridViewDataTextColumn>
							<dx:GridViewDataTextColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" VisibleIndex="1">
							</dx:GridViewDataTextColumn>
						</Columns>
					</dx:ASPxGridLookup>
                </div>--%>
				<div class="col-sm-3">  
					<%--<asp:Button runat="server" OnClick="BtnObtenerSubmenu_Click" ID="BtnObtenerSubmenu"  CssClass="btn btn-info" style="display:none"/>--%>
					<asp:Button runat="server" OnClick="BtnAgregar_Click" ID="BtnAgregar" Text="Agregar Menu" CssClass="btn btn-info" />
				</div>
            </div>
     </div>
	<br />
	<div class="row">
		<div class="form-group">
			<h4 class=""> MENUS ASIGNADOS</h4>
			<div class="col-sm-2">
				<dx:ASPxGridView ID="ASPxGridViewMenu" runat="server" AutoGenerateColumns="False" KeyFieldName="IdMenu" OnInit="ASPxGridViewMenu_Init" Theme="Office2010Black" OnRowDeleting="ASPxGridViewMenu_RowDeleting">
				<SettingsAdaptivity>
				<AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
				</SettingsAdaptivity>

					<SettingsBehavior ConfirmDelete="True" />

				<EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
					<Columns>
						<dx:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="0">
						</dx:GridViewCommandColumn>
						<dx:GridViewDataTextColumn Caption="ID" FieldName="IdMenu" VisibleIndex="1">
						</dx:GridViewDataTextColumn>
						<dx:GridViewDataTextColumn Caption="Menu Asignado" FieldName="Descripcion" VisibleIndex="2">
						</dx:GridViewDataTextColumn>
					</Columns>
				</dx:ASPxGridView>

			</div>
		</div>
	</div>
</asp:Content>
