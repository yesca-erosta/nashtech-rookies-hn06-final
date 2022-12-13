using AssetManagementTeam6.API.Dtos.Pagination;
using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Dtos.Responses;
using AssetManagementTeam6.API.ErrorHandling;
using AssetManagementTeam6.API.Services.Interfaces;
using AssetManagementTeam6.Data.Entities;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Common.Constants;
using Common.Enums;
using System.Dynamic;
using System.Net;

namespace AssetManagementTeam6.API.Services.Implements
{
    public class RequestForReturningService : IRequestForReturningService
    {
        private readonly IRequestForReturningRepository _requestForReturningRepository;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IUserRepository _userRepository;

        public RequestForReturningService(IRequestForReturningRepository requestForReturningRepository, IAssignmentRepository assignmentRepository, IUserRepository userRepository)
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
            var isValid = true;
            var listErrorAsset = new List<string>();
            var listErrorAssignment = new List<string>();
            var listErrorRequester = new List<string>();

            if (assignment == null)
            {
                isValid = false;
                listErrorAssignment.Add("Assignment is not exist or not accepted state");
            }
            else
            {
                if (assignment.Asset == null)
                {
                    isValid = false;
                    listErrorAsset.Add("Asset is not exist");
                }
                else
                {
                    if (assignment.Asset.State != AssetStateEnum.Assigned)
                    {
                        isValid = false;
                        listErrorAsset.Add("Asset is not assigned");
                    }
                }
            }

            if (requestBy == null)
            {
                isValid = false;
                listErrorRequester.Add("Requester is not found");
            }

            if (!isValid)
            {
                var error = new ExpandoObject() as IDictionary<string, object>;
                if (listErrorAsset.Any())
                    error.Add("Asset", listErrorAsset);
                if (listErrorAssignment.Any())
                    error.Add("Assignment", listErrorAssignment);
                if (listErrorRequester.Any())
                    error.Add("Requester", listErrorRequester);

                var myCustomException = new MyCustomException(HttpStatusCode.BadRequest, "Some properties are not valid")
                {
                    Error = error
                };

                throw myCustomException;
            }

            assignment.IsReturning = true;

            var request = new RequestForReturning
            {
                AssignmentId = createRequest.AssignmentId,
                Assignment = assignment!,
                RequestedById = createRequest.RequestedById,
                RequestedBy = requestBy!,
                AcceptedById = createRequest.AcceptedById,
                AcceptedBy = acceptedBy,
                State = RequestForReturningStateEnum.WaitingForReturning
            };

            return await _requestForReturningRepository.Create(request);
        }

        public bool GetFilterByReturnedDate(RequestForReturning r, PaginationQueryModel queryModel)
        {
            if (r.ReturnedDate.HasValue)
            {
                var resultDay = r.ReturnedDate.Value.Day;
                var resultMonth = r.ReturnedDate.Value.Month;
                var resultYear = r.ReturnedDate.Value.Year;

                return resultDay.Equals(queryModel.FilterByReturnedDates.Value.Day)
                    && resultMonth.Equals(queryModel.FilterByReturnedDates.Value.Month)
                    && resultYear.Equals(queryModel.FilterByReturnedDates.Value.Year);
            }
            return false;
        }

