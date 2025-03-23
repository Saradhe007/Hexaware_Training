--Create Database StudInfoSysDB
create database Stud_InfoSysDB

use Stud_InfoSysDB
--create a Student table

Create table Student_Db
(
student_id int identity(100,1) primary key not null, 
first_name varchar(50),
last_name varchar(50),
date_of_birth date,
email varchar(50),
phone_number bigint
)
select * from Student_Db
--create tabele courses

create table Course_Db
(
course_id int identity(200,1)primary key not null,
course_name varchar(50),
credits smallint,
teach_id int not null,
CONSTRAINT tec_Fky FOREIGN KEY (teach_id) REFERENCES Teacher_Db(teacher_id) 
)
select * from Course_DB
--create tabele Enrollment
create table Enrollment_Db
(
enrollment_id int identity (1000,1) primary key not null, 
stud_id int not null,
co_id int not null  ,
CONSTRAINT St_Frky FOREIGN KEY (stud_id) REFERENCES Student_Db(student_id) ,
CONSTRAINT Co_Frky FOREIGN KEY (co_id) REFERENCES Course_Db(course_id) ,
enrollment_date date 
)

--create tabele Teacher
create table Teacher_Db
(
teacher_id int identity(300,1) primary key not null,
first_name varchar(50),
last_name varchar(50),
t_email varchar(50),
)

--create tabele Payment
create table Payment_Db
(
payment_id int identity(500,1) primary key not null,
studt_id int not null,
CONSTRAINT Sd_Frky FOREIGN KEY (studt_id) REFERENCES Student_db(student_id) ,
amount decimal(10,2) not null,
payment_date date 
)

---inserting Student infromation in Student table
Insert into Student_db(first_name, last_name, date_of_birth, email, phone_number) values
('Liam', 'Green', '2003-03-14', 'liam.green@example.com', '9876543201'),
('Amelia', 'Adams', '2004-07-07', 'amelia.adams@example.com', '8765432102'),
('Noah', 'Baker', '2003-01-10', 'noah.baker@example.com', '7654321093'),
('Harper', 'Gonzalez', '2003-09-05', 'harper.gonzalez@example.com', '6543210984'),
('Benjamin', 'Nelson', '2004-12-30', 'benjamin.nelson@example.com', '5432109875'),
('Evelyn', 'Carter', '2003-06-18', 'evelyn.carter@example.com', '4321098766'),
('Mason', 'Mitchell', '2003-04-14', 'mason.mitchell@example.com', '3210987657'),
('Ella', 'Perez', '2004-08-08', 'ella.perez@example.com', '2109876548'),
('Logan', 'Roberts', '2004-05-27', 'logan.roberts@example.com', '1098765439'),
('Avery', 'Turner', '2003-02-12', 'avery.turner@example.com', '0987654320')

select * from Student_db

-- Inserting Course details

Insert into Course_db (course_name,credits,teach_id)
values ('Database Management Systems', 4,300),
('Operating Systems', 3,301),
('Machine Learning', 4,302),
('Artificial Intelligence', 4,303),
('Cloud Computing', 3,304),
('Web Development', 4,305),
('Data Structures and Algorithms', 4,306)

select * from Course_db

--inserting Enrollment details

insert into Enrollment_Db(stud_id,co_id,enrollment_date) 
values (100,201,'2022-06-01'),
(101,206,'2022-06-02'),
(102,202,'2022-06-03'),
(103,205,'2022-06-04'),
(104,204,'2022-06-05'),
(105,206,'2022-06-06'),
(106,206,'2022-06-07'),
(107,201,'2022-06-08'),
(108,203,'2022-06-09'),
(109,202,'2022-06-10')

select * from Enrollment_db

--inserting  Teachers details

insert into Teacher_db (first_name,last_name,t_email)
values ('Amit','Sharma','amit.sharma@gmail.com'),
('Priya','Verma','priya.verma@gmail.com'),
('Rahul','Gupta','rahul.gupta@gmail.com'),
('Sneha','Patel','sneha.patel@gmail.com'),
('Vikram','Singh','vikram.singh@gmail.com'),
('Meera','Nair','meera.nair@gmail.com'),
('Rohan','Das','rohan.das@gmail.com'),
('Anaya','Reddy','ananya.reddy@gmail.com'),
('Shreyas','iyer','shreyas.iyergmail.com'),
('Kavitha','Joshi','kavita.joshi@gmail.com')

select * from Teacher_Db

--inserting  Payments details

insert into Payment_db(studt_id, amount, payment_date)  
VALUES  
(100, 5000, '2022-07-01'),  
(101, 4500, '2022-07-05'),  
(102, 6000, '2022-06-28'),  
(103, 3200, '2022-07-10'),  
(104, 7000, '2022-06-12'),  
(105, 5500, '2022-06-15'),  
(106, 4800, '2022-07-18'),  
(107, 6200, '2022-07-20'),  
(108, 5000, '2022-07-22'),  
(109, 5300, '2022-06-25')

select * from Payment_db

-----Task 1 completed ----