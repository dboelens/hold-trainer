
using IFRHoldClearanceTrainer.models;

public class Fix{
    public required string FixIdentifier {get; set;}
    public int Radial {get; set;}
    public int DistanceUnits {get; set;}

    public string DisplayFix()
    {
        string radial = $"0{Radial}";
        string radialString = radial[^3..];
        string fixString = " of the " + FixIdentifier + " VOR" ;
        if(DistanceUnits >0){
            fixString = fixString +  " "+ radialString + " radial " + DistanceUnits + " DME";
        }
        fixString = fixString + " on the " + radialString + " radial"; 

        return fixString;
    }
}