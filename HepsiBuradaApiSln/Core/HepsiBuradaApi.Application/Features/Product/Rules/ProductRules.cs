using HepsiBuradaApi.Application.Bases;
using HepsiBuradaApi.Application.Features.Product.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiBuradaApi.Application.Features.Product.Rules;

public class ProductRules : BaseRules
{
    public Task ProductTitleMustNotBeSame(string requestTitle, string productTitle)
    {
        if (requestTitle == productTitle) 
            throw new ProductTitleMustNotBeSameException();

        return Task.CompletedTask;
    }
}
