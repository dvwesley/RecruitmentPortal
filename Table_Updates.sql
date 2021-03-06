/****** Object:  Table [dbo].[Recruiter]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Recruiter]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Recruiter](
	[RecruiterId] [int] NOT NULL,
	[PhoneNo] [varchar](10) NULL,
	[Specialty] [nvarchar](100) NULL,
	[AreasOfImprovment] [nvarchar](250) NULL,
	[CareerPath] [nvarchar](250) NULL,
	[Communities] [nvarchar](250) NULL,
	[Salary] [decimal](18, 0) NULL,
	[ManagerId] [int] NULL,
	[RoleId] [int] NULL,
	[Email] [nvarchar](50) NULL,
 CONSTRAINT [PK_Recruiter] PRIMARY KEY CLUSTERED 
(
	[RecruiterId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DocumentType]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DocumentType](
	[DocumentTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DocumentGroup] [int] NULL,
 CONSTRAINT [PK_DocumentType] PRIMARY KEY CLUSTERED 
(
	[DocumentTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[CommissionType]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommissionType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CommissionType](
	[CommissionTypeId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Value] [decimal](18, 0) NOT NULL,
	[IsPercentage] [bit] NOT NULL,
 CONSTRAINT [PK_CommissionType] PRIMARY KEY CLUSTERED 
(
	[CommissionTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Client]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Client]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Client](
	[ClientId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Account] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[BillingContact] [nvarchar](100) NULL,
	[Address1] [nvarchar](50) NULL,
	[Address2] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[State] [varchar](2) NULL,
	[ZipCode] [nvarchar](50) NULL,
	[BlackListed] [bit] NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Referrer]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Referrer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Referrer](
	[ReferrerId] [int] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[MiddleInitial] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[PhoneNo] [varchar](10) NULL,
 CONSTRAINT [PK_Referrer] PRIMARY KEY CLUSTERED 
(
	[ReferrerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SkillSet]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SkillSet]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SkillSet](
	[SkillSetId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_SkillSet] PRIMARY KEY CLUSTERED 
(
	[SkillSetId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ResourceType]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResourceType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ResourceType](
	[ResourceTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_ResourceType] PRIMARY KEY CLUSTERED 
(
	[ResourceTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Vendor]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Vendor]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Vendor](
	[VendorId] [int] NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Address1] [nvarchar](50) NULL,
	[Address2] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[State] [varchar](2) NULL,
	[ZipCode] [varchar](10) NULL,
	[PhoneNo] [varchar](10) NULL,
	[BillingContact] [nvarchar](100) NULL,
	[Terms] [nvarchar](500) NULL,
	[W9] [bit] NULL,
	[InsuranceCertificates] [bit] NULL,
	[Contracts] [bit] NULL,
	[BlackListed] [bit] NOT NULL,
 CONSTRAINT [PK_Vendor] PRIMARY KEY CLUSTERED 
(
	[VendorId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[VendorDocument]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VendorDocument]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[VendorDocument](
	[VendorDocumentId] [int] NOT NULL,
	[VendorId] [int] NOT NULL,
	[DocumentTypeId] [int] NOT NULL,
	[FilePath] [nvarchar](500) NULL,
 CONSTRAINT [PK_VendorDocument] PRIMARY KEY CLUSTERED 
(
	[VendorDocumentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Requirement]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Requirement]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Requirement](
	[RequirementId] [int] NOT NULL,
	[JobNumber] [varchar](10) NOT NULL,
	[Priority] [int] NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Location] [nvarchar](100) NULL,
	[Duration] [int] NULL,
	[HourlyBuyRate] [money] NULL,
	[HourlyBillingRate] [money] NULL,
	[JobTypeId] [int] NOT NULL,
	[OneTimeFee] [money] NULL,
	[InternalResumeCount] [int] NULL,
	[ClientResumeCount] [int] NULL,
	[Status] [int] NULL,
	[ReasonNotClosed] [nvarchar](250) NULL,
	[RecruiterId] [int] NULL,
	[Tier1ClientId] [int] NULL,
	[Tier2ClientId] [int] NULL,
	[AccountManagerId] [int] NULL,
 CONSTRAINT [PK_Requirement] PRIMARY KEY CLUSTERED 
(
	[RequirementId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RecruiterTarget]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecruiterTarget]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RecruiterTarget](
	[RecruiterTargetId] [int] NOT NULL,
	[RecruiterId] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[TargetDate] [date] NOT NULL,
	[TargetType] [int] NOT NULL,
	[TargetCount] [int] NOT NULL,
	[TargetReached] [bit] NULL,
	[NoofResumesContributed] [int] NULL,
	[CommissionAccrued] [money] NULL,
	[CommissionPaid] [money] NULL,
	[H1BContributed] [int] NULL,
	[W2Placement] [int] NULL,
	[CTCPlacement] [int] NULL,
	[1099Placement] [int] NULL,
	[CitizenPlacement] [int] NULL,
	[CTHPlacement] [int] NULL,
	[ProActiveHiring] [int] NULL,
	[Profitability] [money] NULL,
	[CommissionTypeId] [int] NOT NULL,
 CONSTRAINT [PK_RecruiterTarget] PRIMARY KEY CLUSTERED 
(
	[RecruiterTargetId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Resource]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Resource](
	[ResourceId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[MiddleInitial] [varchar](50) NULL,
	[Sex] [varchar](1) NOT NULL,
	[DOB] [date] NULL,
	[Address1] [nvarchar](100) NULL,
	[Address2] [nvarchar](100) NULL,
	[City] [nvarchar](50) NULL,
	[State] [varchar](2) NULL,
	[ZipCode] [varchar](10) NULL,
	[ResourceTypeId] [int] NOT NULL,
	[PhoneNo1] [varchar](50) NULL,
	[PhoneNo2] [varchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Email2] [nvarchar](50) NULL,
	[LinkedInProfile] [nvarchar](50) NULL,
	[SkypeId] [nvarchar](50) NULL,
	[YearsOfExperience] [decimal](10, 2) NULL,
	[PersonalInterview] [bit] NOT NULL,
	[HourlyRate] [money] NULL,
	[Salary] [money] NULL,
	[CTC] [money] NULL,
	[SwitchLaneInterviewed] [bit] NOT NULL,
	[NoofClientInterviews] [int] NULL,
	[DomainExpertise] [nvarchar](500) NULL,
	[LocationPreference] [nvarchar](100) NULL,
	[BackgroundCheck] [bit] NOT NULL,
	[EducationCertificates] [bit] NOT NULL,
	[VisaCopy] [bit] NOT NULL,
	[PassPortCopy] [bit] NOT NULL,
	[LicenseCopy] [bit] NOT NULL,
	[PhotoId] [bit] NOT NULL,
	[I9Form] [bit] NOT NULL,
	[GreenCardDocuments] [bit] NOT NULL,
	[PayStubs] [bit] NOT NULL,
	[W2Copy] [bit] NOT NULL,
	[MaritalStatus] [varchar](1) NULL,
	[NoofKids] [int] NULL,
	[Anniversary] [date] NULL,
	[LastMeetingDate] [date] NULL,
	[NextMeetingDate] [date] NULL,
	[TrainingAreas] [nvarchar](500) NULL,
	[Reference1] [nvarchar](500) NULL,
	[Reference2] [nvarchar](500) NULL,
	[Reference3] [nvarchar](500) NULL,
	[ClientId] [int] NULL,
	[ProjectEndDate] [date] NULL,
	[SpouseDOB] [date] NULL,
	[Kid1DOB] [date] NULL,
	[Kid2DOB] [date] NULL,
	[ReferrerId] [int] NULL,
	[ReferralAmountDue] [money] NULL,
	[ReferredConsultantId] [int] NULL,
	[Tier1VendorId] [int] NULL,
	[Tier2VendorId] [int] NULL,
	[USExperience] [float] NULL,
	[VisaExpirationDate] [date] NULL,
	[EAD] [bit] NOT NULL,
	[OPT] [bit] NOT NULL,
	[SSN] [int] NULL,
	[NDA] [bit] NOT NULL,
	[EADCopy] [bit] NOT NULL,
	[Source] [nvarchar](100) NULL,
	[Availability] [nvarchar](100) NULL,
	[BlackListed] [bit] NOT NULL,
	[Notes] [ntext] NULL,
	[Qualification] [nvarchar](50) NULL,
	[ResidentialStatus] [varchar](1) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedTime] [datetime] NULL,
 CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED 
(
	[ResourceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ClientDocument]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClientDocument]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ClientDocument](
	[ClientDocumentId] [int] NOT NULL,
	[ClientId] [int] NOT NULL,
	[DocumentTypeId] [int] NOT NULL,
	[FilePath] [nvarchar](500) NULL,
 CONSTRAINT [PK_ClientDocument] PRIMARY KEY CLUSTERED 
(
	[ClientDocumentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProjectDetail]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProjectDetail](
	[ProjectDetailId] [int] NOT NULL,
	[RequirementId] [int] NOT NULL,
	[ResourceId] [int] NOT NULL,
	[RecruiterId] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NULL,
	[PlacementDate] [date] NOT NULL,
	[AccountManagerId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectDetail] PRIMARY KEY CLUSTERED 
(
	[ProjectDetailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Invoice](
	[InvoiceId] [int] NOT NULL,
	[ResourceId] [int] NOT NULL,
	[InvoiceNo] [varchar](15) NOT NULL,
	[WeekOf] [date] NOT NULL,
	[Amount] [money] NOT NULL,
	[FilePath] [nvarchar](500) NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Interviewer]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Interviewer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Interviewer](
	[InterviewerId] [int] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[MiddleInitial] [varchar](50) NULL,
	[Address1] [varchar](50) NULL,
	[Address2] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](2) NULL,
	[ZipCode] [varchar](50) NULL,
	[Summary] [varchar](1000) NULL,
	[AvailableonSundays] [bit] NULL,
	[AvailableonMondays] [bit] NULL,
	[AvailableonTuesdays] [bit] NULL,
	[AvailableonWednesdays] [bit] NULL,
	[AvailableonThursdays] [bit] NULL,
	[AvailableonFridays] [bit] NULL,
	[AvailabaleonSaturdays] [bit] NULL,
	[HourlyRate] [money] NULL,
	[FixedFee] [money] NULL,
	[PhoneNo] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[ResourceId] [int] NULL,
 CONSTRAINT [PK_Interviewer] PRIMARY KEY CLUSTERED 
(
	[InterviewerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RequirementSkillSet]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RequirementSkillSet]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RequirementSkillSet](
	[RequirementSkillSetId] [int] NOT NULL,
	[RequirementId] [int] NOT NULL,
	[SkillSetId] [int] NOT NULL,
	[IsMandatory] [bit] NULL,
 CONSTRAINT [PK_RequirementSkillSet] PRIMARY KEY CLUSTERED 
(
	[RequirementSkillSetId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[RequirementDetail]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RequirementDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RequirementDetail](
	[RequirementDetailId] [int] NOT NULL,
	[RequirementId] [int] NOT NULL,
	[RecruiterId] [int] NOT NULL,
	[ResourceId] [int] NULL,
	[ReferredToClient] [bit] NULL,
	[CandidateSelected] [bit] NULL,
	[Notes] [nvarchar](500) NULL,
 CONSTRAINT [PK_RequirementDetail] PRIMARY KEY CLUSTERED 
(
	[RequirementDetailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ResourceSkillSet]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResourceSkillSet]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ResourceSkillSet](
	[ResourceSkillSetId] [int] IDENTITY(1,1) NOT NULL,
	[ResourceId] [int] NOT NULL,
	[SkillSetId] [int] NOT NULL,
	[SelfRating] [int] NULL,
	[SelfComments] [nvarchar](100) NULL,
	[RecruiterRating] [int] NULL,
	[RecruiterComments] [nvarchar](100) NULL,
 CONSTRAINT [PK_ResourceSkillSet] PRIMARY KEY CLUSTERED 
(
	[ResourceSkillSetId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ResourceDocument]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResourceDocument]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ResourceDocument](
	[ResourceDocumentId] [int] IDENTITY(1,1) NOT NULL,
	[ResourceId] [int] NOT NULL,
	[DocumentTypeId] [int] NOT NULL,
	[Description] [nvarchar](100) NULL,
	[FileName] [nvarchar](100) NULL,
	[FilePath] [nvarchar](250) NULL,
	[ContentType] [nvarchar](100) NULL,
	[UploadedBy] [int] NOT NULL,
	[UploadedTimestamp] [datetime] NOT NULL,
 CONSTRAINT [PK_ResourceDocument] PRIMARY KEY CLUSTERED 
(
	[ResourceDocumentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Timesheet]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Timesheet]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Timesheet](
	[TimesheetId] [int] NOT NULL,
	[ResourceId] [int] NOT NULL,
	[WeekOf] [date] NOT NULL,
	[BillableHours] [decimal](10, 2) NOT NULL,
	[FilePath] [nvarchar](500) NOT NULL,
	[ProjectDetailId] [int] NOT NULL,
 CONSTRAINT [PK_Timesheet] PRIMARY KEY CLUSTERED 
(
	[TimesheetId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Interview]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Interview]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Interview](
	[InterviewId] [int] NOT NULL,
	[ResourceId] [int] NOT NULL,
	[ScheduledDate] [datetime] NULL,
	[PhoneNo] [nvarchar](50) NULL,
	[MeetingLink] [nvarchar](500) NULL,
	[Feedback] [nvarchar](500) NULL,
	[ClientInterview] [bit] NULL,
	[SwitchLaneInterview] [bit] NULL,
	[Status] [varchar](1) NOT NULL,
	[ClientId] [int] NULL,
	[InterviewerId] [int] NULL,
	[FeePaid] [bit] NULL,
	[Fee] [money] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[RequirementId] [int] NULL,
 CONSTRAINT [PK_Interview] PRIMARY KEY CLUSTERED 
(
	[InterviewId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InterviewDocument]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InterviewDocument]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[InterviewDocument](
	[InterviewDocumentId] [int] NOT NULL,
	[InterviewId] [int] NOT NULL,
	[DocumentTypeId] [int] NOT NULL,
	[FilePath] [nvarchar](100) NULL,
 CONSTRAINT [PK_InterviewDocument] PRIMARY KEY CLUSTERED 
(
	[InterviewDocumentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[InterviewDetail]    Script Date: 04/27/2015 17:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InterviewDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[InterviewDetail](
	[InterviewDetailId] [int] NOT NULL,
	[InterviewId] [int] NOT NULL,
	[SkillSetId] [int] NOT NULL,
	[Rating] [int] NOT NULL,
	[Comments] [nvarchar](250) NULL,
 CONSTRAINT [PK_InterviewDetail] PRIMARY KEY CLUSTERED 
(
	[InterviewDetailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  ForeignKey [FK_ClientDocument_Client]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClientDocument_Client]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClientDocument]'))
ALTER TABLE [dbo].[ClientDocument]  WITH CHECK ADD  CONSTRAINT [FK_ClientDocument_Client] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([ClientId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClientDocument_Client]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClientDocument]'))
ALTER TABLE [dbo].[ClientDocument] CHECK CONSTRAINT [FK_ClientDocument_Client]
GO
/****** Object:  ForeignKey [FK_ClientDocument_DocumentType]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClientDocument_DocumentType]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClientDocument]'))
ALTER TABLE [dbo].[ClientDocument]  WITH CHECK ADD  CONSTRAINT [FK_ClientDocument_DocumentType] FOREIGN KEY([DocumentTypeId])
REFERENCES [dbo].[DocumentType] ([DocumentTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClientDocument_DocumentType]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClientDocument]'))
ALTER TABLE [dbo].[ClientDocument] CHECK CONSTRAINT [FK_ClientDocument_DocumentType]
GO
/****** Object:  ForeignKey [FK_Interview_Client]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Interview_Client]') AND parent_object_id = OBJECT_ID(N'[dbo].[Interview]'))
ALTER TABLE [dbo].[Interview]  WITH CHECK ADD  CONSTRAINT [FK_Interview_Client] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([ClientId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Interview_Client]') AND parent_object_id = OBJECT_ID(N'[dbo].[Interview]'))
ALTER TABLE [dbo].[Interview] CHECK CONSTRAINT [FK_Interview_Client]
GO
/****** Object:  ForeignKey [FK_Interview_Interviewer]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Interview_Interviewer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Interview]'))
ALTER TABLE [dbo].[Interview]  WITH CHECK ADD  CONSTRAINT [FK_Interview_Interviewer] FOREIGN KEY([InterviewerId])
REFERENCES [dbo].[Interviewer] ([InterviewerId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Interview_Interviewer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Interview]'))
ALTER TABLE [dbo].[Interview] CHECK CONSTRAINT [FK_Interview_Interviewer]
GO
/****** Object:  ForeignKey [FK_Interview_Requirement]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Interview_Requirement]') AND parent_object_id = OBJECT_ID(N'[dbo].[Interview]'))
ALTER TABLE [dbo].[Interview]  WITH CHECK ADD  CONSTRAINT [FK_Interview_Requirement] FOREIGN KEY([RequirementId])
REFERENCES [dbo].[Requirement] ([RequirementId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Interview_Requirement]') AND parent_object_id = OBJECT_ID(N'[dbo].[Interview]'))
ALTER TABLE [dbo].[Interview] CHECK CONSTRAINT [FK_Interview_Requirement]
GO
/****** Object:  ForeignKey [FK_Interview_Resource]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Interview_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[Interview]'))
ALTER TABLE [dbo].[Interview]  WITH CHECK ADD  CONSTRAINT [FK_Interview_Resource] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resource] ([ResourceId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Interview_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[Interview]'))
ALTER TABLE [dbo].[Interview] CHECK CONSTRAINT [FK_Interview_Resource]
GO
/****** Object:  ForeignKey [FK_InterviewDetail_Interview]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_InterviewDetail_Interview]') AND parent_object_id = OBJECT_ID(N'[dbo].[InterviewDetail]'))
ALTER TABLE [dbo].[InterviewDetail]  WITH CHECK ADD  CONSTRAINT [FK_InterviewDetail_Interview] FOREIGN KEY([InterviewId])
REFERENCES [dbo].[Interview] ([InterviewId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_InterviewDetail_Interview]') AND parent_object_id = OBJECT_ID(N'[dbo].[InterviewDetail]'))
ALTER TABLE [dbo].[InterviewDetail] CHECK CONSTRAINT [FK_InterviewDetail_Interview]
GO
/****** Object:  ForeignKey [FK_InterviewDocument_DocumentType]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_InterviewDocument_DocumentType]') AND parent_object_id = OBJECT_ID(N'[dbo].[InterviewDocument]'))
ALTER TABLE [dbo].[InterviewDocument]  WITH CHECK ADD  CONSTRAINT [FK_InterviewDocument_DocumentType] FOREIGN KEY([DocumentTypeId])
REFERENCES [dbo].[DocumentType] ([DocumentTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_InterviewDocument_DocumentType]') AND parent_object_id = OBJECT_ID(N'[dbo].[InterviewDocument]'))
ALTER TABLE [dbo].[InterviewDocument] CHECK CONSTRAINT [FK_InterviewDocument_DocumentType]
GO
/****** Object:  ForeignKey [FK_InterviewDocument_Interview]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_InterviewDocument_Interview]') AND parent_object_id = OBJECT_ID(N'[dbo].[InterviewDocument]'))
ALTER TABLE [dbo].[InterviewDocument]  WITH CHECK ADD  CONSTRAINT [FK_InterviewDocument_Interview] FOREIGN KEY([InterviewId])
REFERENCES [dbo].[Interview] ([InterviewId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_InterviewDocument_Interview]') AND parent_object_id = OBJECT_ID(N'[dbo].[InterviewDocument]'))
ALTER TABLE [dbo].[InterviewDocument] CHECK CONSTRAINT [FK_InterviewDocument_Interview]
GO
/****** Object:  ForeignKey [FK_Interviewer_Resource]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Interviewer_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[Interviewer]'))
ALTER TABLE [dbo].[Interviewer]  WITH CHECK ADD  CONSTRAINT [FK_Interviewer_Resource] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resource] ([ResourceId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Interviewer_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[Interviewer]'))
ALTER TABLE [dbo].[Interviewer] CHECK CONSTRAINT [FK_Interviewer_Resource]
GO
/****** Object:  ForeignKey [FK_Invoice_Resource]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Invoice_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[Invoice]'))
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Resource] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resource] ([ResourceId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Invoice_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[Invoice]'))
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Resource]
GO
/****** Object:  ForeignKey [FK_ProjectDetail_Recruiter]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProjectDetail_Recruiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProjectDetail]'))
ALTER TABLE [dbo].[ProjectDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProjectDetail_Recruiter] FOREIGN KEY([RecruiterId])
REFERENCES [dbo].[Recruiter] ([RecruiterId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProjectDetail_Recruiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProjectDetail]'))
ALTER TABLE [dbo].[ProjectDetail] CHECK CONSTRAINT [FK_ProjectDetail_Recruiter]
GO
/****** Object:  ForeignKey [FK_ProjectDetail_Recruiter1]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProjectDetail_Recruiter1]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProjectDetail]'))
ALTER TABLE [dbo].[ProjectDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProjectDetail_Recruiter1] FOREIGN KEY([AccountManagerId])
REFERENCES [dbo].[Recruiter] ([RecruiterId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProjectDetail_Recruiter1]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProjectDetail]'))
ALTER TABLE [dbo].[ProjectDetail] CHECK CONSTRAINT [FK_ProjectDetail_Recruiter1]
GO
/****** Object:  ForeignKey [FK_ProjectDetail_Requirement]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProjectDetail_Requirement]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProjectDetail]'))
ALTER TABLE [dbo].[ProjectDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProjectDetail_Requirement] FOREIGN KEY([RequirementId])
REFERENCES [dbo].[Requirement] ([RequirementId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProjectDetail_Requirement]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProjectDetail]'))
ALTER TABLE [dbo].[ProjectDetail] CHECK CONSTRAINT [FK_ProjectDetail_Requirement]
GO
/****** Object:  ForeignKey [FK_ProjectDetail_Resource]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProjectDetail_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProjectDetail]'))
ALTER TABLE [dbo].[ProjectDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProjectDetail_Resource] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resource] ([ResourceId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProjectDetail_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProjectDetail]'))
ALTER TABLE [dbo].[ProjectDetail] CHECK CONSTRAINT [FK_ProjectDetail_Resource]
GO
/****** Object:  ForeignKey [FK_RecruiterTarget_CommissionType]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RecruiterTarget_CommissionType]') AND parent_object_id = OBJECT_ID(N'[dbo].[RecruiterTarget]'))
ALTER TABLE [dbo].[RecruiterTarget]  WITH CHECK ADD  CONSTRAINT [FK_RecruiterTarget_CommissionType] FOREIGN KEY([CommissionTypeId])
REFERENCES [dbo].[CommissionType] ([CommissionTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RecruiterTarget_CommissionType]') AND parent_object_id = OBJECT_ID(N'[dbo].[RecruiterTarget]'))
ALTER TABLE [dbo].[RecruiterTarget] CHECK CONSTRAINT [FK_RecruiterTarget_CommissionType]
GO
/****** Object:  ForeignKey [FK_RecruiterTarget_Recruiter]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RecruiterTarget_Recruiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[RecruiterTarget]'))
ALTER TABLE [dbo].[RecruiterTarget]  WITH CHECK ADD  CONSTRAINT [FK_RecruiterTarget_Recruiter] FOREIGN KEY([RecruiterId])
REFERENCES [dbo].[Recruiter] ([RecruiterId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RecruiterTarget_Recruiter]') AND parent_object_id = OBJECT_ID(N'[dbo].[RecruiterTarget]'))
ALTER TABLE [dbo].[RecruiterTarget] CHECK CONSTRAINT [FK_RecruiterTarget_Recruiter]
GO
/****** Object:  ForeignKey [FK_Requirement_Client]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Requirement_Client]') AND parent_object_id = OBJECT_ID(N'[dbo].[Requirement]'))
ALTER TABLE [dbo].[Requirement]  WITH CHECK ADD  CONSTRAINT [FK_Requirement_Client] FOREIGN KEY([Tier1ClientId])
REFERENCES [dbo].[Client] ([ClientId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Requirement_Client]') AND parent_object_id = OBJECT_ID(N'[dbo].[Requirement]'))
ALTER TABLE [dbo].[Requirement] CHECK CONSTRAINT [FK_Requirement_Client]
GO
/****** Object:  ForeignKey [FK_Requirement_Client1]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Requirement_Client1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Requirement]'))
ALTER TABLE [dbo].[Requirement]  WITH CHECK ADD  CONSTRAINT [FK_Requirement_Client1] FOREIGN KEY([Tier2ClientId])
REFERENCES [dbo].[Client] ([ClientId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Requirement_Client1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Requirement]'))
ALTER TABLE [dbo].[Requirement] CHECK CONSTRAINT [FK_Requirement_Client1]
GO
/****** Object:  ForeignKey [FK_RequirementDetail_Requirement]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RequirementDetail_Requirement]') AND parent_object_id = OBJECT_ID(N'[dbo].[RequirementDetail]'))
ALTER TABLE [dbo].[RequirementDetail]  WITH CHECK ADD  CONSTRAINT [FK_RequirementDetail_Requirement] FOREIGN KEY([RequirementId])
REFERENCES [dbo].[Requirement] ([RequirementId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RequirementDetail_Requirement]') AND parent_object_id = OBJECT_ID(N'[dbo].[RequirementDetail]'))
ALTER TABLE [dbo].[RequirementDetail] CHECK CONSTRAINT [FK_RequirementDetail_Requirement]
GO
/****** Object:  ForeignKey [FK_RequirementDetail_Resource]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RequirementDetail_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[RequirementDetail]'))
ALTER TABLE [dbo].[RequirementDetail]  WITH CHECK ADD  CONSTRAINT [FK_RequirementDetail_Resource] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resource] ([ResourceId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RequirementDetail_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[RequirementDetail]'))
ALTER TABLE [dbo].[RequirementDetail] CHECK CONSTRAINT [FK_RequirementDetail_Resource]
GO
/****** Object:  ForeignKey [FK_RequirementSkillSet_Requirement]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RequirementSkillSet_Requirement]') AND parent_object_id = OBJECT_ID(N'[dbo].[RequirementSkillSet]'))
ALTER TABLE [dbo].[RequirementSkillSet]  WITH CHECK ADD  CONSTRAINT [FK_RequirementSkillSet_Requirement] FOREIGN KEY([RequirementId])
REFERENCES [dbo].[Requirement] ([RequirementId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RequirementSkillSet_Requirement]') AND parent_object_id = OBJECT_ID(N'[dbo].[RequirementSkillSet]'))
ALTER TABLE [dbo].[RequirementSkillSet] CHECK CONSTRAINT [FK_RequirementSkillSet_Requirement]
GO
/****** Object:  ForeignKey [FK_RequirementSkillSet_SkillSet]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RequirementSkillSet_SkillSet]') AND parent_object_id = OBJECT_ID(N'[dbo].[RequirementSkillSet]'))
ALTER TABLE [dbo].[RequirementSkillSet]  WITH CHECK ADD  CONSTRAINT [FK_RequirementSkillSet_SkillSet] FOREIGN KEY([SkillSetId])
REFERENCES [dbo].[SkillSet] ([SkillSetId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RequirementSkillSet_SkillSet]') AND parent_object_id = OBJECT_ID(N'[dbo].[RequirementSkillSet]'))
ALTER TABLE [dbo].[RequirementSkillSet] CHECK CONSTRAINT [FK_RequirementSkillSet_SkillSet]
GO
/****** Object:  ForeignKey [FK_Resource_Client]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resource_Client]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resource]'))
ALTER TABLE [dbo].[Resource]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Client] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([ClientId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resource_Client]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resource]'))
ALTER TABLE [dbo].[Resource] CHECK CONSTRAINT [FK_Resource_Client]
GO
/****** Object:  ForeignKey [FK_Resource_Referrer]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resource_Referrer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resource]'))
ALTER TABLE [dbo].[Resource]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Referrer] FOREIGN KEY([ReferrerId])
REFERENCES [dbo].[Referrer] ([ReferrerId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resource_Referrer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resource]'))
ALTER TABLE [dbo].[Resource] CHECK CONSTRAINT [FK_Resource_Referrer]
GO
/****** Object:  ForeignKey [FK_Resource_ResourceType]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resource_ResourceType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resource]'))
ALTER TABLE [dbo].[Resource]  WITH CHECK ADD  CONSTRAINT [FK_Resource_ResourceType] FOREIGN KEY([ResourceTypeId])
REFERENCES [dbo].[ResourceType] ([ResourceTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resource_ResourceType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resource]'))
ALTER TABLE [dbo].[Resource] CHECK CONSTRAINT [FK_Resource_ResourceType]
GO
/****** Object:  ForeignKey [FK_Resource_Vendor]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resource_Vendor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resource]'))
ALTER TABLE [dbo].[Resource]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Vendor] FOREIGN KEY([Tier1VendorId])
REFERENCES [dbo].[Vendor] ([VendorId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resource_Vendor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resource]'))
ALTER TABLE [dbo].[Resource] CHECK CONSTRAINT [FK_Resource_Vendor]
GO
/****** Object:  ForeignKey [FK_Resource_Vendor1]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resource_Vendor1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resource]'))
ALTER TABLE [dbo].[Resource]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Vendor1] FOREIGN KEY([Tier2VendorId])
REFERENCES [dbo].[Vendor] ([VendorId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resource_Vendor1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resource]'))
ALTER TABLE [dbo].[Resource] CHECK CONSTRAINT [FK_Resource_Vendor1]
GO
/****** Object:  ForeignKey [FK_ResourceDocument_DocumentType]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ResourceDocument_DocumentType]') AND parent_object_id = OBJECT_ID(N'[dbo].[ResourceDocument]'))
ALTER TABLE [dbo].[ResourceDocument]  WITH CHECK ADD  CONSTRAINT [FK_ResourceDocument_DocumentType] FOREIGN KEY([DocumentTypeId])
REFERENCES [dbo].[DocumentType] ([DocumentTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ResourceDocument_DocumentType]') AND parent_object_id = OBJECT_ID(N'[dbo].[ResourceDocument]'))
ALTER TABLE [dbo].[ResourceDocument] CHECK CONSTRAINT [FK_ResourceDocument_DocumentType]
GO
/****** Object:  ForeignKey [FK_ResourceDocument_Resource]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ResourceDocument_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[ResourceDocument]'))
ALTER TABLE [dbo].[ResourceDocument]  WITH CHECK ADD  CONSTRAINT [FK_ResourceDocument_Resource] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resource] ([ResourceId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ResourceDocument_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[ResourceDocument]'))
ALTER TABLE [dbo].[ResourceDocument] CHECK CONSTRAINT [FK_ResourceDocument_Resource]
GO
/****** Object:  ForeignKey [FK_ResourceSkillSet_Resource]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ResourceSkillSet_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[ResourceSkillSet]'))
ALTER TABLE [dbo].[ResourceSkillSet]  WITH CHECK ADD  CONSTRAINT [FK_ResourceSkillSet_Resource] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resource] ([ResourceId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ResourceSkillSet_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[ResourceSkillSet]'))
ALTER TABLE [dbo].[ResourceSkillSet] CHECK CONSTRAINT [FK_ResourceSkillSet_Resource]
GO
/****** Object:  ForeignKey [FK_ResourceSkillSet_SkillSet]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ResourceSkillSet_SkillSet]') AND parent_object_id = OBJECT_ID(N'[dbo].[ResourceSkillSet]'))
ALTER TABLE [dbo].[ResourceSkillSet]  WITH CHECK ADD  CONSTRAINT [FK_ResourceSkillSet_SkillSet] FOREIGN KEY([SkillSetId])
REFERENCES [dbo].[SkillSet] ([SkillSetId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ResourceSkillSet_SkillSet]') AND parent_object_id = OBJECT_ID(N'[dbo].[ResourceSkillSet]'))
ALTER TABLE [dbo].[ResourceSkillSet] CHECK CONSTRAINT [FK_ResourceSkillSet_SkillSet]
GO
/****** Object:  ForeignKey [FK_Timesheet_ProjectDetail]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Timesheet_ProjectDetail]') AND parent_object_id = OBJECT_ID(N'[dbo].[Timesheet]'))
ALTER TABLE [dbo].[Timesheet]  WITH CHECK ADD  CONSTRAINT [FK_Timesheet_ProjectDetail] FOREIGN KEY([ProjectDetailId])
REFERENCES [dbo].[ProjectDetail] ([ProjectDetailId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Timesheet_ProjectDetail]') AND parent_object_id = OBJECT_ID(N'[dbo].[Timesheet]'))
ALTER TABLE [dbo].[Timesheet] CHECK CONSTRAINT [FK_Timesheet_ProjectDetail]
GO
/****** Object:  ForeignKey [FK_Timesheet_Resource]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Timesheet_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[Timesheet]'))
ALTER TABLE [dbo].[Timesheet]  WITH CHECK ADD  CONSTRAINT [FK_Timesheet_Resource] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resource] ([ResourceId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Timesheet_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[Timesheet]'))
ALTER TABLE [dbo].[Timesheet] CHECK CONSTRAINT [FK_Timesheet_Resource]
GO
/****** Object:  ForeignKey [FK_VendorDocument_DocumentType]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_VendorDocument_DocumentType]') AND parent_object_id = OBJECT_ID(N'[dbo].[VendorDocument]'))
ALTER TABLE [dbo].[VendorDocument]  WITH CHECK ADD  CONSTRAINT [FK_VendorDocument_DocumentType] FOREIGN KEY([DocumentTypeId])
REFERENCES [dbo].[DocumentType] ([DocumentTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_VendorDocument_DocumentType]') AND parent_object_id = OBJECT_ID(N'[dbo].[VendorDocument]'))
ALTER TABLE [dbo].[VendorDocument] CHECK CONSTRAINT [FK_VendorDocument_DocumentType]
GO
/****** Object:  ForeignKey [FK_VendorDocument_Vendor]    Script Date: 04/27/2015 17:08:22 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_VendorDocument_Vendor]') AND parent_object_id = OBJECT_ID(N'[dbo].[VendorDocument]'))
ALTER TABLE [dbo].[VendorDocument]  WITH CHECK ADD  CONSTRAINT [FK_VendorDocument_Vendor] FOREIGN KEY([VendorId])
REFERENCES [dbo].[Vendor] ([VendorId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_VendorDocument_Vendor]') AND parent_object_id = OBJECT_ID(N'[dbo].[VendorDocument]'))
ALTER TABLE [dbo].[VendorDocument] CHECK CONSTRAINT [FK_VendorDocument_Vendor]
GO
