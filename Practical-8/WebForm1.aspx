
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="P8.WebForm1" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Inventory Management</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="Content/Inventory.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">

    <header class="navbar">
        <div class="logo">Inventory<span>Pro</span></div>
        <div class="nav-label">Inventory Management System</div>
    </header>

    <main class="container">
        <h1>Dashboard</h1>
        <p class="subtitle">Manage your products and inventory.</p>

        <section class="stats">
            <div class="stat-card">
                <div class="stat-icon">📦</div>
                <p>Inventory System</p>
                <h2>Products</h2>
                <span>Manage your stock records</span>
            </div>

            <div class="stat-card">
                <div class="stat-icon">₹</div>
                <p>Stock Management</p>
                <h2>Inventory</h2>
                <span>Track quantity and price</span>
            </div>
        </section>

        <section class="panel">
            <div class="section-heading">
                <div>
                    <h2>Add New Product</h2>
                    <p>Enter product details below</p>
                </div>
            </div>

            <div class="form-grid">
                <div class="field">
                    <label>Product Name</label>
                    <asp:TextBox ID="txtName" runat="server"
                        CssClass="input-control"
                        placeholder="Enter product name" />
                </div>

                <div class="field">
                    <label>Quantity</label>
                    <asp:TextBox ID="txtQuantity" runat="server"
                        CssClass="input-control"
                        TextMode="Number"
                        placeholder="Enter quantity" />
                </div>

                <div class="field">
                    <label>Price (₹)</label>
                    <asp:TextBox ID="txtPrice" runat="server"
                        CssClass="input-control"
                        TextMode="Number"
                        placeholder="Enter price" />
                </div>
            </div>

            <asp:Button ID="btnAdd" runat="server"
                Text="+ Add Product"
                CssClass="primary-btn"
                OnClick="btnAdd_Click" />

        </section>

        <section class="panel">
            <div class="section-heading">
                <div>
                    <h2>Product Inventory</h2>
                    <p>Products saved in your database</p>
                </div>
                <span class="status">Live Records</span>
            </div>

            <div class="table-wrap">
                <asp:GridView ID="GridView1" runat="server"
                    AutoGenerateColumns="False"
                    CssClass="inventory-table"
                    GridLines="None"
                    EmptyDataText="No products found. Add your first product.">

                    <Columns>
                        <asp:BoundField DataField="Id"
                            HeaderText="ID" />

                        <asp:BoundField DataField="ProductName"
                            HeaderText="Product Name" />

                        <asp:BoundField DataField="Quantity"
                            HeaderText="Quantity" />

                        <asp:BoundField DataField="Price"
                            HeaderText="Price (₹)"
                            DataFormatString="{0:N2}" />
                    </Columns>
                </asp:GridView>
            </div>
        </section>
    </main>

</form>
</body>
</html>
