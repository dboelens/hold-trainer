
using IFRHoldClearanceTrainer.models;

public class Fix{
    public required string FixIdentifier {get; set;}
    public int Radial {get; set;}
    public int DistanceUnits {get; set;}

    public string DisplayFix()
    {
        string fixString = " of the " + FixIdentifier + " VOR";
        if(DistanceUnits >0){
            fixString = fixString + " " + Radial + " radial " + DistanceUnits + " DME";
        }

        return fixString;
    }
}