using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelData : MonoBehaviour
{
    public static Vector3[] voxelVerts = new Vector3[8]{
        new Vector3(-.5f, -.5f, .5f), 
        new Vector3(.5f, -.5f, .5f),
        new Vector3(.5f, -.5f, -.5f),
        new Vector3(-.5f, -.5f, -.5f),
        new Vector3(-.5f, .5f, .5f),
        new Vector3(.5f, .5f, .5f),
        new Vector3(.5f, .5f, -.5f),
        new Vector3(-.5f, .5f, -.5f)
    };

    public static Vector3[] voxelUvs = new Vector3[4]{
        new Vector2(0,0),
        new Vector2(0,1),
        new Vector2(1,0),
        new Vector2(1,1)
    };
}
