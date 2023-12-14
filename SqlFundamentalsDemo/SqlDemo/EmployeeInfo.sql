CREATE VIEW [dbo].[EmployeeInfo]
AS 
SELECT 
    E.Id AS EmployeeId,
    ISNULL(E.EmployeeName, (P.FirstName + ' ' + P.LastName)) AS EmployeeFullName, 
    (A.ZipCode + '_' + A.State + ', ' + A.City + '-' + A.Street) AS EmployeeFullAddress,
    (E.CompanyName + '(' + E.Position + ')') AS EmployeeCompanyInfo
FROM 
    [dbo].[Employee] E
    INNER JOIN [dbo].[Person] P ON E.PersonId = P.Id
    INNER JOIN [dbo].[Address] A ON E.AddressId = A.Id