# 🔖 ITI - D0034 - Database

## Variables in MS SQL Server

### 1. Local Variables

- **Local variables** are used within the scope of a batch, stored procedure, or function.
- They are declared using the **`DECLARE`** statement and must start with the **`@`** symbol.

**Syntax**:

```sql
DECLARE @x INT; -- Declare a local variable
SET @x = 10;    -- Assign a value to the variable
SELECT @x;      -- Query the value of the variable
```

#### Assigning Values

- You can assign values to a variable using the `SET` statement or the `SELECT` statement.
- Using `SELECT`, you can also assign a value based on the result of a query.

```sql
-- Assign a value directly
SET @x = 7;

-- Assign a value from a query result
SELECT @x = age
FROM STUDENT
WHERE id = 5;

-- Update a table and assign a value to a variable simultaneously
UPDATE STUDENT
SET name = 'ali', @x = age
WHERE id = 8;
```

> [!Note]
>
> - If the query used to assign a value returns multiple rows, only the value from the last row will be assigned to the variable.
> - If no rows are returned, the variable retains its previous value (or remains `NULL` if not initialized).

> [!Warning]
>
> **Query Results Must Be Atomic (Single Value)**
>
> 1. **Ensure Single Value** : The query must return exactly one value.
> 2. **Multiple Rows Issue** : If multiple rows are returned, only the last row's value is assigned.
> 3. **No Rows Returned** : If no rows are returned, the variable retains its previous value or remains `NULL` if not initialized.

#### Table Variable

- A table variable is a special type of variable that allows you to store a result set (a set of rows and columns) **temporarily** within the scope of a batch, function, or stored procedure.
- It is declared using the **`DECLARE`** statement and behaves similarly to a temporary table but with some key differences.

**Syntax**:

```sql
DECLARE @TableName TABLE (
    Column1 DataType [Constraints],
    Column2 DataType [Constraints],
    ...
);
```

- **`@TableName`** : The name of the table variable, prefixed with `@`.
- **`Column1`, `Column2`, etc.** : Define the structure of the table variable, including column names, data types, and optional constraints (e.g., `PRIMARY KEY`, `UNIQUE`, `NOT NULL`).

**Example**:

```sql
-- Declare a table variable
DECLARE @Employees TABLE (
    EmployeeID INT PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Salary DECIMAL(10, 2)
);

-- Insert data into the table variable
INSERT INTO @Employees (EmployeeID, FirstName, LastName, Salary)
VALUES
    (1, 'John', 'Doe', 50000),
    (2, 'Jane', 'Smith', 60000),
    (3, 'Mike', 'Johnson', 55000);

-- Query the table variable
SELECT * FROM @Employees;

-- Perform operations on the table variable
UPDATE @Employees
SET Salary = Salary * 1.1; -- Give a 10% raise

SELECT * FROM @Employees;
```

### 2. Global Variables

- **Global variables** are predefined system variables provided by SQL Server.
- They are **read-only** and cannot be directly modified.
- Global variables always start with **`@@`**.

#### Common Global Variables in MS SQL Server

- **`@@SERVERNAME`**: Returns the name of the server running SQL Server.
- **`@@VERSION`**: Returns the version of SQL Server.
- **`@@ROWCOUNT`**: Returns the number of rows affected by the last statement.
- **`@@ERROR`**: Returns the error code of the last statement executed. If no error occurred, it returns `0`.
- **`@@IDENTITY`**: Returns the last-inserted identity value in the current session. If no identity column exists, it returns `NULL`.

```sql
SELECT @@SERVERNAME; -- Display the server name
SELECT @@VERSION;    -- Display the SQL Server version
SELECT @@ROWCOUNT;   -- Display the number of rows affected by the last query
SELECT @@ERROR;      -- Check for errors in the last statement
SELECT @@IDENTITY;   -- Get the last-inserted identity value
```

> [!Note]
>
> - Global variables are automatically updated by the system and are not user-modifiable.
> - Use them to retrieve system-level information or monitor the state of your queries.

### Dynamic Queries Using Variables

- **Dynamic queries** are SQL statements that are constructed as strings at runtime and executed using **`EXECUTE`**.
- This approach is particularly useful when:
  - The structure of the query depends on user input or conditions.
  - You need to generate complex or variable SQL statements programmatically.

