using System.Collections.Generic;
using System.Linq;
using ajf.ns_planner.datalayer;
using ajf.ns_planner.datalayer.Repositories;
using ajf.ns_planner.servicelayer.Models;
using AutoMapper;

namespace ajf.ns_planner.servicelayer
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;

        public MatchService(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public void SetMatches(IEnumerable<Match> matches, UnitOfWork unitOfWork)
        {
            _matchRepository.RemoveAll(unitOfWork);

            var enumerable = from m in matches select Match(m);
            _matchRepository.AddRange(enumerable,unitOfWork);
        }

        private static datalayer.Models.Match Match(Match serviceLayerMatch)
        {
            var dataLayerMatch = new datalayer.Models.Match();
            Mapper.Map(serviceLayerMatch, dataLayerMatch);
            return dataLayerMatch;
        }
    }

}