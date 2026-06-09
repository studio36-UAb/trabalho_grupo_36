using Newtonsoft.Json;
using Studio36.ModelComponent.Entities;
using Studio36.ModelComponent.Interfaces;
using Studio36.Utils;

namespace Studio36.ModelComponent.Services
{
    public class JsonProjectAndTaskService : IProjectAndTaskService
    {
        private readonly string _projectsFilePath;
        private readonly string _tasksFilePath;

        public JsonProjectAndTaskService(string projectsFilePath, string tasksFilePath)
        {
            _projectsFilePath = Path.Combine(AppContext.BaseDirectory, projectsFilePath);
            _tasksFilePath = Path.Combine(AppContext.BaseDirectory, tasksFilePath);
        }

        public List<Project> LoadProjects()
        {
            try
            {
                if (File.Exists(_projectsFilePath))
                {
                    string jsonContent = File.ReadAllText(_projectsFilePath);
                    return JsonConvert.DeserializeObject<List<Project>>(jsonContent) ?? new List<Project>();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error loading project data. Using empty state.", ex);
            }
            return new List<Project>();
        }

        public Dictionary<int, List<TaskItem>> LoadTasks()
        {
            try
            {
                if (File.Exists(_tasksFilePath))
                {
                    string jsonContent = File.ReadAllText(_tasksFilePath);
                    return JsonConvert.DeserializeObject<Dictionary<int, List<TaskItem>>>(jsonContent) ?? new Dictionary<int, List<TaskItem>>();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error loading task data. Using empty state.", ex);
            }
            return new Dictionary<int, List<TaskItem>>();
        }

        public void SaveData(List<Project> projects, Dictionary<int, List<TaskItem>> tasks)
        {
            try
            {
                SaveProjects(projects);
                SaveTasks(tasks);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to save data.", ex);
            }
        }

        private void SaveProjects(List<Project> projects)
        {
            EnsureDirectoryExists(_projectsFilePath);
            string jsonContent = JsonConvert.SerializeObject(projects, Formatting.Indented);
            File.WriteAllText(_projectsFilePath, jsonContent);
            Logger.Info($"Project data saved successfully to: {_projectsFilePath}");
        }

        private void SaveTasks(Dictionary<int, List<TaskItem>> tasks)
        {
            EnsureDirectoryExists(_tasksFilePath);
            string jsonContent = JsonConvert.SerializeObject(tasks, Formatting.Indented);
            File.WriteAllText(_tasksFilePath, jsonContent);
            Logger.Info($"Task data saved successfully to: {_tasksFilePath}");
        }

        private void EnsureDirectoryExists(string filePath)
        {
            string? directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
