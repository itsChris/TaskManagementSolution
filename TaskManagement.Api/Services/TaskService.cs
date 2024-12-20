using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TaskManagement.Api.Data;
using TaskManagement.Api.Models;
using TaskManagement.Shared.Models;

namespace TaskManagement.Api.Services
{
    public class TaskService : ITaskService
    {
        private readonly IEntityRepository<TaskEntity> _repository;
        private readonly ILogger<TaskService> _logger;

        public TaskService(IEntityRepository<TaskEntity> repository, ILogger<TaskService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<TaskDto>> GetTasksAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all tasks.");
                var tasks = await _repository.GetAllAsync();
                return tasks.Select(MapToDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all tasks.");
                throw new InvalidOperationException("Could not fetch tasks.", ex);
            }
        }

        public async Task<TaskDto> GetTaskByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Fetching task with ID {TaskId}.", id);
                var task = await _repository.GetByIdAsync(id);

                if (task == null)
                {
                    _logger.LogWarning("Task with ID {TaskId} not found.", id);
                    return null;
                }

                return MapToDto(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching task with ID {TaskId}.", id);
                throw new InvalidOperationException($"Could not fetch task with ID {id}.", ex);
            }
        }

        public async Task<TaskDto> CreateTaskAsync(TaskDto taskDto)
        {
            if (taskDto == null)
            {
                _logger.LogWarning("Invalid task data received for creation.");
                throw new ArgumentNullException(nameof(taskDto), "Task data cannot be null.");
            }

            try
            {
                _logger.LogInformation("Creating a new task.");
                var task = MapToEntity(taskDto);
                var createdTask = await _repository.AddAsync(task);
                return MapToDto(createdTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new task.");
                throw new InvalidOperationException("Could not create task.", ex);
            }
        }

        public async Task<TaskDto> UpdateTaskAsync(TaskDto taskDto)
        {
            if (taskDto == null)
            {
                _logger.LogWarning("Invalid task data received for update.");
                throw new ArgumentNullException(nameof(taskDto), "Task data cannot be null.");
            }

            try
            {
                _logger.LogInformation("Updating task with ID {TaskId}.", taskDto.Id);
                var task = MapToEntity(taskDto);
                var updatedTask = await _repository.UpdateAsync(task);

                if (updatedTask == null)
                {
                    _logger.LogWarning("Task with ID {TaskId} not found for update.", taskDto.Id);
                    return null;
                }

                return MapToDto(updatedTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating task with ID {TaskId}.", taskDto.Id);
                throw new InvalidOperationException($"Could not update task with ID {taskDto.Id}.", ex);
            }
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting task with ID {TaskId}.", id);
                var result = await _repository.DeleteAsync(id);

                if (!result)
                {
                    _logger.LogWarning("Task with ID {TaskId} not found for deletion.", id);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting task with ID {TaskId}.", id);
                throw new InvalidOperationException($"Could not delete task with ID {id}.", ex);
            }
        }

        private static TaskDto MapToDto(TaskEntity task)
        {
            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                CreatedAt = task.CreatedAt,
                DueDate = task.DueDate
            };
        }

        private static TaskEntity MapToEntity(TaskDto taskDto)
        {
            return new TaskEntity
            {
                Id = taskDto.Id,
                Title = taskDto.Title,
                Description = taskDto.Description,
                Status = taskDto.Status,
                CreatedAt = taskDto.CreatedAt,
                DueDate = taskDto.DueDate
            };
        }
    }
}
