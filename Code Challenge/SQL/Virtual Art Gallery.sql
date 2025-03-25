--create a Database 
create database VAG_db

--use Database VAG_db
use VAG_db

--Create the Artists table 
create table Artists(
ArtistID int primary key,
Name varchar(255) not null,
Biography text,
Nationality VARCHAR(100)
)

-- Create the Categories table 
CREATE TABLE Categories ( 
CategoryID INT PRIMARY KEY, 
Name VARCHAR(100) NOT NULL)

-- Create the Artworks table 
CREATE TABLE Artworks ( 
ArtworkID INT PRIMARY KEY, 
Title VARCHAR(255) NOT NULL, 
ArtistID INT, 
CategoryID INT, 
Year INT, 
Description TEXT, 
ImageURL VARCHAR(255), 
FOREIGN KEY (ArtistID) REFERENCES Artists (ArtistID), 
FOREIGN KEY (CategoryID) REFERENCES Categories (CategoryID))

-- Create the Exhibitions table 
CREATE TABLE Exhibitions ( 
ExhibitionID INT PRIMARY KEY, 
Title VARCHAR(255) NOT NULL, 
StartDate DATE, 
EndDate DATE, 
Description TEXT)

-- Create a table to associate artworks with exhibitions 
CREATE TABLE ExhibitionArtworks ( 
ExhibitionID INT, 
ArtworkID INT, 
PRIMARY KEY (ExhibitionID, ArtworkID), 
FOREIGN KEY (ExhibitionID) REFERENCES Exhibitions (ExhibitionID), 
FOREIGN KEY (ArtworkID) REFERENCES Artworks (ArtworkID))

--DML(Insert data to tables)
-- Insert data into the Artists table 
INSERT INTO Artists (ArtistID, Name, Biography, Nationality) VALUES 
(1, 'Pablo Picasso', 'Renowned Spanish painter and sculptor.', 'Spanish'), 
(2, 'Vincent van Gogh', 'Dutch post-impressionist painter.', 'Dutch'), 
(3, 'Leonardo da Vinci', 'Italian polymath of the Renaissance.', 'Italian');

-- Insert data into the Categories table 
INSERT INTO Categories (CategoryID, Name) VALUES 
(1, 'Painting'), 
(2, 'Sculpture'), 
(3, 'Photography')

-- Insert data into the Artworks table 
INSERT INTO Artworks (ArtworkID, Title, ArtistID, CategoryID, Year, Description, ImageURL) VALUES 
(1, 'Starry Night', 2, 1, 1889, 'A famous painting by Vincent van Gogh.', 'starry_night.jpg'), 
(2, 'Mona Lisa', 3, 1, 1503, 'The iconic portrait by Leonardo da Vinci.', 'mona_lisa.jpg'), 
(3, 'Guernica', 1, 1, 1937, 'Pablo Picasso powerful anti-war mural.', 'guernica.jpg')

-- Insert data into the Exhibitions table 
INSERT INTO Exhibitions (ExhibitionID, Title, StartDate, EndDate, Description) VALUES 
(1, 'Modern Art Masterpieces', '2023-01-01', '2023-03-01', 'A collection of modern art masterpieces.'), 
(2, 'Renaissance Art', '2023-04-01', '2023-06-01', 'A showcase of Renaissance art treasures.') 

-- Insert artworks into exhibitions 
INSERT INTO ExhibitionArtworks (ExhibitionID, ArtworkID) VALUES 
(1, 1), 
(1, 2), 
(1, 3), 
(2, 2)

---queries

--1.Retrieve the names of all artists along with the number of artworks they have in the gallery

select a.Name, count(ar.ArtworkID) as Total_artwrks
from Artists a
join Artworks ar on a.ArtistID = ar.ArtistID
group by a.Name
order by Total_artwrks desc

--2.List the titles of artworks created by artists from 'Spanish' and 'Dutch' nationalities, and order in ascending order. 

select ar.Title , ar.Year
from Artworks ar
join Artists a on a.ArtistID = ar.ArtistID
where a.Nationality in('Spanish','Dutch')
order by ar.year asc

--3.Find the names of all artists who have artworks in the 'Painting' category

select a.Name ,count(ar.ArtworkID) as Total_paintings
from Artists a
join Artworks ar on a.ArtistID = ar.ArtistID

join Categories c on ar.CategoryID = c.CategoryID
where c.name='Painting' 
group by a.Name

--4.List the names of artworks from the 'Modern Art Masterpieces' exhibition 

select ar.Title , a.Name , c.Name as Categoryname
from Artworks ar
join Artists a on ar.ArtistID = a.ArtistID
join Categories c on ar.CategoryID = c.CategoryID
join ExhibitionArtworks ea on ar.ArtworkID = ea.ArtworkID
join Exhibitions e on ea.ExhibitionID = e.ExhibitionID

where e.Title = 'Modern Art Masterpieces'

--5.Find the artists who have more than two artworks in the gallery. 
select a.Name ,count(ar.ArtworkID) as Total_artworks
from Artists a
join Artworks ar on a.ArtistID=ar.ArtistID
group by a.Name
having count(ar.ArtworkID)>2

