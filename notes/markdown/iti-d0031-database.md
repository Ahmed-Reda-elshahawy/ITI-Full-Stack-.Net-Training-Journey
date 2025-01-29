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
> - `T1` represent the rows from the first instance of the table `table1` (parent table).
> - `T2` represent the rows from the second instance of the table `table1` (child table).

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
