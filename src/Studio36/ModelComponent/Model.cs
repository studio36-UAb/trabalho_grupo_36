using System;
using Studio36.DTOs;
using Studio36.ModelComponent.Entities;
using Studio36.Interfaces;
using Studio36.ModelComponent.Interfaces;
using Studio36.ModelComponent.Services;

namespace Studio36.ModelComponent
{
    public class Model : IModel
    {
        private readonly List<Project> projects;
        private readonly Dictionary<int, List<TaskItem>> tasksByProject;

        private readonly IAuthenticationService authenticationService;
        private readonly IRegistrationService registrationService;

        private readonly IProjectService projectService;

        public bool IsLoggedIn { get; set; } = false;

        public event Action<LoginResultData>? SendLoginState;
        public event Action<SignUpResultData>? SendSignUpState;
        public event Action<CreateProjectResultData>? SendProjectCreationState;
        public event Action<EditProjectResultData>? SendProjectEditionState;
        public event Action<DeleteProjectResultData>? SendProjectDeletionState;
        public event Action<TaskOperationResultData>? SendTaskOperationState;

        public Model(IAuthenticationService authService, IRegistrationService regService, IProjectService projService)
        {
            authenticationService = authService;
            registrationService = regService;
            projectService = projService;
            projects = projectService.LoadProjects();
            tasksByProject = projectService.LoadTasks();
        }

        public void AreCredentialsValid(LoginRequestData request)
        {
            ValidateLoginInput(request.Email, request.Password);

            (LoginResult result, string message) = authenticationService.VerifyCredentials(request.Email, request.Password);

            SendLoginState?.Invoke(new LoginResultData(result == LoginResult.Success, message));
        }

        public void RegisterUser(SignUpRequestData request)
        {
            (SignUpResult result, string message) = registrationService.RegisterUser(request.Email, request.Password);

            SendSignUpState?.Invoke(new SignUpResultData(result == SignUpResult.Success, message));
        }

        public void CreateProject(CreateProjectRequestData request)
        {
            ValidateProjectInput(request);

            int projectId = GetNextProjectId();

            // Save the project as a domain entity to support future CRUD flows.
            projects.Add(new Project(
                projectId,
                request.Name.Trim(),
                request.Description.Trim(),
                request.StartDate,
                request.EndDate));
            tasksByProject.Add(projectId, new List<TaskItem>());

            projectService.SaveData(projects, tasksByProject);

            SendProjectCreationState?.Invoke(new CreateProjectResultData(
                true,
                projectId,
                $"Project created successfully with ID {projectId}."));
        }

        public void EditProject(EditProjectRequestData request)
        {
            ValidateProjectInput(request.Name, request.Description, request.StartDate, request.EndDate);

            Project project = GetProjectById(request.ProjectId);

            // Update the existing entity without creating a new project or changing its ID.
            project.UpdateDetails(
                request.Name.Trim(),
                request.Description.Trim(),
                request.StartDate,
                request.EndDate);

            projectService.SaveData(projects, tasksByProject);

            SendProjectEditionState?.Invoke(new EditProjectResultData(
                true,
                request.ProjectId,
                $"Project {request.ProjectId} updated successfully."));
        }

        public void DeleteProject(int projectId)
        {
            Project project = GetProjectById(projectId);

            projects.Remove(project);
            tasksByProject.Remove(projectId);

            projectService.SaveData(projects, tasksByProject);

            SendProjectDeletionState?.Invoke(new DeleteProjectResultData(
                true,
                projectId,
                $"Project {projectId} deleted successfully."));
        }

        public void AddTask(int projectId, string taskDescription)
        {
            if (!ProjectExists(projectId))
            {
                throw new ProjectNotFoundException(projectId);
            }

            if (string.IsNullOrWhiteSpace(taskDescription))
            {
                throw new ArgumentException("Task description cannot be empty.");
            }

            if (!tasksByProject.ContainsKey(projectId))
            {
                tasksByProject[projectId] = new List<TaskItem>();
            }

            int taskId = GetNextTaskId();
            tasksByProject[projectId].Add(new TaskItem(taskId, taskDescription.Trim()));
            projectService.SaveData(projects, tasksByProject);

            SendTaskOperationState?.Invoke(new TaskOperationResultData(true, $"Task added successfully with ID {taskId}."));
        }

