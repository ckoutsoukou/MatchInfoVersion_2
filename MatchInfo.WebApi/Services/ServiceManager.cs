using AutoMapper;
using MatchInfo.WebApi.Repositories;
using MatchInfo.WebApi.RepositoriesAbstractions;
using MatchInfo.WebApi.Services;
using MatchInfo.WebApi.ServicesAbstractions;
using System;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IMatchService> _lazyMatchService;
        private readonly Lazy<IMatchOddService> _lazyMatchOddService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _lazyMatchService = new Lazy<IMatchService>(() => new MatchService(repositoryManager, mapper, MatchOddService));
            _lazyMatchOddService = new Lazy<IMatchOddService>(() => new MatchOddService(repositoryManager, mapper));
        }

        public IMatchService MatchService => _lazyMatchService.Value;

        public IMatchOddService MatchOddService => _lazyMatchOddService.Value;
    }
}
