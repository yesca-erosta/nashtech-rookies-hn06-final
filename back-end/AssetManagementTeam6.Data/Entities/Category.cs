using System.ComponentModel.DataAnnotations;

namespace AssetManagementTeam6.Data.Entities
{
    public class Category : BaseEntity<int>
    {
        // TODO: Have confirm name
        [Required]
        public string? NameCategory { get; set; }
    }
}
