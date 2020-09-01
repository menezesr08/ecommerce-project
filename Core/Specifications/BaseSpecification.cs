using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
        }

        // This is just a placeholder for an expression/clause like the where clause.
        // One example of a criteria could be: x=> x.id == id
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria {get;}

        public List<Expression<Func<T, object>>> Includes {get;} = 
            new List<Expression<Func<T, object>>>();

        /* Here we collect all the include expressions like
            .Include(p => p.productType)
            .Include(p => p.productBrand)

        We add then to a list called includes. And we can access this list when we are evaluating this specification. 
        */
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}