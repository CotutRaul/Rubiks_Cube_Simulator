using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CubeCode : MonoBehaviour
{
    public GameObject CubePiecePref = null;
    Transform CubeTransf = null;
    List<GameObject> AllCubePieces = new List<GameObject>();
    GameObject CubePiecesSide;

    public GameObject ScrambleTxt = null;
    string scramble;

    List<GameObject> UpPieces
    {
        get
        {
            return AllCubePieces.FindAll(x=> Mathf.Round(x.transform.localPosition.y) == 2);
        }
    }
    List<GameObject> DownPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.y) == 0);
        }
    }
    List<GameObject> FrontPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.x) == 2);
        }
    }
    List<GameObject> BackPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.x) == 0);
        }
    }
    List<GameObject> RightPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.z) == 2);
        }
    }
    List<GameObject> LeftPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.z) == 0);
        }
    }

    List<GameObject> EdgeShuffle = new List<GameObject>();

    Vector3[] RotationVector =
    {
        new Vector3(0, 1, 0),   new Vector3(0, -1, 0),
        new Vector3(1, 0, 0),   new Vector3(-1, 0, 0),
        new Vector3(0, 0, 1),   new Vector3(0, 0, -1),

        new Vector3(0, -1, 0),   new Vector3(0, 1, 0),
        new Vector3(-1, 0, 0),   new Vector3(1, 0, 0),
        new Vector3(0, 0, -1),   new Vector3(0, 0, 1)
    };


    bool CanRotate = true;
    bool CanSuffle = true;

    void Start()
    {
        CreateCube();
    }


    void Update()
    {
        if(CanRotate)
            CeckInput();
    }

    void CreateCube()
    {
        scramble = "";
        ScrambleTxt.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        foreach (GameObject go in AllCubePieces)
            DestroyImmediate(go);
        AllCubePieces.Clear();

        for (int x = 0; x < 3; x++)
            for (int y = 0; y < 3; y++)
                for (int z = 0; z < 3; z++)
                {
                    
                    GameObject go = Instantiate(CubePiecePref, CubeTransf, false);
                    go.transform.localPosition = new Vector3(x, y, z);

                    go.GetComponent<CubePlanes>().SetPlane(x, y, z);
                    AllCubePieces.Add(go);
                }
        CubePiecesSide = AllCubePieces[13];

    }

    public int speed = 10;
    bool isShift = false;
    
    void CeckInput()
    {
        if (Input.GetKeyDown(KeyCode.N) && CanSuffle)
            CreateCube();
        if (Input.GetKeyDown(KeyCode.S) && CanSuffle)
            StartCoroutine(Shuffle());

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            isShift = true;
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            isShift = false;

        if(isShift == false && CanRotate == true)
        {
            if (Input.GetKeyDown(KeyCode.Y))
                StartCoroutine(Rotate(UpPieces, new Vector3(0, 1, 0), speed));
            else
            if (Input.GetKeyDown(KeyCode.W))
                StartCoroutine(Rotate(DownPieces, new Vector3(0, -1, 0), speed));    
            else
            if (Input.GetKeyDown(KeyCode.B))
                StartCoroutine(Rotate(FrontPieces, new Vector3(1, 0, 0), speed));    
            else
            if (Input.GetKeyDown(KeyCode.G))
                StartCoroutine(Rotate(BackPieces, new Vector3(-1, 0, 0), speed));    
            else
            if (Input.GetKeyDown(KeyCode.R))
                StartCoroutine(Rotate(RightPieces, new Vector3(0, 0, 1), speed));    
            else
            if (Input.GetKeyDown(KeyCode.O))
                StartCoroutine(Rotate(LeftPieces, new Vector3(0, 0, -1), speed));
        }

        if(isShift == true && CanRotate == true)
        {
            if (Input.GetKeyDown(KeyCode.Y))
                StartCoroutine(Rotate(UpPieces, new Vector3(0, -1, 0), speed));
            else
            if (Input.GetKeyDown(KeyCode.W))
                StartCoroutine(Rotate(DownPieces, new Vector3(0, 1, 0), speed));    
            else
            if (Input.GetKeyDown(KeyCode.B))
                StartCoroutine(Rotate(FrontPieces, new Vector3(-1, 0, 0), speed));    
            else
            if (Input.GetKeyDown(KeyCode.G))
                StartCoroutine(Rotate(BackPieces, new Vector3(1, 0, 0), speed));    
            else
            if (Input.GetKeyDown(KeyCode.R))
                StartCoroutine(Rotate(RightPieces, new Vector3(0, 0, -1), speed));    
            else
            if (Input.GetKeyDown(KeyCode.O))
                StartCoroutine(Rotate(LeftPieces, new Vector3(0, 0, 1), speed));
                
        }
        
         
    }

    IEnumerator Rotate(List<GameObject> pieces, Vector3 rotationvec, int speed)
    {
        CanRotate = false;
        int angle = 0;
        while(angle<90)
        {
            foreach (GameObject go in pieces)
                go.transform.RotateAround(CubePiecesSide.transform.position, rotationvec, speed);
            yield return null;
            angle += speed;
        }
        CanRotate = true;
    }
    
    IEnumerator Shuffle()
    {
        CanSuffle = false;
        scramble = "";
        ScrambleTxt.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        for (int moveCount = Random.Range(20,30); moveCount>0; moveCount--)
        {
            int edge = Random.Range(0, 11);
            switch(edge)
            {
                case 0: EdgeShuffle = UpPieces; scramble += "U "; break;
                case 1: EdgeShuffle = DownPieces; scramble += "D "; break;
                case 2: EdgeShuffle = FrontPieces; scramble += "F "; break;
                case 3: EdgeShuffle = BackPieces; scramble += "B "; break;
                case 4: EdgeShuffle = RightPieces; scramble += "R "; break;
                case 5: EdgeShuffle = LeftPieces; scramble += "L "; break;

                case 6: EdgeShuffle = UpPieces; scramble += "U' "; break;
                case 7: EdgeShuffle = DownPieces; scramble += "D' "; break;
                case 8: EdgeShuffle = FrontPieces; scramble += "F' "; break;
                case 9: EdgeShuffle = BackPieces; scramble += "B' "; break;
                case 10: EdgeShuffle = RightPieces; scramble += "R' "; break;
                case 11: EdgeShuffle = LeftPieces; scramble += "L' "; break;
            }
            StartCoroutine(Rotate(EdgeShuffle, RotationVector[edge], 10));
            yield return new WaitForSeconds(.2f);
        }
        ScrambleTxt.GetComponent<TMPro.TextMeshProUGUI>().text = scramble;
        CanSuffle = true;
    }
}
