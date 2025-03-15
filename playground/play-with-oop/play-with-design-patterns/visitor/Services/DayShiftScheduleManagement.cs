namespace visitor.Services;

class DayShiftScheduleManagement : ScheduleManagement
{
    public override void Accept(IScheduleManagementVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override void CalculateOverTime()
    {
        Console.WriteLine("Calculating OverTime for DayShift...");
    }
}
