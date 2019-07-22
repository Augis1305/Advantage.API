using System.Collections.Generic;
using System.Linq;

namespace Advantage.API{
    // Pagination is numbering pages in document. In our case we are numbering pages in website
    public class PaginatedResponse<T>{
        public PaginatedResponse(IEnumerable<T> data, int i, int len){
            // [1] page, 10 results. We are taking page 1 - 1 * 10. That means we are skipping 0 and taking 10 first elements from the dataset. 
            Data = data.Skip((i - 1) * len).Take(len).ToList();
            Total = data.Count();
        }

        public int Total { get; set; } 
        public IEnumerable<T> Data { get; set; }
    }
}