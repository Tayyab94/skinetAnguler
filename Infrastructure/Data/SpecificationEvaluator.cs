using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity>where TEntity:BaseEntity
    {

        public static IQueryable<TEntity>GetQuery(IQueryable<TEntity>inputQuery,ISpecification<TEntity>spec)
        {
                var qeuery=inputQuery;

                if(spec.Criteria!=null)
                    {
                        qeuery=qeuery.Where(spec.Criteria);
                    }

                    if(spec.OrderBy!=null)
                    {
                        qeuery=qeuery.OrderBy(spec.OrderBy);
                    }

                    if(spec.OrderByDescending!=null)
                    {
                        qeuery=qeuery.OrderByDescending(spec.OrderByDescending);
                    }

                    if(spec.IsPagingEnabled)
                    {
                            qeuery=qeuery.Skip(spec.Skip).Take(spec.Take);
                    }

                    qeuery=spec.Includes.Aggregate(qeuery,(current,include)=>current.Include(include));
        
            return qeuery;
        }
        
    }
}