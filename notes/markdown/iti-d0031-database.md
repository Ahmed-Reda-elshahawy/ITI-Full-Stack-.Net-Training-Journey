# 🔖 ITI - D0031 - Database

_Review [Database Restoring](./iti-d0030-database.md#restore-database-using-ssms)_

## Types of Joins in SQL Server

1. **CROSS JOIN**: Returns the Cartesian product of the two tables.
2. **INNER JOIN**: Returns rows when there is at least one match in both tables. (`Equi Join`)
3. **OUTER JOIN**: Returns all rows from both tables, joining them where there is a match.
   - **LEFT JOIN**: Returns all rows from the left table, and the matched rows from the right table.
   - **RIGHT JOIN**: Returns all rows from the right table, and the matched rows from the left table.
   - **FULL JOIN**: Returns rows when there is a match in one of the tables.
4. **SELF JOIN**: Join a table to itself.

### CROSS JOIN

```sql
SELECT * FROM table1 CROSS JOIN table2;
```

### INNER JOIN

```sql
SELECT * FROM table1 INNER JOIN table2 ON table1.column_name = table2.column_name;

-- OR
SELECT * FROM table1 JOIN table2 ON table1.column_name = table2.column_name;
```

### LEFT JOIN

```sql
SELECT * FROM table1 LEFT JOIN table2 ON table1.column_name = table2.column_name;
```

### RIGHT JOIN

```sql
SELECT * FROM table1 RIGHT JOIN table2 ON table1.column_name = table2.column_name;
```

### FULL JOIN

```sql
SELECT * FROM table1 FULL JOIN table2 ON table1.column_name = table2.column_name;
```

### SELF JOIN

```sql
SELECT * FROM table1 T1, table1 T2 WHERE T1.column_name = T2.column_name;
```

> [!Note]
>
> - `T1` and `T2` are aliases for the table `table1`.
> - `T1` represent the rows from the first instance of the table `table1` (parent table which has primary key).
> - `T2` represent the rows from the second instance of the table `table1` (child table with which has foreign key).

**Example**:

```sql
SELECT
    e1.EmployeeName AS Employee,
    e2.EmployeeName AS Manager
FROM
    Employees e1  -- Child (Employee)
LEFT JOIN
    Employees e2  -- Parent (Manager)
ON
    e1.ManagerID = e2.EmployeeID;
```

## JOINs with DML Statements

Using JOINs with DML (Data Manipulation Language) statements like UPDATE and DELETE allows you to modify or delete data based on conditions from related tables.

### Update with Join

Use a JOIN in an UPDATE statement to update rows in one table based on data from another table.

**Syntax**:

```sql
UPDATE t1
SET t1.column = value
FROM Table1 t1
JOIN Table2 t2 ON t1.common_column = t2.common_column
WHERE condition;
```

**Example**:

Update employee salaries based on department budget:

```sql
-- Increases salaries for employees in departments with a budget greater than 100,000.
UPDATE e
SET e.salary = e.salary * 1.1
FROM Employee e
JOIN Department d ON e.department_id = d.department_id
WHERE d.budget > 100000;
```

### Delete with Join

Use a JOIN in a DELETE statement to delete rows from one table based on data from another table.

**Syntax**:

```sql
DELETE t1
FROM Table1 t1
JOIN Table2 t2 ON t1.common_column = t2.common_column
WHERE condition;
```

**Example**:

Delete employees who work in a specific department:

```sql
-- Deletes all employees who belong to the 'HR' department
DELETE e
FROM Employee e
JOIN Department d ON e.department_id = d.department_id
WHERE d.name = 'HR';
```

## Actions For Referential Integrity

### `CASCADE`

- Automatically updates or deletes related rows in the child table when the referenced row in the parent table is updated or deleted.
- Commonly used with **weak entities** (e.g., delete all orders when a customer is deleted).

**Example**:

```sql
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    CustomerID INT,
    FOREIGN KEY (CustomerID)
        REFERENCES Customers(CustomerID)
        ON DELETE CASCADE
);
```

### `SET DEFAULT`

- Sets the foreign key column in the child table to a default value when the referenced row in the parent table is updated or deleted.
- Requires handling cases where the default value might conflict (e.g., using **triggers**).

**Example**:

```sql
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    CustomerID INT DEFAULT 0,
    FOREIGN KEY (CustomerID)
        REFERENCES Customers(CustomerID)
        ON DELETE SET DEFAULT
);
```

### `SET NULL`

- Sets the foreign key column in the child table to NULL when the referenced row in the parent table is updated or deleted.
- Use when the relationship is optional.

**Example**:

```sql
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    CustomerID INT,
    FOREIGN KEY (CustomerID)
        REFERENCES Customers(CustomerID)
        ON DELETE SET NULL
);
```

## Handling `NULL` Values in Queries

### 1. With `WHERE` Clause

- `IS NULL`: Filters rows where a column is `NULL`.

```sql
SELECT * FROM Employees WHERE salary IS NULL;
```

- `IS NOT NULL`: Filters rows where a column is not `NULL`.

```sql
SELECT * FROM Employees WHERE salary IS NOT NULL;
```

### Functions with `SELECT`

- `ISNULL()`: Replaces `NULL` with a default value (single replacement).

```sql
-- Replaces NULL in salary with 0.
SELECT ISNULL(salary, 0) AS salary FROM Employees;
```

- `COALESCE()`: Returns the first non-NULL value from a list (multiple replacements).

```sql
-- Returns salary if not NULL, otherwise bonus, otherwise 0.
SELECT COALESCE(salary, bonus, 0) AS income FROM Employees;
```

## Identity Column

- An Identity Column automatically generates unique numeric values for a column, typically used for primary keys.

**Syntax**:

```sql
CREATE TABLE table_name
(
    column_name INT IDENTITY(seed, increment) PRIMARY KEY,
    ...
);
```

- `seed`: Starting value (e.g., 1).
- `increment`: Step value for each new row (e.g., 1).

**Example**:

```sql
CREATE TABLE Employees
(
    EmployeeID INT IDENTITY(1, 1) PRIMARY KEY, -- Starts at 1, increments by 1
    Name VARCHAR(50)
);
```

### Retrieving the Last Identity Value

- Use `SCOPE_IDENTITY()` to get the last identity value inserted in the current scope.

**Example**:

```sql
INSERT INTO Employees (Name) VALUES ('John Doe');
SELECT SCOPE_IDENTITY(); -- Returns the last generated EmployeeID
```

## Database Integrity

Database integrity ensures data accuracy and consistency. It is categorized into three types:

### 1. Domain Integrity (Range of Values)

Ensures valid data entry in a column.

- **Data Type**: Defines the type of data (e.g., INT, VARCHAR).
- **Check Constraint**: Limits values (e.g., salary >= 0).
- **Default Value**: Sets a default if no value is provided.
- **NOT NULL**: Ensures a column cannot have NULL values.

**DB Objects Used**:

- **Rule**: Shared constraints for columns.
- **Trigger**: Custom logic for data validation.

### 2. Entity Integrity (Uniqueness)

Ensures each row is unique.

- **Primary Key**: Unique identifier for each row.
- **Unique Constraint**: Ensures no duplicate values in a column.

**DB Objects Used**:

- **Index**: Improves performance for unique checks.
- **Trigger**: Custom logic for uniqueness.

### 3. Referential Integrity (Relationship)

Ensures relationships between tables are valid.

- **Foreign Key**: Links data between tables.
- **Custom Constraint**: Additional rules for relationships.
- **Stored Procedure**: Custom logic for maintaining relationships.

**DB Objects Used**:

- **Trigger**: Custom logic for enforcing relationships.

### Table Creation with Constraints

```sql
CREATE TABLE employees (
    employee_id INT PRIMARY KEY,
    employee_name VARCHAR(50) NOT NULL,
    email VARCHAR(50) UNIQUE,
    manager_id INT,
    salary DECIMAL(10, 2) DEFAULT 0.0,
    hire_date DATE DEFAULT GETDATE(),
    overtime INT,
    net_salary AS (ISNULL(salary, 0) + ISNULL(overtime, 0)) PERSISTED,  -- computed + saved
    birth_date DATE,
    age AS YEAR(GETDATE()) - YEAR(birth_date), -- computed + not saved
    CONSTRAINT fk_manager FOREIGN KEY (manager_id) REFERENCES employees(employee_id) ON DELETE SET NULL,
    CONSTRAINT ck_salary CHECK (salary >= 0.0),
    CONSTRAINT ck_overtime CHECK (overtime >= 0),
    CONSTRAINT ck_birth_date CHECK (birth_date <= GETDATE()),
    CONSTRAINT ck_email CHECK (email LIKE '%@%')
);
```

### Constrains Drawbacks

- Applied to both new and existing data.
- Cannot be shared between tables.

### Rules

- **Shared**: Can be used across multiple tables.
- **New Data Only**: Applied only to new data.
- **One Rule per Column**: A column can have only one rule.

```sql
CREATE RULE positive_value AS @value > 0; -- Create rule
SP_BINDRULE 'positive_value', 'employees.salary'; -- Bind rule to column
SP_UNBINDRULE 'employees.salary'; -- Unbind rule
```
