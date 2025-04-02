using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Util;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DefaultContext _context;

        public ProductRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Products.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await GetByIdAsync(id, cancellationToken);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }

        public async Task<ListEntity<Product>> GetAllAsync(string? order, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            IQueryable<Product> query = _context.Products;

            query = order?.ToLower() switch
            {
                "price asc" => query.OrderBy(p => p.Price),
                "price desc" => query.OrderByDescending(p => p.Price),
                "title asc" => query.OrderBy(p => p.Title),
                "title desc" => query.OrderByDescending(p => p.Title),
                _ => query.OrderBy(p => p.Id)
            };

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);


            var products = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return ListEntity<Product>.ReturnList(totalPages, pageSize, products);

        }

        public async Task<ListEntity<Product>> GetAllProductsByCategoryAsync(string category, string? order, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.Products
             .Where(p =>  p.Category.Equals(category));

            if (!string.IsNullOrEmpty(order))
            {
                var orderParams = order.Split(",");
                IOrderedQueryable<Product>? orderedQuery = null;

                foreach (var param in orderParams)
                {
                    var trimmedParam = param.Trim();
                    var descending = trimmedParam.EndsWith(" desc");
                    var propertyName = trimmedParam.Replace(" desc", "").Replace(" asc", "");

                    var parameter = Expression.Parameter(typeof(Product), "p");
                    var property = Expression.Property(parameter, propertyName);
                    var lambda = Expression.Lambda(property, parameter);

                    var methodName = orderedQuery == null
                        ? (descending ? "OrderByDescending" : "OrderBy")
                        : (descending ? "ThenByDescending" : "ThenBy");

                    var method = typeof(Queryable).GetMethods()
                        .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                        .MakeGenericMethod(typeof(Product), property.Type);

                    query = (IOrderedQueryable<Product>)method.Invoke(null, new object[] { orderedQuery ?? query, lambda });
                    orderedQuery = (IOrderedQueryable<Product>)query;
                }
            }



            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var products = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new Product()
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                Description = p.Description,
                Category = p.Category,
                Image = p.Image,
                Rating = new()
                {
                    Rate = p.Rating.Rate,
                    Count = p.Rating.Count
                }
            })
            .ToListAsync(cancellationToken);

            return ListEntity<Product>.ReturnList(totalPages, pageSize, products);
        }

        public async Task<List<string>> GetAllCategoryAsync(CancellationToken cancellationToken)
        {
            var result = await _context.Products.Select(p => p.Category)
                .Distinct()
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
