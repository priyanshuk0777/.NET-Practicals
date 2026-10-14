using InventoryManagement.Models;
using Microsoft.Data.Sqlite;

namespace InventoryManagement.Data;

public class ProductRepository
{
    private readonly string _connectionString;

    public ProductRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Database connection string is missing.");
    }

    public async Task InitializeAsync()
    {
        await using var con = new SqliteConnection(_connectionString);
        await con.OpenAsync();

        const string createTable = @"
            CREATE TABLE IF NOT EXISTS Products (
                ProductId INTEGER PRIMARY KEY AUTOINCREMENT,
                ProductName TEXT NOT NULL,
                Category TEXT NOT NULL,
                Price REAL NOT NULL,
                Quantity INTEGER NOT NULL,
                Supplier TEXT NOT NULL
            );";

        await using (var cmd = new SqliteCommand(createTable, con))
        {
            await cmd.ExecuteNonQueryAsync();
        }

        await using var countCmd = new SqliteCommand("SELECT COUNT(*) FROM Products;", con);
        var count = Convert.ToInt32(await countCmd.ExecuteScalarAsync());

        if (count == 0)
        {
            const string seed = @"
                INSERT INTO Products (ProductName, Category, Price, Quantity, Supplier) VALUES
                ('Wireless Mouse', 'Electronics', 799.00, 25, 'Tech World'),
                ('Mechanical Keyboard', 'Electronics', 2499.00, 15, 'Computer Hub'),
                ('Notebook', 'Stationery', 80.00, 100, 'ABC Stationers'),
                ('Office Chair', 'Furniture', 5499.00, 10, 'Comfort Furniture'),
                ('USB-C Cable', 'Accessories', 399.00, 50, 'Digital Store');";

            await using var seedCmd = new SqliteCommand(seed, con);
            await seedCmd.ExecuteNonQueryAsync();
        }
    }

    public async Task<List<Product>> GetAllAsync(string? search = null)
    {
        var products = new List<Product>();
        const string sql = @"SELECT ProductId, ProductName, Category, Price, Quantity, Supplier
                             FROM Products
                             WHERE (@Search = '' OR ProductName LIKE '%' || @Search || '%'
                                OR Category LIKE '%' || @Search || '%'
                                OR Supplier LIKE '%' || @Search || '%')
                             ORDER BY ProductId DESC;";

        await using var con = new SqliteConnection(_connectionString);
        await using var cmd = new SqliteCommand(sql, con);
        cmd.Parameters.AddWithValue("@Search", search ?? string.Empty);
        await con.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
            products.Add(Map(reader));

        return products;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        const string sql = @"SELECT ProductId, ProductName, Category, Price, Quantity, Supplier
                             FROM Products WHERE ProductId = @ProductId;";
        await using var con = new SqliteConnection(_connectionString);
        await using var cmd = new SqliteCommand(sql, con);
        cmd.Parameters.AddWithValue("@ProductId", id);
        await con.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? Map(reader) : null;
    }

    public async Task<int> CreateAsync(Product product)
    {
        const string sql = @"INSERT INTO Products (ProductName, Category, Price, Quantity, Supplier)
                             VALUES (@ProductName, @Category, @Price, @Quantity, @Supplier);
                             SELECT last_insert_rowid();";
        await using var con = new SqliteConnection(_connectionString);
        await using var cmd = new SqliteCommand(sql, con);
        AddParameters(cmd, product);
        await con.OpenAsync();
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        const string sql = @"UPDATE Products SET ProductName=@ProductName, Category=@Category,
                             Price=@Price, Quantity=@Quantity, Supplier=@Supplier
                             WHERE ProductId=@ProductId;";
        await using var con = new SqliteConnection(_connectionString);
        await using var cmd = new SqliteCommand(sql, con);
        cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
        AddParameters(cmd, product);
        await con.OpenAsync();
        return await cmd.ExecuteNonQueryAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        const string sql = "DELETE FROM Products WHERE ProductId=@ProductId;";
        await using var con = new SqliteConnection(_connectionString);
        await using var cmd = new SqliteCommand(sql, con);
        cmd.Parameters.AddWithValue("@ProductId", id);
        await con.OpenAsync();
        return await cmd.ExecuteNonQueryAsync() > 0;
    }

    private static void AddParameters(SqliteCommand cmd, Product p)
    {
        cmd.Parameters.AddWithValue("@ProductName", p.ProductName);
        cmd.Parameters.AddWithValue("@Category", p.Category);
        cmd.Parameters.AddWithValue("@Price", p.Price);
        cmd.Parameters.AddWithValue("@Quantity", p.Quantity);
        cmd.Parameters.AddWithValue("@Supplier", p.Supplier);
    }

    private static Product Map(SqliteDataReader r) => new()
    {
        ProductId = r.GetInt32(0),
        ProductName = r.GetString(1),
        Category = r.GetString(2),
        Price = Convert.ToDecimal(r.GetValue(3)),
        Quantity = r.GetInt32(4),
        Supplier = r.GetString(5)
    };
}
