IF (NOT EXISTS (SELECT * FROM dbo.Employees))
BEGIN
	DECLARE @counter INT = 1
	WHILE @counter <= 20
	BEGIN
		INSERT INTO dbo.Employees (Name, Position, Status, Salary)
		VALUES ('Иванов Ю.В.', 'Менеджер', 0, 8000),
			   ('Антонов Л.П.', 'Менеджер', 0, 17000),
			   ('Лысенко А.И.', 'Менеджер', 0, 10000),
			   ('Куров А.И.', 'Архитектор', 1, 11000),
			   ('Храмцов А.М.', 'Казначей', 0, 5000),
			   ('Прокуров И.И.', 'Бухгалтер', 1, 15000),
			   ('Чеканова М.С.', 'Гендиректор', 0, 28000)
		SET @counter = @counter + 1
	END
END




