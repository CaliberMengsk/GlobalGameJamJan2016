using UnityEngine;
using System.Collections;

public class GlyphController : MonoBehaviour {

    public Transform[] gesture;
    public GameObject spellObject;
    RaycastHit[] hits;
    Ray ray;
    bool failedCast = false;
    bool hasBeenCast = false;

    int gesturePoint;

    void Start()
    {
        ResetVisuals();
    }

    void ResetVisuals()
    {
        foreach (Transform t in gesture)
        {
            t.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0f);
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && !hasBeenCast)
        {
            bool foundValidPart = false;
            if (!failedCast)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                hits = Physics.RaycastAll(ray);
                foreach (RaycastHit hit in hits)
                {
                    if (gesturePoint == gesture.Length - 1)
                    {
                        Debug.Log("Cast Spell!");
                        spellObject.SendMessage("CastSpell", SendMessageOptions.DontRequireReceiver);
                        gesture[gesturePoint-1].GetComponent<Renderer>().material.color = new Color(1, 1, 1, .25f);
                        gesture[gesturePoint].GetComponent<Renderer>().material.color = new Color(0, 0, 1, .5f);
                        hasBeenCast = true;
                    }
                    else
                    {
                        if (hit.transform == gesture[gesturePoint])
                        {
                            if (gesturePoint > 0)
                            {
                                gesture[gesturePoint - 1].GetComponent<Renderer>().material.color = new Color(1, 1, 1, .25f);
                                gesture[gesturePoint].GetComponent<Renderer>().material.color = new Color(1, 1, 1, .0f);
                            }
                            else
                            {
                                gesture[gesturePoint].GetComponent<Renderer>().material.color = new Color(1, 1, 1, .25f);
                            }
                            //Debug.Log("FoundPoint: " + gesturePoint);
                            foundValidPart = true;
                        }
                        else if (hit.transform == gesture[gesturePoint + 1])
                        {
                            //Debug.Log("Found New Point: " + gesturePoint);
                            gesturePoint += 1;
                            foundValidPart = true;
                        }
                    }
                }
            }
            else
            {
                hasBeenCast = true;
            }
            failedCast = !foundValidPart;
        }
        if (Input.GetMouseButtonUp(0))
        {
            failedCast = false;
            hasBeenCast = false;
            gesturePoint = 0;
            ResetVisuals();
        }
    }

    /*
    public int gridSize = 10;
    public bool isActive = false;
    public Texture2D gridTexture;

    public bool[,] activeSquares;

    void Start()
    {
        activeSquares = new bool[gridSize, gridSize];
    }

	// Use this for initialization
	void Update()
    {
	    if(isActive)
        {
            if(Input.GetMouseButton(0))
            {  
                activeSquares[Mathf.RoundToInt(Input.mousePosition.x / (Screen.width / (gridSize - 1))), Mathf.RoundToInt((Screen.height - Input.mousePosition.y) / (Screen.height / (gridSize - 1)))] = true;
            }else if(Input.GetMouseButtonUp(0))
            {
                activeSquares = new bool[gridSize, gridSize];
            }
        }
	}
	
	// Update is called once per frame
	void OnGUI()
    {
	    for(int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                if(activeSquares[x,y])
                {
                    //GUI.DrawTexture(new Rect((Screen.width / gridSize) * x, (Screen.height / gridSize) * y, Screen.width/gridSize, Screen.height/gridSize), gridTexture);
                }
            }
        }
	}*/
}
