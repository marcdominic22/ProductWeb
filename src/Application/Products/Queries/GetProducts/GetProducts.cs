using ProductCRUD.Application.Common.Dtos;
using ProductCRUD.Domain.Entities;

namespace ProductCRUD.Application.Products.Queries.GetProducts;

public class GetProductsQuery : IRequest<List<ProductDto>>;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
{
    private readonly IProductsRepository _repository;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IProductsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var query = await _repository.ReadAll(orderBy: x => x.Id);

        if (!query.Any() || query == null)
        {
            throw new NotFoundException("None", nameof(Product));
        }

        return _mapper.Map<List<ProductDto>>(query);
    }
}
