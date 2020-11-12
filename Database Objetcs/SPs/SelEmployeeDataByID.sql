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
	Begin try
	
		Select 
		EmployeeID,
		FirstName,
		LastName,
		Phone,
		Email,
		AnnualSalary,
		NumberOfDependents,
		PercentageOfDiscount,
		CostOfBenefitAnnual
		from [dbo].[Employee]
		where EmployeeID = @UniqueId

		select 
			DependentID,
			EmployeeID,
			FirstName,
			LastName,
			Relationship,
			PercentageOfDiscount,
			CostOfBenefitAnnual
		from [dbo].[Dependents]
		where EmployeeID = @UniqueId
	end try
	begin catch
	 SELECT 
         ERROR_NUMBER() AS ErrorNumber
        ,ERROR_SEVERITY() AS ErrorSeverity
        ,ERROR_STATE() AS ErrorState
        ,ERROR_PROCEDURE() AS ErrorProcedure
        ,ERROR_LINE() AS ErrorLine
        ,ERROR_MESSAGE() AS ErrorMessage;
        return;
	end catch 
END
GO

if OBJECT_ID('dbo.SelEmployeeDataByID') is not null
begin 
	Print 'Successfully created proocedure SelEmployeeDataByID'
end

go 

