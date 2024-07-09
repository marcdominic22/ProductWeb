using ProductCRUD.Application.Common.Dtos;
using ProductCRUD.Application.Products.Commands.CreateProduct;
using ProductCRUD.Application.Products.Commands.UpdateProduct;

namespace ClientApp.Data;

public class ProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProductDto>> GetProducts()
    {
        var response = await _httpClient.GetAsync("api/products");

        // Ensure success status code (200-299)
        response.EnsureSuccessStatusCode();

        // Read the content and deserialize
        var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();

        // Return an empty list if products is null (no content)
        return products ?? new List<ProductDto>();
    }

    public async Task<ProductDto> GetProductById(int id)
    {
        var response = await _httpClient.GetAsync($"api/products/{id}");

        response.EnsureSuccessStatusCode();

        // Read the content and deserialize
        var product = await response.Content.ReadFromJsonAsync<ProductDto>();

        // Return an empty list if products is null (no content)
        return product ?? new ProductDto();

    }

    public async Task UpdateProduct(int id, UpdateProductCommand command)
    {
        await _httpClient.PutAsJsonAsync($"api/products/{id}", command);
    }

    public async Task<int> CreateProduct(CreateProductCommand command)
    {
        var response = await _httpClient.PostAsJsonAsync("api/products", command);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<int>();
    }

    public async Task DeleteProductById(int id)
    {
        await _httpClient.DeleteAsync($"api/products/{id}");
    }
}
