using System.ComponentModel.DataAnnotations;
using TodoTaskData.Entities;

namespace TodoTaskDTOS.Entities
{
    public class TaskDTO
    {
        public short Id { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? DueDate { get; set; }

        [Required]
        public TaskPriority Priority { get; set; }

        [Required]
        public TodoTaskData.Entities.TaskStatus Status { get; set; } = TodoTaskData.Entities.TaskStatus.Pending;
        public List<string> Tags { get; set; } = new();
        public List<short> TagIds { get; set; } = new();
        
    }


}
