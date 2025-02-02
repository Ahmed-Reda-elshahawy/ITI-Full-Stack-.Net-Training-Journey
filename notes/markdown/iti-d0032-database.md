# 🔖 ITI - D0032 - Database

## Aggregate Functions

An aggregate function operates on a set of values and returns a single value.

| **Aggregate function** | **Description**                                                 |
| ---------------------- | --------------------------------------------------------------- |
| **AVG**                | Calculate the average of non-NULL values in a set of values.    |
| **SUM**                | Return the summation of all non-NULL values in a set.           |
| **MAX**                | Return the highest value (maximum) in a set of non-NULL values. |
| **MIN**                | Return the lowest value (minimum) in a set of non-NULL values.  |
| **COUNT**              | Return the number of rows in a group that satisfy a condition.  |

> [!TIP]
> To include `NULL` values in aggregate calculations (instead of excluding them by default), use `ISNULL` to replace `NULL` with a value like `0`. For example:
>
> ```sql
> SELECT
>   AVG(ISNULL(salary, 0)) AS avg_salary
> FROM
>  Employee;
> ```
>
> This ensures `NULL` values are considered in the calculation.

## Grouping

### `GROUP BY`

**GROUP BY**: Used to group rows that have the same values into summary rows. Often used with aggregate functions like `COUNT`, `SUM`, `AVG`, etc.

```sql
SELECT department, AVG(salary) AS avg_salary
FROM Employee
GROUP BY department;
```

**Grouping with Multiple Columns**: We can group the result set using multiple columns by specifying them in the `GROUP BY` clause. This allows aggregation based on combinations of column values.

```sql
SELECT department, job_role, AVG(salary) AS avg_salary
FROM Employee
GROUP BY department, job_role;
```

**Using Aggregate Without `GROUP BY`**: When an aggregate function (e.g., `SUM`, `AVG`, `COUNT`) is used without a `GROUP BY` clause, it treats the entire table as a single group and returns one result for the whole dataset.

```sql
SELECT AVG(salary) AS avg_salary
FROM Employee;
```

### `HAVING`

**HAVING**: Filters groups after aggregation. Unlike `WHERE`, which filters rows before grouping, `HAVING` works on grouped results.

```sql
SELECT department, AVG(salary) AS avg_salary
FROM Employee
GROUP BY department
HAVING AVG(salary) > 50000;
```

## Query Execution Order

1. **FROM & JOIN**: Identifies the tables or views to retrieve data from.
2. **WHERE**: Filters rows based on specified conditions.
3. **GROUP BY**: Groups rows that have the same values into summary rows.
4. **HAVING**: Filters groups after aggregation (used with `GROUP BY`).
5. **SELECT**: Specifies the columns to return in the result set.
6. **ORDER BY**: Sorts the result set by specified columns.
7. **LIMIT/OFFSET** (or `TOP` in SQL Server): Limits the number of rows returned.

## Subquery

### Subquery Glossary

- **Inner Query**: The query written inside another query. It executes first and provides results to the **Outer Query**.
- **Outer Query**: The main query that uses the result of the **Inner Query**.

### Subquery Examples

**Non-Correlated Subquery Example**:

```sql
SELECT name, salary
FROM Employee
WHERE salary > (SELECT AVG(salary) FROM Employee);
-- The inner query calculates the average salary and passes it to the outer query.
```

**Correlated Subquery**:

```sql
SELECT e.name, e.salary
FROM Employee e
WHERE salary > (SELECT AVG(salary) FROM Employee WHERE department = e.department);
-- The inner query runs for each row in the outer query, comparing salaries within the same department.
```

### Subquery Best Practices

- Use subqueries when you need to break down complex problems into smaller steps.
- Avoid deeply nested subqueries; they can be hard to read and optimize.

### Subquery Key Points

- Subqueries can be used in `SELECT`, `FROM`, `WHERE`, and `HAVING` clauses.
- Always test subqueries independently to ensure they return the expected results.

### Subquery With DML

Subqueries in DML allow dynamic data manipulation based on conditions or data from other tables.

**`INSERT` With Subquery**:

```sql
INSERT INTO HighSalaryEmployees (name, salary)
SELECT name, salary
FROM Employee
WHERE salary > 50000;
```

**`UPDATE` With Subquery**:

```sql
UPDATE Employee
SET salary = salary * 1.1
WHERE department = (SELECT department FROM Departments WHERE location = 'New York');
```

**`DELETE` With Subquery**:

```sql
DELETE FROM Employee
WHERE department = (SELECT department FROM Departments WHERE budget < 100000);
```

## Set Operations

Set operations allow you to combine the results of two or more queries into a single result set.

### `UNION`

- Combines the results of two or more `SELECT` queries into a single result set.
- Removes duplicate rows from the combined result.
- Requires the same number of columns in all queries, with compatible data types.

**Example**:

```sql
SELECT name FROM Employees
UNION
SELECT name FROM Managers;
```

### `UNION ALL`

- Combines the results of two or more `SELECT` queries into a single result set.
- **Does not remove duplicates**, returns all rows from all queries.
- Requires the same number of columns in all queries, with compatible data types.

