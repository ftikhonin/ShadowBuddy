﻿using MediatR;
using ShadowBuddy.Domain.Enums;

namespace ShadowBuddy.Handlers.Commands;

public class CreateAccountCommand : IRequest
{
    public long UserId { get; set; }
    public string Name { get; set; }
    public double Balance { get; set; }
    public DateTime InitialDate { get; set; }
    public long CurrencyId { get; set; }
    public string Comment { get; set; }

    public CreateAccountCommand(
        long userId, 
        string name, 
        double balance, 
        DateTime initialDate, 
        long currencyId, 
        string comment)
    {
        UserId = userId;
        Name = name;
        Balance = balance;
        InitialDate = initialDate;
        CurrencyId = currencyId;
        Comment = comment;
    }
}