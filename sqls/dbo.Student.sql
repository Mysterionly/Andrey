CREATE TABLE [dbo].[Student] (
    [ID]              INT IDENTITY (1, 1) NOT NULL,
    [user_id]         INT NOT NULL,
    [intro_exam_mark] INT NOT NULL,
    [final_exam_mark] INT NOT NULL,
    CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED ([ID] ASC), 
    CONSTRAINT [FK_Student_ToUser] FOREIGN KEY ([user_id]) REFERENCES [User]([ID])
);

