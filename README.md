# EmployeeBenefitCalculator

##Technology:
WebAPI(.NET Core)
Database (MSSQL)

##Purpose:
This program is an API which interacts with database to get necessary information to calculate Employee's 
and thier benefit cost. An employee can have one or many dependents. The program is based on following assumptions

--The cost of benefits is $1000/year for each employee
--Each dependent (children and possibly spouses) incurs a cost of $500/year
--Anyone whose name starts with ‘A’ gets a 10% discount, employee or dependent
--All employees are paid $2000 per paycheck before deductions
--There are 26 paychecks in a year


How to Run the program:

1. To run this program clone the repo.
2. Build the solution on in visual studion. 
	Note: Make sure the restore the nuget packages
3. Run the application. Interact with the API controllers via Swagger.

The database connection is already handled in the API. It uses MSSQL server which is hosted in Azure.
