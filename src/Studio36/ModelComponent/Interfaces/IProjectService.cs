using Studio36.ModelComponent.Entities;

namespace Studio36.ModelComponent.Interfaces
{
    public interface IProjectService
    {
        List<Project> LoadProjects();
        Dictionary<int, List<TaskItem>> LoadTasks();
        void SaveData(List<Project> projects, Dictionary<int, List<TaskItem>> tasks);
    }
}
