using ProductCRUD.Application;
using ProductCRUD.Domain.Entities;
using ProductCRUD.Infrastructure.Data;

namespace ProductCRUD.Infrastructure.Repositories;

public class ProductsRepository : RepositoryBase<Product>, IProductsRepository
{
    public ProductsRepository(ApplicationDbContext context) : base(context)
    {

    }
}