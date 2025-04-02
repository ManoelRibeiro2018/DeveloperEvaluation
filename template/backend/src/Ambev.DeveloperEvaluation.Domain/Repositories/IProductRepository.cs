using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Util;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default);
        Task<ListEntity<Product>> GetAllAsync(string? order, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
        Task<ListEntity<Product>> GetAllProductsByCategoryAsync(string category, string? order, int pageNumber = 1, int pageSize = 10,  CancellationToken cancellationToken = default);
        Task<List<string>> GetAllCategoryAsync(CancellationToken cancellationToken);
    }
}
