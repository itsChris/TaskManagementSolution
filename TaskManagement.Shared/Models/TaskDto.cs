using System;
using TaskManagement.Shared.Enums;

namespace TaskManagement.Shared.Models
{
    public class TaskDto
    {
        /// <summary>
        /// Die eindeutige ID der Aufgabe.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Der Titel der Aufgabe. Muss ausgefüllt werden.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Eine Beschreibung der Aufgabe. Optional.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Der Status der Aufgabe (z. B. Neu, In Bearbeitung, Abgeschlossen).
        /// </summary>
        public TaskState Status { get; set; } = TaskState.New;

        /// <summary>
        /// Das Erstellungsdatum der Aufgabe. Automatisch gesetzt.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Das Fälligkeitsdatum der Aufgabe. Optional.
        /// </summary>
        public DateTime? DueDate { get; set; }
    }
}
