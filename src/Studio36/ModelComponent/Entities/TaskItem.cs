namespace Studio36.ModelComponent.Entities
{
    public class TaskItem
    {
        public TaskItem(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }

        public void UpdateDescription(string description)
        {
            Description = description;
        }
    }
}
