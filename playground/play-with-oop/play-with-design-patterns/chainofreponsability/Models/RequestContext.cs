namespace chainofreponsability.Models;

record RequestContext
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public string? Data { get; init; }
    public string? UserName { get; set; }
    public bool IsAuthenticated { get; set; } = false;
}
