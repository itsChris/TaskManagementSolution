using System;
using TaskManagement.Shared.Enums;

namespace TaskManagement.Api.Models
{
    /// <summary>
    /// Repräsentiert eine Aufgabe in der Datenbank.
    /// </summary>
    public class TaskEntity
    {
        /// <summary>
        /// Eindeutige ID der Aufgabe.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Titel der Aufgabe. Muss ausgefüllt werden.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Beschreibung der Aufgabe. Optional.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Status der Aufgabe (z. B. Neu, In Bearbeitung, Abgeschlossen).
        /// </summary>
        public TaskState Status { get; set; }

        /// <summary>
        /// Erstellungsdatum der Aufgabe. Muss ausgefüllt werden.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Fälligkeitsdatum der Aufgabe. Optional.
        /// </summary>
        public DateTime? DueDate { get; set; }
    }
}