**Examples**:

```sql
DECLARE @col VARCHAR(20) = 'salary', @t VARCHAR(20) = 'instructor';
-- EXECUTE('SELECT salary FROM instructor');
EXECUTE('SELECT '+@col+' FROM '+@t);
```

```sql
DECLARE @col VARCHAR(20) = 'salary', @t VARCHAR(20) = 'instructor';

-- Construct the dynamic query as a string
DECLARE @query NVARCHAR(MAX) = 'SELECT ' + @col + ' FROM ' + @t;

-- Execute the dynamic query
EXECUTE(@query);
```

## `SYS` Schema in MS SQL Server

The `sys` schema is a predefined schema in SQL Server that contains system objects such as:

- **System Views** : Expose metadata about database objects (tables, indexes, columns, etc.).
- **System Functions** : Provide information about the server, database, or session.
- **System Stored Procedures** : Perform administrative tasks or return system-level information.

These objects are read-only and cannot be modified directly. They are essential for database administration, monitoring, and troubleshooting.

**Example: Querying `sys.tables`**

```sql
SELECT name AS TableName, create_date
FROM sys.tables
WHERE is_ms_shipped = 0; -- Exclude system tables
```

## Control Flow Statements in MS SQL Server

Manage the execution of your SQL code based on conditions, loops, or other logic.

### 1. Conditional Statements

#### `IF`, `ELSE IF`, `ELSE`

- Used to execute blocks of code based on conditions.

**Syntax**:

```sql
IF condition1
    -- Code to execute if condition1 is true
ELSE IF condition2
    -- Code to execute if condition2 is true
ELSE
    -- Code to execute if all conditions are false
```

#### `IF EXISTS`, `IF NOT EXISTS`

- Check for the existence of rows in a query result.

**Example**:

```sql
IF EXISTS (SELECT 1 FROM Employees WHERE Department = 'Sales')
    PRINT 'Sales department exists';
ELSE
    PRINT 'Sales department does not exist';
```

### 2. Block Statements

#### `BEGIN ... END`

- Groups multiple statements into a single block.
- Useful for organizing code within conditional or loop structures.

**Example**:

```sql
IF @value > 10
BEGIN
    PRINT 'Value is greater than 10';
    SET @value = @value * 2;
END
```

### 3. Looping Constructs

#### `WHILE`

- Repeats a block of code while a condition is true.

**Example**:

```sql
DECLARE @i INT = 1;
WHILE @i <= 5
BEGIN
    PRINT @i;
    SET @i = @i + 1;
END
```

#### `CONTINUE`, `BREAK`

- **`CONTINUE`**: Skips the remaining code in the current iteration and moves to the next iteration.
- **`BREAK`**: Exits the loop immediately.

**Example**:

```sql
DECLARE @i INT = 1;
WHILE @i <= 10
BEGIN
    IF @i = 5
        BREAK; -- Exit the loop when @i is 5
    PRINT @i;
    SET @i = @i + 1;
END
```

### 4. Expression-Based Logic

#### `CASE WHEN THEN`

- Evaluates conditions and returns a value based on the first matching condition.

**Example**:

```sql
SELECT
    EmployeeID,
    CASE
        WHEN Salary > 50000 THEN 'High'
        WHEN Salary > 30000 THEN 'Medium'
        ELSE 'Low'
    END AS SalaryLevel
FROM Employees;
```

#### `IIF(condition, true_val, false_val)`

- A shorthand for simple conditional expressions.

**Example**:

```sql
SELECT EmployeeID, IIF(Salary > 50000, 'High', 'Low') AS SalaryLevel
FROM Employees;
```

#### `CHOOSE(index, val1, val2, ...)`

- Returns the value at the specified index from a list of values.

**Example**:

```sql
SELECT CHOOSE(3, 'Red', 'Green', 'Blue', 'Yellow'); -- Returns 'Blue'
```

### 5. Delay Execution

#### `WAITFOR`

- Pauses the execution of a batch, stored procedure, or transaction for a specified time.

**Example**:

```sql
WAITFOR DELAY '00:05'; -- Wait for 5 seconds
PRINT 'Execution resumed after 5 seconds';
```

## Windowing Functions in MS SQL Server

A **window function** performs a calculation across a set of table rows related to the current row, without collapsing rows like `GROUP BY`.

