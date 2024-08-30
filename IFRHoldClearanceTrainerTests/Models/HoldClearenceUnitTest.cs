using System;
using IFRHoldClearanceTrainer.models;

namespace IFRHoldClearanceTrainerTests.Models;

public class HoldClearenceUnitTest
{
    public HoldClearenceUnitTest(){
        
    }

    [Fact]
    public void HoldClearenceOmitsStandardTime(){
        var clearence = new HoldClearence
        {
            Direcion = Direction.North,
            Fix = new Fix
            {
                FixIdentifier = "PAE"
            },
            HoldType = HoldType.Time,
            HoldTypeUnit = 1,
            HoldDirection = HoldDirection.Left,
            EFCTime = DateTime.Now.TimeOfDay
        };

        var clearenceDisplayString = clearence.DisplayClearence();

        Assert.DoesNotContain("minute",clearenceDisplayString, StringComparison.InvariantCultureIgnoreCase);
    }

    [Fact]
    public void HoldClearenceOmitsStandardTurns(){
        var clearence = new HoldClearence
        {
            Direcion = Direction.North,
            Fix = new Fix
            {
                FixIdentifier = "PAE"
            },
            HoldType = HoldType.Time,
            HoldTypeUnit = 2,
            HoldDirection = HoldDirection.Right,
            EFCTime = DateTime.Now.TimeOfDay
        };

        var clearenceDisplayString = clearence.DisplayClearence();

        Assert.DoesNotContain("turns",clearenceDisplayString, StringComparison.InvariantCultureIgnoreCase);
    }

    [Fact]
    public void HoldClearenceDoesNotOmitMiles(){
        var clearence = new HoldClearence
        {
            Direcion = Direction.North,
            Fix = new Fix
            {
                FixIdentifier = "PAE"
            },
            HoldType = HoldType.Distance,
            HoldTypeUnit = 2,
            HoldDirection = HoldDirection.Left,
            EFCTime = DateTime.Now.TimeOfDay
        };

        var clearenceDisplayString = clearence.DisplayClearence();

        Assert.Contains("turns",clearenceDisplayString, StringComparison.InvariantCultureIgnoreCase);
    }

    [Fact]
    public void HoldClearenceDoesNotOmitNonStandardTime(){
        var clearence = new HoldClearence
        {
            Direcion = Direction.North,
            Fix = new Fix
            {
                FixIdentifier = "PAE"
            },
            HoldType = HoldType.Time,
            HoldTypeUnit = 2,
            HoldDirection = HoldDirection.Left,
            EFCTime = DateTime.Now.TimeOfDay
        };

        var clearenceDisplayString = clearence.DisplayClearence();

        Assert.Contains("minute",clearenceDisplayString, StringComparison.InvariantCultureIgnoreCase);
    }

    [Fact]
    public void HoldClearenceContainsExpectedElementsForMiles(){
        var expectedComponents = new List<string>{
            "VOR",
            "mile legs",
            "left turns",
            "Expect further clearence"
        };

        var clearence = new HoldClearence
        {
            Direcion = Direction.North,
            Fix = new Fix
            {
                FixIdentifier = "PAE"
            },
            HoldType = HoldType.Distance,
            HoldTypeUnit = 2,
            HoldDirection = HoldDirection.Left,
            EFCTime = DateTime.Now.TimeOfDay
        };

        var clearenceDisplayString = clearence.DisplayClearence();

        Assert.All(expectedComponents, component =>{
            Assert.Contains(component,clearenceDisplayString, StringComparison.InvariantCultureIgnoreCase);
        });   
    }
    [Fact]
    public void HoldClearenceContainsExpectedElementsForMinutes(){
        var expectedComponents = new List<string>{
            "VOR",
            "minute legs",
            "left turns",
            "Expect further clearence"
        };

        var clearence = new HoldClearence
        {
            Direcion = Direction.North,
            Fix = new Fix
            {
                FixIdentifier = "PAE"
            },
            HoldType = HoldType.Time,
            HoldTypeUnit = 2,
            HoldDirection = HoldDirection.Left,
            EFCTime = DateTime.Now.TimeOfDay
        };

        var clearenceDisplayString = clearence.DisplayClearence();

        Assert.All(expectedComponents, component =>{
            Assert.Contains(component,clearenceDisplayString, StringComparison.InvariantCultureIgnoreCase);
        });   
    }
}
