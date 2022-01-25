USE [master]
GO
/****** Object:  Database [Crm]    Script Date: 25.01.2022 00:01:12 ******/
CREATE DATABASE [Crm]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Crm', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS01\MSSQL\DATA\Crm.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Crm_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS01\MSSQL\DATA\Crm_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Crm] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Crm].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Crm] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Crm] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Crm] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Crm] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Crm] SET ARITHABORT OFF 
GO
ALTER DATABASE [Crm] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Crm] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Crm] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Crm] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Crm] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Crm] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Crm] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Crm] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Crm] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Crm] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Crm] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Crm] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Crm] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Crm] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Crm] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Crm] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Crm] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Crm] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Crm] SET  MULTI_USER 
GO
ALTER DATABASE [Crm] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Crm] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Crm] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Crm] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Crm] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Crm] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Crm] SET QUERY_STORE = OFF
GO
USE [Crm]
GO
/****** Object:  User [AppUser]    Script Date: 25.01.2022 00:01:12 ******/
CREATE USER [AppUser] FOR LOGIN [AppUser] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [AppUser]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [AppUser]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [AppUser]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [AppUser]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [AppUser]
GO
ALTER ROLE [db_datareader] ADD MEMBER [AppUser]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [AppUser]
GO
ALTER ROLE [db_denydatareader] ADD MEMBER [AppUser]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [AppUser]
GO
/****** Object:  Table [dbo].[CustomerCalls]    Script Date: 25.01.2022 00:01:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerCalls](
	[Id] [uniqueidentifier] NULL,
	[CustomerNo] [int] NULL,
	[DateOfCall] [date] NULL,
	[TimeOfCall] [time](7) NULL,
	[Subject] [nvarchar](100) NULL,
	[Description] [nvarchar](max) NULL,
	[Enabled] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 25.01.2022 00:01:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerNo] [int] IDENTITY(1,1) NOT NULL,
	[CustomerName] [nvarchar](100) NULL,
	[CustomerSurname] [nvarchar](100) NULL,
	[Address] [nvarchar](200) NULL,
	[PostCode] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[DateOfBirth] [date] NULL,
	[Enabled] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[CustomerReport]    Script Date: 25.01.2022 00:01:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CustomerReport]
AS
SELECT        c.CustomerNo, c.CustomerName, c.CustomerSurname, cc.DateOfCall, cc.TimeOfCall, cc.Subject, cc.Description
FROM            dbo.Customers AS c INNER JOIN
                         dbo.CustomerCalls AS cc ON cc.CustomerNo = c.CustomerNo
WHERE        (cc.Enabled = 1) AND (c.Enabled = 1)
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 25.01.2022 00:01:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[Id] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Enabled] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 25.01.2022 00:01:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](8) NULL,
	[PermissionId] [int] NULL,
	[Enabled] [bit] NULL
) ON [PRIMARY]
GO
INSERT [dbo].[CustomerCalls] ([Id], [CustomerNo], [DateOfCall], [TimeOfCall], [Subject], [Description], [Enabled]) VALUES (N'7c61004f-13dd-444d-976d-58b0f68f526f', 4, CAST(N'1980-12-12' AS Date), CAST(N'09:05:00' AS Time), N'subjo', N'desc', 1)
GO
INSERT [dbo].[CustomerCalls] ([Id], [CustomerNo], [DateOfCall], [TimeOfCall], [Subject], [Description], [Enabled]) VALUES (N'da9b6cf9-7631-4009-a939-6256521ba8ff', 4, CAST(N'1980-12-12' AS Date), CAST(N'09:05:00' AS Time), N'subjo', N'desc', 1)
GO
INSERT [dbo].[CustomerCalls] ([Id], [CustomerNo], [DateOfCall], [TimeOfCall], [Subject], [Description], [Enabled]) VALUES (N'62eec794-c877-4b03-b9c8-9dd431091e5e', 4, CAST(N'1980-12-12' AS Date), CAST(N'09:05:00' AS Time), N'subjo2222', N'desc2222', 1)
GO
INSERT [dbo].[CustomerCalls] ([Id], [CustomerNo], [DateOfCall], [TimeOfCall], [Subject], [Description], [Enabled]) VALUES (N'e3dbd817-24c3-4bab-934c-388c7893ffca', 4, CAST(N'1980-12-12' AS Date), CAST(N'09:05:00' AS Time), N'updated subject', N'desc2222', 0)
GO
INSERT [dbo].[CustomerCalls] ([Id], [CustomerNo], [DateOfCall], [TimeOfCall], [Subject], [Description], [Enabled]) VALUES (N'5332f19e-9879-4e77-931b-c2210c840c54', 1005, CAST(N'1980-12-12' AS Date), CAST(N'11:05:00' AS Time), N'real updated subject', N'desc2222', 1)
GO
INSERT [dbo].[CustomerCalls] ([Id], [CustomerNo], [DateOfCall], [TimeOfCall], [Subject], [Description], [Enabled]) VALUES (N'b7a42f2e-efd3-46f1-8f3d-ab1084c7b11f', 4, CAST(N'2021-12-28' AS Date), CAST(N'00:23:00' AS Time), N'first insert', N'congrats', 1)
GO
INSERT [dbo].[CustomerCalls] ([Id], [CustomerNo], [DateOfCall], [TimeOfCall], [Subject], [Description], [Enabled]) VALUES (N'faad6098-1c5e-4973-8d00-6bc8d0e0ca96', 4, CAST(N'1980-12-12' AS Date), CAST(N'09:05:00' AS Time), N'updated subject', N'desc2222', 0)
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 
GO
INSERT [dbo].[Customers] ([CustomerNo], [CustomerName], [CustomerSurname], [Address], [PostCode], [Country], [DateOfBirth], [Enabled]) VALUES (1, N'alissandro', N'fidannichi', N'kaga sokak', N'444', N'CY', CAST(N'1980-12-12' AS Date), 0)
GO
INSERT [dbo].[Customers] ([CustomerNo], [CustomerName], [CustomerSurname], [Address], [PostCode], [Country], [DateOfBirth], [Enabled]) VALUES (4, N'xx', N'xx', N'xx', N'xx', N'xx', CAST(N'2022-01-21' AS Date), 0)
GO
INSERT [dbo].[Customers] ([CustomerNo], [CustomerName], [CustomerSurname], [Address], [PostCode], [Country], [DateOfBirth], [Enabled]) VALUES (5, N'costas', N'updated spanulis', N'adress', N'pob xxx', N'GR', CAST(N'1995-01-01' AS Date), 0)
GO
INSERT [dbo].[Customers] ([CustomerNo], [CustomerName], [CustomerSurname], [Address], [PostCode], [Country], [DateOfBirth], [Enabled]) VALUES (6, N'costas', N'vessely', N'adress', N'pob xxx', N'GR', CAST(N'1985-12-13' AS Date), 1)
GO
INSERT [dbo].[Customers] ([CustomerNo], [CustomerName], [CustomerSurname], [Address], [PostCode], [Country], [DateOfBirth], [Enabled]) VALUES (1005, N'costas', N'sloukas', N'adress', N'pob xxx', N'GR', CAST(N'1985-12-16' AS Date), 1)
GO
INSERT [dbo].[Customers] ([CustomerNo], [CustomerName], [CustomerSurname], [Address], [PostCode], [Country], [DateOfBirth], [Enabled]) VALUES (1006, N'ali', N'fidanli', N'panaya', N'pob vvvv', N'CY', CAST(N'2000-12-31' AS Date), 1)
GO
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
INSERT [dbo].[Permissions] ([Id], [Name], [Enabled]) VALUES (1, N'Employee', 1)
GO
INSERT [dbo].[Permissions] ([Id], [Name], [Enabled]) VALUES (2, N'Manager', 1)
GO
INSERT [dbo].[Permissions] ([Id], [Name], [Enabled]) VALUES (3, N'Director', 1)
GO
INSERT [dbo].[Users] ([Username], [Password], [PermissionId], [Enabled]) VALUES (N'employee', N'employee', 1, 1)
GO
INSERT [dbo].[Users] ([Username], [Password], [PermissionId], [Enabled]) VALUES (N'manager', N'manager', 2, 1)
GO
INSERT [dbo].[Users] ([Username], [Password], [PermissionId], [Enabled]) VALUES (N'director', N'director', 3, 1)
GO
/****** Object:  StoredProcedure [dbo].[GetCustomer]    Script Date: 25.01.2022 00:01:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create procedure [dbo].[GetCustomer]
@CustomerNo int 
AS

BEGIN
Select * from Customers where CustomerNo = @CustomerNo
END
GO
/****** Object:  StoredProcedure [dbo].[GetCustomerCall]    Script Date: 25.01.2022 00:01:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create procedure [dbo].[GetCustomerCall]
@Id uniqueidentifier
AS

BEGIN
Select * from CustomerCalls where Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetCustomerCalls]    Script Date: 25.01.2022 00:01:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[GetCustomerCalls]
--@Row_ID int 
AS

BEGIN
Select * from CustomerCalls where Enabled = 1
END
GO
/****** Object:  StoredProcedure [dbo].[GetCustomers]    Script Date: 25.01.2022 00:01:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[GetCustomers]
--@Row_ID int 
AS

BEGIN
Select * from Customers where Enabled = 1
END
GO
/****** Object:  StoredProcedure [dbo].[GetPermission]    Script Date: 25.01.2022 00:01:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create procedure [dbo].[GetPermission]
@Id int
AS

BEGIN
Select * from Permissions where Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetPermissions]    Script Date: 25.01.2022 00:01:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[GetPermissions]
--@Row_ID int 
AS

BEGIN
Select * from Permissions where Enabled = 1
END
GO
/****** Object:  StoredProcedure [dbo].[GetUser]    Script Date: 25.01.2022 00:01:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[GetUser]
@UserName nvarchar(50)
AS

BEGIN
Select * from Users where Username = @UserName
END
GO
/****** Object:  StoredProcedure [dbo].[GetUsers]    Script Date: 25.01.2022 00:01:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create procedure [dbo].[GetUsers]
AS

BEGIN
Select * from Users where Enabled = 1
END
GO
/****** Object:  StoredProcedure [dbo].[InsertCustomer]    Script Date: 25.01.2022 00:01:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[InsertCustomer]
@CustomerName nvarchar(100),
@CustomerSurname nvarchar(100),
@Address nvarchar(200),
@PostCode nvarchar(50),
@Country nvarchar(2),
@DateOfBirth date
AS
BEGIN
insert into [dbo].[Customers]
   values( @CustomerName
      ,@CustomerSurname
      ,@Address
      ,@PostCode
      ,@Country
      ,@DateOfBirth
      ,1 )
	  select SCOPE_IDENTITY() CustomerNo
END
GO
/****** Object:  StoredProcedure [dbo].[InsertCustomerCall]    Script Date: 25.01.2022 00:01:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[InsertCustomerCall]
@CustomerNo int,
@DateOfCall date,
@TimeOfCall time(7),
@Subject nvarchar(100),
@Description nvarchar(max)
AS

DECLARE @Id AS uniqueidentifier

BEGIN

Select @Id = NEWID()
---
insert into [dbo].[CustomerCalls]
   values ( 
       @Id,
       @CustomerNo
      ,@DateOfCall
      ,@TimeOfCall
      ,@Subject
      ,@Description
      ,1
)

Select @Id Id

---
END
GO
/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 25.01.2022 00:01:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[InsertUser]
@UserName nvarchar(50), 
@Password nvarchar(8),
@PermissionId int
AS
BEGIN
insert into [dbo].Users
   values ( 
   @UserName,
   @Password,
   @PermissionId,
   1
   )

   select @UserName UserName

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCustomer]    Script Date: 25.01.2022 00:01:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[UpdateCustomer]
@CustomerNo int ,
@CustomerName nvarchar(100),
@CustomerSurname nvarchar(100),
@Address nvarchar(200),
@PostCode nvarchar(50),
@Country nvarchar(2),
@DateOfBirth date,
@Enabled bit
AS
BEGIN
UPDATE [dbo].[Customers]
   SET [CustomerName] = @CustomerName
      ,[CustomerSurname] = @CustomerSurname
      ,[Address] = @Address
      ,[PostCode] = @PostCode
      ,[Country] = @Country
      ,[DateofBirth] = @DateOfBirth
      ,[Enabled] = @Enabled
 WHERE CustomerNo = @CustomerNo
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCustomerCall]    Script Date: 25.01.2022 00:01:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[UpdateCustomerCall]
@Id uniqueidentifier ,
@CustomerNo int,
@DateOfCall date,
@TimeOfCall time(7),
@Subject nvarchar(100),
@Description nvarchar(max),
@Enabled bit
AS
BEGIN

---
UPDATE [dbo].[CustomerCalls]
   SET 
       [CustomerNo] = @CustomerNo
      ,[DateOfCall] = @DateOfCall
      ,[TimeOfCall] = @TimeOfCall
      ,[Subject] = @Subject
      ,[Description] = @Description
      ,[Enabled] = @Enabled
 WHERE [Id] = @Id
---
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 25.01.2022 00:01:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[UpdateUser]
@UserName nvarchar(50), 
@Password nvarchar(8),
@PermissionId int,
@Enabled bit
AS
BEGIN
UPDATE [dbo].Users
   SET 
   Password = @UserName ,
   PermissionId = @PermissionId,
   Enabled = @Enabled
 WHERE UserName = @UserName
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "c"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 226
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cc"
            Begin Extent = 
               Top = 6
               Left = 264
               Bottom = 136
               Right = 434
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CustomerReport'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CustomerReport'
GO
USE [master]
GO
ALTER DATABASE [Crm] SET  READ_WRITE 
GO
