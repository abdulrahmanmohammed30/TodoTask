using TodoTaskData.Entities;
using TodoTaskDTOS.Entities;

namespace TodoTaskApp.Mappers
{
    public static class TaskMapper
    {
        public static TaskDTO ToTaskDto(TodoTaskData.Entities.Task task) => new TaskDTO
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            CreatedAt = task.CreatedAt,
            DueDate = task.DueDate,
            Priority = task.Priority,
            Status = task.Status,
            Tags = task.TaskTags.Select(tt => tt.Tag.Name).ToList()
        };

        public static TodoTaskData.Entities.Task ToTask(TaskDTO dto) => new TodoTaskData.Entities.Task
        {
            Title = dto.Title,
            Description = dto.Description,
            DueDate = dto.DueDate,
            Priority = dto.Priority,
            Status = dto.Status,
            TaskTags = dto.TagIds.Select(tagId => new TaskTag
            {
                TagId = tagId
            }).ToList()
        };
    }
}
