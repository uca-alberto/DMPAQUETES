<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DMINVENTARIO.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>LOGIN</title>
     <link rel="stylesheet" href="~/assets/css/normalize.css"/>
     <%--<link rel="stylesheet" href="../BCFOODS/assets/css/normalize.css"/>--%>

    <link rel="stylesheet" href="~/assets/css/bootstrap.min.css"/>
    <link rel="stylesheet" href="~/assets/css/font-awesome.min.css"/>
    <link rel="stylesheet" href="~/assets/css/themify-icons.css"/>
    <link rel="stylesheet" href="~/assets/css/flag-icon.min.css"/>
    <link rel="stylesheet" href="~/assets/css/cs-skin-elastic.css"/>
    <link rel="stylesheet" href="~/assets/scss/style.css"/>
    <link href="assets/css/lib/vector-map/jqvmap.min.css" rel="stylesheet"/>
    <script src="assets/sweetalert.min.js"></script>
         <script>
        function Validacion() {
            swal({
                title: "Error",
                text: "Datos Incorrectos",
                icon: "warning",
                button: "OK",
            });
    }
    </script>
</head>
<body>
            <div class="sufee-login d-flex align-content-center flex-wrap">
        <div class="container">
            <div class="login-content">
                <div class="login-logo">
                    <a href="index.html">
                        <img class="align-content" src="assets/imagenes/Uca_Logo.jpg" alt="">
                    </a>
                </div>
                <div class="login-form">
                    <form method="post" enctype="multipart/form-data" class="form-horizontal" runat="server">
                          <!--nombre-->
                          <div class="row form-group">
                            <div class="col col-md-3"><label for="text-input" class=" form-control-label">Usuario</label></div>
                            <div class="col-12 col-md-9">
                                 <asp:TextBox ID="Usuario" runat="server" Text="" ToolTip="Usuario" CssClass="form-control" ValidateRequestMode="Enabled"></asp:TextBox>         
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Usuario" Display="Dynamic" ErrorMessage="Este Campo es requerido" BorderColor="#FF5050" BorderStyle="None" CssClass="form_hint" ForeColor="Red"></asp:RequiredFieldValidator>
                                 <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Mnombre" ErrorMessage="Nombre incorrecto"></asp:RegularExpressionValidator>--%>
                            </div>
                          </div>
                            <!--Contraseña-->
                          <div class="row form-group">
                            <div class="col col-md-3"><label for="email-input" class=" form-control-label">Contraseña</label></div>
                            <div class="col-12 col-md-9">
                                 <asp:TextBox ID="Contraseña" runat="server" TextMode="Password" ToolTip="Contraseña" CssClass="form-control" ValidateRequestMode="Enabled"></asp:TextBox>                                              
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Contraseña" Display="Dynamic" ErrorMessage="Este Campo es requerido" BorderColor="#FF5050" BorderStyle="None" CssClass="form_hint" ForeColor="Red"></asp:RequiredFieldValidator>
                                 <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Mcontrasena" ErrorMessage="Contraseña incorrecta"></asp:RegularExpressionValidator>--%>
                            </div>
                          </div>
                         <div class="modal-footer">
                          <asp:HiddenField runat="server" ID="Logeo" />
                          <asp:Button ID="BtnLogin" runat="server" Text="Ingresar" CssClass="btn btn-primary" OnClick="BtnLogin_Click" />
                            </div>
						<div class="modal-footer">
							<asp:Label runat="server" ID="Error" Visible="false"></asp:Label>
						</div>
                    </form>
                </div>
            </div>
        </div>
    </div>
      <script src="~/assets/js/vendor/jquery-2.1.4.min.js"></script>
    <script src="~/assets/js/plugins.js"></script>
    <script src="~/assets/js/main.js"></script>
</body>
</html>

