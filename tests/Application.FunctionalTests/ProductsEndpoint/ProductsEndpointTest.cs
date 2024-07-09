namespace ProductCRUD.Application.FunctionalTests;

using ProductCRUD.Application.Products.Commands.CreateProduct;
using ProductCRUD.Application.Products.Commands.DeleteProduct;
using ProductCRUD.Application.Products.Commands.UpdateProduct;
using ProductCRUD.Application.Products.Queries.GetProduct;
using ProductCRUD.Application.Products.Queries.GetProducts;
using ProductCRUD.Domain.Entities;
using static Testing;
public class ProductsEndpointTest : BaseTestFixture
{
    
    [Test]
    public async Task GetProducts_ReturnsExpectedProducts()
    {
        var query = new GetProductsQuery();

        var result = await SendAsync(query);

        result.Count().Should().BeGreaterThan(2);
        result.Should().Contain(x => x.Id == 13);
        result.Should().NotBeNullOrEmpty();

    }

    [Test]
    public async Task GetByIDShouldRequireValidProductId()
    {
        var command = new GetProductQuery{ProductId = 99};
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task GetProductById_ReturnsExpectedProduct()
    {
        int id = 11;
        var query = new GetProductQuery
        {
            ProductId = id
        };

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Id.Should().Be(id);
        result.DateCreated.Should().NotBeNull();
    }

    [Test]
    public async Task CreateProduct_ReturnsNewProductId()
    {
        var command = new CreateProductCommand
        {
            Data = new(){
                Name = "Test Product Create",
                Description = "A Create Test Product for Seeding",
                SupplierName = "Create Test Supplier",
                Price = 225,
                Tax = 23.1
            }

        };

        var result = await SendAsync(command);

        var list = await FindAsync<Product>(result);

        list.Should().NotBeNull();
        list!.Name.Should().Be(command.Data.Name);
        list!.Description.Should().Be(command.Data.Description);
        list!.SupplierName.Should().Be(command.Data.SupplierName);
        list!.Price.Should().Be(command.Data.Price);
        list!.Tax.Should().Be(command.Data.Tax);
    }

    [Test]
    public async Task DeleteShouldRequireValidProductId()
    {
        var command = new DeleteProductCommand(99);
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteProductById()
    {
        int id = 13;

        await SendAsync(new DeleteProductCommand(id));

        var list = await FindAsync<Product>(id);

        list.Should().BeNull();
    }

    [Test]
    public async Task ShouldUpdateProduct()
    {
        var command = new UpdateProductCommand
        {
            Id = 15,
            ProductName = "Updated List Product",
            Price = 20,
            Tax = 0.5
        };

        await SendAsync(command);

        var list = await FindAsync<Product>(command.Id);

        list.Should().NotBeNull();
        list!.Name.Should().Be(command.ProductName);
        list.LastModifiedBy.Should().NotBeNull();
    }

    
    [Test]
    public async Task UpdateShouldRequireValidProductId()
    {
        var command = new UpdateProductCommand
        {
            Id = 99,
            ProductName = "Test Product Create",
            Description = "A Create Test Product for Seeding",
            SupplierName = "Create Test Supplier",
            Price = 225,
            Tax = 23.1
        };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }
}
