<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListarPedido.aspx.cs" Inherits="DMINVENTARIO.Views.ListarPedido" %>
<%@ Register assembly="DevExpress.Web.v18.1, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<br />
    <h2> LISTADO DE PEDIDOS</h2>
     <div class="row">
            <div class="form-group">
                <div class="col-sm-4">
                    <dx:ASPxGridView ID="GridListaPedido" runat="server" AutoGenerateColumns="False" Theme="Office2010Black" OnInit="GridListaPedido_Init" KeyFieldName="CONSECUTIVO" OnCustomButtonCallback="GridListaPedido_CustomButtonCallback" OnRowDeleting="GridListaPedido_RowDeleting">
                    <SettingsAdaptivity AdaptiveColumnPosition="Left" AdaptiveDetailColumnCount="3" AdaptivityMode="HideDataCells" AllowOnlyOneAdaptiveDetailExpanded="True">
                    <AdaptiveDetailLayoutProperties ColCount="1">
						<SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit">
						</SettingsAdaptivity>
						</AdaptiveDetailLayoutProperties>
                    </SettingsAdaptivity>
                        <SettingsEditing NewItemRowPosition="Bottom" Mode="PopupEditForm">
                        </SettingsEditing>
                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" ConfirmDelete="True" />
                        <SettingsDataSecurity AllowEdit="False" />
                    <EditFormLayoutProperties ColCount="1">
						<SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit">
						</SettingsAdaptivity>
						</EditFormLayoutProperties>
                        <Columns>

                            <dx:GridViewCommandColumn ShowInCustomizationForm="true" VisibleIndex="0" ShowClearFilterButton="true" ShowDeleteButton="True">
                            </dx:GridViewCommandColumn>
                            <dx:GridViewCommandColumn ButtonRenderMode="Image" ButtonType="Image" VisibleIndex="1">
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton ID="Edit" Image-ToolTip="Editar Pedido">
                                        <Image AlternateText="Editar" IconID="businessobjects_boreport2_16x16">

                                        </Image>
                                    </dx:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dx:GridViewCommandColumn>
                           
                            <dx:GridViewDataTextColumn FieldName="ID" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CONSECUTIVO" VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="BODEGA" VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="NombreCliente" Caption="CLIENTE" VisibleIndex="5">
                            </dx:GridViewDataTextColumn>
							<dx:GridViewDataTextColumn FieldName="MONEDA" VisibleIndex="6">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="USUARIO" VisibleIndex="7">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="FECHA_CREACION" VisibleIndex="8">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="USUARIO_MODIFICA" VisibleIndex="9">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="FECHA_MODIFICA" VisibleIndex="10">
                            </dx:GridViewDataTextColumn>
							<dx:GridViewDataTextColumn FieldName="ESTADO" VisibleIndex="11">
                            </dx:GridViewDataTextColumn>
							<dx:GridViewDataTextColumn FieldName="TOTAL_LIBRAS" Caption="TOTAL LIBRAS" VisibleIndex="12">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="TOTAL_PEDIDO" Caption="TOTAL PEDIDO" VisibleIndex="13">
                            </dx:GridViewDataTextColumn>
							<dx:GridViewDataTextColumn FieldName="COMPANI" Caption="EMPRESA" VisibleIndex="14">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
                </div>
            </div>
        </div>
</asp:Content>
