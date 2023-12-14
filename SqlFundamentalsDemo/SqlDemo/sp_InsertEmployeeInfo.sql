CREATE PROCEDURE sp_InsertEmployeeInfo
    @EmployeeName NVARCHAR(100) = NULL,
    @FirstName NVARCHAR(50) = NULL,
    @LastName NVARCHAR(50) = NULL,
    @CompanyName NVARCHAR(20),
    @Position NVARCHAR(30) = NULL,
    @Street NVARCHAR(50),
    @City NVARCHAR(20) = NULL,
    @State NVARCHAR(50) = NULL,
    @ZipCode NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MaxCompanyNameLength INT = 20;
    
    IF ((@EmployeeName IS NULL OR LTRIM(RTRIM(@EmployeeName)) = '') AND (@FirstName IS NULL OR LTRIM(RTRIM(@FirstName)) = '') AND (@LastName IS NULL OR LTRIM(RTRIM(@LastName)) = ''))
        THROW 51000, 'At least one of the fields (EmployeeName or FirstName or LastName) must be provided.', 1;
            
    IF (LEN(@CompanyName) > @MaxCompanyNameLength)
        SET @CompanyName = LEFT(@CompanyName, @MaxCompanyNameLength);
        
    DECLARE @AddressId int;
    INSERT INTO dbo.Address(Street, City, State, ZipCode)
    VALUES (@Street, @City, @State, @ZipCode);
    SET @AddressId = SCOPE_IDENTITY();
    
    DECLARE @PersonId int;
    INSERT INTO dbo.Person(FirstName, LastName)
    VALUES (@FirstName, @LastName);
    SET @PersonId = SCOPE_IDENTITY();
    
    INSERT INTO dbo.Employee(AddressId, PersonId, CompanyName, Position, EmployeeName)
    VALUES (@AddressId, @PersonId, @CompanyName, @Position, @EmployeeName);

END