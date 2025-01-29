# 🔖 ITI - D0030 - Database

## Primary Key vs. Foreign Key

### Primary Key

- Uniquely identifies each record in table.
- Cannot contain **Null** values.
- Must be **unique** across the table.
- A table can have only **one primary key**.
- Can be a single column or a combination of columns (**composite key**).
- Ensures **entity integrity** (no duplicate records)

### Foreign Key

- A column (or set of columns) in one table that refers to the **primary key** in another table.
- Ensures **referential integrity** between two related tables.
- Can contain **Null** values (unless explicit constrained)
- A table can have **multiple foreign keys**.
- Used to create relationships (links) between tables.
- Prevents actions that would destroy the link between tables (e.g. deleting a record referenced by a foreign key).

> [!Note]
>
> **Mapping Representation**:
>
> - **Primary Key**: Represented by an **underline solid line** (`_________`).
> - **Foreign Key**: Represented by an **underline dashed line** (`_ _ _ _ _`).

## ER-to-Relational Mapping

- Step 1: Mapping of Regular Entity Types.
- Step 2: Mapping of Weak Entity Types.
- Step 3: Mapping of Binary 1:1 Relation Types.
- Step 4: Mapping of Binary 1:N Relationship Types.
- Step 5: Mapping of Binary M:N Relationship Types.
- Step 6: Mapping of N-ary Relationship Types.
- Step 7: Mapping of Unary Relationship Types.

### Step 1: Mapping of Regular Entity

- **Map each regular entity to a table**.
- **Composite attributes**: Break them into individual columns in the same table.
- **Multi-valued attributes**: Create a separate table for them and link it to the main table using a foreign key.
- **Derived attributes**: Typically, these are not stored in the database as they can be calculated when needed.

**Example**:

- **Entity**: `Employee`
- Attributes:
  - `EmpID` (Primary Key)
  - `Name`
  - `Salary`
  - Composite Attribute: `Address` → (`Street`, `City`, `Zip`)
  - Multi-valued Attribute: `Phone`
  - Derived Attribute: `Age` (derived from DOB)
- **Mapping**:

  ```sql
  Table Employee {
    EmpID int [primary key]
    Name varchar
    Salary decimal
    Street varchar
    City varchar
    Zip varchar
  }

  Table EmployeePhone {
    EmpID int [ref: > Employee.EmpID]
    Phone varchar
  }
  ```

### Step 2: Mapping of Weak Entity

- **Map each weak entity to a table**.
- **Link** the weak entity table to its owner table using a **foreign key**.
- The primary key of the weak entity table consists of:
  - Its **partial identifier**.
  - The **primary key** of the identifying (strong) entity.

**Example:**

- **Weak Entity**: `Dependent`
- **Strong Entity**: `Employee`
- **Attributes of Dependent**:
  - `DepName` (Partial Identifier)
  - `Relationship`
    Primary Key of Dependent: (`EmpID` + `DepName`)
- **Mapping**:

  ```sql
  Table Employee {
    EmpID int [primary key]
    Name varchar
    Salary decimal
  }

  Table Dependent {
    DepName varchar
    Relationship varchar
    EmpID int [ref: > Employee.EmpID]

    indexes {
      (DepName, EmpID) [primary key]
    }
  }
  ```

### Step 3: Mapping of Binary 1:1 Relation Types

#### Both Entities Mandatory

- Combine both entities into a **single table**.
- Use the primary key of either entity as the primary key for the combined table.

**Example**:

- **Entities**: `Husband`, `Wife`
- **Attributes**:
  - `HusbandID`
  - `WifeID`
- **Mapping**:

  ```sql
  Table Marriage {
    HusbandID int [primary key]
    WifeID int
  }
  ```

#### One Mandatory and One Optional

- Create **two tables**, one for each entity.
- Add a **foreign key** in the mandatory table referencing the optional table.

**Example**:

- **Entities**: `Passport`, `Person`
- **Attributes**:
  - `PassportID`
  - `PersonID`
- **Mapping**

  ```sql
  -- mandatory
  Table Passport {
    PassportID int [primary key]
    PersonID int [ref: > Person.PersonID]
  }

  -- optional
  Table Person {
    PersonID int [primary key]
    Name varchar
  }
  ```

#### Both Entities Optional

