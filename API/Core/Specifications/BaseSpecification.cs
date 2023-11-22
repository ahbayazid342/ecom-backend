using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Npgsql.Internal.TypeHandlers.DateTimeHandlers;

namespace API.Core.Specifications
{
    public class BaseSpecification<T> : ISpecifications<T>
    {
        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        
        //public Expression<Func<T, object>> OrderBy { get; private set; }

        //public Expression<Func<T, object>> OrderByDescending {get; private set;}

        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        //sorting order
        /*protected void AddOrderBy(Expression<Func<T, object>> orderByExp) {
            OrderBy = orderByExp;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExp) {
            OrderByDescending = orderByDescExp;
        }*/
    }
}