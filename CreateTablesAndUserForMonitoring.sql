--using database [system].[dbo].[Monitoring]

drop table LoggingEvents
drop table TestDetails
drop table CurrentTestToken

create table LoggingEvents (
ID bigint not null identity(1, 1),
AlertSent int default -100,
TestingHost nvarchar(100),
TestingUser nvarchar(100),
TestID int,
TargetMachine nvarchar(100),
FeedbackValue nvarchar(100),
Successful bit,
FeedbackMessage nvarchar(max),
ResponseTime bigint,
TimeStamp DateTime,
primary key (ID)
)

create table TestDetails (
ID int not null identity(1, 1),
TestDisplayName nvarchar(100),
TestProtocol nvarchar(100),
TestDescription nvarchar(max),
TokenNeeded nvarchar(100),
TestOptionString nvarchar(max),
EmailDistributionList nvarchar(100),
primary key (ID)
)


create table CurrentTestToken (
TestingHost nvarchar(100),
TestingUser nvarchar(100),
TimeStamp DateTime,
GracePeriod int
)

-- user 
-- alter table LoggingEvents foreignkey...
alter table LoggingEvents add constraint FK_TestID foreign key (TestID) references TestDetails(ID)

--create user MonitoringService for login [CONFIGIT-DE-GFK\MonitoringService]
--EXEC sp_addrolemember N'db_datawriter', N'MonitoringService'  
--EXEC sp_addrolemember N'db_datareader', N'MonitoringService'  

