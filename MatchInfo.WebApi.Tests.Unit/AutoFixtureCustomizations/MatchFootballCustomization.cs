using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchInfo.WebApi.Profiles;
using AutoMapper;
using Castle.Core.Resource;

namespace MatchInfo.WebApi.Tests.Unit.AutoFixtureCustomizations
{
    public class MatchFootballCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Entities.Match>(transform => transform
                .With(match => match.Sport, 1));
        }
    }

    public class NoMatchOddsCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Entities.Match>(transform => transform
                .With(cust => cust.MatchOdds, new List<Entities.MatchOdd>())
                );
        }
    }

}
