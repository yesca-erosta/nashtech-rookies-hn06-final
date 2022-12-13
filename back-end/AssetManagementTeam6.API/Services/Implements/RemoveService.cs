using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.ErrorHandling;
using AssetManagementTeam6.API.Models;
using AssetManagementTeam6.API.Services.Interfaces;
using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Common.Enums;
using System.Dynamic;
using System.Net;

namespace AssetManagementTeam6.API.Services.Implements
{
    public class RemoveService : IRemoveService
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAssetRepository _assetRepository;

        public RemoveService(IAssignmentRepository assignmentRepository, IUserRepository userRepository, IAssetRepository assetRepository)
        {
            _assignmentRepository = assignmentRepository;
            _userRepository = userRepository;
            _assetRepository = assetRepository;
        }

        public async Task<Assignment> Create(AssignmentRequest createRequest)
        {

            var asset = await _assetRepository.GetOneAsync(asset => asset.Id == createRequest.AssetId);

            var assignedTo = await _userRepository.GetOneAsync(u => u.Id == createRequest.AssignedToId);

            var assignedBy = await _userRepository.GetOneAsync(u => u.Id == createRequest.AssignedById);

            var assetInAssignment = await _assignmentRepository.GetListAsync(a => a.State != AssignmentStateEnum.Deleted && a.AssetId == createRequest.AssetId);


            var isValid = true;
            var listErrorAsset = new List<string>();
            var listErrorAssignee = new List<string>();
            var listErrorAssigner = new List<string>();

            if (asset == null)
            {
                isValid = false;
                listErrorAsset.Add("Asset is not exist");
            }
            else
            {
                if (asset.State == AssetStateEnum.Assigned)
                {
                    isValid = false;
                    listErrorAsset.Add("Asset is assigned for someone else");
                }

                if (asset.State != AssetStateEnum.Available)
                {
                    isValid = false;
                    listErrorAsset.Add("Asset is not available");
                }
            }

            if (assetInAssignment.Any())
            {
                isValid = false;
                listErrorAsset.Add("Asset is existed in assignment");
            }

            if (assignedTo == null)
            {
                isValid = false;
                listErrorAssignee.Add("Assignee is not exist");
            }

            if (assignedBy == null)
            {
                isValid = false;
                listErrorAssigner.Add("Assigner is not exist");
            }

            if (!isValid)
            {
                var error  = new ExpandoObject() as IDictionary<string, object>;
                if (listErrorAsset.Any())
                    error.Add("Asset", listErrorAsset);
                if (listErrorAssigner.Any())
                    error.Add("Assigner", listErrorAssigner);
                if (listErrorAssignee.Any())
                    error.Add("Assignee", listErrorAssignee);

                var myCustomException = new MyCustomException(HttpStatusCode.BadRequest, "Some properties are not valid")
                {
                    Error = error
                };
                throw myCustomException;
            }

            var newAssignment = new Assignment
            {
                AssetId = createRequest.AssetId,
                Asset = asset!,
                AssignedBy = assignedBy,
                AssignedById = createRequest.AssignedById,
                AssignedDate = createRequest.AssignedDate,
                AssignedTo = assignedTo!,
                AssignedToId = createRequest.AssignedToId,
                Note = createRequest.Note,
                State = AssignmentStateEnum.WaitingForAcceptance
            };

            var createdAssignment = await _assignmentRepository.Create(newAssignment);

            if (createdAssignment == null)
                throw new MyCustomException(HttpStatusCode.InternalServerError, "Create new assignment failed");

            return createdAssignment;
        }

        public List<ExampleModelsClass> GetExportExcelData()
        {
            var output = new List<ExampleModelsClass>();
            for(int i = 0; i < 100; i++)
            {
                output.Add(new ExampleModelsClass()
                {
                    Name = $"Name {i+1}",
                    Age = 20 + i%10,
                    DoB = new DateTime(2000,1,1),
                    Score = 2 + i/20.0,
                    Pass = (2 + i / 20.0) > 4,
                    Remark = $"Remark {i+1} : Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, ...."
                });
            }
            return output;
        }

        public async Task<Assignment> Update(AssignmentRequest createRequest)
        {
            throw new MyCustomException(HttpStatusCode.Conflict, "The data has changed"); 
        }
    }
}
