﻿using Common.Constants;
using Common.Enums;

namespace AssetManagementTeam6.API.Dtos.Pagination
{
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
        public string? StaffCode { get; set; }
        public string? Name { get; set; }

        // filter 
        public StaffEnum? Type { get; set; }

        //sorting
        public string? Sort { get; set; }
    }
}