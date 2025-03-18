using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiBuradaApi.Application.Features.Auth.Command.Login;

public class LoginCommandResponse
{
    public string Token { get; set; }
    public string ResfreshToken { get; set; }
    public DateTime ExpireTime { get; set; }
}
