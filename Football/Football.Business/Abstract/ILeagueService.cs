using Football.Dtos.Request.Leagues;
using Football.Dtos.Response.Leagues;
using Football.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Business.Abstract
{
    public interface ILeagueService
    {
        Task<ICollection<ListLeagueResponse>> GetAllAsync();
        IList<League> GetAll();
        Task<ListLeagueResponse> GetByIdList(int id);
        Task<DetailsLeagueResponse> GetByIdDetails(int id);
        Task<int> Add(AddLeagueRequest addLeagueRequest);
        Task<int> Update(UpdateLeagueRequest updateLeagueRequest);
        Task Delete(int id);
        Task<bool> IsExists(int id);
    }
}
