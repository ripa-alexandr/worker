IF (NOT EXISTS (SELECT * FROM dbo.Employees))
BEGIN
	DECLARE @counter INT = 1
	WHILE @counter <= 20
	BEGIN
		INSERT INTO dbo.Employees (Name, Position, Status, Salary)
		VALUES ('������ �.�.', '��������', 0, 8000),
			   ('������� �.�.', '��������', 0, 17000),
			   ('������� �.�.', '��������', 0, 10000),
			   ('����� �.�.', '����������', 1, 11000),
			   ('������� �.�.', '��������', 0, 5000),
			   ('�������� �.�.', '���������', 1, 15000),
			   ('�������� �.�.', '�����������', 0, 28000)
		SET @counter = @counter + 1
	END
END




