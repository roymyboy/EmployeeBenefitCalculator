Create table Employee
(
    EmployeeID uniqueIdentifier Primary Key,
    FirstName varchar(255) Not Null,
    LastName varchar(255) Not null,
    Phone varchar(255) null,
    Email varchar(255) null,
    AnnualSalary decimal not null,
	CostOfBenefitAnnual decimal not null,
	PercentageOfDiscount decimal not null,
    NumberOfDependents int null
)