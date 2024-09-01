using System.ComponentModel.DataAnnotations;
using RL.Data.DataModels.Common;

namespace RL.Data.DataModels;

public class PlanUserData : IChangeTrackable
{
    [Key]
    public int Id { get; set; }
    public int PlanId { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }

    public int ProcedureId { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}