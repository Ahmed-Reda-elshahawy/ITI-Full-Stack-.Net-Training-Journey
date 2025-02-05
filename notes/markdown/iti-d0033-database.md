## Database Security in MS SQL Sever
Database security involves **authentication** (verifying user identity) and **authorization** (granting permissions).
### Authentication
#### Windows Authentication
- Uses Windows user accounts to connect to the database.
- Ideal for internal users (e.g., administrators).
#### SQL Server Authentication
1. **Enable Mixed Mode Authentication**:
	- Right-click the server in **SQL Server Management Studio (SSMS)** → **Properties** → **Security** tab.
	- Select **SQL Server and Windows Authentication mode**.
	- Restart the SQL Server service.
2. **Create a New Login**:
	- Go to **Security** → **Logins** → Right-click → **New Login**.
	- Enter a username and password.
	- Assign the login to a database by mapping it under the **User Mapping** tab.
3. **Check Current User**:
```sql
SELECT SUSER_NAME(); -- Returns the current login name
```
### Authorization (Permissions)
#### SCHEMA Object
- **Purpose**:
    - Logical grouping of database objects (e.g., tables, views).
    - Enhances security by assigning users to specific schemas.
- **Default Schema**: `dbo` (database owner).
#### Create and Manage Schema
**1. Create Schema**:
```sql
CREATE SCHEMA hr;
```

**2. Transfer Objects to a Schema**:
```sql
ALTER SCHEMA hr TRANSFER dbo.hr_employee;
```

**3. Create Tables in a Schema**:
```sql
CREATE TABLE hr.employee (
    id INT IDENTITY(1, 1) PRIMARY KEY,
    name VARCHAR(255)
);
```

#### Assign Permissions
1. **Grant Permissions to a User**:
```sql
GRANT SELECT, INSERT ON hr.employee TO [username];
```

2. **Revoke Permissions**:
```sql
REVOKE INSERT ON hr.employee FROM [username];
```

#### Assign Permissions Using Wizard
1. **Open SSMS and Connect to the Server**:
    - Launch SSMS and connect to your SQL Server instance.
2. **Navigate to the Database**:
    - Expand the **Databases** folder in the Object Explorer.
    - Select the database where you want to assign permissions.
3. **Go to the User**:
    - Expand the **Security** folder under the database.
    - Expand the **Users** folder.
    - Right-click the user you want to assign permissions to and select **Properties**.
4. **Open the Securables Page**:
    - In the **User Properties** window, go to the **Securables** page on the left-hand side. 
5. **Add Objects to Assign Permissions**:
    - Click **Search** to add objects (e.g., tables, views, schemas).
    - Choose one of the following options:
        - **Specific objects**: Manually select objects (e.g., tables, views).   
        - **All objects of the types**: Select all objects of a specific type (e.g., all tables).    
        - **This database**: Assign permissions at the database level.     
6. **Select the Object(s)**:
    - If you chose **Specific objects**, click **Object Types** to select the types (e.g., tables, views).  
    - Click **Browse** to select the specific objects. 
7. **Assign Permissions**:
    - In the **Permissions for [Object]** section, check the permissions you want to grant (e.g., `SELECT`, `INSERT`, `UPDATE`, `DELETE`).    
    - Use the **Grant** or **Deny** checkboxes to control access.    
8. **Save Changes**:
    - Click **OK** to save the permissions.
### Security Steps
1. **Change Authentication Mode**:
    - Enable **SQL Server and Windows Authentication** in server properties. 
2. **Restart SQL Server**:
    - Apply the changes by restarting the SQL Server service.
3. **Create a Login**:
    - Add a new login under **Security → Logins**.
4. **Create a User**:
    - Map the login to a database user under **Security → Users**.
5. **Create a Schema**:
    - Use `CREATE SCHEMA` to group objects logically. 
6. **Assign Objects to Schema**:
    - Transfer or create objects within the schema. 
7. **Add User to Schema**:
    - Assign the user to the schema for access control.
