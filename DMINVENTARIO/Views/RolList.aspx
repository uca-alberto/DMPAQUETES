<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RolList.aspx.cs" Inherits="DMINVENTARIO.Views.RolList" %>
<%@ Register assembly="DevExpress.Web.v18.1, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<h2>ADMINISTRACIÓN DE ROLES</h2>
	<br />
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" EnableTheming="True" Theme="Office2010Black" KeyFieldName="ID_ROL" OnRowUpdating="ASPxGridView1_RowUpdating" OnRowDeleting="ASPxGridView1_RowDeleting" OnRowInserting="ASPxGridView1_RowInserting">
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
		<dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="ID_ROL" Name="Id">
		</dx:GridViewColumnLayoutItem>
		<dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="ROL" Name="Rol">
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
            <dx:GridViewDataTextColumn FieldName="ID_ROL" Name="ID" VisibleIndex="1" ReadOnly="true" ShowInCustomizationForm="False">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ROL" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
			<dx:GridViewDataTextColumn FieldName="FECHA_CREACION" VisibleIndex="3" Caption="FECH CREACION">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="FECHA_MODIFICACION" VisibleIndex="4" Caption="FECHA MODIFICACION">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
</asp:Content>
