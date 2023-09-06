using System;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
//using NSubstitute;
//using NSubstitute.ExceptionExtensions;
//using NSubstitute.ReturnsExtensions;
using MatchInfo.WebApi.Models;
using MatchInfo.WebApi.Repositories;
using MatchInfo.WebApi.Services;
using Xunit;
using AutoFixture;
using Microsoft.Extensions.DependencyModel;
using Moq;
using MatchInfo.WebApi.RepositoriesAbstractions;
using AutoMapper;
using MatchInfo.WebApi.Profiles;
using MatchInfo.WebApi.ServicesAbstractions;
using Castle.Core.Internal;
using NSubstitute;
using AutoFixture.Xunit2;
using MatchInfo.WebApi.Tests.Unit.AutoFixtureDataAttributes;
using Newtonsoft.Json.Linq;
using NSubstitute.ReturnsExtensions;
using NSubstitute.ExceptionExtensions;
using Microsoft.AspNetCore.Mvc;

namespace MatchInfo.WebApi.Tests.Unit;

public class MatchServiceTests
{
    //private readonly IFixture _fixture;
    private readonly Mock<IRepositoryManager> _repositoryManagerMock;
    private static IMapper _mapper;
    // private readonly IRepositoryManager _repositoryManagerMock;

    private readonly Mock<IMatchOddService> _matchOddServiceMock;
    //private readonly IMatchOddService _matchOddServiceMock;

    private readonly MatchService _sut;

    //public IMapper GetMapper()
    //{
    
    //    var configuration = new MapperConfiguration(cfg => {
    //        cfg.AddProfile(new MatchProfile());
    //        cfg.AddProfile(new MatchOddProfile());
    //    });
    //    return new Mapper(configuration);
    //}

