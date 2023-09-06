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
    public class MatchFootballAndNoOddsDataAttribute : AutoDataAttribute
    {
        public MatchFootballAndNoOddsDataAttribute() : base(() => new Fixture().Customize(new MatchFootballAndNoOddsCustomization())) { }
    }



}