**Example**:

```sql
SELECT name FROM Employees
UNION ALL
SELECT name FROM Managers;
```

### `INTERSECT`

- Returns only the rows that are common to both queries.
- Removes duplicates in the final result.
- Requires the same number of columns in both queries, with compatible data types.

**Example**:

```sql
SELECT name FROM Employees
INTERSECT
SELECT name FROM Managers;
```

### `EXCEPT`

- Returns rows from the first query that are **not present** in the second query.
- Removes duplicates in the final result.
- Requires the same number of columns in both queries, with compatible data types.

**Example**:

```sql
SELECT name FROM Employees
EXCEPT
SELECT name FROM Managers;
```

## Data Types In MS SQL Server

### Numeric Data Types

- **`bit`**: Boolean (0 or 1).
- **`tinyint`**: 1 byte (0 to 255).
- **`smallint`**: 2 bytes (-32,768 to 32,767).
- **`int`**: 4 bytes (-2.1B to 2.1B).
- **`bigint`**: 8 bytes (-9.2Q to 9.2Q).

### Decimal Data Types

- **`smallmoney`**: 4 bytes, monetary values (precision: 4 decimal places).
- **`money`**: 8 bytes, monetary values (precision: 4 decimal places).
- **`real`**: 4 bytes, floating-point (7-digit precision).
- **`float`**: 8 bytes, floating-point (15-digit precision).
- **`decimal(p, s)`**: Fixed precision and scale (e.g., `decimal(10, 2)` for 10 digits, 2 decimals).

### Text Data Types

- **`char(n)`**: Fixed-length, non-Unicode (max 8,000 chars).
- **`varchar(n)`**: Variable-length, non-Unicode (max 8,000 chars).
- **`nchar(n)`**: Fixed-length, Unicode (max 4,000 chars).
- **`nvarchar(n)`**: Variable-length, Unicode (max 4,000 chars).
- **`nvarchar(max)`**: Variable-length, Unicode (up to 2 GB).

### Date & Time Data Types

- **`date`**: Stores date only (YYYY-MM-DD).
- **`time`**: Stores time only (HH:MM:SS ).
- **`time(n)`**: Time with fractional seconds precision (e.g., `time(3)` = HH:MM:SS .**ddd**).
- **`smalldatetime`**: Date and time (YYYY-MM-DD HH:MM:00, year range: 1900-2079).
- **`datetime`**: Date and time (YYYY-MM-DD HH:MM:SS .**ddd**, year range: 1753-9999).
- **`datetime2(n)`**: High-precision date and time (e.g., `datetime2(7)` = YYYY-MM-DD HH:MM:SS .**ddddddd**, year range: 0001-9999).

#### Binary Data Types

- **`binary(n)`**: Fixed-length binary data (max 8,000 bytes).
- **`varbinary(n)`**: Variable-length binary data (max 8,000 bytes).
- **`varbinary(max)`**: Variable-length binary data (up to 2 GB).
- **`image`**: Deprecated, use `varbinary(max)` instead.

### Other Data Types

- **`uniqueidentifier`**: 16-byte GUID.
- **`xml`**: Stores XML data.
- **`json`**: Stores JSON data (SQL Server 2016+).

## Using Column Numbers in `ORDER BY`

In SQL, you can refer to columns by their **positional number** in the `SELECT` clause when using `ORDER BY`. This can simplify queries but should be used cautiously for clarity.

**Example**:

```sql
SELECT
    name,
    age
FROM
    employee
ORDER BY 1 ASC;  -- Sorts by the first column (`name`)
```

## `LIKE` operator with `WHERE` clause

- The `LIKE` operator in SQL Server is used to perform pattern matching on strings.
- It is commonly used in the `WHERE` clause to filter rows based on specific patterns in text columns.
- The `LIKE` operator supports wildcard characters, making it a powerful tool for searching and filtering data.

### Supported Wildcards with `LIKE` operator In MS SQL Server

#### `%`: Zero or More Characters

- Matches any string of zero or more characters.
- **Example**:

```sql
SELECT * FROM Employees WHERE LastName LIKE 'Smi%';
-- This query will return all employees whose last name starts with "Smi" (e.g., Smith, Smiley).
```

#### `_`: Exactly One Character

- Matches exactly one character.
- **Example**:

```sql
SELECT * FROM Employees WHERE LastName LIKE '_ing';
-- This query will return all employees whose last name has "ing" as the last three letters and has exactly one character before it (e.g., King, Ring)
```

#### `[]`: Any Single Character in a Set

- Matches any single character within the specified set.
- **Example**:

```sql
SELECT * FROM Employees WHERE LastName LIKE '[ABC]o%';
-- This query will return all employees whose last name starts with "Ao", "Bo", or "Co".
```

#### `[^]`: Any Single Character NOT in a Set

- Matches any single character that is NOT in the specified set.
- **Example**:

```sql
SELECT * FROM Employees WHERE LastName LIKE '[^ABC]o%';
-- This query will return all employees whose last name starts with "o" but does not have "A", "B", or "C" before it.
```

