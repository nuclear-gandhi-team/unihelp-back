# UniHelp-back
Back-end part of UniHelp web application.

Api base path: https://unihelpback.azurewebsites.net/swagger/index.html

The main goal of the project is to create a centralized electronic space for all members of the university community. Simplified access to basic information will allow students and teachers to effectively plan their time and work tasks. 

## Tasks
### Task 1 : User Data Management System
Displaying the list of students, search and sort by different criteria.
Creating, storing, and editing information about each student, including basic personal data, and educational success data such as grades for credit and exams, etc.

### Task 2 : Student Database Analytics System
Assessment and analysis of academic performance of students based on their grades, rankings, and participation in activities.
Assessment and analysis of student attendance.

### Task 3 : Commenting Tasks
Ability to comment tasks that are finished. With replies, quotes, ability for teachers to delete comments of students.

### Task 4 : Online Testing & Assignment System
Providing the ability for teachers to create tests and lab works of different subjects and themes.
Providing the ability for students to undergo online testing according to the schedule and terms.
Providing the teacher's ability to give matks for assignments and to check and change attendance.

## Installation
1) Ensure you have .NET Core SDK installed. (Or any required SDK or software)
2) Clone the repository: git clone https://github.com/username/project.git
3) Go into the project directory: cd project
4) Install the project dependencies:
5) If a .NET Project, restore the packages: dotnet restore
6) If a Node.js project, install node modules: npm install
7) Build the project: dotnet build or npm build
8) Setup the database (replace with the proper user, password and other necessary parameters)
dotnet ef database update
9) Run the project: dotnet run or npm start

## # Technologies used:
- ASP.NET Core
- EF Core
- MS SQL Server
- Identity framework

N-Layered architecture was used, so the Solution is divided into 4 projects
- Unihelp.Application
- Unihelp.Domain
- Unihelp.Persistence
- Unihelp.WebAPI

Deployed to Azure.


## Usage
1) Registration :
register-teacher:
```json
{
  "firstName": "Name1",
  "lastName": "Surname1",
  "email": "NameSurname1@example.com",
  "password": "sS123.1",
  "confirmPassword": "sS123.1"
}
```

register-student:
```json

{
  "firstName": "student2",
  "lastName": "student2",
  "email": "userStudent2@example.com",
  "password": "Qqw12.",
  "confirmPassword": "Qqw12.",
  "faculty": "FTI",
  "group": "MI-32",
  "course": 28
}
```


2) Login:
depending on user role, student and teacher will have different abilities in our
```json
{
  "email": "userStudent2@example.com",
  "password": "Qqw12."
}
```

result:
```json
{
  "token": "Bearer...",
  "role": "Student"
}
```
3) Update personal info (user should be authowized)
update-name
```json
{
  "newFirstName": "student3",
  "newLastName": "student3"
}
```
update-password
```json
{
  "oldPassword": "Qqw12.",
  "newPassword": "Qqw12.11",
  "confirmNewPassword": "Qqw12.11"
}
```

4) ...































