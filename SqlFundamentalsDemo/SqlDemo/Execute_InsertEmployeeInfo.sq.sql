USE [SQLDemo]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[sp_InsertEmployeeInfo]
		@EmployeeName = N'Michael Brown',
		@FirstName = N'Michael',
		@LastName = N'Brown',
		@CompanyName = N'Company A',
		@Position = N'Developer',
		@Street = N'Broadway'

SELECT	@return_value as 'Return Value'

GO
