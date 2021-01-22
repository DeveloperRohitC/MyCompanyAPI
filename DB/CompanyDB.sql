Create database MyCompany

use MyCompany

create table Department
(
DepartmentID int identity(1,1) primary key,
DepartmentName varchar(100) not null
)

select * from Department

--insert into Department (DepartmentName) values ('Support')


create table Employee
(
EmployeeID int identity(1,1) primary key,
EmployeeName varchar(100) not null,
Department varchar(100),
DateOfJoining date,
PhotoFileName varchar(500)
)

select * from Employee

--insert into Employee values ('Rohit','Support','2020-06-10','pic.png')

