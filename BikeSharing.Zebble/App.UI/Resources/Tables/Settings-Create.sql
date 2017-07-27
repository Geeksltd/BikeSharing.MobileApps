-- Settings Table ========================
CREATE TABLE [Settings] (
    [Id] uniqueidentifier PRIMARY KEY,
    [Name] nvarchar(200)  NOT NULL,
    [UserIdKey] int  NOT NULL
)
