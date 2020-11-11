Use [EmployeeInformation]

if OBJECT_ID('dbo.SelEmployeeData') is not null
begin 
	Print 'Dropping Stored Procedure'
	Drop procedure [SelEmployeeData]
end
else 
begin
	Print 'Prodcedure SelEmployeeData doesnot exist. Creating now' 
end 


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Utsav
-- Create Date: <Create Date, , >
-- Description: Selects all the data in Employee Table 
-- =============================================
CREATE PROCEDURE SelEmployeeData

AS
BEGIN
	
	Select 
	EmployeeID,
	FirstName,
	LastName,
	Phone,
	Email,
	AnnualSalary,
	NumberOfDependents
	from [dbo].[Employee]

	select 
		DependentID,
		dpndt.EmployeeID,
		dpndt.FirstName,
		dpndt.LastName,
		Relationship
	from [dbo].[Dependents] dpndt
	inner join [dbo].[Employee] emp on dpndt.EmployeeID = emp.EmployeeID
END
GO

if OBJECT_ID('dbo.SelEmployeeData') is not null
begin 
	Print 'Successfully created proocedure SelEmployeeData'
end

go 

