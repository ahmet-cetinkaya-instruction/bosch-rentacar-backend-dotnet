using Core.Business.Responses;

namespace Business.Responses.Models;

public class PaginateListModelResponse: PaginateListResponseBase
{
    public IList<ListModelResponse> Items { get; set; }
}