using Blazored.LocalStorage;
using TaskManagement.Shared.Models;

namespace TaskManagement.Blazor.Services
{
    public class TaskApiClient : ITaskApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public TaskApiClient(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _localStorage = localStorage ?? throw new ArgumentNullException(nameof(localStorage));
        }

        private async Task AddAuthorizationHeaderAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<IEnumerable<TaskDto>> GetTasksAsync()
        {
            try
            {
                await AddAuthorizationHeaderAsync();
                var tasks = await _httpClient.GetFromJsonAsync<IEnumerable<TaskDto>>("api/tasks");
                return tasks ?? new List<TaskDto>();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching tasks: {ex.Message}");
                throw new InvalidOperationException("An error occurred while fetching tasks.", ex);
            }
        }

        public async Task<TaskDto> GetTaskByIdAsync(int id)
        {
            try
            {
                var task = await _httpClient.GetFromJsonAsync<TaskDto>($"api/tasks/{id}");
                if (task == null)
                {
                    throw new InvalidOperationException($"Task with ID {id} was not found.");
                }
                return task;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching task with ID {id}: {ex.Message}");
                throw new InvalidOperationException($"An error occurred while fetching the task with ID {id}.", ex);
            }
        }

        public async Task CreateTaskAsync(TaskDto taskDto)
        {
            if (taskDto == null)
            {
                throw new ArgumentNullException(nameof(taskDto), "Task data cannot be null.");
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/tasks", taskDto);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error creating task: {ex.Message}");
                throw new InvalidOperationException("An error occurred while creating the task.", ex);
            }
        }

        public async Task UpdateTaskAsync(TaskDto taskDto)
        {
            if (taskDto == null)
            {
                throw new ArgumentNullException(nameof(taskDto), "Task data cannot be null.");
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/tasks/{taskDto.Id}", taskDto);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating task with ID {taskDto.Id}: {ex.Message}");
                throw new InvalidOperationException($"An error occurred while updating the task with ID {taskDto.Id}.", ex);
            }
        }

        public async Task DeleteTaskAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/tasks/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting task with ID {id}: {ex.Message}");
                throw new InvalidOperationException($"An error occurred while deleting the task with ID {id}.", ex);
            }
        }
    }
}
