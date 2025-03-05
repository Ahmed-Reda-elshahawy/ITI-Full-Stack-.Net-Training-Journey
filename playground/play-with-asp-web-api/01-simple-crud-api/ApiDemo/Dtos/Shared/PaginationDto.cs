using System.Text.Json.Serialization;

namespace ApiDemo.Dtos.Shared;

public class PaginationDto(int page, int pageSize, int totalCount)
{
    public int Page { get; } = Math.Max(page, 1);
    public int PageSize { get; } = Math.Max(pageSize, 1);
    public int TotalPages  => (int)Math.Ceiling((double)TotalCount / PageSize);

    [JsonIgnore]
    public int TotalCount { get; } = Math.Max(totalCount, 0);

    [JsonIgnore]
    public int Take => PageSize;

    [JsonIgnore]
    public int Skip => (Page - 1) * PageSize;
}
