using MediatR;
using Microsoft.EntityFrameworkCore;
using RL.Backend.Exceptions;
using RL.Backend.Models;
using RL.Data;
using RL.Data.DataModels;

namespace RL.Backend.Commands.Handlers.Plans;

public class SaveUserProcedureToPlanCommandHandler : IRequestHandler<SaveProcedureToUserCommand, ApiResponse<Unit>>
{
    private readonly RLContext _context;

    public SaveUserProcedureToPlanCommandHandler(RLContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<Unit>> Handle(SaveProcedureToUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //Validate request
            if (request.UserId < 1)
                return ApiResponse<Unit>.Fail(new BadRequestException("Invalid UserId"));
            if (request.ProcedureId < 1)
                return ApiResponse<Unit>.Fail(new BadRequestException("Invalid ProcedureId"));

           
            var procedure = await _context.Procedures.FirstOrDefaultAsync(p => p.ProcedureId == request.ProcedureId);
            var user = await _context.Users.FirstOrDefaultAsync(p => p.UserId == request.UserId);
            var userData = await _context.PlanUserDatas.AnyAsync(p => p.UserId == request.UserId && p.ProcedureId == request.ProcedureId);

           
            if (procedure is null)
                return ApiResponse<Unit>.Fail(new NotFoundException($"ProcedureId: {request.ProcedureId} not found"));
            if (user is null)
                return ApiResponse<Unit>.Fail(new NotFoundException($"UserId: {request.UserId} not found"));

            if (userData)
            {
                return ApiResponse<Unit>.Fail(new BadRequestException("Already procedure added"));
            }

            _context.PlanUserDatas.Add(new PlanUserData
            {
                PlanId=request.PlanId,
                UserId= request.UserId,
                UserName=request.UserName,
                ProcedureId = request.ProcedureId,
                UpdateDate= DateTime.UtcNow,
                CreateDate= DateTime.UtcNow,
            });
            
           

            await _context.SaveChangesAsync();

            return ApiResponse<Unit>.Succeed(new Unit());
        }
        catch (Exception e)
        {
            return ApiResponse<Unit>.Fail(e);
        }
    }
}