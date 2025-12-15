using System;
using IFRHoldClearanceTrainer.models;

namespace IFRHoldClearanceTrainer.services;

public interface IDirectionRules
{
    public Direction GenerateLogicalDirection(int radial);
}

public class BasicDirectionRulesEngine : IDirectionRules
{
    private IRandom random;

    public BasicDirectionRulesEngine(IRandom random)
    {
        this.random = random;
    }
    public Direction GenerateLogicalDirection(int radial)
    {
        switch (radial)
        {
            case  180 or 360:
                return CoinFlip(Direction.North, Direction.South);
            case > 0 and < 90:
                return CoinFlip(Direction.NorthEast, Direction.SouthWest);
            case 90 or 270:
                return CoinFlip(Direction.East, Direction.West);
            case > 90 and < 180:
                return CoinFlip(Direction.NorthWest, Direction.SouthEast);
            case > 180 and < 270:
                return CoinFlip(Direction.NorthEast, Direction.SouthWest);
            case > 270 and < 360:
                return CoinFlip(Direction.NorthWest, Direction.SouthEast);
            default:
                throw new InvalidDataException($"Invalid radial or direction received. Radial:{radial}");
   
        }
    }
    private Direction CoinFlip(Direction direction1, Direction direction2)
    {
        return random.Next(0,10) % 2 == 0 ? direction1: direction2;
    }
}

