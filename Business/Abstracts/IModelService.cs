using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Requests.Models;
using Business.Responses.Models;
using Core.Business.Requests;

namespace Business.Abstracts
{
    public interface IModelService
    {
        GetModelResponse GetById(GetModelRequest request);
        PaginateListModelResponse GetList(PageRequest request);
    }
}