- Create **three tables**: one for each entity and a third table for the relationship.
- The third table contains **two foreign keys**, one for each entity, with one of them serving as the primary key.

**Example**:

- **Entities**: `Student` and `Advisor`
- **Mapping**:

  ```sql
  Table Student {
    StudentID int [primary key]
    Name varchar
  }

  Table Advisor {
    AdvisorID int [primary key]
    Name varchar
  }

  Table Advising {
    StudentID int [ref: > Student.StudentID]
    AdvisorID int [ref: > Advisor.AdvisorID]

    indexes {
      (StudentID, AdvisorID) [primary key]
    }
  }
  ```

### Step 4: Mapping of Binary 1:N Relationship

For 1:N relationships, focus on the entity with the "many" side.

#### Many is Mandatory

- **Create a table for each entity**.
- Add a **foreign key** in the "many" table referencing the "one" table.

**Example**:

- **Relationship**: `Department` and `Employee` (1:N)
  - `Department` has one or more employees.
- **Mapping**:

```sql
Table Department {
  DeptID int [primary key]
  DeptName varchar
}

Table Employee {
  EmpID int [primary key]
  Name varchar
  DeptID int [ref: > Department.DeptID]
}
```

#### Many is Optional

- Create a table for each entity and an **additional table** for the relationship.
- The relationship table contains **two foreign keys**, one for each entity.
  - Use the foreign key of the **"one" side entity** as the primary key.

### Step 5: Mapping of Binary M:N Relationship

- Create table for each entity and **additional table** for the relationship.
- The relationship table contains **two foreign keys**, one for each entity.
  - Use the 2 foreign keys as a **composite primary key**.

**Example**:

- **Entities**: `Student` and `Course`
  - Students can enroll in multiple courses, and each course can have multiple students.
- **Mapping**:

  ```sql
  Table Student {
    StudentID int [primary key]
    Name varchar
  }

  Table Course {
    CourseID int [primary key]
    CourseName varchar
  }

  Table Enrollment {
    StudentID int [ref: > Student.StudentID]
    CourseID int [ref: > Course.CourseID]

    indexes {
      (StudentID, CourseID) [primary key]
    }
  }
  ```

### Step 6: Mapping of N-ary Relationship

- Create a table for each entity and an **additional table** for the relationship.
- The relationship table contains **N foreign keys**, one for each participating entity.
  - Use the combination of the foreign keys with other columns as a **composite primary key**.

**Example**:

- **Entities**: `Doctor`, `Patient`, `Treatment`
  - A doctor provides treatment to a patient.
- **Mapping**:

  ```sql
  Table Doctor {
    DoctorID int [primary key]
    Name varchar
  }

  Table Patient {
    PatientID int [primary key]
    Name varchar
  }

  Table Treatment {
    TreatmentID int [primary key]
    Description varchar
  }

  Table DoctorPatientTreatment {
    DoctorID int [ref: > Doctor.DoctorID]
    PatientID int [ref: > Patient.PatientID]
    TreatmentID int [ref: > Treatment.TreatmentID]

    indexes {
      (DoctorID, PatientID, TreatmentID) [primary key]
    }
  }
  ```

### Step 7: Mapping of Unary Relationship

#### Unary Relationship (`1:M`)

- Add a **foreign key** in the same table that references the primary key of the same table.

**Example**:

- Employee supervises other employees.
- **Mapping**:

  ```sql
  Table Employee {
    EmpID int [primary key]
    Name varchar
    SupervisorID int [ref: > Employee.EmpID]
  }
  ```

#### Unary Relationship (`M:N`)

- Create a **separate table** to represent the relationship.
- This table contains **two foreign keys**, both referencing the primary key of the original table.

**Example**:

- Employees work as mentors for other employees.
- **Mapping**:

```sql
Table Employee {
  EmpID int [primary key]
  Name varchar
}

Table Mentorship {
  MentorID int [ref: > Employee.EmpID]
  MenteeID int [ref: > Employee.EmpID]

  indexes {
    (MentorID, MenteeID) [primary key]
  }
}
```

## SQL (Structured Query Language)

- **Definition**: SQL is a standardized language used to communicate with relational databases to perform operations like `querying`, `updating`, and `managing` data.
- **Core Functionality**: Data definition (`DDL`), data query (`DQL`),data manipulation (`DML`), data control (`DCL`), and transaction control (`TCL`).

