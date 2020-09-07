using UnityEngine;
using System.Collections;

// ReSharper disable once InconsistentNaming
public static class RNG {
    public static int waveNr = 0;
    public static readonly int[] GoldIncome = {350, 500, 650, 750};
    
    private static readonly int[] CreatureNr = {60, 70, 80, 90, 100};

    #region Health Modifier
    private static readonly double[] SpiderlingHealthModif = {1, 2 , 2.5, 3, 4};
    private static readonly double[] TurtleHealthModif = {1, 2, 2.5, 3, 4};
    private static readonly double[] SkeletonHealthModif = {1, 2, 2.5, 3, 4};
    private static readonly double[] BatHealthModif = {1, 2, 2.5, 3, 4};
    private static readonly double[] MageHealthModif = {1, 1.5, 2, 2.5, 3};
    private static readonly double[] OrcHealthModif = {1, 1.5, 2, 2.5, 3};
    #endregion

    #region Gold Ratio
    private static readonly double[] SpiderlingGold = {.02, .01, .005, .0025, .001};
    private static readonly double[] TurtleGold = {.02, .01, .005, .0025, .001};
    private static readonly double[] SkeletonGold = {.02, .01, .005, .0025, .001};
    private static readonly double[] BatGold = {.02, .01, .005, .0025, .001};
    private static readonly double[] MageGold = {.02, .01, .005, .0025, .001};
    private static readonly double[] OrcGold = {.02, .01, .005, .0025, .001};
    #endregion

    #region Resist Rate
    private static readonly double[][] SpiderlingResist = {
        new []{.9, .05, .05},
        new []{.85, .075, .075},
        new []{.8, .1, .1},
        new []{.75, .125, .125},
        new []{.6, .2, .2}
    };
    private static readonly double[][] TurtleResist = {
        new []{.9, .05, .05},
        new []{.85, .075, .075},
        new []{.8, .1, .1},
        new []{.75, .125, .125},
        new []{.6, .2, .2}
    };
    private static readonly double[][] SkeletonResist = {
        new []{.9, .05, .05},
        new []{.85, .075, .075},
        new []{.8, .1, .1},
        new []{.75, .125, .125},
        new []{.6, .2, .2}
    };
    private static readonly double[][] BatResist = {
        new []{.9, .05, .05},
        new []{.85, .075, .075},
        new []{.8, .1, .1},
        new []{.75, .125, .125},
        new []{.6, .2, .2}
    };
    private static readonly double[][] MageResist = {
        new []{.9, .05, .05},
        new []{.85, .075, .075},
        new []{.8, .1, .1},
        new []{.75, .125, .125},
        new []{.6, .2, .2}
    };
    private static readonly double[][] OrcResist = {
        new []{.9, .05, .05},
        new []{.85, .075, .075},
        new []{.8, .1, .1},
        new []{.75, .125, .125},
        new []{.6, .2, .2}
    };
    #endregion

    public static float GetHealthModifier(string mob) {
        switch (mob) {
            case "Spiderling":
                return (float)SpiderlingHealthModif[waveNr];
            case "Turtle":
                return (float)TurtleHealthModif[waveNr];
            case "Skeleton":
                return (float)SkeletonHealthModif[waveNr];
            case "Bat":
                return (float)BatHealthModif[waveNr];
            case "Mage":
                return (float)MageHealthModif[waveNr];
            case "Orc":
                return (float)OrcHealthModif[waveNr];
        }

        return 1;
    }
    
    public static float GoldRatio(string mob) {
        switch (mob) {
            case "Spiderling":
                return (float)SpiderlingGold[waveNr];
            case "Turtle":
                return (float)TurtleGold[waveNr];
            case "Skeleton":
                return (float)SkeletonGold[waveNr];
            case "Bat":
                return (float)BatGold[waveNr];
            case "Mage":
                return (float)MageGold[waveNr];
            case "Orc":
                return (float)OrcGold[waveNr];
        }

        return 1;
    }

    public static string GetResist(string mob) {
        double r = Random.Range(0.0f, 1.0f), sum;
        
        switch (mob) {
            case "Spiderling":
                sum = SpiderlingResist[waveNr][2];
                if (r <= sum) return "Magical";
                sum += SpiderlingResist[waveNr][1];
                if (r <= sum) return "Physical";
                return "None";
            case "Turtle":
                sum = TurtleResist[waveNr][2];
                if (r <= sum) return "Magical";
                sum += TurtleResist[waveNr][1];
                if (r <= sum) return "Physical";
                return "None";
            case "Skeleton":
                sum = SkeletonResist[waveNr][2];
                if (r <= sum) return "Magical";
                sum += SkeletonResist[waveNr][1];
                if (r <= sum) return "Physical";
                return "None";
            case "Bat":
                sum = BatResist[waveNr][2];
                if (r <= sum) return "Magical";
                sum += BatResist[waveNr][1];
                if (r <= sum) return "Physical";
                return "None";
            case "Mage":
                sum = MageResist[waveNr][2];
                if (r <= sum) return "Magical";
                sum += MageResist[waveNr][1];
                if (r <= sum) return "Physical";
                return "None";
            case "Orc":
                sum = OrcResist[waveNr][2];
                if (r <= sum) return "Magical";
                sum += OrcResist[waveNr][1];
                if (r <= sum) return "Physical";
                return "None";
        }

        return null;
    }
    
    public static ArrayList WaveCreatureList() {
        ArrayList creatureList = new ArrayList();
        int spiderlingNr = 0,
            turtleNr = 0,
            skeletonNr = 0,
            batNr = 0,
            mageNr = 0,
            orcNr = 0;
        
        /* Static creature number/type depending on wave */
        switch (waveNr) {
            case 0:
                /* 30 per Portal */
                spiderlingNr = 9;
                turtleNr = 7;
                skeletonNr = 7;
                batNr = 4;
                mageNr = 2;
                orcNr = 1;
                break;
            
            case 1:
                /* 35 per Portal */
                spiderlingNr = 10;
                turtleNr = 8;
                skeletonNr = 6;
                batNr = 5;
                mageNr = 4;
                orcNr = 2;
                break;
            
            case 2:
                /* 40 per Portal */
                spiderlingNr = 10;
                turtleNr = 8;
                skeletonNr = 7;
                batNr = 6;
                mageNr = 6;
                orcNr = 3;
                break;
            
            case 3:
                /* 45 per Portal */
                spiderlingNr = 12;
                turtleNr = 9;
                skeletonNr = 7;
                batNr = 7;
                mageNr = 6;
                orcNr = 4;
                break;
            
            case 4:
                /* 50 per Portal */
                spiderlingNr = 12;
                turtleNr = 10;
                skeletonNr = 9;
                batNr = 8;
                mageNr = 7;
                orcNr = 4;
                break;
        }
        
        for (int i = 0; i < spiderlingNr; i++)
            creatureList.Add("Spiderling");
        for (int i = 0; i < turtleNr; i++)
            creatureList.Add("Turtle");
        for (int i = 0; i < skeletonNr; i++)
            creatureList.Add("Skeleton");
        for (int i = 0; i < batNr; i++)
            creatureList.Add("Bat");
        for (int i = 0; i < mageNr; i++)
            creatureList.Add("Mage");
        for (int i = 0; i < orcNr; i++)
            creatureList.Add("Orc");

        return creatureList;
    }

    public static int WaveCreatureNr() {
        return CreatureNr[waveNr];
    }
}