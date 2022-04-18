using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public GameObject block;
    public int size;

    // Start is called before the first frame update
    void Start(){
       StartCoroutine(BuildWorld());
    }

    // Update is called once per frame
    void Update(){
        
    }

    IEnumerator BuildWorld(){
        for(int z = 0; z < size; z++){
            for(int y = 0; y < size; y++){
                for(int x = 0; x < size; x++){
                    Vector3 pos = new Vector3(x, y, z);
                    GameObject cubo = GameObject.Instantiate(block, pos, Quaternion.identity);
                    cubo.transform.parent = this.transform;
                    cubo.name = x + " " + y + " " + z;
                }
            }
            yield return null;
        }
    }
}
