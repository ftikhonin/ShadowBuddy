using ShadowBuddy.Domain.Entities;

namespace ShadowBuddy.Handlers;

public class GetCategoriesQueryResult
{
    public Category[] Categories { get; set; }
}