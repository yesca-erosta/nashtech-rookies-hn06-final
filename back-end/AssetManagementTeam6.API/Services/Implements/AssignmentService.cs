using AssetManagementTeam6.API.Dtos.Pagination;
using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Dtos.Responses;
using AssetManagementTeam6.API.Services.Interfaces;
using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Common.Constants;
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

        public async Task<IEnumerable<GetAssetResponse>> CheckAvailableAsset()
        {
            var assetIds = new List<int>();

            var assignments = await _assignmentRepository.GetListAsync();

            var assignedAssets = assignments.Select(assignment => assignment.Asset.Id);

            foreach (var assetCode in assignedAssets)
            {
                assetIds.Add(assetCode!);
            }

            var assets = await _assetRepository.GetListAsync(asset => !assetIds.Contains(asset.Id));

            return assets.Select(asset => new GetAssetResponse(asset)).ToList();
        }

        public async Task<Assignment?> Create(AssignmentRequest createRequest)
        {
            var asset = await _assetRepository.GetOneAsync(asset => asset.Id == createRequest.AssetId);

            var assignedTo = await _userRepository.GetOneAsync(u => u.Id == createRequest.AssignedToId);

            var assignedBy = await _userRepository.GetOneAsync(u => u.Id == createRequest.AssignedById);

            var assetAssigned = await _assignmentRepository.GetOneAsync(ass => ass.AssetId == createRequest.AssetId);

            if (asset == null || assignedTo == null || assetAssigned != null || assignedBy == null)
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

        public async Task<IEnumerable<GetAssignmentResponse>> GetListByUserLoggedIn(int id)
        {
            var assignments = await _assignmentRepository.GetListAsync(ass => ass.AssignedToId == id);

            return assignments.Select(ass => new GetAssignmentResponse(ass)).ToList();
        }

        public async Task<Pagination<GetAssignmentResponse?>> GetPagination(PaginationQueryModel queryModel)
        {
            // get all list
            var assignments = await _assignmentRepository.GetListAsync();
           
            // search assignment by assetcode or assetname or assignee
            var nameToQuery = "";
            if (!string.IsNullOrEmpty(queryModel.ValueSearch))
            {
                nameToQuery = queryModel.ValueSearch.Trim().ToLower();

                assignments = assignments?.Where(u => u!.Asset.AssetCode!.ToLower().Contains(nameToQuery) ||
                                        u!.Asset.AssetName!.ToLower().Contains(nameToQuery) ||
                                        u!.AssignedTo.UserName!.ToLower().Contains(nameToQuery)).ToList();
            }

            //TODO: filter assignment by state or date
            if (queryModel.AssignmentStates != null)
            {
                assignments = assignments?.Where(u => queryModel.AssignmentStates.Contains(u.State))?.ToList();
            }

            // sorting
            var sortOption = queryModel.Sort ??= Constants.AssignmentIdAcsending;

            switch (sortOption)
            {
                case Constants.AssignmentIdAcsending:
                    assignments = assignments?.OrderBy(u => u.Id)?.ToList();
                    break;
                case Constants.AssignmentIdDescending:
                    assignments = assignments?.OrderByDescending(u => u.Id)?.ToList();
                    break;
                case Constants.AssignmentCodeAcsending:
                    assignments = assignments?.OrderBy(u => u.Asset.AssetCode)?.ToList();
                    break;
                case Constants.AssignmentCodeDescending:
                    assignments = assignments?.OrderByDescending(u => u.Asset.AssetCode)?.ToList();
                    break;
                case Constants.AssignmentNameAcsending:
                    assignments = assignments?.OrderBy(u => u.Asset.AssetName)?.ToList();
                    break;
                case Constants.AssignmentNameDescending:
                    assignments = assignments?.OrderByDescending(u => u.Asset.AssetName)?.ToList();
                    break;
                case Constants.AssignmentAssignedToAcsending:
                    assignments = assignments?.OrderBy(u => u.AssignedTo.UserName)?.ToList();
                    break;
                case Constants.AssignmentAssignedToDescending:
                    assignments = assignments?.OrderByDescending(u => u.AssignedTo.UserName)?.ToList();
                    break;
                case Constants.AssignmentAssignedByAcsending:
                    assignments = assignments?.OrderBy(u => u.AssignedBy!.UserName)?.ToList();
                    break;
                case Constants.AssignmentAssignedByDescending:
                    assignments = assignments?.OrderByDescending(u => u.AssignedBy!.UserName)?.ToList();
                    break;
                case Constants.AssignmentAssignedDateAcsending:
                    assignments = assignments?.OrderBy(u => u.AssignedDate)?.ToList();
                    break;
                case Constants.AssignmentAssignedDateDescending:
                    assignments = assignments?.OrderByDescending(u => u.AssignedDate)?.ToList();
                    break;
                case Constants.AssignmentStateAcsending:
                    assignments = assignments?.OrderBy(u => u.State)?.ToList();
                    break;
                case Constants.AssignmentStateDescending:
                    assignments = assignments?.OrderByDescending(u => u.State)?.ToList();
                    break;
                default:
                    assignments = assignments?.OrderBy(u => u.Id)?.ToList();
                    break;
            }


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
