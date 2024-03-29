﻿using ShadowBuddy.Domain.Entities;

namespace ShadowBuddy.Handlers.Queries;

public class GetOperationsQueryResult
{
    public Operation[] Operations { get; set; }

    public GetOperationsQueryResult(Operation[] operations)
    {
        Operations = operations;
    }
}