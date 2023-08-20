using System.Collections.Generic;

namespace UserMgmtApi.Dtos
{
    public class PagedDto<T>
    {
        public PagedDto(int totalCount, int totalPages, int pageNumber, int pageSize, T[] items)
        {
            TotalCount = totalCount;
            TotalPages = totalPages;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Items = items;
        }

        public int TotalCount { get;  }
        public int TotalPages { get;  }
        public int PageNumber { get; }
        public int PageSize { get; }
        public T[] Items { get; }
    }
}
