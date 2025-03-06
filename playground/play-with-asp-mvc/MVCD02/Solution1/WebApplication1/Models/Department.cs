using WebApplication1.Models;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int Capacity { get; set; }

    List<Student> Students { get; set; }
}
