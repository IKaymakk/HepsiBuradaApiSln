using HepsiBuradaApi.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiBuradaApi.Application.Features.Auth.Exceptions;

public partial class UserAlreadyExistException : BaseException
{
    public UserAlreadyExistException() : base("Böyle bir kullanıcı zaten mevcut!") { }
}
