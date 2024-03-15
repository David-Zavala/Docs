CREATE TABLE [dbo].[Docs] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max) NOT NULL,
    [Email] [nvarchar](128) NOT NULL,
    [BirthDate] [date] NOT NULL,
    [Age] [int] NOT NULL,
    [Education] [nvarchar](max) NOT NULL,
    [DocPath] [nvarchar](max) NOT NULL,
    [LastUpdate] [date] NOT NULL,
    CONSTRAINT [PK_dbo.Docs] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_Email] ON [dbo].[Docs]([Email])
CREATE TABLE [dbo].[Users] (
    [Email] [nvarchar](128) NOT NULL,
    [Password] [nvarchar](max) NOT NULL,
    [Name] [nvarchar](max) NOT NULL,
    [BirthDate] [date] NOT NULL,
    CONSTRAINT [PK_dbo.Users] PRIMARY KEY ([Email])
)