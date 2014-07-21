
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/21/2014 12:40:17
-- Generated from EDMX file: F:\Projects\AndroidToolkitV2\AndroidToolkit\AndroidToolkit.Data\Entities\AndroidToolkitDb.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [AndroidToolkitDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_DeviceRecovery]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Recoveries] DROP CONSTRAINT [FK_DeviceRecovery];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Devices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Devices];
GO
IF OBJECT_ID(N'[dbo].[Recoveries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Recoveries];
GO
IF OBJECT_ID(N'[dbo].[Helps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Helps];
GO
IF OBJECT_ID(N'[dbo].[BugReports]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BugReports];
GO
IF OBJECT_ID(N'[dbo].[Reviews]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Reviews];
GO
IF OBJECT_ID(N'[dbo].[Posts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Posts];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Devices'
CREATE TABLE [dbo].[Devices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Manufacturer] nvarchar(max)  NOT NULL,
    [Year] nvarchar(max)  NOT NULL,
    [Image] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Recoveries'
CREATE TABLE [dbo].[Recoveries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Version] nvarchar(max)  NOT NULL,
    [DeviceId] int  NOT NULL,
    [Download] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Helps'
CREATE TABLE [dbo].[Helps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'BugReports'
CREATE TABLE [dbo].[BugReports] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [ErrorLog] nvarchar(max)  NOT NULL,
    [Author] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Reviews'
CREATE TABLE [dbo].[Reviews] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [Rating] int  NOT NULL,
    [Author] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Posts'
CREATE TABLE [dbo].[Posts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL,
    [Tags] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Devices'
ALTER TABLE [dbo].[Devices]
ADD CONSTRAINT [PK_Devices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Recoveries'
ALTER TABLE [dbo].[Recoveries]
ADD CONSTRAINT [PK_Recoveries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Helps'
ALTER TABLE [dbo].[Helps]
ADD CONSTRAINT [PK_Helps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BugReports'
ALTER TABLE [dbo].[BugReports]
ADD CONSTRAINT [PK_BugReports]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Reviews'
ALTER TABLE [dbo].[Reviews]
ADD CONSTRAINT [PK_Reviews]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [PK_Posts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DeviceId] in table 'Recoveries'
ALTER TABLE [dbo].[Recoveries]
ADD CONSTRAINT [FK_DeviceRecovery]
    FOREIGN KEY ([DeviceId])
    REFERENCES [dbo].[Devices]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DeviceRecovery'
CREATE INDEX [IX_FK_DeviceRecovery]
ON [dbo].[Recoveries]
    ([DeviceId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------