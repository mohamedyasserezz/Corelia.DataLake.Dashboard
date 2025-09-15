using Corelia.DataLake.Dashboard.Domain.Contract.Service.File;
using Corelia.DataLake.Dashboard.Domain.Contract.Service.Task;
using Corelia.DataLake.Dashboard.Domain.Entities.Tasks;
using Corelia.DataLake.Dashboard.Shared._Common.Errors;
using Corelia.DataLake.Dashboard.Shared.Abstraction;
using Corelia.DataLake.Dashboard.Shared.Models.Task;
using System.Net.Http.Json;

namespace Corelia.DataLake.Dashboard.Application.Services.Tasks
{
    public class TaskServices(HttpClient httpClient,
        IFileService fileService) : ITaskServices
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IFileService _fileService = fileService;

        public Task<Result<TaskResponse>> AssignTaskAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<TaskResponse>> CancelTaskAsync(TaskRequest request)
        {
            var response = await _httpClient.GetAsync($"api/tasks{request.Id}");

            var taskAnnontation = await response.Content.ReadFromJsonAsync<TaskAnnotation>();

            if (taskAnnontation is null)
                return Result.Failure<TaskResponse>(TasksErrors.TaskNotFound);

            taskAnnontation.ReviewsRejected+= 1;

            var task = new TaskResponse(
                        taskAnnontation.Id,
                        taskAnnontation.AnnotatorsCount > 0 ? true : false,
                        taskAnnontation.IsLabeled,
                        taskAnnontation.Reviewed,
                        taskAnnontation.CreatedAt,
                        taskAnnontation.UpdatedAt,
                        IsCanceled: true,
                        _fileService.GetImageUrl("task", taskAnnontation.FileUpload),
                        taskAnnontation.Project
                        );

            if (task is null)
                return Result.Failure<TaskResponse>(TasksErrors.TaskNotFound);

            return Result.Success(task);
        }

        public async Task<Result<TaskResponse>> CompleteTaskAsync(TaskRequest request)
        {
            var response = await _httpClient.GetAsync($"api/tasks{request.Id}");

            var taskAnnontation = await response.Content.ReadFromJsonAsync<TaskAnnotation>();

            if (taskAnnontation is null)
                return Result.Failure<TaskResponse>(TasksErrors.TaskNotFound);

            taskAnnontation.IsLabeled = true;

            var task = new TaskResponse(
                        taskAnnontation.Id,
                        taskAnnontation.AnnotatorsCount > 0 ? true : false,
                        taskAnnontation.IsLabeled,
                        taskAnnontation.Reviewed,
                        taskAnnontation.CreatedAt,
                        taskAnnontation.UpdatedAt,
                        taskAnnontation.ReviewsRejected > 0 ? true : false,
                        _fileService.GetImageUrl("task", taskAnnontation.FileUpload),
                        taskAnnontation.Project
                        );

            if (task is null)
                return Result.Failure<TaskResponse>(TasksErrors.TaskNotFound);

            return Result.Success(task);
            //var response = GetTaskByIdAsync(request);

            //if (response.Result.IsFailure)
            //    return Result.Failure<TaskResponse>(TasksErrors.TaskNotFound);

            //var task = response.Result.Value;

            //var canceledTask = new TaskResponse(
            //            task.Id,
            //            task.IsAssigned,
            //            IsCompleted:true,
            //            task.IsReviewed,
            //            task.CreatedOn,
            //            task.CompletedOn,
            //            task.IsCanceled,
            //            task.FileUrl,
            //            task.ProjectId
            //            );

            //return Result.Success(canceledTask);

        }

        public Task<Result> DeleteTaskAsync(TaskRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<TaskResponse>>> GetAllTasksAsync()
        {
            var response = await _httpClient.GetAsync("api/tasks");

            if (response.IsSuccessStatusCode)
            {
                var tasksAnnontation = await response.Content.ReadFromJsonAsync<IEnumerable<TaskAnnotation>>();

                var emptyTaskList = new List<TaskResponse>();
                if (tasksAnnontation is null || !tasksAnnontation.Any())
                    return Result.Success(emptyTaskList.AsEnumerable());

                var tasks = tasksAnnontation.Select(t =>
                    new TaskResponse(
                        t.Id,
                        t.AnnotatorsCount > 0 ? true : false,
                        t.IsLabeled,
                        t.Reviewed,
                        t.CreatedAt,
                        t.UpdatedAt,
                        t.ReviewsRejected > 0 ? true : false,
                        _fileService.GetImageUrl("task", t.FileUpload),
                        t.Project
                        ));

                if (tasks is null)
                    return Result.Failure<IEnumerable<TaskResponse>>(TasksErrors.TasksNotFound);

                return Result.Success(tasks);
            }
            else
            {
                return Result.Failure<IEnumerable<TaskResponse>>(TasksErrors.TasksNotFound);
            }
        }

        public async Task<Result<TaskResponse>> GetTaskByIdAsync(TaskRequest request)
        {
            var response = await _httpClient.GetAsync($"api/tasks/{request.Id}");


            var taskAnnontation = await response.Content.ReadFromJsonAsync<TaskAnnotation>();

            if (taskAnnontation is null)
                return Result.Failure<TaskResponse>(TasksErrors.TaskNotFound);

            var task = new TaskResponse(
                        taskAnnontation.Id,
                        taskAnnontation.AnnotatorsCount > 0 ? true : false,
                        taskAnnontation.IsLabeled,
                        taskAnnontation.Reviewed,
                        taskAnnontation.CreatedAt,
                        taskAnnontation.UpdatedAt,
                        taskAnnontation.ReviewsRejected > 0 ? true : false,
                        _fileService.GetImageUrl("task", taskAnnontation.FileUpload),
                        taskAnnontation.Project
                        );

            if (task is null)
                return Result.Failure<TaskResponse>(TasksErrors.TaskNotFound);

            return Result.Success(task);

        }

        public Task<Result<IEnumerable<TaskResponse>>> GetTasksByProjectAsync(TaskRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Result<TaskResponse>> ReviewTaskAsync(TaskRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Result<TaskResponse>> UpdateTaskAsync(TaskRequest request)
        {
            
            throw new NotImplementedException();
        }
    }
}