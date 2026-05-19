using Studio36.ModelComponent.Entities;
using Studio36.ModelComponent.Interfaces;

namespace Studio36.Tests;

public class MockProjectService : IProjectAndTaskService
{
    public List<Project> Projects { get; set; } = new()
    {
        new Project(1, "Projeto de demonstração", "Descrição", DateTime.Today, DateTime.Today.AddDays(7))
    };
    public Dictionary<int, List<TaskItem>> Tasks { get; set; } = new();

    public List<Project> LoadProjects() => Projects;
    public Dictionary<int, List<TaskItem>> LoadTasks() => Tasks;
    public void SaveData(List<Project> projects, Dictionary<int, List<TaskItem>> tasks)
    {
        Projects = new List<Project>(projects);
        Tasks = new Dictionary<int, List<TaskItem>>(tasks);
    }
}
