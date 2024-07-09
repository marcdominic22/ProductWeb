using ProductCRUD.Application.Common.Dtos;
using ProductCRUD.Domain.Entities;

namespace ProductCRUD.Application.Products.Queries.GetProduct;

public class GetProductQuery : IRequest<ProductDto>
{
    public int ProductId { get; set; }
}

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
{
    private readonly IProductsRepository _repository;
    private readonly IMapper _mapper;

    public GetProductQueryHandler(IProductsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var query = await _repository.ReadSingle(x => x.Id == request.ProductId);

        if (query == null)
        {
            throw new NotFoundException(request.ProductId.ToString(),nameof(Product));
        }

        return _mapper.Map<ProductDto>(query);
    }
}

