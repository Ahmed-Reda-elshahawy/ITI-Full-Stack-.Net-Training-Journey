# 🔖 ITI - D0036 - Database

## How SQL Server Engine Handles a Query

1. **Query Submission** → You write and run a SQL query.
2. **Parsing & Syntax Check** → SQL Server **checks for errors** (like missing commas or wrong keywords).
3. **Binding & Optimization**
   - SQL Server **looks at metadata** (table structure, indexes, etc.).
   - It **creates a Query Tree** (a step-by-step breakdown of your query).
   - The **Optimizer** finds the **fastest way** to run your query.
4. **Execution Plan Generation**
   - SQL Server **chooses the best execution plan** (it decides **how** to get the data quickly).
   - It might use **indexes, table scans, joins**, etc.
5. **Query Execution**
   - SQL Server **runs the chosen execution plan** and fetches the data.
6. **Results Return** → The output is **sent back to you**.

## Stored Procedures

### What is Stored Procedures

- A **Stored Procedure** is a **saved SQL script** that you can run **anytime**.
- It helps **reuse** queries and makes **execution faster**.
- It **reduces** code repetition and **improves security**.

### Stored Procedures Syntax

Stored procedures in SQL Server **can accept parameters** to make them more flexible. There are **two types of parameters**:  
2. **Input Parameters** – Used to pass values **into** the procedure.  
3. **Output Parameters** – Used to **return** values **from** the procedure.

##### Key Points About `RETURN` in Stored Procedures

- **Stops procedure execution immediately**.
- **Returns an integer value** (commonly used for success/failure status).
- **Cannot return a table or dataset** (use `OUTPUT` parameters instead).

**Example**:

```sql
CREATE PROCEDURE GetEmployeeDetails
    @EmpID INT,              -- Input Parameter
    @EmpName VARCHAR(100) OUTPUT -- Output Parameter
AS
BEGIN
    -- Fetch employee details
    SELECT @EmpName = Name FROM Employees WHERE EmployeeID = @EmpID;

    -- If no employee found, return 1 (error)
    IF @EmpName IS NULL
    BEGIN
        RETURN 1;
    END

    -- Return 0 for success
    RETURN 0;
END;
```

#### Create a Stored Procedure

```sql
CREATE PROCEDURE GetAllEmployees
AS
BEGIN
    -- Fetch all employees
    SELECT * FROM Employees;

    -- Return a status code (0 means success)
    RETURN 0;
END;
```

#### Running a Stored Procedure

```sql
EXEC GetAllEmployees;
```

#### Stored Procedure with Parameters

```sql
CREATE PROCEDURE GetEmployeeByID @EmpID INT
AS
BEGIN
    SELECT * FROM Employees WHERE EmployeeID = @EmpID;
END;
```

#### Running With Parameters

```sql
EXEC GetEmployeeByID @EmpID = 5;
```

#### View The Query behind a Stored Procedure

```sql
SP_HELPTEXT Procedure_Name;
```

#### Deleting a Stored Procedure

```sql
DROP PROCEDURE GetEmployeeByID;
```

#### Save Stored Procedure into a Table

4. **Create a Table** to store the output.
5. **Use `INSERT INTO ... EXEC`** to run the procedure and save results.

**Example**:

```sql
-- create stored procedure
CREATE PROCEDURE GetEmployees
AS
BEGIN
    SELECT EmployeeID, Name, Salary FROM Employees;
END;

-- create a table
CREATE TABLE EmployeeBackup (
    EmployeeID INT,
    Name VARCHAR(100),
    Salary DECIMAL(10,2)
);

-- execute stored procedure and save data
INSERT INTO EmployeeBackup
EXEC GetEmployees;
```

#### Using Stored Procedure to Create Dynamic Queries

Stored procedures **can build and execute dynamic SQL queries** using the `EXEC` or `sp_executesql` command. This is useful when:

- The query structure **changes based on input**.
- You need to **filter data dynamically**.
- You want to **optimize performance** by avoiding hardcoded queries.

```sql
CREATE PROCEDURE GetEmployeesByDepartment
    @DeptName VARCHAR(100)
AS
BEGIN
    DECLARE @SQL NVARCHAR(MAX);

    -- Build the dynamic SQL query
    SET @SQL = 'SELECT * FROM Employees WHERE Department = ''' + @DeptName + '''';

    -- Execute the query
    EXEC (@SQL);
END;

EXEC GetEmployeesByDepartment 'HR';
```

```sql
CREATE PROCEDURE GetEmployeesByDepartmentSafe
    @DeptName VARCHAR(100)
AS
BEGIN
    DECLARE @SQL NVARCHAR(MAX);

    -- Build the query with parameters
    SET @SQL = 'SELECT * FROM Employees WHERE Department = @Dept';

    -- Execute with parameters
    EXEC sp_executesql @SQL, N'@Dept VARCHAR(100)', @DeptName;
END;
```

