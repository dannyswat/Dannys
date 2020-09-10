using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Dannys.Framework.Repositories
{
    public class SearchOptions<TEntity> : SearchCountOptions<TEntity> where TEntity : class
    {
        public List<SortOption<TEntity>> SortOptions { get; set; }

        public int StartOffset { get; set; }

        public int PageSize { get; set; } = 20;
    }

    public class SearchCountOptions<TEntity> where TEntity : class
    {
        public Expression<Func<TEntity, bool>> FilterExpression { get; set; }
    }

    public class SortOption<TEntity> where TEntity : class
    {
        public string Property { get; set; }

        public bool Descending { get; set; }
    }
}
