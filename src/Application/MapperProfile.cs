using ProductCRUD.Application.Common.Dtos;
using ProductCRUD.Domain.Entities;

namespace ProductCRUD.Application;

public class MapperProfile: Profile
{
    public MapperProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(d => d.DateCreated, opt => opt.MapFrom(s => s != null ? s.Created.UtcDateTime : (DateTime?)null));
    }
}
