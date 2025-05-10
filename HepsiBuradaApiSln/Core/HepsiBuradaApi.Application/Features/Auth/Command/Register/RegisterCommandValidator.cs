using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiBuradaApi.Application.Features.Auth.Command.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommandRequest>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(50)
            .MinimumLength(3)
            .WithName("İsim Soyisim");

        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(60)
            .EmailAddress()
            .MinimumLength(8);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MaximumLength(16)
            .MinimumLength(6)
            .WithName("Parola");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .MaximumLength(16)
            .MinimumLength(6)
            .Equal(x => x.Password)
            .WithName("Parola Tekrarı");

    }
}
