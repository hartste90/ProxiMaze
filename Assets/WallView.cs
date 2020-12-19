using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallView : MonoBehaviour
{
    public List<Material> materialsList;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<MeshRenderer>().material = materialsList[Random.Range(0, materialsList.Count)];
    }
}
