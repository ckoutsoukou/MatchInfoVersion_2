namespace MatchInfo.WebApi.RepositoriesAbstractions
{

    public interface IRepositoryManager
    {
        /// <summary>
        /// Gets the matches repository.
        /// </summary>
        IMatchesRepository MatchesRepository { get; }

        /// <summary>
        /// Gets the matchOdds repository.
        /// </summary>
        IMatchOddsRepository MatchOddsRepository { get; }


        IUnitOfWorkMatchInfo UnitOfWork { get; }
    }
}
