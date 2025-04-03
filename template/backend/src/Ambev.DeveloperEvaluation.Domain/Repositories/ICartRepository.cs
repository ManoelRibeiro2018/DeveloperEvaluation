using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Util;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ICartRepository
    {
        public Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default);
        public Task<Cart> UpdateAsync(Cart cart, CancellationToken cancellationToken = default);
        public Task DeleteAsync(Cart cart, CancellationToken cancellationToken = default);
        public Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<ListEntity<Cart>> GetAllAsync(int pageNumber, int pageSize, string? order, CancellationToken cancellationToken = default);
    }
}
