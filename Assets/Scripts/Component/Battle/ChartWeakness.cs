using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartWeakness : MonoBehaviour
{
    public static double ElementChart(Elements.Element attacker, Elements.Element defender)
    {
        double[,] chartElement = new double[,] {
            {0.5, 0.5, 1.0, 1.5, 1.0, 1.0 },
            {1.5, 0.5, 0.5, 1.0, 1.0, 1.0 },
            {1.0, 1.5, 0.5, 0.5, 1.0, 1.0 },
            {0.5, 1.0, 1.5, 0.5, 1.0, 1.0 },
            {1.0, 1.0, 1.0, 1.0, 0.5, 1.5 },
            {1.0, 1.0, 1.0, 1.0, 1.5, 0.5 }
        };
        return chartElement[(int)attacker,(int)defender];
    }

    public static double ElementCheck(Elements.Element attacker, Elements.Element defender)
    {
        if (attacker == Elements.Element.Light || attacker == Elements.Element.Dark)
        {
            if (attacker == defender)
            {
                return 0.5;
            }
            else
            {
                return defender == Elements.Element.Light || defender == Elements.Element.Light ? 1.5 : 1.0;
            }
        }
        else
        {
            if (((attacker - defender) + 4) % 4 % 5 == 1)
            {
                return 1.5;
            }
            else if (((attacker - defender) + 4) % 4 % 5 == 2)
            {
                return 0.5;
            }
            else
            {
                return 1.0;
            }
        }
    }
}
