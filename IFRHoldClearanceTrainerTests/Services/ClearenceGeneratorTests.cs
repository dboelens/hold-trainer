using IFRHoldClearanceTrainer.models;
using IFRHoldClearanceTrainer.services;
using Moq;

namespace IFRHoldClearanceTrainerTests.services;

public class ClearenceGeneratorUnitTest
{
    private IList<VOR> vorList = new List<VOR>{
			new() 
			{
				Identifier = "PAE",
				IFRChartImage = "FOO",
				SectionalImage = "BAR"
			},
    };

    public ClearenceGeneratorUnitTest(){

    }
    
    [Theory]
    [InlineData(90, 5, 90)]
    [InlineData(91, 5, 90)]
    [InlineData(92, 5, 90)]
    [InlineData(93, 5, 95)]
    [InlineData(94, 5, 95)]
    [InlineData(95, 5, 95)]
    [InlineData(96, 5, 95)]
    [InlineData(97, 5, 95)]
    [InlineData(98, 5, 100)]
    [InlineData(99, 5, 100)]
    [InlineData(100, 5, 100)]
    [InlineData(187, 5, 185)]
    [InlineData(360, 5, 360)]
    [InlineData(0, 5, 0)]
    public void GenerateFixReturnsReasonableRadials(int radial, int distance, int result)
    {
        var randomMock = new Mock<IRandom>();
        randomMock.Setup(s => s.Next(1)).Returns(0);
        randomMock.Setup(s => s.Next(It.IsAny<int>())).Returns<int>(i => i == vorList.Count ? vorList.Count: distance);
        randomMock.Setup(s => s.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(radial);

        var generator = new ClearenceGenerater(randomMock.Object, vorList);
        var fix = generator.GenerateFix();

        Assert.Equal(result, fix.Radial);

    }
}