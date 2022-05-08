using AutoMapper;
using Football.Business.Abstract;
using Football.DataAccess.Repositories;
using Football.Dtos.Request.Player;
using Football.Dtos.Response.Players;
using Football.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Business.Concrete
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository playerRepository;
        private readonly IMapper mapper;
        public PlayerService(IPlayerRepository playerRepository, IMapper mapper)
        {
            this.playerRepository = playerRepository;
            this.mapper = mapper;
        }

        public async Task<int> Add(AddPlayerRequest addPlayerRequest)
        {
            var player = mapper.Map<Player>(addPlayerRequest);
            return await playerRepository.Add(player);
        }

        public async Task Delete(int id)
        {
            await playerRepository.Delete(id);
        }

        public IList<Player> GetAll()
        {
            return playerRepository.GetAllEntities();
        }

        public async Task<ICollection<ListPlayerResponse>> GetAllAsync()
        {
            var players = await playerRepository.GetAllEntitiesAsync();
            var listPlayerResponse = mapper.Map<List<ListPlayerResponse>>(players);
            return listPlayerResponse;
        }

        public async Task<ListPlayerResponse> GetByIdList(int id)
        {
            var player = await playerRepository.GetEntityById(id);
            var listPlayerResponse = mapper.Map<ListPlayerResponse>(player);
            return listPlayerResponse;
        }

        public async Task<bool> IsExists(int id)
        {
            return await playerRepository.IsExists(id);
        }

        public async Task<int> Update(UpdatePlayerRequest updatePlayerRequest)
        {
            var player = mapper.Map<Player>(updatePlayerRequest);
            return await playerRepository.Update(player);
        }
    }
}
