INSERT INTO Lecturers (Name, Surname, Degree)
VALUES ('Prof. Dr.', 'Schmidt', 'Professor'),
       ('Dr.', 'Müller', 'Professor');


INSERT INTO Semesters (Name, StartDate, EndDate)
VALUES ('Wintersemester 2024/2025', '2024-10-01', '2025-03-31'),
       ('Sommersemester 2025', '2025-04-01', '2025-09-30');


-- Get the IDs of the previously inserted lecturers (assuming auto-increment)
SELECT id FROM Lecturers WHERE Name = 'Prof. Dr.' AND Surname = 'Schmidt';
SET @profSchmidtId = LAST_INSERT_ID();

SELECT id FROM Lecturers WHERE Name = 'Dr.' AND Surname = 'Müller';
SET @drMuellerId = LAST_INSERT_ID();

-- Insert courses with lecturer IDs
INSERT INTO Courses (Name, Description, Startdate, Enddate, LecturerId)
VALUES ('Software Engineering', 'Grundlagen der Softwareentwicklung', '2024-10-01', '2025-02-28', @profSchmidtId),
       ('Datenbanken', 'Datenbankkonzepte und SQL', '2024-10-15', '2025-01-31', @drMuellerId);

-- Get the ID of the previously inserted winter semester (assuming auto-increment)
SELECT id FROM Semesters WHERE Name = 'Wintersemester 2024/2025';
SET @winterSemesterId = LAST_INSERT_ID();

INSERT INTO Students (Name, Surname, Birthdate, SemesterId)
VALUES ('Max', 'Mustermann', '2000-01-15', @winterSemesterId),
       ('Erika', 'Musterfrau', '2001-05-08', @winterSemesterId);
