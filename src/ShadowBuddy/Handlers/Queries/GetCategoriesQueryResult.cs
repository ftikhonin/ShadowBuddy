using ShadowBuddy.Domain.Entities;

namespace ShadowBuddy.Handlers.Queries;

public class GetCategoriesQueryResult
{
    public Category[] Categories { get; set; }
}