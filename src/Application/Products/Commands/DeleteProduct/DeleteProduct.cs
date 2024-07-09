namespace ProductCRUD.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(int Id) : IRequest;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductsRepository _repository;

    public DeleteProductCommandHandler(IProductsRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.ReadSingle(l => l.Id == request.Id);

        Guard.Against.NotFound(request.Id, entity);

        await _repository.DeleteAsync(entity);
    }
}
