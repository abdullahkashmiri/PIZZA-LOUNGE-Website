create DataBase PizzaLounge_database;

use PizzaLounge_database;

--------------------------------------------------------------------------------------------------------------
-- table for users
create Table Users (
UserId int Primary Key Identity(1,1),
User_name varchar(255) not NULL Unique,
Email varchar(225) Null,
u_password varchar(53) Not Null,
orders int null default 0
);

--------------------------------------------------------------------------------------------------------------
-- table for admin
create Table Admins (
AdminId int Primary Key Identity(1,1),
User_name varchar(255) not NULL Unique,
Email varchar(225) Null,
a_password varchar(53) Not Null,
);

--------------------------------------------------------------------------------------------------------------
-- table for user's discount
create Table user_discount (
UserId int Primary Key,
discount decimal(3,2) default 0.00,
valid_till  Date,
foreign key(UserId) references Users(UserId)
);

--------------------------------------------------------------------------------------------------------------
-- table for products
CREATE TABLE Products
(
    ProductId INT PRIMARY KEY identity(1,1) ,
    Name VARCHAR(50) NULL Unique,
    Description VARCHAR(MAX) NULL,
    Price INT NULL,
    ImageUrl VARCHAR(MAX) NULL,
    CategoryId INT NULL, -- fk
    IsActive int NULL,
    Rating DECIMAL(3, 1) NULL -- New rating column
);

--------------------------------------------------------------------------------------------------------------
-- table for cart
create table Carts
(
CartId int primary key identity(1,1) NOT NULL,
ProductId int NULL,--fk
Quantity int NULL,
UserId int, -- fk
Price int,
OrderNo int,
Size int,-- 1 small 2 medium 3 large 
C_Order int, -- 1 for custom order else 0
Status int null,
Foreign Key(ProductId) References Products(ProductId),
Foreign Key(UserId) References Users(UserId)
); 

--------------------------------------------------------------------------------------------------------------
-- table for orders
create table Orders
(
OrderNo int Primary Key Identity(1,1),
UserId int NULL, --Fk
OrderDate datetime NULL,
Price int,
Status int NULL,
Foreign Key(UserId) References Users(UserId)
);

--------------------------------------------------------------------------------------------------------------
-- table for complaints
CREATE TABLE complaint_form (
  [complaint_id] INT primary key identity (1,1) NOT NULL,
  [username] VARCHAR(50) NOT NULL,
  [email] VARCHAR(50) NOT NULL,
  [message] varchar(max) NOT NULL,
  [submit_time] datetime NOT NULL,
  status int null,
); 

--------------------------------------------------------------------------------------------------------------
-- table for jobs
CREATE TABLE job_form (
  [job_id] INT primary key identity (1,1) NOT NULL,
  [firstname] VARCHAR(50) NOT NULL,
  [lastname] VARCHAR(50) NOT NULL,
  [email] VARCHAR(50) NOT NULL,
  [city] VARCHAR(50) NOT NULL,
  [zip] VARCHAR(50) NOT NULL,
  cv_file VARBINARY(MAX) NOT NULL,
  status int null,
);

--------------------------------------------------------------------------------------------------------------
-- table for Reservation
CREATE TABLE Reservation (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Day VARCHAR(10),
    Hour varchar(10),
    Name VARCHAR(50),
    Phone VARCHAR(20),
    Persons INT,
	UserId int,
	status int null,
	Foreign Key(UserId) References Users(UserId)
);

--------------------------------------------------------------------------------------------------------------
-- table for Custom Order
CREATE TABLE Custom_Order (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    Size NVARCHAR(50) NULL,
    Crust NVARCHAR(50) NULL,
    Sauce NVARCHAR(50) NULL,
    CheeseQuantity NVARCHAR(50) NULL,
    Toppings NVARCHAR(200) NULL,
	Amount int NULL
);

--------------------------------------------------------------------------------------------------------------
-- table for Rating 
CREATE TABLE Rating (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100),
    ProductID INT,
    ProductName VARCHAR(100),
    Rating INT,
    UserId INT,
    Comment VARCHAR(100),
	Status int NULL,
	--Foreign Key(ProductId) References Products(ProductId),
	Foreign Key(UserId) References Users(UserId)
);
  
