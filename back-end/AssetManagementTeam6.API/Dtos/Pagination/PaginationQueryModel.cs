using Common.Constants;
using Common.Enums;
using System.Diagnostics.CodeAnalysis;

namespace AssetManagementTeam6.API.Dtos.Pagination
{
    [ExcludeFromCodeCoverage]
    public class PaginationQueryModel
    {
        private int _page;
        private int _pageSize;

        public int Page
        {
            get => _page;
            set => _page = value > 0 ? value : Constants.DefaultPage;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value >= 10 ? value : Constants.DefaultPageSize;
        }

        // searching
        public string? ValueSearch { get; set; }

        // filter 
        public List<StaffEnum>? Types { get; set; }
        public List<string>? Categories { get; set; }
        public List<AssetStateEnum>? AssetStates { get; set; }
        public List<AssignmentStateEnum>? AssignmentStates { get; set; }
        //TODO
        public DateTime? FilterByAssignedDates { get; set; }

        //sorting
        public string? Sort { get; set; }
    }
}
