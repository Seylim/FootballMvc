using AutoMapper;
using Football.Business.Abstract;
using Football.DataAccess.Repositories;
using Football.Dtos.Request.Coach;
using Football.Dtos.Response.Coaches;
using Football.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Business.Concrete
{
    public class CoachService : ICoachService
    {
        private readonly ICoachRepository coachRepository;
        private readonly IMapper mapper;
        public CoachService(ICoachRepository coachRepository, IMapper mapper)
        {
            this.coachRepository = coachRepository;
            this.mapper = mapper;
        }

        public async Task<int> Add(AddCoachRequest addCoachRequest)
        {
            var coach = mapper.Map<Coach>(addCoachRequest);
            return await coachRepository.Add(coach);
        }

        public async Task Delete(int id)
        {
            await coachRepository.Delete(id);
        }

        public IList<Coach> GetAll()
        {
            return coachRepository.GetAllEntities();
        }

        public async Task<ICollection<ListCoachResponse>> GetAllAsync()
        {
            var coaches = await coachRepository.GetAllEntitiesAsync();
            var listCoachResponse = mapper.Map<List<ListCoachResponse>>(coaches);
            return listCoachResponse;
        }

        public async Task<ListCoachResponse> GetById(int id)
        {
            var coach = await coachRepository.GetEntityById(id);
            var listCoachResponse = mapper.Map<ListCoachResponse>(coach);
            return listCoachResponse;
        }

        public async Task<bool> IsExists(int id)
        {
            return await coachRepository.IsExists(id);
        }

        public async Task<int> Update(UpdateCoachRequest updateCoachRequest)
        {
            var coach = mapper.Map<Coach>(updateCoachRequest);
            return await coachRepository.Update(coach);
        }
    }
}
