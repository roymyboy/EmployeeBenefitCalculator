Use [EmployeeInformation]

if OBJECT_ID('dbo.InsEmployeeData') is not null
begin 
	Print 'Dropping Stored Procedure'
	Drop procedure [InsEmployeeData]
end
else 
begin
	Print 'Prodcedure [InsEmployeeData] doesnot exist. Creating now' 
end 

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Utsav
-- Create Date: <Create Date, , >
-- Description: Inserts data Employee and Dependent Table 
-- =============================================
CREATE PROCEDURE [dbo].[InsEmployeeData]
(
	@DependentsDataTableType as [dbo].[DependentsTableType] READONLY,
    @EmployeeDataTableType as [dbo].[EmployeeTableType] READONLY
)

AS
BEGIN
	
	Insert into [dbo].[Employee]
	Select 
	EmployeeID,
	FirstName,
	LastName,
	Phone,
	Email,
	AnnualSalary,
	CostOfBenefitAnnual,
	PercentageOfDiscount,
	NumberOfDependents
	from @EmployeeDataTableType

	Insert into [dbo].[Dependents]
	select 
		DependentID,
		EmployeeID,
		FirstName,
		LastName,
		Relationship,
		PercentageOfDiscount,
		CostOfBenefitAnnual
	from @DependentsDataTableType
	
END
GO

if OBJECT_ID('dbo.InsEmployeeData') is not null
begin 
	Print 'Successfully created proocedure [InsEmployeeData]'
end

go

