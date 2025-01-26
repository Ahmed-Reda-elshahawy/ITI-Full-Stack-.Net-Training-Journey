# 🔖 ITI - D0028 - C Sharp - Basics (Part8)

> 📖 is used for notes covered in the lecture, 💡 for extra interesting notes.

## Windows Forms

- We can add our own custom controls (User defined controls) to the toolbox.
- Each control have some properties, events

### TrackBar Control

- The `TrackBar` control is used to select a value from a range of values.

## Connect Windows Forms with Database

### Steps to Deal with DB: Using ADO

- Create DB
- Connect to database using `ConnectionString`
- Bind command with connection
- Open connection
- Execute command
- Close connection

> [!Note]
>
> - Above steps have `open` and `close` connection steps so it's called `Connected Architecture`.
> - We can also use `Disconnected Architecture` where we don't need to open and close connection.

### key points

- `SqlConnection` is used to connect to SQL Server.
- `SqlCommand` is used to execute SQL commands.
- `SqlDataReader` is used to read data from the database (based on `Connected Architecture`).
- `SqlDataAdapter` is used to fill a `DataSet` with data from the database (based on `Disconnected Architecture`).
- `DataSet` is used to store data from the database (mapped to `Database` in database).
- `DataTable` is used to store data from the database (mapped to `Table` in database).
- `DataRow` is used to store a row of data from a `DataTable`.
- `DataCell` is used to store a cell of data from a `DataRow`.

**Example**:

```csharp
const con = new SqlConnection("Data Source=.;Initial Catalog=MyDB;Integrated Security=True");

SqlCommand cmd = new SqlCommand("SELECT * FROM MyTable where id=@id");

cmd.Parameters.AddWithValue("@id", 1);

cmd.Connection = con;

con.Open();

SqlDataReader dr = cmd.ExecuteReader();

if(dr.Read()) // or use dr.HasRows
{
    dr.GetString(0); // Get the first column value
    dr.GetString(1); // Get the second column value

    // or use column name instead of index
    // dr.GetString("name");

    // or use indexer instead of GetString
} else {
    // No data found for this id
}
con.Close();
```

**Example: Using `SqlDataAdapter`**

```csharp
const con = new SqlConnection("Data Source=.;Initial Catalog=MyDB;Integrated Security=True");

SqlCommand cmd = new SqlCommand("SELECT * FROM MyTable where id=@id");

cmd.Parameters.AddWithValue("@id", 1);

cmd.Connection = con;

con.Open();

SqlDataAdapter da = new SqlDataAdapter(cmd);

DataTable dt = new DataTable();

da.Fill(dt);

if (dt.Rows.Count > 0)
{
    DataRow row = dt.Rows[0];

    row["name"]; // Get the value of name column
    row["age"]; // Get the value of age column
} else {
    // No data found for this id
}

con.Close();
```
