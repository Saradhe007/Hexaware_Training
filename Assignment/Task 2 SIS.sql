--use Stud_InfoSysDB
use Stud_InfoSysDB

--Task2
--Select, Where, Between, AND, LIKE:  
--1. Write an SQL query to insert a new student into the "Students" table with the following details: 
-- First Name: John Last Name: Doe  Date of Birth: 1995-08-15 john.doe@example.com  Phone Number: 1234567890'

Insert into Student_Db  (first_name, last_name, date_of_birth, email, phone_number) 
values('John','Doe','1995-08-15','john.doe@example.com','1234567890')

select * from Student_Db

--2. Write an SQL query to enroll a student in a course. Choose an existing student and course and 
--insert a record into the "Enrollments" table with the enrollment date. 

insert into Enrollment_Db( stud_id,co_id,enrollment_date )
values (110,204,'2022-07-10')

select * from Student_Db where student_id = 110
select * from Course_Db where course_id= 204

--3.Update the email address of a specific teacher in the "Teacher" table. Choose any teacher and modify their email address. 

update Teacher_Db set t_email = 'ananya.reddy200@gmail.com' where teacher_id = 307
select * from Teacher_Db

--4.Write an SQL query to delete a specific enrollment record from the "Enrollments" table. Select an enrollment record based on the student and course.


delete from Enrollment_Db where stud_id = 109 and co_id=202
select * from Enrollment_Db

--5.Update the "Courses" table to assign a specific teacher to a course. Choose any course and teacher from the respective tables. 
select * from Course_Db
select * from Teacher_Db

update course_Db set teach_id = 301 where course_id = 206   

--6 . Delete a specific student from the "Students" table and remove all their enrollment records from the "Enrollments" table. Be sure to maintain referential integrity. 

delete from Student_Db where student_id=105
Delete from Enrollment_Db where stud_id =105 
Delete from Payment_db where studt_id =105
--7.Update the payment amount for a specific payment record in the "Payments" table. Choose any payment record and modify the payment amount.
select * from Payment_db
update Payment_Db set amount =4500 where payment_id = 503


-----Task 2 completed ----







