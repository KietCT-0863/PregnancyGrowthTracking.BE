USE [PregnancyGrowthTrackingDB]
GO
/****** Object:  Table [dbo].[Blog]    Script Date: 31/03/2025 2:37:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog](
	[BlogId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[BlogImageUrl] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[BlogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogCate]    Script Date: 31/03/2025 2:37:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogCate](
	[BlogCateId] [int] IDENTITY(1,1) NOT NULL,
	[BlogId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BlogCateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 31/03/2025 2:37:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CommentLike]    Script Date: 31/03/2025 2:37:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommentLike](
	[CommentLikeId] [int] IDENTITY(1,1) NOT NULL,
	[CommentId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CommentLikeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Foetus]    Script Date: 31/03/2025 2:37:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Foetus](
	[FoetusId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[GestationalAge] [int] NULL,
	[ExpectedBirthDate] [date] NULL,
	[Gender] [nvarchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[FoetusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GrowthData]    Script Date: 31/03/2025 2:37:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrowthData](
	[GrowthDataId] [int] IDENTITY(1,1) NOT NULL,
	[FoetusId] [int] NULL,
	[Date] [date] NULL,
	[GrowthStandardId] [int] NULL,
	[Age] [int] NULL,
	[HC] [float] NULL,
	[AC] [float] NULL,
	[FL] [float] NULL,
	[EFW] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[GrowthDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GrowthStandard]    Script Date: 31/03/2025 2:37:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrowthStandard](
	[GrowthStandardId] [int] IDENTITY(1,1) NOT NULL,
	[GestationalAge] [int] NULL,
	[HC_Median] [float] NULL,
	[AC_Median] [float] NULL,
	[FL_Median] [float] NULL,
	[EFW_Median] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[GrowthStandardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Membership]    Script Date: 31/03/2025 2:37:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Membership](
	[MembershipId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[Price] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[MembershipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 31/03/2025 2:37:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[MembershipId] [int] NULL,
	[Date] [date] NULL,
	[TotalPrice] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Post]    Script Date: 31/03/2025 2:37:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[PostId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Body] [nvarchar](255) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[PostImageUrl] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostComment]    Script Date: 31/03/2025 2:37:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostComment](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[Comment] [nvarchar](max) NOT NULL,
	[PostId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ParentCommentId] [int] NULL,
	[CommentImageUrl] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostLike]    Script Date: 31/03/2025 2:37:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostLike](
	[PostLikeId] [int] IDENTITY(1,1) NOT NULL,
	[PostId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PostLikeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostTag]    Script Date: 31/03/2025 2:37:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostTag](
	[PostTagId] [int] IDENTITY(1,1) NOT NULL,
	[PostId] [int] NOT NULL,
	[TagId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PostTagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 31/03/2025 2:37:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tag]    Script Date: 31/03/2025 2:37:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tag](
	[TagId] [int] IDENTITY(1,1) NOT NULL,
	[TagName] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 31/03/2025 2:37:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[FullName] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[Password] [nvarchar](50) NULL,
	[DOB] [date] NULL,
	[Phone] [nvarchar](15) NULL,
	[Available] [bit] NULL,
	[RoleId] [int] NULL,
	[ProfileImageUrl] [nvarchar](255) NULL,
	[CreatedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserNote]    Script Date: 31/03/2025 2:37:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserNote](
	[NoteId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Date] [date] NULL,
	[Diagnosis] [nvarchar](255) NULL,
	[Note] [nvarchar](255) NULL,
	[Detail] [nvarchar](255) NULL,
	[UserNotePhoto] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserReminders]    Script Date: 31/03/2025 2:37:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserReminders](
	[RemindId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Date] [date] NULL,
	[Notification] [nvarchar](510) NULL,
	[Title] [nvarchar](510) NOT NULL,
	[ReminderType] [nvarchar](510) NULL,
	[IsEmailSent] [bit] NOT NULL,
	[Time] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[RemindId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Blog] ON 
GO
INSERT [dbo].[Blog] ([BlogId], [Title], [Body], [BlogImageUrl]) VALUES (1, N'Chăm Sóc Sức Khỏe Khi Mang Thai: Mẹ Bầu Cần Biết Gì?', N'Mang thai là một hành trình tuyệt vời nhưng cũng đầy thử thách đối với các mẹ bầu.
Việc chăm sóc sức khỏe trong thai kỳ không chỉ giúp mẹ bầu khỏe mạnh mà còn tạo điều kiện tốt nhất
cho sự phát triển của thai nhi.

1. Chế Độ Dinh Dưỡng Hợp Lý
Dinh dưỡng đóng vai trò quan trọng trong suốt thai kỳ, giúp thai nhi phát triển toàn diện và mẹ bầu có đủ sức khỏe.
- Bổ sung đầy đủ dưỡng chất: Protein, sắt, canxi, axit folic, omega-3 và vitamin D là những dưỡng chất quan trọng cần thiết.
- Ăn nhiều rau xanh và trái cây: Cung cấp chất xơ, vitamin và khoáng chất giúp mẹ bầu tiêu hóa tốt và hạn chế táo bón.
- Uống đủ nước: Khoảng 2-3 lít nước mỗi ngày để duy trì lượng nước ối và giúp cơ thể trao đổi chất tốt hơn.
- Hạn chế thực phẩm chế biến sẵn, nhiều đường và dầu mỡ để tránh tăng cân quá mức và nguy cơ tiểu đường thai kỳ.

2. Duy Trì Thói Quen Vận Động
Vận động nhẹ nhàng giúp mẹ bầu khỏe mạnh, giảm căng thẳng và chuẩn bị tốt cho quá trình sinh nở.
- Tập yoga bầu: Giúp tăng cường sự dẻo dai, giảm đau lưng và cải thiện giấc ngủ.
- Đi bộ nhẹ nhàng: Mỗi ngày 30 phút giúp tăng cường tuần hoàn máu và giữ tinh thần thư thái.
- Bơi lội: Là một lựa chọn tuyệt vời giúp giảm áp lực lên cột sống và tăng cường sức khỏe tim mạch.

3. Kiểm Tra Sức Khỏe Định Kỳ
Khám thai định kỳ giúp theo dõi sự phát triển của thai nhi và phát hiện sớm những bất thường nếu có.
- Lịch khám thai chuẩn: Nên khám thai ít nhất 8-10 lần trong suốt thai kỳ.
- Siêu âm thai: Theo dõi sự phát triển của bé và kiểm tra các dấu hiệu bất thường.
- Xét nghiệm máu và nước tiểu: Kiểm tra thiếu máu, tiểu đường thai kỳ và các bệnh lý khác.

4. Chăm Sóc Tâm Lý và Giấc Ngủ
Tâm lý thoải mái và giấc ngủ chất lượng là yếu tố quan trọng giúp mẹ bầu có một thai kỳ khỏe mạnh.
- Giữ tinh thần vui vẻ, tránh căng thẳng: Nghe nhạc, đọc sách hoặc tập thiền giúp thư giãn.
- Ngủ đủ giấc: Trung bình 7-9 tiếng mỗi ngày để cơ thể hồi phục và giảm mệt mỏi.
- Tránh sử dụng điện thoại hoặc thiết bị điện tử trước khi ngủ để có giấc ngủ ngon hơn.

5. Những Điều Cần Tránh
- Tránh rượu, bia, thuốc lá và caffeine: Những chất này có thể gây ảnh hưởng đến sự phát triển của thai nhi.
- Không làm việc quá sức: Mẹ bầu nên có chế độ nghỉ ngơi hợp lý, tránh mang vác nặng.
- Không tự ý dùng thuốc: Nếu cần sử dụng bất kỳ loại thuốc nào, hãy tham khảo ý kiến bác sĩ.

Kết Luận
Sức khỏe thai kỳ là yếu tố quan trọng quyết định sự phát triển của thai nhi và sự an toàn của mẹ bầu.
Việc duy trì một chế độ dinh dưỡng hợp lý, vận động đều đặn, kiểm tra sức khỏe định kỳ và giữ tinh thần thoải mái
sẽ giúp mẹ bầu có một thai kỳ khỏe mạnh và an toàn.', N'https://pregnancy-growth-tracking-blog.s3.amazonaws.com/blog/2c63eecf-688c-4ec5-b8f2-b32010eca8f9_1.jpg')
GO
INSERT [dbo].[Blog] ([BlogId], [Title], [Body], [BlogImageUrl]) VALUES (2, N'Dinh Dưỡng Khi Mang Thai: Ăn Gì Để Mẹ Khỏe, Bé Phát Triển Tốt?', N'Mang thai là giai đoạn quan trọng mà mẹ bầu cần đặc biệt quan tâm đến chế độ dinh dưỡng. Một thực đơn khoa học không chỉ giúp mẹ có sức khỏe tốt mà còn hỗ trợ thai nhi phát triển toàn diện. Vậy mẹ bầu nên ăn gì và cần lưu ý điều gì trong thai kỳ?  

1. Những Nhóm Dinh Dưỡng Quan Trọng Cho Mẹ Bầu  

1.1. Axit folic - Hỗ trợ phát triển não bộ thai nhi  
Axit folic giúp ngăn ngừa dị tật ống thần kinh ở thai nhi.  
- Nguồn thực phẩm giàu axit folic: Rau bina, súp lơ xanh, bơ, các loại đậu, ngũ cốc nguyên cám, cam, chuối.  

1.2. Sắt - Ngăn ngừa thiếu máu cho mẹ và bé  
Sắt giúp tạo hồng cầu, đảm bảo oxy cho thai nhi.  
- Nguồn thực phẩm giàu sắt: Thịt đỏ, gan động vật, trứng, rau xanh đậm.  

1.3. Canxi - Giúp xương và răng bé chắc khỏe  
Canxi rất cần thiết cho sự phát triển của thai nhi.  
- Nguồn thực phẩm giàu canxi: Sữa, phô mai, sữa chua, hạnh nhân, rau xanh đậm.  

1.4. DHA - Phát triển trí não và thị giác thai nhi  
DHA giúp bé phát triển trí não và thị giác tốt hơn.  
- Nguồn thực phẩm giàu DHA: Cá hồi, cá chép, hạt chia, quả óc chó, dầu cá.  

2. Những Thực Phẩm Mẹ Bầu Nên Tránh  
- Đồ ăn tái sống như sushi, gỏi cá.  
- Đồ uống chứa caffeine và cồn.  
- Thực phẩm chế biến sẵn, nhiều chất bảo quản.  
- Đồ ăn quá mặn hoặc nhiều đường.  

3. Gợi Ý Thực Đơn Dinh Dưỡng Cho Mẹ Bầu  
- Bữa sáng: 1 ly sữa + bánh mì nguyên cám + trứng + trái cây.  
- Bữa trưa: Cơm gạo lứt + cá hồi kho + canh rau ngót + nước cam.  
- Bữa tối: Cháo yến mạch + thịt gà + salad bơ + sữa chua.  
- Bữa phụ: Hạt óc chó, hạnh nhân, sinh tố bơ, chuối.  

4. Kết Luận  
Dinh dưỡng đóng vai trò quan trọng giúp mẹ khỏe mạnh và bé phát triển toàn diện. Mẹ bầu hãy chọn thực phẩm lành mạnh và duy trì lối sống khoa học để có thai kỳ khỏe mạnh!', N'https://pregnancy-growth-tracking-blog.s3.amazonaws.com/blog/81b0fe4b-74cf-4e6a-af23-b5a76230538e_2.jpeg')
GO
INSERT [dbo].[Blog] ([BlogId], [Title], [Body], [BlogImageUrl]) VALUES (3, N'Các Giai Đoạn Phát Triển Của Thai Nhi Và Những Điều Mẹ Cần Biết', N'Mang thai là một hành trình kỳ diệu với nhiều thay đổi quan trọng trong cơ thể mẹ bầu. Thai nhi sẽ phát triển qua từng giai đoạn, từ một phôi nhỏ bé đến khi trở thành một em bé hoàn chỉnh. Hiểu rõ sự phát triển của bé qua từng tuần giúp mẹ có chế độ dinh dưỡng và chăm sóc hợp lý để thai kỳ khỏe mạnh.  

1. Ba Giai Đoạn Phát Triển Của Thai Nhi  

1.1 Tam cá nguyệt thứ nhất (Tuần 1 - Tuần 12)  
- Đây là giai đoạn hình thành quan trọng nhất của thai nhi. Tim thai bắt đầu đập vào khoảng tuần thứ 5-6.  
- Các cơ quan quan trọng như não, tim, gan, và phổi bắt đầu hình thành.  
- Tay, chân, mắt, mũi, miệng dần phát triển rõ rệt từ tuần thứ 8 trở đi.  
- Mẹ bầu có thể cảm thấy buồn nôn, mệt mỏi do thay đổi nội tiết tố.  

1.2 Tam cá nguyệt thứ hai (Tuần 13 - Tuần 26)  
- Thai nhi bắt đầu cử động nhiều hơn, mẹ có thể cảm nhận cú đạp đầu tiên vào khoảng tuần 18-22.  
- Xương và cơ bắp của bé phát triển mạnh mẽ hơn.  
- Hệ thần kinh và các giác quan như thị giác, thính giác bắt đầu hoàn thiện.  
- Bé có thể nghe được giọng nói của mẹ từ tuần thứ 20.  

1.3 Tam cá nguyệt thứ ba (Tuần 27 - Tuần 40)  
- Thai nhi phát triển nhanh chóng và đạt cân nặng từ 2.5 - 3.5kg khi đủ tháng.  
- Phổi của bé tiếp tục trưởng thành để chuẩn bị cho quá trình hô hấp sau khi chào đời.  
- Bé phản ứng với ánh sáng, âm thanh, có thể nấc cụt và cử động mạnh hơn.  

2. Những Dấu Hiệu Sắp Sinh Mẹ Cần Lưu Ý  
- Bụng tụt xuống thấp, cảm giác nặng nề hơn.  
- Các cơn gò tử cung xuất hiện thường xuyên và mạnh hơn.  
- Dịch nhầy cổ tử cung tiết ra nhiều hơn.  
- Rò rỉ nước ối hoặc vỡ ối.  

3. Kết Luận  
Mỗi giai đoạn thai kỳ đều có những thay đổi quan trọng mà mẹ cần theo dõi sát sao. Duy trì chế độ ăn uống khoa học, nghỉ ngơi hợp lý và khám thai định kỳ sẽ giúp thai nhi phát triển khỏe mạnh.', N'https://pregnancy-growth-tracking-blog.s3.amazonaws.com/blog/f2b51300-9fcf-400f-b0dc-dda95fde272b_3.webp')
GO
INSERT [dbo].[Blog] ([BlogId], [Title], [Body], [BlogImageUrl]) VALUES (4, N'10 Triệu Chứng Thường Gặp Khi Mang Thai Và Cách Xử Lý', N'Mang thai là một hành trình đầy hạnh phúc nhưng cũng đi kèm với nhiều thay đổi về sức khỏe và tâm lý. Trong suốt thai kỳ, mẹ bầu có thể gặp phải nhiều triệu chứng khác nhau, từ ốm nghén đến đau lưng, mất ngủ hay chuột rút. Hiểu rõ nguyên nhân và cách xử lý sẽ giúp mẹ bầu có một thai kỳ thoải mái và khỏe mạnh hơn.  

1. Ốm Nghén (Buồn Nôn, Nôn Mửa)  
Nguyên nhân: Do sự thay đổi nội tiết tố, thường xuất hiện trong 3 tháng đầu thai kỳ.  
Cách xử lý: Chia nhỏ bữa ăn, uống nước gừng ấm, tránh xa mùi thức ăn mạnh.  

2. Mệt Mỏi, Buồn Ngủ  
Nguyên nhân: Do hormone progesterone làm mẹ bầu uể oải.  
Cách xử lý: Nghỉ ngơi nhiều, bổ sung sắt và vitamin B12, tập thể dục nhẹ.  

3. Đau Lưng, Đau Hông  
Nguyên nhân: Do thai nhi phát triển làm thay đổi trọng tâm cơ thể.  
Cách xử lý: Tránh đứng lâu, sử dụng gối hỗ trợ khi ngủ, massage nhẹ.  

4. Chuột Rút Chân  
Nguyên nhân: Thiếu canxi, magie, kali.  
Cách xử lý: Bổ sung thực phẩm giàu canxi, uống nước đầy đủ, massage chân.  

5. Táo Bón, Đầy Hơi  
Nguyên nhân: Hormone làm giảm nhu động ruột.  
Cách xử lý: Uống nhiều nước, ăn thực phẩm giàu chất xơ, đi bộ nhẹ.  

6. Thèm Ăn Hoặc Chán Ăn  
Nguyên nhân: Do thay đổi hormone.  
Cách xử lý: Chế biến món ăn khác đi, ăn thực phẩm bổ dưỡng.  

7. Khó Thở  
Nguyên nhân: Do tử cung lớn chèn ép cơ hoành.  
Cách xử lý: Ngồi thẳng lưng, hít thở sâu.  

8. Thay Đổi Tâm Trạng  
Nguyên nhân: Hormone thay đổi.  
Cách xử lý: Nghỉ ngơi, tâm sự với người thân.  

9. Giãn Tĩnh Mạch, Sưng Chân  
Nguyên nhân: Do lưu lượng máu tăng.  
Cách xử lý: Tránh đứng lâu, gác chân cao khi ngồi.  

10. Mất Ngủ  
Nguyên nhân: Do căng thẳng, tư thế ngủ không thoải mái.  
Cách xử lý: Ngủ nghiêng trái, sử dụng gối bà bầu.', N'https://pregnancy-growth-tracking-blog.s3.amazonaws.com/blog/5b555aa2-ebc4-4e57-b941-b57aef57ac91_4.webp')
GO
INSERT [dbo].[Blog] ([BlogId], [Title], [Body], [BlogImageUrl]) VALUES (5, N'Các Xét Nghiệm & Siêu Âm Quan Trọng Trong Thai Kỳ', N'Trong suốt thai kỳ, các xét nghiệm và siêu âm đóng vai trò quan trọng trong việc theo dõi sức khỏe của mẹ và sự phát triển của thai nhi. Việc thực hiện đúng các xét nghiệm cần thiết theo từng giai đoạn sẽ giúp phát hiện sớm những bất thường (nếu có) và đảm bảo một thai kỳ an toàn.  

1. Xét Nghiệm & Siêu Âm Trong 3 Tháng Đầu (Tuần 1 – Tuần 13)  
- Xét nghiệm máu & nước tiểu: kiểm tra nhóm máu, công thức máu, đường huyết, tầm soát bệnh lây nhiễm.  
- Xét nghiệm đo nồng độ hCG & progesterone: đánh giá tình trạng thai nhi.  
- Siêu âm đo độ mờ da gáy: xác định nguy cơ hội chứng Down.  

2. Xét Nghiệm & Siêu Âm Trong 3 Tháng Giữa (Tuần 14 – Tuần 27)  
- Xét nghiệm Triple Test: đánh giá nguy cơ dị tật bẩm sinh.  
- Siêu âm hình thái học thai nhi: kiểm tra sự phát triển của các cơ quan quan trọng.  
- Xét nghiệm tiểu đường thai kỳ: kiểm tra dung nạp glucose.  

3. Xét Nghiệm & Siêu Âm Trong 3 Tháng Cuối (Tuần 28 – Tuần 40)  
- Xét nghiệm máu & nước tiểu định kỳ: theo dõi sức khỏe mẹ bầu.  
- Siêu âm Doppler: đánh giá lưu lượng máu qua nhau thai.  
- Siêu âm đánh giá nước ối & ngôi thai: kiểm tra tình trạng thai nhi trước sinh.  

Việc tuân thủ các xét nghiệm và siêu âm đúng theo lịch trình sẽ giúp mẹ bầu an tâm hơn và đảm bảo thai nhi phát triển khỏe mạnh. Nếu có bất kỳ dấu hiệu bất thường nào, mẹ nên liên hệ ngay với bác sĩ để được kiểm tra và tư vấn kịp thời.', N'https://pregnancy-growth-tracking-blog.s3.amazonaws.com/blog/0e69ea79-cd23-4722-ad06-5ca0eeaa73ce_5.png')
GO
INSERT [dbo].[Blog] ([BlogId], [Title], [Body], [BlogImageUrl]) VALUES (6, N'Lợi Ích & Bài Tập Thể Dục Tốt Nhất Cho Mẹ Bầu', N'Tập thể dục khi mang thai không chỉ giúp mẹ bầu duy trì sức khỏe, mà còn hỗ trợ quá trình sinh nở diễn ra thuận lợi hơn. Tuy nhiên, không phải bài tập nào cũng phù hợp với thai kỳ. Bài viết này sẽ giúp mẹ bầu hiểu rõ lợi ích của việc vận động và lựa chọn các bài tập an toàn, hiệu quả.  

1. Lợi Ích Của Việc Tập Thể Dục Khi Mang Thai  
- Duy trì cân nặng hợp lý, hạn chế nguy cơ béo phì thai kỳ.  
- Cải thiện tâm trạng & giấc ngủ, giảm căng thẳng.  
- Giảm đau lưng & chuột rút, hỗ trợ cơ xương chắc khỏe.  
- Tăng sức bền, hỗ trợ quá trình sinh nở.  
- Giảm nguy cơ tiểu đường thai kỳ & tiền sản giật.  

2. Các Môn Thể Dục An Toàn Cho Mẹ Bầu  
- Đi bộ: Tốt cho tim mạch, giảm phù chân.  
- Bơi lội: Giảm áp lực lên khớp, thư giãn cơ thể.  
- Yoga bầu: Cải thiện hô hấp, giúp cơ thể linh hoạt hơn.  
- Tập Kegel: Tăng cường cơ sàn chậu, hỗ trợ quá trình sinh nở.  
- Đạp xe tại chỗ: Cải thiện sức khỏe tim mạch, giảm đau lưng.  

3. Lưu Ý Khi Tập Luyện  
- Lắng nghe cơ thể, tránh tập luyện quá sức.  
- Uống đủ nước, tránh mất nước.  
- Tránh bài tập gây áp lực lên bụng.  
- Tham khảo ý kiến bác sĩ nếu có tiền sử bệnh lý.  

Duy trì tập thể dục trong thai kỳ giúp mẹ bầu khỏe mạnh hơn và chuẩn bị tốt cho quá trình sinh nở. Chỉ cần chọn bài tập phù hợp và tập luyện đúng cách, mẹ sẽ có một thai kỳ dễ dàng hơn.', N'https://pregnancy-growth-tracking-blog.s3.amazonaws.com/blog/885a6601-cec1-4144-b70a-68cfc558b8e7_6.webp')
GO
INSERT [dbo].[Blog] ([BlogId], [Title], [Body], [BlogImageUrl]) VALUES (7, N'Danh Sách Đồ Sơ Sinh Cần Chuẩn Bị Trước Khi Sinh', N'Chuẩn bị đồ sơ sinh đầy đủ giúp mẹ bầu yên tâm chào đón bé yêu mà không bị bối rối vào phút chót. Tuy nhiên, không phải món đồ nào cũng cần thiết. Hãy cùng tham khảo danh sách các vật dụng quan trọng nhất để tránh mua sắm lãng phí nhé!  

1. Quần Áo & Phụ Kiện Cho Bé  
- Quần áo sơ sinh (10 bộ)  
- Bao tay, bao chân, mũ sơ sinh (5 – 7 bộ)  
- Khăn xô, khăn sữa (20 cái)  
- Yếm vải (5 cái)  
- Khăn tắm mềm (2 – 3 cái)  

2. Đồ Chăm Sóc & Vệ Sinh Cho Bé  
- Bình sữa & núm ti (2 bình)  
- Tã giấy hoặc tã vải (2 – 3 bịch)  
- Sữa tắm & dầu gội dịu nhẹ  
- Dụng cụ hút mũi  
- Bấm móng tay cho bé  

3. Đồ Dùng Khi Cho Bé Bú  
- Sữa công thức (nếu cần)  
- Máy hâm sữa  
- Dụng cụ rửa bình sữa  
- Gối chống trào ngược  

4. Giường Ngủ & Chăn Gối Cho Bé  
- Nôi hoặc cũi  
- Chăn, gối, đệm sơ sinh  
- Nhiệt kế  

5. Đồ Dùng Khi Ra Ngoài  
- Xe đẩy em bé  
- Túi đựng đồ cho bé  
- Mũ che nắng, áo khoác mỏng  

6. Đồ Dành Cho Mẹ Sau Sinh  
- Băng vệ sinh dành cho mẹ sau sinh  
- Áo lót cho con bú  
- Máy hút sữa  
- Đai nịt bụng sau sinh  

Việc chuẩn bị đồ sơ sinh không cần quá nhiều nhưng phải đủ và phù hợp. Mẹ hãy tham khảo danh sách trên để chuẩn bị chu đáo nhất cho bé yêu nhé!', N'https://pregnancy-growth-tracking-blog.s3.amazonaws.com/blog/64dbac92-c694-4caf-9113-773234ccb57c_7.jpg')
GO
INSERT [dbo].[Blog] ([BlogId], [Title], [Body], [BlogImageUrl]) VALUES (8, N'Sinh Thường Hay Sinh Mổ? So Sánh Ưu Nhược Điểm & Cách Chuẩn Bị', N'Việc chọn phương pháp sinh con là quyết định quan trọng đối với mẹ bầu. Cả sinh thường và sinh mổ đều có ưu nhược điểm riêng. Cùng tìm hiểu chi tiết để có sự chuẩn bị tốt nhất nhé!  

1. Sinh Thường (Sinh Tự Nhiên)  
Ưu điểm:  
- Hồi phục nhanh, ít rủi ro, bé nhận vi khuẩn có lợi.  
Nhược điểm:  
- Đau chuyển dạ, nguy cơ rách tầng sinh môn.  

2. Sinh Mổ  
Ưu điểm:  
- Không đau chuyển dạ, chủ động thời gian sinh.  
Nhược điểm:  
- Hồi phục lâu, nguy cơ nhiễm trùng cao.  

3. Khi Nào Cần Sinh Mổ?  
- Thai ngôi mông, mẹ bị nhau tiền đạo, thai nhi quá lớn,...  

4. So Sánh Sinh Thường & Sinh Mổ  
- Thời gian hồi phục: Sinh thường nhanh hơn.  
- Đau đớn: Sinh thường đau khi chuyển dạ, sinh mổ đau sau khi mổ.  

Cả hai phương pháp đều có ưu nhược điểm riêng, mẹ hãy tham khảo ý kiến bác sĩ để lựa chọn phù hợp!', N'https://pregnancy-growth-tracking-blog.s3.amazonaws.com/blog/3494b495-4c8d-4268-94ac-27ff64d75302_8.jpg')
GO
INSERT [dbo].[Blog] ([BlogId], [Title], [Body], [BlogImageUrl]) VALUES (9, N'Các Mốc Khám Thai Quan Trọng – Mẹ Bầu Cần Chuẩn Bị Gì?', N'Khám thai định kỳ giúp mẹ bầu theo dõi sự phát triển của thai nhi và phát hiện sớm các vấn đề bất thường. Hãy cùng tìm hiểu lịch khám thai chuẩn theo từng giai đoạn nhé!  

1. Lần 1 (Tuần 5 – 8): Khám thai lần đầu, kiểm tra thai đã vào tử cung chưa.  
2. Lần 2 (Tuần 11 – 13): Siêu âm đo độ mờ da gáy, xét nghiệm Double Test.  
3. Lần 3 (Tuần 16 – 18): Xét nghiệm Triple Test, kiểm tra tim thai.  
4. Lần 4 (Tuần 20 – 22): Siêu âm 4D kiểm tra hình thái thai nhi.  
5. Lần 5 (Tuần 24 – 28): Xét nghiệm tiểu đường thai kỳ, tiêm vắc-xin uốn ván.  
6. Lần 6 (Tuần 30 – 32): Kiểm tra nước ối, cân nặng, đánh giá nguy cơ sinh non.  
7. Lần 7 (Tuần 35 – 36): Xét nghiệm vi khuẩn Streptococcus nhóm B, kiểm tra ngôi thai.  
8. Lần 8 (Tuần 38 – 40): Kiểm tra cuối cùng trước khi sinh.  

Lịch khám thai giúp đảm bảo sức khỏe cho cả mẹ và bé. Mẹ đừng bỏ qua bất kỳ mốc khám quan trọng nào nhé!', N'https://pregnancy-growth-tracking-blog.s3.amazonaws.com/blog/6b0c919f-5fdc-4561-8ca5-31ca97bf78d5_9.webp')
GO
INSERT [dbo].[Blog] ([BlogId], [Title], [Body], [BlogImageUrl]) VALUES (10, N'Những Vấn Đề Sức Khỏe Mẹ Bầu Có Thể Gặp & Cách Ứng Phó', N'Một số bệnh lý thai kỳ có thể ảnh hưởng đến sức khỏe của mẹ và bé. Dưới đây là những bệnh lý thường gặp và cách phòng ngừa:  

1. Tiểu Đường Thai Kỳ:  
- Nguyên nhân: Hormone thai kỳ làm giảm insulin.  
- Triệu chứng: Khát nước nhiều, đi tiểu thường xuyên.  
- Phòng ngừa: Ăn uống khoa học, kiểm tra đường huyết định kỳ.  

2. Tăng Huyết Áp Thai Kỳ & Tiền Sản Giật:  
- Nguyên nhân: Rối loạn nội tiết tố, thai đôi.  
- Triệu chứng: Huyết áp cao, phù nề, đau đầu.  
- Phòng ngừa: Ăn nhạt, theo dõi huyết áp.  

3. Thiếu Máu Khi Mang Thai:  
- Nguyên nhân: Thiếu sắt, folate.  
- Triệu chứng: Hoa mắt, chóng mặt, tim đập nhanh.  
- Phòng ngừa: Bổ sung sắt, ăn uống đủ chất.  

4. Đa Ối & Thiểu Ối:  
- Nguyên nhân: Tiểu đường thai kỳ, suy nhau thai.  
- Biến chứng: Nguy cơ sinh non, thai suy dinh dưỡng.  

5. Nhiễm Trùng Đường Tiểu:  
- Nguyên nhân: Vi khuẩn xâm nhập, thai lớn chèn ép.  
- Triệu chứng: Tiểu buốt, đau bụng dưới.  
- Phòng ngừa: Uống nhiều nước, giữ vệ sinh sạch sẽ.  

Mẹ bầu hãy khám thai định kỳ để phát hiện và điều trị sớm nhé!', N'https://pregnancy-growth-tracking-blog.s3.amazonaws.com/blog/63fc4bfa-13b6-45d6-a33e-20c3bbf50784_10.jpg')
GO
INSERT [dbo].[Blog] ([BlogId], [Title], [Body], [BlogImageUrl]) VALUES (11, N'Cách Giữ Tâm Lý Ổn Định Khi Mang Thai – Mẹ An Yên, Bé Khỏe Mạnh', N'Căng thẳng khi mang thai có thể ảnh hưởng đến sức khỏe của mẹ và bé. Dưới đây là những cách giúp mẹ thư giãn và giảm căng thẳng hiệu quả:  

1. Hít thở sâu & thư giãn cơ thể: Giúp mẹ bình tĩnh và ngủ ngon hơn.  
2. Tập yoga & thiền định: Cải thiện tâm trạng, giảm lo âu.  
3. Nghe nhạc & đọc sách thư giãn: Giúp mẹ vui vẻ, bé phát triển tốt.  
4. Massage nhẹ nhàng: Giảm đau lưng, thư giãn cơ bắp.  
5. Chế độ ăn uống hợp lý: Bổ sung thực phẩm tốt cho tâm trạng.  
6. Chia sẻ & nhận sự hỗ trợ: Nói chuyện với người thân, tham gia hội nhóm mẹ bầu.  
7. Đi bộ & hít thở không khí trong lành: Giúp lưu thông khí huyết, giảm stress.  

Mẹ bầu hãy giữ tinh thần thoải mái để có một thai kỳ khỏe mạnh và hạnh phúc nhé!', N'https://pregnancy-growth-tracking-blog.s3.amazonaws.com/blog/f2914098-48b9-4635-b06a-07f3cce75534_11.jpg')
GO
INSERT [dbo].[Blog] ([BlogId], [Title], [Body], [BlogImageUrl]) VALUES (12, N'Mang Thai Ảnh Hưởng Đến Tâm Lý Như Thế Nào? Mẹ Cần Làm Gì Để Luôn Vui Vẻ?', N'Thời kỳ mang thai có thể khiến mẹ bầu trải qua nhiều cảm xúc khác nhau. Dưới đây là những thay đổi tâm lý thường gặp và cách ổn định cảm xúc:  

1. Thay đổi cảm xúc: Mẹ có thể vui vẻ, hạnh phúc nhưng cũng dễ lo lắng, căng thẳng, cáu gắt.  
2. Nguyên nhân gây ảnh hưởng: Hormone thai kỳ, áp lực tài chính, mâu thuẫn gia đình.  
3. Cách giảm căng thẳng:  
   - Dành thời gian chăm sóc bản thân.  
   - Tập yoga, hít thở sâu, thiền định.  
   - Chia sẻ cảm xúc với người thân, nghe nhạc, đọc sách.  
   - Vận động nhẹ nhàng, đi bộ mỗi ngày.  
   - Ăn uống khoa học để duy trì tâm trạng tốt.  

Mẹ bầu hãy thư giãn và tận hưởng thai kỳ một cách nhẹ nhàng để luôn có tâm lý tích cực nhé!', N'https://pregnancy-growth-tracking-blog.s3.amazonaws.com/blog/80e486f0-c02e-42c7-ac36-2073bb04d154_12.jpg')
GO
INSERT [dbo].[Blog] ([BlogId], [Title], [Body], [BlogImageUrl]) VALUES (13, N'Đặt Tên Cho Bé – Gợi Ý Những Cái Tên Đẹp Và Ý Nghĩa Nhất', N'Việc đặt tên cho con không chỉ là chọn một cái tên đẹp mà còn mang ý nghĩa sâu sắc, ảnh hưởng đến cuộc đời của bé. Một cái tên hay sẽ là món quà đầu tiên mà bố mẹ dành tặng cho con.  

1. Những Nguyên Tắc Quan Trọng Khi Đặt Tên Cho Bé  
- Tên có ý nghĩa đẹp: Mong ước tương lai tươi sáng, phẩm chất tốt đẹp.  
- Dễ đọc, dễ nhớ: Giúp con tự tin khi giới thiệu tên mình.  
- Hợp phong thủy, ngũ hành: Chọn tên theo mệnh của con để mang lại may mắn.  
- Tránh trùng với tên người thân: Thể hiện sự tôn trọng trong gia đình.  
- Hợp với họ của bé: Giúp tên bé có âm điệu hài hòa.  

2. Gợi Ý Đặt Tên Bé Trai Ý Nghĩa  
- Tên mạnh mẽ, kiên cường: Thiên Bảo, Minh Quân, Anh Dũng.  
- Tên thông minh, thành công: Tuấn Kiệt, Hữu Minh, Đức Trí.  
- Tên bình an, hạnh phúc: An Khang, Gia Hưng, Trường An.  

3. Gợi Ý Đặt Tên Bé Gái Hay & Thanh Nhã  
- Tên dịu dàng, nữ tính: Diễm Quỳnh, Bích Thảo, Ngọc Lan.  
- Tên thông minh, tài giỏi: Tuệ Nhi, Minh Châu, Bảo Anh.  
- Tên mang ý nghĩa bình an: An Nhiên, Gia Hân, Kim Ngân.  

4. Đặt Tên Cho Bé Theo Ngũ Hành  
Bố mẹ có thể chọn tên hợp mệnh của bé để mang lại may mắn:  
- Mệnh Kim: Bảo, Châu, Nguyên, Kim, Ngân.  
- Mệnh Mộc: Bình, Tùng, Quỳnh, Trúc, Lâm.  
- Mệnh Thủy: Hải, Giang, Vân, Thủy, Tuyết.  
- Mệnh Hỏa: Nhật, Dương, Minh, Ánh, Hồng.  
- Mệnh Thổ: Sơn, Cường, Bảo, Thành, Khang.  

5. Lưu Ý Khi Đặt Tên Cho Bé  
- Tránh đặt tên quá dài, khó đọc.  
- Không nên đặt tên trùng với người lớn trong gia đình.  
- Kiểm tra ý nghĩa để tránh những nghĩa không mong muốn.  

Tên gọi sẽ theo bé suốt cuộc đời, vì vậy bố mẹ hãy chọn một cái tên thật đẹp và ý nghĩa để gửi gắm tình yêu thương nhé!', N'https://pregnancy-growth-tracking-blog.s3.amazonaws.com/blog/8a4bfbc7-fe78-487f-adb0-6bb936d8d91f_13.jpg')
GO
INSERT [dbo].[Blog] ([BlogId], [Title], [Body], [BlogImageUrl]) VALUES (14, N'Mang Thai Vẫn Xinh – Mẹo Làm Đẹp Giúp Mẹ Bầu Tự Tin, Rạng Rỡ', N'Mang thai là giai đoạn cơ thể mẹ bầu có nhiều thay đổi, không chỉ về vóc dáng mà còn về làn da, mái tóc. Tuy nhiên, mẹ hoàn toàn có thể duy trì vẻ đẹp rạng rỡ nếu biết cách chăm sóc đúng. Dưới đây là những bí quyết giúp mẹ bầu luôn xinh đẹp, tự tin trong suốt thai kỳ!  

1. Chăm Sóc Da Khi Mang Thai  
Khi mang thai, nội tiết tố thay đổi có thể khiến làn da của mẹ gặp các vấn đề như sạm nám, mụn, da khô. Để giữ làn da khỏe đẹp, mẹ cần:  
- Dưỡng ẩm da: Uống đủ nước, sử dụng kem dưỡng ẩm an toàn cho bà bầu.  
- Chống nắng cẩn thận: Dùng kem chống nắng có chỉ số SPF từ 30 trở lên, đội mũ rộng vành khi ra ngoài.  
- Làm sạch da đúng cách: Rửa mặt với sữa rửa mặt dịu nhẹ 2 lần/ngày, tránh sản phẩm chứa hóa chất mạnh.  
- Sử dụng mỹ phẩm an toàn: Chọn sản phẩm có nguồn gốc tự nhiên, tránh retinol, paraben, hydroquinone.  

2. Giữ Mái Tóc Khỏe Đẹp  
Nhiều mẹ bầu than phiền tóc bị rụng nhiều hoặc khô xơ trong thai kỳ. Để có mái tóc suôn mượt, mẹ hãy:  
- Gội đầu bằng dầu gội thảo dược: Tránh dầu gội chứa sulfate gây khô tóc.  
- Massage da đầu: Kích thích tuần hoàn máu, giúp tóc mọc chắc khỏe hơn.  
- Bổ sung thực phẩm tốt cho tóc: Ăn nhiều thực phẩm chứa sắt, protein như cá hồi, trứng, rau xanh.  
- Hạn chế nhuộm tóc: Nếu muốn làm đẹp, mẹ nên chọn thuốc nhuộm hữu cơ, không chứa amoniac.  

3. Giữ Gìn Vóc Dáng Gọn Gàng  
Tăng cân khi mang thai là điều bình thường, nhưng mẹ vẫn có thể duy trì vóc dáng cân đối bằng cách:  
- Tập thể dục nhẹ nhàng: Yoga, bơi lội, đi bộ giúp mẹ bầu dẻo dai, hạn chế tăng cân quá mức.  
- Chế độ ăn uống lành mạnh: Hạn chế đồ chiên rán, ăn nhiều rau củ, trái cây, protein tốt.  
- Mặc đồ bầu phù hợp: Chọn trang phục co giãn tốt, giúp mẹ thoải mái nhưng vẫn thanh lịch.  

4. Giữ Tinh Thần Vui Vẻ, Lạc Quan  
Làm đẹp không chỉ là chăm sóc bên ngoài mà còn là giữ tinh thần tươi vui:  
- Ngủ đủ giấc: Hạn chế thức khuya, giúp da luôn căng mịn.  
- Tránh căng thẳng: Nghe nhạc thư giãn, trò chuyện với bạn bè, người thân.  
- Làm điều mình thích: Đọc sách, xem phim, chăm sóc bản thân để luôn vui vẻ.  

Mang thai không có nghĩa là mẹ bầu phải bỏ bê bản thân. Với những bí quyết đơn giản trên, mẹ sẽ luôn rạng rỡ, xinh đẹp trong suốt 9 tháng thai kỳ. Hãy yêu bản thân và tận hưởng khoảng thời gian tuyệt vời này nhé!', N'https://pregnancy-growth-tracking-blog.s3.amazonaws.com/blog/7c3159ba-f844-4aec-823d-4f793cee9157_14.png')
GO
INSERT [dbo].[Blog] ([BlogId], [Title], [Body], [BlogImageUrl]) VALUES (15, N'Lập Kế Hoạch Tài Chính Khi Mang Thai – Chi Tiêu Hợp Lý, An Tâm Đón Bé', N'Mang thai là một hành trình đầy hạnh phúc nhưng cũng đi kèm nhiều khoản chi phí không nhỏ. Việc lên kế hoạch tài chính sớm sẽ giúp mẹ bầu chủ động hơn trong chi tiêu, tránh áp lực kinh tế. Dưới đây là những gợi ý giúp mẹ quản lý tài chính hiệu quả khi mang thai.  

1. Dự Trù Chi Phí Khi Mang Thai  
Mỗi giai đoạn thai kỳ sẽ phát sinh những khoản chi tiêu khác nhau. Dưới đây là một số khoản quan trọng mẹ bầu cần chuẩn bị:  
- Khám thai định kỳ: Mỗi lần khám có thể tốn từ 200.000 – 1.000.000 VNĐ tùy bệnh viện và dịch vụ.  
- Xét nghiệm & siêu âm: Một số xét nghiệm quan trọng như Double Test, Triple Test, siêu âm 4D có thể lên đến 3 – 5 triệu VNĐ/lần.  
- Chi phí sinh con: Tùy vào hình thức sinh thường hay sinh mổ, chi phí có thể dao động từ 5 – 30 triệu VNĐ hoặc cao hơn nếu sinh ở bệnh viện quốc tế.  
- Đồ dùng cho mẹ & bé: Gồm quần áo bầu, đồ sơ sinh, sữa, bỉm, xe đẩy,… có thể lên đến 10 – 20 triệu VNĐ.  
- Dự phòng chi phí phát sinh: Trong thai kỳ, mẹ có thể cần nhập viện, bổ sung dinh dưỡng đặc biệt hoặc mua thuốc bổ.  

2. Lập Kế Hoạch Tài Chính Hợp Lý  
Để không bị quá tải về tài chính, mẹ bầu nên lập kế hoạch chi tiêu cụ thể theo từng tháng. Một số mẹo giúp mẹ quản lý tài chính hiệu quả:  
- Tiết kiệm từ sớm: Ngay khi có kế hoạch sinh con, mẹ nên dành ra một khoản tiết kiệm cố định mỗi tháng.  
- Chia nhỏ ngân sách: Phân chia chi phí thành từng nhóm: khám thai, đồ dùng, sinh nở,… để dễ kiểm soát.  
- Hạn chế mua sắm không cần thiết: Không nên mua quá nhiều đồ sơ sinh, chỉ chọn những món thực sự cần thiết.  
- Tận dụng bảo hiểm y tế & bảo hiểm thai sản: Giúp giảm chi phí khám và sinh nở đáng kể.  
- Lập quỹ dự phòng: Luôn có một khoản tiền để xử lý những tình huống bất ngờ như sinh non, biến chứng thai kỳ.  

3. Tiết Kiệm Hiệu Quả Khi Mang Thai  
Dưới đây là một số mẹo giúp mẹ bầu tiết kiệm tài chính mà vẫn đảm bảo đầy đủ cho mẹ và bé:  
- Tận dụng đồ có sẵn: Hỏi xin hoặc mua lại đồ sơ sinh từ người thân, bạn bè.  
- Mua hàng giảm giá, săn khuyến mãi: Chọn thời điểm sale để mua đồ với giá tốt.  
- Tự nấu ăn tại nhà: Giúp tiết kiệm chi phí ăn uống và đảm bảo dinh dưỡng tốt hơn.  
- So sánh giá trước khi mua: Kiểm tra nhiều nơi để chọn sản phẩm chất lượng với giá hợp lý nhất.  

4. Chuẩn Bị Tài Chính Dài Hạn Cho Bé  
Không chỉ trong thai kỳ, sau khi sinh con mẹ cũng cần một kế hoạch tài chính lâu dài:  
- Lập quỹ tiết kiệm cho bé: Để đảm bảo chi phí học tập, sức khỏe sau này.  
- Xem xét mua bảo hiểm cho bé: Bảo hiểm sức khỏe giúp giảm bớt gánh nặng tài chính khi bé ốm đau.  
- Dự trù chi phí chăm sóc con: Bao gồm tiền bỉm sữa, tiêm phòng, gửi trẻ,…  

Việc chuẩn bị tài chính khi mang thai là vô cùng quan trọng để đảm bảo mẹ bầu có một thai kỳ khỏe mạnh mà không gặp áp lực kinh tế. Hy vọng với những mẹo trên, mẹ sẽ có một kế hoạch chi tiêu thông minh, giúp cả gia đình an tâm chào đón thiên thần nhỏ!', N'https://pregnancy-growth-tracking-blog.s3.amazonaws.com/blog/c541da5f-3b11-4c14-a476-47e116b7f147_15.jpg')
GO
INSERT [dbo].[Blog] ([BlogId], [Title], [Body], [BlogImageUrl]) VALUES (16, N'Chuẩn Bị Sinh Con: Lập Kế Hoạch Để Mẹ Tròn Con Vuông', N'Ngày sinh bé yêu là một trong những khoảnh khắc quan trọng nhất trong cuộc đời mẹ bầu. Việc lập kế hoạch sinh chi tiết sẽ giúp mẹ tự tin, giảm lo lắng và sẵn sàng chào đón bé yêu một cách suôn sẻ nhất.  

1. Xác Định Ngày Dự Sinh  
Ngày dự sinh được tính dựa trên chu kỳ kinh nguyệt cuối cùng của mẹ hoặc thông qua siêu âm thai. Tuy nhiên, chỉ khoảng 5% mẹ bầu sinh đúng ngày dự kiến, nên mẹ cần chuẩn bị trước từ tuần thứ 36 để tránh bị động.  

Dấu hiệu chuyển dạ mẹ cần biết:  
- Bụng tụt xuống thấp.  
- Xuất hiện các cơn co thắt tử cung mạnh và đều đặn.  
- Ra dịch nhầy hoặc vỡ ối.  

2. Lựa Chọn Nơi Sinh  
Mẹ cần chọn nơi sinh phù hợp để đảm bảo an toàn cho cả mẹ và bé:  
- Sinh tại bệnh viện công: Chi phí thấp hơn, nhưng có thể đông đúc.  
- Sinh tại bệnh viện tư hoặc quốc tế: Dịch vụ tốt, phòng riêng, chi phí cao hơn.  

Lưu ý:  
- Mẹ nên đăng ký hồ sơ sinh từ tháng thứ 7 hoặc sớm hơn.  
- Tìm hiểu chi phí sinh thường & sinh mổ để chuẩn bị tài chính.  

3. Chuẩn Bị Đồ Dùng Đi Sinh  
Mẹ nên chuẩn bị giỏ đồ đi sinh từ tuần 36, bao gồm:  

Cho mẹ:  
- Quần áo thoải mái, bỉm cho mẹ, băng vệ sinh, dép.  
- Giấy tờ tùy thân, bảo hiểm y tế, hồ sơ khám thai.  

Cho bé:  
- Quần áo sơ sinh, mũ, bao tay, bao chân, tã.  
- Chăn ủ ấm, bình sữa, sữa công thức (nếu cần).  

4. Lựa Chọn Hình Thức Sinh  
Tùy theo sức khỏe của mẹ và bé, bác sĩ sẽ tư vấn hình thức sinh phù hợp:  
- Sinh thường: Hồi phục nhanh hơn, ít ảnh hưởng đến sức khỏe mẹ.  
- Sinh mổ: Dành cho trường hợp mẹ có chỉ định đặc biệt, thời gian hồi phục lâu hơn.  

Lưu ý: Nếu mẹ muốn sinh không đau, có thể tham khảo phương pháp gây tê màng cứng.  

5. Chuẩn Bị Tâm Lý Trước Khi Sinh  
Càng gần ngày sinh, mẹ càng dễ lo lắng. Một số cách giúp mẹ giữ tâm lý ổn định:  
- Tập thở và thực hành các bài tập thư giãn.  
- Trò chuyện với bác sĩ, người thân để cảm thấy yên tâm hơn.  
- Đọc sách về chăm sóc trẻ sơ sinh để chuẩn bị tâm lý làm mẹ.  

6. Kế Hoạch Chăm Sóc Sau Sinh  
Sau khi sinh, mẹ cần có kế hoạch nghỉ ngơi và chăm sóc bản thân:  
- Dinh dưỡng: Bổ sung thực phẩm giúp mẹ nhanh hồi phục và có nhiều sữa.  
- Hỗ trợ từ gia đình: Nhờ người thân giúp đỡ để mẹ có thời gian nghỉ ngơi.  
- Lên kế hoạch tiêm phòng cho bé: Đảm bảo bé được tiêm chủng đúng lịch.  

Việc lập kế hoạch sinh sẽ giúp mẹ bầu cảm thấy chủ động, giảm căng thẳng và chuẩn bị tốt nhất cho hành trình đón con yêu chào đời. Hãy bắt đầu chuẩn bị ngay từ hôm nay để có một cuộc vượt cạn an toàn và suôn sẻ!', N'https://pregnancy-growth-tracking-blog.s3.amazonaws.com/blog/5a894b1f-dbac-4625-b5f3-87de2ef0122c_16.jpg')
GO
INSERT [dbo].[Blog] ([BlogId], [Title], [Body], [BlogImageUrl]) VALUES (17, N'Bí Quyết Chăm Sóc Trẻ Sơ Sinh Giúp Bé Khỏe Mạnh, Phát Triển Tốt', N'Trẻ sơ sinh cần được chăm sóc đặc biệt trong những tuần đầu đời để đảm bảo sức khỏe và phát triển tốt. Dưới đây là những hướng dẫn quan trọng giúp bố mẹ chăm sóc bé đúng cách.  

1. Giữ Ấm Cho Trẻ  

- Trẻ sơ sinh chưa thể tự điều chỉnh thân nhiệt, do đó bố mẹ cần giữ ấm cho bé.  
- Nhiệt độ phòng nên duy trì khoảng 26 - 28 độ C.  
- Mặc quần áo phù hợp, không quá dày hoặc quá mỏng.  
- Đội mũ, đi tất tay, tất chân nếu thời tiết lạnh.  

2. Cho Bé Bú Đúng Cách  

- Cho bé bú mẹ hoàn toàn trong 6 tháng đầu để đảm bảo cung cấp đủ dinh dưỡng.  
- Nếu mẹ không đủ sữa, có thể bổ sung sữa công thức theo hướng dẫn của bác sĩ.  
- Bú theo nhu cầu của bé, trung bình 2 - 3 giờ một lần.  
- Sau khi bú, vỗ nhẹ lưng bé để giúp bé ợ hơi, tránh bị đầy bụng.  

3. Tắm Và Vệ Sinh Cho Bé  

- Tắm cho bé khoảng 3 - 4 lần mỗi tuần bằng nước ấm.  
- Dùng khăn mềm lau mặt, vùng cổ, nách và bẹn mỗi ngày.  
- Giữ vệ sinh rốn sạch sẽ, lau khô sau khi tắm để tránh nhiễm trùng.  
- Thay tã thường xuyên, lau sạch và giữ vùng da bé khô thoáng.  

4. Chăm Sóc Giấc Ngủ Của Bé  

- Trẻ sơ sinh ngủ khoảng 16 - 18 giờ mỗi ngày.  
- Đặt bé nằm ngửa khi ngủ để giảm nguy cơ đột tử.  
- Không để chăn, gối mềm hoặc đồ chơi trong nôi để tránh nguy cơ ngạt thở.  
- Khi bé thức giấc giữa đêm, mẹ nên dỗ nhẹ nhàng, tránh bật đèn quá sáng.  

5. Theo Dõi Sức Khỏe Của Bé  

- Đưa bé đi khám định kỳ theo lịch của bác sĩ.  
- Theo dõi cân nặng, chiều cao để đánh giá sự phát triển.  
- Nếu bé có dấu hiệu bất thường như sốt cao, bú ít, quấy khóc kéo dài, cần đưa bé đi khám ngay.  

Kết Luận  

Chăm sóc trẻ sơ sinh là một hành trình đầy yêu thương nhưng cũng nhiều thử thách. Bố mẹ cần nắm vững những kiến thức cơ bản để đảm bảo bé phát triển khỏe mạnh.', N'https://pregnancy-growth-tracking-blog.s3.amazonaws.com/blog/728889b3-71eb-49fd-82bf-ca70eaceabfd_17.jpg')
GO
INSERT [dbo].[Blog] ([BlogId], [Title], [Body], [BlogImageUrl]) VALUES (18, N'Sữa Mẹ – Nguồn Dinh Dưỡng Vàng: Hướng Dẫn Cách Cho Bé Bú Đúng', N'Sữa mẹ là nguồn dinh dưỡng tốt nhất cho trẻ sơ sinh, cung cấp đầy đủ dưỡng chất cần thiết giúp bé phát triển khỏe mạnh. Việc cho con bú không chỉ mang lại lợi ích cho bé mà còn giúp mẹ nhanh chóng phục hồi sau sinh. Dưới đây là những thông tin quan trọng về việc nuôi con bằng sữa mẹ.  

1. Lợi Ích Của Sữa Mẹ  

Đối với bé  
- Cung cấp đầy đủ dưỡng chất cần thiết trong 6 tháng đầu đời.  
- Chứa kháng thể giúp tăng cường hệ miễn dịch, bảo vệ bé khỏi nhiễm trùng.  
- Hỗ trợ hệ tiêu hóa non nớt của bé, giảm nguy cơ táo bón và tiêu chảy.  
- Giúp bé phát triển trí não nhờ DHA và các axit béo quan trọng.  

Đối với mẹ  
- Giúp tử cung co hồi nhanh hơn sau sinh.  
- Giảm nguy cơ mắc ung thư vú, ung thư buồng trứng.  
- Hỗ trợ giảm cân sau sinh nhờ đốt cháy calo.  
- Tạo sự gắn kết tình cảm giữa mẹ và bé.  

2. Cách Cho Bé Bú Đúng Cách  

- Bế bé đúng tư thế: Đặt bé nằm nghiêng, đầu và thân trên cùng một đường thẳng.  
- Ngậm bắt vú đúng: Miệng bé ngậm sâu vào quầng vú, không chỉ ngậm đầu ti.  
- Cho bú theo nhu cầu: Bé có thể bú từ 8 - 12 lần/ngày, không cần ép theo giờ cố định.  
- Đổi bên khi bú: Giúp bé nhận đủ sữa và kích thích sản xuất sữa mẹ.  
- Vỗ ợ hơi sau khi bú: Giúp bé tránh bị đầy hơi, nôn trớ.  

3. Một Số Vấn Đề Khi Cho Bé Bú Và Cách Xử Lý  

- Bé bú ít hoặc không chịu bú: Kiểm tra tư thế bú, đảm bảo bé không bị đầy hơi.  
- Núm vú bị đau, nứt: Thay đổi tư thế bú, bôi sữa mẹ lên đầu vú để làm dịu.  
- Ít sữa: Mẹ nên uống nhiều nước, ăn uống đầy đủ dinh dưỡng, ngủ đủ giấc.  
- Căng tức ngực: Massage nhẹ nhàng, chườm ấm để giảm khó chịu.  

4. Khi Nào Cần Bổ Sung Sữa Công Thức?  

- Mẹ gặp vấn đề sức khỏe và không thể cho con bú.  
- Bé không tăng cân đủ theo tiêu chuẩn.  
- Mẹ bị thiếu sữa trầm trọng dù đã thử nhiều cách kích sữa.  

Kết Luận  

Nuôi con bằng sữa mẹ mang lại nhiều lợi ích cho cả mẹ và bé. Để duy trì nguồn sữa dồi dào, mẹ cần ăn uống lành mạnh, nghỉ ngơi hợp lý và cho bé bú đúng cách. Nếu gặp khó khăn, mẹ có thể tham khảo ý kiến bác sĩ để tìm giải pháp phù hợp.', N'https://pregnancy-growth-tracking-blog.s3.amazonaws.com/blog/1b15f2d5-ce0b-41ac-9ffe-c2c0a9cce6fa_18.jpg')
GO
INSERT [dbo].[Blog] ([BlogId], [Title], [Body], [BlogImageUrl]) VALUES (19, N'Cách Kích Thích Trí Não Thai Nhi Phát Triển Toàn Diện', N'Việc kích thích trí não thai nhi ngay từ trong bụng mẹ giúp bé phát triển tốt hơn về nhận thức và tư duy sau khi chào đời. Nhiều nghiên cứu cho thấy, sự tương tác giữa mẹ và bé trong thai kỳ có thể giúp bé thông minh hơn. Dưới đây là những phương pháp hiệu quả để dạy thai nhi thông minh ngay từ khi còn trong bụng mẹ.  

1. Chế Độ Dinh Dưỡng Hỗ Trợ Phát Triển Não Bộ Thai Nhi  

Dinh dưỡng đóng vai trò quan trọng trong sự phát triển trí não của bé. Mẹ bầu nên bổ sung:  

- Axit béo Omega-3 (DHA, EPA): Có nhiều trong cá hồi, hạt chia, hạt óc chó giúp phát triển não bộ thai nhi.  
- Choline: Hỗ trợ trí nhớ và tăng khả năng học hỏi sau này, có trong trứng, sữa và các loại đậu.  
- Sắt và axit folic: Giúp hình thành tế bào não, giảm nguy cơ dị tật ống thần kinh.  
- Protein: Cần thiết cho sự phát triển toàn diện của não bộ, có trong thịt, cá, trứng, sữa.  

2. Nói Chuyện Và Đọc Sách Cho Bé Nghe  

Thai nhi có thể nghe âm thanh từ khoảng tuần thứ 16 - 20 của thai kỳ. Việc mẹ thường xuyên trò chuyện, hát ru hoặc đọc sách giúp bé làm quen với giọng nói của mẹ và kích thích trí não phát triển.  

3. Cho Bé Nghe Nhạc  

Âm nhạc có tác động tích cực đến sự phát triển trí não của thai nhi.  

- Chọn những bản nhạc nhẹ nhàng, nhạc cổ điển như Mozart, Beethoven.  
- Không nên mở nhạc quá to, âm lượng phù hợp là dưới 65 dB.  
- Nghe nhạc vào những thời điểm mẹ thư giãn, như trước khi đi ngủ hoặc lúc nghỉ trưa.  

4. Massage Bụng Bầu – Gắn Kết Cảm Xúc Với Bé  

Massage nhẹ nhàng giúp bé cảm nhận được sự quan tâm từ mẹ, đồng thời kích thích phản xạ thần kinh.  

5. Tập Thể Dục Nhẹ Nhàng  

Vận động nhẹ nhàng giúp tăng cường lưu thông máu, cung cấp oxy cho thai nhi và giúp bé phát triển tốt hơn.  

6. Kiểm Soát Căng Thẳng  

Căng thẳng kéo dài có thể ảnh hưởng đến sự phát triển trí não của thai nhi.  

Kết Luận  

Dạy thai nhi thông minh không chỉ là việc áp dụng các phương pháp kích thích trí não mà còn là tạo môi trường tốt nhất cho sự phát triển của bé. Chế độ dinh dưỡng, tương tác qua lời nói, âm nhạc, vận động và tâm lý của mẹ đều góp phần quan trọng trong việc nuôi dưỡng một em bé khỏe mạnh và thông minh.', N'https://pregnancy-growth-tracking-blog.s3.amazonaws.com/blog/cf83b2e2-377d-47d3-9d8b-ebd7ac445fea_19.webp')
GO
INSERT [dbo].[Blog] ([BlogId], [Title], [Body], [BlogImageUrl]) VALUES (20, N'Những Dấu Hiệu Chuyển Dạ Mẹ Bầu Cần Biết', N'Chuyển dạ là một giai đoạn quan trọng đánh dấu việc em bé sắp chào đời. Tuy nhiên, không phải mẹ bầu nào cũng nhận biết rõ các dấu hiệu chuyển dạ thật sự. Việc hiểu rõ những tín hiệu cơ thể sẽ giúp mẹ chuẩn bị tốt hơn và tránh những tình huống bất ngờ.  

1. Bụng Bầu Hạ Thấp Xuống  

Trong những tuần cuối thai kỳ, thai nhi sẽ dịch chuyển dần xuống phía dưới khung chậu để chuẩn bị ra đời. Điều này khiến mẹ có cảm giác bụng bầu tụt xuống, giúp mẹ dễ thở hơn nhưng đồng thời cũng gây áp lực lên bàng quang, làm mẹ đi tiểu nhiều hơn.  

2. Xuất Hiện Cơn Gò Tử Cung  

Cơn gò tử cung là dấu hiệu quan trọng nhất của quá trình chuyển dạ. Nếu mẹ cảm thấy cơn gò trở nên mạnh hơn, đau nhiều hơn và xuất hiện theo chu kỳ từ 5 - 10 phút một lần, đó là dấu hiệu mẹ sắp sinh và cần đến bệnh viện ngay.  

3. Ra Dịch Nhầy Hồng Hoặc Có Máu  

Trước khi sinh, cổ tử cung sẽ giãn nở để chuẩn bị cho em bé ra ngoài, làm bong nút nhầy cổ tử cung. Lúc này, mẹ có thể thấy dịch nhầy màu hồng hoặc lẫn một ít máu.  

4. Vỡ Ối  

Vỡ ối là dấu hiệu rõ ràng nhất cho thấy mẹ sắp sinh. Khi nước ối vỡ, mẹ nên đến bệnh viện ngay để tránh nguy cơ nhiễm trùng cho em bé.  

5. Đau Lưng Dữ Dội  

Những cơn đau lưng kéo dài, đặc biệt là ở vùng thắt lưng, có thể là dấu hiệu tử cung đang co bóp mạnh để chuẩn bị sinh.  

6. Tiêu Chảy Hoặc Buồn Nôn  

Một số mẹ bầu có thể gặp tình trạng tiêu chảy hoặc buồn nôn ngay trước khi chuyển dạ. Đây là cách cơ thể tự làm sạch để chuẩn bị cho quá trình sinh nở.  

7. Cổ Tử Cung Mở  

Trong các lần khám thai cuối cùng, bác sĩ sẽ kiểm tra độ mở của cổ tử cung. Khi cổ tử cung mở từ 3 - 4cm và có cơn gò đều đặn, mẹ đã sẵn sàng để sinh.  

Khi Nào Mẹ Nên Đến Bệnh Viện?  

- Cơn gò tử cung xuất hiện đều đặn, khoảng 5 - 10 phút một lần.  
- Vỡ ối, đặc biệt là nếu nước ối có màu xanh đục hoặc vàng nâu.  
- Ra máu nhiều hoặc dịch nhầy có màu đỏ tươi.  
- Đau lưng dữ dội hoặc cảm giác thai máy yếu hơn bình thường.  

Kết Luận  

Nhận biết đúng các dấu hiệu chuyển dạ sẽ giúp mẹ có sự chuẩn bị tốt nhất cho quá trình sinh nở. Nếu có bất kỳ dấu hiệu nào bất thường hoặc không chắc chắn, mẹ hãy liên hệ ngay với bác sĩ để được tư vấn và hỗ trợ kịp thời.', N'https://pregnancy-growth-tracking-blog.s3.amazonaws.com/blog/79d34fba-3d7d-4bc9-9ea3-b363f401ab00_20.webp')
GO
SET IDENTITY_INSERT [dbo].[Blog] OFF
GO
SET IDENTITY_INSERT [dbo].[BlogCate] ON 
GO
INSERT [dbo].[BlogCate] ([BlogCateId], [BlogId], [CategoryId]) VALUES (1, 1, 1)
GO
INSERT [dbo].[BlogCate] ([BlogCateId], [BlogId], [CategoryId]) VALUES (3, 2, 2)
GO
INSERT [dbo].[BlogCate] ([BlogCateId], [BlogId], [CategoryId]) VALUES (4, 3, 3)
GO
INSERT [dbo].[BlogCate] ([BlogCateId], [BlogId], [CategoryId]) VALUES (7, 4, 4)
GO
INSERT [dbo].[BlogCate] ([BlogCateId], [BlogId], [CategoryId]) VALUES (8, 5, 5)
GO
INSERT [dbo].[BlogCate] ([BlogCateId], [BlogId], [CategoryId]) VALUES (9, 6, 6)
GO
INSERT [dbo].[BlogCate] ([BlogCateId], [BlogId], [CategoryId]) VALUES (10, 7, 7)
GO
INSERT [dbo].[BlogCate] ([BlogCateId], [BlogId], [CategoryId]) VALUES (11, 8, 8)
GO
INSERT [dbo].[BlogCate] ([BlogCateId], [BlogId], [CategoryId]) VALUES (12, 9, 9)
GO
INSERT [dbo].[BlogCate] ([BlogCateId], [BlogId], [CategoryId]) VALUES (13, 10, 10)
GO
INSERT [dbo].[BlogCate] ([BlogCateId], [BlogId], [CategoryId]) VALUES (14, 11, 11)
GO
INSERT [dbo].[BlogCate] ([BlogCateId], [BlogId], [CategoryId]) VALUES (15, 12, 12)
GO
INSERT [dbo].[BlogCate] ([BlogCateId], [BlogId], [CategoryId]) VALUES (16, 13, 13)
GO
INSERT [dbo].[BlogCate] ([BlogCateId], [BlogId], [CategoryId]) VALUES (17, 14, 14)
GO
INSERT [dbo].[BlogCate] ([BlogCateId], [BlogId], [CategoryId]) VALUES (18, 15, 15)
GO
INSERT [dbo].[BlogCate] ([BlogCateId], [BlogId], [CategoryId]) VALUES (19, 16, 16)
GO
INSERT [dbo].[BlogCate] ([BlogCateId], [BlogId], [CategoryId]) VALUES (20, 17, 17)
GO
INSERT [dbo].[BlogCate] ([BlogCateId], [BlogId], [CategoryId]) VALUES (21, 18, 18)
GO
INSERT [dbo].[BlogCate] ([BlogCateId], [BlogId], [CategoryId]) VALUES (22, 19, 19)
GO
INSERT [dbo].[BlogCate] ([BlogCateId], [BlogId], [CategoryId]) VALUES (23, 20, 20)
GO
SET IDENTITY_INSERT [dbo].[BlogCate] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (1, N'Sức khỏe thai kỳ')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (2, N'Dinh dưỡng mẹ bầu')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (3, N'Phát triển thai nhi')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (4, N'Triệu chứng thai kỳ')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (5, N'Xét nghiệm & siêu âm')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (6, N'Tập thể dục bầu')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (7, N'Chuẩn bị đồ sơ sinh')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (8, N'Sinh thường & sinh mổ')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (9, N'Lịch khám thai')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (10, N'Bệnh lý thai kỳ')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (11, N'Giảm căng thẳng')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (12, N'Tâm lý mẹ bầu')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (13, N'Đặt tên cho bé')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (14, N'Làm đẹp khi mang thai')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (15, N'Chuẩn bị tài chính')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (16, N'Lập kế hoạch sinh')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (17, N'Chăm sóc trẻ sơ sinh')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (18, N'Nuôi con bằng sữa mẹ')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (19, N'Dạy thai nhi thông minh')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (20, N'Dấu hiệu chuyển dạ')
GO
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[CommentLike] ON 
GO
INSERT [dbo].[CommentLike] ([CommentLikeId], [CommentId], [UserId]) VALUES (4, 2, 3)
GO
SET IDENTITY_INSERT [dbo].[CommentLike] OFF
GO
SET IDENTITY_INSERT [dbo].[Foetus] ON 
GO
INSERT [dbo].[Foetus] ([FoetusId], [UserId], [Name], [GestationalAge], [ExpectedBirthDate], [Gender]) VALUES (4, 4, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Foetus] ([FoetusId], [UserId], [Name], [GestationalAge], [ExpectedBirthDate], [Gender]) VALUES (5, 4, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Foetus] ([FoetusId], [UserId], [Name], [GestationalAge], [ExpectedBirthDate], [Gender]) VALUES (6, 5, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Foetus] ([FoetusId], [UserId], [Name], [GestationalAge], [ExpectedBirthDate], [Gender]) VALUES (7, 6, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Foetus] ([FoetusId], [UserId], [Name], [GestationalAge], [ExpectedBirthDate], [Gender]) VALUES (8, 6, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Foetus] ([FoetusId], [UserId], [Name], [GestationalAge], [ExpectedBirthDate], [Gender]) VALUES (11, 2, N'Nguyễn Văn A', 12, CAST(N'2025-03-18' AS Date), N'Nam')
GO
INSERT [dbo].[Foetus] ([FoetusId], [UserId], [Name], [GestationalAge], [ExpectedBirthDate], [Gender]) VALUES (12, 18, N'Nguyễn Văn Tester', 24, CAST(N'2025-09-30' AS Date), N'Nam')
GO
INSERT [dbo].[Foetus] ([FoetusId], [UserId], [Name], [GestationalAge], [ExpectedBirthDate], [Gender]) VALUES (13, 18, N'Nguyễn Thị DevOps', 0, NULL, N'Nữ')
GO
INSERT [dbo].[Foetus] ([FoetusId], [UserId], [Name], [GestationalAge], [ExpectedBirthDate], [Gender]) VALUES (14, 17, N'Phương Nghi', 39, CAST(N'2025-09-30' AS Date), N'Nam')
GO
INSERT [dbo].[Foetus] ([FoetusId], [UserId], [Name], [GestationalAge], [ExpectedBirthDate], [Gender]) VALUES (15, 19, N'Lộc Ngu', 34, CAST(N'2025-09-30' AS Date), N'Nam')
GO
INSERT [dbo].[Foetus] ([FoetusId], [UserId], [Name], [GestationalAge], [ExpectedBirthDate], [Gender]) VALUES (17, 17, N'Gia Khang', 19, CAST(N'2025-09-24' AS Date), N'Nam')
GO
INSERT [dbo].[Foetus] ([FoetusId], [UserId], [Name], [GestationalAge], [ExpectedBirthDate], [Gender]) VALUES (21, 14, N'Trần Văn Hô', 0, NULL, N'Nam')
GO
INSERT [dbo].[Foetus] ([FoetusId], [UserId], [Name], [GestationalAge], [ExpectedBirthDate], [Gender]) VALUES (22, 21, N'Nhi', 16, CAST(N'2025-10-01' AS Date), N'Nữ')
GO
INSERT [dbo].[Foetus] ([FoetusId], [UserId], [Name], [GestationalAge], [ExpectedBirthDate], [Gender]) VALUES (23, 3, N'Khang con', 24, CAST(N'2025-07-17' AS Date), N'Nam')
GO
INSERT [dbo].[Foetus] ([FoetusId], [UserId], [Name], [GestationalAge], [ExpectedBirthDate], [Gender]) VALUES (26, 29, N'Sam', 0, NULL, N'Nam')
GO
INSERT [dbo].[Foetus] ([FoetusId], [UserId], [Name], [GestationalAge], [ExpectedBirthDate], [Gender]) VALUES (27, 27, N'Minh', 0, NULL, N'Nam')
GO
INSERT [dbo].[Foetus] ([FoetusId], [UserId], [Name], [GestationalAge], [ExpectedBirthDate], [Gender]) VALUES (31, 22, N'hihi', 14, CAST(N'2025-10-08' AS Date), N'Nữ')
GO
INSERT [dbo].[Foetus] ([FoetusId], [UserId], [Name], [GestationalAge], [ExpectedBirthDate], [Gender]) VALUES (35, 50, N'Nhi', 15, CAST(N'2025-10-12' AS Date), N'Nữ')
GO
SET IDENTITY_INSERT [dbo].[Foetus] OFF
GO
SET IDENTITY_INSERT [dbo].[GrowthData] ON 
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (34, 11, CAST(N'2025-03-18' AS Date), 29, 40, 350, 33333, 33333, 3332)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (35, 11, CAST(N'2025-03-18' AS Date), 9, 20, 234, 234, 234, 234)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (44, 14, CAST(N'2025-03-29' AS Date), 1, 12, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (45, 15, CAST(N'2025-03-18' AS Date), 1, 12, 123, 123, 123, 123)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (46, 15, CAST(N'2025-03-18' AS Date), 2, 13, 132, 123, 123, 123)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (47, 15, CAST(N'2025-03-18' AS Date), 4, 15, 123, 123, 123, 23123)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (48, 15, CAST(N'2025-03-18' AS Date), 23, 34, 3124, 23423, 4234234, 423423)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (50, 14, CAST(N'2025-03-29' AS Date), 5, 16, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (63, 14, CAST(N'2025-03-29' AS Date), 2, 13, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (64, 14, CAST(N'2025-03-29' AS Date), 8, 19, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (65, 17, CAST(N'2025-03-19' AS Date), 2, 13, 123, 123, 123, 122)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (66, 11, CAST(N'2025-03-19' AS Date), 1, 12, 213, 123, 123, 123)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (67, 22, CAST(N'2025-03-19' AS Date), 1, 12, 70, 80, 90, 100)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (68, 22, CAST(N'2025-03-19' AS Date), 2, 13, 70, 90, 100, 90)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (69, 22, CAST(N'2025-03-19' AS Date), 3, 14, 65, 70, 90, 65)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (70, 22, CAST(N'2025-03-19' AS Date), 4, 15, 90, 80, 70, 50)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (71, 14, CAST(N'2025-03-29' AS Date), 4, 15, 122, 93, 18, 69)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (72, 14, CAST(N'2025-03-29' AS Date), 9, 20, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (73, 14, CAST(N'2025-03-29' AS Date), 10, 21, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (74, 14, CAST(N'2025-03-29' AS Date), 11, 22, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (75, 14, CAST(N'2025-03-29' AS Date), 3, 14, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (76, 14, CAST(N'2025-03-29' AS Date), 6, 17, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (77, 14, CAST(N'2025-03-31' AS Date), 7, 18, 150, 129, 27, 190)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (78, 23, CAST(N'2025-03-20' AS Date), 12, 23, 1, 321, 2, 555)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (79, 23, CAST(N'2025-03-20' AS Date), 13, 24, 21, 222, 454, 657)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (80, 14, CAST(N'2025-03-29' AS Date), 21, 32, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (81, 22, CAST(N'2025-03-21' AS Date), 5, 16, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (82, 17, CAST(N'2025-03-25' AS Date), 4, 15, 111, 93, 18, 70)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (87, 17, CAST(N'2025-03-25' AS Date), 8, 19, 84, 69, 11, 23)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (88, 14, CAST(N'2025-03-29' AS Date), 12, 23, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (89, 31, CAST(N'2025-03-26' AS Date), 1, 12, 60, 55, 9, 14)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (90, 31, CAST(N'2025-03-26' AS Date), 1, 12, 60, 55, 9, 14)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (91, 12, CAST(N'2018-08-28' AS Date), 1, 12, 72, 55, 9, 13)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (92, 12, CAST(N'2018-09-04' AS Date), 2, 13, 86, 70, 10, 25)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (93, 12, CAST(N'2018-09-11' AS Date), 3, 14, 97, 79, 16, 40)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (94, 12, CAST(N'2018-09-18' AS Date), 4, 15, 110, 95, 19, 75)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (95, 12, CAST(N'2018-09-25' AS Date), 5, 16, 126, 107, 22, 98)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (96, 12, CAST(N'2018-10-02' AS Date), 6, 17, 136, 116, 26, 145)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (97, 12, CAST(N'2018-10-09' AS Date), 7, 18, 152, 130, 28, 195)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (98, 12, CAST(N'2018-10-16' AS Date), 8, 19, 160, 140, 32, 250)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (99, 12, CAST(N'2018-10-23' AS Date), 9, 20, 177, 155, 35, 310)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (100, 12, CAST(N'2018-10-30' AS Date), 10, 21, 190, 165, 37, 370)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (101, 12, CAST(N'2018-11-06' AS Date), 11, 22, 200, 178, 40, 420)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (102, 12, CAST(N'2018-11-13' AS Date), 12, 23, 215, 188, 43, 510)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (103, 12, CAST(N'2018-11-20' AS Date), 13, 24, 224, 200, 45, 605)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (104, 12, CAST(N'2018-11-27' AS Date), 14, 25, 230, 207, 48, 670)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (105, 12, CAST(N'2018-12-04' AS Date), 15, 26, 244, 218, 50, 780)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (106, 12, CAST(N'2018-12-11' AS Date), 16, 27, 250, 231, 53, 890)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (107, 12, CAST(N'2018-12-18' AS Date), 17, 28, 265, 238, 55, 1020)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (108, 12, CAST(N'2018-12-25' AS Date), 18, 29, 270, 255, 58, 1170)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (109, 12, CAST(N'2019-01-01' AS Date), 19, 30, 285, 262, 60, 1330)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (110, 12, CAST(N'2019-01-08' AS Date), 20, 31, 292, 272, 62, 1510)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (111, 12, CAST(N'2019-01-15' AS Date), 21, 32, 300, 278, 64, 1720)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (112, 12, CAST(N'2019-01-22' AS Date), 22, 33, 308, 285, 66, 1930)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (113, 12, CAST(N'2019-01-29' AS Date), 23, 34, 312, 295, 68, 2160)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (114, 12, CAST(N'2019-02-05' AS Date), 24, 35, 320, 310, 69, 2400)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (115, 12, CAST(N'2019-02-12' AS Date), 25, 36, 328, 322, 71, 2650)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (116, 12, CAST(N'2019-02-19' AS Date), 26, 37, 332, 325, 73, 2890)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (117, 12, CAST(N'2019-02-26' AS Date), 27, 38, 338, 338, 74, 3110)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (118, 12, CAST(N'2019-03-05' AS Date), 28, 39, 342, 350, 76, 3300)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (119, 12, CAST(N'2025-03-26' AS Date), 29, 40, 348, 355, 77, 3470)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (124, 31, CAST(N'2025-03-27' AS Date), 2, 13, 95, 77, 13, 25)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (125, 14, CAST(N'2025-03-29' AS Date), 14, 25, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (126, 31, CAST(N'2025-03-27' AS Date), 3, 14, 120, 100, 20, 50)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (127, 14, CAST(N'2025-03-29' AS Date), 22, 33, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (128, 14, CAST(N'2025-03-29' AS Date), 29, 40, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (129, 14, CAST(N'2025-03-29' AS Date), 24, 35, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (130, 14, CAST(N'2025-03-29' AS Date), 27, 38, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (131, 14, CAST(N'2025-03-29' AS Date), 17, 28, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (132, 14, CAST(N'2025-03-29' AS Date), 19, 30, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (133, 14, CAST(N'2025-03-29' AS Date), 13, 24, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (134, 14, CAST(N'2025-03-29' AS Date), 15, 26, 300, 200, 60, 900)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (135, 14, CAST(N'2025-03-29' AS Date), 16, 27, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (136, 14, CAST(N'2025-03-29' AS Date), 18, 29, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (137, 14, CAST(N'2025-03-29' AS Date), 20, 31, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (138, 14, CAST(N'2025-03-29' AS Date), 23, 34, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (139, 14, CAST(N'2025-03-29' AS Date), 25, 36, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (140, 14, CAST(N'2025-03-29' AS Date), 26, 37, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (141, 14, CAST(N'2025-03-29' AS Date), 28, 39, 0, 0, 0, 0)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (142, 35, CAST(N'2025-03-30' AS Date), 1, 12, 90, 70, 10, 14)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (143, 35, CAST(N'2025-03-31' AS Date), 2, 13, 84, 69, 11, 23)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (144, 35, CAST(N'2025-03-31' AS Date), 3, 14, 98, 81, 15, 42)
GO
INSERT [dbo].[GrowthData] ([GrowthDataId], [FoetusId], [Date], [GrowthStandardId], [Age], [HC], [AC], [FL], [EFW]) VALUES (145, 35, CAST(N'2025-03-31' AS Date), 4, 15, 0, 0, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[GrowthData] OFF
GO
SET IDENTITY_INSERT [dbo].[GrowthStandard] ON 
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (1, 12, 70, 56, 8, 14)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (2, 13, 84, 69, 11, 23)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (3, 14, 98, 81, 15, 42)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (4, 15, 111, 93, 18, 70)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (5, 16, 124, 105, 21, 100)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (6, 17, 137, 117, 24, 140)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (7, 18, 150, 129, 27, 190)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (8, 19, 162, 141, 30, 240)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (9, 20, 175, 152, 33, 300)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (10, 21, 187, 164, 36, 360)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (11, 22, 198, 175, 39, 430)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (12, 23, 210, 186, 42, 501)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (13, 24, 221, 197, 44, 600)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (14, 25, 232, 208, 47, 660)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (15, 26, 242, 219, 49, 760)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (16, 27, 252, 229, 52, 875)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (17, 28, 262, 240, 54, 1005)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (18, 29, 271, 250, 56, 1153)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (19, 30, 280, 260, 59, 1319)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (20, 31, 288, 270, 61, 1502)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (21, 32, 296, 280, 63, 1702)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (22, 33, 304, 290, 65, 1918)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (23, 34, 311, 299, 67, 2146)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (24, 35, 318, 309, 68, 2383)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (25, 36, 324, 318, 70, 2622)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (26, 37, 330, 327, 72, 2859)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (27, 38, 335, 336, 73, 3083)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (28, 39, 340, 345, 75, 3288)
GO
INSERT [dbo].[GrowthStandard] ([GrowthStandardId], [GestationalAge], [HC_Median], [AC_Median], [FL_Median], [EFW_Median]) VALUES (29, 40, 344, 354, 76, 3462)
GO
SET IDENTITY_INSERT [dbo].[GrowthStandard] OFF
GO
SET IDENTITY_INSERT [dbo].[Membership] ON 
GO
INSERT [dbo].[Membership] ([MembershipId], [Description], [Price]) VALUES (1, N'Free', 0)
GO
INSERT [dbo].[Membership] ([MembershipId], [Description], [Price]) VALUES (2, N'Pay', 9.99)
GO
SET IDENTITY_INSERT [dbo].[Membership] OFF
GO
SET IDENTITY_INSERT [dbo].[Payment] ON 
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (1, 7, 2, CAST(N'2025-03-18' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (2, 17, 2, CAST(N'2025-03-18' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (3, 18, 2, CAST(N'2025-03-18' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (4, 18, 2, CAST(N'2025-03-18' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (5, 19, 2, CAST(N'2025-03-18' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (6, 15, 2, CAST(N'2025-03-18' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (7, 8, 2, CAST(N'2025-03-18' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (8, 20, 2, CAST(N'2025-03-18' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (9, 10, 2, CAST(N'2025-03-18' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (10, 11, 2, CAST(N'2025-03-18' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (11, 2, 2, CAST(N'2025-01-01' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (12, 3, 2, CAST(N'2025-01-05' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (13, 4, 2, CAST(N'2025-01-10' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (14, 5, 2, CAST(N'2025-01-15' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (15, 6, 2, CAST(N'2025-01-20' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (16, 7, 2, CAST(N'2025-01-25' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (17, 8, 2, CAST(N'2025-01-30' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (18, 9, 2, CAST(N'2025-02-04' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (19, 10, 2, CAST(N'2025-02-09' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (20, 11, 2, CAST(N'2025-02-14' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (21, 12, 2, CAST(N'2025-02-19' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (22, 13, 2, CAST(N'2025-02-24' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (23, 14, 2, CAST(N'2025-02-28' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (24, 15, 2, CAST(N'2025-03-04' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (25, 16, 2, CAST(N'2025-03-09' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (26, 17, 2, CAST(N'2025-03-14' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (27, 18, 2, CAST(N'2025-03-19' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (28, 19, 2, CAST(N'2025-03-23' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (29, 20, 2, CAST(N'2025-01-02' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (30, 21, 2, CAST(N'2025-01-06' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (31, 22, 2, CAST(N'2025-01-08' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (32, 23, 2, CAST(N'2025-01-12' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (33, 24, 2, CAST(N'2025-01-18' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (34, 25, 2, CAST(N'2025-01-22' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (35, 26, 2, CAST(N'2025-01-28' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (36, 27, 2, CAST(N'2025-02-02' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (37, 28, 2, CAST(N'2025-02-06' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (38, 29, 2, CAST(N'2025-02-11' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (39, 2, 2, CAST(N'2025-02-16' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (40, 3, 2, CAST(N'2025-02-21' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (41, 4, 2, CAST(N'2025-02-26' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (42, 5, 2, CAST(N'2025-03-01' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (43, 6, 2, CAST(N'2025-03-06' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (44, 7, 2, CAST(N'2025-03-11' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (45, 8, 2, CAST(N'2025-03-16' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (46, 9, 2, CAST(N'2025-03-20' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (47, 10, 2, CAST(N'2025-03-22' AS Date), 244755)
GO
INSERT [dbo].[Payment] ([PaymentId], [UserId], [MembershipId], [Date], [TotalPrice]) VALUES (48, 11, 2, CAST(N'2025-03-23' AS Date), 244755)
GO
SET IDENTITY_INSERT [dbo].[Payment] OFF
GO
SET IDENTITY_INSERT [dbo].[Post] ON 
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (1, 1, N'test', N'test', CAST(N'2025-03-20T08:02:04.343' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post/a5eebe48-c67d-4659-b092-d188827f4e4f_HC05.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (2, 3, N'string', N'string', CAST(N'2025-03-20T15:18:36.023' AS DateTime), 0, NULL)
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (3, 3, N'Khangeiu', N'co con chim nho nhat', CAST(N'2025-03-20T15:40:27.293' AS DateTime), 0, NULL)
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (4, 1, N'helooo', N'hello baby cua a', CAST(N'2025-03-20T19:26:42.133' AS DateTime), 0, NULL)
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (43, 3, N'helloo', N'aloo', CAST(N'2025-03-21T01:48:28.907' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/a8becc71-6127-44cf-8081-9e4364e75db2_concho.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (44, 1, N'khang', N'heloo minh la khag dep trai ne', CAST(N'2025-03-21T02:10:17.233' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/a60ae702-9263-4a4c-8b37-04eeb0d893d7_hehe.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (45, 1, N'đâsdas', N'đâsdas', CAST(N'2025-03-21T02:15:41.840' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/50883d7d-9262-4c4a-bf55-a31f83f8744b_concho.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (46, 1, N'khangchupaka', N'đâsdasdasdasdasdas', CAST(N'2025-03-21T02:19:44.493' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/7a951918-d2cb-466c-b644-cfcf213b924e_concho.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (47, 1, N'Đây là Khang', N'Khang đang cười', CAST(N'2025-03-21T02:24:30.913' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/e2e39411-8f14-47e4-bc46-f2d795dda232_concho.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (48, 1, N'đây là khang', N'khang đang cười', CAST(N'2025-03-21T02:29:21.153' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/dbc72a7c-da49-45b3-96f4-180e277b5521_concho.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (49, 3, N'helloo', N'aloo', CAST(N'2025-03-21T02:29:49.017' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/bad99626-f36e-4934-a12d-edf9a719ed69_concho.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (50, 3, N'helloodd', N'aloosss', CAST(N'2025-03-21T02:30:19.737' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/88522d32-02aa-4c54-b6db-d293b5ff9ddd_concho.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (57, 3, N'đâs', N'đâs', CAST(N'2025-03-21T03:28:30.023' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/76d93d27-e50b-4f4c-a29a-98904a7ab74b_hehe.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (58, 18, N'Làm ơn để tao ngủ', N'Làm ơn để tao ngủ', CAST(N'2025-03-21T03:30:46.633' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/e3bec999-86c4-4a73-adfe-60c8fa314738_18. The Moon.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (60, 21, N'ne', N'ne', CAST(N'2025-03-21T03:32:48.420' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/d785fa9a-3c75-4296-8fb7-be99abf77931_227.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (64, 21, N'Kinh nghiệm khám thai lần đầu – Hồi hộp, lo lắng và hạnh phúc', N'Kinh nghiệm khám thai lần đầu – Hồi hộp, lo lắng và hạnh phúc', CAST(N'2025-03-21T03:37:40.287' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/ac12e8eb-10d4-4559-90af-200f0285d948_227.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (68, 21, N'Kinh nghiệm khám thai lần đầu – Hồi hộp, lo lắng và hạnh phúc', N'Lần đầu tiên biết mình mang thai, cảm xúc của mình là một sự pha trộn kỳ lạ giữa hạnh phúc, lo lắng và một chút bối rối. Hai vạch hiện lên trên que thử làm tim mình như ngừng đập.', CAST(N'2025-03-21T03:44:04.330' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/243acffd-0436-4f01-8360-629b62d5e1a6_227.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (69, 21, N'aa', N'aa', CAST(N'2025-03-21T03:49:32.610' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/f6d200fc-da2c-43cb-9c87-1212703d51ca_227.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (71, 1, N'heloo', N'dsadasdasdasdas', CAST(N'2025-03-21T03:56:04.203' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/3edecdd2-05d5-4d69-89d6-1b4d9a3106cb_Logo bau-01.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (73, 18, N'Vợ tôi', N'Shirakami Fubuki', CAST(N'2025-03-21T06:18:07.947' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/daf2ef86-567e-4a72-bc9d-29387046dfb5_2. The High Priestess.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (74, 18, N'Vợ tôi', N'Shirakami Fubuki', CAST(N'2025-03-21T06:21:50.360' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/e6e44db8-e513-40f2-b69e-f46918db99de_2. The High Priestess.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (75, 18, N'Vợ tôi', N'Shishiro Botan', CAST(N'2025-03-21T06:48:55.710' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/16b7f6c8-dc90-4d15-9388-14401a5d49e7_7. The Chariot.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (76, 27, N'New', N'New', CAST(N'2025-03-21T14:53:19.680' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/30216eff-56cf-432a-8e43-e74d5392336f_FPT.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (79, 29, N'Hành trình 9 tháng an tâm của mẹ Mai', N'Mai là một bà mẹ trẻ đang mang thai đứa con đầu lòng.', CAST(N'2025-03-22T01:33:43.877' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/cebc1b2c-a2f3-4b7c-af0c-9361665d3151_Logo bau-02 (2).png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (80, 18, N'Vợ tôi', N'Ookami Mio', CAST(N'2025-03-22T10:50:38.410' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/37f6a2c9-83cb-46b5-a0a6-971245a3a39e_5. The Heierophant.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (81, 2, N'Dinh dưỡng mẹ bầu', N'Con tôi trong bụng 5 tháng đã biết đạp', CAST(N'2025-03-23T11:52:35.033' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/ff413648-62f0-4ce1-9f3c-23f33506e3b2_images.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (82, 2, N'Dinh dưỡng mẹ bầu', N'Con tôi trong bụng 5 tháng đã biết đạp', CAST(N'2025-03-23T12:43:01.463' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/0bee8199-2744-4be3-a797-3dcd435bf3f2_walking_17492446.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (83, 2, N'Dinh dưỡng mẹ bầu', N'Con tôi trong bụng 5 tháng đã biết đạp', CAST(N'2025-03-23T12:48:04.340' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/f20c96d9-32cc-438f-952a-dfbefa338fdf_walking_17492446.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (84, 2, N'Dinh dưỡng mẹ bầu', N'Con tôi trong bụng 5 tháng đã biết đạp', CAST(N'2025-03-23T12:49:42.600' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/b2cd0650-af5d-4516-b24c-4691c5712627_walking_17492446.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (85, 2, N'Dinh dưỡng mẹ bầu', N'Con tôi trong bụng 5 tháng đã biết đạp', CAST(N'2025-03-23T12:51:55.407' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/0a01acf3-b436-4c9c-a93c-70e74be5974b_walking_17492446.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (86, 2, N'Dinh dưỡng mẹ bầu', N'Con tôi trong bụng 5 tháng đã biết đạp', CAST(N'2025-03-23T12:57:27.130' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/e9a859d2-ccc7-4755-a98b-16fe20821234_walking_17492446.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (87, 2, N'Chăm sóc thai kỳ', N'Hôm nay tôi đi khám thai, bé rất khỏe mạnh', CAST(N'2025-03-23T12:57:41.677' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/fb8a9ad4-9770-4359-ae87-60943366d972_walking_17492446.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (88, 2, N'Dinh dưỡng mẹ bầu', N'Con tôi trong bụng 5 tháng đã biết đạp', CAST(N'2025-03-23T14:59:05.773' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/276874b4-24ce-4d6c-a4fb-b5d2d8325154_walking_17492446.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (89, 2, N'Chăm sóc thai kỳ', N'Hôm nay tôi đi khám thai, bé rất khỏe mạnh', CAST(N'2025-03-23T14:59:17.443' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/1708ea4c-6741-4bf0-85a9-eb57e1ed13c7_walking_17492446.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (90, 2, N'Dinh dưỡng mẹ bầu', N'Con tôi trong bụng 5 tháng đã biết đạp', CAST(N'2025-03-24T08:12:59.853' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/d528bc9c-703e-42f1-b20e-e833b8d0ebc4_walking_17492446.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (91, 2, N'Chăm sóc thai kỳ', N'Hôm nay tôi đi khám thai, bé rất khỏe mạnh', CAST(N'2025-03-24T08:13:13.263' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/8eebf5c2-5217-496f-b0ad-7aa762d8c4a5_walking_17492446.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (92, 2, N'Dinh dưỡng mẹ bầu', N'Con tôi trong bụng 5 tháng đã biết đạp xe đạp', CAST(N'2025-03-24T08:32:39.183' AS DateTime), 1, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/88b1f71a-6fee-41d0-892f-87422aaa8a1f_walking_17492446.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (93, 2, N'Chăm sóc thai kỳ', N'Hôm nay tôi đi khám thai, bé rất khỏe mạnh', CAST(N'2025-03-24T08:32:53.977' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/948784c1-7acc-4a35-a710-c3c388eac050_walking_17492446.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (94, 18, N'Vợ tôi', N'Tokino Sora', CAST(N'2025-03-24T09:51:57.467' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/4404db11-8ac4-4db0-9d5b-a559d9e898b0_21. The World.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (95, 18, N'Vợ tôi', N'Nekomata Okayu', CAST(N'2025-03-24T09:55:50.110' AS DateTime), 1, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/19a9bc4e-4643-4ff8-ac66-78c14f917aaa_12. The Hanged Cat.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (96, 29, N'ok', N'ok', CAST(N'2025-03-24T16:04:49.363' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/7404e174-8967-4051-b0b1-dc1554bbaf9f_Logo bau-02 (2).png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (97, 1, N'dsadas', N'đâs', CAST(N'2025-03-24T18:31:53.780' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/4f9d9931-a167-4d66-b436-f3ebc0806dae_cdee0cc151cf70cdc587e55168b4f0a9.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (98, 1, N'dsadas', N'toi la kapypara', CAST(N'2025-03-24T18:38:37.890' AS DateTime), 1, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/8b4c5afb-488c-498c-ba03-93580ae0a540_cdee0cc151cf70cdc587e55168b4f0a9.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (99, 2, N'dá', N'dsadsa', CAST(N'2025-03-24T18:49:00.027' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/7d52b32e-0688-4a38-b6e2-d50fce6232f2_471568861_2577185089336842_6353956741876802673_n.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (100, 2, N'a', N'd', CAST(N'2025-03-24T18:49:33.637' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/f58ebda7-ccd4-4464-b1cb-21b9ffd74f3e_471568861_2577185089336842_6353956741876802673_n.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (101, 1, N'lalalal', N'okokokoko', CAST(N'2025-03-24T19:20:27.070' AS DateTime), 1, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/7c2201d2-d01f-414e-9376-947eb7b5b471_471568861_2577185089336842_6353956741876802673_n.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (102, 3, N'dá', N'afgggg', CAST(N'2025-03-24T19:23:15.460' AS DateTime), 1, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/c047fc80-0b2e-40a4-9683-ce5ffc95993f_469050738_2307778806232533_5681386953925953343_n.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (103, 1, N'LOCLOCLOC', N'fjajsdklfjlksdjflsd', CAST(N'2025-03-24T19:28:31.573' AS DateTime), 1, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/e8752422-1eeb-447d-86ce-9a87d0b2e316_556d52094f1224c5bfcd3abac7de6f8d.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (104, 1, N'đâs', N'đâsdasdas', CAST(N'2025-03-24T19:31:28.060' AS DateTime), 1, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/9bd17d27-1459-410a-b1e5-eb02959047a3_cdee0cc151cf70cdc587e55168b4f0a9.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (105, 1, N'llosodaos', N'đâsdas', CAST(N'2025-03-24T19:34:26.013' AS DateTime), 1, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/4a061724-b25a-4591-9b87-3de45281143f_471568861_2577185089336842_6353956741876802673_n.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (111, 29, N'hehe', N'Quỷ Nhập Tràng là một ví dụ điển hình của việc có ý tưởng tốt nhưng triển khai quá tệ. Phim chọn một chủ đề dân gian hấp lại và không tạo được sự kịch tính cần thiết.', CAST(N'2025-03-25T03:23:32.590' AS DateTime), 1, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/d88b5c56-7fcc-4b3a-a832-0380a52c749a_qu_nh_p_tr_ng_-_payoff_poster_-_kc_07032025_1_.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (112, 50, N'Review Quỷ Nhập Tràng', N'Quỷ nhập tràng.Quỷ thì ít,hài dón thì nhiều', CAST(N'2025-03-25T04:23:38.427' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/b9018241-6708-4d7c-8c73-3b556cfc1b24_qu_nh_p_tr_ng_-_payoff_poster_-_kc_07032025_1_.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (113, 1, N'cxc', N'scascascas', CAST(N'2025-03-25T18:57:07.443' AS DateTime), 1, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/a056b68a-ca9e-463f-83e6-3b4e1006fb7d_cdee0cc151cf70cdc587e55168b4f0a9.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (114, 50, N'Chia sẻ biểu đồ', N'Đây là biểu đồ của bé nhà mình.Các mom có thể tham khảo', CAST(N'2025-03-26T08:36:05.537' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/2c018eb0-b92b-459c-a2e8-5cdbc0e537eb_thaiki.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (115, 1, N'em be moi sinh', N'em la loc 2 tuoi ruoi', CAST(N'2025-03-27T15:58:09.893' AS DateTime), 1, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/d6e5f8d8-fefa-420b-b9c7-29e59be8daf3_Screenshot 2025-01-17 010854.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (116, 1, N'khjkhj', N'hgfhfghfg', CAST(N'2025-03-27T16:47:17.520' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/5c0ff7ab-184a-42aa-b8ef-bc83330461aa_cdee0cc151cf70cdc587e55168b4f0a9.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (117, 50, N'ok', N'ok', CAST(N'2025-03-28T08:17:58.863' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/3b0e72ac-d853-48c7-94a5-dfebe39664f7_Led.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (118, 50, N'c', N'c', CAST(N'2025-03-28T17:13:00.540' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/b0ddbf73-2123-4c04-8642-2ffc1585d1a3_e5.jfif')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (119, 1, N'dá', N'đâsdasdas', CAST(N'2025-03-28T18:37:55.903' AS DateTime), 1, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/c9d04773-7e8e-444b-b12e-c355ad519e65_Screenshot 2025-01-17 010854.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (120, 2, N'helogdsfsdf', N'hhfgdfjfgjfg', CAST(N'2025-03-28T19:34:24.750' AS DateTime), 1, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/ee098b8a-498d-47c6-a9ce-5eb2a22628a0_cdee0cc151cf70cdc587e55168b4f0a9.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (121, 70, N'tôi yêu tokuda', N'Tokuda in my heart', CAST(N'2025-03-29T10:29:43.767' AS DateTime), 1, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/06d38497-0a92-46cb-8065-5e1ad27fdfcd_ongcap3_PZUE.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (122, 50, N'Hihi', N'Hihi', CAST(N'2025-03-29T12:57:37.260' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/c2dd506b-6c8e-40d5-8939-ef6854393c82_hehe.webp')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (123, 50, N'Hihi', N'Hihi', CAST(N'2025-03-29T13:03:39.607' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/c98bd41f-773f-4dfd-b13d-640b688a0fd1_ok.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (124, 50, N'.', N'.', CAST(N'2025-03-29T13:05:24.507' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/d374d349-95b7-457a-b236-1998a0989919_hhi1.gif')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (125, 50, N'Hihi', N'Hihi', CAST(N'2025-03-29T13:07:43.043' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/175c1b5f-01ae-46a5-b8df-ac4e7f9a5cec_Hihi2.gif')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (126, 50, N'Hihi', N'Hihi', CAST(N'2025-03-29T13:11:23.590' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/71a3cfe2-ab4e-4201-9ba9-cea58de02ea5_bce81f40ebc0724d0b00db7d539dd97c.0000000.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (127, 50, N'Hihi', N'Hihi', CAST(N'2025-03-29T13:18:01.643' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/cbb21828-2788-44e9-aa44-8d63b350c21b_newwww.jpg')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (128, 50, N'.', N'.', CAST(N'2025-03-29T13:20:30.530' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/12a62894-4126-4c56-b17d-bc0dc3f38c46_NEw4.gif')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (129, 50, N'.', N'.', CAST(N'2025-03-29T13:24:14.617' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/c89f1f39-36bc-4aaf-a8ec-a73db05e7e62_New5.gif')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (130, 50, N'.', N'.', CAST(N'2025-03-29T13:25:12.700' AS DateTime), 1, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/1e0ea684-b1bd-41b3-9d0b-3e72a85aba15_New6.gif')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (131, 50, N'.', N'.', CAST(N'2025-03-29T13:34:13.513' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/38ee3bb3-efdf-4888-b236-98503fa18f8e_okne.gif')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (132, 50, N'.', N'.', CAST(N'2025-03-29T13:37:00.263' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/3d8cf228-69f7-4ad3-9e0f-1779f82cd3d6_oca.gif')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (133, 50, N'Ôi, bạn ơi, sức đề kháng bạn kém là do bạn không chơi đồ đấy', N'.', CAST(N'2025-03-29T13:40:40.970' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/c4cf6713-6e7a-4bc0-a274-65eb2a1b8a3e_banh.gif')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (134, 50, N'Ôi, bạn ơi, sức đề kháng bạn kém là do bạn không chơi đồ đấy😭😭😭', N'.', CAST(N'2025-03-29T13:44:40.660' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/ab6b7959-916e-4452-afac-c53c96b3e029_anhbank.gif')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (135, 50, N'Thể dục thể thao nâng cao sức khỏe 😘😘😘', N'.', CAST(N'2025-03-29T13:46:42.173' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/22dfe48d-6c79-42e9-86f5-12a67e9fe8cc_tapdi.gif')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (136, 50, N'.', N'.', CAST(N'2025-03-29T14:03:43.700' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/45912963-c83d-4696-a930-f40ccff4c851_Screenshot 2025-03-29 210224.png')
GO
INSERT [dbo].[Post] ([PostId], [UserId], [Title], [Body], [CreatedDate], [IsActive], [PostImageUrl]) VALUES (137, 50, N'.', N'.', CAST(N'2025-03-30T16:08:27.173' AS DateTime), 0, N'https://pregnancy-growth-tracking-forum-post.s3.amazonaws.com/post_images/d906bc0c-182e-4041-a5ca-6d4ffc82ba37_hehe.gif')
GO
SET IDENTITY_INSERT [dbo].[Post] OFF
GO
SET IDENTITY_INSERT [dbo].[PostComment] ON 
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (2, N'ok', 1, 1, CAST(N'2025-03-20T08:03:36.607' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (3, N'test comment', 1, 1, CAST(N'2025-03-20T08:03:53.457' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (4, N'hello', 1, 3, CAST(N'2025-03-21T02:36:17.150' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (5, N'xin chao', 1, 1, CAST(N'2025-03-21T02:57:33.760' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (6, N'hello khang', 50, 1, CAST(N'2025-03-21T02:57:48.143' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (8, N'heaa', 69, 21, CAST(N'2025-03-21T03:49:41.927' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (10, N'hhh', 58, 1, CAST(N'2025-03-21T04:37:55.423' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (11, N'ádasdas', 58, 1, CAST(N'2025-03-21T04:37:58.520' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (12, N'dcasfasdf', 57, 1, CAST(N'2025-03-21T04:38:12.933' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (13, N'Heloo', 1, 17, CAST(N'2025-03-21T05:50:27.583' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (14, N'good wife', 75, 18, CAST(N'2025-03-21T06:56:55.337' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (15, N'Hello', 1, 25, CAST(N'2025-03-21T13:59:21.873' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (17, N'Mình có thể xem biểu đồ tuần 15 của bạn được không', 79, 27, CAST(N'2025-03-22T07:16:26.927' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (18, N'anccginguvay', 2, 1, CAST(N'2025-03-22T13:01:01.080' AS DateTime), 17, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/2/06d9b012-3a44-4226-a3c7-0820ecb86d00_SE184444.jpg')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (21, N'hello2222', 2, 3, CAST(N'2025-03-24T17:45:06.763' AS DateTime), 0, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/2/38e3be89-1210-40c1-be6c-487f6a737a49_471568861_2577185089336842_6353956741876802673_n.jpg')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (22, N'string', 90, 3, CAST(N'2025-03-24T19:35:50.003' AS DateTime), 0, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/90/662abe20-4a2c-43f7-9519-08c924ea9c44_cdee0cc151cf70cdc587e55168b4f0a9.jpg')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (23, N'lo', 90, 1, CAST(N'2025-03-24T19:42:08.547' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (24, N'im me mom', 90, 1, CAST(N'2025-03-24T19:43:06.790' AS DateTime), 0, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/90/e951fd6e-cd7a-4400-8404-1388a77cbd98_471568861_2577185089336842_6353956741876802673_n.jpg')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (25, N'hello', 90, 1, CAST(N'2025-03-24T19:43:34.087' AS DateTime), 23, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (26, N'nice ass', 90, 1, CAST(N'2025-03-24T19:49:17.147' AS DateTime), 14, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (27, N'm hay', 90, 1, CAST(N'2025-03-24T19:49:25.370' AS DateTime), 21, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (28, N'hjghjgkgjkg', 90, 1, CAST(N'2025-03-25T03:44:57.513' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (29, N'hohohoho', 90, 1, CAST(N'2025-03-25T03:45:21.227' AS DateTime), 0, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/90/ec389b97-11cf-488b-af43-0c21db739be9_cdee0cc151cf70cdc587e55168b4f0a9.jpg')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (30, N'djkashdkashk', 2, 2, CAST(N'2025-03-25T04:11:23.817' AS DateTime), 0, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/2/82c42a00-1321-4eee-824d-b017fac736a7_469050738_2307778806232533_5681386953925953343_n.jpg')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (31, N'đâsdas', 4, 2, CAST(N'2025-03-25T04:12:02.203' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (32, N'dsadas', 90, 1, CAST(N'2025-03-25T04:13:39.263' AS DateTime), 13, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (33, N'hello', 90, 1, CAST(N'2025-03-25T04:13:46.107' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (34, N'admin toi can ho tro', 90, 50, CAST(N'2025-03-25T04:18:48.460' AS DateTime), 2, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (35, N'hehe', 90, 50, CAST(N'2025-03-25T04:19:49.097' AS DateTime), 2, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/90/8144c0e8-1934-436b-9e60-710188a95945_Logo bau-02 (2).png')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (41, N'ok', 111, 50, CAST(N'2025-03-25T04:31:18.610' AS DateTime), 33, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (43, N'ok', 101, 50, CAST(N'2025-03-25T06:08:46.200' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (44, N'hihi', 112, 50, CAST(N'2025-03-25T06:09:47.373' AS DateTime), 0, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/112/5f09d911-e863-4df4-a83f-561bcbdf365b_Logo bau-02 (2).png')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (45, N'helo', 91, 1, CAST(N'2025-03-25T18:42:15.750' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (46, N'lo lo', 91, 1, CAST(N'2025-03-25T18:42:35.557' AS DateTime), 45, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (47, N'ưere', 90, 17, CAST(N'2025-03-26T06:40:27.713' AS DateTime), 33, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/90/78d67bb6-7d88-4893-8ac8-111e223f7eca_screencapture-pregnancy-growth-tracking-vercel-app-member-calendar-2025-03-20-00_28_00.png')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (48, N'hihi', 91, 50, CAST(N'2025-03-26T07:22:00.657' AS DateTime), 0, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/91/56529dd8-f309-4b46-b54b-7ff65d047fa9_qu_nh_p_tr_ng_-_payoff_poster_-_kc_07032025_1_.jpg')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (50, N'hi', 90, 50, CAST(N'2025-03-26T08:23:53.713' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (51, N'hi', 90, 50, CAST(N'2025-03-26T08:24:04.660' AS DateTime), 22, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/90/666218ed-3d9a-45e8-8109-e3b9494123a4_qu_nh_p_tr_ng_-_payoff_poster_-_kc_07032025_1_.jpg')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (52, N'lo', 90, 50, CAST(N'2025-03-26T08:33:08.197' AS DateTime), 22, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (54, N'ok', 103, 50, CAST(N'2025-03-27T02:21:59.597' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (56, N'ok', 90, 50, CAST(N'2025-03-27T02:28:28.657' AS DateTime), 0, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/90/d7470686-b3ca-4c7d-bfd7-82566e5689ac_thaiki.jpg')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (57, N'đâs', 102, 1, CAST(N'2025-03-27T15:58:31.757' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (58, N'yeu anh', 102, 1, CAST(N'2025-03-27T15:58:38.530' AS DateTime), 57, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (59, N'im di', 102, 1, CAST(N'2025-03-27T15:58:47.403' AS DateTime), 0, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/102/82056a0b-76a7-4661-a2d6-466cdede47ad_471568861_2577185089336842_6353956741876802673_n.jpg')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (60, N'alo', 111, 1, CAST(N'2025-03-27T16:37:06.780' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (61, N'hi', 90, 29, CAST(N'2025-03-27T16:49:48.740' AS DateTime), 33, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (65, N'ok', 114, 50, CAST(N'2025-03-28T08:23:34.210' AS DateTime), 53, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (68, N'k', 103, 1, CAST(N'2025-03-28T08:32:27.113' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (69, N'hhhh', 117, 50, CAST(N'2025-03-28T14:23:41.033' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (70, N'a nhon', 90, 1, CAST(N'2025-03-28T17:45:23.997' AS DateTime), 23, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (73, N'dm van eo dc', 90, 1, CAST(N'2025-03-28T18:04:53.667' AS DateTime), 33, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (74, N'alo', 91, 1, CAST(N'2025-03-28T18:14:49.057' AS DateTime), 45, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (75, N'helo', 92, 2, CAST(N'2025-03-28T19:36:11.213' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (76, N'lfhjkldsghjkdfsahgjkldfhsklghlsdfhgsdfjklghjkldfahglkdfh', 90, 2, CAST(N'2025-03-28T19:38:44.997' AS DateTime), 70, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/90/73e3ec26-04fa-4240-ad2c-04b124cc52d6_469050738_2307778806232533_5681386953925953343_n.jpg')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (77, N'toilalocdepzao', 120, 2, CAST(N'2025-03-28T19:39:50.740' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (78, N'Khangchupaka', 92, 2, CAST(N'2025-03-28T19:40:00.567' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (79, N'locdz', 92, 2, CAST(N'2025-03-28T19:40:14.353' AS DateTime), 78, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (80, N'lfhjkldsghjkdfsahgjkldfhsklghlsdfhgsdfjklghjkldfahglkdfh', 92, 2, CAST(N'2025-03-28T19:41:00.913' AS DateTime), 79, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/92/5c74fe17-bdbe-4364-b4e4-2a8c6e7e8fd1_469050738_2307778806232533_5681386953925953343_n.jpg')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (81, N'đâs', 92, 1, CAST(N'2025-03-28T19:43:55.110' AS DateTime), 78, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (82, N'hi', 92, 50, CAST(N'2025-03-28T23:01:32.380' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (83, N'hello', 101, 50, CAST(N'2025-03-29T04:03:27.147' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (84, N'ok', 101, 50, CAST(N'2025-03-29T04:03:41.887' AS DateTime), 83, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (85, N'lo', 95, 17, CAST(N'2025-03-29T04:52:10.437' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (86, N'ưeqe', 95, 17, CAST(N'2025-03-29T04:52:13.580' AS DateTime), 85, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (87, N'qưe', 95, 17, CAST(N'2025-03-29T04:52:17.783' AS DateTime), 85, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (88, N'huhu', 101, 17, CAST(N'2025-03-29T04:52:49.773' AS DateTime), 83, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (89, N'hi', 120, 50, CAST(N'2025-03-29T10:05:31.557' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (90, N'k', 115, 50, CAST(N'2025-03-29T10:21:56.997' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (91, N'ok', 115, 50, CAST(N'2025-03-29T10:25:58.777' AS DateTime), 90, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (92, N'Peter', 121, 50, CAST(N'2025-03-29T10:33:59.053' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (93, N'pk', 121, 50, CAST(N'2025-03-29T10:34:06.477' AS DateTime), 92, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (94, N'd', 121, 50, CAST(N'2025-03-29T10:34:36.450' AS DateTime), 92, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/121/cf405f31-3e3f-462d-a94c-c1a600e84627_Breadboard.jpg')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (95, N'ok', 121, 50, CAST(N'2025-03-29T10:35:02.293' AS DateTime), 0, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/121/1021fdac-46d0-4a7c-829f-5731fe1fcc41_Arduino Micro.jpg')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (96, N'adu', 135, 50, CAST(N'2025-03-29T13:50:09.600' AS DateTime), 0, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/135/1d485f50-d912-42bf-ae63-96cef054f502_okkkk.gif')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (97, N'.', 121, 50, CAST(N'2025-03-29T13:52:23.200' AS DateTime), 0, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/121/ebb5cb13-a3f1-4d3f-ae91-132f914c6e33_tapdi.gif')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (98, N'😘', 121, 50, CAST(N'2025-03-29T13:52:44.223' AS DateTime), 0, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/121/c208ea74-8123-475c-a1d0-d7bc82770d05_tapdi.gif')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (99, N'Hi', 119, 50, CAST(N'2025-03-29T13:54:41.183' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (100, N'.', 130, 50, CAST(N'2025-03-29T13:55:20.767' AS DateTime), 0, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/130/70813f90-bd6e-41fe-bb0f-3c491785d905_New6.gif')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (101, N'.', 135, 50, CAST(N'2025-03-29T13:55:36.217' AS DateTime), 0, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/135/ad0f8c5f-9b69-409d-9253-33acb387f190_ok.png')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (102, N'.', 135, 50, CAST(N'2025-03-29T13:55:53.090' AS DateTime), 0, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/135/1aa759cd-ab79-4aaf-b96f-362bac1970a7_New6.gif')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (103, N'.', 130, 50, CAST(N'2025-03-29T13:56:46.603' AS DateTime), 0, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/130/17d56532-e716-4fd9-add0-3c08d6431b6e_New6.gif')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (104, N'A Bảnh nói cc gì cũng đúng 😘😘😘', 134, 50, CAST(N'2025-03-29T13:57:40.047' AS DateTime), 0, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/134/9fb59d12-e0fa-4bd7-9362-41e4fd5a03fc_okkkk.gif')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (105, N'.', 134, 50, CAST(N'2025-03-29T13:58:17.200' AS DateTime), 104, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/134/8a4f7821-d14c-4a83-8e27-3c67fe243318_New6.gif')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (106, N'j', 134, 50, CAST(N'2025-03-30T02:41:28.783' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (107, N'helo', 130, 1, CAST(N'2025-03-30T12:17:18.380' AS DateTime), 100, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (108, N'hi', 130, 1, CAST(N'2025-03-30T12:44:57.507' AS DateTime), 100, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (109, N'hdgf', 130, 1, CAST(N'2025-03-30T12:45:34.590' AS DateTime), 100, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (110, N'ok', 115, 1, CAST(N'2025-03-30T12:58:31.923' AS DateTime), 90, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (111, N'ki', 115, 1, CAST(N'2025-03-30T12:58:37.300' AS DateTime), 90, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (112, N'la', 115, 1, CAST(N'2025-03-30T12:58:43.753' AS DateTime), 90, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (113, N'l', 130, 1, CAST(N'2025-03-30T13:06:07.723' AS DateTime), 100, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (114, N'ok', 120, 1, CAST(N'2025-03-30T13:06:46.123' AS DateTime), 77, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (115, N'l', 120, 1, CAST(N'2025-03-30T13:06:55.070' AS DateTime), 77, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (116, N'phai', 120, 1, CAST(N'2025-03-30T13:07:22.650' AS DateTime), 77, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (118, N'animal', 113, 1, CAST(N'2025-03-30T13:08:19.920' AS DateTime), 117, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (119, N'animal', 113, 1, CAST(N'2025-03-30T13:08:29.287' AS DateTime), 0, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (120, N'dog', 113, 1, CAST(N'2025-03-30T13:08:38.543' AS DateTime), 119, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (121, N'cat', 113, 1, CAST(N'2025-03-30T13:09:15.633' AS DateTime), 119, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (122, N'lll', 130, 1, CAST(N'2025-03-30T13:15:14.140' AS DateTime), 107, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (123, N'ko', 119, 1, CAST(N'2025-03-30T13:15:25.133' AS DateTime), 99, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (124, N'ok', 119, 1, CAST(N'2025-03-30T13:15:28.870' AS DateTime), 123, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (125, N'la', 119, 1, CAST(N'2025-03-30T13:15:34.030' AS DateTime), 124, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (126, N'oke luon ne', 119, 1, CAST(N'2025-03-30T13:15:43.137' AS DateTime), 124, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (127, N'koko', 119, 1, CAST(N'2025-03-30T13:15:55.703' AS DateTime), 123, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (128, N'kokokokdosa', 130, 1, CAST(N'2025-03-30T14:17:46.153' AS DateTime), 122, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (129, N'haha', 130, 1, CAST(N'2025-03-30T14:23:56.903' AS DateTime), 122, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (130, N'fasfas', 130, 1, CAST(N'2025-03-30T14:24:03.433' AS DateTime), 128, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (131, N'fasfasfas', 130, 1, CAST(N'2025-03-30T14:24:06.267' AS DateTime), 130, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (132, N'la sao nua', 130, 1, CAST(N'2025-03-30T14:25:37.910' AS DateTime), 108, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (133, N'dsadas', 130, 1, CAST(N'2025-03-30T14:33:36.740' AS DateTime), 100, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (134, N'đâs', 130, 1, CAST(N'2025-03-30T14:33:40.303' AS DateTime), 100, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (135, N'dc chua', 130, 1, CAST(N'2025-03-30T14:34:44.467' AS DateTime), 132, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (136, N'la fdsfsd', 130, 1, CAST(N'2025-03-30T14:35:01.703' AS DateTime), 122, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (137, N'đâsdas', 130, 1, CAST(N'2025-03-30T14:35:21.620' AS DateTime), 132, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (138, N'hi', 130, 1, CAST(N'2025-03-30T14:35:30.143' AS DateTime), 103, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (139, N'ba', 130, 1, CAST(N'2025-03-30T14:35:34.447' AS DateTime), 138, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (140, N'bon', 130, 1, CAST(N'2025-03-30T14:35:39.333' AS DateTime), 139, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (141, N'bon', 130, 1, CAST(N'2025-03-30T14:35:44.247' AS DateTime), 139, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (142, N'.', 137, 50, CAST(N'2025-03-30T16:09:10.110' AS DateTime), 0, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/137/2f010da1-8ecf-476a-90f2-eb4b78d0f4c3_hehe.gif')
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (143, N'ok', 130, 1, CAST(N'2025-03-30T17:14:32.750' AS DateTime), 107, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (144, N'xĩninxin', 130, 1, CAST(N'2025-03-30T17:14:45.647' AS DateTime), 131, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (145, N'ba', 130, 1, CAST(N'2025-03-30T17:22:38.267' AS DateTime), 138, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (148, N'vl', 121, 1, CAST(N'2025-03-30T18:02:41.947' AS DateTime), 98, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (149, N'ok', 130, 50, CAST(N'2025-03-31T00:09:25.230' AS DateTime), 134, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (150, N'ok', 130, 50, CAST(N'2025-03-31T00:09:37.300' AS DateTime), 128, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (151, N'ok', 130, 50, CAST(N'2025-03-31T00:09:43.037' AS DateTime), 130, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (152, N'ok', 130, 50, CAST(N'2025-03-31T00:10:01.780' AS DateTime), 138, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (153, N'h', 130, 50, CAST(N'2025-03-31T00:10:59.120' AS DateTime), 149, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (154, N'ok nh', 130, 50, CAST(N'2025-03-31T00:11:14.660' AS DateTime), 151, NULL)
GO
INSERT [dbo].[PostComment] ([CommentId], [Comment], [PostId], [UserId], [CreatedDate], [ParentCommentId], [CommentImageUrl]) VALUES (155, N'j', 130, 50, CAST(N'2025-03-31T00:13:08.610' AS DateTime), 0, N'https://pregnancy-app-profile-images.s3.amazonaws.com/comments/130/b4c92f7d-6ccf-4c6a-a965-af560d07005b_thaiki.jpg')
GO
SET IDENTITY_INSERT [dbo].[PostComment] OFF
GO
SET IDENTITY_INSERT [dbo].[PostLike] ON 
GO
INSERT [dbo].[PostLike] ([PostLikeId], [PostId], [UserId]) VALUES (4, 1, 4)
GO
INSERT [dbo].[PostLike] ([PostLikeId], [PostId], [UserId]) VALUES (7, 2, 3)
GO
INSERT [dbo].[PostLike] ([PostLikeId], [PostId], [UserId]) VALUES (1, 2, 4)
GO
INSERT [dbo].[PostLike] ([PostLikeId], [PostId], [UserId]) VALUES (32, 90, 1)
GO
INSERT [dbo].[PostLike] ([PostLikeId], [PostId], [UserId]) VALUES (18, 91, 1)
GO
INSERT [dbo].[PostLike] ([PostLikeId], [PostId], [UserId]) VALUES (14, 92, 1)
GO
INSERT [dbo].[PostLike] ([PostLikeId], [PostId], [UserId]) VALUES (19, 94, 1)
GO
INSERT [dbo].[PostLike] ([PostLikeId], [PostId], [UserId]) VALUES (34, 98, 11)
GO
INSERT [dbo].[PostLike] ([PostLikeId], [PostId], [UserId]) VALUES (35, 101, 11)
GO
INSERT [dbo].[PostLike] ([PostLikeId], [PostId], [UserId]) VALUES (36, 113, 50)
GO
INSERT [dbo].[PostLike] ([PostLikeId], [PostId], [UserId]) VALUES (21, 115, 1)
GO
INSERT [dbo].[PostLike] ([PostLikeId], [PostId], [UserId]) VALUES (40, 121, 1)
GO
INSERT [dbo].[PostLike] ([PostLikeId], [PostId], [UserId]) VALUES (39, 130, 1)
GO
SET IDENTITY_INSERT [dbo].[PostLike] OFF
GO
SET IDENTITY_INSERT [dbo].[PostTag] ON 
GO
INSERT [dbo].[PostTag] ([PostTagId], [PostId], [TagId]) VALUES (1, 1, 1)
GO
INSERT [dbo].[PostTag] ([PostTagId], [PostId], [TagId]) VALUES (2, 2, 2)
GO
INSERT [dbo].[PostTag] ([PostTagId], [PostId], [TagId]) VALUES (5, 4, 5)
GO
INSERT [dbo].[PostTag] ([PostTagId], [PostId], [TagId]) VALUES (10, 58, 11)
GO
INSERT [dbo].[PostTag] ([PostTagId], [PostId], [TagId]) VALUES (11, 58, 12)
GO
INSERT [dbo].[PostTag] ([PostTagId], [PostId], [TagId]) VALUES (12, 74, 13)
GO
INSERT [dbo].[PostTag] ([PostTagId], [PostId], [TagId]) VALUES (13, 75, 13)
GO
INSERT [dbo].[PostTag] ([PostTagId], [PostId], [TagId]) VALUES (14, 99, 14)
GO
INSERT [dbo].[PostTag] ([PostTagId], [PostId], [TagId]) VALUES (15, 99, 15)
GO
INSERT [dbo].[PostTag] ([PostTagId], [PostId], [TagId]) VALUES (16, 100, 16)
GO
INSERT [dbo].[PostTag] ([PostTagId], [PostId], [TagId]) VALUES (17, 102, 2)
GO
INSERT [dbo].[PostTag] ([PostTagId], [PostId], [TagId]) VALUES (18, 102, 2)
GO
SET IDENTITY_INSERT [dbo].[PostTag] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 
GO
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (1, N'admin')
GO
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (2, N'vip')
GO
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (3, N'member')
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Tag] ON 
GO
INSERT [dbo].[Tag] ([TagId], [TagName]) VALUES (1, N'test')
GO
INSERT [dbo].[Tag] ([TagId], [TagName]) VALUES (2, N'string')
GO
INSERT [dbo].[Tag] ([TagId], [TagName]) VALUES (3, N'loveyou3000')
GO
INSERT [dbo].[Tag] ([TagId], [TagName]) VALUES (4, N'thieuno4phuong')
GO
INSERT [dbo].[Tag] ([TagId], [TagName]) VALUES (5, N'haha')
GO
INSERT [dbo].[Tag] ([TagId], [TagName]) VALUES (11, N'nsfw')
GO
INSERT [dbo].[Tag] ([TagId], [TagName]) VALUES (12, N'r-18')
GO
INSERT [dbo].[Tag] ([TagId], [TagName]) VALUES (13, N'hololibe')
GO
INSERT [dbo].[Tag] ([TagId], [TagName]) VALUES (14, N'đâsdas')
GO
INSERT [dbo].[Tag] ([TagId], [TagName]) VALUES (15, N'đâsử')
GO
INSERT [dbo].[Tag] ([TagId], [TagName]) VALUES (16, N's')
GO
SET IDENTITY_INSERT [dbo].[Tag] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (1, N'admin1', N'Admin User', N'admin@example.com', N'admin123', CAST(N'1980-01-01' AS Date), N'0123456789', 1, 1, NULL, CAST(N'2025-02-26T19:22:07.843' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (2, N'vip1', N'Vip One', N'vip1@example.com', N'vip123', CAST(N'1990-02-01' AS Date), N'0987654321', 1, 2, NULL, CAST(N'2025-02-26T19:22:07.843' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (3, N'vip2', N'Vip Two', N'vip2@example.com', N'vip123', CAST(N'1992-03-01' AS Date), N'0987654322', 1, 2, N'https://pregnancy-app-profile-images.s3.amazonaws.com/3/ea231651-2450-4a53-a779-0cebda44bb83_concho.png', CAST(N'2025-02-26T19:22:07.843' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (4, N'vip3', N'Vip Three', N'vip3@example.com', N'vip123', CAST(N'1994-04-01' AS Date), N'0987654323', 1, 2, NULL, CAST(N'2025-02-26T19:22:07.843' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (5, N'vip4', N'Vip Four', N'vip4@example.com', N'vip123', CAST(N'1996-05-01' AS Date), N'0987654324', 1, 2, NULL, CAST(N'2025-02-26T19:22:07.843' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (6, N'vip5', N'Vip Five', N'vip5@example.com', N'vip123', CAST(N'1998-06-01' AS Date), N'0987654325', 1, 2, NULL, CAST(N'2025-02-26T19:22:07.843' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (7, N'member1', N'Member One', N'member1@example.com', N'member123', CAST(N'2000-07-01' AS Date), N'0987654326', 1, 3, NULL, CAST(N'2025-02-26T19:22:07.843' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (8, N'member2', N'Member Two', N'member2@example.com', N'member123', CAST(N'2001-08-01' AS Date), N'0987654327', 1, 3, NULL, CAST(N'2025-02-26T19:22:07.843' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (9, N'member3', N'Member Three', N'member3@example.com', N'member123', CAST(N'2002-09-01' AS Date), N'0987654328', 1, 3, NULL, CAST(N'2025-02-26T19:22:07.843' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (10, N'member4', N'Member Four', N'member4@example.com', N'member123', CAST(N'2003-10-01' AS Date), N'0987654329', 1, 3, NULL, CAST(N'2025-02-26T19:22:07.843' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (11, N'member5', N'Member Fivess', N'member5@example.com', N'member123', CAST(N'2004-11-01' AS Date), N'0987654330', 1, 3, NULL, CAST(N'2025-02-26T19:22:07.843' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (12, N'member6', N'Member Six', N'member6@example.com', N'member123', CAST(N'2005-12-01' AS Date), N'0987654331', 1, 3, N'https://pregnancy-app-profile-images.s3.amazonaws.com/12/5ce93c45-5063-4925-86be-cb178c71c090_Screenshot 2024-04-25 110431.png', CAST(N'2025-02-26T19:22:07.843' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (13, N'member7', N'Member Seven', N'member7@example.com', N'member123', CAST(N'2006-01-01' AS Date), N'0987654332', 1, 3, N'https://pregnancy-app-profile-images.s3.amazonaws.com/13/ee490018-4009-451d-86ef-e0b225791409_z6414732375218_e4ca46866fdcffeacc17fb6be2e48708.jpg', CAST(N'2025-02-26T19:22:07.843' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (14, N'member8', N'Member Eight', N'member8@example.com', N'member123', CAST(N'2007-02-01' AS Date), N'0987654333', 1, 3, NULL, CAST(N'2025-02-26T19:22:07.843' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (15, N'member9', N'Member Nine', N'member9@example.com', N'member123', CAST(N'2008-03-01' AS Date), N'0987654334', 1, 3, NULL, CAST(N'2025-02-26T19:22:07.843' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (16, N'member10', N'Member Ten', N'member10@example.com', N'member123', CAST(N'2009-04-01' AS Date), N'0987654335', 1, 3, NULL, CAST(N'2025-02-26T19:22:07.843' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (17, N'Giakhang', N'Ngô Bá Kháaaa', N'khanglove9999k@gmail.com', N'Khang123', CAST(N'2006-10-12' AS Date), N'0383989481', 1, 2, N'https://pregnancy-app-profile-images.s3.amazonaws.com/17/b3e6c6a4-0dac-475b-b4fc-99fd9cc3774e_ongcap3_PZUE.jpg', CAST(N'2025-03-18T08:14:21.190' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (18, N'kiet', N'Chu Tuấn Kiệt', N'kietvipro123456789@gmail.com', N'kiet123', CAST(N'2001-01-22' AS Date), N'0582121840', 1, 2, N'https://pregnancy-app-profile-images.s3.amazonaws.com/18/8bc20de4-1289-4696-862b-a137e2652b84_d0cd9575-999a-49c6-a5b3-d602c1e5b39b.jpg', CAST(N'2025-03-18T11:29:59.163' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (19, N'Thanhloc', N'Nhan Thanh loc', N'nhanthanhloc1753@gmail.com', N'Khang123', CAST(N'2004-12-03' AS Date), N'0941925750', 1, 3, NULL, CAST(N'2025-03-18T11:34:10.277' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (20, N'Phuongnghi', N'Lê Nguyễn Phương Nghi', N'khangdgse184442@fpt.edu.vn', N'Khang123', CAST(N'2007-08-17' AS Date), N'0383989481', 1, 2, NULL, CAST(N'2025-03-18T12:08:08.950' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (21, N'Longlatao12092004', N'Do Thanh Long', N'longdtse184a444@fpt.edu.vn', N'Long12092004', CAST(N'2025-03-26' AS Date), N'0123456789', 0, 2, N'https://pregnancy-app-profile-images.s3.amazonaws.com/21/83cb1e5e-b129-4188-83dc-912cae1c484e_Logo bau-02 (1).png', CAST(N'2025-03-18T15:44:11.270' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (22, N'swt', N'Nguyễn Văn SWT', N'swt@gmail.com', N'swt123', CAST(N'2005-02-23' AS Date), N'0383989481', 1, 2, NULL, CAST(N'2025-03-20T15:25:45.137' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (23, N'Longnew122', N'Thành Long', N'longdtse18444@gmail.com', N'Long12092004', CAST(N'2004-09-12' AS Date), N'0123456789', 0, 3, NULL, CAST(N'2025-03-21T13:55:24.280' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (24, N'Longnewne122', N'Thành Long', N'longdtse184444@gmail.com', N'Longne12092004', CAST(N'2025-03-21' AS Date), N'0123456789', 0, 3, NULL, CAST(N'2025-03-21T13:56:08.667' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (25, N'Longnewneeee1', N'Thành Long', N'longdtse184444aaaa@fpt.edu.vn', N'Longne12092004', CAST(N'2004-09-12' AS Date), N'0123456789', 0, 2, N'https://pregnancy-app-profile-images.s3.amazonaws.com/25/3f9fd648-cb22-4c42-9418-75e413c74c01_pro.jpg', CAST(N'2025-03-21T13:56:58.443' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (26, N'LongDT99', N'Thành Long', N'longdtse184444@fpt.edu.vn', N'Longne12092004', CAST(N'2025-03-20' AS Date), N'0123456789', 0, 3, NULL, CAST(N'2025-03-21T14:14:36.870' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (27, N'LongDT4444', N'Thành Long', N'sonne1609ggg2333@gmail.com', N'Long1209', CAST(N'2025-03-21' AS Date), N'0123456789', 1, 2, N'https://pregnancy-app-profile-images.s3.amazonaws.com/27/1697bb07-fb35-4a92-95b3-2661c4d1181c_pro.jpg', CAST(N'2025-03-21T14:28:29.927' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (28, N'Tranvana', N'Trần Văn B', N'danielpetter14@gmail.com', N'Khang123', CAST(N'2006-06-06' AS Date), N'0383989481', 1, 3, NULL, CAST(N'2025-03-21T20:19:27.207' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (29, N'LongDTNew4444', N'Thành Long', N'sonne16092333ggghhhhh@gmail.com', N'Long1209', CAST(N'2025-03-22' AS Date), N'0123456789', 1, 2, NULL, CAST(N'2025-03-22T01:19:16.340' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (50, N'Longnew1', N'Do Thanh Long', N'sonne16092333@gmail.com', N'Son1209', CAST(N'2025-03-26' AS Date), N'0123456789', 1, 2, N'https://pregnancy-app-profile-images.s3.amazonaws.com/50/dba0c07e-9182-48e7-9154-4c28ee0398d4_newava.jpg', CAST(N'2025-03-25T03:37:47.080' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (51, N'Thitran', N'Trần Văn Thị', N'Thi123@gmail.com', N'Thi123', CAST(N'2003-12-12' AS Date), N'0987272211', 1, 3, NULL, CAST(N'2025-03-25T16:40:23.463' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (52, N'user01', N'John Doe', N'john.doe@example.com', N'hashedpassword', CAST(N'1990-05-15' AS Date), N'0987654321', 1, 2, NULL, CAST(N'2025-01-02T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (53, N'user02', N'Jane Smith', N'jane.smith@example.com', N'hashedpassword', CAST(N'1985-07-21' AS Date), N'0987654322', 1, 2, NULL, CAST(N'2025-01-05T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (54, N'user03', N'Alice Brown', N'alice.brown@example.com', N'hashedpassword', CAST(N'1992-09-12' AS Date), N'0987654323', 1, 2, NULL, CAST(N'2025-01-10T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (55, N'user04', N'Bob Johnson', N'bob.johnson@example.com', N'hashedpassword', CAST(N'1988-11-30' AS Date), N'0987654324', 1, 2, NULL, CAST(N'2025-01-15T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (56, N'user05', N'Charlie Wilson', N'charlie.wilson@example.com', N'hashedpassword', CAST(N'1995-03-25' AS Date), N'0987654325', 1, 2, NULL, CAST(N'2025-01-20T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (57, N'user06', N'Emma Davis', N'emma.davis@example.com', N'hashedpassword', CAST(N'1993-06-18' AS Date), N'0987654326', 1, 2, NULL, CAST(N'2025-01-25T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (58, N'user07', N'Daniel White', N'daniel.white@example.com', N'hashedpassword', CAST(N'1987-04-10' AS Date), N'0987654327', 1, 3, NULL, CAST(N'2025-01-03T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (59, N'user08', N'Olivia Miller', N'olivia.miller@example.com', N'hashedpassword', CAST(N'1991-08-15' AS Date), N'0987654328', 1, 3, NULL, CAST(N'2025-01-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (60, N'user09', N'William Anderson', N'william.anderson@example.com', N'hashedpassword', CAST(N'1986-02-28' AS Date), N'0987654329', 1, 3, NULL, CAST(N'2025-01-08T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (61, N'user10', N'Sophia Taylor', N'sophia.taylor@example.com', N'hashedpassword', CAST(N'1994-12-05' AS Date), N'0987654330', 1, 3, NULL, CAST(N'2025-01-12T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (62, N'user11', N'Ethan Martinez', N'ethan.martinez@example.com', N'hashedpassword', CAST(N'1997-07-22' AS Date), N'0987654331', 1, 3, NULL, CAST(N'2025-01-14T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (63, N'user12', N'Ava Hernandez', N'ava.hernandez@example.com', N'hashedpassword', CAST(N'1990-09-17' AS Date), N'0987654332', 1, 3, NULL, CAST(N'2025-01-18T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (64, N'user13', N'James Garcia', N'james.garcia@example.com', N'hashedpassword', CAST(N'1989-05-30' AS Date), N'0987654333', 1, 3, NULL, CAST(N'2025-01-21T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (65, N'user14', N'Isabella Martinez', N'isabella.martinez@example.com', N'hashedpassword', CAST(N'1996-03-08' AS Date), N'0987654334', 1, 3, NULL, CAST(N'2025-01-23T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (66, N'user15', N'Mason Rodriguez', N'mason.rodriguez@example.com', N'hashedpassword', CAST(N'1998-01-29' AS Date), N'0987654335', 1, 3, NULL, CAST(N'2025-01-27T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (67, N'user16', N'Mia Lopez', N'mia.lopez@example.com', N'hashedpassword', CAST(N'1992-10-11' AS Date), N'0987654336', 1, 3, NULL, CAST(N'2025-01-30T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (68, N'Long1209', N'Đỗ Thành Long', N'sonne16092333@gmail.com', N'Long1209', CAST(N'2004-09-12' AS Date), N'0123456789', 1, 3, NULL, CAST(N'2025-03-28T13:38:59.747' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (69, N'Longnew2', N'Thành Long', N'longdtse184441114@fpt.edu.vn', N'Long1209', CAST(N'2025-03-26' AS Date), N'0123456789', 1, 3, NULL, CAST(N'2025-03-28T14:43:43.997' AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [FullName], [Email], [Password], [DOB], [Phone], [Available], [RoleId], [ProfileImageUrl], [CreatedAt]) VALUES (70, N'Doangia', N'Trần thị CCCCCC', N'abcd@gmail.com', N'Khang123', CAST(N'2005-02-16' AS Date), N'0943961671', 1, 3, N'https://pregnancy-app-profile-images.s3.amazonaws.com/70/67e44437-41ce-47d8-b7e6-50837f3977a3_ongcap3_PZUE.jpg', CAST(N'2025-03-29T10:13:29.920' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserNote] ON 
GO
INSERT [dbo].[UserNote] ([NoteId], [UserId], [Date], [Diagnosis], [Note], [Detail], [UserNotePhoto]) VALUES (31, 17, CAST(N'2025-03-12' AS Date), N'qưeqweq', N'qưeqwe', N'eqweqwe', N'https://pregnancy-growth-tracking-user-note.s3.amazonaws.com/user-notes/99a47e9d-bb4c-44b2-81a5-06b058f8731c-ongcap3_PZUE.jpg')
GO
INSERT [dbo].[UserNote] ([NoteId], [UserId], [Date], [Diagnosis], [Note], [Detail], [UserNotePhoto]) VALUES (33, 17, CAST(N'2025-03-12' AS Date), N'daylaghichu', N'daylaghichu', N'daylaghichu', N'https://pregnancy-growth-tracking-user-note.s3.amazonaws.com/user-notes/2194f17e-582e-4653-ae9f-a583edd54850-ongcap3_PZUE.jpg')
GO
INSERT [dbo].[UserNote] ([NoteId], [UserId], [Date], [Diagnosis], [Note], [Detail], [UserNotePhoto]) VALUES (32, 17, CAST(N'2025-03-13' AS Date), N'Thiếu máu não', N'Đại Học y Dược- Trần văn A', N'uống vitamin nhiều vô', N'https://pregnancy-growth-tracking-user-note.s3.amazonaws.com/user-notes/e9d21fce-1a33-4787-9625-cc95f28ca3f3-download.jpg')
GO
INSERT [dbo].[UserNote] ([NoteId], [UserId], [Date], [Diagnosis], [Note], [Detail], [UserNotePhoto]) VALUES (24, 17, CAST(N'2025-03-26' AS Date), N'qưeqweqw', N'qưeqwe', N'eqweqwewe', N'https://pregnancy-growth-tracking-user-note.s3.amazonaws.com/user-notes/c8c83189-dbfa-43b9-a4f8-83654a4e8ffe-ongcap3_PZUE.jpg')
GO
INSERT [dbo].[UserNote] ([NoteId], [UserId], [Date], [Diagnosis], [Note], [Detail], [UserNotePhoto]) VALUES (26, 17, CAST(N'2025-02-25' AS Date), N'ádasd', N'ádasd', N'ádasd', N'https://pregnancy-growth-tracking-user-note.s3.amazonaws.com/user-notes/4b0a6556-89e8-4a34-8af8-a318b1a85945-ongcap3_PZUE.jpg')
GO
INSERT [dbo].[UserNote] ([NoteId], [UserId], [Date], [Diagnosis], [Note], [Detail], [UserNotePhoto]) VALUES (27, 17, CAST(N'2025-03-19' AS Date), N'ádasd', N'ádasd', N'ádasdsd', N'https://pregnancy-growth-tracking-user-note.s3.amazonaws.com/user-notes/a924a026-16ae-4d9d-b043-ff83b332b311-ongcap3_PZUE.jpg')
GO
INSERT [dbo].[UserNote] ([NoteId], [UserId], [Date], [Diagnosis], [Note], [Detail], [UserNotePhoto]) VALUES (28, 17, CAST(N'2025-03-14' AS Date), N'ádasd', N'ádasd', N'ádasdsd', N'https://pregnancy-growth-tracking-user-note.s3.amazonaws.com/user-notes/755dadd6-0282-47b8-81f1-da63d10a52eb-ongcap3_PZUE.jpg')
GO
INSERT [dbo].[UserNote] ([NoteId], [UserId], [Date], [Diagnosis], [Note], [Detail], [UserNotePhoto]) VALUES (29, 17, CAST(N'2025-03-08' AS Date), N'ádasd', N'ádasd', N'ád3wqesdads', N'https://pregnancy-growth-tracking-user-note.s3.amazonaws.com/user-notes/c58b373c-bf52-413d-abc7-83cbd322008a-download.jpg')
GO
INSERT [dbo].[UserNote] ([NoteId], [UserId], [Date], [Diagnosis], [Note], [Detail], [UserNotePhoto]) VALUES (30, 17, CAST(N'2025-03-12' AS Date), N'ưeqwekhjsgfkdhgfkjskfagjhagkjdfgjgajshgfkjsdfgksdgfjsdkgfkasdgkfgskdfgkasghkjdfhkjsdahfkhadkjfhksjdhfkjashkjdfghaksdfhkjasgdfksafgjasgdfjhgaskdfgkhasdgdfkasgdkhfgaksdgfhkjasdhkjlfhasljfdhldsafsdfasdfas', N'ưeqweq', N'dffddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddđ', N'https://pregnancy-growth-tracking-user-note.s3.amazonaws.com/user-notes/7367a3fa-ab12-4205-a0da-8234a8aa126b-ongcap3_PZUE.jpg')
GO
SET IDENTITY_INSERT [dbo].[UserNote] OFF
GO
SET IDENTITY_INSERT [dbo].[UserReminders] ON 
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (5, 19, CAST(N'2025-03-18' AS Date), N'Panadol 250mml 2 viên', N'Uống Thuốc', N'Uống thuốc', 1, N'23:12')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (6, 17, CAST(N'2025-03-29' AS Date), N'Panadol 250mml 10 viên ', N'Uống Thuốc 222222', N'Uống thuốc', 1, N'23:13')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (13, 3, CAST(N'2025-03-20' AS Date), N'Test notification', N'Test Event', N'Uống thuốc', 1, N'14:00')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (14, 3, CAST(N'2025-03-21' AS Date), N'Test notification', N'Test Event', N'Uống thuốc', 1, N'14:00')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (16, 3, CAST(N'2025-03-21' AS Date), N'ăn cơm với thuốc', N'123', N'Uống thuốc', 1, N'12:59')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (17, 3, CAST(N'2025-03-21' AS Date), N'ăn cơm với thuốc', N'123', N'Uống thuốc', 1, N'12:59')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (18, 3, CAST(N'2025-03-21' AS Date), N'ăn cơm với thuốc', N'123', N'Uống thuốc', 1, N'12:59')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (19, 3, CAST(N'2025-03-21' AS Date), N'ăn cơm với thuốc', N'123', N'Uống thuốc', 1, N'12:59')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (20, 3, CAST(N'2025-03-21' AS Date), N'ăn cơm với thuốc', N'123', N'Uống thuốc', 1, N'12:59')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (21, 3, CAST(N'2025-03-21' AS Date), N'ăn cơm với thuốc', N'123', N'Uống thuốc', 1, N'12:59')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (22, 3, CAST(N'2025-03-21' AS Date), N'ăn cơm với thuốc', N'123', N'Uống thuốc', 1, N'12:59')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (23, 3, CAST(N'2025-03-21' AS Date), N'ăn cơm với thuốc', N'123', N'Uống thuốc', 1, N'12:59')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (24, 3, CAST(N'2025-03-21' AS Date), N'ăn cơm với thuốc', N'123', N'Uống thuốc', 1, N'12:59')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (30, 17, CAST(N'2025-03-21' AS Date), N'Hehehee', N'Uống thuốc đi bé iu 2123', N'Uống thuốc', 1, N'20:27')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (31, 27, CAST(N'2025-03-21' AS Date), N'Uống 2 viên mỗi ngày sau khi ăn no', N'Uống thuốc', N'Uống thuốc', 1, N'22:35')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (37, 17, CAST(N'2025-03-23' AS Date), N'eheh', N'ngủ', N'Cuộc hẹn bác sĩ', 1, N'06:18')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (60, 29, CAST(N'2025-03-22' AS Date), N'Ăn no trước khi uống', N'Uông thuốc', N'Uống thuốc', 0, N'20:45')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (61, 29, CAST(N'2025-03-22' AS Date), N'Tập cardio', N'Tập thể dục', N'Tập thể dục', 1, N'21:15')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (63, 17, CAST(N'2025-04-01' AS Date), N'234234', N'23423', N'Uống thuốc', 0, N'00:12')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (65, 22, CAST(N'2025-03-24' AS Date), N'Bạn hãy nhớ uống thuốc đúng giờ nhé.', N'Sức khỏe', N'Uống thuốc', 0, N'13:30')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (66, 22, CAST(N'2025-03-24' AS Date), N'', N'Sức Khỏe', N'Uống thuốc', 0, N'13:45')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (67, 22, CAST(N'2025-03-24' AS Date), N'Bạn hãy nhớ uống thuốc đúng giờ nhé.', N'Sức khỏe', N'Uống thuốc', 0, N'13:46')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (68, 22, CAST(N'2025-03-24' AS Date), N'Bạn hãy nhớ uống thuốc đúng giờ nhé.', N'Sức khỏe', N'Uống thuốc', 0, N'13:46')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (69, 22, CAST(N'2025-03-24' AS Date), N'', N'sức khỏe', N'Cuộc hẹn bác sĩ', 0, N'13:47')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (70, 22, CAST(N'2025-03-24' AS Date), N'', N'sức khỏe', N'Uống thuốc', 0, N'13:48')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (71, 22, CAST(N'2025-03-24' AS Date), N'Bạn hãy nhớ uống thuốc đúng giờ nhé.', N'Sức khỏe', N'Uống thuốc', 0, N'13:50')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (72, 22, CAST(N'2025-03-24' AS Date), N'Bạn hãy nhớ uống thuốc đúng giờ nhé.', N'Sức khỏe', N'Uống thuốc', 0, N'13:51')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (73, 22, CAST(N'2025-04-24' AS Date), N'Bạn hãy nhớ uống thuốc đúng giờ nhé.', N'Sức khỏe', N'Uống thuốc', 0, N'15:30')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (74, 22, CAST(N'2025-04-24' AS Date), N'Bạn hãy nhớ uống thuốc đúng giờ nhé.', N'Sức khỏe', N'Uống thuốc', 0, N'15:30')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (76, 17, CAST(N'2025-03-27' AS Date), N'ưerwerwerwerwe', N'heheh', N'Uống thuốc', 1, N'23:03')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (81, 50, CAST(N'2025-03-30' AS Date), N'Ăn no trước khi uống', N'Uống thuốc', N'Uống thuốc', 1, N'10:50')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (82, 17, CAST(N'2025-04-02' AS Date), N'Hehehheeee', N'Đi ngủ đi m', N'Cuộc hẹn bác sĩ', 0, N'12:31')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (83, 17, CAST(N'2025-04-01' AS Date), N'ĐI khám ABC', N'Đi khám thai ở từ dũ', N'Khám thai', 0, N'17:57')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (84, 50, CAST(N'2025-03-31' AS Date), N'Goodluck', N'Presentation', N'Cuộc hẹn bác sĩ', 1, N'13:20')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (85, 17, CAST(N'2025-04-05' AS Date), N'Hehehhehehhe', N'Man đần', N'Cuộc hẹn bác sĩ', 0, N'04:07')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (86, 17, CAST(N'2025-04-05' AS Date), N'ưerwerwerwrwerwrwer', N'ewrerwerwerwerwerwerwer', N'Khám thai', 0, N'05:22')
GO
INSERT [dbo].[UserReminders] ([RemindId], [UserId], [Date], [Notification], [Title], [ReminderType], [IsEmailSent], [Time]) VALUES (87, 50, CAST(N'2025-03-31' AS Date), N'ok', N'ok', N'Cuộc hẹn bác sĩ', 1, N'14:35')
GO
SET IDENTITY_INSERT [dbo].[UserReminders] OFF
GO
/****** Object:  Index [UQ__BlogCate__F5A70D9171323183]    Script Date: 31/03/2025 2:37:07 CH ******/
ALTER TABLE [dbo].[BlogCate] ADD UNIQUE NONCLUSTERED 
(
	[BlogId] ASC,
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UC_CommentLike]    Script Date: 31/03/2025 2:37:07 CH ******/
ALTER TABLE [dbo].[CommentLike] ADD  CONSTRAINT [UC_CommentLike] UNIQUE NONCLUSTERED 
(
	[CommentId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UC_PostLike]    Script Date: 31/03/2025 2:37:07 CH ******/
ALTER TABLE [dbo].[PostLike] ADD  CONSTRAINT [UC_PostLike] UNIQUE NONCLUSTERED 
(
	[PostId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Post] ADD  CONSTRAINT [DF_Post_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[UserReminders] ADD  CONSTRAINT [DF_UserReminders_IsEmailSent]  DEFAULT ((0)) FOR [IsEmailSent]
GO
ALTER TABLE [dbo].[BlogCate]  WITH CHECK ADD FOREIGN KEY([BlogId])
REFERENCES [dbo].[Blog] ([BlogId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BlogCate]  WITH CHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CommentLike]  WITH CHECK ADD FOREIGN KEY([CommentId])
REFERENCES [dbo].[PostComment] ([CommentId])
GO
ALTER TABLE [dbo].[CommentLike]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Foetus]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[GrowthData]  WITH CHECK ADD FOREIGN KEY([FoetusId])
REFERENCES [dbo].[Foetus] ([FoetusId])
GO
ALTER TABLE [dbo].[GrowthData]  WITH CHECK ADD  CONSTRAINT [FK_GrowthData_GrowthStandard] FOREIGN KEY([GrowthStandardId])
REFERENCES [dbo].[GrowthStandard] ([GrowthStandardId])
GO
ALTER TABLE [dbo].[GrowthData] CHECK CONSTRAINT [FK_GrowthData_GrowthStandard]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD FOREIGN KEY([MembershipId])
REFERENCES [dbo].[Membership] ([MembershipId])
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[PostComment]  WITH CHECK ADD FOREIGN KEY([PostId])
REFERENCES [dbo].[Post] ([PostId])
GO
ALTER TABLE [dbo].[PostComment]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[PostLike]  WITH CHECK ADD FOREIGN KEY([PostId])
REFERENCES [dbo].[Post] ([PostId])
GO
ALTER TABLE [dbo].[PostLike]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[PostTag]  WITH CHECK ADD FOREIGN KEY([PostId])
REFERENCES [dbo].[Post] ([PostId])
GO
ALTER TABLE [dbo].[PostTag]  WITH CHECK ADD FOREIGN KEY([TagId])
REFERENCES [dbo].[Tag] ([TagId])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[UserNote]  WITH CHECK ADD  CONSTRAINT [FK_UserNote_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserNote] CHECK CONSTRAINT [FK_UserNote_User]
GO
ALTER TABLE [dbo].[UserReminders]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
