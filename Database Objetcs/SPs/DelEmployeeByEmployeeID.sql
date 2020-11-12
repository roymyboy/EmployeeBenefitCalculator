Use [EmployeeInformation]


if OBJECT_ID('dbo.DelEmployeeByEmployeeID') is not null
begin 
	Print 'Dropping Stored Procedure'
	Drop procedure [DelEmployeeByEmployeeID]
end
else 
begin
	Print 'Prodcedure DelEmployeeByEmployeeID doesnot exist. Creating now' 
end 



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Utsav
-- Create Date: <Create Date, , >
-- Description: delete an Employee information 
--				by given unique employee ID
-- =============================================
CREATE PROCEDURE DelEmployeeByEmployeeID
(
	@UniqueId varchar(255)
)

AS
BEGIN
	Begin try
	
		delete 
		from [dbo].[Employee]
		where EmployeeID = @UniqueId

		delete 
		
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

if OBJECT_ID('dbo.DelEmployeeByEmployeeID') is not null
begin 
	Print 'Successfully created proocedure DelEmployeeByEmployeeID'
end

go 

