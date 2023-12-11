INSERT INTO [dbo].[Person] (Id, FirstName, LastName) 
VALUES 
(1, 'John', 'Doe'),
(2, 'Jane', 'Smith')

INSERT INTO [dbo].[Address] (Id, Street, City, State, ZipCode) 
VALUES 
(1, 'Baker Street', 'London', 'England', 'NW1 6XE'),
(2, 'Broadway', 'New York', 'NY', '10012')

INSERT INTO [dbo].[Employee] (Id, AddressId, PersonId, CompanyName, Position, EmployeeName)
VALUES 
(1, 1, 1, 'Company A', 'Manager', 'John Doe'),
(2, 2, 2, 'Company B', 'Assistant', 'Jane Smith')

INSERT INTO [dbo].[Company] (Id, Name, AddressId)
VALUES 
(1, 'Company A', 1),
(2, 'Company B', 2)