namespace ProductCRUD.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IProductsRepository _repository;

    public CreateProductCommandValidator(IProductsRepository repository)
    {
        _repository = repository;

        RuleFor(v => v.Data.Name)
            .NotEmpty()
            .MaximumLength(100)
            .MustAsync(BeUniqueProductName)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");

        RuleFor(v => v.Data.SupplierName)
            .NotEmpty();

        RuleFor(v => v.Data.Description)
            .NotEmpty();

        RuleFor(v => v.Data.Price)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(0).WithMessage("Price should not be negative.");

        RuleFor(v => v.Data.Tax)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(0).WithMessage("Price should not be negative.");
    }

    public async Task<bool> BeUniqueProductName(string productName, CancellationToken cancellationToken)
    {
        return await _repository.ReadAll(l => l.Name == productName) == null ? false : true;
    }
}
