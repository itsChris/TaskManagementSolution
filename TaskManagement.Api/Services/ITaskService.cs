using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Shared.Models;

namespace TaskManagement.Api.Services
{
    public interface ITaskService
    {
        /// <summary>
        /// Holt alle Tasks.
        /// </summary>
        /// <returns>Eine Liste aller Tasks als DTOs.</returns>
        Task<IEnumerable<TaskDto>> GetTasksAsync();

        /// <summary>
        /// Holt eine Aufgabe anhand der ID.
        /// </summary>
        /// <param name="id">Die ID der Aufgabe.</param>
        /// <returns>Das gefundene TaskDto oder null, falls nicht vorhanden.</returns>
        Task<TaskDto> GetTaskByIdAsync(int id);

        /// <summary>
        /// Erstellt eine neue Aufgabe.
        /// </summary>
        /// <param name="taskDto">Die Daten der neuen Aufgabe.</param>
        /// <returns>Das erstellte TaskDto.</returns>
        Task<TaskDto> CreateTaskAsync(TaskDto taskDto);

        /// <summary>
        /// Aktualisiert eine bestehende Aufgabe.
        /// </summary>
        /// <param name="taskDto">Die aktualisierten Daten der Aufgabe.</param>
        /// <returns>Das aktualisierte TaskDto oder null, falls die Aufgabe nicht existiert.</returns>
        Task<TaskDto> UpdateTaskAsync(TaskDto taskDto);

        /// <summary>
        /// Löscht eine Aufgabe anhand der ID.
        /// </summary>
        /// <param name="id">Die ID der zu löschenden Aufgabe.</param>
        /// <returns>True, wenn die Aufgabe gelöscht wurde, andernfalls false.</returns>
        Task<bool> DeleteTaskAsync(int id);
    }
}
