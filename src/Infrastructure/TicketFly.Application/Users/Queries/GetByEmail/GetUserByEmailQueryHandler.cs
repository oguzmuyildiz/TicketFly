namespace TicketFly.Application.Users.Queries.GetByEmail;
public class GetUserByEmailQueryHandler(IAppDbContext context, IMapper mapper) : 
    IRequestHandler<GetUserByEmailQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(GetUserByEmailQuery request, 
        CancellationToken cancellationToken)
    {
        var result = await context.Users
               .FirstOrDefaultAsync(t => t.Email == request.Email, cancellationToken: cancellationToken);

        if (result is null)
        {
            return Result.Failure<UserDto>(
                Error.NotFound("User not found", $"User with Email {request.Email} not found."));
        }
        return mapper.Map<UserDto>(result);
    }
}