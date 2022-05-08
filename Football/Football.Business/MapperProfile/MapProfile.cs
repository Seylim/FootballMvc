using AutoMapper;
using Football.Dtos.Request.Club;
using Football.Dtos.Request.Coach;
using Football.Dtos.Request.Leagues;
using Football.Dtos.Request.Player;
using Football.Dtos.Response;
using Football.Dtos.Response.Coaches;
using Football.Dtos.Response.Leagues;
using Football.Dtos.Response.Players;
using Football.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Business.MapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            //Club
            CreateMap<AddClubRequest, Club>();
            CreateMap<UpdateClubRequest, Club>();
            CreateMap<Club, DetailsClubResponse>();
            CreateMap<Club, ListClubResponse>();

            //Player
            CreateMap<AddPlayerRequest, Player>();
            CreateMap<UpdatePlayerRequest, Player>();
            CreateMap<Player, ListPlayerResponse>();

            //Coach
            CreateMap<AddCoachRequest, Coach>();
            CreateMap<UpdateCoachRequest, Coach>();
            CreateMap<Coach, ListCoachResponse>();

            //League
            CreateMap<AddLeagueRequest, League>();
            CreateMap<UpdateLeagueRequest, League>();
            CreateMap<League, DetailsLeagueResponse>();
            CreateMap<League, ListLeagueResponse>();
        }
    }
}