8. **Set Permissions**:
    - Grant or revoke permissions using `GRANT` or `REVOKE`. 
9. **Disconnect and Reconnect**:
    - Log out and log back in with the new login to test permissions. 
10. **Test Permissions**:
    - Run queries to verify access.

### System Admins
#### 1. Windows Admin
- The Windows user account used to install SQL Server becomes an admin by default. 
- No additional setup is required for this account.
#### 2. SA (System Administrator)
- A default SQL Server login created during installation. 
- **Important**: You must set a strong password for the `sa` account after enabling Mixed Mode Authentication.

**Steps to Set SA Password**:
1. Open **SQL Server Management Studio (SSMS)**.
2. Connect to the SQL Server instance.
3. Expand the **Security** folder in the Object Explorer.
4. Go to **Logins** → Right-click `sa` → Select **Properties**.
5. In the **General** page, enter a strong password and confirm it.
6. Go to the **Status** page and ensure **Login** is set to **Enabled**.
7. Click **OK** to save changes.
### Security: Key Points
- **Authentication**: Verify user identity (Windows or SQL Server).
- **Authorization**: Grant permissions using schemas and roles.
- **Schemas**: Group objects and control access.
- **Permissions**: Use `GRANT` and `REVOKE` to manage access.

## Synonyms in MS SQL Server
- A synonym is like a shortcut or an alias for a database object (e.g., table, view, stored procedure).
- It simplifies queries by hiding the actual object name or its location.
- Helps in managing complex or long names and makes code easier to read.
- Useful when working with multiple databases or servers.
- Created using the **`CREATE SYNONYM`** command and dropped with **`DROP SYNONYM`**.
**Example**:
```sql
CREATE SYNONYM Emp FOR EmployeeTable;
-- Now you can use `Emp` instead of `EmployeeTable` in your queries.
DROP SYNONYM Emp;
```

## Using Full Path in MS SQL Server
- The full path helps identify an object clearly: **`[SERVER].[DATABASE].[SCHEMA].[TABLE]`**.
- It’s preferred for avoiding confusion when working with multiple servers, databases, or schemas.
- Makes queries more precise and avoids errors if object names are repeated in different places.
- Example: **`[MyServer].[MyDB].[dbo].[Customers]`** ensures you’re always referencing the correct table.
>[!TIP]
> Use full paths for clarity, especially in complex environments!

## T-SQL (Transact SQL)
**T-SQL** is a SQL flavor used in Microsoft SQL Server

### `TOP` in T-SQL
`TOP` is used to limit the number of rows returned in a query result. It’s great when you only need a specific number or percentage of records.
#### `TOP`: Key Points
- Limits the number of rows returned.
- Can use a number (e.g., `TOP 10`) or a percentage (e.g., `TOP 10 PERCENT`).
```sql
SELECT TOP 5 * FROM Employees;
```

### `NEWID()` in T-SQL
`NEWID()` generates a unique value called a **GUID** (Globally Unique Identifier). It’s often used to create unique IDs for rows in a table.

#### `NEWID()`: Key Points
- Creates a unique GUID each time it’s called.
- Used for generating unique identifiers.
- Returns a value of type `UNIQUEIDENTIFIER`.

```sql
SELECT NEWID() AS UniqueID;
```
This query will return a randomly generated GUID, like: `1F2E3D4C-5B6A-7C8D-9E0F-1A2B3C4D5E6F`.
### `SELECT INTO` in T-SQL
`SELECT INTO` is used to create a new table and fill it with the results of a `SELECT` query. It’s like copying data from one table to a new table.
#### `SELECT INTO`: key Points
- Is a DDL Statement.
- Creates a new table automatically.
- Copies data from an existing table.
- The new table has the same structure as the data selected.
```sql
SELECT * INTO NewTable FROM Employees;
```

### `INSERT INTO ... SELECT` in T-SQL
This is used to insert data into an **existing table** by selecting rows from another table. It’s like copying specific data from one table to another.

