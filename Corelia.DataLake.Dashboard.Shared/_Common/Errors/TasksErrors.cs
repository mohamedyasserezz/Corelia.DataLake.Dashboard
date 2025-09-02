using Corelia.DataLake.Dashboard.Shared.Abstraction;
using Microsoft.AspNetCore.Http;

namespace Corelia.DataLake.Dashboard.Shared._Common.Errors
{
    public static class TasksErrors
    {
        public static readonly Error TasksNotFound =
            new("Tasks.NotFound", "No tasks found", StatusCodes.Status404NotFound);
    }
}