### ANSI SQL

- **Definition**: A standardized version of SQL defined by the American National Standards Institute (ANSI) to ensure compatibility across different database systems.
- **Core Feature**: Focuses on basic SQL syntax and operations like `SELECT`, `INSERT`, `UPDATE`, `DELETE`.

### SQL Flavors (Vendor-Specific Extensions)

- **T-SQL (Transact-SQL)**: Microsoft SQL Server, Azure SQL.
- **PL/SQL (Procedural Language SQL)**: Oracle Database.
- **MySQL (SQL/PSM)**: MySQL database.
- **pgSQL (PL/pgSQL)**: PostgreSQL

### SQL Core Functionality

1. **Data Definition Language (DDL)**

   - **Purpose**: Defines database structure and schema.
   - **Key Commands**:
     - `CREATE TABLE`, `CREATE VIEW`, `CREATE FUNCTION`
     - `ALTER TABLE`, `DROP TABLE`

2. **Data Query Language (DQL)**

   - **Purpose**: Retrieves data from the database.
   - **Key Commands**:
     - `SELECT`, `Aggregate Functions` (e.g., `SUM`, `AVG`)
     - `GROUP BY`, `UNION`
     - `JOINS`, `Subqueries`

3. **Data Manipulation Language (DML)**

   - **Purpose**: Manipulates existing data in tables.
   - **Key Commands**:
     - `INSERT`, `UPDATE`, `DELETE`
     - `MERGE` (for upserts)

4. **Data Control Language (DCL)**

   - **Purpose**: Manages access and permissions.
   - **Key Commands**:
     - `GRANT`, `DENY`, `REVOKE`

5. **Transaction Control Language (TCL)**
   - **Purpose**: Manages transaction integrity and execution.
   - **Key Commands**:
     - `BEGIN TRANSACTION`, `COMMIT`, `ROLLBACK`

## Microsoft SQL Server

**Microsoft SQL Server** is a relational database management system (RDBMS) developed by Microsoft that supports a wide range of transaction processing, business intelligence, and analytics applications.

### SQL Server Editions

1. **Express**: Free entry-level database.
2. **Developer**: Full-featured edition for development and testing.

### SQL Server Management Studio (SSMS)

An integrated environment for managing SQL Server databases, used for configuring, managing, and administering all components within SQL Server.

#### Create Database Using SSMS

1. Open SQL Server Management Studio.
2. Connect to the SQL Server instance.
3. Right-click on `Databases` in the Object Explorer.
4. Select `New Database`.
5. Enter the database name and configure other settings.
   - **Database Name**
   - **Owner**: Default is `dbo`.
   - **Database Files**: Data file (`mdf`) and log file settings (`ldf`).
6. Click `OK` to create the database.

#### Create Table Using SSMS

1. Open SQL Server Management Studio.
2. Connect to the SQL Server instance.
3. Expand the database in which you want to create the table.
4. Right-click on `Tables` and select `New Table`.
5. Add columns to the table by specifying the column name, data type, and other properties.
6. Press `Ctrl + S` to save the table.
7. Refresh `Tables` to view the newly created table.

#### Create Diagram Using SSMS

1. Open SQL Server Management Studio.
2. Connect to the SQL Server instance.
3. Right-click on `Database Diagrams` in the Object Explorer.
4. Select `New Database Diagram`.
5. Select the tables you want to include in the diagram and then press `Add`.
6. Arrange the tables and create relationships between them by dragging the primary key to the foreign key.
7. Save the diagram by pressing `Ctrl + S`.

#### Insert, Update and Query Data Using SSMS

1. **Insert Data**:
   - Right-click on the table and select `Edit Top 200 Rows`.
   - Enter the data in the table.
   - Press `Ctrl + S` to save the data.
2. **Update Data**:
   - Right-click on the table and select `Edit Top 200 Rows`.
   - Update the data in the table.
   - Press `Ctrl + S` to save the changes.
3. **Query Data**:
   - Open a new query window.
   - Write the SQL query to retrieve data.
   - Execute the query by pressing `F5`.
   - View the results in the output window.
   - **OR** Right-click on the table and select `Select Top 1000 Rows`.

#### Restore Database Using SSMS

