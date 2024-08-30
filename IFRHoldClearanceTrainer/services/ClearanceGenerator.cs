namespace IFRHoldClearanceTrainer.services;

using IFRHoldClearanceTrainer.models;

public interface IClearenceGenerator
{
    public HoldClearence Generate();
    public string GetFixResource(string identifier, ChartType chartType);
}

public class ClearenceGenerater: IClearenceGenerator
{
    private IRandom random;
    private IList<VOR> vorList;
    private const int RADIALMIN = 1;
    private const int RADIALMAX = 360;
    private const int MAXDISTANCE = 10;
    private const int MAXLEG = 10;
    private const int MAXIMUMCLEARENCEDURATION = 60;

    public ClearenceGenerater(IRandom randomizer, IList<VOR> vorList, int radialMin = -1, int radialMax = -1, int maxDistance = -1, int maxLeg = -1, int maximumClearenceDuration = -1)
    {
        random = randomizer;
        this.vorList = vorList;
    }

    public HoldClearence Generate()
    {
        var direction = (Direction)random.Next(
            Enum.GetValues(typeof(Direction)).Cast<int>().Max()
        );
        
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

        // Step 1: Determine Fix
        var fix = GenerateFix();

        //Step 2: Determine distanceFromFix


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
        var radial = (double) random.Next(RADIALMIN, RADIALMAX);
        return new Fix
        {
            FixIdentifier = GetRandomFixIdentifier(),
            Radial =  (int)(Math.Round(radial/5.0) * 5.0),
            DistanceUnits = random.Next(MAXDISTANCE)
        };
    }

    public string GetRandomFixIdentifier()
    {

        return vorList.Count > 1 ? 
            vorList.ToArray()[random.Next(1, vorList.Count)-1].Identifier 
            : vorList.First().Identifier;
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