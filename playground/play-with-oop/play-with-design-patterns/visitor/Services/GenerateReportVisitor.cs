namespace visitor.Services;

class GenerateReportVisitor : IScheduleManagementVisitor
{
    public void Visit(DayShiftScheduleManagement schedule)
    {
        Console.WriteLine($"Genearting report for {schedule.GetType().Name}!");
    }

    public void Visit(NightShiftScheduleManagement schedule)
    {
        Console.WriteLine($"Genearting report for {schedule.GetType().Name}!");
    }

    public void Visit(RemoteWorkShiftScheduleManagement schedule)
    {
        Console.WriteLine($"Genearting report for {schedule.GetType().Name}!");
    }
}
