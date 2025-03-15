namespace visitor.Services;

interface IScheduleManagementVisitor
{
    void Visit(DayShiftScheduleManagement schedule);
    void Visit(NightShiftScheduleManagement schedule);
    void Visit(RemoteWorkShiftScheduleManagement schedule);
}
