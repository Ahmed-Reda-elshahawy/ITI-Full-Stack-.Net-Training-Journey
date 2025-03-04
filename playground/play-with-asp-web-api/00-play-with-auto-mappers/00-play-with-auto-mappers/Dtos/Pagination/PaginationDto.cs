using System.Text.Json.Serialization;

namespace _00_play_with_auto_mappers.Dtos.Pagination;

public class PaginationDto
{
    public PaginationDto(int page, int pageSize, int totalCount)
    {
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public int Page { get; init; }
    public int PageSize { get; init; }

    public int TotalPages { get => (int)Math.Ceiling((double)TotalCount / PageSize); }

    [JsonIgnore]
    public int TotalCount { get; init; }

    [JsonIgnore]
    public int Take { get => PageSize; }

    [JsonIgnore]
    public int Skip { get => (Page - 1) * PageSize; }
}
