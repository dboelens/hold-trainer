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
        randomMock.Setup(s => s.Next(It.IsAny<int>())).Returns(distance);
        randomMock.Setup(s => s.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(radial);

        var generator = new ClearenceGenerater(randomMock.Object, vorList);
        var fix = generator.GenerateFix();

        Assert.Equal(result, fix.Radial);

    }

    [Theory]
    [InlineData(ChartType.VFRSectional, "BAR")]
    [InlineData(ChartType.IFREnRoute, "FOO")]
    public void GeneratorReturnsCorrectImageReference(ChartType chartType, string result){
        var randomMock = new Mock<IRandom>();
        var generator = new ClearenceGenerater(randomMock.Object, vorList);
        var vor = vorList.FirstOrDefault()!.Identifier;

       var resource = generator.GetFixResource(vor, chartType);

       Assert.Equal(result, resource);
    }

    [Fact]
    public void GeneratorWorksWithOneVOR(){
        var singleVorList = new List<VOR>{
            new(){
                Identifier = "Foo"
            }
        };

        var randomMock = new Mock<IRandom>(); 
        var generator = new ClearenceGenerater(randomMock.Object, singleVorList);

        var identifier = generator.GetRandomFixIdentifier();

        Assert.Equal(singleVorList.First().Identifier, identifier);
    }

    [Fact]
    public void GeneratorWorksWithMultipleVORs(){
        int sequence1 = 3;
        int sequence2 = 1;
        int sequence3 = 2;

        var vorList = new List<VOR>{
            new(){
                Identifier = "Foo"
            },
            new(){
                Identifier = "Bar"
            },
            new(){
                Identifier = "Fiz"
            }
        };

        var randomMock = new Mock<IRandom>(); 
        randomMock.SetupSequence(s => s.Next(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(sequence1)
            .Returns(sequence2)
            .Returns(sequence3);
        var generator = new ClearenceGenerater(randomMock.Object, vorList);
        var vorArray = vorList.ToArray();
        
        var identifier1 = generator.GetRandomFixIdentifier();
        var identifier2 = generator.GetRandomFixIdentifier();
        var identifier3 = generator.GetRandomFixIdentifier();

        Assert.Equal(vorArray[sequence1].Identifier, identifier1);
        Assert.Equal(vorArray[sequence2].Identifier, identifier2);
        Assert.Equal(vorArray[sequence3].Identifier, identifier3);
    }

}