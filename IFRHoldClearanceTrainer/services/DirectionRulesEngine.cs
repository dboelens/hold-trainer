using System;
using IFRHoldClearanceTrainer.models;

namespace IFRHoldClearanceTrainer.services;

public interface IDirectionRules
{
    public Direction GenerateLogicalDirection(int radial, HoldDirection direction);
}

public class BasicDirectionRulesEngine : IDirectionRules
{
    public Direction GenerateLogicalDirection(int radial, HoldDirection direction)
    {
        if(radial >= 0 && radial < 45) return Direction.North;
        else if(radial == 45 && direction == HoldDirection.Right) return Direction.North;
        else if(radial == 45 && direction == HoldDirection.Left) return Direction.East;
        else if(radial > 45 && radial < 135) return Direction.East;
        else if(radial == 135 && direction == HoldDirection.Right) return Direction.East;
        else if(radial == 135 && direction == HoldDirection.Left) return Direction.South;
        else if(radial > 135 && radial < 215) return Direction.South;
        else if(radial == 215 && direction == HoldDirection.Right) return Direction.South;
        else if(radial == 215 && direction == HoldDirection.Left) return Direction.West;
        else if (radial > 215 && radial < 315) return Direction.West;
        else if (radial == 315 && direction == HoldDirection.Right) return Direction.West;
        else if (radial == 315 && direction == HoldDirection.Left) return Direction.North;
        else if (radial > 315 && radial <= 360) return Direction.North;
        else
        {
            throw new InvalidDataException($"Invalid radial or direction received. Radial:{radial} | Direction:{direction}");
        }
    }
}
