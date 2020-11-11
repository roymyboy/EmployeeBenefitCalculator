Use [EmployeeInformation]

Create Type [dbo].[DependentsTableType] as Table
(
	DependentID uniqueidentifier not null,
	EmployeeID uniqueidentifier not null,
	FirstName varchar(255) not null,
	LastName varchar(255) not null,
	Relationship varchar(255) not null
)

go