### 1. `LEAD()`

- **Purpose** : Accesses data from a subsequent row within the same result set.
- **Syntax**:

```sql
LEAD(column_name, offset, default_value) OVER (PARTITION BY ... ORDER BY ...)
```

- **Parameters**:
  - **`column_name`**: The column to retrieve.
  - **`offset`**: The number of rows ahead to look (default is 1).
  - **`default_value`**: Value to return if the specified row does not exist.
- **Example**:

```sql
SELECT
    EmployeeID, Salary,
    LEAD(Salary, 1) OVER (ORDER BY EmployeeID) AS NextSalary
FROM Employees;
```

### 2. `LAG()`

- **Purpose** : Accesses data from a preceding row within the same result set.
- **Syntax**:

```sql
LAG(column_name, offset, default_value) OVER (PARTITION BY ... ORDER BY ...)
```

- **Parameters**:
  - Same as **`LEAD()`**.
- **Example**:

```sql
SELECT
    EmployeeID, Salary,
    LAG(Salary, 1) OVER (ORDER BY EmployeeID) AS PreviousSalary
FROM Employees;
```

### 3. `FIRST_VALUE()`

- **Purpose** : Returns the first value in an ordered dataset.
- **Syntax**:

```sql
FIRST_VALUE(column_name) OVER (PARTITION BY ... ORDER BY ...)
```

- **Example**:

```sql
SELECT
    EmployeeID, Salary,
    FIRST_VALUE(Salary) OVER (PARTITION BY Department ORDER BY Salary DESC) AS HighestSalaryInDept
FROM Employees;
```

### 4. `LAST_VALUE()`

- **Purpose** : Returns the last value in an ordered dataset.
- **Syntax**:

```sql
LAST_VALUE(column_name) OVER (PARTITION BY ... ORDER BY ... ROWS BETWEEN ... AND ...)
```

- **Note** : Requires a frame specification (`ROWS` or `RANGE`) to define the window boundary.
- **Example**:

```sql
SELECT
    EmployeeID, Salary,
    LAST_VALUE(Salary) OVER (PARTITION BY Department ORDER BY Salary DESC ROWS BETWEEN UNBOUNDED PRECEDING AND UNBOUNDED FOLLOWING) AS LowestSalaryInDept
FROM Employees;
```

## Functions in MS SQL Server

### Built-in Function in MS SQL Server

SQL Server provides set of useful **Scalar Built-in Function**.

#### 1. NULL Handling

- `ISNULL(expression, replacement)`: Replaces `NULL` with a specified value.
- `COALESCE(value1, value2, ..., valueN)`: Returns the **first non-NULL** value from the list.

#### 2. Data Type Conversion

- `CAST(expression AS datatype)`: Converts data type.
- `CONVERT(datatype, expression, format)`: Converts data type with optional date formatting.

**Date Functions:**

- `FORMAT(date, format)`: Custom date formatting.
- `EOMONTH(date, offset)`: End of the month.
- `YEAR(date)`, `MONTH(date)`, `DAY(date)`: Extract parts of a date.
- `DATEDIFF(unit, start, end)`: Difference between dates.
- `DATENAME(unit, date)`: Return character string representing specific datepart.

#### 3. System Functions

- `DB_NAME()`: Returns the current database name.
- `@@SERVERNAME`, `@@VERSION`: System information.

#### 4. Aggregate Functions

`SUM(column)`, `COUNT(column)`, `MAX(column)`, `MIN(column)`, `AVG(column)`: Perform calculations on a set of values.

#### 5. Date & Time Functions

- `GETDATE()` → Current date & time.
- `SYSDATETIME()` → More precise current timestamp

#### 6. String Functions

- `CONCAT(value1, value2, ...)`: Combines values.
- `CONCAT_WS(separator, value1, value2, ...)`: Concatenates with a separator.
- `UPPER(text)`, `LOWER(text)`: Convert case.
- `LEN(text)`: Returns string length.
- `SUBSTRING(text, start, length)`: Extracts part of a string.
- `REVERSE(text)`: Reverses a string.
- `REPLICATE(text, n)`: Repeats text `n` times.
- `REPLACE(text, old, new)`: Replaces part of a string.
- `CHARINDEX(substring, text)`: Finds position of substring.
- `STUFF(text, start, length, insert_text)`: Replaces part of text.
- `SPACE(n)`: Returns `n` spaces.
- `ISNUMERIC(value)`, `ISDATE(value)`: Checks if a value is numeric or a valid date.

