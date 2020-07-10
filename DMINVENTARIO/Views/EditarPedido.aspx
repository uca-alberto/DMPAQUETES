<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarPedido.aspx.cs" Inherits="DMINVENTARIO.Views.EditarPedido" %>
<%@ Register assembly="DevExpress.Web.v18.1, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<script>
        function Validacion() {
            swal({
                title: "Error",
                text: "El Articulo ya Existe En Este Pedido",
                icon: "warning",
                button: "OK",
            });
    }
    </script>
    <script>
        function Insertar(data) {
            swal({
                title: "Pedido Actualizado",
                text: "Correctamente",
                icon: "success",
                // buttons: true,
                //dangerMode: true,
            })
                .then((willDelete) => {
                    if (willDelete) {
                        location.href = "ListarPedido.aspx";
                    }
                });
        }
    </script>
        <!-------- Alerta de permisos ------>
     <script>
         function Acceso(data) {
             swal({
                 title: "Usted no tiene acceso",
                 text: "restricted access",
                 icon: "error",

             })
                 .then((willDelete) => {
                     if (willDelete) {
                         location.href = "../../Default.aspx";
                     }
                 });
		 }
		 function Lookup_ValueChanged(s, e) {
			 debugger;
			 document.getElementById("MainContent_BtnSugerirPrecio").click();
		 }
    </script>

	<br />
	<asp:Label runat="server" ID="Error3" CssClass="control-label" style="color:red"></asp:Label>
	<br />
	<h1>Actulización de Pedido</h1>
	<br />
        <div class="row">
            <div class="form-group">
                <label for="lblDepot" class="control-label col-sm-1">Consecutivo</label>
                <div class="col-sm-3">
                    <asp:TextBox runat="server" ID="NextConsecutivo" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
                <label for="lblDepartment" class="control-label col-sm-1 ">Cliente</label>
                <div class="col-sm-3">
                    <dx:ASPxGridLookup ID="ASPxGridLookupCliente" required="required" runat="server" CssClass="form-control" OnInit="ASPxGridLookupCliente_Init" AutoGenerateColumns="False" Theme="Office2010Black" KeyFieldName="CLIENTE">
                    <GridViewProperties>
                    <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True"></SettingsBehavior>
                    <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                    </GridViewProperties>
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="CLIENTE" VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="NOMBRE" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
							<dx:GridViewDataTextColumn FieldName="MONEDA_NIVEL" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
							<dx:GridViewDataTextColumn FieldName="NIVEL_PRECIO" VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridLookup>
                </div>
                <label for="lblEmployee" class="control-label col-sm-1">Bodega</label>
                <div class="col-sm-3">
                    <dx:ASPxGridLookup ID="ASPxGridLookupBodega"  required="required" runat="server" CssClass="form-control" OnInit="ASPxGridLookupBodega_Init" AutoGenerateColumns="False" EnableTheming="True" Theme="Office2010Black" KeyFieldName="BODEGA">
                    <GridViewProperties>
                    <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True"></SettingsBehavior>

                    <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                    </GridViewProperties>
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="BODEGA" VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="NOMBRE" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridLookup>
                </div>
                <div class="col-sm-3">
                    <%--<asp:Button runat="server" ID="BtnBuscarProducto" OnClick="BtnBuscarProducto_Click" />--%>
                </div>
            </div>
        </div>
    <br />
     <div class="row">
            <div class="form-group">
                <label for="lblDepot" class="control-label col-sm-1">Articulo</label>
                <div class="col-sm-3">
					 <dx:ASPxGridLookup ID="ASPxGridLookupArticulo" required="required" runat="server" CssClass="form-control" OnInit="ASPxGridLookupArticulo_Init" AutoGenerateColumns="False" EnableTheming="True" Theme="Office2010Black" KeyFieldName="IdArticulo">
                    <GridViewProperties>
                    <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True"></SettingsBehavior>

                    <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                    </GridViewProperties>
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="IdArticulo" VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Descripcion" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                        </Columns>
						 <ClientSideEvents ValueChanged="Lookup_ValueChanged" />
                    </dx:ASPxGridLookup>
				<asp:Button runat="server" style="display:none" Text="Sugerir Precio" CssClass="btn btn-info" ID="BtnSugerirPrecio" OnClick="BtnSugerirPrecio_Click"/>
                </div>
                <label for="lblDepartment" class="control-label col-sm-1 ">Precio</label>
                <div class="col-sm-3">
                    <asp:TextBox runat="server" ID="TxtPrecio" CssClass="form-control" Enabled="false"></asp:TextBox>

                </div>
                <label for="lblEmployee" class="control-label col-sm-1">Cantidad Solicitada</label>
                <div class="col-sm-3">
                    <asp:TextBox runat="server" ID="TxtCantidadSolicitada" CssClass="form-control" ></asp:TextBox>
                </div>
                <div class="col-sm-3">
                    <%--<asp:Button runat="server" ID="BtnBuscarProducto" OnClick="BtnBuscarProducto_Click" />--%>
                </div>
            </div>
        </div>
    <br />
    <div class="row">
            <div class="form-group">
                <label for="lblDepot" class="control-label col-sm-1">Moneda</label>
				<div class="col-sm-3">
					<asp:DropDownList runat="server" ID="DropMoneda" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="DropMoneda_SelectedIndexChanged">
						<asp:ListItem Value="L">CORDOBA</asp:ListItem>
                        <asp:ListItem Value="D">DOLAR</asp:ListItem>
					</asp:DropDownList>
                </div>
                <label for="lblMonedaPedido" class="control-label col-sm-1 ">Moneda Pedido</label>
					<div class="col-sm-3">
					<asp:DropDownList runat="server" ID="DropMonedaPedido" CssClass="form-control">
						<asp:ListItem Value="L">CORDOBA</asp:ListItem>
                        <asp:ListItem Value="D">DOLAR</asp:ListItem>
					</asp:DropDownList>
                </div>
				<div class="col-sm-3">
					<asp:CheckBox runat="server" CssClass="" ID="CheckMigracion"/>&nbsp&nbsp<label for="lblDepartment">Migrar Pedido</label>
				</div>
				<div class="col-sm-4">
					 <asp:Button runat="server" ID="AgregarProducto" OnClick="AgregarProducto_Click" CssClass="btn btn-info" Text="Agregar Articulo" />
                    &nbsp&nbsp
				  <asp:Button runat="server" ID="BtnGuardarEncabezado" OnClick="BtnGuardarEncabezado_Click" CssClass="btn btn-warning" Text="Actualizar Pedido" />
				</div>
                
            </div>
        </div>
	<br />
	<div class="row">
		<div class="form-group">
			<label for="lblDepot" class="control-label col-sm-1">Cantidad Total</label>
                <div class="col-sm-3">
                    <asp:TextBox runat="server" ID="Total_Libras" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
				<label for="lblDepot" class="control-label col-sm-1">Total Pedido</label>
                <div class="col-sm-3">
                    <asp:TextBox runat="server" ID="Total_Pedido" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
		</div>
 </div>
    <br />
      <div class="row">
            <div class="form-group">
                <label for="lblDepot" class="control-label col-sm-1">Detalle</label>
                <div class="col-sm-3">
                    <dx:ASPxGridView ID="GridLineas" runat="server" KeyFieldName="ID" AutoGenerateColumns="False" EnableTheming="True" Theme="Office2010Black" OnRowUpdating="GridLineas_RowUpdating" OnRowDeleting="GridLineas_RowDeleting" KeyboardSupport="True" OnInit="GridLineas_Init">
                    <SettingsAdaptivity>
                    <AdaptiveDetailLayoutProperties ColCount="2" AlignItemCaptionsInAllGroups="True">
                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit">
                        </SettingsAdaptivity>
						<SettingsItemCaptions AllowWrapCaption="True" />
                        </AdaptiveDetailLayoutProperties>
                    </SettingsAdaptivity>

                       <%-- <SettingsEditing UseFormLayout="False">
                        </SettingsEditing>
                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" ConfirmDelete="True" />
                        <SettingsDataSecurity AllowInsert="False" AllowReadUnexposedColumnsFromClientApi="False" AllowReadUnlistedFieldsFromClientApi="False" />--%>
						<SettingsBehavior ConfirmDelete="True" />
                    <EditFormLayoutProperties ColCount="2">
						<Items>
							<dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="PEDIDO">
							</dx:GridViewColumnLayoutItem>
							<dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="ARTICULO">
							</dx:GridViewColumnLayoutItem>
							<dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="CANTIDAD">
							</dx:GridViewColumnLayoutItem>
							<dx:GridViewColumnLayoutItem ColSpan="1" ColumnName="DESCUENTO%">
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
                            <dx:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="0" ShowUpdateButton="True" ShowEditButton="True">
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn FieldName="PEDIDO" VisibleIndex="1" ReadOnly="true">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ARTICULO" VisibleIndex="2" ReadOnly="true">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="DESCRIPCION" VisibleIndex="3" ReadOnly="true" PropertiesTextEdit-MaxLength="20">
							<PropertiesTextEdit MaxLength="20"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="PRECIO" VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CANTIDAD_FACTURAR" VisibleIndex="5" Caption="CANTIDAD">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="LOTE" VisibleIndex="6" ReadOnly="true">
                            </dx:GridViewDataTextColumn>
							<dx:GridViewDataTextColumn FieldName="DESCUENTO" VisibleIndex="7" Caption="DESCUENTO MONTO">
                            </dx:GridViewDataTextColumn>
							<dx:GridViewDataTextColumn FieldName="DESCUENTOPORCENTAJE" VisibleIndex="8" Caption="DESCUENTO%">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
                      
                </div>
            </div>
        </div>
</asp:Content>
