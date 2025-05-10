using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiBuradaApi.Application.Behaviors;

public class FluentValidationBehaviors<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{

    private readonly IEnumerable<IValidator<TRequest>> _validator;

    public FluentValidationBehaviors(IEnumerable<IValidator<TRequest>> validator)
    {
        _validator = validator;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var failtures = _validator
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .GroupBy(x => x.ErrorMessage)
            .Select(x => x.First())
            .Where(x => x.ErrorMessage != null)
            .ToList();

        if (failtures.Any())
            throw new ValidationException(failtures);

        return next();
    }
}
