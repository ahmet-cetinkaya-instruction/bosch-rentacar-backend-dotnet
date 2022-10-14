using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Business.Responses;

namespace Business.Responses.Brands
{
    public class PaginateListBrandResponse:PaginateListResponseBase
    {
        public IList<ListBrandResponse> Items { get; set; }
    }
}
