using HepsiBuradaApi.Application.DTOs;
using HepsiBuradaApi.Application.Interfaces.AutoMapper;
using HepsiBuradaApi.Application.Interfaces.UnitOfWorks;
using HepsiBuradaApi.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiBuradaApi.Application.Features.Product.Queries.GetAllProducts
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, IList<GetAllProductQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public GetAllProductQueryHandler(IUnitOfWork unitOfWork, IMapper _mapper)
        {
            _unitOfWork = unitOfWork;
            mapper = _mapper;
        }

        public async Task<IList<GetAllProductQueryResponse>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.GetReadRepository<Domain.Entities.Product>().GetAllAsync(include: x => x.Include(x => x.Brand));
            mapper.Map<BrandDto, Brand>(new Brand());

            var mappedProducts = mapper.Map<GetAllProductQueryResponse, Domain.Entities.Product>(products);
            foreach (var x in mappedProducts)
                x.Price -= x.Price * x.Discount / 100;

            return mappedProducts;
        }
    }
}
