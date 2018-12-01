use LocalDB;

if not exists (select 1 from sysobjects where name = 'Task_Table' and xtype = 'U')
begin
	create table dbo.Task_Table
	(
		[Task_ID] int identity(1,1) not null primary key,
		[Task] varchar(max) null,
		[Priority] int null,
		[Parent_ID] int null,
		[Start_Date] datetime null,
		[End_Date] datetime null
	)  
end