--------------------------------------------------------------------------------------------------------------
----------------------------------------INSERTING VALUES------------------------------------------------------
--------------------------------------------------------------------------------------------------------------

INSERT INTO Products(  Name, Description, Price, ImageUrl, CategoryId, IsActive, Rating)
VALUES 
( 'Chiken Tikka Pizza', 'A savory chicken tikka pizza', 1100, '../TemplateFiles/menuimages/tikka.jpg', 1, 1, 4.5),
( 'Chiken Fajita Pizza', 'Savor the chicken fajita pizza', 1100, '../TemplateFiles/menuimages/fajita.jpg', 1, 1, 4.2),
( 'Tikka Achari Pizza', 'Taste spicy chicken achari pizza', 1100, '../TemplateFiles/menuimages/achari.jpg', 1, 1, 4.2),
( 'Vegetable Pizza', 'A delicious celebration of veggies', 1100, '../TemplateFiles/menuimages/veg.jpg', 1, 1, 4.0),
( 'Chapli Kabab Pizza', 'Perfection for a scrumptious meal', 1100, '../TemplateFiles/menuimages/chapli.jpg', 1, 1, 4.7),
( 'Cheesy Pizza', 'Simple yet satisfying cheesy pizza', 1100, '../TemplateFiles/menuimages/cheesy.jpg', 1, 1, 4.5),
( 'Mexican Pizza (Special)', 'Embark on a flavor adventure', 1300, '../TemplateFiles/menuimages/mexican.jpg', 1, 1, 4.8),
( 'Shawarma Pizza', 'Exotic flavor of shawarma', 1200 , '../TemplateFiles/menuimages/shawarma.jpg', 1, 0, 4.5),
( 'Seekh Kabab Pizza', 'Delicious pizza featuring seekh kabab', 1100, '../TemplateFiles/menuimages/seekh.jpg', 1, 0, 4.1),
( 'Mughlayi Pizza', 'Perfectly baked delight', 1100, '../TemplateFiles/menuimages/mughlayi.jpg', 1, 0, 4.6),
( 'Pepperoni Pizza', 'Enjoy a classic pepperoni pizza', 100, '../TemplateFiles/menuimages/pepperoni.jpg', 1, 0, 4.3),
( 'Chicken Bar B.Q Pizza', 'Delicious base of smoky BBQ', 100, '../TemplateFiles/menuimages/bbq.jpg', 1, 0, 4.2),
( 'Royal Pizza (Special)', 'Treat yourself with a luxurious feast', 1350, '../TemplateFiles/menuimages/royal.jpg', 1, 0, 5.0),
( 'Stuff Crust Pizza', 'Takes pizza to next level', 1300, '../TemplateFiles/menuimages/stuff.jpg', 1, 0, 5.0),
( 'Malai Boti Pizza', 'Satisfy your cravings', 1200, '../TemplateFiles/menuimages/malai.jpg', 1, 0, 4.7),
( 'Lasagne Pizza', 'Enjoy the best of both worlds', 1200, '../TemplateFiles/menuimages/lasagne.jpg', 1, 0, 4.8),
( 'Supreme Pizza (Special)', 'Get the ultimate pizza experience', 1300, '../TemplateFiles/menuimages/supreme.jpg', 1, 1, 4.9);

--------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------
INSERT INTO user_discount (UserId, discount, valid_till)
VALUES
    (1, 0.10, '2023-06-30'),
    (2, 0.15, '2023-07-31'),
    (3, 0.20, '2023-08-31'),
    (0, 0.05, '2023-09-30'),
    (1, 0.10, '2023-10-31'),
    (2, 0.25, '2023-11-30'),
    (3, 0.15, '2023-12-31'),
    (0, 0.10, '2024-01-31'),
    (0, 0.05, '2024-02-29'),
    (0, 0.20, '2024-03-31');

	INSERT INTO job_form (firstname, lastname, email, city, zip, cv_file, status)