- Connect to the SQL Server instance.
- Right-click on `Databases` in the Object Explorer.
- Create a new database with the same name as the backup database.
- Right-click on the newly created database and select `Tasks` > `Restore` > `Database`.
- Click on `Device` and select the backup file.
- Click `OK` to restore the database.
- Change the database owner to the correct user if needed or use `sa` user.
- Refresh the database to view the restored data.

## DQL - Data Query Language

### SELECT Statement

- **Purpose**: Retrieves data from one or more tables.
- **Syntax**:

  ```sql
  SELECT column1, column2, ...
  FROM table_name
  WHERE condition;
  ```

- **Example**:

  ```sql
  SELECT
      first_name,
      last_name,
      email
  FROM
      sales.customers;
  ```

### Null Comparison

- Never to use `=` or `!=` to compare `NULL` values.
- Use `IS NULL` or `IS NOT NULL` instead.

### Aliases

- We can use alias names for columns using the `AS` keyword (e.g., `SELECT column_name AS alias_name`), also another way in sql server is to use `SELECT column_name = alias_name`.

**Example**:

```sql
SELECT
  first_name + ' ' + last_name AS full_name
FROM
  sales.customers;

-- another way
SELECT
  full_name = first_name + ' ' + last_name
FROM
  sales.customers;

-- using [] with alias names containing spaces
SELECT
  [full name] = first_name + ' ' + last_name
FROM
  sales.customers;
```

> [!Note]
>
> Concatenation Operator in example above using `+` Operator is valid because `first_name` and `last_name` are of type `VARCHAR`, but if we want to concatenate columns of type `INT` we should cast them to `VARCHAR` first.
>
> ```sql
>  SELECT
>    first_name + ' ' + last_name AS full_name,
>    first_name + last_name + CAST(age AS VARCHAR) AS username
>  FROM
>    sales.customers;
> ```

### Logical Operators in WHERE Clause

- **AND**: Returns rows where both conditions are true.
- **OR**: Returns rows where either condition is true.

**Example**:

```sql
SELECT
    first_name,
    last_name,
    email
FROM
  sales.customers
WHERE
    city = 'New York'
    AND state = 'NY';
```

## DML - Data Manipulation Language

### INSERT Statement

- **Purpose**: Adds new rows to a table.
- **Syntax**:

  ```sql
  INSERT INTO table_name (column1, column2, ...)
  VALUES (value1, value2, ...);
  ```

- **Example**:

  ```sql
  INSERT INTO sales.customers (first_name, last_name, email)
  VALUES ('John', 'Doe', 'john@example.com');
  ```

### UPDATE Statement

- **Purpose**: Modifies existing records in a table.
- **Syntax**:

  ```sql
  UPDATE table_name
  SET column1 = value1, column2 = value2, ...
  WHERE condition;
  ```

- **Example**:

  ```sql
  UPDATE sales.customers
  SET email = 'johndoe@example.com'
  WHERE first_name = 'John' AND last_name = 'Doe';
  ```

### DELETE Statement

- **Purpose**: Removes one or more rows from a table.
- **Syntax**:

  ```sql
  DELETE FROM table_name
  WHERE condition;
  ```

- **Example**:

  ```sql
  DELETE FROM sales.customers
  WHERE first_name = 'John' AND last_name = 'Doe';
  ```

## DDL - Data Definition Language

### Create Table Statement

- **Purpose**: Creates a new table in the database.
- **Syntax**:

  ```sql
  CREATE TABLE table_name (
      column1 datatype,
      column2 datatype,
      ...
  );
  ```

- **Example**:

  ```sql
  CREATE TABLE sales.customers (
      customer_id INT PRIMARY KEY,
      first_name VARCHAR(50),
      last_name VARCHAR(50),
      email VARCHAR(100)
  );
  ```

### Alter Table Statement

- **Purpose**: Modifies an existing table structure.
- **Syntax**:

  ```sql
  ALTER TABLE table_name
  ADD column_name datatype;
  ```

- **Example**:

  ```sql
  ALTER TABLE sales.customers
  ADD phone_number VARCHAR(20);
  ```

### Drop Table Statement

- **Purpose**: Removes a table from the database.
- **Syntax**:

  ```sql
  DROP TABLE table_name;
  ```

- **Example**:

  ```sql
  DROP TABLE sales.customers;
  ```
