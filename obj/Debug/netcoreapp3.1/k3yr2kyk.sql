IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Products] (
    [ProductId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [ShortDescription] nvarchar(max) NULL,
    [LongDescription] nvarchar(max) NULL,
    [Price] decimal(18,2) NOT NULL,
    [ImageUrl] nvarchar(max) NULL,
    [ImageThumbnailUrl] nvarchar(max) NULL,
    [InStock] bit NOT NULL,
    [IsFeatured] bit NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([ProductId])
);

GO

CREATE TABLE [Images] (
    [ID] int NOT NULL IDENTITY,
    [ImageURL] nvarchar(max) NULL,
    [ProductID] int NOT NULL,
    CONSTRAINT [PK_Images] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Images_Products_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE
);

GO

CREATE TABLE [ShoppingCartItems] (
    [ShoppingCartItemId] int NOT NULL IDENTITY,
    [ProductId] int NULL,
    [Amount] int NOT NULL,
    [ShoppingCartId] nvarchar(max) NULL,
    CONSTRAINT [PK_ShoppingCartItems] PRIMARY KEY ([ShoppingCartItemId]),
    CONSTRAINT [FK_ShoppingCartItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE NO ACTION
);

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'ImageThumbnailUrl', N'ImageUrl', N'InStock', N'IsFeatured', N'LongDescription', N'Name', N'Price', N'ShortDescription') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] ON;
INSERT INTO [Products] ([ProductId], [ImageThumbnailUrl], [ImageUrl], [InStock], [IsFeatured], [LongDescription], [Name], [Price], [ShortDescription])
VALUES (1, N'https://i.imgur.com/jDlwQfT.png', NULL, CAST(1 AS bit), CAST(1 AS bit), N'მანქანის ''ჩიხოლები'' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში ლორემ იპსუმ', N'მანქანის გადასაფარებელი', 12.0, N'მანქანის ''ჩიხოლები'' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში'),
(2, N'https://i.imgur.com/jM45dWU.png', NULL, CAST(1 AS bit), CAST(1 AS bit), N'მანქანის ''ჩიხოლები'' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში ლორემ იპსუმ', N'ჩაიდანი', 14.0, N'ჩაიდანი მოკლედ რაღაც ინფორმაცია'),
(3, N'https://i.imgur.com/AIwoQYN.png', NULL, CAST(1 AS bit), CAST(1 AS bit), N'მანქანის ''ჩიხოლები'' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში ლორემ იპსუმ', N'ანკესი', 16.0, N'ანკესი მოკლედ'),
(4, N'https://i.imgur.com/jDlwQfT.png', NULL, CAST(1 AS bit), CAST(1 AS bit), N'მანქანის ''ჩიხოლები'' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში ლორემ იპსუმ', N'მანქანის გადასაფარებელი', 12.0, N'მანქანის ''ჩიხოლები'' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში'),
(5, N'https://i.imgur.com/jM45dWU.png', NULL, CAST(1 AS bit), CAST(1 AS bit), N'მანქანის ''ჩიხოლები'' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში ლორემ იპსუმ', N'ჩაიდანი', 14.0, N'ჩაიდანი მოკლედ რაღაც ინფორმაცია'),
(6, N'https://i.imgur.com/AIwoQYN.png', NULL, CAST(1 AS bit), CAST(1 AS bit), N'მანქანის ''ჩიხოლები'' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში ლორემ იპსუმ', N'ანკესი', 16.0, N'ანკესი მოკლედ'),
(7, N'https://i.imgur.com/jDlwQfT.png', NULL, CAST(1 AS bit), CAST(1 AS bit), N'მანქანის ''ჩიხოლები'' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში ლორემ იპსუმ', N'მანქანის გადასაფარებელი', 12.0, N'მანქანის ''ჩიხოლები'' გვაქვს ორ ფერში ლურჯში და ნაცრისფერში');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProductId', N'ImageThumbnailUrl', N'ImageUrl', N'InStock', N'IsFeatured', N'LongDescription', N'Name', N'Price', N'ShortDescription') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'ImageURL', N'ProductID') AND [object_id] = OBJECT_ID(N'[Images]'))
    SET IDENTITY_INSERT [Images] ON;
INSERT INTO [Images] ([ID], [ImageURL], [ProductID])
VALUES (1, N'https://i.imgur.com/APSUSkV.jpg', 1),
(2, N'https://i.imgur.com/FSTeMK2.jpg', 1),
(3, N'https://i.imgur.com/qKxxqUb.jpg', 1),
(4, N'https://i.imgur.com/QfIRm5K.jpg', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'ImageURL', N'ProductID') AND [object_id] = OBJECT_ID(N'[Images]'))
    SET IDENTITY_INSERT [Images] OFF;

GO

CREATE INDEX [IX_Images_ProductID] ON [Images] ([ProductID]);

GO

CREATE INDEX [IX_ShoppingCartItems_ProductId] ON [ShoppingCartItems] ([ProductId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210118174304_redo', N'3.0.0');

GO

