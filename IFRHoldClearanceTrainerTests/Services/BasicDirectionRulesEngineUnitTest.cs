using System;
using IFRHoldClearanceTrainer.models;
using IFRHoldClearanceTrainer.services;
using Moq;

namespace IFRHoldClearanceTrainerTests.Services;

public class BasicDirectionRulesEngineUnitTest
{
    [Fact]
    public void GenerateLogicalDirectionThrowsForZeroRadial()
    {
        var randomMock = new Mock<IRandom>();
        var directionRules = new BasicDirectionRulesEngine(randomMock.Object);

        Assert.Throws<InvalidDataException>(() => directionRules.GenerateLogicalDirection(0));

    }
    [Fact]
    public void GenerateLogicalDirectionThrowsForLargeRadial()
    {
         var randomMock = new Mock<IRandom>();
        var directionRules = new BasicDirectionRulesEngine(randomMock.Object);

        Assert.Throws<InvalidDataException>(() => directionRules.GenerateLogicalDirection(361));
    }

    [Theory]
    [InlineData(44, 2, Direction.NorthEast)]
    [InlineData(44, 3, Direction.SouthWest)]
    [InlineData(90, 2, Direction.East)]
    [InlineData(90, 3, Direction.West)]
    [InlineData(135, 2, Direction.NorthWest)]
    [InlineData(135, 3, Direction.SouthEast)]
    [InlineData(180, 2, Direction.North)]
    [InlineData(180, 3, Direction.South)]
    [InlineData(215, 2, Direction.NorthEast)]
    [InlineData(215, 3, Direction.SouthWest)]
    [InlineData(270, 2, Direction.East)]
    [InlineData(270, 3, Direction.West)]
    [InlineData(315, 2, Direction.NorthWest)]
    [InlineData(315, 3, Direction.SouthEast)]
    [InlineData(360, 2, Direction.North)]
    [InlineData(360, 3, Direction.South)]
    public void GenerateLogicalDirectionGeneratesLogicalDirection(int radial, int randomResult, Direction expectedResult){

        var randomMock = new Mock<IRandom>();
        randomMock.Setup(s => s.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(randomResult);

        var directionRules = new BasicDirectionRulesEngine(randomMock.Object);

        var result = directionRules.GenerateLogicalDirection(radial);

        Assert.Equal(expectedResult, result);
    }
}
