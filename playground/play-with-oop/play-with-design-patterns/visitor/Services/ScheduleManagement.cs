namespace visitor.Services;

abstract class ScheduleManagement
{
    public abstract void CalculateOverTime();
    public abstract void Accept(IScheduleManagementVisitor visitor);
}
