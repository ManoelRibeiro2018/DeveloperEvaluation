﻿using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<List<Sale>> GetAllAsync(CancellationToken cancellationToken);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Guid> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);
    }
}
