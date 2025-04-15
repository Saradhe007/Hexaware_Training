create database StudentInfoDB;

use StudentInfoDB;

create table Student
(
StudentId int primary key not null,
FirstName varchar (100) not null ,
LastName varchar(100)not null,
DateOfBirth date not null,
Email varchar(50)not null,
PhoneNumber varchar(20)not null
)

create table Course
(
CourseId int primary key not null ,
CourseName Varchar(100)not null,
CourseCode varchar(20)not null,
InstructorName varchar(100)not null
)

create table Teacher
(
TeacherID int primary key not null,
FirstName varchar(100)not null,
LastName  varchar(100)not null,
Email varchar(50)not null
)

create table Enrollment
(
EnrollmentId int primary key not null,
StudentId int,
CourseId int,
EnrollmentDate date not null,
CONSTRAINT St_Frky FOREIGN KEY (StudentId) REFERENCES Student(StudentId) ,
CONSTRAINT Co_Frky FOREIGN KEY (CourseId) REFERENCES Course(CourseId) ,
)
create table Payment(
PaymentId int primary key not null,
StudentId int,
Amount decimal(10,2) not null,
PaymentDate date not null,
CONSTRAINT Stud_Frky FOREIGN KEY (StudentId) REFERENCES Student(StudentId) 
)

select * from Student
drop table Enrollment