use [TGBOT]

CREATE TABLE [Messages] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [content] nvarchar(MAX) NOT NULL,
  [is_final] BIT NOT NULL
)
GO

CREATE TABLE [Replies] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [content] nvarchar(MAX) NOT NULL,
  [for_message_id] int NOT NULL,
  [next_message_id] int NOT NULL
)
GO

CREATE TABLE [Users] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [chat_id] int NOT NULL,
  [current_message_id] int NOT NULL
)
GO

ALTER TABLE [Replies] ADD FOREIGN KEY ([for_message_id]) REFERENCES [Messages] ([id])
GO

ALTER TABLE [Replies] ADD FOREIGN KEY ([next_message_id]) REFERENCES [Messages] ([id])
GO

ALTER TABLE [Users] ADD FOREIGN KEY ([current_message_id]) REFERENCES [Messages] ([id])
GO
