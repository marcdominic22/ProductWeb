using ProductCRUD.Application.Interfaces;
using ProductCRUD.Domain.Entities;

namespace ProductCRUD.Application;
public interface IProductsRepository : IRepositoryBase<Product>
{
}
