<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrearPaquete.aspx.cs" Inherits="DMINVENTARIO.Views.CrearPaquete" %>
<%@ Register assembly="DevExpress.Web.v18.1, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	  <script>
		  function Insertar(data) {
			  swal({
				  title: "Paquete Agregado",
				  text: "Correctamente",
				  icon: "success",
				  // buttons: true,
				  //dangerMode: true,
			  })
				  .then((willDelete) => {
					  if (willDelete) {
						  location.href = "CrearPaquete.aspx";
					  }
				  });
		  }

		  function Lookup_ValueChanged(s, e) {
			  debugger;
			  document.getElementById("MainContent_BuscarCodigoPro").click();
		  }
    </script>
	<link href="../Content/Tabs.css" rel="stylesheet" />
	<br />
	<h1>Creacion de Paquetes</h1>
	<br />
	<div class="row">
		<div class="form-group">
			<label for="lblDepot" class="control-label col-sm-1">Transaccion</label>
			<div class="col-sm-3">
				<asp:DropDownList runat="server" ID="DropTransaccion" CssClass="form-control" OnSelectedIndexChanged="DropTransaccion_SelectedIndexChanged" AutoPostBack="true">
						<asp:ListItem Value="0">SELECCIONE</asp:ListItem>
						<asp:ListItem Value="COMPRA">COMPRA</asp:ListItem>
						<asp:ListItem Value="AJUSTE">AJUSTE</asp:ListItem>
                        <asp:ListItem Value="TRASPASO">TRASPASO</asp:ListItem>
					</asp:DropDownList>
			</div>
			<div class="col-sm-3">
				<asp:Button runat="server" ID="BuscarCodigoPro" OnClick="BuscarCodigo_Click" style="display:none" />
				<asp:Button runat="server" ID="GuardarDocumento" Text="Guardar Documento" CssClass="btn btn-info" OnClick="GuardarDocumento_Click" />
			</div>
		</div>
	</div>
	<br />
		<asp:UpdatePanel runat="server" ID="Ru">
					<ContentTemplate>
							<div class="row">
									<div class="form-group">
										<label for="lblDepot" class="control-label col-sm-1">Consecutivo</label>
										<div class="col-sm-3">
											<asp:TextBox runat="server" ID="TextConsecutivo" CssClass="form-control" Enabled="false"></asp:TextBox>
										</div>
										<label for="lblDepot" class="control-label col-sm-1">Fecha</label>
										<div class="col-sm-3">
											<dx:ASPxDateEdit runat="server" ID="DateDocumento" CssClass="form-control" required="required" >
											</dx:ASPxDateEdit>
										</div>
										<label for="lblDepot" class="control-label col-sm-1">Referencia</label>
										<div class="col-sm-3">
											<asp:TextBox runat="server" ID="TextReferencia" CssClass="form-control"></asp:TextBox>
										</div>
									</div>
							  </div>
							<br />
							<div class="row">
									<div class="form-group">
										<label for="lblEmployee" class="control-label col-sm-1">Bodega Origen</label>
											<div class="col-sm-3">
												<dx:ASPxGridLookup ID="ASPxGridLookupBodega" required="required" runat="server" CssClass="form-control" AutoGenerateColumns="False" OnInit="ASPxGridLookupBodega_Init" EnableTheming="True" Theme="Office2010Black" KeyFieldName="BODEGA">
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
										<label for="lblEmployee" class="control-label col-sm-1">Bodega Destino</label>
											<div class="col-sm-3">
												<dx:ASPxGridLookup ID="ASPxGridLookupBodegaDestino" required="required" runat="server" CssClass="form-control" AutoGenerateColumns="False" OnInit="ASPxGridLookupBodega_Init" EnableTheming="True" Theme="Office2010Black" KeyFieldName="BODEGA">
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
									</div>
							  </div>

							<br />
							<div class="row">
									<div class="form-group">
										<label for="lblDepot" class="control-label col-sm-1">Articulo</label>
										<div class="col-sm-3">
										<asp:TextBox runat="server" ID="TextArticulo" CssClass="form-control" onkeypress="return EnterEvent(event)"></asp:TextBox>
										<asp:TextBox runat="server" Visible="false" ID="TextArticuloDescripcion"></asp:TextBox>
											<%--
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
										--%>
											</div>
										<label for="lblDepot" class="control-label col-sm-1">Cantidad</label>
										<div class="col-sm-3">
											<asp:TextBox runat="server" ID="TextCantidad" CssClass="form-control"></asp:TextBox>
										</div>
									</div>
							  </div>
							<br />
							<div class="row">
								<div class="form-group">
									<label for="lblDepot" class="control-label col-sm-1">Costo Local</label>
										<div class="col-sm-3">
											<asp:TextBox runat="server" ID="Textcostolocal" CssClass="form-control" Text="0"></asp:TextBox>
											<%--<asp:Button runat="server" ID="BtnBuscarProducto" OnClick="BtnBuscarProducto_Click" />--%>
										</div>
										<label for="lblDepot" class="control-label col-sm-1">Costo Dolar</label>
										<div class="col-sm-3">
											<asp:TextBox runat="server" ID="Textcostodolar" CssClass="form-control" Text="0"></asp:TextBox>
											<%--<asp:Button runat="server" ID="BtnBuscarProducto" OnClick="BtnBuscarProducto_Click" />--%>
										</div>
									<div class="col-sm-3">
										<asp:Button runat="server" ID="BtnAgregarArticulo" Text="Agregar Articulo" CssClass="btn btn-info" OnClick="BtnAgregarArticulo_Click" />
									</div>
								</div>
							</div>
							<br />
							<div class="row">
								<div class="form-group">
									
								</div>
							</div>
							<br />
							<div class="row">
								<div class="col-sm-3">

									<dx:ASPxGridView ID="ASPxGridViewDetalle" runat="server" Theme="Office2010Black" OnRowDeleting="ASPxGridViewDetalle_RowDeleting" OnInit="ASPxGridViewDetalle_Init" AutoGenerateColumns="False" KeyFieldName="ARTICULO">
									<SettingsAdaptivity>
									<AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
									</SettingsAdaptivity>

									<EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
										<Columns>
											<dx:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="0">
											</dx:GridViewCommandColumn>
											<dx:GridViewDataTextColumn Caption="Articulo" FieldName="ARTICULO" VisibleIndex="1">
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn Caption="Descripcion" FieldName="ARTICULODESCRIPCION" VisibleIndex="2">
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn Caption="Bodega" FieldName="BODEGA" VisibleIndex="3">
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn Caption="Cantidad" FieldName="CANTIDAD" VisibleIndex="4">
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn Caption="Costo Local" FieldName="COSTO_TOTAL_LOCAL" VisibleIndex="5">
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn Caption="Costo Dolar" FieldName="COSTO_TOTAL_DOLAR" VisibleIndex="6">
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn Caption="Precio Local" FieldName="PRECIO_TOTAL_LOCAL" VisibleIndex="7">
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn Caption="Precio Dolar" FieldName="PRECIO_TOTAL_DOLAR" VisibleIndex="8">
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn Caption="Bodega Destino" FieldName="BODEGA_DESTINO" VisibleIndex="9">
											</dx:GridViewDataTextColumn>
										</Columns>
									</dx:ASPxGridView>

								</div>
							</div>
					</ContentTemplate>
				</asp:UpdatePanel>

	<script>
		function openCity(cityName) {
			debugger;
		  var i;
		  var x = document.getElementsByClassName("city");
		  for (i = 0; i < x.length; i++) {
			x[i].style.display = "none";  
		  }
		  document.getElementById(cityName).style.display = "block";
		  }
		function EnterEvent(e) {
			debugger;
			if (e.keyCode == Keys.Enter) {
				document.getElementById("MainContent_BuscarCodigoPro").click();
			}
		}
		
	</script>
</asp:Content>
