CREATE TRIGGER [trg_InsertEmployee]
	ON [dbo].[Employee]
	FOR DELETE, INSERT, UPDATE
	AS
	BEGIN
		SET NOCOUNT ON
		
		DECLARE @InsertedAddressId int;
		SELECT @InsertedAddressId = AddressId FROM inserted;

		DECLARE @CompanyName nvarchar(20);
		SELECT @CompanyName = CompanyName FROM inserted;

		DECLARE @AddressId int;
		INSERT INTO dbo.Address (Street, City, State, ZipCode)
			SELECT Street, City, State, ZipCode FROM dbo.Address WHERE Id = @InsertedAddressId;
		SET @AddressId = SCOPE_IDENTITY();
    
		INSERT INTO dbo.Company (Name, AddressId) 
		VALUES (@CompanyName, @AddressId);
	END