VALUES
    ('a', 'Doe', 'john@example.com', 'New York', '12345', 0x012345, NULL),
    ('b', 'Smith', 'jane@example.com', 'Los Angeles', '54321', 0x987654, NULL),
    ('c', 'Johnson', 'michael@example.com', 'Chicago', '67890', 0x456789, NULL),
    ('d', 'Brown', 'emily@example.com', 'Houston', '09876', 0x234567, NULL),
    ('a', 'Wilson', 'david@example.com', 'San Francisco', '13579', 0x876543, NULL),
    ('b', 'Taylor', 'sarah@example.com', 'Seattle', '97531', 0x345678, NULL),
    ('c', 'Anderson', 'robert@example.com', 'Boston', '24680', 0x567890, NULL),
    ('d', 'Lee', 'jessica@example.com', 'Miami', '01928', 0x654321, NULL),
    ('a', 'Martin', 'daniel@example.com', 'Dallas', '28374', 0x789012, NULL),
    ('b', 'Clark', 'olivia@example.com', 'Phoenix', '72648', 0x890123, NULL);


	INSERT INTO complaint_form (username, email, message, submit_time, status)
VALUES
    ('a', 'user1@example.com', 'Complaint 1', '2023-05-01', NULL),
    ('User2', 'user2@example.com', 'Complaint 2', '2023-05-02', NULL),
    ('User3', 'user3@example.com', 'Complaint 3', '2023-05-03', NULL),
    ('a', 'user4@example.com', 'Complaint 4', '2023-05-04', NULL),
    ('User5', 'user5@example.com', 'Complaint 5', '2023-05-05', NULL),
    ('User6', 'user6@example.com', 'Complaint 6', '2023-05-06', NULL),
    ('a', 'user7@example.com', 'Complaint 7', '2023-05-07', NULL),
    ('User8', 'user8@example.com', 'Complaint 8', '2023-05-08', NULL),
    ('User9', 'user9@example.com', 'Complaint 9', '2023-05-09', NULL),
    ('User10', 'user10@example.com', 'Complaint 10', '2023-05-10', NULL);



 INSERT INTO Rating (Name, ProductID, ProductName, Rating, UserId, Comment)
VALUES
    ('Abdullah', 1, 'Chiken Tikka Pizza', 4, 1, 'Great pizza!'),
    ('Jane Smith', 2, 'Chiken Fajita Pizza', 5, 2, 'Delicious and flavorful'),
    ('Michael Johnson', 1, 'Chiken Tikka Pizza', 3, 3, 'Could be better'),
    ('Emily Davis', 3, 'Tikka Achari Pizza', 4, 4, 'Spicy and tasty'),
    ('David Wilson', 2, 'Chiken Fajita Pizza', 4, 1, 'Good quality ingredients');

	-- Insert 10 orders with user IDs from 1 to 4
INSERT INTO Orders (UserId, OrderDate, Price, Status)
SELECT TOP 10
    ABS(CHECKSUM(NEWID())) % 4 + 1 AS UserId, -- Generate random user IDs from 1 to 4
    GETDATE() AS OrderDate, -- Use the current date as the order date
    ABS(CHECKSUM(NEWID())) % 3000 AS Price, -- Generate random prices between 0 and 99
    NULL AS Status -- Set the initial status as NULL
FROM sys.all_columns AS c1 CROSS JOIN sys.all_columns AS c2;

INSERT INTO Reservation (Day, Hour, Name, Phone, Persons, UserId, status)
VALUES
    ('2023-05-01', '12:00 PM', 'John Doe', '123-456-7890', 2, 1, NULL),
    ('2023-05-02', '1:00 PM', 'Jane Smith', '987-654-3210', 4, 2, NULL),
    ('2023-05-03', '2:00 PM', 'Michael Johnson', '456-789-0123', 3, 3, NULL),
    ('2023-05-04', '3:00 PM', 'Emily Brown', '789-012-3456', 5, 4, NULL),
    ('2023-05-05', '4:00 PM', 'David Wilson', '234-567-8901', 2, 1, NULL),
    ('2023-05-06', '5:00 PM', 'Sarah Taylor', '567-890-1234', 4, 2, NULL),
    ('2023-05-07', '6:00 PM', 'Robert Anderson', '876-543-2109', 3, 4, NULL),
    ('2023-05-08', '7:00 PM', 'Jessica Lee', '654-321-0987', 2, 2, NULL),
    ('2023-05-09', '8:00 PM', 'Daniel Martin', '789-012-3456', 5, 1, NULL),
    ('2023-05-10', '9:00 PM', 'Olivia Clark', '012-345-6789', 3, 3, NULL);

