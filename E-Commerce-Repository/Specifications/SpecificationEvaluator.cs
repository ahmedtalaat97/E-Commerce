using E_Commerce_Core.Enities;
using E_Commerce_Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Repository.Specifications
{
    public  class SpecificationEvaluator<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {

        public static IQueryable<TEntity> BuildQuery(IQueryable<TEntity> inputQuery,ISpecification<TEntity> specification)
        {
            var query = inputQuery;

            if (specification.Criteria != null)
            
               query = query.Where(specification.Criteria);



            if(specification.OrderBy is not null)
                query = query.OrderBy(specification.OrderBy);

            if(specification.OrderByDesc is not null)
                query=query.OrderByDescending(specification.OrderByDesc);

            if (specification.IsPaginated)
           
             query=query.Skip(specification.Skip).Take(specification.Take);


            if (specification.IncludeExpressions.Any())
            {

                foreach (var item in specification.IncludeExpressions)
                {

                    query = query.Include(item);
                }

            }
            
        return query;
        }

    }
}