        public void EditTask(int projectId, int taskId, string newDescription)
        {
            if (!ProjectExists(projectId))
            {
                throw new ProjectNotFoundException(projectId);
            }

            if (!tasksByProject.ContainsKey(projectId))
            {
                 throw new ArgumentException($"Project {projectId} has no tasks.");
            }

            TaskItem? task = tasksByProject[projectId].FirstOrDefault(t => t.Id == taskId);
            if (task == null)
            {
                throw new ArgumentException($"Task with ID {taskId} not found in project {projectId}.");
            }

            if (string.IsNullOrWhiteSpace(newDescription))
            {
                throw new ArgumentException("Task description cannot be empty.");
            }

            task.UpdateDescription(newDescription.Trim());
            projectService.SaveData(projects, tasksByProject);

            SendTaskOperationState?.Invoke(new TaskOperationResultData(true, "Task updated successfully."));
        }

        public void DeleteTask(int projectId, int taskId)
        {
            if (!ProjectExists(projectId))
            {
                throw new ProjectNotFoundException(projectId);
            }

            if (!tasksByProject.ContainsKey(projectId))
            {
                 throw new ArgumentException($"Project {projectId} has no tasks.");
            }

            TaskItem? task = tasksByProject[projectId].FirstOrDefault(t => t.Id == taskId);
            if (task == null)
            {
                throw new ArgumentException($"Task with ID {taskId} not found in project {projectId}.");
            }

            tasksByProject[projectId].Remove(task);
            projectService.SaveData(projects, tasksByProject);

            SendTaskOperationState?.Invoke(new TaskOperationResultData(true, "Task removed successfully."));
        }

        private int GetNextTaskId()
        {
            int maxId = 0;
            foreach (var taskList in tasksByProject.Values)
            {
                if (taskList.Count > 0)
                {
                    int currentMax = taskList.Max(t => t.Id);
                    if (currentMax > maxId) maxId = currentMax;
                }
            }
            return maxId + 1;
        }

        public ProjectReportData GetProjectReportData(int projectId)
        {
            Project project = GetProjectById(projectId);

            List<string> tasks = tasksByProject.ContainsKey(projectId)
                ? tasksByProject[projectId].Select(t => $"[{t.Id}] {t.Description}").ToList()
                : new List<string>();

            return new ProjectReportData(
                project.Id,
                project.Name,
                project.Description,
                project.StartDate,
                project.EndDate,
                tasks);
        }

        private void ValidateLoginInput(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new InvalidLoginInputException("The email cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidLoginInputException("The password cannot be empty.");
            }
        }

        private static void ValidateProjectInput(CreateProjectRequestData request)
        {
            ValidateProjectInput(request.Name, request.Description, request.StartDate, request.EndDate);
        }

        private static void ValidateProjectInput(string name, string description, DateTime startDate, DateTime endDate)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The project name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("The project description cannot be empty.");
            }

            if (endDate < startDate)
            {
                throw new ArgumentException("The project end date cannot be earlier than the start date.");
            }
        }

        private int GetNextProjectId()
        {
            // Calculate the next ID based on the projects already existing in the Model's state.
            if (projects.Count == 0)
            {
                return 1;
            }

            return projects.Max(project => project.Id) + 1;
        }

        public List<string> GetTasksByProject(int projectId)
        {
            // Check if the project exists in the current Model state.
            if (!ProjectExists(projectId))
            {
                throw new ProjectNotFoundException(projectId);
            }

            // If the project exists but has no tasks yet, return an empty list.
            if (!tasksByProject.ContainsKey(projectId))
            {
                return new List<string>();
            }

            return tasksByProject[projectId].Select(t => $"[{t.Id}] {t.Description}").ToList();
        }

        public List<string> GetProjects()
        {
            // Return the updated list of projects existing in the Model.
            return projects
                .OrderBy(project => project.Id)
                .Select(project => $"{project.Id} - {project.Name}")
                .ToList();
        }

        private bool ProjectExists(int projectId)
        {
            return projects.Any(project => project.Id == projectId);
        }

        private Project GetProjectById(int projectId)
        {
            Project? project = projects.FirstOrDefault(project => project.Id == projectId);

            if (project == null)
            {
                throw new ProjectNotFoundException(projectId);
            }

            return project;
        }
    }
}
