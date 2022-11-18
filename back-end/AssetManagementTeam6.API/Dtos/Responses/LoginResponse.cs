namespace AssetManagementTeam6.API.Dtos.Responses
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }
        public string? Token { get; set; }
    }
}
