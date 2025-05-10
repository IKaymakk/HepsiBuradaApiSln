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
    public Task ProductTitleMustNotBeSame(IList<Domain.Entities.Product> products, string requestTitle)
    {
        if (products.Any(x => x.Title == requestTitle)) throw new ProductTitleMustNotBeSameException();

        return Task.CompletedTask;
    }
}
