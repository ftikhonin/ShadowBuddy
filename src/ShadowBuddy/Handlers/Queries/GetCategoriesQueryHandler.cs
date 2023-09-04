using MediatR;
using ShadowBuddy.Domain.Repositories;
using ShadowBuddy.Infrastructure.Exceptions;

namespace ShadowBuddy.Handlers.Queries;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, GetCategoriesQueryResult>
{
    private readonly IAccountProcessingRepository _accountProcessingRepository;

    public GetCategoriesQueryHandler(IAccountProcessingRepository accountProcessingRepository)
    {
        _accountProcessingRepository = accountProcessingRepository ??
                                       throw new ArgumentNullException(nameof(accountProcessingRepository));
    }

    public async Task<GetCategoriesQueryResult> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _accountProcessingRepository.GetCategories();

        if (categories is null || !categories.Any())
        {
            throw new NotFoundException($"Categories not found.");
        }

        return new GetCategoriesQueryResult(categories);
    }
}