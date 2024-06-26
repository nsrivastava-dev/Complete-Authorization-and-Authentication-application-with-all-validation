USE [remainingtopics]
GO
/****** Object:  Table [dbo].[tbl_register]    Script Date: 4/16/2024 12:12:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_register](
	[name] [varchar](100) NULL,
	[email] [varchar](100) NOT NULL,
	[gender] [varchar](20) NULL,
	[qul] [varchar](50) NULL,
	[mobno] [bigint] NULL,
	[pass] [varchar](100) NULL,
	[address] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_users]    Script Date: 4/16/2024 12:12:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_users](
	[name] [varchar](100) NULL,
	[gender] [varchar](30) NULL,
	[email] [varchar](100) NOT NULL,
	[password] [varchar](30) NULL,
	[mobno] [bigint] NULL,
	[status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tbl_register] ([name], [email], [gender], [qul], [mobno], [pass], [address]) VALUES (N'Aleesha Naaz', N'aleesha@gmail.com', N'Female', N'B.Tech', 787744544, N'12345', N'abc colony')
INSERT [dbo].[tbl_register] ([name], [email], [gender], [qul], [mobno], [pass], [address]) VALUES (N'Atul Verma', N'atul@gmail.com', N'Male', N'BCA', 545786441, N'123456789', N'xyz colony tareenpur')
INSERT [dbo].[tbl_register] ([name], [email], [gender], [qul], [mobno], [pass], [address]) VALUES (N'Nitesh', N'nitesh@gmail.com', N'Male', N'Diploma', 8948508636, N'123', N'xyz colony')
INSERT [dbo].[tbl_register] ([name], [email], [gender], [qul], [mobno], [pass], [address]) VALUES (N'Priyanshu', N'priyanshu@gmail.com', N'Male', N'Diploma', 34567987, N'0000', N'www xyz')
GO
INSERT [dbo].[tbl_users] ([name], [gender], [email], [password], [mobno], [status]) VALUES (N'Aleesha', N'Female', N'aleesha@gmail.com', N'1234', 8948508636, 1)
INSERT [dbo].[tbl_users] ([name], [gender], [email], [password], [mobno], [status]) VALUES (N'Nitesh', N'Male', N'nitesh@gmail.com', N'123', 8948508636, 1)
GO
/****** Object:  StoredProcedure [dbo].[sp_login]    Script Date: 4/16/2024 12:12:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_login]
@email varchar(100),
@pass varchar(100)
as
begin
--if exists(select * from tbl_users where email=@email and password=@pass)
--begin
--	select 1
--end
--else
--begin
--	select 0
--end
select * from tbl_users where email=@email and password=@pass
end
GO
/****** Object:  StoredProcedure [dbo].[sp_register]    Script Date: 4/16/2024 12:12:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_register]
@name varchar(100)=null,
@email varchar(100)=null ,
@gender varchar(20)=null,
@qul varchar(50)=null,
@mobno bigint=0,
@pass varchar(100)=null,
@address varchar(200)=null,
@npass varchar(100)=null,
@action int
as
begin
if(@action=1)
begin
insert into tbl_register values(@name,@email,@gender,@qul,@mobno,@pass,@address)
end
if(@action=2)
begin
select * from tbl_register where email=@email and pass=@pass
end
if(@action=3)
begin
select * from tbl_register where email != @email
end
if(@action=4)
begin 
select * from tbl_register where email=@email
end
if(@action=5)
begin

update tbl_register set pass=@npass where email=@email
end
end
GO
