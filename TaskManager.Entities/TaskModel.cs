namespace TaskManager.Entities
{
    public class TaskModel
    {
        public int TaskId { get; set; }
        public int? ParentTaskId { get; set; }
        public string Task { get; set; }
        public int Priority { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsEditable { get; set; }
    }
}
