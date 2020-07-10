<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Consultarticulo.aspx.cs" Inherits="DMINVENTARIO.Views.Consultarticulo" %>
<%@ Register assembly="DevExpress.Web.v18.1, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<link href="../Content/Tabs.css" rel="stylesheet" />
	<br />
	<h1>Consulta de Articulos</h1>
	<br />
	<div class="row">
            <div class="form-group">
                <label for="lblDepot" class="control-label col-sm-1">Articulo</label>
                <div class="col-sm-3">
				<asp:TextBox runat="server" ID="TextArticulo" CssClass="form-control" onkeypress="return EnterEvent(event)"></asp:TextBox>
                <asp:Button style="display:none" runat="server" ID="BuscarArticulo" OnClick="BuscarArticulo_Click" />
				</div>
                <label for="lblDepartment" class="control-label col-sm-1 ">Descripcion</label>
                <div class="col-sm-3">
					<asp:TextBox runat="server" ID="TextDescripcion" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
     </div>

	<br />
	<div class="row">
		<div class="form-group">
			<label for="lblDepartment" class="control-label col-sm-1 ">Costo Local</label>
                <div class="col-sm-3">
					<asp:TextBox runat="server" ID="TextCostoLocal" CssClass="form-control"></asp:TextBox>
                </div>
			<label for="lblDepartment" class="control-label col-sm-1 ">Costo Dolar</label>
                <div class="col-sm-3">
					<asp:TextBox runat="server" ID="TextCostoDolar" CssClass="form-control"></asp:TextBox>
                </div>
				 <label for="lblDepartment" class="control-label col-sm-1 ">Precio</label>
                <div class="col-sm-3">
					<asp:TextBox runat="server" ID="TextPrecio" CssClass="form-control"></asp:TextBox>
                </div>
		</div>
	</div>
	<div class="row">
		<div class="form-group">
			<label for="lblDepartment" class="control-label col-sm-1 ">Fecha Inicial</label>
			<div class="col-sm-3">
				<dx:ASPxDateEdit runat="server" ID="DateInicial" CssClass="form-control" required="required" >
				</dx:ASPxDateEdit>
			</div>
			<label for="lblDepartment" class="control-label col-sm-1 ">Fecha Final</label>
			<div class="col-sm-3">
				<dx:ASPxDateEdit runat="server" ID="DateFinal" CssClass="form-control" required="required" >
				</dx:ASPxDateEdit>
			</div>
			<div class="col-sm-3">
				<asp:Button runat="server" ID="BtnFiltros" OnClick="BtnFiltros_Click" CssClass="btn btn-info" Text="Actualizar" />
			</div>
		</div>
	</div>
	
	<br />
	<asp:UpdatePanel runat="server">
		<ContentTemplate>
			<div class="row" style="border-style:solid;">
				<div class="w3-bar w3-black">
					<label onclick="openCity('INV')" class="w3-bar-item w3-button">Transaccion Inventario</label>
					<label onclick="openCity('EXI')" class="w3-bar-item w3-button">Existencia Bodegas</label>
				</div>
				<br />
				<div id="INV" class="w3-container city">
				  <dx:ASPxGridView ID="ASPxGridViewTransaccion" runat="server" OnInit="ASPxGridViewTransaccion_Init" Theme="BlackGlass" AutoGenerateColumns="False">
				  	<SettingsAdaptivity>
						<AdaptiveDetailLayoutProperties ColCount="1">
							<SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit">
							</SettingsAdaptivity>
						</AdaptiveDetailLayoutProperties>
					  </SettingsAdaptivity>
					  <Settings ShowFilterRow="True" ShowGroupPanel="True" />
					  <EditFormLayoutProperties ColCount="1">
					  </EditFormLayoutProperties>
					  <Columns>
						  <dx:GridViewCommandColumn ShowClearFilterButton="True" VisibleIndex="0">
						  </dx:GridViewCommandColumn>
						  <dx:GridViewDataTextColumn FieldName="Bodega" Name="Bodega" VisibleIndex="1">
						  </dx:GridViewDataTextColumn>
						  <dx:GridViewDataTextColumn FieldName="Cantidad" Name="Cantidad" VisibleIndex="2">
						  </dx:GridViewDataTextColumn>
						  <dx:GridViewDataTextColumn FieldName="Tipo" Name="Tipo" VisibleIndex="3">
						  </dx:GridViewDataTextColumn>
						  <dx:GridViewDataTextColumn FieldName="CostoLocal" Caption="Costo local" VisibleIndex="4">
						  </dx:GridViewDataTextColumn>
						  <dx:GridViewDataTextColumn FieldName="CostoDolar" Caption="Costo Dolar" VisibleIndex="5">
						  </dx:GridViewDataTextColumn>
						    <dx:GridViewDataTextColumn FieldName="CostoComparativoLocal" Caption="Costo Comparativo Local" VisibleIndex="6">
						  </dx:GridViewDataTextColumn>
						    <dx:GridViewDataTextColumn FieldName="CostoComparativoDolar" Caption="Costo Comparativo Dolar" VisibleIndex="7">
						  </dx:GridViewDataTextColumn>
						    <dx:GridViewDataTextColumn FieldName="PrecioLocal" Caption="Precio Local" VisibleIndex="8">
						  </dx:GridViewDataTextColumn>
						    <dx:GridViewDataTextColumn FieldName="PrecioDolar" Caption="Precio Dolar" VisibleIndex="9">
						  </dx:GridViewDataTextColumn>
						  <dx:GridViewDataTextColumn FieldName="FechaHora" Caption="Fecha" VisibleIndex="10">
						  </dx:GridViewDataTextColumn>
					  </Columns>
				  </dx:ASPxGridView>
				</div>

				<div id="EXI" class="w3-container city" style="display:none">
				  <dx:ASPxGridView ID="ASPxGridViewExistencia" runat="server" OnInit="ASPxGridViewExistencia_Init" Theme="BlackGlass" AutoGenerateColumns="False">
					  <SettingsAdaptivity>
						<AdaptiveDetailLayoutProperties ColCount="1">
							<SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit">
							</SettingsAdaptivity>
						</AdaptiveDetailLayoutProperties>
					  </SettingsAdaptivity>
					  <Settings ShowFilterRow="True" />
					  <EditFormLayoutProperties ColCount="1">
					  </EditFormLayoutProperties>
					  <Columns>
						  <dx:GridViewCommandColumn ShowClearFilterButton="True" VisibleIndex="0">
						  </dx:GridViewCommandColumn>
						  <dx:GridViewDataTextColumn FieldName="IdArticulo" Name="Articulo" VisibleIndex="1">
						  </dx:GridViewDataTextColumn>
						  <dx:GridViewDataTextColumn FieldName="ArticuloDescripcion" Name="Descripcion" VisibleIndex="2">
						  </dx:GridViewDataTextColumn>
						  <dx:GridViewDataTextColumn FieldName="Bodega" Name="Bodega" VisibleIndex="3">
						  </dx:GridViewDataTextColumn>
						  <dx:GridViewDataTextColumn FieldName="CantidadDisponible" Name="Cantidad Disponible" VisibleIndex="4">
						  </dx:GridViewDataTextColumn>
					  </Columns>
				  </dx:ASPxGridView>
				</div>

			<%--	<div id="PRE" class="w3-container city" style="display:none">
				  <div class="row">
					<div class="form-group">
						<label for="lblDepot" class="control-label col-sm-1">Articulo</label>
						<div class="col-sm-3">
							
						</div>
						<label for="lblDepartment" class="control-label col-sm-1 ">Precio</label>
						<div class="col-sm-3">
						</div>
					</div>
				  </div>
				</div>--%>
				<br />
			</div>
		</ContentTemplate>
	</asp:UpdatePanel>
	<script>
		function openCity(cityName) {
		  var i;
		  var x = document.getElementsByClassName("city");
		  for (i = 0; i < x.length; i++) {
			x[i].style.display = "none";  
		  }
		  document.getElementById(cityName).style.display = "block";  
		}

		function EnterEvent(e) {
			if (e.keyCode == Keys.Enter) {
				document.getElementById("myCheck").click();
			}
		}
	</script>
</asp:Content>
