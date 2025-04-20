--create Database 
create database CarConnectDB

--use CarConnect_DB
use CarConnectDB
--1.create Customer Table

create table  Customer(
CustomerId int identity(100,1) primary key not null,
FirstName varchar(50),
LastName varchar(50),
Email varchar(50),
Phone_number bigint,
Address varchar(255),
Username varchar(50),
Password varchar(50),
RegistrationDate date
)

--2.create Vechile Table

create table Vehicle(

VehicleId int identity(200,1) primary key not null,
Model Varchar(100),
Make Varchar(100),
Year int,
color varchar(30),
RegistrationNumber varchar(20),
Availability bit,
DailyRate Decimal(10,2)
)

--3.create Reservation Table

create table Reservation(
ReservationId int identity(300,1) primary key not null,
customerid int not null,
vehicleid int not null,
StartDate datetime,
EndDate datetime,
TotalCost decimal(10,2),
--Status
constraint fk_custm foreign key (CustomerID) references Customer(CustomerID),
constraint fk_vehc foreign key (vehicleid) references Vehicle(VehicleID)
)

--4. create Admin Table
create table  Admin(
 AdminId int identity(400,1) primary key not null,
FirstName varchar(50),
LastName varchar(50),
Email varchar(50),
Phone_number bigint,
Username varchar(50),
Password varchar(50),
Role varchar(50),
JoinDate date

)

select * from Vehicle


