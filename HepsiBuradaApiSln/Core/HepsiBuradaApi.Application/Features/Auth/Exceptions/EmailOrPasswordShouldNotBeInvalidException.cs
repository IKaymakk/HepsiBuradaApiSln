using HepsiBuradaApi.Application.Bases;

namespace HepsiBuradaApi.Application.Features.Auth.Exceptions;

public class EmailOrPasswordShouldNotBeInvalidException : BaseException
{
    public EmailOrPasswordShouldNotBeInvalidException() : base("Email veya parola hatalı!!") { }
}