    public MatchServiceTests()
    {
        if (_mapper == null)
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile(new MatchProfile());
                cfg.AddProfile(new MatchOddProfile());
            });
            _mapper = new Mapper(configuration);
        }
        _repositoryManagerMock = _repositoryManagerMock?? new Mock<IRepositoryManager>(MockBehavior.Strict);
        _matchOddServiceMock = _matchOddServiceMock?? new Mock<IMatchOddService>(MockBehavior.Strict);
        _sut = _sut?? new MatchService(_repositoryManagerMock.Object, _mapper, _matchOddServiceMock.Object);
    }

    [Fact]
    public void GetAll_ShouldReturnEmpltyList_WhenNoMatchExist()
    {
        //Arrange
        var testList = new List<Match>();
        _repositoryManagerMock.Setup(x => x.MatchesRepository.GetList(null, It.IsAny<bool>(), It.IsAny<string>())).Returns(()=> new List<Entities.Match>());

        //Act
        var result = _sut.GetList(null, It.IsAny<bool>(), It.IsAny<string>());

        //Assert
        result.Should().BeEmpty();
    }

  

    [Theory]
    [InlineAutoData("PAO", "AEK", "1","12X")]
    [InlineAutoData("ARHS", "AEK", "2", "1XX")]
    public void GetAll_ShouldReturnMatches_WhenSomeMatchExist(string teamA, string teamB, byte sport, string specifier, Entities.Match match)
    {
        //Arrange
        match.TeamA = teamA;
        match.TeamB = teamB;
        match.Sport = sport;
        match.MatchOdds.Select(c => { c.MatchId = match.Id; c.Specifier = specifier; return c; }).ToList();
        var expectedMatches = new List<Entities.Match> { match };
        _repositoryManagerMock.Setup(x => x.MatchesRepository.GetList(null, It.IsAny<bool>(), It.IsAny<string>())).Returns(() => expectedMatches);
        var expecteMatchDtos = _mapper.Map<List<Entities.Match>, List<Models.MatchDto>>(expectedMatches);

        //Act
        var result = _sut.GetList(null, It.IsAny<bool>(), It.IsAny<string>());

        //Assert
        result.Should().BeEquivalentTo(expecteMatchDtos);
    }


    [Theory]
    [AutoData]
    public void GetById_ShouldThrowsKeyNotFoundException_WhenNoMatchExists(int id)
    {
        // Arrange
        _repositoryManagerMock.Setup(x => x.MatchesRepository.GetById(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<string>())).Returns(() => null);

        // Act
        var resultAction = () => _sut.GetById(id);

        // Assert
        resultAction.Should().Throw<KeyNotFoundException>().WithMessage($"Match with id {id} does not exist");
    }


    [Theory]
    [InlineAutoData("PAO", "AEK", "1", "12X")]
    [InlineAutoData("ARHS", "AEK", "2", "1XX")]
    public void GetByIdAsync_ShouldReturnMatch_WhenMatchExists(string teamA, string teamB, byte sport, string specifier, Entities.Match existingMatch)
    {
        // Arrange
        existingMatch.TeamA = teamA;
        existingMatch.TeamB = teamB;
        existingMatch.Sport = sport;
        existingMatch.MatchOdds.Select(c => { c.MatchId = existingMatch.Id; c.Specifier = specifier; return c; }).ToList();

        _repositoryManagerMock.Setup(x => x.MatchesRepository.GetById(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<string>())).Returns(() => existingMatch);

        // Act
        var result = _sut.GetById(existingMatch.Id);

        // Assert
        result.Should().BeEquivalentTo(_mapper.Map<Entities.Match, Models.MatchDto>(existingMatch));
    }


    //[Theory]
    //[InlineAutoData("PAO", "AEK", "Football", "12X")]
    //[InlineAutoData("ARHS", "AEK", "Basketball", "1XX")]
    //public void Insert_ShouldInsertMatch_WhenDetailsAreValid(string teamA, string teamB, string sportCategory, string specifier, Models.MatchDto insertedMatchDto)
    //{
    //    // Arrange
    //    insertedMatchDto.TeamA = teamA;
    //    insertedMatchDto.TeamB = teamB;
    //    insertedMatchDto.SportCategory = sportCategory;
    //    insertedMatchDto.MatchOddDtos.Select(c => { c.MatchId = insertedMatchDto.Id; c.Specifier = specifier; return c; }).ToList();
        
        
        
 
 
        
    //    Entities.Match insertedMatch = _mapper.Map<Models.MatchDto, Entities.Match>(insertedMatchDto);

    //   // _repositoryManagerMock.Setup(x => x.MatchesRepository.Insert(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

    //    // Act
    //  //  var result =  _sut.CreateAsync(user);

    //    // Assert
    //  // result.Should().BeTrue();

    //    //////////////////////////////////////////////////////////////////////////////

    //    _repositoryManagerMock.Setup(r => r.MatchesRepository.Insert(It.IsAny<Entities.Match>(), true)).Returns(() => insertedMatch);
      

    //    var result = _sut.Insert(insertedMatchDto);
    //    _repositoryManagerMock.Verify(x => x.MatchesRepository.Insert(It.IsAny<Entities.Match>(), true), Times.Once);
    //    result.Should().BeEquivalentTo(insertedMatchDto);

    //    //Assert.Equal(matchDto.Name, employee.Name);
    //    //Assert.Equal(matchDto.Age, employee.Age);
    //    //Assert.Equal(matchDto.AccountNumber, employee.AccountNumber);
    //}

    //[Fact]
    //public async Task CreateAsync_ShouldCreateUser_WhenDetailsAreValid()
    //{
    //    // Arrange
    //    var user = new User
    //    {
    //        Id = Guid.NewGuid(),
    //        FullName = "Nick Chapsas"
    //    };
    //    _userRepository.CreateAsync(user).Returns(true);

    //    // Act
    //    var result = await _sut.CreateAsync(user);

    //    // Assert
    //    result.Should().BeTrue();
    //}

    //[Fact]
    //public async Task CreateAsync_ShouldLogMessageAndException_WhenExceptionIsThrown()
    //{
    //    // Arrange
    //    var user = new User
    //    {
    //        Id = Guid.NewGuid(),
    //        FullName = "Nick Chapsas"
    //    };
    //    var sqliteException = new SqliteException("Something went wrong", 500);
    //    _userRepository.CreateAsync(user)
    //        .Throws(sqliteException);

    //    // Act
    //    var requestAction = async () => await _sut.CreateAsync(user);

    //    // Assert
    //    await requestAction.Should()
    //        .ThrowAsync<SqliteException>().WithMessage("Something went wrong");
    //    _logger.Received(1).LogError(Arg.Is(sqliteException),
    //        Arg.Is("Something went wrong while creating a user"));
    //}

    //[Fact]
    //public async Task DeleteByIdAsync_ShouldDeleteUser_WhenUserExists()
    //{
    //    // Arrange
    //    var userId = Guid.NewGuid();
    //    _userRepository.DeleteByIdAsync(userId).Returns(true);

    //    // Act
    //    var result = await _sut.DeleteByIdAsync(userId);

    //    // Assert
    //    result.Should().BeTrue();
    //}

    //[Fact]
    //public async Task DeleteByIdAsync_ShouldNotDeleteUser_WhenUserDoesntExists()
    //{
    //    // Arrange
    //    var userId = Guid.NewGuid();
    //    _userRepository.DeleteByIdAsync(userId).Returns(false);

    //    // Act
    //    var result = await _sut.DeleteByIdAsync(userId);

    //    // Assert
    //    result.Should().BeFalse();
    //}


    //[Fact]
    //public async Task DeleteByIdAsync_ShouldLogMessageAndException_WhenExceptionIsThrown()
    //{
    //    // Arrange
    //    var userId = Guid.NewGuid();
    //    var sqliteException = new SqliteException("Something went wrong", 500);
    //    _userRepository.DeleteByIdAsync(userId)
    //        .Throws(sqliteException);

    //    // Act
    //    var requestAction = async () => await _sut.DeleteByIdAsync(userId);

    //    // Assert
    //    await requestAction.Should()
    //        .ThrowAsync<SqliteException>().WithMessage("Something went wrong");
    //    _logger.Received(1).LogError(Arg.Is(sqliteException),
    //        Arg.Is("Something went wrong while deleting user with id {0}"),
    //        Arg.Is(userId));
    //}
}
