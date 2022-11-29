using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Services.Interfaces;
using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;

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

            var assignedTo = await _userRepository.GetOneAsync(u => u.Id == createRequest.AssignedToID);

           var assignedBy = await _userRepository.GetOneAsync(u => u.Id == createRequest.AssignedById);

            if(asset == null || assignedTo == null)
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
                AssignedToID = createRequest.AssignedToID,
                Note = createRequest.Note,
                State = createRequest.State
            };

            var createdAssignment = await _assignmentRepository.Create(newAssignment);

            if (createdAssignment == null)
            {
                return null;
            }

            return  createdAssignment;
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
            var result = await _assignmentRepository.GetOneAsync(a => a.AssignedToID == assignedUserId);
            if (result == null)
            {
                return null!;
            }

            return result;
        }
    }
}
