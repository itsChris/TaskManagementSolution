using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Shared.Models;

namespace TaskManagement.Blazor.Services
{
    public interface ITaskApiClient
    {
        /// <summary>
        /// Ruft alle Tasks aus der API ab.
        /// </summary>
        Task<IEnumerable<TaskDto>> GetTasksAsync();

        /// <summary>
        /// Ruft einen Task anhand seiner ID aus der API ab.
        /// </summary>
        /// <param name="id">Die ID des Tasks.</param>
        Task<TaskDto> GetTaskByIdAsync(int id);

        /// <summary>
        /// Erstellt einen neuen Task über die API.
        /// </summary>
        /// <param name="taskDto">Das Task-Objekt mit den Daten des neuen Tasks.</param>
        Task CreateTaskAsync(TaskDto taskDto);

        /// <summary>
        /// Aktualisiert einen bestehenden Task über die API.
        /// </summary>
        /// <param name="taskDto">Das Task-Objekt mit den aktualisierten Daten.</param>
        Task UpdateTaskAsync(TaskDto taskDto);

        /// <summary>
        /// Löscht einen Task anhand seiner ID über die API.
        /// </summary>
        /// <param name="id">Die ID des zu löschenden Tasks.</param>
        Task DeleteTaskAsync(int id);
    }
}
