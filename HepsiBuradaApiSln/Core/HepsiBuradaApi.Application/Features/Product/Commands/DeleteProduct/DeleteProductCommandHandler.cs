using HepsiBuradaApi.Application.Interfaces.UnitOfWorks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiBuradaApi.Application.Features.Product.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest>
    {
        private readonly IUnitOfWork _unitofwork;

        public DeleteProductCommandHandler(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _unitofwork.GetReadRepository<Domain.Entities.Product>()
                .GetAsync(x => x.Id == request.id && !x.IsDeleted);
            product.IsDeleted = false;

            await _unitofwork.GetWriteRepository<Domain.Entities.Product>()
                .UpdateAsync(product);

            await _unitofwork.SaveAsync();

        }
    }
}
