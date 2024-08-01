using System.Collections.Generic;
public static class Data_Exp1
{
    public static int entriesCounter;
    public static List<int> entriesList = new List<int> { };

    public static List<(int, int)> dataPositionOrder = new List<(int, int)> { };

    public static List<(float, float)> dataHeadVsTranslationAngle = new List<(float, float)> { };
    public static List<(float, float)> dataEyeVsTranslationAngle = new List<(float, float)> { };
    public static List<(float, float)> dataEyeVsHeadAngle = new List<(float, float)> { };
}
