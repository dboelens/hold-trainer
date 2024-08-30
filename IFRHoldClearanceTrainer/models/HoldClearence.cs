namespace IFRHoldClearanceTrainer.models;
public class HoldClearence
{
    public required Direction Direcion {get; set;}
    public required Fix Fix {get; set;}
    public HoldType HoldType {get; set;}
    public int HoldTypeUnit {get; set;}
    public HoldDirection HoldDirection {get; set;}
    public TimeSpan EFCTime {get; set;}

    public string DisplayClearence()
    {
        string holdLocation = 
        "Hold " + this.Direcion.ToString() +
        Fix.DisplayFix() + Environment.NewLine;

        string holdType = "";
        if (this.HoldType != HoldType.Time){
            holdType = HoldTypeUnit + " mile legs\n";
        }
        else if(this.HoldTypeUnit > 1){
            holdType = HoldTypeUnit + " minute legs\n";
        }

        string holdDirection = "";
        if(this.HoldDirection != HoldDirection.Right){
            holdDirection = "Left turns \n";
        }
        
        string efc = "Expect further clearence at " + EFCTime.ToString("hh\\:mm") + " UTC";

        return holdLocation + holdType + holdDirection + efc;
        

    }
}