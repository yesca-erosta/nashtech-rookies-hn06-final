using AssetManagementTeam6.API.Dtos.Pagination;
using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Dtos.Responses;
using AssetManagementTeam6.API.Services.Interfaces;
using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Common.Enums;

namespace AssetManagementTeam6.API.Services.Implements
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAssetRepository _assetRepository;

        public AssignmentService(IAssignmentRepository assignmentRepository, IUserRepository userRepository, IAssetRepository assetRepository)
        {
            _assignmentRepository = assignmentRepository;
            _userRepository = userRepository;
            _assetRepository = assetRepository;
        }

        public async Task<Assignment?> Create(AssignmentRequest createRequest)
        {
            var asset = await _assetRepository.GetOneAsync(asset => asset.Id == createRequest.AssetId);

            var assignedTo = await _userRepository.GetOneAsync(u => u.Id == createRequest.AssignedToId);

            var assignedBy = await _userRepository.GetOneAsync(u => u.Id == createRequest.AssignedById);

            var assetAssigned = await _assignmentRepository.GetOneAsync(ass => ass.AssetId == createRequest.AssetId);

            if (asset == null || assignedTo == null || assetAssigned != null)
            {
                return null;
            }

            var newAssignment = new Assignment
            {
                AssetId = createRequest.AssetId,
                Asset = asset,
                AssignedBy = assignedBy,
                AssignedById = createRequest.AssignedById,
                AssignedDate = createRequest.AssignedDate,
                AssignedTo = assignedTo,
                AssignedToId = createRequest.AssignedToId,
                Note = createRequest.Note,
                State = AssignmentStateEnum.WaitingForAcceptance
            };

            var createdAssignment = await _assignmentRepository.Create(newAssignment);

            if (createdAssignment == null)
            {
                return null;
            }

            return createdAssignment;
        }

        public async Task<IEnumerable<GetAssignmentResponse>> GetAllAsync()
        {
            var assignments = await _assignmentRepository.GetListAsync();

            return assignments.Select(ass => new GetAssignmentResponse(ass)).ToList();
        }

        public async Task<Assignment> GetAssignmentByAssignedAsset(int assetId)
        {
            var result = await _assignmentRepository.GetOneAsync(a => a.AssetId == assetId);

            if (result == null)
            {
                return null!;
            }

            return result;
        }

        public async Task<Assignment> GetAssignmentByAssignedUser(int assignedUserId)
        {
            var result = await _assignmentRepository.GetOneAsync(a => a.AssignedToId == assignedUserId);
            if (result == null)
            {
                return null!;
            }

            return result;
        }

        public async Task<Pagination<GetAssignmentResponse?>> GetPagination(PaginationQueryModel queryModel)
        {
            var assignments = await _assignmentRepository.GetListAsync();

            var output = new Pagination<GetAssignmentResponse>();

            output.TotalRecord = assignments.Count();

            var listAssignments = assignments.Select(ass => new GetAssignmentResponse(ass));

            output.Source = listAssignments.Skip((queryModel.Page - 1) * queryModel.PageSize)
                                   .Take(queryModel.PageSize)
                                   .ToList();
            output.TotalPage = (output.TotalRecord - 1) / queryModel.PageSize + 1;

            if (queryModel.Page > output.TotalPage)
            {
                queryModel.Page = output.TotalPage;
            }

            output.QueryModel = queryModel;

            return output!;
        }
        public async Task<GetAssignmentResponse?> Update(int id, AssignmentRequest? updatedRequest)
        {
            var updatedAssignment = await _assignmentRepository.GetOneAsync(x => x.Id == id);
            var assignedTo = await _userRepository.GetOneAsync(x => x.Id == updatedRequest.AssignedToId);
            var assignedBy = await _userRepository.GetOneAsync(x => x.Id == updatedRequest.AssignedById);
            var updateAsset = await _assetRepository.GetOneAsync(x => x.Id == updatedRequest.AssetId);

            if (updatedAssignment == null) return null;
            if (assignedTo == null) return null;
            if (assignedBy == null) return null;
            if (updateAsset == null) return null;

            {
                updatedAssignment.Note = updatedRequest.Note;
                updatedAssignment.AssignedDate = updatedRequest.AssignedDate;
                updatedAssignment.AssetId = updatedRequest.AssetId;
                updatedAssignment.Asset = updateAsset;
                updatedAssignment.AssignedToId = updatedRequest.AssignedToId;
                updatedAssignment.AssignedTo = assignedTo;
                updatedAssignment.AssignedById = updatedRequest.AssignedById;
                updatedAssignment.AssignedBy = assignedBy;
            }

            await _assignmentRepository.Update(updatedAssignment);
            
            var result = new GetAssignmentResponse(updatedAssignment);

            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var deletedAssignment = await _assignmentRepository.GetOneAsync(x => x.Id == id);

            if (deletedAssignment == null)
            {
                return false;
            }

            return await _assignmentRepository.Delete(deletedAssignment);
        }

        public async Task<Assignment?> GetAssignmentById(int id)
        {
            return await _assignmentRepository.GetOneAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<GetAssetResponse>> GetAllAssignedAsset()
        {
            var assignment = await _assignmentRepository.GetListAsync();
            var asset = assignment.Select(x => new GetAssetResponse(x.Asset)).ToList();
            return asset;
        }

        public async Task<IEnumerable<GetUserResponse>> GetAllAssignedUser()
        {
            var assignment = await _assignmentRepository.GetListAsync();
            var user = assignment.Select(x => new GetUserResponse(x.AssignedTo)).ToList();
            return user;
        }
    }
}
