using AssetManagementTeam6.Data.Entities;
using Common.Enums;

namespace AssetManagementTeam6.API.Dtos.Responses
{
    public class GetRequestForReturningResponse
    {
        public Assignment Assignment { get; set; }
        public User RequestedBy { get; set; }
        public User? AcceptedBy { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public RequestForReturningStateEnum State { get; set; }

        public GetRequestForReturningResponse(RequestForReturning request)
        {
            Assignment = request.Assignment;
            RequestedBy = request.RequestedBy;
            AcceptedBy = request.AcceptedBy;
            ReturnedDate = request.ReturnedDate;
            State = request.State;
        }
    }
}
