
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/14/2017 12:50:22
-- Generated from EDMX file: C:\Users\323122960\Documents\Visual Studio 2013\Projects\SurveyDemo\SurveyDemo\EF_Survey.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Survey];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CustomersInteracts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Interacts] DROP CONSTRAINT [FK_CustomersInteracts];
GO
IF OBJECT_ID(N'[dbo].[FK_InteractsEmployees]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Interacts] DROP CONSTRAINT [FK_InteractsEmployees];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[Interacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Interacts];
GO
IF OBJECT_ID(N'[dbo].[SurveysIn]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SurveysIn];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [custId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [empId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL
);
GO
--use [Survey] alter table [dbo].[Interacts] alter column [uuid] nvarchar(max) NULL
--alter table [dbo].[SurveysIn] alter column  [uuid] nvarchar(max) not NULL
-- Creating table 'Interacts'
CREATE TABLE [dbo].[Interacts] (
    [interactId] int IDENTITY(1,1) NOT NULL,
    [uuid] nvarchar(max) NULL,
    [Customer_custId] int  NOT NULL,
    [Employee_empId] int  NOT NULL
);
GO

-- Creating table 'SurveysIn'
--use [Survey]
CREATE TABLE [dbo].[SurveysIn] (
    [surveyId] int identity(1,1) not null,
	[uuid]   NVARCHAR(MAX)            NOT NULL,
    [Reward] NVARCHAR (MAX) NOT NULL,
    [Rating] NVARCHAR (MAX) NOT NULL
);
--CREATE TABLE [dbo].[SurveysIn] (
--    [uuid] int NOT NULL,
--    [Reward] nvarchar(max)  NOT NULL,
--    [Rating] nvarchar(max)  NOT NULL
--);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [custId] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([custId] ASC);
GO

-- Creating primary key on [empId] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([empId] ASC);
GO

-- Creating primary key on [interactId] in table 'Interacts'
ALTER TABLE [dbo].[Interacts]
ADD CONSTRAINT [PK_Interacts]
    PRIMARY KEY CLUSTERED ([interactId] ASC);
GO

-- Creating primary key on [uuid] in table 'SurveysIn'
--use [Survey]
ALTER TABLE [dbo].[SurveysIn]
ADD CONSTRAINT [PK_SurveysIn]
    PRIMARY KEY CLUSTERED ([surveyId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Customer_custId] in table 'Interacts'
ALTER TABLE [dbo].[Interacts]
ADD CONSTRAINT [FK_CustomersInteracts]
    FOREIGN KEY ([Customer_custId])
    REFERENCES [dbo].[Customers]
        ([custId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomersInteracts'
CREATE INDEX [IX_FK_CustomersInteracts]
ON [dbo].[Interacts]
    ([Customer_custId]);
GO

-- Creating foreign key on [Employee_empId] in table 'Interacts'
ALTER TABLE [dbo].[Interacts]
ADD CONSTRAINT [FK_InteractsEmployees]
    FOREIGN KEY ([Employee_empId])
    REFERENCES [dbo].[Employees]
        ([empId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InteractsEmployees'
CREATE INDEX [IX_FK_InteractsEmployees]
ON [dbo].[Interacts]
    ([Employee_empId]);
GO

--Insert some dummy values:
SET IDENTITY_INSERT [dbo].[Customers] ON --can explicitly insert values in PK col instead of auto-insert
INSERT INTO [dbo].[Customers] ([custId], [Name], [Email]) VALUES (1, N'Mani', N'abc@abc.com')
INSERT INTO [dbo].[Customers] ([custId], [Name], [Email]) VALUES (4, N'Zoya', N'def@def.com')
INSERT INTO [dbo].[Customers] ([custId], [Name], [Email]) VALUES (5, N'Papo', N'ghi@ghi.com')
INSERT INTO [dbo].[Customers] ([custId], [Name], [Email]) VALUES (6, N'Arham', N'jkl@jkl.com')
SET IDENTITY_INSERT [dbo].[Customers] OFF

SET IDENTITY_INSERT [dbo].[Employees] ON
INSERT INTO [dbo].[Employees] ([empId], [Name], [Email]) VALUES (1, N'Paul', N'123@123.com')
INSERT INTO [dbo].[Employees] ([empId], [Name], [Email]) VALUES (2, N'Michael', N'456@456.com')
INSERT INTO [dbo].[Employees] ([empId], [Name], [Email]) VALUES (3, N'Jenny', N'789@789.com')
INSERT INTO [dbo].[Employees] ([empId], [Name], [Email]) VALUES (4, N'Liz', N'aaa@aaa.com')
SET IDENTITY_INSERT [dbo].[Employees] OFF

SET IDENTITY_INSERT [dbo].[Interacts] ON
INSERT INTO [dbo].[Interacts] ([interactId], [uuid], [Customer_custId], [Employee_empId]) VALUES (1, NULL, 1, 1)
INSERT INTO [dbo].[Interacts] ([interactId], [uuid], [Customer_custId], [Employee_empId]) VALUES (2, NULL, 4, 2)
INSERT INTO [dbo].[Interacts] ([interactId], [uuid], [Customer_custId], [Employee_empId]) VALUES (3, NULL, 5, 2)
INSERT INTO [dbo].[Interacts] ([interactId], [uuid], [Customer_custId], [Employee_empId]) VALUES (4, NULL, 6, 3)
INSERT INTO [dbo].[Interacts] ([interactId], [uuid], [Customer_custId], [Employee_empId]) VALUES (5, NULL, 6, 4)
SET IDENTITY_INSERT [dbo].[Interacts] OFF



-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
