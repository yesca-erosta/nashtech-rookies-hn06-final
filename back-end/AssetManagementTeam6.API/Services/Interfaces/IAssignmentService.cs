﻿using AssetManagementTeam6.API.Dtos.Pagination;
using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Dtos.Responses;
using AssetManagementTeam6.Data.Entities;
using Common.Enums;

namespace AssetManagementTeam6.API.Services.Interfaces
{
    public interface IAssignmentService
    {
        Task<Assignment?> Create(AssignmentRequest createRequest);
        Task<bool> GetAssignmentByAssignedUser(int assignedUserId);
        Task<bool> GetAssignmentByAssignedAsset(int assetId);
        Task<Assignment?> GetAssignmentById(int id);
        Task<IEnumerable<GetAssignmentResponse>> GetAllAsync();
        Task<Pagination<GetAssignmentResponse?>> GetPagination(PaginationQueryModel queryModel);
        Task<IEnumerable<GetAssetResponse>> CheckAvailableAsset();
        Task<IEnumerable<GetAssignmentResponse>> GetListAssignmentByUserLoggedIn(int id);
        Task<GetAssignmentResponse> AcceptedAssignment(int id);
        Task<GetAssignmentResponse> DeclinedAssignment(int id);
        Task<GetAssignmentResponse?> Update(int id, AssignmentRequest? updatedRequest);
        Task<bool> Delete(int id);
    }
}
