using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace HepsiBuradaApi.Application.Interfaces.AutoMapper;

public interface IMapper
{
    TDestionation Map<TDestionation, TSource>(TSource source, string? ignore = null);
    IList<TDestionation> Map<TDestionation, TSource>(IList<TSource> source, string? ignore = null);

    TDestination Map<TDestination>(object source, string? ignore = null);
    IList<TDestination> Map<TDestination>(IList<object> source, string? ignore = null);
}
