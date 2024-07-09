using ProductCRUD.Application.Common.Dtos;

namespace ProductCRUD.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand() : IRequest<ProductDto>
{
    public required int Id { get; set; }
    public string? ProductName { get; init; }
    public string? SupplierName { get; init; }
    public string? Description { get; init; }
    public int? Price { get; init; }
    public double? Tax { get; init; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
{
    private readonly IProductsRepository _repository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IProductsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.ReadSingle(t => t.Id == request.Id);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.ProductName ?? entity.Name;
        entity.SupplierName = request.SupplierName ?? entity.SupplierName;
        entity.Description = request.Description ?? entity.Description;
        entity.Price = request.Price ?? entity.Price;
        entity.Tax = request.Tax ?? entity.Tax;

        var result = await _repository.UpdateAsync(entity);

        return _mapper.Map<ProductDto>(result);

    }
}