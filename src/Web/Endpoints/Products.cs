using ProductCRUD.Application.Common.Dtos;
using ProductCRUD.Application.Products.Commands.CreateProduct;
using ProductCRUD.Application.Products.Commands.DeleteProduct;
using ProductCRUD.Application.Products.Commands.UpdateProduct;
using ProductCRUD.Application.Products.Queries.GetProduct;
using ProductCRUD.Application.Products.Queries.GetProducts;

namespace ProductCRUD.Web.Endpoints;

public class Products : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetProducts)
            .MapGet(GetProductById,"{id}")
            .MapPut(UpdateProduct, "{id}")
            .MapPost(CreateProduct)
            .MapDelete(DeleteProductById, "{id}");
    }

    public Task<List<ProductDto>> GetProducts(ISender sender)
    {
        return  sender.Send(new GetProductsQuery());
    }

    public Task<ProductDto> GetProductById(ISender sender, int Id)
    {
        return  sender.Send(new GetProductQuery{ProductId = Id});
    }

    public async Task<IResult> UpdateProduct(ISender sender, int id, UpdateProductCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteProductById(ISender sender, int id)
    {
        await sender.Send(new DeleteProductCommand(id));
        return Results.NoContent();
    }

    public Task<int> CreateProduct(ISender sender, CreateProductCommand command)
    {
        return sender.Send(command);
    }
}
