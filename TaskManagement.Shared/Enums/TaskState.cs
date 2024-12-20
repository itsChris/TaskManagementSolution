namespace TaskManagement.Shared.Enums
{
    public enum TaskState
    {
        /// <summary>
        /// Die Aufgabe ist neu und noch nicht begonnen.
        /// </summary>
        New,

        /// <summary>
        /// Die Aufgabe wird gerade bearbeitet.
        /// </summary>
        InProgress,

        /// <summary>
        /// Die Aufgabe wurde abgeschlossen.
        /// </summary>
        Completed,

        /// <summary>
        /// Die Aufgabe wurde storniert und ist nicht mehr relevant.
        /// </summary>
        Cancelled
    }
}
