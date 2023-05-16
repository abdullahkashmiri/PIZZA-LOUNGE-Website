create DataBase PizzaLounge_db;

use PizzaLounge_db;

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
    ProductId INT PRIMARY KEY NOT NULL,
    Name VARCHAR(50) NULL Unique,
    Description VARCHAR(MAX) NULL,
    Price INT NULL,
    ImageUrl VARCHAR(MAX) NULL,
    CategoryId INT NULL, -- fk
    IsActive BIT NULL,
    Rating DECIMAL(3, 1) NULL -- New rating column
	--Foreign Key(CategoryId) References Categories(CategoryId)
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
--ProductId int NULL, --fk
Price int,
--Quantity int NULL,
--Status varchar(50) NULL
--PaymentId int NULL,--fk
--Foreign Key(ProductId) References Products(ProductId),
Foreign Key(UserId) References Users(UserId)
--Foreign Key(PaymentId) References Payment(PaymentId)
);

--------------------------------------------------------------------------------------------------------------
-- table for complaints
CREATE TABLE complaint_form (
  [complaint_id] INT primary key identity (1,1) NOT NULL,
  [username] VARCHAR(50) NOT NULL,
  [email] VARCHAR(50) NOT NULL,
  [message] varchar(max) NOT NULL,
  [submit_time] datetime NOT NULL
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
  cv_file VARBINARY(MAX) NOT NULL
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
Foreign Key(ProductId) References Products(ProductId),
	Foreign Key(UserId) References Users(UserId)
);

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

--------------------------------------------------------------------------------------------------------------
----------------------------------------INSERTING VALUES------------------------------------------------------
--------------------------------------------------------------------------------------------------------------
delete from Products

INSERT INTO Products(ProductId, Name, Description, Price, ImageUrl, CategoryId, IsActive, Rating)
VALUES 
(1, 'Chiken Tikka Pizza', 'A savory chicken tikka pizza', 1100, '../TemplateFiles/menuimages/tikka.jpg', 1, 1, 4.5),
(2, 'Chiken Fajita Pizza', 'Savor the chicken fajita pizza', 1100, '../TemplateFiles/menuimages/fajita.jpg', 1, 1, 4.2),
(3, 'Tikka Achari Pizza', 'Taste spicy chicken achari pizza', 1100, '../TemplateFiles/menuimages/achari.jpg', 1, 1, 4.2),
(4, 'Vegetable Pizza', 'A delicious celebration of veggies', 1100, '../TemplateFiles/menuimages/veg.jpg', 1, 1, 4.0),
(5, 'Chapli Kabab Pizza', 'Perfection for a scrumptious meal', 1100, '../TemplateFiles/menuimages/chapli.jpg', 1, 1, 4.7),
(6, 'Cheesy Pizza', 'Simple yet satisfying cheesy pizza', 1100, '../TemplateFiles/menuimages/cheesy.jpg', 1, 1, 4.5),
(7, 'Mexican Pizza (Special)', 'Embark on a flavor adventure', 1300, '../TemplateFiles/menuimages/mexican.jpg', 1, 1, 4.8),
(8, 'Shawarma Pizza', 'Exotic flavor of shawarma', 1200 , '../TemplateFiles/menuimages/shawarma.jpg', 1, 0, 4.5),
(9, 'Seekh Kabab Pizza', 'Delicious pizza featuring seekh kabab', 1100, '../TemplateFiles/menuimages/seekh.jpg', 1, 0, 4.1),
(10, 'Mughlayi Pizza', 'Perfectly baked delight', 1100, '../TemplateFiles/menuimages/mughlayi.jpg', 1, 0, 4.6),
(11, 'Pepperoni Pizza', 'Enjoy a classic pepperoni pizza', 100, '../TemplateFiles/menuimages/pepperoni.jpg', 1, 0, 4.3),
(12, 'Chicken Bar B.Q Pizza', 'Delicious base of smoky BBQ', 100, '../TemplateFiles/menuimages/bbq.jpg', 1, 0, 4.2),
(13, 'Royal Pizza (Special)', 'Treat yourself with a luxurious feast', 1350, '../TemplateFiles/menuimages/royal.jpg', 1, 0, 5.0),
(14, 'Stuff Crust Pizza', 'Takes pizza to next level', 1300, '../TemplateFiles/menuimages/stuff.jpg', 1, 0, 5.0),
(15, 'Malai Boti Pizza', 'Satisfy your cravings', 1200, '../TemplateFiles/menuimages/malai.jpg', 1, 0, 4.7),
(16, 'Lasagne Pizza', 'Enjoy the best of both worlds', 1200, '../TemplateFiles/menuimages/lasagne.jpg', 1, 0, 4.8),
(17, 'Supreme Pizza (Special)', 'Get the ultimate pizza experience', 1300, '../TemplateFiles/menuimages/supreme.jpg', 1, 1, 4.9);
--------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------
 INSERT INTO Rating (Name, ProductID, ProductName, Rating, UserId, Comment)
VALUES
    ('Abdullah', 1, 'Chiken Tikka Pizza', 4, 1, 'Great pizza!'),
    ('Jane Smith', 2, 'Chiken Fajita Pizza', 5, 1, 'Delicious and flavorful'),
    ('Michael Johnson', 1, 'Chiken Tikka Pizza', 3, 3, 'Could be better'),
    ('Emily Davis', 3, 'Tikka Achari Pizza', 4, 4, 'Spicy and tasty'),
    ('David Wilson', 2, 'Chiken Fajita Pizza', 4, 5, 'Good quality ingredients');

	
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





