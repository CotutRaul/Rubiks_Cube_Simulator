using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlanes : MonoBehaviour
{
    public GameObject UpPlane, DownPlane, FrontPlane, BackPlane, RightPlane, LeftPlane;

    public void SetPlane(int x, int y, int z)  //pentru colturi
    {
        if (y == 2)
            DownPlane.SetActive(false);
        else
            if (y == 0)
            UpPlane.SetActive(false);

        if (z == 0)
            RightPlane.SetActive(false);
        else
            if (z == 2)
            LeftPlane.SetActive(false);

        if (x == 0)
            FrontPlane.SetActive(false);
        else
            if (x == 2)
            BackPlane.SetActive(false);

        if(y == 1)
        {
            UpPlane.SetActive(false);
            DownPlane.SetActive(false);
        }
        if(z == 1)
        {
            RightPlane.SetActive(false);
            LeftPlane.SetActive(false);
        }
        if(x == 1)
        {
            FrontPlane.SetActive(false);
            BackPlane.SetActive(false);
        }
            
    }
}
