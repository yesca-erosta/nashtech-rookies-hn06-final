using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.Data.Entities;

namespace AssetManagementTeam6.API.Services.Interfaces
{
    public interface IAssignmentService
    {
        Task<Assignment?> Create(AssignmentRequest createRequest);
    }
}
