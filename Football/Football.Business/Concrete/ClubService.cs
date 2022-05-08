using AutoMapper;
using Football.Business.Abstract;
using Football.DataAccess.Repositories;
using Football.Dtos.Request.Club;
using Football.Dtos.Response;
using Football.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Business.Concrete
{
    public class ClubService : IClubService
    {
        private readonly IClubRepository clubRepository;
        private readonly IMapper mapper;
        public ClubService(IClubRepository clubRepository, IMapper mapper)
        {
            this.clubRepository = clubRepository;
            this.mapper = mapper;
        }

        public async Task<int> Add(AddClubRequest addClubRequest)
        {
            var club = mapper.Map<Club>(addClubRequest);
            return await clubRepository.Add(club);
        }

        public async Task Delete(int id)
        {
            await clubRepository.Delete(id);
        }

        public IList<Club> GetAll()
        {
            return clubRepository.GetAllEntities();
        }

        public async Task<ICollection<ListClubResponse>> GetAllAsync()
        {
            var clubs = await clubRepository.GetAllEntitiesAsync();
            var listedClubResponse = mapper.Map<List<ListClubResponse>>(clubs);
            return listedClubResponse;
        }

        public async Task<DetailsClubResponse> GetByIdDetails(int id)
        {
            var club = await clubRepository.GetEntityById(id);
            var detailsClubResponse = mapper.Map<DetailsClubResponse>(club);
            return detailsClubResponse;
        }

        public async Task<ListClubResponse> GetByIdList(int id)
        {
            var club = await clubRepository.GetEntityById(id);
            var listClubResponse = mapper.Map<ListClubResponse>(club);
            return listClubResponse;
        }

        public async Task<bool> IsExists(int id)
        {
            return await clubRepository.IsExists(id);
        }

        public async Task<int> Update(UpdateClubRequest updateClubRequest)
        {
            var club = mapper.Map<Club>(updateClubRequest);
            var result = await clubRepository.Update(club);
            return result;
        }
    }
}
