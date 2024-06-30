using Domain.Common;

namespace Domain.Entities
{
    public class GroupTeacher : BaseEntity
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
