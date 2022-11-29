using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowControl;
public class GarFile
{
	[Key]
    [DataType(DataType.Date)]  
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
    public DateTime Date { get; set; }
    public string DeltaUrl { get; set; }
    public string FullUrl { get; set; }
    public string LocalPath { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public DateTime? DownloadRequestedAt { get; set; }
    public DateTime? DownloadedAt { get; set; }
    public DateTime? ProcessRequestedAt { get; set; }    
    public DateTime? ProcessedAt { get; set; }
    public Guid CorrelationId { get; set; }
}
