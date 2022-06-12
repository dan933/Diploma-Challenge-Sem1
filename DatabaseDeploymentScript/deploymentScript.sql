USE DiplomaChallengeSem1;

DROP TABLE IF EXISTS ProcedureSelection;
DROP TABLE IF EXISTS TREATMENT;
DROP TABLE IF EXISTS PETSelection;
DROP TABLE IF EXISTS [PROCEDURE];
DROP TABLE IF EXISTS PET;
DROP TABLE IF EXISTS [Owner];

GO

CREATE TABLE [Owner]
(
    OwnerId INT IDENTITY(1,1) PRIMARY KEY,
    Surname NVARCHAR(MAX) NOT NULL,
    Firstname NVARCHAR(MAX) NOT NULL,
    Phone NVARCHAR(MAX) NOT NULL
);

GO

CREATE TABLE PET
(
    PetId INT IDENTITY(1,1) PRIMARY KEY,
    PetName NVARCHAR(MAX),
    [Type] NVARCHAR(MAX)
);

GO 

CREATE TABLE PETSelection
(
    PetSelectionId INT IDENTITY(1,1) PRIMARY KEY,
    OwnerId INT FOREIGN KEY REFERENCES [OWNER],
    PetId INT FOREIGN KEY REFERENCES PET,
    CONSTRAINT AK_PETSelection UNIQUE(PetId)
    
);

GO

CREATE TABLE [PROCEDURE]
(
    ProcedureID INT IDENTITY(1,1) PRIMARY KEY,
    [DESCRIPTION] NVARCHAR(MAX),
    Price FLOAT    
);

GO

CREATE TABLE TREATMENT
(
    TreatmentID INT IDENTITY(1,1) PRIMARY KEY,    
    PetId INT FOREIGN KEY REFERENCES PETSelection,
    ProcedureID INT FOREIGN KEY REFERENCES [PROCEDURE],
    [Date] DATE,
    Notes NVARCHAR(MAX),    
    Payment FLOAT    
);

GO

INSERT INTO [dbo].[Owner]
           ([Surname]
           ,[Firstname]
           ,[Phone])
     VALUES
           ('Sinatra', 'Frank', '400111222'),
           ('Ellington', 'Duke', '400222333'),
           ('Fitzgerald', 'Ella', '400333444')
GO

INSERT INTO [dbo].[PET]
           ([PetName]
           ,[Type])
     VALUES
           ('Buster', 'Dog'),
           ('Fluffy', 'Cat'),
           ('Stew', 'Rabbit'),
           ('Buster', 'Echidna')

INSERT INTO [dbo].[PROCEDURE]
           ([DESCRIPTION]
           ,[Price])
     VALUES
           ('Rabies Vaccination',24.00),
           ('Examine and Treat Wound', 30),
           ('Heart Worm Test', 25),
           ('Tetnus Vaccination', 17)
GO

INSERT INTO [dbo].[PETSelection]
           ([OwnerId]
           ,[PetId])
     VALUES
           (1, 1),
           (1, 2),
           (2, 3),
           (3, 4)
GO

INSERT INTO [dbo].[TREATMENT]
           (         
            [PetId]
            ,[ProcedureID]
            ,[Date]
            ,[Notes]
            ,[Payment]
           )
     VALUES
        (1, 1, '20-Jun-17', 'Routine Vaccination', ' 22.00  '),
        (1, 2, '15-May-18', 'Booster Shot', ' 24.00  '),
        (2, 2, '10-May-18', 'Wounds sustained in apparent cat fight.', ' 30.00  '),
        (3, 2, '10-May-18', 'Wounds sustained during attempt to cook the stew.', ' 30.00  '),
        (4, 4, '20-Jun-17', 'Stepped on a Rusty Nail', ' 17.00  '),
        (4, 1, '20-Jun-17', 'Routine Vaccination', ' 22.00  ');


--SELECT o.*, p.*
--FROM [Owner] AS O
--INNER JOIN PETSelection as ps ON ps.OwnerId = o.OwnerId
--INNER JOIN PET as p ON p.PetId = ps.PetId;