#### `INSERT INTO ... SELECT`: Key Points
- Adds data to an existing table.
- Data comes from a `SELECT` query.
- Both tables must have compatible columns.
```sql
INSERT INTO EmployeesBackup (EmployeeID, Name, Salary)
SELECT EmployeeID, Name, Salary FROM Employees;
```

### `BULK INSERT` in T-SQL
`BULK INSERT` is used to load a large amount of data from a file (like a CSV or text file) into a SQL Server table quickly. It’s great for importing data in bulk.

#### `BULK INSERT`: Key Points
- Imports data from a file into a table.
- Faster than inserting row by row.
- File must match the table structure.

```sql
BULK INSERT Employees
FROM 'C:\data\employees.txt'
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n');
```
This query will insert data from the `employees.txt` file into the `Employees` table. The file uses commas (`,`) to separate columns and new lines (`\n`) for rows.

### `DELETE` vs. `DROP` vs. `TRUNCATE`
#### `DELETE`
- **What it does:** Removes specific rows from a table (or all rows if no condition is specified).
- **Logs changes:** Yes, writes to the transaction log.
- **Can have a WHERE clause:** Yes, to delete specific rows.

**Example**:
```sql
DELETE FROM Employees WHERE EmployeeID = 101;
```
This deletes the row where `EmployeeID` is 101.

#### `DROP`
- **What it does:** Deletes the entire table (structure + data).
- **Logs changes:** Yes, but only for the metadata change (not individual rows).
- **Cannot be rolled back:** Once dropped, the table is gone unless you restore from backup.

**Example**:
```sql
DROP TABLE Employees;
```
This removes the `Employees` table completely.

#### `TRUNCATE`
- **What it does:** Quickly removes all rows from a table but keeps the table structure intact.
- **Logs changes:** Minimal logging compared to `DELETE`.
- **Resets identity column:** Yes, resets auto-increment values.
- **Cannot have a WHERE clause:** Always deletes all rows.

**Example**:
```sql
TRUNCATE TABLE Employees;
```
This clears all rows in the `Employees` table.
### Ranking Functions in MS SQL Server
**Ranking functions** are used to calculate rankings or row numbers over a result set. They're especially useful for ordering and grouping data.
#### `ROW_NUMBER()`
- Assigns a unique sequential number to each row within a result set.
- **Key Points:**
    - No ties allowed. Each row gets a unique number.
    - Starts at 1 by default.

**Example**:
```sql
SELECT EmployeeID, Name, Salary,
       ROW_NUMBER() OVER (ORDER BY Salary DESC) AS RowNum
FROM Employees;
```
This assigns a unique rank to each employee based on their salary (highest to lowest).

#### `RANK()`
- Assigns a rank to each row, with gaps if there are ties.
- **Key Points:**
    - Rows with the same value get the same rank.
    - Gaps occur after tied rows.

**Example**:
```sql
SELECT EmployeeID, Name, Salary,
       RANK() OVER (ORDER BY Salary DESC) AS RankNum
FROM Employees;
```
If two employees have the same salary, they’ll share the same rank, and the next rank will skip a number.

#### `DENSE_RANK()`
- Similar to `RANK()`, but without gaps.
- **Key Points:**
    - Rows with the same value get the same rank.
    - No gaps in ranking.

**Example**:
```sql
SELECT EmployeeID, Name, Salary,
       DENSE_RANK() OVER (ORDER BY Salary DESC) AS DenseRankNum
FROM Employees;
```
If two employees have the same salary, they’ll share the same rank, but the next rank won’t skip a number.

#### `NTILE(num)`
- Divides rows into a specified number of groups (or "buckets").
- **Key Points:**
    - Distributes rows as evenly as possible.
    - Useful for percentiles or dividing data into categories.

**Example**:
```sql
SELECT EmployeeID, Name, Salary,
       NTILE(4) OVER (ORDER BY Salary DESC) AS Quartile
FROM Employees;
```
This divides employees into 4 groups based on their salary (e.g., top 25%, next 25%, etc.).

