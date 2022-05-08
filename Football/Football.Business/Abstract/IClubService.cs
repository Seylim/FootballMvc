using Football.Dtos.Request.Club;
using Football.Dtos.Response;
using Football.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Business.Abstract
{
    public interface IClubService 
    {
        Task<ICollection<ListClubResponse>> GetAllAsync();
        IList<Club> GetAll();
        Task<ListClubResponse> GetByIdList(int id);
        Task<DetailsClubResponse> GetByIdDetails(int id);
        Task<int> Add(AddClubRequest addClubRequest);
        Task<int> Update(UpdateClubRequest updateClubRequest);
        Task Delete(int id);
        Task<bool> IsExists(int id);
    }
}
