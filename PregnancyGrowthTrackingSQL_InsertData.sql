INSERT INTO Membership (Description, Price)
VALUES 
    ('Free', 0),
    ('Pay', 9.99);

INSERT INTO Role (RoleName) 
VALUES 
('admin'),
('vip'),
('member');

INSERT INTO [dbo].[User] 
([UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) 
VALUES 
('admin_user', 'Admin User', 'admin@example.com', 'admin123', '1980-01-01', '0123456789', 1, 1, NULL, '2025-02-26 19:22:07.843'),
('vip1', 'Vip One', 'vip1@example.com', 'vip123', '1990-02-01', '0987654321', 1, 2, NULL, '2025-02-26 19:22:07.843'),
('vip2', 'Vip Two', 'vip2@example.com', 'vip123', '1992-03-01', '0987654322', 1, 2, NULL, '2025-02-26 19:22:07.843'),
('vip3', 'Vip Three', 'vip3@example.com', 'vip123', '1994-04-01', '0987654323', 1, 2, NULL, '2025-02-26 19:22:07.843'),
('vip4', 'Vip Four', 'vip4@example.com', 'vip123', '1996-05-01', '0987654324', 1, 2, NULL, '2025-02-26 19:22:07.843'),
('vip5', 'Vip Five', 'vip5@example.com', 'vip123', '1998-06-01', '0987654325', 1, 2, NULL, '2025-02-26 19:22:07.843'),
('member1', 'Member One', 'member1@example.com', 'member123', '2000-07-01', '0987654326', 1, 3, NULL, '2025-02-26 19:22:07.843'),
('member2', 'Member Two', 'member2@example.com', 'member123', '2001-08-01', '0987654327', 1, 3, NULL, '2025-02-26 19:22:07.843'),
('member3', 'Member Three', 'member3@example.com', 'member123', '2002-09-01', '0987654328', 1, 3, NULL, '2025-02-26 19:22:07.843'),
('member4', 'Member Four', 'member4@example.com', 'member123', '2003-10-01', '0987654329', 1, 3, NULL, '2025-02-26 19:22:07.843'),
('member5', 'Member Five', 'member5@example.com', 'member123', '2004-11-01', '0987654330', 1, 3, NULL, '2025-02-26 19:22:07.843'),
('member6', 'Member Six', 'member6@example.com', 'member123', '2005-12-01', '0987654331', 1, 3, NULL, '2025-02-26 19:22:07.843'),
('member7', 'Member Seven', 'member7@example.com', 'member123', '2006-01-01', '0987654332', 1, 3, NULL, '2025-02-26 19:22:07.843'),
('member8', 'Member Eight', 'member8@example.com', 'member123', '2007-02-01', '0987654333', 1, 3, NULL, '2025-02-26 19:22:07.843'),
('member9', 'Member Nine', 'member9@example.com', 'member123', '2008-03-01', '0987654334', 1, 3, NULL, '2025-02-26 19:22:07.843'),
('member10', 'Member Ten', 'member10@example.com', 'member123', '2009-04-01', '0987654335', 1, 3, NULL, '2025-02-26 19:22:07.843');

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

-- Chèn dữ liệu vào bảng Blog
INSERT INTO Blog (Title, Body)
VALUES
('His mother had always taught him', 'His mother had always taught him not to ever think of himself as better than others. He''d tried to live by this motto. He never looked down on those who were less fortunate or who had less money than him. But the stupidity of the group of people he was talking to made him change his mind.'),
('He was an expert but not in a discipline', 'He was an expert but not in a discipline that anyone could fully appreciate. He knew how to hold the cone just right so that the soft server ice-cream fell into it at the precise angle to form a perfect cone each and every time. It had taken years to perfect and he could now do it without even putting any thought behind it.'),
('Dave watched as the forest burned up on the hill.', 'Dave watched as the forest burned up on the hill, only a few miles from her house. The car had been hastily packed and Marta was inside trying to round up the last of the pets. Dave went through his mental list of the most important papers and documents that they couldn''t leave behind. He scolded himself for not having prepared these better in advance and hoped that he had remembered everything that was needed. He continued to wait for Marta to appear with the pets, but she still was nowhere to be seen.'),
('All he wanted was a candy bar.', 'All he wanted was a candy bar. It didn''t seem like a difficult request to comprehend, but the clerk remained frozen and didn''t seem to want to honor the request. It might have had something to do with the gun pointed at his face.'),
('Hopes and dreams were dashed that day.', 'Hopes and dreams were dashed that day. It should have been expected, but it still came as a shock. The warning signs had been ignored in favor of the possibility, however remote, that it could actually happen. That possibility had grown from hope to an undeniable belief it must be destiny. That was until it wasn''t and the hopes and dreams came crashing down.'),
('Dave wasn''t exactly sure how he had ended up', 'Dave wasn''t exactly sure how he had ended up in this predicament. He ran through all the events that had lead to this current situation and it still didn''t make sense. He wanted to spend some time to try and make sense of it all, but he had higher priorities at the moment. The first was how to get out of his current situation of being naked in a tree with snow falling all around and no way for him to get down.'),
('This is important to remember.', 'This is important to remember. Love isn''t like pie. You don''t need to divide it among all your friends and loved ones. No matter how much love you give, you can always give more. It doesn''t run out, so don''t try to hold back giving it as if it may one day run out. Give it freely and as much as you want.'),
('One can cook on and with an open fire.', 'One can cook on and with an open fire. These are some of the ways to cook with fire outside. Cooking meat using a spit is a great way to evenly cook meat. In order to keep meat from burning, it''s best to slowly rotate it.'),
('There are different types of secrets.', 'There are different types of secrets. She had held onto plenty of them during her life, but this one was different. She found herself holding onto the worst type. It was the type of secret that could gnaw away at your insides if you didn''t tell someone about it, but it could end up getting you killed if you did.'),
('They rushed out the door.', 'They rushed out the door, grabbing anything and everything they could think of they might need. There was no time to double-check to make sure they weren''t leaving something important behind. Everything was thrown into the car and they sped off. Thirty minutes later they were safe and that was when it dawned on them that they had forgotten the most important thing of all.'),
('It wasn''t quite yet time to panic.', 'It wasn''t quite yet time to panic. There was still time to salvage the situation. At least that is what she was telling himself. The reality was that it was time to panic and there wasn''t time to salvage the situation, but he continued to delude himself into believing there was.'),
('She was aware that things could go wrong.', 'She was aware that things could go wrong. In fact, she had trained her entire life in anticipation that things would go wrong one day. She had quiet confidence as she started to see that this was the day that all her training would be worthwhile and useful. At this point, she had no idea just how wrong everything would go that day.'),
('She wanted rainbow hair.', 'She wanted rainbow hair. That''s what she told the hairdresser. It should be deep rainbow colors, too. She wasn''t interested in pastel rainbow hair. She wanted it deep and vibrant so there was no doubt that she had done this on purpose.'),
('The paper was blank.', 'The paper was blank. It shouldn''t have been. There should have been writing on the paper, at least a paragraph if not more. The fact that the writing wasn''t there was frustrating. Actually, it was even more than frustrating. It was downright distressing.'),
('The trees, therefore, must be such old', 'The trees, therefore, must be such old and primitive techniques that they thought nothing of them, deeming them so inconsequential that even savages like us would know of them and not be suspicious. At that, they probably didn''t have too much time after they detected us orbiting and intending to land. And if that were true, there could be only one place where their civilization was hidden.'),
('There was only one way to do things in the Statton house.', 'There was only one way to do things in the Statton house. That one way was to do exactly what the father, Charlie, demanded. He made the decisions and everyone else followed without question. That was until today.'),
('She was in a hurry.', 'She was in a hurry. Not the standard hurry when you''re in a rush to get someplace, but a frantic hurry. The type of hurry where a few seconds could mean life or death. She raced down the road ignoring speed limits and weaving between cars. She was only a few minutes away when traffic came to a dead standstill on the road ahead.'),
('She had a terrible habit of comparing her life to others', 'She had a terrible habit of comparing her life to others. She realized that their life experiences were completely different than her own and that she saw only what they wanted her to see, but that didn''t matter. She still compared herself and yearned for what she thought they had and she didn''t.'),
('The rain and wind abruptly stopped.', 'The rain and wind abruptly stopped, but the sky still had the gray swirls of storms in the distance. Dave knew this feeling all too well. The calm before the storm. He only had a limited amount of time before all Hell broke loose, but he stopped to admire the calmness. Maybe it would be different this time, he thought, with the knowledge deep within that it wouldn''t.'),
('He couldn''t remember exactly where he had read it', 'He couldn''t remember exactly where he had read it, but he was sure that he had. The fact that she didn''t believe him was quite frustrating as he began to search the Internet to find the article. It wasn''t as if it was something that seemed impossible. Yet she insisted on always seeing the source whenever he stated a fact.'),
('He wandered down the stairs and into the basement', 'He wandered down the stairs and into the basement. The damp, musty smell of un-use hung in the air. A single, small window let in a glimmer of light, but this simply made the shadows in the basement deeper. He inhaled deeply and looked around at a mess that had been accumulating for over 25 years. He was positive that this was the place he wanted to live.'),
('She has seen this scene before.', 'She has seen this scene before. It had come to her in dreams many times before. She had to pinch herself to make sure it wasn''t a dream again. As her fingers squeezed against her arm, she felt the pain. It was this pain that immediately woke her up.'),
('It''s an unfortunate reality that we don''t teach people how to make money', 'It''s an unfortunate reality that we don''t teach people how to make money (beyond getting a 9 to 5 job) as part of our education system. The truth is there are a lot of different, legitimate ways to make money. That doesn''t mean they are easy and that you won''t have to work hard to succeed, but it does mean that if you''re willing to open your mind a bit you don''t have to be stuck in an office from 9 to 5 for the next fifty years of your life.'),
('The robot clicked disapprovingly.', 'The robot clicked disapprovingly, gurgled briefly inside its cubical interior and extruded a pony glass of brownish liquid. "Sir, you will undoubtedly end up in a drunkard''s grave, dead of hepatic cirrhosis," it informed me virtuously as it returned my ID card. I glared as I pushed the glass across the table.'),
('It went through such rapid contortions', 'It went through such rapid contortions that the little bear was forced to change his hold on it so many times he became confused in the darkness, and could not, for the life of him, tell whether he held the sheep right side up, or upside down. But that point was decided for him a moment later by the animal itself, who, with a sudden twist, jabbed its horns so hard into his lowest ribs that he gave a grunt of anger and disgust.'),
('She patiently waited for his number to be called.', 'She patiently waited for his number to be called. She had no desire to be there, but her mom had insisted that she go. She''s resisted at first, but over time she realized it was simply easier to appease her and go. Mom tended to be that way. She would keep insisting until you wore down and did what she wanted. So, here she sat, patiently waiting for her number to be called.'),
('Ten more steps.', 'If he could take ten more steps it would be over, but his legs wouldn''t move. He tried to will them to work, but they wouldn''t listen to his brain. Ten more steps and it would be over but it didn''t appear he would be able to do it.'),
('He had three simple rules by which he lived.', 'He had three simple rules by which he lived. The first was to never eat blue food. There was nothing in nature that was edible that was blue. People often asked about blueberries, but everyone knows those are actually purple. He understood it was one of the stranger rules to live by, but it had served him well thus far in the 50+ years of his life.'),
('The chair sat in the corner where it had been', 'The chair sat in the corner where it had been for over 25 years. The only difference was there was someone actually sitting in it. How long had it been since someone had done that? Ten years or more he imagined. Yet there was no denying the presence in the chair now.'),
('Things aren''t going well at all', 'Things aren''t going well at all with mom today. She is just a limp noodle and wants to sleep all the time. I sure hope that things get better soon.');


-- Chèn dữ liệu vào bảng Category (chỉ chèn nếu chưa tồn tại để tránh trùng lặp)
INSERT INTO Category (CategoryName)
SELECT DISTINCT tag FROM (
    VALUES ('history'), ('american'), ('crime'), ('french'), ('fiction'), ('english'), ('magical'), ('mystery'), ('love'), ('classic')
) AS Categories(tag)
WHERE NOT EXISTS (
    SELECT 1 FROM Category c WHERE c.CategoryName = Categories.tag
);

-- Chèn dữ liệu vào bảng BlogCate
INSERT INTO BlogCate (BlogId, CategoryId)
SELECT b.BlogId, c.CategoryId
FROM Blog b
JOIN (
    VALUES
    (1, 'history'), (1, 'american'), (1, 'crime'),
    (2, 'french'), (2, 'fiction'), (2, 'english'),
    (3, 'magical'), (3, 'history'), (3, 'french'),
    (4, 'mystery'), (4, 'english'), (4, 'american'),
    (5, 'crime'), (5, 'mystery'), (5, 'love'),
    (6, 'english'), (6, 'classic'), (6, 'american'),
    (7, 'magical'), (7, 'crime'),
    (8, 'american'), (8, 'english'),
    (9, 'american'), (9, 'history'), (9, 'magical'),
    (10, 'fiction'), (10, 'magical'), (10, 'history'),
    (11, 'mystery'), (11, 'american'), (11, 'history'),
    (12, 'love'), (12, 'english'),
    (13, 'mystery'), (13, 'classic'), (13, 'french'),
    (14, 'mystery'), (14, 'english'), (14, 'love'),
    (15, 'fiction'), (15, 'history'), (15, 'crime'),
    (16, 'magical'), (16, 'french'), (16, 'american'),
    (17, 'french'), (17, 'magical'), (17, 'english'),
    (18, 'history'), (18, 'french'), (18, 'love'),
    (19, 'fiction'), (19, 'crime'), (19, 'magical'),
    (20, 'french'), (20, 'classic'),
    (21, 'french'), (21, 'american'),
    (22, 'classic'), (22, 'love'), (22, 'history'),
    (23, 'crime'), (23, 'english'),
	(24, 'crime'), (24, 'american'), (24, 'love'),
    (25, 'fiction'), (25, 'history'), (25, 'french'),
    (26, 'french'), (26, 'mystery'), (26, 'crime'),
    (27, 'mystery'), (27, 'classic'), (27, 'love'),
    (28, 'crime'), (28, 'love'),
    (29, 'mystery'), (29, 'american'),
    (30, 'american'), (30, 'love'), (30, 'fiction')
) AS BlogTags(BlogId, CategoryName)
ON b.BlogId = BlogTags.BlogId
JOIN Category c ON BlogTags.CategoryName = c.CategoryName;