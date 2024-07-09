namespace ProductCRUD.Application.FunctionalTests;

using ProductCRUD.Domain.Entities;
using static Testing;

[TestFixture]
public abstract class BaseTestFixture
{
    [SetUp]
    public async Task TestSetUp()
    {
        await ResetState();
        
        var DateNow = DateTime.UtcNow;

        await AddListAsync(new List<Product>
        {
            new() {
                Id = 11,
                Name = "Test Product001",
                Description = "A Test Product for Seeding001",
                SupplierName = "Test Supplier001",
                Price = 25,
                Tax = 1.5,
                Created = DateNow
            },
            new() {
                Id = 12,
                Name = "Test Product002",
                Description = "A Test Product for Seeding002",
                SupplierName = "Test Supplier002",
                Price = 35,
                Tax = 3.5,
                Created = DateNow
            },
            new() {
                Id = 13,
                Name = "Test Product003",
                Description = "A Test Product for Seeding003",
                SupplierName = "Test Supplier003",
                Price = 45,
                Tax = 0.5,
                Created = DateNow
            },
            new() {
                Id = 14,
                Name = "Test Product004",
                Description = "A Test Product for Seeding004",
                SupplierName = "Test Supplier004",
                Price = 55,
                Tax = 2.5,
                Created = DateNow
            },
            new() {
                Id = 15,
                Name = "Test Product005",
                Description = "A Test Product for Seeding005",
                SupplierName = "Test Supplier005",
                Price = 25,
                Tax = 1.5,
                Created = DateNow
            },
        });

    }
}
