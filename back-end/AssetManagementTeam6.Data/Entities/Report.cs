namespace AssetManagementTeam6.Data.Entities
{
    public class Report : BaseEntity<int>
    {
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public int Total { get; set; }
        public int Assigned { get; set; }
        public int Available { get; set; }
        public int NotAvailable { get; set; }
        public int WaitingForRecycling { get; set; }
        public int Recycled { get; set; }
    }
}
