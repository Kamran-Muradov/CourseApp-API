namespace Service.DTOs.Admin.Groups
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int StudentCount { get; set; }
        public IEnumerable<string> Teachers { get; set; }
        public string Room { get; set; }
        public string Education { get; set; }
    }
}
