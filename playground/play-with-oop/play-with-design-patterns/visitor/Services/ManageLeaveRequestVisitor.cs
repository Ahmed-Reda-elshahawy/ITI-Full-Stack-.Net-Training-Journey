namespace visitor.Services;

class ManageLeaveRequestVisitor : IScheduleManagementVisitor
{
    public void Visit(DayShiftScheduleManagement schedule)
    {
        Console.WriteLine($"Managing leave request for {schedule.GetType().Name}!");
    }

    public void Visit(NightShiftScheduleManagement schedule)
    {
        Console.WriteLine($"Managing leave request for {schedule.GetType().Name}!");
    }

    public void Visit(RemoteWorkShiftScheduleManagement schedule)
    {
        Console.WriteLine($"Managing leave request for {schedule.GetType().Name}!");
    }
}