--------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------
INSERT INTO Admins (User_name, Email, a_password)
VALUES ('Darsaab', 'dar@gmail.com', '123');
-- Insert four users into the Users table
INSERT INTO Users (User_name, Email, u_password)
VALUES
    ('a', 'a@example.com', 'password1'),
    ('b', 'b@example.com', 'password2'),
    ('c', 'c@example.com', 'password3'),
    ('d', 'd@example.com', 'password4');
	  

	 
--------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------
select * From Users;
select * From user_discount;
select * from Products;
select * From complaint_form;
select * From job_form;
select * from Carts;
select * from Orders;
select * from Reservation;
select * from Custom_Order;
select * from Rating;
--------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------

--------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------
Create Procedure discount_order_manage
@Action varchar(20),
@User_id int = null,
@disc decimal(3,2) = 0.00,
@amount int = 0

As Begin
	Set	NoCount On;

	IF @Action = 'DISCOUNT'
	Begin
		Select discount From user_discount Where UserId = @User_id	
	End

	IF @Action = 'ORDER'
	Begin
		If @amount >= 500 --apply discount if total amount is above 500
		Begin
			Set @amount = @amount - @amount*@disc;
			Delete From user_discount Where UserId = @User_id
		End
		Insert into Orders(UserId, OrderDate, Price) Values(@User_id, GETDATE(), @amount);
		Update Users Set orders = orders + 1 Where UserId = @User_id --increment user's orders
	End
END

--------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------

Create Procedure cart_manage
@Action varchar(20),
@Prod_id int = null,
@User_id int = null,
@Quantity int = 0

As Begin
	Set	NoCount On;
	Declare @Price int = 0

-- For Insert
	If @Action = 'INSERT'
	Begin
		Select @Price = Price From dbo.Products p where ProductId = @Prod_id
		Insert Into Carts (UserId, ProductId, Quantity, Price) Values(@User_id, @Prod_id, @Quantity, @Price)
	End

-- For Inc
	If @Action = 'INC'
	Begin
		Select @Price = Price From dbo.Products p where ProductId = @Prod_id
		Update Carts Set Quantity = Quantity + 1, Price = Price + @Price
		Where UserId = @User_id AND ProductId = @Prod_id;
	End

	-- For Dec
	If @Action = 'DEC'
	Begin
		Declare @q int
		Select @q = Quantity From Carts Where UserId = @User_id AND ProductId = @Prod_id;
		IF @q = 1
		Begin
			Delete From Carts 
			Where UserId = @User_id AND ProductId = @Prod_id;
		End

		Else
		 Begin
			Select @Price = Price From dbo.Products p where ProductId = @Prod_id
			Update Carts Set Quantity = Quantity - 1, Price = Price - @Price
			Where UserId = @User_id AND ProductId = @Prod_id;
		 End
	End

-- For Delete
	If @Action = 'DELETE'
	Begin
		Delete From Carts 
		Where UserId = @User_id AND ProductId = @Prod_id;
	End

-- For GetbyId
	If @Action = 'GETBYID'
	Begin
		Select * From Carts
		Where UserId = @User_id AND ProductId = @Prod_id;
	End

-- For TotalPrice
	If @Action = 'PRICE'
	Begin
		Select Sum(Price) As t_price From Carts
		Group by UserId having UserId = @User_id 
	End

-- For clear
	If @Action = 'CLEAR'
	Begin
		Delete From Carts
		Where UserId = @User_id
	End
END

--------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------

--Drop Table user_discount
--Drop Table Users
--Drop Table Categories
--Drop Table Products
--Drop table complaint_form
--Delete From user_discount
--Delete From Users


 