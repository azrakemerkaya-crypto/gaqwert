CREATE DATABASE BordoStockDb;
GO

USE BordoStockDb;
GO

CREATE TABLE Categories (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(250) NULL,
    IsActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE Suppliers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CompanyName NVARCHAR(150) NOT NULL UNIQUE,
    ContactName NVARCHAR(100) NULL,
    Phone NVARCHAR(20) NULL,
    Email NVARCHAR(150) NULL,
    Address NVARCHAR(250) NULL,
    TaxNumber NVARCHAR(50) NULL,
    IsActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(150) NOT NULL,
    Code NVARCHAR(50) NOT NULL UNIQUE,
    Barcode NVARCHAR(100) NULL,
    CategoryId INT NOT NULL,
    SupplierId INT NULL,
    PurchasePrice DECIMAL(18,2) NOT NULL DEFAULT 0,
    SalePrice DECIMAL(18,2) NOT NULL DEFAULT 0,
    StockQuantity INT NOT NULL DEFAULT 0,
    MinStockLevel INT NOT NULL DEFAULT 0,
    Unit NVARCHAR(30) NOT NULL DEFAULT 'Adet',
    Description NVARCHAR(500) NULL,
    ImageUrl NVARCHAR(250) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Products_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(Id),
    CONSTRAINT FK_Products_Suppliers FOREIGN KEY (SupplierId) REFERENCES Suppliers(Id)
);

CREATE TABLE Warehouses (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL UNIQUE,
    Address NVARCHAR(250) NULL,
    IsActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE StockMovements (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProductId INT NOT NULL,
    WarehouseId INT NOT NULL,
    MovementType NVARCHAR(20) NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL DEFAULT 0,
    MovementDate DATETIME NOT NULL DEFAULT GETDATE(),
    Description NVARCHAR(250) NULL,
    CONSTRAINT FK_StockMovements_Products FOREIGN KEY (ProductId) REFERENCES Products(Id),
    CONSTRAINT FK_StockMovements_Warehouses FOREIGN KEY (WarehouseId) REFERENCES Warehouses(Id)
);

CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Phone NVARCHAR(20) NULL,
    Role NVARCHAR(50) NOT NULL DEFAULT 'Admin',
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

INSERT INTO Categories (Name, Description, IsActive)
VALUES
('Elektronik', 'Elektronik ürünler', 1),
('Ofis', 'Ofis malzemeleri', 1),
('Ev', 'Ev ürünleri', 1);

INSERT INTO Suppliers (CompanyName, ContactName, Phone, Email, Address, TaxNumber, IsActive)
VALUES
('TeknoMarket', 'Ali Yılmaz', '05551234567', 'ali@teknomarket.com', 'İstanbul', '1234567890', 1),
('Ofis Plus', 'Ayşe Demir', '05557654321', 'ayse@ofisplus.com', 'Ankara', '0987654321', 1);

INSERT INTO Warehouses (Name, Address, IsActive)
VALUES
('Ana Depo', 'İstanbul Merkez', 1),
('Ankara Depo', 'Ankara Merkez', 1);

INSERT INTO Products (Name, Code, Barcode, CategoryId, SupplierId, PurchasePrice, SalePrice, StockQuantity, MinStockLevel, Unit, Description, IsActive)
VALUES
('Laptop', 'LAP-001', '8901234567890', 1, 1, 18000, 24000, 15, 5, 'Adet', 'Yüksek performanslı laptop', 1),
('Masaüstü Bilgisayar', 'PC-002', '8901234567891', 1, 1, 22000, 29000, 8, 4, 'Adet', 'Ofis bilgisayarı', 1),
('Kalem Seti', 'OFF-003', '8901234567892', 2, 2, 120, 180, 40, 10, 'Adet', 'Ofis kalem seti', 1);

INSERT INTO StockMovements (ProductId, WarehouseId, MovementType, Quantity, UnitPrice, Description)
VALUES
(1, 1, 'In', 10, 18000, 'Yenileme girişi'),
(2, 1, 'Out', 2, 22000, 'Müşteri teslimi'),
(3, 2, 'In', 25, 120, 'Ek stok girişi');

INSERT INTO Users (FullName, Email, PasswordHash, Phone, Role, IsActive)
VALUES
('Admin Kullanıcı', 'admin@bordostock.com', 'admin123', '05550000000', 'Admin', 1);
GO
