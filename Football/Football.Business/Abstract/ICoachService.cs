using Football.Dtos.Request.Coach;
using Football.Dtos.Response.Coaches;
using Football.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Business.Abstract
{
    public interface ICoachService
    {
        Task<ICollection<ListCoachResponse>> GetAllAsync();
        IList<Coach> GetAll();
        Task<ListCoachResponse> GetById(int id);
        Task<int> Add(AddCoachRequest addCoachRequest);
        Task<int> Update(UpdateCoachRequest updateCoachRequest);
        Task Delete(int id);
        Task<bool> IsExists(int id);
    }
}
