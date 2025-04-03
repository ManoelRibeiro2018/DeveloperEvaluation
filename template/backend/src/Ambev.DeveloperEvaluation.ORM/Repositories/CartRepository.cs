using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Util;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DefaultContext _context;

        public CartRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task DeleteAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }

        public async Task<ListEntity<Cart>> GetAllAsync(int pageNumber, int pageSize, string order, CancellationToken cancellationToken = default)
        {
            IQueryable<Cart> query = _context.Carts.AsNoTracking();

            if (!string.IsNullOrEmpty(order))
            {
                var orderParams = order.Split(",");
                IOrderedQueryable<Cart>? orderedQuery = null;

                foreach (var param in orderParams)
                {
                    var trimmedParam = param.Trim();
                    var descending = trimmedParam.EndsWith(" desc");
                    var propertyName = trimmedParam.Replace(" desc", "").Replace(" asc", "");

                    var parameter = Expression.Parameter(typeof(Cart), "p");
                    var property = Expression.Property(parameter, propertyName);
                    var lambda = Expression.Lambda(property, parameter);

                    var methodName = orderedQuery == null
                        ? (descending ? "OrderByDescending" : "OrderBy")
                        : (descending ? "ThenByDescending" : "ThenBy");

                    var method = typeof(Queryable).GetMethods()
                        .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(Cart), property.Type);

                    query = (IOrderedQueryable<Cart>)method.Invoke(null, new object[] { orderedQuery ?? query, lambda });
                    orderedQuery = (IOrderedQueryable<Cart>)query;
                }
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var carts = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new Cart()
            {
                Id = p.Id,
                Date = p.Date,
                UserId = p.UserId,
                Products = p.Products,
            })
            .ToListAsync(cancellationToken);

            return ListEntity<Cart>.ReturnList(totalPages, pageSize, carts);
        }

        public async Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Carts.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<Cart> UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
            return cart;
        }
    }
}