-- artists who have more than two artworks

--7.Find the total number of artworks in each category 

select c.Name as Category_name ,count(ar.ArtworkID) as Total_Artwork
from Categories c
left join Artworks ar on c.CategoryID = ar.CategoryID
group by c.Name

--8. List artists who have more than 3 artworks in the gallery.

select a.Name ,count(ar.ArtworkID) as Total_artworks
from Artists a
join Artworks ar on a.ArtistID=ar.ArtistID
group by a.Name
having count(ar.ArtworkID)>3

-- artists who have more than three artworks

--9.Find the artworks created by artists from a specific nationality (e.g., Spanish). 


select ar.title , a.Name , a.Nationality
from Artworks ar
join Artists a on ar.ArtistID = a.ArtistID
where a.Nationality = 'Spanish'

--10. List exhibitions that feature artwork by both Vincent van Gogh and Leonardo da Vinci.

select e.ExhibitionID,e.Title
from Exhibitions e
join ExhibitionArtworks ea on e.ExhibitionID =ea.ExhibitionID
join Artworks ar on ea.ArtworkID = ar.ArtworkID
join ExhibitionArtworks ea1 on e.ExhibitionID = ea1.ExhibitionID
join Artists a on ar.ArtistID =a.ArtistID

group by e.ExhibitionID , e.Title

--11. Find all the artworks that have not been included in any exhibition. 

--here inserting a new data in Artworks
Insert into Artworks (ArtworkID, Title, ArtistID, CategoryID, Year, Description, ImageURL) 
Values (4, 'Untitled Artwork', 1, 1, 2024, 'An unexhibited artwork.', 'untitled.jpg');

Select a.ArtworkID, a.Title, a.ArtistID, a.CategoryID, a.Year, a.Description
from Artworks a
left join ExhibitionArtworks ea on a.ArtworkID = ea.ArtworkID
where ea.ExhibitionID is null;


--12.List artists who have created artworks in all available categories. 

select a.ArtistID, ar.Name
from Artworks a
join Artists ar on a.ArtistID = ar.ArtistID
group by a.ArtistID , ar.Name
having count(distinct a.CategoryID) = (select count(*) from Categories)

--No artists have created artworks in all available categories. 

--13 .List the total number of artworks in each category.
select c.CategoryID, c.Name as CategoryName, Count(a.ArtworkID) as TotalArtworks
from Categories c
left join Artworks a On c.CategoryID = a.CategoryID
Group By c.CategoryID, c.Name

--14. Find the artists who have more than 2 artworks in the gallery.
Select a.ArtistID, a.Name, COUNT(ar.ArtworkID) AS TotalArtworks
FROM Artists a
JOIN Artworks ar ON a.ArtistID = ar.ArtistID
GROUP BY a.ArtistID, a.Name
HAVING COUNT(ar.ArtworkID) > 2

--15. List the categories with the average year of artworks they contain, only for categories with more than 1 artwork.

Select c.CategoryID, c.Name as CategoryName, Avg(a.Year) as AvgYear
From Categories c
join Artworks a on c.CategoryID = a.CategoryID
Group by c.CategoryID, c.Name
having Count(a.ArtworkID) > 1

--16.Find the artworks that were exhibited in the 'Modern Art Masterpieces' exhibition. 

Select ar.ArtworkID, ar.Title, a.Name as ArtistName, c.Name as CategoryName
From Artworks ar
join Artists a ON ar.ArtistID = a.ArtistID
join Categories c ON ar.CategoryID = c.CategoryID
join ExhibitionArtworks ea ON ar.ArtworkID = ea.ArtworkID
join Exhibitions e ON ea.ExhibitionID = e.ExhibitionID
Where e.Title = 'Modern Art Masterpieces'

--17.Find the categories where the average year of artworks is greater than the average year of all artworks. 

Select c.CategoryID, c.Name as CategoryName, Avg(ar.Year) as AvgCategoryYear
From Categories c
join Artworks ar on c.CategoryID = ar.CategoryID
group by c.CategoryID, c.Name
having Avg(ar.Year) > (select Avg(Year) From Artworks)

--18.List the artworks that were not exhibited in any exhibition. 
select ar.ArtworkID, ar.Title, a.Name as ArtistName, c.Name as CategoryName
from Artworks ar
join Artists a on ar.ArtistID =a.ArtistID
join Categories c on ar.CategoryID=c.CategoryID
left join ExhibitionArtworks ea ON ar.ArtworkID = ea.ArtworkID
where ea.ExhibitionID is null

--19.Show artists who have artworks in the same category as "Mona Lisa." 
select distinct a.ArtistID , a.Name as ArtistName
from Artists a
join Artworks ar on a.ArtistID=ar.ArtistID
where ar.CategoryID = (
select CategoryID from Artworks where Title='Mona Lisa')

--20.List the names of artists and the number of artworks they have in the gallery. 

select a.name as ArtistName , count(ar.ArtworkID) as ArtworkCount
from Artists a
left join Artworks ar on a.ArtistID = ar.ArtistID
group by ar.ArtworkID , a.Name
order by ArtworkCount desc


----Code Challenge completed -----