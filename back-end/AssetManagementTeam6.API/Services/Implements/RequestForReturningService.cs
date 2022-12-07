using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Services.Interfaces;
using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Common.Enums;

namespace AssetManagementTeam6.API.Services.Implements
{
    public class RequestForReturningService : IRequestForReturningService
    {
        private readonly IRequestForReturningRepository _requestForReturningRepository;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IUserRepository _userRepository;

        public RequestForReturningService(IRequestForReturningRepository requestForReturningRepository, IAssignmentRepository assignmentRepository,IUserRepository userRepository)
        {
            _requestForReturningRepository = requestForReturningRepository;
            _assignmentRepository = assignmentRepository;
            _userRepository = userRepository;
        }

        public async Task<RequestForReturning?> Create(RequestForReturningRequest createRequest)
        {
            var assignment = await _assignmentRepository.GetOneAsync(assignment => assignment.Id == createRequest.AssignmentId 
                                                                    && assignment.State == AssignmentStateEnum.Accepted);

            var requestBy = await _userRepository.GetOneAsync(u => u.Id == createRequest.RequestedById);

            var acceptedBy = await _userRepository.GetOneAsync(u => u.Id == createRequest.AcceptedById);

            if (assignment == null || requestBy == null)
            {
                return null;
            }

            var request = new RequestForReturning
            {
                AssignmentId = createRequest.AssignmentId,
                Assignment = assignment,
                RequestedById = createRequest.RequestedById,
                RequestedBy = requestBy,
                AcceptedById = createRequest.AcceptedById,
                AcceptedBy = acceptedBy,
                State = RequestForReturningStateEnum.WaitingForReturning
            };

            var createdRequest = await _requestForReturningRepository.Create(request);

            if (createdRequest == null)
            {
                return null;
            }

            return createdRequest;

        }
    }
}
