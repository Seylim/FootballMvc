using Football.Dtos.Request.Player;
using Football.Dtos.Response.Players;
using Football.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Business.Abstract
{
    public interface IPlayerService
    {
        Task<ICollection<ListPlayerResponse>> GetAllAsync();
        IList<Player> GetAll();
        Task<ListPlayerResponse> GetByIdList(int id);
        Task<int> Add(AddPlayerRequest entity);
        Task<int> Update(UpdatePlayerRequest entity);
        Task Delete(int id);
        Task<bool> IsExists(int id);
    }
}
