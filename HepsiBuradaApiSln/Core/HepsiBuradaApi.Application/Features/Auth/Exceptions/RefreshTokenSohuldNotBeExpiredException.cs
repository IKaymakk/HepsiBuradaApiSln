using HepsiBuradaApi.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiBuradaApi.Application.Features.Auth.Exceptions;

public class RefreshTokenSohuldNotBeExpiredException : BaseException
{
    public RefreshTokenSohuldNotBeExpiredException() : base("Oturum Süresi Sona Ermiştir. Lütfen Tekrar Giriş Yapın.") { }
}
