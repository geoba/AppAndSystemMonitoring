USE [master]
GO
/****** Object:  Database [Monitoring]    Script Date: 26.10.2018 16:48:32 ******/
CREATE DATABASE [Monitoring]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Monitoring', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Monitoring.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Monitoring_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Monitoring_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Monitoring] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Monitoring].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Monitoring] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Monitoring] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Monitoring] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Monitoring] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Monitoring] SET ARITHABORT OFF 
GO
ALTER DATABASE [Monitoring] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Monitoring] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Monitoring] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Monitoring] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Monitoring] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Monitoring] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Monitoring] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Monitoring] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Monitoring] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Monitoring] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Monitoring] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Monitoring] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Monitoring] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Monitoring] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Monitoring] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Monitoring] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Monitoring] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Monitoring] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Monitoring] SET  MULTI_USER 
GO
ALTER DATABASE [Monitoring] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Monitoring] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Monitoring] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Monitoring] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Monitoring] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Monitoring] SET QUERY_STORE = OFF
GO
USE [Monitoring]
GO
/****** Object:  User [MonitoringService]    Script Date: 26.10.2018 16:48:32 ******/
CREATE USER [MonitoringService] FOR LOGIN [RYZEN7GTX1080\MonitoringService] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [MonitoringReader]    Script Date: 26.10.2018 16:48:32 ******/
CREATE USER [MonitoringReader] FOR LOGIN [RYZEN7GTX1080\MonitoringReader] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [MonitoringService]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [MonitoringService]
GO
/****** Object:  Table [dbo].[MostRecentLoggingEvents]    Script Date: 26.10.2018 16:48:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MostRecentLoggingEvents](
	[ID] [bigint] NOT NULL,
	[AlertSent] [int] NULL,
	[TestingHost] [nvarchar](100) NULL,
	[TestingUser] [nvarchar](100) NULL,
	[TestID] [int] NULL,
	[TargetMachine] [nvarchar](100) NULL,
	[FeedbackValue] [nvarchar](100) NULL,
	[Successful] [bit] NULL,
	[FeedbackMessage] [nvarchar](max) NULL,
	[ResponseTime] [bigint] NULL,
	[TimeStamp] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestDetails]    Script Date: 26.10.2018 16:48:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TestDisplayName] [nvarchar](100) NULL,
	[TestProtocol] [nvarchar](100) NULL,
	[TestDescription] [nvarchar](max) NULL,
	[TokenNeeded] [nvarchar](100) NULL,
	[TestOptionString] [nvarchar](max) NULL,
	[EmailDistributionList] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[MostRecentEventDetails_vw]    Script Date: 26.10.2018 16:48:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[MostRecentEventDetails_vw] as
select MRLE.ID as EventId, AlertSent, TestingHost, TestingUser, TargetMachine, FeedbackValue, Successful, FeedbackMessage, ResponseTime, TimeStamp, TD.ID as TestID, TestDisplayName, TestProtocol, TestDescription, TokenNeeded, TestOptionString, EmailDistributionList
-- select MRLE.ID as EventId, AlertSent, TD.ID as TestID
from MostRecentLoggingEvents as MRLE
join TestDetails as TD
on (MRLE.TestID = TD.ID)

GO
/****** Object:  Table [dbo].[CurrentTestToken]    Script Date: 26.10.2018 16:48:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CurrentTestToken](
	[TestingHost] [nvarchar](100) NULL,
	[TestingUser] [nvarchar](100) NULL,
	[TimeStamp] [datetime] NULL,
	[GracePeriod] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoggingEvents]    Script Date: 26.10.2018 16:48:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoggingEvents](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[AlertSent] [int] NULL,
	[TestingHost] [nvarchar](100) NULL,
	[TestingUser] [nvarchar](100) NULL,
	[TestID] [int] NULL,
	[TargetMachine] [nvarchar](100) NULL,
	[FeedbackValue] [nvarchar](100) NULL,
	[Successful] [bit] NULL,
	[FeedbackMessage] [nvarchar](max) NULL,
	[ResponseTime] [bigint] NULL,
	[TimeStamp] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [primIndex]    Script Date: 26.10.2018 16:48:33 ******/
CREATE NONCLUSTERED INDEX [primIndex] ON [dbo].[MostRecentLoggingEvents]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LoggingEvents] ADD  DEFAULT ((-100)) FOR [AlertSent]
GO
ALTER TABLE [dbo].[MostRecentLoggingEvents] ADD  DEFAULT ((-100)) FOR [AlertSent]
GO
ALTER TABLE [dbo].[LoggingEvents]  WITH CHECK ADD  CONSTRAINT [FK_TestID] FOREIGN KEY([TestID])
REFERENCES [dbo].[TestDetails] ([ID])
GO
ALTER TABLE [dbo].[LoggingEvents] CHECK CONSTRAINT [FK_TestID]
GO
USE [master]
GO
ALTER DATABASE [Monitoring] SET  READ_WRITE 
GO
