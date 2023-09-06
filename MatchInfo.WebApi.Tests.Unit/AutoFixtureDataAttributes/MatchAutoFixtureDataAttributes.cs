using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchInfo.WebApi.Profiles;
using AutoMapper;
using AutoFixture.Xunit2;
using MatchInfo.WebApi.Tests.Unit.AutoFixtureCustomizations;

namespace MatchInfo.WebApi.Tests.Unit.AutoFixtureDataAttributes
{
    public class MatchFootballDataAttribute : AutoDataAttribute
    {
        public MatchFootballDataAttribute() : base(() => new Fixture().Customize(new MatchFootballCustomization())) { }
    }

    public class NoMatchOddsDataAttribute : AutoDataAttribute
    {
        public NoMatchOddsDataAttribute() : base(() => new Fixture().Customize(new NoMatchOddsCustomization())) { }
    }

}
