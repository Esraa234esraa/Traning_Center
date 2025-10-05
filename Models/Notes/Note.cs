using System.ComponentModel.DataAnnotations.Schema;
using TrainingCenterAPI.Models.BaseEntitys;
using TrainingCenterAPI.Models.Students;

namespace TrainingCenterAPI.Models.Notes
{
    public class Note : BaseEntity
    {
        public required string Description { get; set; }
        public Guid CurrentStudentId { get; set; }
        [ForeignKey(nameof(CurrentStudentId))]
        public CurrentStudent CurrentStudent { get; set; }
    }
}