#### `[a-z]`: Range of Characters

- Matches any single character within the specified range.
- Example:

```sql
SELECT * FROM Employees WHERE LastName LIKE '[A-M]%';
-- This query will return all employees whose last name starts with any letter from A to M.
```

#### Escaping Special Characters (`%`, `_`, `[`, `]`)

- To match special characters like `%`, `_`, `[`, or `]` literally, you need to escape them using square brackets (`[ ]`).
- **Example**:

```sql
SELECT * FROM Products WHERE ProductName LIKE '%[%]';
-- This query will return all products whose name contains the `%` symbol.
```

```sql
SELECT * FROM Products WHERE ProductName LIKE '%[_]%';
-- This query will return all products whose name contains the `_` symbol.
```

#### Grouping with Parentheses (`()`)

- While parentheses are not directly supported for grouping in `LIKE` patterns, they can be used in combination with other SQL constructs (e.g., `OR` conditions) to create complex queries.
- **Example**:

```sql
SELECT * FROM Products WHERE ProductName LIKE 'Book%' OR ProductName LIKE 'Pen%';
```

> [!NOTE]
>
> By default, the `LIKE` operator in SQL Server is case-insensitive if the collation of the database is case-insensitive.

## BATCH vs. Script vs. Transaction

### Batch

- A **batch** is a set of SQL statements sent to the server for execution as a single unit.
- Batches are separated by the `GO` keyword in SQL Server.
- Each batch is compiled and executed independently.
- If one statement in a batch fails, the rest of the batch may still execute (unless error handling is in place).

```sql
SELECT * FROM Employees;
GO

UPDATE Employees SET salary = salary * 1.1 WHERE department = 'Sales';
GO

-- Two batches: one for `SELECT` and one for `UPDATE`.
```

### Script

- A **script** is a collection of SQL statements (can include multiple batches) saved in a file or written ad-hoc.
- Scripts are used for automation, deployment, or repetitive tasks.

### Transaction

- A **transaction** is a logical unit of work that ensures data integrity.
- Transactions follow the **ACID** properties (Atomicity, Consistency, Isolation, Durability).
- Use `BEGIN TRANSACTION`, `COMMIT`, and `ROLLBACK` to control transactions.
- If any part of the transaction fails, the entire transaction can be rolled back.

#### Transactions & Database Files

- **`.mdf`**: Primary database file storing data (tables, indexes, etc.).
- **`.ldf`**: Log file recording all transactions and modifications.
- **How Transactions Use `.mdf` and `.ldf`**:
  - When a transaction starts, changes are written to the **log file (`.ldf`)** first.
  - Once the transaction is committed, changes are applied to the **data file (`.mdf`)**.
  - If the transaction fails, the log file ensures the database can be rolled back to a consistent state.

#### Error Handling with `TRY...CATCH`

- Use `TRY...CATCH` to handle errors in transactions.
- If an error occurs in the `TRY` block, execution moves to the `CATCH` block, where you can roll back the transaction and log the error.

**Example**:

```sql
/*
1. Unfortunately the company ended the contract with Mr. Kamel Mohamed
(SSN=223344) so try to delete his data from your database in case you know that you
will be temporarily in his position (your SSN =102672) .
Hint: (Check if Mr. Kamel has dependents, works as a department manager, supervises
any employees or works in any projects and handle these cases).
*/
BEGIN TRY
	BEGIN TRANSACTION;

	-- delete all dependent
	DELETE FROM Dependent
	WHERE ESSN = 223344;

	-- reassign manager
	UPDATE Departments
	SET MGRSSN = 102672
	WHERE MGRSSN = 223344;

	-- reassign supervisor
	UPDATE Employee
	SET Superssn = 102672
	WHERE Superssn = 223344;

	-- remove his project work
	DELETE FROM Works_for
	WHERE ESSn = 223344;

	-- delete kamal from employee
	DELETE FROM Employee
	WHERE SSN = 223344;

	COMMIT;
END TRY
BEGIN CATCH
	ROLLBACK;
	SELECT
		ERROR_LINE() AS err_line,
		ERROR_MESSAGE() AS err_message;
END CATCH
```

### Summary of ACID Properties:

|**Property**    |	**Description**   						|			**Example**                                       |
|----------------|----------------------------------------------------------------------|-------------------------------------------------------------------------|
|**Atomicity**   |Transactions are all-or-nothing.	                                |Money transfer: both debit and credit must succeed, or neither happens.  |
|**Consistency** |Transactions maintain database integrity and rules.	 		|Account balance cannot go negative.					  |
|**Isolation**   |Transactions are isolated from each other until committed.	        |Two transfers happening simultaneously do not interfere with each other. |
|**Durability**  |Committed transactions are permanent, even after system failures.	|Updated data remains intact after a crash.				  |

### Why ACID Properties Are Important:
- **Data Integrity**: Ensures that the database remains accurate and consistent.
- **Concurrency Control**: Allows multiple transactions to occur simultaneously without causing conflicts.
- **Recovery**: Ensures that the database can recover from failures without losing data.

