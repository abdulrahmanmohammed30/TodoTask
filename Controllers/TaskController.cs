using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoTask.CustomModelBinders;
using TodoTaskApp.Mappers;
using TodoTaskData.Repository;
using TodoTaskDTOS.Entities;

namespace TodoTask.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskRepository _repository;

        public TaskController(ITaskRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            var tasks = await _repository.GetAllAsync();
            var taskDtos = tasks.Select(TaskMapper.ToTaskDto).ToList();
            return View(taskDtos);
        }

        [HttpGet("tasks/high-priority")]
        public async Task<IActionResult> GetHighPriorityTasks()
        {
            var tasks = await _repository.GetAllAsync();
            var taskDtos = tasks.Select(TaskMapper.ToTaskDto).ToList();
            return PartialView("~/Views/Shared/_HighPriorityPartialView.cshtml",taskDtos);
        }


        [HttpGet("tasks/create")]
        public IActionResult Create() => View();

        [HttpPost("tasks/create")]
        public async Task<IActionResult> Create(string title, string description)
        {
            TaskDTO dto = new TaskDTO();
            if (!ModelState.IsValid) return View(dto);
            if (ModelState.IsValid)
            {
            var task = TaskMapper.ToTask(dto);
            await _repository.CreateAsync(task);
            return RedirectToAction(nameof(Index));

            }
            return View(dto);

        }

        [HttpGet("tasks/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null) return NotFound();

            var taskDto = TaskMapper.ToTaskDto(task);
            return View(taskDto);
        }

        [HttpGet("tasks/edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null) return NotFound();
            var taskDto = TaskMapper.ToTaskDto(task);
            return View(taskDto);
        }

        [HttpPost("tasks/edit/{id}")]
        public async Task<IActionResult> Edit(int id, [ModelBinder(BinderType = typeof(TaskCustomModelBinder))] TaskDTO dto)
        {
            if (id != dto.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync(TaskMapper.ToTask(dto));
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _repository.ExistsAsync(id)) return NotFound();
                    throw;
                }
            }
            return View(dto);
        }

        [HttpGet("tasks/search")]
        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return NotFound("SearchTerm cannot be null or empty");

            var tasks=await _repository.GetByName(searchTerm);
            var tasksDtos=tasks.Select(TaskMapper.ToTaskDto).ToList();
            return View(tasksDtos);
        }

        [HttpGet("tasks/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            
            if (task == null) return NotFound();
                var taskDto = TaskMapper.ToTaskDto(task);
            return View(taskDto);
        }

        [HttpPost("tasks/delete/{id}", Name = "DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!await _repository.ExistsAsync(id)) return NotFound();

            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

