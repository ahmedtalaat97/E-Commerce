using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Core.Interfaces
{
    public interface ISpecification<T>
    {
        //Where
        public Expression<Func<T,bool>> Criteria { get; }

        //Include
        public List<Expression<Func<T, object>>> IncludeExpressions { get;  }

        //orderBy

        public Expression<Func<T,object>> OrderBy { get; }

        public Expression<Func<T, object>> OrderByDesc    { get; }

        //pagination
        public int Skip { get; }

        public int Take { get;  }

        public bool IsPaginated {  get;  }

    }
}