#### 7. Math Functions

- `SIN(x)`, `COS(x)`, `TAN(x)`: Trigonometric functions.
- `LOG(x)`, `POWER(x, y)`, `SQRT(x)`: Logarithm, exponentiation, square root.
- `ABS(x)`: Absolute value.

#### 8. Logical Functions

- `IIF(condition, true_value, false_value)`: Inline conditional check.

#### 9. Ranking Functions

- `ROW_NUMBER() OVER(ORDER BY column)`: Unique row number.
- `RANK() OVER(ORDER BY column)`: Ranking with gaps.
- `DENSE_RANK() OVER(ORDER BY column)`: Continuous ranking.
- `NTILE(n) OVER(ORDER BY column)`: Distributes rows into `n` groups.

#### 10. Window Functions

- `SUM() OVER(...)`, `AVG() OVER(...)`: Running totals & moving averages.
- `LAG(column, offset) OVER(...)`: Previous row’s value.
- `LEAD(column, offset) OVER(...)`: Next row’s value.
- `FIRST_VALUE(column) OVER(...)`, `LAST_VALUE(column) OVER(...)`: First & last values in window.

### User-Defined Functions (UDFs) in SQL Server

#### Types of UDFs

1. **Scalar Function** → Returns a **single** value.
2. **Inline Table-Valued Function (ITVF)** → Returns a **table**, with a single `SELECT` statement (works like a **view**).
3. **Multi-Statement Table-Valued Function (MSTVF)** → Returns a **table**, but allows **DECLARE, IF, WHILE, and multiple INSERT statements**.

#### 1. Scalar Function (Returns One Value)

- Accepts parameters and returns one value (string, int, etc.).

**Example: Using Scalar**

```sql
-- Declaration
CREATE FUNCTION dbo.getsname(@sid INT)
RETURNS VARCHAR(20)
AS
BEGIN
    DECLARE @name VARCHAR(20)
    SELECT @name = st_fname FROM student WHERE st_id = @sid
    RETURN @name
END

-- Calling
SELECT dbo.getsname(1); -- Must use schema name (dbo)
```

- **Use Case**: Get a student's first name based on their ID.

#### 2. Inline Table-Valued Function (ITVF)

- Returns a **table** from a single `SELECT` statement.
- Works like a **view** but accepts parameters.

**Example: Inline Table-Valued Function**

```sql
-- Declaration
CREATE FUNCTION getinsts(@did INT)
RETURNS TABLE
AS
RETURN
(
    SELECT ins_name, salary * 12 AS annualsal
    FROM instructor
    WHERE Dept_Id = @did
);

-- Calling
SELECT * FROM getinsts(10);
```

- **Use Case**: Get **instructor names** and **annual salaries** for a given department.
- **No need for `dbo.` schema prefix while calling!**

#### 3. Multi-Statement Table-Valued Function (MSTVF)

- Returns a **table** and allows multiple operations (`DECLARE`, `IF`, `WHILE`, `INSERT`).
- More flexible than **ITVF**, but **slower** due to table variable usage.

**Example: Multi-Statement Table-Valued Function**

```sql
-- Declaration
CREATE FUNCTION getTopInstructors(@minSalary INT)
RETURNS @InstructorTable TABLE
(
    InstructorID INT,
    InstructorName VARCHAR(50),
    MonthlySalary DECIMAL(10,2)
)
AS
BEGIN
    INSERT INTO @InstructorTable
    SELECT ins_id, ins_name, salary
    FROM instructor
    WHERE salary > @minSalary

    RETURN
END

-- Calling
SELECT * FROM dbo.getTopInstructors(5000);
```

- **Use Case**: Get **instructors with a salary greater than a specified amount**.

#### User Defined Functions: Key Points

- **Scalar** → Returns **one value**, must use `dbo.` when calling.
- **ITVF** → Returns **table (one `SELECT`)**, no need for schema prefix when calling.
- **MSTVF** → Returns **table (multi-step logic)**, allows `DECLARE` & `INSERT`, but is **slower**.
