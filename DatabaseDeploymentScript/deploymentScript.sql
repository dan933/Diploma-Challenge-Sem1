USE DiplomaChallengeSem1;

DROP TABLE IF EXISTS PETSelection;
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

INSERT INTO [dbo].[Owner]
           ([Surname]
           ,[Firstname]
           ,[Phone])
     VALUES
           ('Albert', 'Daniel', '0'),
           ('Farid', 'Albert', '041234'),
           ('Fred','Smith', '0413568')
GO

INSERT INTO [dbo].[PET]
           ([PetName]
           ,[Type])
     VALUES
           ('Charlie', 'Dog'),
           ('Ollie', 'Cat'),
           ('catdog','CatDog')



--SELECT o.*, p.*
--FROM [Owner] AS O
--INNER JOIN PETSelection as ps ON ps.OwnerId = o.OwnerId
--INNER JOIN PET as p ON p.PetId = ps.PetId;
