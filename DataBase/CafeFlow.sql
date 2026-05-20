Create database CafeFlowDB;
use CafeFlowDB;
CREATE TABLE Users (
    UserId INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(50),
    Password VARCHAR(255),
    Role VARCHAR(20)
);
CREATE TABLE Products (
    ProductId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100),
    Category VARCHAR(50),
    Price DECIMAL(10,2)
);
CREATE TABLE Customers (
    CustomerId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100),
    Phone VARCHAR(20),
    Address VARCHAR(200)
);
CREATE TABLE Orders (
    OrderId INT AUTO_INCREMENT PRIMARY KEY,
    CustomerName VARCHAR(100),
    Product VARCHAR(100),
    Quantity INT,
    Total DECIMAL(10,2),
    OrderDate DATETIME DEFAULT CURRENT_TIMESTAMP
);
select * from orders;
describe Users;
Alter table Users change Password PasswordHash varchar(255);
Alter table Users change ID ID int auto_increment ;
INSERT INTO Users(Username, PasswordHash, Role)
VALUES
(
'customer',
'$2a$11$OYxEm0eMYAeX9n1XtdxJ1uoZUdwPgge3N49PILmZGAa/PODQWgzAO',
'Customer'
);
iNSERT INTO Users(Username, PasswordHash, Role)
VALUES
(
'admin',
'$2a$11$DhhtphCtAEjJNJELh1mokeO3xQReUKAZaVyZ068sQSrZkmya6xQ06',
'Admin'
);