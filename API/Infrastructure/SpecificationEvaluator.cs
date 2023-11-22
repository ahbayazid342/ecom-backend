using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core.Specifications;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure
{
    public class SpecificationEvaluator<TEntity> where TEntity:BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
         ISpecifications<TEntity> spec){

            var query = inputQuery;

            if (spec.Criteria != null) {
                query = query.Where (spec.Criteria);
            }


            //sorting here
            /*if (spec.OrderBy != null) {
                query = query.OrderBy (spec.OrderBy);
            }

            if (spec.OrderByDescending != null) {
                query = query.OrderByDescending (spec.OrderByDescending);
            }*/

            query = spec.Includes.Aggregate(query, (current, include) =>  current.Include(include));

            return query;
         }
    }
}