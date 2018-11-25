using System.Collections.Generic;

namespace JTPBlog.Models
{
    public class PaginationViewModel<T>
    {
        public int NumberOfRows { get; set; }
        public int PageNumber { get; set; }
        public int Offset { get; set; }
        public IEnumerable<T> Content { get; set; }
    }
}
