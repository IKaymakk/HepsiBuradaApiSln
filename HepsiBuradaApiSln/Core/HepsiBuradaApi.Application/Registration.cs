using FluentValidation;
using HepsiBuradaApi.Application.Behaviors;
using HepsiBuradaApi.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HepsiBuradaApi.Application;

public static class Registration
{
    public static void AddApplicaiton(this IServiceCollection service)
    {
        var assembly = Assembly.GetExecutingAssembly();

        service.AddMediatR(opt => opt.RegisterServicesFromAssembly(assembly));

        service.AddTransient<ExceptionMiddleWare>();

        service.AddValidatorsFromAssembly(assembly);
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");

        service.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehaviors<,>));
    }
}
