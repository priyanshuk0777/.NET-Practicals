-- SQLite database script (optional).
-- The application creates inventory.db and this table automatically on first run.

CREATE TABLE IF NOT EXISTS Products (
    ProductId INTEGER PRIMARY KEY AUTOINCREMENT,
    ProductName TEXT NOT NULL,
    Category TEXT NOT NULL,
    Price REAL NOT NULL,
    Quantity INTEGER NOT NULL,
    Supplier TEXT NOT NULL
);
