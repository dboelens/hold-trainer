namespace IFRHoldClearanceTrainer.services;

using IFRHoldClearanceTrainer.models;

public interface IClearenceGenerator
{
    public HoldClearence Generate();
    public string GetFixResource(string identifier, ChartType chartType);
}

public class ClearnceGenerater: IClearenceGenerator
{
    private IRandom random;
    private IList<VOR> vorList;
    private const int RADIALMIN = 0;
    private const int RADIALMAX = 360;
    private const int MAXDISTANCE = 10;
    private const int MAXLEG = 10;
    private const int MAXIMUMCLEARENCEDURATION = 60;

    public ClearnceGenerater(IRandom randomizer, IList<VOR> vorList)
    {
        random = randomizer;
        this.vorList = vorList;
    }

    public HoldClearence Generate()
    {
        var direction = (Direction)random.Next(
            Enum.GetValues(typeof(Direction)).Cast<int>().Max()
        );
        var fix = GenerateFix();
        var holdType = (HoldType)random.Next(0,2);
        var holdTypeUnit = random.Next(MAXLEG);
        var holdDirection = (HoldDirection)random.Next(0,2);
        var efcTime = DateTime.UtcNow.TimeOfDay.Add(
            new TimeSpan(
                0,
                random.Next(MAXIMUMCLEARENCEDURATION),
                0
            )
        );

        return new HoldClearence{
            Direcion = direction,
            Fix = fix,
            HoldType = holdType,
            HoldTypeUnit = holdTypeUnit,
            HoldDirection = holdDirection,
            EFCTime = efcTime
        };
    }

    public Fix GenerateFix()
    {
        return new Fix
        {
            FixIdentifier = GetFixIdentifier(),
            Radial = random.Next(RADIALMIN, RADIALMAX),
            DistanceUnits = random.Next(MAXDISTANCE)
        };
    }

    public string GetFixIdentifier()
    {
        return vorList.ToArray()[random.Next(vorList.Count)].Identifier;
    }

    public string GetFixResource(string identifier, ChartType chartType)
    {
        var vor = vorList.Single(
            s => s.Identifier.Equals(
                identifier,
                 StringComparison.InvariantCultureIgnoreCase
                 ));
        
        return chartType == ChartType.VFRSectional ? 
            vor.SectionalImage:
            vor.IFRChartImage;    
    }
}