using System.ComponentModel.DataAnnotations;
using TodoTask.CustomAttributeValidators;
using TodoTaskData.Entities;

namespace TodoTaskDTOS.Entities
{
    public class TaskDTO
    {
        public short Id { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters")]

        public string Title { get; set; }

        [MaxLength(500)]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DueDate("CreatedAt")]
        [Required(ErrorMessage = "Due Date is required")]
        [DataType(DataType.DateTime)]

        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "Priority is required")]
        public TaskPriority Priority { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public TodoTaskData.Entities.TaskStatus Status { get; set; } = TodoTaskData.Entities.TaskStatus.Pending;
        public List<string> Tags { get; set; } = new();
        public List<short> TagIds { get; set; } = new();
        
    }


}
