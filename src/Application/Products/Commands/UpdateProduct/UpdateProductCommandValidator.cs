namespace ProductCRUD.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    private readonly IProductsRepository _repository;

    public UpdateProductCommandValidator(IProductsRepository repository)
    {
        _repository = repository;

        RuleFor(v => v.ProductName)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueProductName)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");

        RuleFor(v => v.Price)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(0).WithMessage("Price should not be negative.");

        RuleFor(v => v.Tax)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(0).WithMessage("Price should not be negative.");
    }

    public async Task<bool> BeUniqueProductName(string productName, CancellationToken cancellationToken)
    {
        return await _repository.ReadAll(l => l.Name == productName) == null ? false : true;
    }
}
