# Student Enrollment System

A SQL Server database and a small C# WinForms app for managing students, courses, and grades.

## Files

- `Setup.sql` — creates the tables and adds sample data
- `Queries.sql` — the eleven required queries
- `[SQL]/` — the Visual Studio project

## Setting up the database

1. In SSMS, run: `CREATE DATABASE University;`
2. Open `Setup.sql` and run it.

The tables are Major, Student, Course, and Enrollment. Grade is stored in Enrollment because it belongs to a student in a specific course, not to the student or the course on its own.

**Note:** the query that deletes students with grades below 30 needs cascade delete on the Enrollment foreign key, otherwise SQL Server blocks it. You can set it in the table designer.