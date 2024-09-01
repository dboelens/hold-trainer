using System;
using IFRHoldClearanceTrainer.models;
using IFRHoldClearanceTrainer.services;

namespace IFRHoldClearanceTrainerTests.Services;

public class BasicDirectionRulesEngineUnitTest
{
    public BasicDirectionRulesEngineUnitTest()
    {

    }

    [Theory]
    [InlineData(0, HoldDirection.Left, Direction.North)]
    [InlineData(0, HoldDirection.Right, Direction.North)]
    [InlineData(44, HoldDirection.Right, Direction.North)]
    [InlineData(44, HoldDirection.Left, Direction.North)]
    [InlineData(45, HoldDirection.Right, Direction.North)]
    [InlineData(45, HoldDirection.Left, Direction.East)]
    [InlineData(90, HoldDirection.Left, Direction.East)]
    [InlineData(90, HoldDirection.Right, Direction.East)]
    [InlineData(134, HoldDirection.Left, Direction.East)]
    [InlineData(134, HoldDirection.Right, Direction.East)]
    [InlineData(135, HoldDirection.Right, Direction.East)]
    [InlineData(135, HoldDirection.Left, Direction.South)]
    [InlineData(180, HoldDirection.Left, Direction.South)]
    [InlineData(180, HoldDirection.Right, Direction.South)]
    [InlineData(214, HoldDirection.Right, Direction.South)]
    [InlineData(214, HoldDirection.Left, Direction.South)]
    [InlineData(215, HoldDirection.Right, Direction.South)]
    [InlineData(215, HoldDirection.Left, Direction.West)]
    [InlineData(270, HoldDirection.Right, Direction.West)]
    [InlineData(270, HoldDirection.Left, Direction.West)]
    [InlineData(314, HoldDirection.Right, Direction.West)]
    [InlineData(314, HoldDirection.Left, Direction.West)]
    [InlineData(315, HoldDirection.Right, Direction.West)]
    [InlineData(315, HoldDirection.Left, Direction.North)]
    [InlineData(360, HoldDirection.Left, Direction.North)]
    [InlineData(360, HoldDirection.Right, Direction.North)]
    public void GenerateLogicalDirectionGeneratesLogicalDirection(int radial, HoldDirection holdDirection, Direction expectedResult){
        var directionRules = new BasicDirectionRulesEngine();

        var result = directionRules.GenerateLogicalDirection(radial, holdDirection);

        Assert.Equal(expectedResult, result);
    }
}
