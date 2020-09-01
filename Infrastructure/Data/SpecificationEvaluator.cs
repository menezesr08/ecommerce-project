using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
/*
The job of this class is to evaluate the specification against the inputQuery
The inputquery is a DBSet<T> where T could be Product, ProductBrand etc. So basically are just passing the products table for example.
The specification contains the actual conditions that we need to perform against the table.
We will return an Iqueryable which will be executed later.
*/
namespace Infrastructure.Data
{
    // TEntity is the as T just a different name. Both are just name for generic type
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null)
            {
                // An example of criteria here could be: x => x.id == id
                // We are just checking if the query (which is a table) has the id
                query = query.Where(spec.Criteria);
            }

            /* this method is the same as
             .Include(p => p.productType)
            .Include(p => p.productBrand)

            Here we are just aggregrate multiple include statements
            */
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            // query is an Iqueryable. So in this case you might be return a query such as:
            // .where(x.id==id).includes(p => p.productType).includes(p => p.productBrand)
            // above query is a iqueryable which will get executed when we apply the FirstorDefaultAsync method later
            return query;
        }
    }
}