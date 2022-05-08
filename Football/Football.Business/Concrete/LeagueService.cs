using AutoMapper;
using Football.Business.Abstract;
using Football.DataAccess.Repositories;
using Football.Dtos.Request.Leagues;
using Football.Dtos.Response.Leagues;
using Football.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Business.Concrete
{
    public class LeagueService : ILeagueService
    {
        private readonly ILeagueRepository leagueRepository;
        private readonly IMapper mapper;
        public LeagueService(ILeagueRepository leagueRepository, IMapper mapper)
        {
            this.leagueRepository = leagueRepository;
            this.mapper = mapper;
        }

        public async Task<int> Add(AddLeagueRequest addLeagueRequest)
        {
            var league = mapper.Map<League>(addLeagueRequest);
            return await leagueRepository.Add(league);
        }

        public async Task Delete(int id)
        {
            await leagueRepository.Delete(id);
        }

        public IList<League> GetAll()
        {
            return leagueRepository.GetAllEntities();
        }

        public async Task<ICollection<ListLeagueResponse>> GetAllAsync()
        {
            var leagues = await leagueRepository.GetAllEntitiesAsync();
            var listLeagueResponse = mapper.Map<List<ListLeagueResponse>>(leagues);
            return listLeagueResponse;
        }

        public async Task<DetailsLeagueResponse> GetByIdDetails(int id)
        {
            var league = await leagueRepository.GetEntityById(id);
            var detailLeagueResponse = mapper.Map<DetailsLeagueResponse>(league);
            return detailLeagueResponse;
        }

        public async Task<ListLeagueResponse> GetByIdList(int id)
        {
            var league = await leagueRepository.GetEntityById(id);
            var listLeagueResponse = mapper.Map<ListLeagueResponse>(league);
            return listLeagueResponse;
        }

        public async Task<bool> IsExists(int id)
        {
            return await leagueRepository.IsExists(id);
        }

        public async Task<int> Update(UpdateLeagueRequest updateLeagueRequest)
        {
            var league = mapper.Map<League>(updateLeagueRequest);
            return await leagueRepository.Update(league);
        }
    }
}
