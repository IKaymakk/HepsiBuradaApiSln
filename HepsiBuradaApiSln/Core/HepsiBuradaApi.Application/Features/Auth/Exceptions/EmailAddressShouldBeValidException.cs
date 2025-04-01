using HepsiBuradaApi.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiBuradaApi.Application.Features.Auth.Exceptions
{
    public class EmailAddressShouldBeValidException :BaseException
    {
        public EmailAddressShouldBeValidException(): base("Böyle bir mail adresi bulunmamaktadır!") { }
    }
}
