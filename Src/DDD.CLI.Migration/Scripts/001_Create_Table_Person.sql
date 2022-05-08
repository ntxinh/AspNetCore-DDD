CREATE TABLE Person (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    LastName varchar(255) NOT NULL,
    FirstName varchar(255),
    Age int,
    [CreatedAt] [DATETIME2] NOT NULL,
    [CreatedBy] [INT] NOT NULL,
    [UpdatedAt] [DATETIME2] NOT NULL,
    [UpdatedBy] [INT] NOT NULL,
    [Active] [BIT] NOT NULL,
    [IsDelete] [BIT] NOT NULL
);
