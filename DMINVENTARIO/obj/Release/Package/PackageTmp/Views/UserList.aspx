<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="DMINVENTARIO.Views.UserList" %>
<%@ Register assembly="DevExpress.Web.v18.1, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<h2>ADMINISTRACIÓN DE USUARIOS</h2>
	<br />
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" EnableTheming="True" Theme="Office2010Black" KeyFieldName="ID_USUARIO" OnRowUpdating="ASPxGridView1_RowUpdating" OnRowDeleting="ASPxGridView1_RowDeleting" OnRowInserting="ASPxGridView1_RowInserting">
		<SettingsAdaptivity AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="True">
			<AdaptiveDetailLayoutProperties ColCount="2" AlignItemCaptionsInAllGroups="True">
				<SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit">
				</SettingsAdaptivity>
				<SettingsItemCaptions AllowWrapCaption="True" />
			</AdaptiveDetailLayoutProperties>
		</SettingsAdaptivity>

		<SettingsBehavior ConfirmDelete="True" />

<EditFormLayoutProperties ColCount="2">
    <Items>
		<dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="ID_USUARIO" Name="Id" Caption="ID">
		</dx:GridViewColumnLayoutItem>
		<dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="USUARIO" Name="Usuario" Caption="USUARIO">
		</dx:GridViewColumnLayoutItem>
		<dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="PASS" Name="Contraseña" Caption="CONTRASEÑA">
		</dx:GridViewColumnLayoutItem>
		<dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="ID_ROL" Name="Rol" Caption="ROLID">
		</dx:GridViewColumnLayoutItem>
		<dx:EditModeCommandLayoutItem>
		</dx:EditModeCommandLayoutItem>
	</Items>
    <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit">
        <GridSettings StretchLastItem="False">
        </GridSettings>
    </SettingsAdaptivity>
        </EditFormLayoutProperties>
        <Columns>
            <dx:GridViewCommandColumn ButtonRenderMode="Button" ButtonType="Button" ShowCancelButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowUpdateButton="True" VisibleIndex="0" ShowDeleteButton="True" ShowNewButtonInHeader="True">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="ID_USUARIO" Name="ID" VisibleIndex="1" ReadOnly="true" ShowInCustomizationForm="False" Caption="ID">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="USUARIO" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PASS" VisibleIndex="3" Caption="CONTRASEÑA">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ID_ROL" VisibleIndex="4" Caption="ROLID">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
</asp:Content>
