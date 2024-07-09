using ProductCRUD.Application.Common.Dtos;
using ProductCRUD.Domain.Entities;

namespace ProductCRUD.Application.Products.Commands.CreateProduct;

public record CreateProductCommand : IRequest<int>
{
    public ProductDto Data { get; init; } = new ProductDto();
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IProductsRepository _repository;

    public CreateProductCommandHandler(IProductsRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new Product
        {
            Name = request.Data.Name,
            Description = request.Data.Description,
            SupplierName = request.Data.SupplierName,
            Price = request.Data.Price,
            Tax = request.Data.Tax
        };

        var result = await _repository.CreateAsync(entity);

        return result.Id;
    }
}