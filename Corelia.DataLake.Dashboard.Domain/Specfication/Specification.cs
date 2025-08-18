using Corelia.DataLake.Dashboard.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Domain.Specfication
{
    public class Specification<TEntity, TKey> : ISpecification<TEntity, TKey>
    where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; set; } = null;
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>>? OrderBy { get; set; } = null;
        public Expression<Func<TEntity, object>>? OrderByDesc { get; set; } = null;
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationEnabled { get; set; }
        public Specification()
        {

        }
        public Specification(Expression<Func<TEntity, bool>>? expression)
        {
            Criteria = expression;
        }
        private protected virtual void AddIncludes()
        {

        }
        protected virtual void AddOrderBy(Expression<Func<TEntity, object>>? orderBy)
        {
            OrderBy = orderBy;
        }
        protected virtual void AddOrderByDesc(Expression<Func<TEntity, object>>? orderByDesc)
        {
            OrderByDesc = orderByDesc;
        }
        private protected virtual void ApplyPagination(int skip, int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }
    }
}
