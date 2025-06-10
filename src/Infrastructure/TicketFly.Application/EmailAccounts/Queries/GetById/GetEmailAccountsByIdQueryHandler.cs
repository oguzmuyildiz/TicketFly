namespace TicketFly.Application.EmailAccounts.Queries.GetById;
public class GetEmailAccountsByIdQueryHandler(IAppDbContext context, IMapper mapper) : 
    IRequestHandler<GetEmailAccountsByIdQuery, Result<EmailAccountDto>>
{
    public async Task<Result<EmailAccountDto>> Handle(GetEmailAccountsByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var entity = await context.EmailAccounts
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken: cancellationToken);

        return mapper.Map<EmailAccountDto>(entity);
    }
}