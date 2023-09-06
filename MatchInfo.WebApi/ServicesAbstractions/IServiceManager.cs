namespace MatchInfo.WebApi.ServicesAbstractions
{
    public interface IServiceManager
    {
        IMatchService MatchService { get; }

        //IAccountService AccountService { get; }
    }
}
