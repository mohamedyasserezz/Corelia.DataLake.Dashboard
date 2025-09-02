using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Domain.Entities.Tasks
{
    public class CommentAuthor
    {
        public Dictionary<string, object> CommentAuthors { get; set; }
    }

    public class ResultItem
    {
        public Dictionary<string, object> Result { get; set; }
    }

    public class Draft
    {
        public DateTime CreatedAt { get; set; }
        public List<ResultItem> Result { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class ModelInfo
    {
        public Dictionary<string, object> Model { get; set; }
    }

    public class ModelRun
    {
        public Dictionary<string, object> ModelRunData { get; set; }
    }
    public class Prediction
    {
        public DateTime CreatedAt { get; set; }
        public ModelInfo Model { get; set; }
        public ModelRun ModelRun { get; set; }
        public string ModelVersion { get; set; }
        public int Project { get; set; }
        public List<ResultItem> Result { get; set; }
        public double Score { get; set; }
        public int Task { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class Reviewer
    {
        public Dictionary<string, object> Reviewers { get; set; }
    }

    public class UpdatedBy
    {
        public Dictionary<string, object> UpdatedByData { get; set; }
    }
    public class TaskAnnotation
    {
        public string Agreement { get; set; } 
        public string Annotations { get; set; }
        public string AnnotationsIds { get; set; }
        public string AnnotationsResults { get; set; }
        public List<int> Annotators { get; set; }
        public int AnnotatorsCount { get; set; }
        public List<CommentAuthor> CommentAuthors { get; set; }
        public int CommentAuthorsCount { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedAt { get; set; }
        public Dictionary<string, object> Data { get; set; }
        public List<Draft> Drafts { get; set; }
        public string FileUpload { get; set; }
        public int Id { get; set; }
        public List<Prediction> Predictions { get; set; }
        public string PredictionsModelVersions { get; set; }
        public string PredictionsResults { get; set; }
        public List<Reviewer> Reviewers { get; set; }
        public int ReviewersCount { get; set; }
        public string StorageFilename { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<UpdatedBy> UpdatedBy { get; set; }
        public double AvgLeadTime { get; set; }
        public int CancelledAnnotations { get; set; }
        public int CommentCount { get; set; }
        public DateTime CompletedAt { get; set; }
        public bool DraftExists { get; set; }
        public bool GroundTruth { get; set; }
        public int InnerId { get; set; }
        public bool IsLabeled { get; set; }
        public DateTime LastCommentUpdatedAt { get; set; }
        public Dictionary<string, object> Meta { get; set; }
        public int Overlap { get; set; }
        public double PredictionsScore { get; set; }
        public int Project { get; set; }
        public bool Reviewed { get; set; }
        public int ReviewsAccepted { get; set; }
        public int ReviewsRejected { get; set; }
        public int TotalAnnotations { get; set; }
        public int TotalPredictions { get; set; }
        public int UnresolvedCommentCount { get; set; }
    }
}
