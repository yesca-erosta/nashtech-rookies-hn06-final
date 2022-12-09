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
            var assignments = await _assignmentRepository.GetListAsync(x => x.State != AssignmentStateEnum.Deleted);
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

            if(asset == null)
            {
                throw new Exception("Asset is not exist");
            }

            if (asset.State == AssetStateEnum.Assigned)
            {
                throw new Exception("Asset is assigned for someone else");
            }

            if (asset.State != AssetStateEnum.Available)
            {
                throw new Exception("Asset is not available");
            }

            if(assignedTo == null)
            {
                throw new Exception("Assignee is not exist");
            }

            if(assignedBy == null)
            {
                throw new Exception("Assigner is not exist");
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
            var assignments = await _assignmentRepository.GetListAsync(x => x.State != AssignmentStateEnum.Deleted);

            return assignments.Select(ass => new GetAssignmentResponse(ass)).ToList();
        }

        public async Task<bool> GetAssignmentByAssignedAsset(int assetId)
        {
            var result = await _assignmentRepository.GetListAsync(x => x.AssetId == assetId && x.State != AssignmentStateEnum.Deleted);

            if (result.Count() == 0 || result == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> GetAssignmentByAssignedUser(int assignedUserId)
        {
            var result = await _assignmentRepository.GetListAsync(a => a.AssignedToId == assignedUserId && a.State != AssignmentStateEnum.Deleted);

            if (result.Count() == 0 || result == null)
            {
                return false;
            }           

            return true;
        }

        public async Task<IEnumerable<GetAssignmentResponse>> GetListAssignmentByUserLoggedIn(int id)
        {
            var assignments = await _assignmentRepository.GetListAsync(ass => ass.AssignedToId == id 
                                                                    && ass.State != AssignmentStateEnum.Deleted
                                                                    && ass.State != AssignmentStateEnum.Declined
                                                                    && ass.AssignedDate <= DateTime.UtcNow);

            return assignments.Select(ass => new GetAssignmentResponse(ass)).ToList();
        }

        public async Task<Assignment?> GetAssignmentById(int id)
        {
            return await _assignmentRepository.GetOneAsync(x => x.Id == id && x.State != AssignmentStateEnum.Deleted);

        }

        public async Task<Pagination<GetAssignmentResponse?>> GetPagination(PaginationQueryModel queryModel)
        {
            // get all list
            var assignments = await _assignmentRepository.GetListAsync(x => x.State != AssignmentStateEnum.Deleted);
           
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
            if(queryModel.FilterByAssignedDates != null)
            {
                assignments = assignments?.Where(u => u.AssignedDate.Day.Equals(queryModel.FilterByAssignedDates.Value.Day)
                                                    && u.AssignedDate.Month.Equals(queryModel.FilterByAssignedDates.Value.Month)
                                                    && u.AssignedDate.Year.Equals(queryModel.FilterByAssignedDates.Value.Year));
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

        public async Task<GetAssignmentResponse> AcceptedAssignment(int id)
        {
            var updatedAssignment = await _assignmentRepository.GetOneAsync(x => x.Id == id && x.State != AssignmentStateEnum.Deleted);

            if(updatedAssignment == null)
            {
                throw new Exception("Assignment is not exist");
            }

            if(updatedAssignment.State != AssignmentStateEnum.WaitingForAcceptance)
            {
                throw new Exception("Assignment state is not waiting for acceptance");
            }

            if(updatedAssignment.Asset == null)
            {
                throw new Exception("Asset is not exist");
            }

            if(updatedAssignment.Asset.State != AssetStateEnum.Available)
            {
                throw new Exception("Asset state not available");
            }

            updatedAssignment.Asset.State = AssetStateEnum.Assigned;

            updatedAssignment.State = AssignmentStateEnum.Accepted;

            var result = await _assignmentRepository.Update(updatedAssignment);

            return new GetAssignmentResponse(result!);
        }

        public async Task<GetAssignmentResponse> DeclinedAssignment(int id)
        {
            var updatedAssignment = await _assignmentRepository.GetOneAsync(x => x.Id == id && x.State != AssignmentStateEnum.Deleted);

            if (updatedAssignment == null)
            {
                throw new Exception("Assignment is not exist");
            }

            if (updatedAssignment.State != AssignmentStateEnum.WaitingForAcceptance)
            {
                throw new Exception("Assignment state is not waiting for acceptance");
            }

            if (updatedAssignment.Asset == null)
            {
                throw new Exception("Asset is not exist");
            }

            if (updatedAssignment.Asset.State != AssetStateEnum.Available)
            {
                throw new Exception("Asset state not available");
            }

            var assetState = updatedAssignment.Asset.State;

            updatedAssignment.Asset.State = AssetStateEnum.Available;
            
            updatedAssignment.State = AssignmentStateEnum.Declined;

            var result = await _assignmentRepository.Update(updatedAssignment);

            if (result == null)
            {
                return null!;
            }

            return new GetAssignmentResponse(result!);
        }

        public async Task<GetAssignmentResponse?> Update(int id, AssignmentRequest? updatedRequest)
        {
            var updatedAssignment = await _assignmentRepository.GetOneAsync(x => x.Id == id && x.State != AssignmentStateEnum.Deleted);
            var assignedTo = await _userRepository.GetOneAsync(x => x.Id == updatedRequest!.AssignedToId);
            var assignedBy = await _userRepository.GetOneAsync(x => x.Id == updatedRequest!.AssignedById);
            var updatedAsset = await _assetRepository.GetOneAsync(x => x.Id == updatedRequest!.AssetId );

            if(updatedAssignment == null)
            {
                throw new Exception("Assignment is not exist");
            }

            if(updatedAssignment.State != AssignmentStateEnum.WaitingForAcceptance)
            {
                throw new Exception("Invalid state");
            }

            if(updatedAsset == null)
            {
                throw new Exception("Asset is not exist");
            }

            if(updatedAsset.State != AssetStateEnum.Available)
            {
                throw new Exception("Asset not available");
            }

            if(assignedTo == null)
            {
                throw new Exception("Asignee is not exist");
            }
            
            updatedAssignment.Note = updatedRequest?.Note ?? "";
            updatedAssignment.AssignedDate = updatedRequest!.AssignedDate;
            updatedAssignment.AssetId = updatedRequest.AssetId;
            updatedAssignment.Asset = updatedAsset;
            updatedAssignment.AssignedToId = updatedRequest.AssignedToId;
            updatedAssignment.AssignedTo = assignedTo;
            updatedAssignment.AssignedById = updatedRequest.AssignedById;
            updatedAssignment.AssignedBy = assignedBy;
            
            await _assignmentRepository.Update(updatedAssignment);

            var result = new GetAssignmentResponse(updatedAssignment);

            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var deletedAssignment = await _assignmentRepository.GetOneAsync(x => x.Id == id && x.State != AssignmentStateEnum.Deleted);

            if (deletedAssignment == null)
            {
                throw new Exception("Assignment is not exist");
            }

            if(deletedAssignment.State != AssignmentStateEnum.WaitingForAcceptance || deletedAssignment.State != AssignmentStateEnum.Declined)
            {
                throw new Exception("Invalid state");
            }

            return await _assignmentRepository.Delete(deletedAssignment);
        }

    }
}
