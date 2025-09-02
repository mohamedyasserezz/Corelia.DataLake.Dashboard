using Corelia.DataLake.Dashboard.Shared.Abstraction;
using Corelia.DataLake.Dashboard.Shared.Models.Task;

namespace Corelia.DataLake.Dashboard.Domain.Contract.Service.Task
{
    public interface ITaskServices
    {
        Task<Result<IEnumerable<TaskResponse>>> GetTasksByProjectAsync(TaskRequest request);
        Task<Result<TaskResponse>> GetTaskByIdAsync(TaskRequest request);
        Task<Result<IEnumerable<TaskResponse>>> GetAllTasksAsync();
        Task<Result<TaskResponse>> AssignTaskAsync();
        Task<Result<TaskResponse>> UpdateTaskAsync(TaskRequest request);
        Task<Result> DeleteTaskAsync(TaskRequest request);
        Task<Result<TaskResponse>> CompleteTaskAsync(TaskRequest request);
        Task<Result<TaskResponse>> ReviewTaskAsync(TaskRequest request);
        Task<Result<TaskResponse>> CancelTaskAsync(TaskRequest request);
    }
}
