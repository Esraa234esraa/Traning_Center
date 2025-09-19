using System.Linq.Expressions;

namespace TrainingCenterAPI.Helpers
{
    public static class QueryableExtension
    {
        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, Dictionary<Expression<Func<T, bool>>, bool> filters, int pageNumber, int pageSize, ref ResponseDTO responseDto)
        {
            foreach (var filter in filters)
            {
                if (filter.Value)
                {
                    query = query.Where(filter.Key);
                }
            }
            if (pageNumber == 0)
                pageNumber = 1;

            if (pageSize == 0)
            {
                query = query.AsQueryable();

                responseDto.pageNumber = pageNumber;
                responseDto.PageSize = query.Count();
                responseDto.TotalItems = query.Count();
                responseDto.TotalPages = pageNumber;

                return query;
            }
            var count = query.Count();
            query = query
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).AsQueryable();


            responseDto.pageNumber = pageNumber;
            responseDto.PageSize = pageSize;
            responseDto.TotalItems = count;
            responseDto.TotalPages = (int)Math.Ceiling(count / (double)pageSize);


            return query;
        }


    }
}

