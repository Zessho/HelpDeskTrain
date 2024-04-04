CREATE TABLE [dbo].[Activs]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [CabNumber] NVARCHAR(50) NOT NULL, 
    [DepartamentID] INT NULL,
	CONSTRAINT [FK_Activs_ToDepartaments] FOREIGN KEY ([DepartamentID])
	REFERENCES [Departments]([Id]) ON DELETE SET NULL
)
