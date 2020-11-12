Use [EmployeeInformation]

Create Type [dbo].[EmployeeTableType] as Table
(
	EmployeeID uniqueidentifier not null,
	FirstName varchar(255) not null,
	LastName varchar(255) not null,
	Phone varchar(255) null,
	Email varchar(255) null,
	AnnualSalary decimal not null,
	CostOfBenefitAnnual decimal not null,
	PercentageOfDiscount decimal not null,
	NumberOfDependents int null
)

go
