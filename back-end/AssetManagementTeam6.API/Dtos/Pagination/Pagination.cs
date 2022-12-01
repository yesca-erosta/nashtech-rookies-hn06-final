using System.Diagnostics.CodeAnalysis;

namespace AssetManagementTeam6.API.Dtos.Pagination
{
    [ExcludeFromCodeCoverage]
    public class Pagination<T>
    {
        public IEnumerable<T>? Source { get; set; }
        public int TotalRecord { get; set; }
        public int TotalPage { get; set; }
        public PaginationQueryModel QueryModel { get; set; }
    }
}
