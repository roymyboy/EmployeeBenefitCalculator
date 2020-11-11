Use [EmployeeInformation]


if OBJECT_ID('dbo.SelEmployeeDataByID') is not null
begin 
	Print 'Dropping Stored Procedure'
	Drop procedure [SelEmployeeDataByID]
end
else 
begin
	Print 'Prodcedure SelEmployeeDataByID doesnot exist. Creating now' 
end 



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Utsav
-- Create Date: <Create Date, , >
-- Description: Selects an Employee information 
--				by given unique employee ID
-- =============================================
CREATE PROCEDURE SelEmployeeDataByID
(
	@UniqueId varchar(255)
)

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
	where EmployeeID = @UniqueId

	select 
		DependentID,
		EmployeeID,
		FirstName,
		LastName,
		Relationship
	from [dbo].[Dependents]
	where EmployeeID = @UniqueId
END
GO

if OBJECT_ID('dbo.SelEmployeeDataByID') is not null
begin 
	Print 'Successfully created proocedure SelEmployeeDataByID'
end

go 

