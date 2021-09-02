# Readme

## How to Run
1. Open the solution in Visual Studio.
2. If Microsoft SQL Server 2017 LocalDB is installed, you may add the AE\Database\AE.mdf file to Data Connections in Server Explorer and update the connection string in appsettings.json.
3. You may also choose to use a brand new database by updating the connection string first and then run "Update-Database" in Visual Studio Package Manager Console.
4. If a new database is used, you may add ports by following SQL statements:
```
INSERT INTO [dbo].[Port] ([Id], [Name], [Latitude], [Longitude]) VALUES (N'AUSYD', N'Sydney', -33.851505, 151.19885)
INSERT INTO [dbo].[Port] ([Id], [Name], [Latitude], [Longitude]) VALUES (N'BRIQI', N'ITAQUI', -2.583817, -44.366225)
INSERT INTO [dbo].[Port] ([Id], [Name], [Latitude], [Longitude]) VALUES (N'GBFXT', N'Felixstowe', 51.959055, 1.2981075)
INSERT INTO [dbo].[Port] ([Id], [Name], [Latitude], [Longitude]) VALUES (N'HKHKG', N'Hong Kong', 22.287945, 114.18135)
INSERT INTO [dbo].[Port] ([Id], [Name], [Latitude], [Longitude]) VALUES (N'NLRTM', N'Rotterdam', 51.943306, 4.14181232)
INSERT INTO [dbo].[Port] ([Id], [Name], [Latitude], [Longitude]) VALUES (N'USMSY', N'New Orleans', 29.95669, -90.14589)
```
4. If a new database is used, the following SQL statements should be executed before running the tests.
```
INSERT INTO [dbo].[Ship] ([Id], [Name], [Latitude], [Longitude], [Velocity]) VALUES (N'ship1', N'AllZero', 0, 0, 0)
INSERT INTO [dbo].[Ship] ([Id], [Name], [Latitude], [Longitude], [Velocity]) VALUES (N'ship2', N'HKG', 22.622041, 114.0325996, 10.1314)
```

## API
For the information of endpoints, please run the project and refer to the swagger page.
Please note that the velocity unit is knot and the distance unit is nautical mile.