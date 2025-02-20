INSERT INTO Membership (Description, Price)
VALUES 
    ('Free', 0),
    ('Pay', 9.99);

INSERT INTO Role (Role) 
VALUES 
('admin'),
('member'),
('guest');

INSERT INTO [User] (UserName, FullName, Email, Password, DOB, Phone, Available, RoleId, ProfileImageUrl)
VALUES
-- Admin
('admin_user', 'Admin User', 'admin@example.com', 'admin123', '1980-01-01', '0123456789', 1, 1, null),
-- Members
('member1', 'Member One', 'member1@example.com', 'member123', '1990-02-01', '0987654321', 1, 2, null),
('member2', 'Member Two', 'member2@example.com', 'member123', '1992-03-01', '0987654322', 1, 2, null),
('member3', 'Member Three', 'member3@example.com', 'member123', '1994-04-01', '0987654323', 1, 2, null),
('member4', 'Member Four', 'member4@example.com', 'member123', '1996-05-01', '0987654324', 1, 2, null),
('member5', 'Member Five', 'member5@example.com', 'member123', '1998-06-01', '0987654325', 1, 2, null),
-- Guests
('guest1', 'Guest One', 'guest1@example.com', 'guest123', '2000-07-01', '0987654326', 1, 3, null),
('guest2', 'Guest Two', 'guest2@example.com', 'guest123', '2001-08-01', '0987654327', 1, 3, null),
('guest3', 'Guest Three', 'guest3@example.com', 'guest123', '2002-09-01', '0987654328', 1, 3, null),
('guest4', 'Guest Four', 'guest4@example.com', 'guest123', '2003-10-01', '0987654329', 1, 3, null),
('guest5', 'Guest Five', 'guest5@example.com', 'guest123', '2004-11-01', '0987654330', 1, 3, null),
('guest6', 'Guest Six', 'guest6@example.com', 'guest123', '2005-12-01', '0987654331', 1, 3, null),
('guest7', 'Guest Seven', 'guest7@example.com', 'guest123', '2006-01-01', '0987654332', 1, 3, null),
('guest8', 'Guest Eight', 'guest8@example.com', 'guest123', '2007-02-01', '0987654333', 1, 3, null),
('guest9', 'Guest Nine', 'guest9@example.com', 'guest123', '2008-03-01', '0987654334', 1, 3, null),
('guest10', 'Guest Ten', 'guest10@example.com', 'guest123', '2009-04-01', '0987654335', 1, 3, null);

INSERT INTO Foetus (UserId)
VALUES
-- Member 1: 2 children
(2),
(2),
-- Member 2: 1 child
(3),
-- Member 3: 2 children
(4),
(4),
-- Member 4: 1 child
(5),
-- Member 5: 2 children
(6),
(6),
-- Guest 4: 1 children
(10),
-- Guest 1: 1 children
(7);

DECLARE @StartDate DATE = '2024-02-12'; -- Ngày bắt đầu theo dõi

INSERT INTO [dbo].[GrowthData] (FoetusId, [Date], GrowthStandardId, Age, HC, AC, FL, EFW)
VALUES
(1, DATEADD(WEEK, 0, @StartDate), 1, 12, 70, 56, 8, 14),
(1, DATEADD(WEEK, 1, @StartDate), 2, 13, 84, 69, 11, 23),
(1, DATEADD(WEEK, 2, @StartDate), 3, 14, 98, 81, 15, 42),
(1, DATEADD(WEEK, 3, @StartDate), 4, 15, 111, 93, 18, 70),
(1, DATEADD(WEEK, 4, @StartDate), 5, 16, 124, 105, 21, 100),
(1, DATEADD(WEEK, 5, @StartDate), 6, 17, 137, 117, 24, 140),
(1, DATEADD(WEEK, 6, @StartDate), 7, 18, 150, 129, 27, 190),
(1, DATEADD(WEEK, 7, @StartDate), 8, 19, 162, 141, 30, 240),
(1, DATEADD(WEEK, 8, @StartDate), 9, 20, 175, 152, 33, 300),
(1, DATEADD(WEEK, 9, @StartDate), 10, 21, 187, 164, 36, 360),
(1, DATEADD(WEEK, 10, @StartDate), 11, 22, 198, 175, 39, 430),
(1, DATEADD(WEEK, 11, @StartDate), 12, 23, 210, 186, 42, 501),
(1, DATEADD(WEEK, 12, @StartDate), 13, 24, 221, 197, 44, 600),
(1, DATEADD(WEEK, 13, @StartDate), 14, 25, 232, 208, 47, 660),
(1, DATEADD(WEEK, 14, @StartDate), 15, 26, 242, 219, 49, 760),
(1, DATEADD(WEEK, 15, @StartDate), 16, 27, 252, 229, 52, 875),
(1, DATEADD(WEEK, 16, @StartDate), 17, 28, 262, 240, 54, 1005),
(1, DATEADD(WEEK, 17, @StartDate), 18, 29, 271, 250, 56, 1153),
(1, DATEADD(WEEK, 18, @StartDate), 19, 30, 280, 260, 59, 1319),
(1, DATEADD(WEEK, 19, @StartDate), 20, 31, 288, 270, 61, 1502),
(1, DATEADD(WEEK, 20, @StartDate), 21, 32, 296, 280, 63, 1702),
(1, DATEADD(WEEK, 21, @StartDate), 22, 33, 304, 290, 65, 1918),
(1, DATEADD(WEEK, 22, @StartDate), 23, 34, 311, 299, 67, 2146),
(1, DATEADD(WEEK, 23, @StartDate), 24, 35, 318, 309, 68, 2383),
(1, DATEADD(WEEK, 24, @StartDate), 25, 36, 324, 318, 70, 2622),
(1, DATEADD(WEEK, 25, @StartDate), 26, 37, 330, 327, 72, 2859),
(1, DATEADD(WEEK, 26, @StartDate), 27, 38, 335, 336, 73, 3083),
(1, DATEADD(WEEK, 27, @StartDate), 28, 39, 340, 345, 75, 3288),
(1, DATEADD(WEEK, 28, @StartDate), 29, 40, 344, 354, 76, 3462);


INSERT INTO [dbo].[GrowthStandard] (GestationalAge, HC_Median, AC_Median, FL_Median, EFW_Median)
VALUES
(12, 70, 56, 8, 14),
(13, 84, 69, 11, 23),
(14, 98, 81, 15, 42),
(15, 111, 93, 18, 70),
(16, 124, 105, 21, 100),
(17, 137, 117, 24, 140),
(18, 150, 129, 27, 190),
(19, 162, 141, 30, 240),
(20, 175, 152, 33, 300),
(21, 187, 164, 36, 360),
(22, 198, 175, 39, 430),
(23, 210, 186, 42, 501),
(24, 221, 197, 44, 600),
(25, 232, 208, 47, 660),
(26, 242, 219, 49, 760),
(27, 252, 229, 52, 875),
(28, 262, 240, 54, 1005),
(29, 271, 250, 56, 1153),
(30, 280, 260, 59, 1319),
(31, 288, 270, 61, 1502),
(32, 296, 280, 63, 1702),
(33, 304, 290, 65, 1918),
(34, 311, 299, 67, 2146),
(35, 318, 309, 68, 2383),
(36, 324, 318, 70, 2622),
(37, 330, 327, 72, 2859),
(38, 335, 336, 73, 3083),
(39, 340, 345, 75, 3288),
(40, 344, 354, 76, 3462);

