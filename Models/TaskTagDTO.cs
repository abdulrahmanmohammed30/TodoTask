using System.ComponentModel.DataAnnotations;

namespace TodoTaskDTOS.Entities
{
    public class TaskTagDTO
    {
        [Required]
        public short TaskId { get; set; }

        [Required]
        public short TagId { get; set; }
    }
}
