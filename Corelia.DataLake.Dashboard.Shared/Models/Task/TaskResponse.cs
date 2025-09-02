namespace Corelia.DataLake.Dashboard.Shared.Models.Task
{
    public record TaskResponse(
        int Id,
        bool IsAssigned,
        bool IsCompleted,
        bool IsReviewed,
        DateTime CreatedOn,
        DateTime CompletedOn,
        bool IsCanceled,
        string FileUrl,
        int ProjectId
        );
    
}