#### Using `PARTITION BY` with Ranking Functions
The **`PARTITION BY`** clause is used to divide the result set into partitions (groups) and apply the ranking function independently to each partition. It’s like resetting the ranking or numbering for each group.

##### `PARTITION BY`: Key Points
- Splits the data into groups based on one or more columns.
- Ranking functions are applied within each group separately.
- Works with all ranking functions: `ROW_NUMBER()`, `RANK()`, `DENSE_RANK()`, and `NTILE()`.

**Example: `RANK()` with `PARTITION BY`**:

```sql
-- Rank employees by salary within each department, with ties
SELECT EmployeeID, Department, Name, Salary,
       RANK() OVER (PARTITION BY Department ORDER BY Salary DESC) AS RankNum
FROM Employees;

-- If two employees in the same department have the same salary, they’ll share the same rank, and the next rank will skip a number.
```

**Example: `DENSE_RANK()` with `PARTITION BY`**:

```sql
-- Rank employees by salary within each department, with ties but no gaps
SELECT EmployeeID, Department, Name, Salary,
       DENSE_RANK() OVER (PARTITION BY Department ORDER BY Salary DESC) AS DenseRankNum
FROM Employees;

-- If two employees in the same department have the same salary, they’ll share the same rank, but the next rank won’t skip a number.
```

**Example: `NTILE(num)` with `PARTITION BY`**:

```sql
-- Divide employees into quartiles based on salary within each department
SELECT EmployeeID, Department, Name, Salary,
       NTILE(4) OVER (PARTITION BY Department ORDER BY Salary DESC) AS Quartile
FROM Employees;

-- This divides employees in each department into 4 groups (quartiles) based on their salary
```

### `MERGE` in T-SQL
The **`MERGE`** statement is used to perform multiple operations (`INSERT`, `UPDATE`, `DELETE`) on a target table based on data from a source table, all in a single statement. It’s especially useful for synchronizing data between two tables.

#### `MERGE`: key Points
- Combines **INSERT** , **UPDATE** , and **DELETE** logic into one operation.
- Compares rows in a **target table** with rows in a **source table** .
- Uses a **MATCHED** condition to decide what action to take (update, insert, or delete).
- Helps keep data consistent between tables.

#### `MERGE` Basic Syntax
```sql
MERGE TargetTable AS TARGET
USING SourceTable AS SOURCE
ON (TARGET.KeyColumn = SOURCE.KeyColumn)
WHEN MATCHED THEN
    UPDATE SET TARGET.Column1 = SOURCE.Column1,
               TARGET.Column2 = SOURCE.Column2
WHEN NOT MATCHED BY TARGET THEN
    INSERT (Column1, Column2)
    VALUES (SOURCE.Column1, SOURCE.Column2)
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
```
1. **`ON` Clause:** Defines how rows in the target and source tables are matched.
2. **`WHEN MATCHED`:** Updates rows in the target table if they exist in both tables.
3. **`WHEN NOT MATCHED BY TARGET`:** Inserts rows from the source table that don’t exist in the target table.
4. **`WHEN NOT MATCHED BY SOURCE`:** Deletes rows in the target table that don’t exist in the source table.

**Example**:

Suppose we have two tables: `Employees` (target) and `NewEmployees` (source). We want to synchronize them.
```sql
MERGE Employees AS TARGET
USING NewEmployees AS SOURCE
ON (TARGET.EmployeeID = SOURCE.EmployeeID)
WHEN MATCHED AND TARGET.Salary <> SOURCE.Salary THEN
    UPDATE SET TARGET.Salary = SOURCE.Salary
WHEN NOT MATCHED BY TARGET THEN
    INSERT (EmployeeID, Name, Salary)
    VALUES (SOURCE.EmployeeID, SOURCE.Name, SOURCE.Salary)
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
```
- **Matched Rows:** Update salary if it differs.
- **Unmatched Rows in Target:** Insert new employees.
- **Unmatched Rows in Source:** Delete employees no longer in the source.