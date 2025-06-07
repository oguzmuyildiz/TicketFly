namespace TicketFly.Application.Users.Queries.GetById;
public class GetUserByIdQueryHandler(IAppDbContext context, IMapper mapper) : 
    IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var result = await context.Users
               .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken: cancellationToken);

        if (result is null)
        {
            return Result.Failure<UserDto>(
                Error.NotFound("User not found", $"User with ID {request.Id} not found."));
        }
        return mapper.Map<UserDto>(result);
    }
}