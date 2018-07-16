using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barriers : MonoBehaviour
{

    private int[] barrierArray;

    // Use this for initialization
    void Start()
    {
        barrierArray = new int[32 * 32];

        // create array
        /* 
         * bit 1: north
         * bit 2: east
         * bit 3: south
         * bit 4: west 
         */

        // Use x + 32 * y to access spot;

        BarrierLine(0, 0, 0, 0, Direction.North);
        Debug.Log(barrierArray[0]);
        Debug.Log(GetBarrier(0, 0, Direction.North));
    }

    public void BarrierLine(int x1, int y1, int x2, int y2, Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                for (int i = 0; i <= (x2 - x1); i++)
                    for (int j = 0; j <= (y2 - y1); j++)
                        barrierArray[x1 + i + 32 * (y1 + j)] = barrierArray[x1 + i + 32 * (y1 + j)] | 1;
                break;
            case Direction.East:
                for (int i = 0; i <= (x2 - x1); i++)
                    for (int j = 0; j <= (y2 - y1); j++)
                        barrierArray[x1 + i + 32 * (y1 + j)] = barrierArray[x1 + i + 32 * (y1 + j)] | (1 << 1);
                break;
            case Direction.South:
                for (int i = 0; i <= (x2 - x1); i++)
                    for (int j = 0; j <= (y2 - y1); j++)
                        barrierArray[x1 + i + 32 * (y1 + j)] = barrierArray[x1 + i + 32 * (y1 + j)] | (1 << 2);
                break;
            case Direction.West:
                for (int i = 0; i <= (x2 - x1); i++)
                    for (int j = 0; j <= (y2 - y1); j++)
                        barrierArray[x1 + i + 32 * (y1 + j)] = barrierArray[x1 + i + 32 * (y1 + j)] | (1 << 3);
                break;
        }
    }

    public bool GetBarrier(int x, int y, Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                return (barrierArray[x + 32 * y] & 1) > 0;
            case Direction.East:
                return (barrierArray[x + 32 * y] & (1 << 1)) > 0;
            case Direction.South:
                return (barrierArray[x + 32 * y] & (1 << 2)) > 0;
            case Direction.West:
                return (barrierArray[x + 32 * y] & (1 << 3)) > 0;
        }
        return true;
    }
}
