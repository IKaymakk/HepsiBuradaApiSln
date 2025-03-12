using HepsiBuradaApi.Application.Features.Product.Rules;
using HepsiBuradaApi.Application.Interfaces.UnitOfWorks;
using HepsiBuradaApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiBuradaApi.Application.Features.Product.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ProductRules _productRules;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork, ProductRules productRules)
    {
        _unitOfWork = unitOfWork;
        _productRules = productRules;
    }

    public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        IList<Domain.Entities.Product> products = await _unitOfWork.GetReadRepository<Domain.Entities.Product>().GetAllAsync();

        await _productRules.ProductTitleMustNotBeSame(products, request.Title);

        Domain.Entities.Product product = new(request.Title, request.Description, request.Price, request.Discount, request.BrandId);
        await _unitOfWork.GetWriteRepository<Domain.Entities.Product>().AddAsync(product);

        if (await _unitOfWork.SaveAsync() > 0)
        {
            foreach (var categoryId in request.CategoryIds)
                await _unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new ProductCategory()
                {
                    CategoryId = categoryId,
                    ProductId = product.Id
                });
            await _unitOfWork.SaveAsync();
        }

        return Unit.Value;
    }
}
