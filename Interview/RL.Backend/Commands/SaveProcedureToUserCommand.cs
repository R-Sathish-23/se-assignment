using MediatR;
using RL.Backend.Models;

namespace RL.Backend.Commands
{
    public class SaveProcedureToUserCommand : IRequest<ApiResponse<Unit>>
    {
        public int PlanId { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public int ProcedureId { get; set; }
    }
}