using System;
using MatchInfo.WebApi.DbContexts;
using MatchInfo.WebApi.RepositoriesAbstractions;

namespace MatchInfo.WebApi.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        /// <summary>
        /// The matches repository.
        /// </summary>
        private readonly Lazy<IMatchesRepository> _matchesRepository;

        /// <summary>
        /// The matchOdds repository.
        /// </summary>
        private readonly Lazy<IMatchOddsRepository> _matchOddsRepository;

        private readonly Lazy<IUnitOfWorkMatchInfo> _unitOfWork;
        public RepositoryManager(MatchInfoDbContext dbContext)
        {
            _matchesRepository = new Lazy<IMatchesRepository>(() => new MatchesRepository(dbContext));
            _matchOddsRepository = new Lazy<IMatchOddsRepository>(() => new MatchOddsRepository(dbContext));
            _unitOfWork = new Lazy<IUnitOfWorkMatchInfo>(() => new UnitOfWorkMatchInfo(dbContext));
        }

        /// <summary>
        /// Gets or sets the matches repository.
        /// </summary>
        public IMatchesRepository MatchesRepository=> _matchesRepository.Value;

        /// <summary>
        /// Gets or sets the matchOdds repository.
        /// </summary>
        public IMatchOddsRepository MatchOddsRepository=>_matchOddsRepository.Value;

        public IUnitOfWorkMatchInfo UnitOfWork => _unitOfWork.Value;
    }
}
