USE DiplomaChallengeSem1;

DROP VIEW IF EXISTS view_PROCEDURE;
DROP TABLE IF EXISTS ProcedureSelection;
DROP TABLE IF EXISTS TREATMENT;
DROP TABLE IF EXISTS PETSelection;
DROP TABLE IF EXISTS [PROCEDURE];
DROP TABLE IF EXISTS PET;
DROP TABLE IF EXISTS [Owner];

GO

CREATE TABLE [OWNER]
(   
    ID INT IDENTITY(1,1) PRIMARY Key,
    OwnerId INT,
    Surname NVARCHAR(300) NOT NULL,
    Firstname NVARCHAR(300) NOT NULL,
    Email NVARCHAR(300),
    Phone NVARCHAR(300) NOT NULL
);

GO

CREATE TABLE PET
(
    OwnerId INT FOREIGN KEY REFERENCES [OWNER],    
    PetName NVARCHAR(300),
    [Type] NVARCHAR(300)
    Constraint PK_PET Primary Key (OwnerId, PetName)
);

GO

CREATE TABLE [PROCEDURE]
(
    ProcedureID INT PRIMARY KEY,
    [Description] NVARCHAR(300),
    Price MONEY
);

GO

CREATE TABLE TREATMENT
(
    ID INT IDENTITY(1,1) PRIMARY KEY,
    OwnerID INT,
    PetName NVARCHAR(300),
    ProcedureID INT FOREIGN key REFERENCES [PROCEDURE],
    [Date] DATE,
    Notes NVARCHAR(300),
    Payment MONEY
    Constraint FK_PET FOREIGN Key (OwnerID, PetName) REFERENCES PET
);

GO


CREATE VIEW view_PROCEDURE AS 
SELECT t.OwnerID,t.PetName,t.[Date],p.ProcedureID, p.[Description], p.Price
FROM TREATMENT AS T
INNER JOIN [PROCEDURE] AS P ON T.ProcedureID = P.ProcedureID;

GO



INSERT INTO [OWNER] (OwnerID,Surname,Firstname,Phone )
VALUES
    ('1', 'Sinatra', 'Frank', '400111222 '),
    ('2', 'Ellington', 'Duke', '400222333 '),
    ('3', 'Fitzgerald', 'Ella', '400333444 ');

GO
INSERT INTO [dbo].[PROCEDURE]
			([ProcedureID], [Description],[Price])
VALUES
	(1, 'Rabies Vaccination', 24.00 ),
	(10, 'Examine and Treat Wound', 30.00),
	(5, 'Heart Worm Test', 25.00 ),
    (8, 'Heart Worm Test', 25.00 );
           
GO

INSERT INTO PET (OwnerID,PetName,Type )
VALUES
    (1, 'Buster', 'Dog '),
    (1, 'Fluffy', 'Cat '),
    (2, 'Stew', 'Rabbit '),
    (3, 'Buster', 'Echidna ');

GO

INSERT INTO TREATMENT ([OwnerID],[PetName],[ProcedureID],[Date],[Notes],[Payment] )
VALUES
    (1, 'Buster', 1, '20-Jun-17', 'Routine Vaccination', ' 22.00  '),
    (1, 'Buster', 1, '15-May-18', 'Booster Shot', ' 24.00  '),
    (1, 'Fluffy', 10, '10-May-18', 'Wounds sustained in apparent cat fight.', ' 30.00  '),
    (2, 'Stew', 10, '10-May-18', 'Wounds sustained during attempt to cook the stew.', ' 30.00  '),
    (3, 'Buster', 8, '20-Jun-17', 'Stepped on a Rusty Nail', ' 17.00  ');
