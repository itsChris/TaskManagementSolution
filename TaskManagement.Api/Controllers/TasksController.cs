using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Api.Services;
using TaskManagement.Shared.Models;

namespace TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TasksController> _logger;

        public TasksController(ITaskService taskService, ILogger<TasksController> logger)
        {
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                _logger.LogInformation("Fetching all tasks. User: {User}", User?.Identity?.Name);
                var tasks = await _taskService.GetTasksAsync();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all tasks.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching task with ID {TaskId}.", id);
                var task = await _taskService.GetTaskByIdAsync(id);

                if (task == null)
                {
                    _logger.LogWarning("Task with ID {TaskId} not found.", id);
                    return NotFound($"Task with ID {id} not found.");
                }

                return Ok(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the task with ID {TaskId}.", id);
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskDto taskDto)
        {
            if (taskDto == null)
            {
                _logger.LogWarning("Invalid task data received.");
                return BadRequest("Task data is invalid.");
            }

            try
            {
                _logger.LogInformation("Creating a new task.");
                var createdTask = await _taskService.CreateTaskAsync(taskDto);
                return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new task.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskDto taskDto)
        {
            if (taskDto == null || taskDto.Id != id)
            {
                _logger.LogWarning("Invalid task data received for update.");
                return BadRequest("Task data is invalid or does not match the provided ID.");
            }

            try
            {
                _logger.LogInformation("Updating task with ID {TaskId}.", id);
                var updatedTask = await _taskService.UpdateTaskAsync(taskDto);

                if (updatedTask == null)
                {
                    _logger.LogWarning("Task with ID {TaskId} not found for update.", id);
                    return NotFound($"Task with ID {id} not found.");
                }

                return Ok(updatedTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the task with ID {TaskId}.", id);
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                _logger.LogInformation("Deleting task with ID {TaskId}.", id);
                var deleted = await _taskService.DeleteTaskAsync(id);

                if (!deleted)
                {
                    _logger.LogWarning("Task with ID {TaskId} not found for deletion.", id);
                    return NotFound($"Task with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the task with ID {TaskId}.", id);
                return StatusCode(500, "An internal server error occurred.");
            }
        }
    }
}
