using Microsoft.AspNetCore.Mvc.ModelBinding;
using TodoTaskData.Entities;
using TodoTaskDTOS.Entities;

namespace TodoTask.CustomModelBinders
{
    public class TaskCustomModelBinder : IModelBinder
    {
        public System.Threading.Tasks.Task BindModelAsync(ModelBindingContext bindingContext)
        {
           var dto=new TaskDTO();

            var titleObj = bindingContext.ValueProvider.GetValue("Title"); 
            if (titleObj != ValueProviderResult.None &&
                titleObj.FirstValue!=null)
            {
                dto.Title = titleObj.FirstValue.ToString();
            }

            var descriptionObj = bindingContext.ValueProvider.GetValue("Description");
            if(descriptionObj != ValueProviderResult.None &&
                descriptionObj.FirstValue != null)
            {
                dto.Description=descriptionObj.FirstValue.ToString();
            }
            var priorityObj = bindingContext.ValueProvider.GetValue("Priority");
            if (priorityObj != ValueProviderResult.None && priorityObj.FirstValue != null
                && Enum.TryParse(typeof(TaskPriority), priorityObj.FirstValue, true, out var priority))
            {
                dto.Priority = (TaskPriority)priority;
            }
            else
            {
                bindingContext.ModelState.AddModelError("Priority", "Invalid priority value.");
            }

            var tagsObj= bindingContext.ValueProvider.GetValue("tags");
            if (priorityObj != ValueProviderResult.None && tagsObj.FirstValue != null
               && tagsObj is object tagsInstanceObj && tagsInstanceObj is List<string> tags)
            {
                dto.Tags = tags;
            }
            var dueDateObj = bindingContext.ValueProvider.GetValue("DueDate");
            if (dueDateObj != ValueProviderResult.None && dueDateObj.FirstValue != null)
            {
                if (DateTime.TryParse(dueDateObj.FirstValue, out var dueDate))
                {
                    dto.DueDate = dueDate;
                }
                else
                {
                    bindingContext.ModelState.AddModelError("DueDate", "Invalid due date value.");
                }
            }

            bindingContext.Result = ModelBindingResult.Success(dto);
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
