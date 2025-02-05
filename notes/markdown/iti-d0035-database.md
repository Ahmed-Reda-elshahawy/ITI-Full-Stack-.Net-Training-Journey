# 🔖 ITI - D0035 - Database

## Indexes in MS SQL Server

### What is an Index?

- SQL Server **stores data in a heap by default**, meaning **rows are stored randomly** inside **pages**.
- To **find data without an index**, SQL Server does a **Table Scan** → meaning it **checks every page one by one** (which is slow).
- **Indexes help speed this up** → Instead of scanning everything, SQL Server **uses an index** (like a book's table of contents) to find data faster.
- SQL Server **uses a B+ Tree** (a sorted data structure) for indexes.

### Types of Indexes

1. **Clustered Index** → A table can have **only one** clustered index.
2. **Non-Clustered Index** → A table can have **many** non-clustered indexes.

#### 1. Clustered Index

- A table can have **only one** clustered index because it **controls how the data is physically stored**.
- By default, SQL Server **stores data in a heap (unsorted)** unless a **Clustered Index** exists.
- Usually, the **Primary Key** is a **Clustered Index** by default.
- If a table **already exists without a Primary Key**, you can manually create a **Clustered Index** on a column.
- If you later **add a Primary Key**, SQL Server **creates a Non-Clustered Index** on it instead.

##### How does Clustered Index Work?

A **Clustered Index** is a **B+ Tree** with **3 levels**:

1. **Top & Middle Levels** → Store **index pages** (like a table of contents).
2. **Bottom Level** → Stores **actual data**, sorted based on the indexed column.

##### Syntax to Create a Clustered Index

```sql
CREATE CLUSTERED INDEX Index_Name
ON Table_Name (Column_Name);
```

#### 2. Non-Clustered Index

- A table can have **many** Non-Clustered Indexes.
- If a table **doesn’t have a Clustered Index**, it remains a **heap** (data stored in random order).
- **B+ Tree stores only index pages**, which **point to the actual data** stored separately.
- **Does NOT change how data is physically stored**, unlike a Clustered Index.

##### Syntax to Create a Non-Clustered Index

```sql
CREATE NONCLUSTERED INDEX Index_Name
ON Table_Name (Column_Name);
```

### Checking if an Index is Used

- Use **"Display Estimated Execution Plan"** in SQL Server to check if an index is being used by a query.

### Indexes: Key Points

- **UNIQUE Constraint** → Automatically creates a **Non-Clustered Index** by default.
- **Heap tables (tables without a Clustered Index) are slower** when searching for data.
- You can **view existing indexes** by right-clicking the table in SQL Server and checking the **Indexes section**.

## Cursors in MS SQL Server

### What is a Cursor?

- A **Cursor** is a way to **loop through** the rows of a **Result Set** (the output of a `SELECT` query).
- It allows **row-by-row processing**, unlike a normal `SELECT`, which returns all data at once.

### Steps to Use a Cursor

1. **Declare the Cursor** and assign it a `SELECT` query.
2. **Declare Variables** to store values from the `SELECT` columns.
3. **Open the Cursor** → Moves the pointer to the first row.
4. **Fetch the First Row** → Store its data in variables.
5. **Loop (`WHILE @@FETCH_STATUS = 0`)** → Keep fetching the next row.
6. **Close the Cursor** after the loop ends.
7. **Deallocate the Cursor** to free memory.

### Types of Cursors

#### 1. READ ONLY Cursor

- **Cannot modify data** → Just for reading records.
- **Faster** than an `UPDATE` cursor.

#### 2. UPDATE Cursor

- **Allows modifications** → You can update the current row using `WHERE CURRENT OF cursor_name`.
- Used when you **need to update or delete** rows **one by one**.

### Example 1: Using an UPDATE Cursor

This example **updates salaries** based on their current values.

```sql
DECLARE C1 CURSOR FOR
SELECT Salary FROM T_Employee
FOR UPDATE;

DECLARE @salary INT;

OPEN C1;
FETCH NEXT FROM C1 INTO @salary;

WHILE @@FETCH_STATUS = 0
BEGIN
    IF @salary < 3000
        UPDATE T_Employee
        SET Salary = Salary * 1.1
        WHERE CURRENT OF C1;
    ELSE
        UPDATE T_Employee
        SET Salary = Salary * 1.2
        WHERE CURRENT OF C1;

    FETCH NEXT FROM C1 INTO @salary;
END;

CLOSE C1;
DEALLOCATE C1;
```

### Example 2: Using a READ ONLY Cursor

This example **retrieves department names and manager names**, but does **not modify data**.

```sql
DECLARE C1 CURSOR FOR
SELECT d.Dept_Name, i.Ins_Name
FROM Department d
INNER JOIN Instructor i ON d.Dept_Manager = i.Ins_Id
FOR READ ONLY;

DECLARE @dept_name VARCHAR(30), @mgr_name VARCHAR(30);

OPEN C1;
FETCH NEXT FROM C1 INTO @dept_name, @mgr_name;

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Department: ' + @dept_name + ', Manager: ' + @mgr_name;
    FETCH NEXT FROM C1 INTO @dept_name, @mgr_name;
END;

CLOSE C1;
DEALLOCATE C1;
```

### Cursors: Key Points

- **Use Cursors Only When Necessary** → They are **slower** than `SET BASED` operations (`UPDATE`, `DELETE`, `MERGE`).
- **Use `FOR UPDATE` for modifying** data and **`FOR READ ONLY`** for reading only.
- **Always CLOSE and DEALLOCATE** a cursor to free memory.
- **Avoid Cursors on Large Data Sets** → They **consume more resources** and can **slow down performance**.

## Advanced Grouping & Pivoting in MS SQL Server

### 1. ROLLUP (Make Subtotals and a Grand Total)

- It helps to **summarize data step by step**.
- It gives **subtotals** and then a **final total**.

**Example**:

```sql
SELECT Department, JobTitle, SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY ROLLUP (Department, JobTitle);
```

**What happens here?**

- Groups data by `Department` and `JobTitle`.
- **Adds a subtotal** for each `Department` (ignores `JobTitle`).
- **Adds a grand total** row for all departments.

### 2. CUBE (Make All Possible Subtotals)

- It **creates all kinds of subtotals** for the given columns.
- It’s like `ROLLUP`, but **more powerful** because it makes more subtotal combinations.

**Example**:

```sql
SELECT Department, JobTitle, SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY CUBE (Department, JobTitle);
```

**What happens here?**

- Groups by `(Department, JobTitle)`.
- **Creates subtotals for each Department**.
- **Creates subtotals for each JobTitle**.
- **Adds a grand total row** (everything combined).

> **CUBE gives more subtotals than ROLLUP** because it considers all possibilities.

### 3. GROUPING SETS (Pick Your Own Subtotals)

- It **lets you choose exactly which subtotals you want**.
- More **flexible** than `ROLLUP` or `CUBE`.

**Example**:

```sql
SELECT Department, JobTitle, SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY GROUPING SETS (
    (Department, JobTitle), -- Normal grouping
    (Department),           -- Subtotal for each department
    (JobTitle),             -- Subtotal for each job title
    ()                      -- Grand total for everything
);
```

### 4. PIVOT (Turn Rows into Columns)

- It transforms row values into column names.
- It makes the data easier to read for reports.

#### Example: Convert Sales Data (Rows → Columns)

**Before PIVOT:**

| Year | Product | Sales |
| ---- | ------- | ----- |
| 2023 | Laptop  | 10000 |
| 2023 | Phone   | 15000 |
| 2024 | Laptop  | 12000 |
| 2024 | Phone   | 17000 |

**After PIVOT:**

| Year | Laptop | Phone |
| ---- | ------ | ----- |
| 2023 | 10000  | 15000 |
| 2024 | 12000  | 17000 |

```sql
SELECT *
FROM (SELECT Year, Product, Sales FROM SalesData) AS SourceTable
PIVOT (
    SUM(Sales)
    FOR Product IN (Laptop, Phone)
) AS PivotTable;
```

**What happens here?**

- The `Product` values (`Laptop`, `Phone`) become **column names**.
- Their `Sales` values go under them.

### 5. UNPIVOT (Turn Columns into Rows)

- It does the opposite of PIVOT.
- It turns column names back into row values.

#### Example: Convert Columns to Rows

**Before UNPIVOT:**

| Year | Laptop | Phone |
| ---- | ------ | ----- |
| 2023 | 10000  | 15000 |
| 2024 | 12000  | 17000 |

**After UNPIVOT:**

| Year | Product | Sales |
| ---- | ------- | ----- |
| 2023 | Laptop  | 10000 |
| 2023 | Phone   | 15000 |
| 2024 | Laptop  | 12000 |
| 2024 | Phone   | 17000 |

```sql
SELECT Year, Product, Sales
FROM SalesData
UNPIVOT (
    Sales FOR Product IN (Laptop, Phone)
) AS UnpivotTable;
```

**What happens?**

- The **column names (`Laptop`, `Phone`) turn into row values** under `Product`.

## Views in MS SQL Server

### What is a View?

- A **View** is a **saved `SELECT` query** that **acts like a virtual table**.
- It **simplifies complex queries** by saving them as a reusable object.
- Views help in **hiding database structure** and **restricting access** to sensitive data.
- **Views do not store data** (except Indexed Views).
- Views **cannot accept parameters** like stored procedures.
- Views **cannot contain DML (`INSERT`, `UPDATE`, `DELETE`) statements** inside their body.

### Types of Views in MS SQL Server

#### 1. Standard View (Virtual Table)

- **Just a saved query** → Runs fresh every time you use it.
- **Does not store data**, only structure and query logic.

#### 2. Partitioned View

- **Combines data from multiple tables across different databases or servers**.
- **Used in distributed databases** to merge data from multiple locations.

#### 3. Indexed View (Materialized View)

- **Stores actual data on disk** (unlike a standard view).
- Improves performance by **precomputing and storing query results**.
- Needs to be **refreshed manually or periodically** to sync with original data.

### Syntax to Create a View

```sql
CREATE VIEW View_Name AS
SELECT column1, column2
FROM Table_Name
WHERE condition
[WITH ENCRYPTION] -- Hides view definition from users
[WITH CHECK OPTION]; -- Prevents modifications that violate the WHERE clause
```

To **view the query behind a view**:

```sql
SP_HELPTEXT View_Name;
```

To **drop a view**:

```sql
DROP VIEW View_Name;
```

### DML Operations on Views

✅ **You can use `INSERT`, `UPDATE`, and `DELETE` on views**, but there are **rules**:

1. **If the view is based on a single table** → You can modify data freely.
2. **If the view joins multiple tables**:
   - ✅ **UPDATE** → Works **only if modifying a single table** in the view.
   - ❌ **DELETE** → **Not allowed** if the view has multiple tables.
   - ❌ **INSERT** → **Not allowed** unless it affects only one table.
3. **To modify multiple tables, split operations into multiple queries**.

### Views: Key Points

- Views **simplify queries** by acting as **virtual tables**.
- **Standard Views do not store data**; they fetch fresh data every time.
- **Indexed Views store data** on disk and **must be refreshed**.
- **DML on Views follows strict rules** (single-table updates allowed, multi-table changes restricted).
- **Partitioned Views** help in **combining distributed databases**.
- Use **"WITH CHECK OPTION"** to **prevent unwanted modifications**.

## Backups in MS SQL Server

- A **backup** is a copy of your database that you can use if something goes wrong.
- It **protects your data** from accidental deletion, crashes, or corruption.

### Syntax of Full Backup

```sql
BACKUP DATABASE MyDatabase
TO DISK = 'C:\Backup\MyDatabase_Full.bak';
```

### Syntax of Restoring Full Backup

```sql
RESTORE DATABASE MyDatabase
FROM DISK = 'C:\Backup\MyDatabase_Full.bak'
WITH REPLACE;
```

## Jobs in MS SQL Server

- A **job** is a task that runs **automatically** in SQL Server.
- It helps with things like **backups, updates, and reports** without needing manual work.
- SQL Server Agent runs jobs at scheduled times.

### Create a Job Using Wizard (Easy Way)

1. Open **SQL Server Management Studio (SSMS)**.
2. Expand **SQL Server Agent** → Right-click **Jobs** → Click **New Job**.
3. In **General**, enter the **Job Name**.
4. In **Steps**, click **New**, write your SQL command, and save.
5. In **Schedules**, click **New**, set when the job should run, and save.
6. Click **OK** → Job is ready!