        public async Task<Pagination<GetRequestForReturningResponse?>> GetPagination(PaginationQueryModel queryModel)
        {
            // get all list
            var requests = await _requestForReturningRepository.GetListAsync();

            // search request for returning by assetcode or assetname or requester
            var nameToQuery = "";
            if (!string.IsNullOrEmpty(queryModel.ValueSearch))
            {
                nameToQuery = queryModel.ValueSearch.Trim().ToLower();

                requests = requests?.Where(u => u!.Assignment.Asset.AssetCode!.ToLower().Contains(nameToQuery) ||
                                        u!.Assignment.Asset.AssetName!.ToLower().Contains(nameToQuery) ||
                                        u!.RequestedBy.UserName!.ToLower().Contains(nameToQuery)).ToList();
            }

            //TODO: filter request for returning by state or returned date
            if (queryModel.RequestForReturningState != null)
            {
                requests = requests?.Where(r => queryModel.RequestForReturningState.Contains(r.State))?.ToList();
            }
            if (queryModel.FilterByReturnedDates != null)
            {
                requests = requests?.Where(r => GetFilterByReturnedDate(r, queryModel)).ToList();
            }

            // sorting
            var sortOption = queryModel.Sort ??= Constants.RequestForReturningIdAcsending;

            switch (sortOption)
            {
                case Constants.RequestForReturningIdAcsending:
                    requests = requests?.OrderBy(r => r.Id)?.ToList();
                    break;
                case Constants.RequestForReturningIdDescending:
                    requests = requests?.OrderByDescending(r => r.Id)?.ToList();
                    break;
                case Constants.RequestForReturningCodeAcsending:
                    requests = requests?.OrderBy(r => r.Assignment.Asset.AssetCode)?.ToList();
                    break;
                case Constants.RequestForReturningCodeDescending:
                    requests = requests?.OrderByDescending(r => r.Assignment.Asset.AssetCode)?.ToList();
                    break;
                case Constants.RequestForReturningNameAcsending:
                    requests = requests?.OrderBy(r => r.Assignment.Asset.AssetName)?.ToList();
                    break;
                case Constants.RequestForReturningNameDescending:
                    requests = requests?.OrderByDescending(r => r.Assignment.Asset.AssetName)?.ToList();
                    break;
                case Constants.RequestForReturningRequestedByAcsending:
                    requests = requests?.OrderBy(r => r.RequestedBy.UserName)?.ToList();
                    break;
                case Constants.RequestForReturningRequestedByDescending:
                    requests = requests?.OrderByDescending(r => r.RequestedBy.UserName)?.ToList();
                    break;
                case Constants.RequestForReturningAcceptedByAcsending:
                    requests = requests?.OrderBy(r => r.AcceptedBy!.UserName)?.ToList();
                    break;
                case Constants.RequestForReturningAcceptedByDescending:
                    requests = requests?.OrderByDescending(r => r.AcceptedBy!.UserName)?.ToList();
                    break;
                case Constants.RequestForReturningAssignedDateAcsending:
                    requests = requests?.OrderBy(r => r.Assignment.AssignedDate)?.ToList();
                    break;
                case Constants.RequestForReturningAssignedDateDescending:
                    requests = requests?.OrderByDescending(r => r.Assignment.AssignedDate)?.ToList();
                    break;
                case Constants.RequestForReturningStateAcsending:
                    requests = requests?.OrderBy(r => r.State)?.ToList();
                    break;
                case Constants.RequestForReturningStateDescending:
                    requests = requests?.OrderByDescending(r => r.State)?.ToList();
                    break;
                case Constants.RequestForReturningReturnedDateAcsending:
                    requests = requests?.OrderBy(r => r.ReturnedDate)?.ToList();
                    break;
                case Constants.RequestForReturningReturnedDateDescending:
                    requests = requests?.OrderByDescending(r => r.ReturnedDate)?.ToList();
                    break;
                default:
                    requests = requests?.OrderBy(r => r.Id)?.ToList();
                    break;
            }

            //paging
            var output = new Pagination<GetRequestForReturningResponse>();

            output.TotalRecord = requests.Count();

            var listRequests = requests.Select(ass => new GetRequestForReturningResponse(ass));

            output.Source = listRequests.Skip((queryModel.Page - 1) * queryModel.PageSize)
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

        public async Task<GetRequestForReturningResponse> CompleteReturningRequest(RequestForReturning request)
        {
            var isValid = true;
            var listErrorAsset = new List<string>();
            var listErrorAssignment = new List<string>();
            var listErrorRequestForReturning = new List<string>();
            var listErrorRequester = new List<string>();

            if (request.State != RequestForReturningStateEnum.WaitingForReturning)
            {
                isValid = false;
                listErrorRequestForReturning.Add("Request state is wrong");
            }

            if (request.Assignment == null)
            {
                isValid = false;
                listErrorAssignment.Add("Assignment is not exist");
            }
            else
            {
                if (request.Assignment.State != AssignmentStateEnum.Accepted)
                {
                    isValid = false;
                    listErrorAssignment.Add("Assignment state is not accepted");
                }

                if (request.Assignment.Asset == null)
                {
                    isValid = false;
                    listErrorAsset.Add("Asset is not exist");
                }
                else
                {
                    if (request.Assignment.Asset.State != AssetStateEnum.Assigned)
                    {
                        isValid = false;
                        listErrorAsset.Add("Asset is not assigned");
                    }
                }
            }

            if (request.RequestedBy == null)
            {
                isValid = false;
                listErrorRequester.Add("Requester is not found");
            }

            if (!isValid)
            {
                var error = new ExpandoObject() as IDictionary<string, object>;
                if (listErrorAsset.Any())
                    error.Add("Asset", listErrorAsset);
                if (listErrorAssignment.Any())
                    error.Add("Assignment", listErrorAssignment);
                if (listErrorRequester.Any())
                    error.Add("Requester", listErrorRequester);
                if (listErrorRequestForReturning.Any())
                {
                    error.Add("RequestForReturning", listErrorRequestForReturning);
                }

                var myCustomException = new MyCustomException(HttpStatusCode.BadRequest, "Some properties are not valid")
                {
                    Error = error
                };

                throw myCustomException;
            }

            request.State = RequestForReturningStateEnum.Completed;
            request.ReturnedDate = DateTime.UtcNow;
            request.Assignment!.Asset!.State = AssetStateEnum.Available;
            request.Assignment.State = AssignmentStateEnum.Deleted;

            await _assignmentRepository.Update(request.Assignment);

            var result = await _requestForReturningRepository.Update(request);

            return new GetRequestForReturningResponse(result);
        }

        public async Task<GetRequestForReturningResponse> CancelReturningRequest(RequestForReturning request)
        {
            var isValid = true;
            var listErrorAsset = new List<string>();
            var listErrorAssignment = new List<string>();
            var listErrorRequestForReturning = new List<string>();
            var listErrorRequester = new List<string>();

            if (request.State != RequestForReturningStateEnum.WaitingForReturning)
            {
                isValid = false;
                listErrorRequestForReturning.Add("Request state is wrong");
            }

            if (request.Assignment == null)
            {
                isValid = false;
                listErrorAssignment.Add("Assignment is not exist");
            }
            else
            {
                if (request.Assignment.State != AssignmentStateEnum.Accepted)
                {
                    isValid = false;
                    listErrorAssignment.Add("Assignment state is not accepted");
                }

                if (request.Assignment.Asset == null)
                {
                    isValid = false;
                    listErrorAsset.Add("Asset is not exist");
                }
                else
                {
                    if (request.Assignment.Asset.State != AssetStateEnum.Assigned)
                    {
                        isValid = false;
                        listErrorAsset.Add("Asset is not assigned");
                    }
                }
            }

            if (request.RequestedBy == null)
            {
                isValid = false;
                listErrorRequester.Add("Requester is not found");
            }

            if (!isValid)
            {
                var error = new ExpandoObject() as IDictionary<string, object>;
                if (listErrorAsset.Any())
                    error.Add("Asset", listErrorAsset);
                if (listErrorAssignment.Any())
                    error.Add("Assignment", listErrorAssignment);
                if (listErrorRequester.Any())
                    error.Add("Requester", listErrorRequester);
                if (listErrorRequestForReturning.Any())
                {
                    error.Add("RequestForReturning", listErrorRequestForReturning);
                }

                var myCustomException = new MyCustomException(HttpStatusCode.BadRequest, "Some properties are not valid")
                {
                    Error = error
                };

                throw myCustomException;
            }

            await _requestForReturningRepository.Delete(request);

            return new GetRequestForReturningResponse(request);
        }

        public async Task<RequestForReturning> GetRequestForReturningById(int id)
        {
            var result = await _requestForReturningRepository.GetOneAsync(r => r.Id == id);

            if (result == null)
            {
                return null!;
            }

            return result;
        }
    }
}