### Difference between Stored Procedure vs. Function vs. View

| Feature                  | **Stored Procedure**                  | **Function** | **View**                               |
| ------------------------ | ------------------------------------- | ------------ | -------------------------------------- |
| Returns Data?            | ✅ Yes (Table or Value)               | ✅ Yes       | ✅ Yes                                 |
| Can Modify Data?         | ✅ Yes (`INSERT`, `UPDATE`, `DELETE`) | ❌ No        | ✅ Yes (If certain conditions are met) |
| Can Have Parameters?     | ✅ Yes                                | ✅ Yes       | ❌ No                                  |
| Can Be Used in `SELECT`? | ❌ No                                 | ✅ Yes       | ✅ Yes                                 |

### Stored Procedure: Key Points

- **Faster execution** because SQL Server **caches the query plan**.
- **More secure** by preventing **SQL injection** and hiding **database details**.
- **Avoids code repetition** by reusing the same SQL logic.
- **Supports parameters** to make it **more flexible**.
- **Can run any SQL statement** including **DDL (create, alter, drop)**, **DML (insert, update, delete)**, and **DQL (select)**.

## Triggers

A **Trigger** is a special **automatic action** that runs **when something happens** in a table (like `INSERT`, `UPDATE`, or `DELETE`). It helps in **keeping data safe, logging changes, or enforcing rules**.

### Types of Triggers

1. **AFTER Trigger** (default) – Runs **after** an `INSERT`, `UPDATE`, or `DELETE`.
2. **INSTEAD OF Trigger** – Runs **instead of** the actual `INSERT`, `UPDATE`, or `DELETE`.
3. **DDL Trigger** – Runs on **database changes** (`CREATE`, `DROP`, `ALTER`).

### Triggers Examples

#### Example: AFTER INSERT Trigger

This trigger **logs new employee data** into another table after an `INSERT`.

```sql
CREATE TRIGGER AfterInsertEmployee
ON Employees
AFTER INSERT
AS
BEGIN
    INSERT INTO EmployeeLog (EmpID, ActionType, ActionDate)
    SELECT EmployeeID, 'INSERT', GETDATE() FROM inserted;
END;
```

- **`inserted` table** → Holds new rows being inserted.
- **`deleted` table** → Holds old rows being updated or deleted.

#### Example: INSTEAD OF DELETE Trigger

Stops deleting employees and marks them as "Inactive" instead.

```sql
CREATE TRIGGER PreventDeleteEmployee
ON Employees
INSTEAD OF DELETE
AS
BEGIN
    UPDATE Employees
    SET IsActive = 0
    WHERE EmployeeID IN (SELECT EmployeeID FROM deleted);
END;
```

### Triggers: Key Points

- **Triggers run automatically** when data changes in a table.
- **Triggers inherit the schema** of the table they are created on.
- Use **`inserted` and `deleted` tables** to access old and new row data.
- **Triggers cannot return values** like functions or stored procedures.
- Too many triggers **can slow down performance**, so use them wisely.
- **DDL triggers** work at the database level (e.g., prevent table drops).

### Runtime Triggers

A **runtime trigger** is **not an actual trigger** like `AFTER` or `INSTEAD OF` triggers. Instead, it refers to **an action that runs at the time of execution (runtime) without being predefined in the database**.

In SQL Server, the **`OUTPUT` clause** can act like a **runtime trigger** because it captures the affected rows and returns data at runtime.

#### Example: Runtime Trigger using OUTPUT Clause

```sql
DELETE FROM Instructor
OUTPUT GETDATE() AS DeletionTime, deleted.*
WHERE Ins_Id = 44;
```

- **`OUTPUT GETDATE() AS DeletionTime`** → Captures the exact time of deletion.
- **`deleted.*`** → Returns the deleted row data.
- Unlike a regular trigger, this runs only when the query executes.

## Backups in MS SQL Server

_Review [Backups in Ms SQL Server](./iti-d0035-database.md#backups-in-ms-sql-server)_

### Types of Backups

- [Full Backup](./iti-d0035-database.md#syntax-of-full-backup)
- Differential Backup
- Transactional Log Backup

#### Differential Backup

- This only backs up the changes made after the last full backup.
- It is faster and smaller than a full backup.

**Example**:

```sql
BACKUP DATABASE MyDatabase
TO DISK = 'C:\Backup\MyDatabase_Diff.bak'
WITH DIFFERENTIAL;
```

- Use this when you want to save space and time but still keep recent changes.

#### Transaction Log Backup (Save Ongoing Changes)

- This backs up all recent transactions (`INSERT`, `UPDATE`, `DELETE`).
- It helps to recover data up to the exact second before a crash.
- You must have a full backup first before using this.

**Example**:

```sql
BACKUP LOG MyDatabase
TO DISK = 'C:\Backup\MyDatabase_Log.trn';
```

- Use this for point-in-time recovery (getting back lost data exactly as it was).
