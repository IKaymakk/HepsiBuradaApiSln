using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HepsiBuradaApi.Application.Exceptions;

public class ExceptionModel : ErrorStatusCode
{
    public IEnumerable<string> Errors { get; set; }

    public override string ToString() => JsonConvert.SerializeObject(Errors);
}

public class ErrorStatusCode
{
    public int StatusCode { get; set; }
}