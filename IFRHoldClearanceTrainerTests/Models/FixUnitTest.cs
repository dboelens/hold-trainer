using System;

namespace IFRHoldClearanceTrainerTests.Models;

public class FixUnitTest
{
    public FixUnitTest(){

    }

    [Fact]
    public void ShouldDisplayRadialInformation()
    {
        var fix = new Fix
        {
            FixIdentifier = "Foo",
            Radial = 100,
            DistanceUnits = 1
        };

        var fixDisplayString = fix.DisplayFix();

        Assert.Contains("Radial",fixDisplayString, StringComparison.InvariantCultureIgnoreCase);
        Assert.Contains("DME",fixDisplayString, StringComparison.InvariantCultureIgnoreCase);
    }
}
