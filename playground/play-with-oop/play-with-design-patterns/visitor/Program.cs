using visitor.Services;

Test(new NightShiftScheduleManagement());
Test(new DayShiftScheduleManagement());
Test(new RemoteWorkShiftScheduleManagement());



void Test(ScheduleManagement schedule)
{
    Console.WriteLine("=========================================");
    schedule.CalculateOverTime();
    schedule.Accept(new GenerateReportVisitor());
    schedule.Accept(new ManageLeaveRequestVisitor());
    Console.WriteLine("=========================================");
}