-- Azure SQL Database deployment script
-- Run this script against your Azure SQL Database
-- Database is already created by Azure, we just need to create tables and seed data

-- Create Dogs table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Dogs' AND xtype='U')
BEGIN
    CREATE TABLE Dogs (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Breed NVARCHAR(50) NOT NULL,
        Age INT NOT NULL,
        Size NVARCHAR(20) NOT NULL,
        Color NVARCHAR(20) NOT NULL,
        IsGoodWithKids BIT NOT NULL,
        IsGoodWithOtherPets BIT NOT NULL,
        EnergyLevel INT NOT NULL,
        Description NVARCHAR(500),
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
    );
END
GO

-- Create Cats table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Cats' AND xtype='U')
BEGIN
    CREATE TABLE Cats (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Breed NVARCHAR(50) NOT NULL,
        Age INT NOT NULL,
        Color NVARCHAR(20) NOT NULL,
        IsIndoor BIT NOT NULL,
        IsDeclawed BIT NOT NULL,
        IsGoodWithKids BIT NOT NULL,
        IsGoodWithOtherPets BIT NOT NULL,
        PlayfulnessLevel INT NOT NULL,
        Description NVARCHAR(500),
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
    );
END
GO

-- Create NamedLists table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='NamedLists' AND xtype='U')
BEGIN
    CREATE TABLE NamedLists (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Description NVARCHAR(500),
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
    );
END
GO

-- Create DogLists table (many-to-many relationship)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DogLists' AND xtype='U')
BEGIN
    CREATE TABLE DogLists (
        DogId INT NOT NULL,
        NamedListId INT NOT NULL,
        AddedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        PRIMARY KEY (DogId, NamedListId),
        FOREIGN KEY (DogId) REFERENCES Dogs(Id) ON DELETE CASCADE,
        FOREIGN KEY (NamedListId) REFERENCES NamedLists(Id) ON DELETE CASCADE
    );
END
GO

-- Create CatLists table (many-to-many relationship)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CatLists' AND xtype='U')
BEGIN
    CREATE TABLE CatLists (
        CatId INT NOT NULL,
        NamedListId INT NOT NULL,
        AddedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        PRIMARY KEY (CatId, NamedListId),
        FOREIGN KEY (CatId) REFERENCES Cats(Id) ON DELETE CASCADE,
        FOREIGN KEY (NamedListId) REFERENCES NamedLists(Id) ON DELETE CASCADE
    );
END
GO

-- Seed data for Dogs (only if table is empty)
IF NOT EXISTS (SELECT 1 FROM Dogs)
BEGIN
    INSERT INTO Dogs (Name, Breed, Age, Size, Color, IsGoodWithKids, IsGoodWithOtherPets, EnergyLevel, Description)
    VALUES
        ('Max', 'Golden Retriever', 3, 'Large', 'Golden', 1, 1, 8, 'Friendly and energetic dog, loves to play fetch and swim.'),
        ('Bella', 'Labrador', 2, 'Large', 'Black', 1, 1, 9, 'Very active and loves children. Great family pet.');
END

-- Seed data for Cats (only if table is empty)
IF NOT EXISTS (SELECT 1 FROM Cats)
BEGIN
    INSERT INTO Cats (Name, Breed, Age, Color, IsIndoor, IsDeclawed, IsGoodWithKids, IsGoodWithOtherPets, PlayfulnessLevel, Description)
    VALUES
        ('Whiskers', 'Persian', 2, 'White', 1, 0, 1, 1, 5, 'Calm and fluffy, loves to be brushed and petted.'),
        ('Luna', 'Siamese', 1, 'Cream', 1, 0, 1, 0, 9, 'Very vocal and playful, loves attention.');
END

-- Seed data for NamedLists (only if table is empty)
IF NOT EXISTS (SELECT 1 FROM NamedLists)
BEGIN
    INSERT INTO NamedLists (Name, Description)
    VALUES
        ('Family Pets', 'Pets that are great with children and families');
END

-- Seed data for DogLists (associating dogs with named lists)
IF NOT EXISTS (SELECT 1 FROM DogLists)
BEGIN
    DECLARE @FamilyPetsId INT = (SELECT Id FROM NamedLists WHERE Name = 'Family Pets');

    INSERT INTO DogLists (DogId, NamedListId)
    SELECT Id, @FamilyPetsId FROM Dogs WHERE Name IN ('Max', 'Bella');
END

-- Seed data for CatLists (associating cats with named lists)
IF NOT EXISTS (SELECT 1 FROM CatLists)
BEGIN
    DECLARE @FamilyPetsId2 INT = (SELECT Id FROM NamedLists WHERE Name = 'Family Pets');

    INSERT INTO CatLists (CatId, NamedListId)
    SELECT Id, @FamilyPetsId2 FROM Cats WHERE Name = 'Whiskers';
END

GO