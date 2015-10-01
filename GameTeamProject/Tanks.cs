using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTeamProject
{
    public enum TankType
    {
        Yellow,
        White,
        Green,
        Purple
    }

    public enum TankFrames
    { 
        Up = 0,
        Left = 2,
        Down = 4,
        Right = 6
    }
    static class Tanks
    {
        private static Dictionary<string, int[]> type = new Dictionary<string, int[]>()
        {
            {"Yellow", new int[]{0, 0}},
            {"White", new int[]{8, 0}},
            {"Green", new int[]{0, 8}},
            {"Purple", new int[]{8, 8}}
        };

        public static int[] GetType(TankType enumType)
        {
            switch (enumType)
            {
                case TankType.Yellow:
                    return type["Yellow"];
                case TankType.White:
                    return type["White"];
                case TankType.Green:
                    return type["Green"];
                case TankType.Purple:
                    return type["Purple"];
                default:
                    break;
            }

            return null;
        }
    }
}
