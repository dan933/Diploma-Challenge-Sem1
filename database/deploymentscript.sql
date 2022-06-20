DROP TABLE IF EXISTS TREATMENT;
DROP TABLE IF EXISTS PET;
DROP TABLE IF EXISTS [OWNER];
DROP TABLE IF EXISTS [PROCEDURE];

GO 

CREATE TABLE OWNER(
    OwnerID INT IDENTITY(1,1) PRIMARY KEY,
    SurName NVARCHAR(300),
    FirstName NVARCHAR(300),
    Phone NVARCHAR(300)
    CONSTRAINT UN_OWNER UNIQUE (Phone)
);

GO

CREATE TABLE PET(
    ID INT IDENTITY(1,1) PRIMARY KEY,
    OwnerID INT FOREIGN KEY REFERENCES OWNER,
    PetName NVARCHAR(300),
    Type NVARCHAR(300)
    CONSTRAINT UN_PetOwner UNIQUE (OwnerID, PetName)
)

GO

CREATE TABLE [PROCEDURE](
    ID INT IDENTITY(1,1) PRIMARY KEY,
    ProcdeureID INT,
    Description NVARCHAR(300),
    Price DECIMAL
)

GO 

CREATE TABLE TREATMENT(
    ID INT IDENTITY(1,1) PRIMARY KEY,
    FK_PetID INT FOREIGN KEY REFERENCES PET,
    FK_ProcedureID INT FOREIGN KEY REFERENCES [PROCEDURE],
    Date DATETIME,
    Notes NVARCHAR(300),
    Payment DECIMAL
)

GO 

INSERT INTO [OWNER]
(
    Surname,
    FirstName,
    Phone
)

VALUES
    ('Sinatra', 'Frank', '400111222'),
    ('Ellington', 'Duke', '400222333'),
    ('Fitzgerald', 'Ella', '400333444');


GO 

INSERT INTO PET(
    OwnerID,
    PetName,
    Type
)
VALUES
    (1, 'Buster', 'Dog'),
    (1, 'Fluffy', 'Cat'),
    (2, 'Stew', 'Rabbit'),
    (3, 'Buster', 'Echidna');

GO

INSERT INTO [PROCEDURE](
    ProcdeureID,
    Description,
    Price
)

VALUES
    (1, 'Rabies Vaccination', '24.00 '),
    (10, 'Examine and Treat Wound', '30.00 '),
    (5, 'Heart Worm Test', '25.00 '),
    (8, 'Tetnus Vaccination', '17.00 ');

GO 

INSERT INTO TREATMENT(
    FK_PetID,
    FK_ProcedureID,
    Date,
    Notes,
    Payment
)

VALUES
    (1, 1, '20-Jun-17', 'Routine Vaccination', 22.00),
    (1, 1, '15-May-18', 'Booster Shot', 24.00 ),
    (2, 2, '10-May-18', 'Wounds sustained in apparent cat fight.', 30.00),
    (3, 2, '10-May-18', 'Wounds sustained during attempt to cook the stew.', 30.00),
    (4, 4, '20-Jun-17', 'Stepped on a Rusty Nail', 17.00),
    (4, 1, '20-Jun-17', 'Routine Vaccination', 22.00);