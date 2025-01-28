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
  - Use the combination of these foreign keys as a **composite primary key**.

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
