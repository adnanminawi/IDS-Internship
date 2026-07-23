USE University;

SELECT C.Course_Name, M.Major_Name
FROM Course C
INNER JOIN Major M ON M.Major_id = C.Major_id
WHERE M.Major_Name= 'Computer Science';

SELECT S.Student_Name, C.Course_Name
FROM Student S
INNER JOIN Enrollment E ON E.Student_id = S.Student_id
INNER JOIN Course C ON C.Course_id = E.Course_id
WHERE C.Course_Name ='Digital Marketing';

SELECT S.Student_Name, M.Major_Name
FROM Student S
INNER JOIN Major M ON M.Major_id = S.Major_id
WHERE M.Major_Name = 'Computer Science'
ORDER BY S.Student_Name;

SELECT S.Student_Name,C.Course_Name, E.Grade
FROM Student S
INNER JOIN Enrollment E ON E.Student_id = S.Student_id
INNER JOIN Course C ON C.Course_id= E.Course_id
WHERE C.Course_Name = 'JAVA' AND E.Grade >= 50;

SELECT S.Student_Name, C.Course_Name, E.Grade
FROM Student S
INNER JOIN Enrollment E ON E.Student_id = S.Student_id
INNER JOIN Course	C ON C.Course_id = E.Course_id
WHERE E.Grade <50 AND C.Course_Name = 'Chemistry Lab';

SELECT DISTINCT E.Grade, C.Course_Name
FROM Enrollment E
INNER JOIN Course C ON C.Course_id = E.Course_id
WHERE C.Course_Name = 'JAVA';

SELECT COUNT (*) AS PassedCount
FROM Student S
INNER JOIN Enrollment E ON E.Student_id= S.Student_id
INNER JOIN Course C ON C.Course_id = E.Course_id
WHERE E.Grade >=50 AND C.Course_Name ='JAVA';

SELECT S.Student_Name, C.Course_Name
FROM Student S
INNER JOIN Enrollment E ON E.Student_id= S.Student_id
INNER JOIN Course C ON C.Course_id = E.Course_id
WHERE E.Grade IS NULL;

UPDATE Enrollment
SET Grade = 50
WHERE Grade BETWEEN 47 AND 49;

DELETE FROM Student	
WHERE Student_id IN (SELECT Student_id FROM Enrollment WHERE Grade< 30);

UPDATE Student
SET AverageGrade = (
    SELECT AVG(Grade * 1.0)
    FROM Enrollment
    WHERE Enrollment.Student_id = Student.Student_id
);