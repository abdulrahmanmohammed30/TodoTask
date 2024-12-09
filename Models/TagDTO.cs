using System.ComponentModel.DataAnnotations;

namespace TodoTaskDTOS.Entities
{
    public class TagDTO
    {
        public short Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }
    }
}
