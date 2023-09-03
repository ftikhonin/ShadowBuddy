using ShadowBuddy.Domain.Entities;

namespace ShadowBuddy.Handlers.Queries;

public class GetCategoriesQueryResult
{
    public Category[] Categories { get; set; }

    public GetCategoriesQueryResult(Category[] categories)
    {
        Categories = categories;
    }
}