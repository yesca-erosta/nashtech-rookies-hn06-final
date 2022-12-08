﻿using AssetManagementTeam6.Data.Entities;
using Common.Enums;

namespace AssetManagementTeam6.API.Dtos.Responses
{
    public class GetAssignmentResponse
    {
        public int Id { get; set; }
        public string AssetCode { get; set; }
        public string AssetName { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedBy { get; set; }
        public DateTime AssignedDate { get; set; }
        public AssignmentStateEnum State { get; set; }

        public string Note { get; set; }

        public string FullName { get; set; }

        public int userId { get; set; }

        public int assetId { get; set; }

        public GetAssignmentResponse(Assignment assignment)
        {
            Id = assignment.Id;
            AssetCode = assignment.Asset.AssetCode!;
            AssetName = assignment.Asset.AssetName!;
            AssignedTo = assignment.AssignedTo.UserName;
            AssignedBy = assignment.AssignedBy?.UserName ?? "";
            AssignedDate = assignment.AssignedDate;
            State = assignment.State;
            Note = assignment.Note!;
            FullName = assignment.AssignedTo.FullName!;
            userId = assignment.AssignedToId;
            assetId = assignment.AssetId;

        }
    }
}
