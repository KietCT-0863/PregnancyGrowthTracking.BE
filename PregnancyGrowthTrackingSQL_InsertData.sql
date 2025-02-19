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
