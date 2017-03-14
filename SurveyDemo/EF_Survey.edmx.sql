
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/14/2017 09:34:16
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


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


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

-- Creating table 'Interacts'
CREATE TABLE [dbo].[Interacts] (
    [interactId] int IDENTITY(1,1) NOT NULL,
    [Customers_custId] int  NOT NULL,
    [Employees_empId] int  NOT NULL,
    [SurveysOut_uuid] int  NOT NULL
);
GO

-- Creating table 'SurveysOut'
CREATE TABLE [dbo].[SurveysOut] (
    [uuid] int IDENTITY(1,1) NOT NULL,
    [SurveysIn_uuid] int  NOT NULL
);
GO

-- Creating table 'SurveysIn'
CREATE TABLE [dbo].[SurveysIn] (
    [uuid] int IDENTITY(1,1) NOT NULL
);
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

-- Creating primary key on [uuid] in table 'SurveysOut'
ALTER TABLE [dbo].[SurveysOut]
ADD CONSTRAINT [PK_SurveysOut]
    PRIMARY KEY CLUSTERED ([uuid] ASC);
GO

-- Creating primary key on [uuid] in table 'SurveysIn'
ALTER TABLE [dbo].[SurveysIn]
ADD CONSTRAINT [PK_SurveysIn]
    PRIMARY KEY CLUSTERED ([uuid] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Customers_custId] in table 'Interacts'
ALTER TABLE [dbo].[Interacts]
ADD CONSTRAINT [FK_CustomersInteracts]
    FOREIGN KEY ([Customers_custId])
    REFERENCES [dbo].[Customers]
        ([custId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomersInteracts'
CREATE INDEX [IX_FK_CustomersInteracts]
ON [dbo].[Interacts]
    ([Customers_custId]);
GO

-- Creating foreign key on [Employees_empId] in table 'Interacts'
ALTER TABLE [dbo].[Interacts]
ADD CONSTRAINT [FK_InteractsEmployees]
    FOREIGN KEY ([Employees_empId])
    REFERENCES [dbo].[Employees]
        ([empId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InteractsEmployees'
CREATE INDEX [IX_FK_InteractsEmployees]
ON [dbo].[Interacts]
    ([Employees_empId]);
GO

-- Creating foreign key on [SurveysOut_uuid] in table 'Interacts'
ALTER TABLE [dbo].[Interacts]
ADD CONSTRAINT [FK_InteractsSurveysOut]
    FOREIGN KEY ([SurveysOut_uuid])
    REFERENCES [dbo].[SurveysOut]
        ([uuid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InteractsSurveysOut'
CREATE INDEX [IX_FK_InteractsSurveysOut]
ON [dbo].[Interacts]
    ([SurveysOut_uuid]);
GO

-- Creating foreign key on [SurveysIn_uuid] in table 'SurveysOut'
ALTER TABLE [dbo].[SurveysOut]
ADD CONSTRAINT [FK_SurveysOutSurveysIn]
    FOREIGN KEY ([SurveysIn_uuid])
    REFERENCES [dbo].[SurveysIn]
        ([uuid])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SurveysOutSurveysIn'
CREATE INDEX [IX_FK_SurveysOutSurveysIn]
ON [dbo].[SurveysOut]
    ([SurveysIn_uuid]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------