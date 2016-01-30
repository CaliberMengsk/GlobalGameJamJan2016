using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {
    public static Globals instance;
    public static GameObject player;
    void Start()
    {
        if(Globals.instance == null)
        {
            Globals.instance = GetComponent<Globals>();
        }
        else
        {
            Destroy(gameObject);
        }
        Globals.player = GameObject.Find("Player");
    }
}
