--use Stud_InfoSysDB
use Stud_InfoSysDB
--Task 3
--Aggregate functions, Having, Order By, GroupBy and Joins: 

--1. Write an SQL query to calculate the total payments made by a specific student. You will need to join the "Payments" table with the "Students" table based on the student's ID.
select * from Payment_Db
select s.student_id , s.first_name,s.last_name , Sum(p.amount) as Total_payments
From Student_Db as s
join Payment_Db p on s.student_id = p.studt_id
where s.student_id = 102
Group by  s.student_id , s.first_name,s.last_name
select * from Enrollment_Db

--2.Write an SQL query to retrieve a list of courses along with the count of students enrolled in each course. Use a JOIN operation between the "Courses" table and the "Enrollments" table. 
select * from Course_db

select c.course_id, c.course_name ,count(e.stud_id) as Student_Count
from Course_Db c
left join Enrollment_Db e on c.course_id = e.co_id
group by c.course_id, c.course_name 
order by Student_count Desc

--3.Write an SQL query to find the names of students who have not enrolled in any course. Use a LEFT JOIN between the "Students" table and the "Enrollments" table to identify students 
--without enrollments.

select s.student_id , s.first_name,s.last_name
From Student_Db s
left join Enrollment_Db e on s.student_id = e.stud_id
where e.stud_id is null
Select * From Enrollment_Db

--4.Write an SQL query to retrieve the first name, last name of students, and the names of the courses they are enrolled in. Use JOIN operations between the "Students" table and the 
--"Enrollments" and "Courses" tables.
select s.first_name,s.last_name , c.course_name
From Student_Db s
join Enrollment_Db e on s.student_id = e.stud_id
join Course_Db c on e.co_id = c.course_id

--5.Create a query to list the names of teachers and the courses they are assigned to. Join the 
--"Teacher" table with the "Courses" table. 
select*from  Teacher_Db
select*from Course_Db

select t.first_name ,t.last_name, c.course_name
from Teacher_Db t
join Course_Db c on t.teacher_id = c.teach_id

--6.Retrieve a list of students and their enrollment dates for a specific course. You'll need to join the "Students" table with the "Enrollments" and "Courses" tables. 

select s.student_id , s.first_name ,s.last_name , e.enrollment_date , c.course_name
from Student_Db s
join Enrollment_Db e on s.student_id = e.stud_id 
join Course_Db c on e.co_id   = c.course_id
where c.course_name='Database Management Systems'
--7.Find the names of students who have not made any payments. Use a LEFT JOIN between the "Students" table and the "Payments" table and filter for students with NULL payment records. 

select s.student_id , s.first_name ,s.last_name
from Student_Db s
left join Payment_Db p on s.student_id = p.studt_id
where p.payment_id is null
--8.Write a query to identify courses that have no enrollments. You'll need to use a LEFT JOIN between the "Courses" table and the "Enrollments" table and filter for courses with NULL 
--enrollment records.

select c.course_id , c.course_name
from Course_Db c
left join Enrollment_Db e on c.course_id = e.co_id
where e.enrollment_id is null

--9.Identify students who are enrolled in more than one course. Use a self-join on the "Enrollments" table to find students with multiple enrollment records. 


update Enrollment_Db set stud_id = 100 where co_id = 202


select en.stud_id , s.first_name ,s.last_name,count(en.co_id) as Count_course
from Enrollment_Db en
join Student_Db s on en.stud_id = s.student_id
group by en.stud_id , s.first_name ,s.last_name
having count(en.co_id)>1

--Find teachers who are not assigned to any courses. Use a LEFT JOIN between the "Teacher" table and the "Courses" table and filter for teachers with NULL course assignments. 

select t.teacher_id,t.first_name,t.last_name
from Teacher_Db t
left join Course_Db c
on t.teacher_id=c.teach_id
where c.course_id is null
---Task 3 Completed---