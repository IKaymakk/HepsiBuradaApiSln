using MediatR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiBuradaApi.Application.Features.Auth.Command.Login;

public class LoginCommandRequest : IRequest<LoginCommandResponse>
{
    [DefaultValue("ibrahimkaymak817@outlook.com")]
    public string Email { get; set; }
    [DefaultValue("5351Kaymak")]
    public string Password { get; set; }
}
