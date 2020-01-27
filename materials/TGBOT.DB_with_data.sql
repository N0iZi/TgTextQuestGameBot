USE [TGBOT]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 28.01.2020 00:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[content] [nvarchar](max) NOT NULL,
	[is_final] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Replies]    Script Date: 28.01.2020 00:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Replies](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[content] [nvarchar](max) NOT NULL,
	[for_message_id] [int] NOT NULL,
	[next_message_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 28.01.2020 00:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[chat_id] [int] NOT NULL,
	[current_message_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Messages] ON 
GO
INSERT [dbo].[Messages] ([id], [content], [is_final]) VALUES (1, N'Your character is an IT student and he has only 3 days left to complete project. What will you do?', 0)
GO
INSERT [dbo].[Messages] ([id], [content], [is_final]) VALUES (3, N'Ok, the theme is a simple DB surfing web application. Which programing language will you choose?', 0)
GO
INSERT [dbo].[Messages] ([id], [content], [is_final]) VALUES (5, N'Which concrete DB you choose?', 0)
GO
INSERT [dbo].[Messages] ([id], [content], [is_final]) VALUES (6, N'Which concrete DB you choose?', 0)
GO
INSERT [dbo].[Messages] ([id], [content], [is_final]) VALUES (7, N'Will you use ORM?', 0)
GO
INSERT [dbo].[Messages] ([id], [content], [is_final]) VALUES (8, N'Will you use ORM?', 0)
GO
INSERT [dbo].[Messages] ([id], [content], [is_final]) VALUES (9, N'Will you write a documentation to your project?', 0)
GO
INSERT [dbo].[Messages] ([id], [content], [is_final]) VALUES (10, N'Are you ready to work without breaks?', 0)
GO
INSERT [dbo].[Messages] ([id], [content], [is_final]) VALUES (11, N'Hmmm... Okay... Your project is not competed and you are going on 2nd term. Game over.', 1)
GO
INSERT [dbo].[Messages] ([id], [content], [is_final]) VALUES (12, N'I think you need to study programing from the begining... Game over.', 1)
GO
INSERT [dbo].[Messages] ([id], [content], [is_final]) VALUES (13, N'No DB for DB surfing application... Not bad. Game over.', 1)
GO
INSERT [dbo].[Messages] ([id], [content], [is_final]) VALUES (15, N'You tried to work with standard SQL queries, but it took too much time... You are late, welcome to 2nd term. Game over.', 1)
GO
INSERT [dbo].[Messages] ([id], [content], [is_final]) VALUES (17, N'Strange choise, but ok... Will you use ORM?', 0)
GO
INSERT [dbo].[Messages] ([id], [content], [is_final]) VALUES (18, N'Hibernate is for Java and C# analog is called NHibernate. You were close but game is over.', 1)
GO
INSERT [dbo].[Messages] ([id], [content], [is_final]) VALUES (19, N'There is no EntityFramework for Java! Game over.', 1)
GO
INSERT [dbo].[Messages] ([id], [content], [is_final]) VALUES (20, N'You made your choises. You have no time now. Game over.', 1)
GO
INSERT [dbo].[Messages] ([id], [content], [is_final]) VALUES (22, N'Well... No documentation - no positive mark. Game over.', 1)
GO
INSERT [dbo].[Messages] ([id], [content], [is_final]) VALUES (23, N'Finally! You made all right choises and you are able to get positive mark for your project! You win!', 1)
GO
SET IDENTITY_INSERT [dbo].[Messages] OFF
GO
SET IDENTITY_INSERT [dbo].[Replies] ON 
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (1, N'Forget about it.', 1, 11)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (3, N'Start doing it NOW.', 1, 3)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (4, N'Start doing it LATER.', 1, 10)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (5, N'C#', 3, 5)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (6, N'Pascal', 3, 12)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (7, N'Java', 3, 6)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (8, N'MS SQL Server Express', 5, 7)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (9, N'No DB', 5, 13)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (10, N'SQLite', 5, 7)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (11, N'MS SQL Server', 6, 17)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (12, N'No DB', 6, 13)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (13, N'SQLite', 6, 8)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (14, N'EntityFramework', 7, 9)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (15, N'Nope', 7, 15)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (16, N'Hibernate', 7, 18)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (17, N'EntityFramework', 8, 19)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (18, N'Nope', 8, 15)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (19, N'Hibernate', 8, 9)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (20, N'Yes, sure!', 9, 23)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (21, N'Nope. Why?', 9, 22)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (22, N'Yes, I am ready.', 10, 3)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (23, N'Of course I am NOT!', 10, 20)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (24, N'EntityFramework', 17, 19)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (25, N'No DB', 17, 13)
GO
INSERT [dbo].[Replies] ([id], [content], [for_message_id], [next_message_id]) VALUES (27, N'Hibernate', 17, 9)
GO
SET IDENTITY_INSERT [dbo].[Replies] OFF
GO
ALTER TABLE [dbo].[Replies]  WITH CHECK ADD FOREIGN KEY([for_message_id])
REFERENCES [dbo].[Messages] ([id])
GO
ALTER TABLE [dbo].[Replies]  WITH CHECK ADD FOREIGN KEY([next_message_id])
REFERENCES [dbo].[Messages] ([id])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([current_message_id])
REFERENCES [dbo].[Messages] ([id])
GO