--Drop table Orders
--drop table Users
--drop table user_discount
--drop table Carts


-- table for cart
--create table Carts
--(
--CartId int primary key identity(1,1) NOT NULL,
--ProductId int NULL,--fk
--Quantity int NULL,
--UserId int, -- fk
--Price int,
--Foreign Key(ProductId) References Products(ProductId),
--Foreign Key(UserId) References Users(UserId)
--);

-- Insert 5 rows with C_Order = 1
INSERT INTO Carts (ProductId, Quantity, UserId, Price, OrderNo, Size, C_Order)
VALUES
    (1, 10, 1, 50, 1001, 2, 1),
    (2, 5, 1, 20, 1002, 1, 1),
    (3, 2, 1, 15, 1003, 3, 1),
    (4, 3, 1, 25, 1004, 2, 1),
    (5, 8, 1, 40, 1005, 1, 1);

-- Insert 5 rows with C_Order = 0
INSERT INTO Carts (ProductId, Quantity, UserId, Price, OrderNo, Size, C_Order)
VALUES
    (6, 4, 1, 30, 1006, 2, 0),
    (7, 7, 1, 35, 1007, 1, 0),
    (8, 6, 1, 45, 1008, 3, 0),
    (9, 9, 1, 55, 1009, 1, 0),
    (10, 1, 1, 10, 1010, 2, 0);Select * From Carts




Drop Table user_discount
Drop Table Users
Drop Table Categories
Drop Table Products
Drop table complaint_form
Delete From user_discount
Delete From Users



select * from Products;
drop table Products;
-- table for products

INSERT INTO Products(ProductId, Name, Description, Price, ImageUrl, CategoryId, IsActive, Rating)
VALUES 
(1, 'Margherita Pizza', 'Classic cheese pizza with basil', 1000, '../TemplateFiles/menuimages/iimg1.jpg', 1, 1, 4.5),
(2, 'Pepperoni Pizza', 'Delicious pizza with pepperoni', 1200, '../TemplateFiles/menuimages/iimg2.jpg', 1, 1, 4.2),
(3, 'Vegetarian Pizza', 'Fresh vegetable medley on crust', 1100, '../TemplateFiles/menuimages/iimg3.jpg', 1, 1, 3.8),
(4, 'BBQ Chicken Pizza', 'Savory pizza with BBQ chicken', 1300, '../TemplateFiles/menuimages/iimg4.jpg', 1, 1, 4.0),
(5, 'Mushroom Pizza', 'Hearty pizza with mushrooms', 1000, '../TemplateFiles/menuimages/iimg5.jpg', 1, 1, 4.7),
(6, 'Hawaiian Pizza', 'Tropical twist with ham, pineapple', 1200, '../TemplateFiles/menuimages/iimg6.jpg', 1, 1, 3.5),
(7, 'Supreme Pizza', 'Assortment of toppings for pizza', 1500, '../TemplateFiles/menuimages/iimg7.jpg', 1, 1, 4.8),
(8, 'Chicken Alfredo Pizza', 'Creamy Alfredo with grilled chicken', 1400, '../TemplateFiles/menuimages/img10.jpg', 1, 1, 4.3),
(9, 'Meat Lovers Pizza', 'Meaty delight with savory meats', 1600, '../TemplateFiles/menuimages/img11.jpg', 1, 0, 4.1),
(10, 'Veggie Delight Pizza', 'Vegetarian pizza with fresh veggies', 1100, '../TemplateFiles/menuimages/img12.jpg', 1, 0, 4.6),
(11, 'Cheeseburger', 'Cheeseburger, lettuce and tomato', 800, '../TemplateFiles/menuimages/iimg1.jpg', 1, 0, 3.9),
(12, 'Chicken Sandwich', 'Grilled chicken sandwich with mayo', 900, '../TemplateFiles/menuimages/iimg2.jpg', 1, 0, 4.2),
(13, 'Spaghetti Bolognese', 'Italian pasta with meat sauce', 1200, '../TemplateFiles/menuimages/img10.jpg', 1, 0, 4.4),
(14, 'Caesar Salad', 'Fresh salad with Caesar dressing', 700, '../TemplateFiles/menuimages/img9.jpg', 1, 0, 4.0),
(15, 'Garlic Bread', 'Buttery garlic breadsticks & dips', 400, '../TemplateFiles/menuimages/img11.jpg', 1, 0, 3.7),
(16, 'French Fries', 'Crispy and golden french fries', 300, '../TemplateFiles/menuimages/iimg3.jpg', 1, 0, 4.8),
(17, 'Cheesecake',			'Creamy and rich cheesecake  ', 600		, '../TemplateFiles/menuimages/iimg4.jpg', 1, 0, 3.5),
(18, 'Chocolate Brownie',	'Decadent chocolate brownie  ', 500			, '../TemplateFiles/menuimages/iimg5.jpg', 1, 0, 4.0),
(19, 'Vanilla Ice Cream',	'Smooth and creamy vanilla ice cream'		, 300, '../TemplateFiles/menuimages/iimg6.jpg', 1, 0,3.0),
(20, 'Chocolate Ice Cream',	'Smooth and creamy ice cream'		, 300, '../TemplateFiles/menuimages/iimg7.jpg', 1, 0,4.0);
