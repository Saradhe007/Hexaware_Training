--use Stud_InfoSysDB
use Stud_InfoSysDB
--Task 4 
--Subquery and its type: 
--1.Write an SQL query to calculate the average number of students enrolled in each course. Use aggregate functions and subqueries to achieve this.

select avg(Student_count) as Avg_Stud_Course
from( select co_id , count(stud_id) as Student_Count
from Enrollment_Db 
Group by co_id
) as Course_Enrollment

--2.Identify the student(s) who made the highest payment. Use a subquery to find the maximum payment amount and then retrieve the student(s) associated with that amount.

select s.student_id , s.first_name,s.last_name , p.amount as Payment_amount
from Student_Db s
join Payment_Db p on s.student_id=p.studt_id
where p.amount=(select max(amount)from Payment_Db)

--3.Retrieve a list of courses with the highest number of enrollments. Use subqueries to find the course(s) with the maximum enrollment count

select c.course_id, c.course_name, count(e.stud_id) as enrollment_count
from Course_Db c
join Enrollment_Db e on c.course_id = e.co_id
group by c.course_id, c.course_name
having count(e.stud_id) = (select max(course_enrollment_count)
from (select count(stud_id) as course_enrollment_count
from Enrollment_Db group by co_id) as enrollment_counts
)
--4.Calculate the total payments made to courses taught by each teacher. Use subqueries to sum payments for each teacher's courses. 

select t.teacher_id,t.first_name ,t.last_name,(select sum(p.amount) from Payment_Db p
join Enrollment_Db e on p.studt_id = e.stud_id
where e.co_id in(select c.course_id FROM Course_Db c WHERE c.teach_id = t.teacher_id)
)as Total_payment
from Teacher_Db t

--5.Identify students who are enrolled in all available courses. Use subqueries to compare a student's enrollments with the total number of courses. 

select s.student_id , s.first_name,s.last_name 
From Student_Db as s
join Enrollment_Db e on s.student_id = e.stud_id
group by s.student_id , s.first_name,s.last_name 
having count(e.co_id) = (select count(*)from Course_Db)

--6.Retrieve the names of teachers who have not been assigned to any courses. Use subqueries to find teachers with no course assignments. 
select t.teacher_id, t.first_name,t.last_name
From Teacher_Db t
Where NOT EXISTS 
(
select 1 
from Course_Db c 
where c.teach_id = t.teacher_id
)
--7.Calculate the average age of all students. Use subqueries to calculate the age of each student based on their date of birth. 

select avg(datediff (year,date_of_birth,getdate())) as average_age
from Student_Db
--8.Identify courses with no enrollments. Use subqueries to find courses without enrollment records.

select course_id,course_name
from Course_Db c
where not exists (
select 1 from Enrollment_Db e where e.co_id=c.course_id)
--9.Calculate the total payments made by each student for each course they are enrolled in. Use ubqueries and aggregate functions to sum payments.


select s.student_id, s.first_name, s.last_name, c.course_id, c.course_name, 
(select sum(p.amount) 
from Payment_Db p 
join Enrollment_Db e on p.studt_id = e.stud_id
where e.stud_id = s.student_id and e.co_id = c.course_id
) as Total_payments
from Student_Db s
join Enrollment_Db e on s.student_id = e.stud_id
join Course_Db c on e.co_id = c.course_id
group by s.student_id, s.first_name, s.last_name, c.course_id, c.course_name
--10. Identify students who have made more than one payment. Use subqueries and aggregate functions to count payments per student and filter for those with counts greater than one.

select student_id
from Student_Db 
where student_id in ( select p.studt_id from Payment_Db p group by p.studt_id having count(p.amount) > 1)

-- no student has made multiple payments.

--11.Write an SQL query to calculate the total payments made by each student. Join the "Students" table with the "Payments" table and use GROUP BY to calculate the sum of payments for each 
--student.
select s.student_id, s.first_name, s.last_name, sum(p.amount) as Total_payments
FROM Student_Db s
JOIN Payment_Db p ON s.student_id = p.studt_id
GROUP BY s.student_id, s.first_name, s.last_name

--12.Retrieve a list of course names along with the count of students enrolled in each course. Use JOIN operations between the "Courses" table and the "Enrollments" table and GROUP BY to 
--count enrollments. 

select c.course_name,count(e.stud_id) as Stud_count
from Course_Db c
join Enrollment_Db e on c.course_id = e.co_id
group by c.course_name

--13.Calculate the average payment amount made by students. Use JOIN operations between the 
--"Students" table and the "Payments" table and GROUP BY to calculate the average

select s.student_id, s.first_name, s.last_name, avg(p.amount) as Avg_payment
from Student_Db s 
join Payment_Db p  on s.student_id = p.studt_id
group by s.student_id, s.first_name, s.last_name

-----Task 4  completed ----