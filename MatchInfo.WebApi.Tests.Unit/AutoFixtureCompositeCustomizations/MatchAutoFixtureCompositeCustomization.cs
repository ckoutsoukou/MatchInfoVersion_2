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
    public class MatchFootballAndNoOddsCustomization : CompositeCustomization
    {
        public MatchFootballAndNoOddsCustomization()
            : base(new MatchFootballCustomization(),
                   new NoMatchOddsCustomization())
        {
        }
    }



}
