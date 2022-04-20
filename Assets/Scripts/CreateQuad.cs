using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateQuad : MonoBehaviour
{
    enum Cubeside { BOTTOM, TOP, LEFT, RIGHT, FRONT, BACK }
    public Material material;

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

        void Start()
    {
        CreateCube();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateCube(){
        CreateSidesOfCube();
        CombineQuads();
    }

    void CreateSidesOfCube(){
        Quad(Cubeside.LEFT);
        Quad(Cubeside.RIGHT);
        Quad(Cubeside.BOTTOM);
        Quad(Cubeside.TOP);
        Quad(Cubeside.BACK);
        Quad(Cubeside.FRONT);
    }

    void Quad(Cubeside side){
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[4];
        Vector3[] normals = new Vector3[4];
        int[] triangles = new int[]{3,1,0, 3,2,1};
        Vector2[] uv = new Vector2[] {voxelUvs[3], voxelUvs[1], voxelUvs[0], voxelUvs[2]};

        switch (side)
        {
            case Cubeside.FRONT:
                vertices = new Vector3[]{voxelVerts[4], voxelVerts[5], voxelVerts[1], voxelVerts[0]};
                normals = new Vector3[]{Vector3.forward, Vector3.forward,Vector3.forward,Vector3.forward};
                
                break;
            case Cubeside.BOTTOM:
                vertices = new Vector3[]{voxelVerts[0], voxelVerts[1], voxelVerts[2], voxelVerts[3]};
                normals = new Vector3[]{Vector3.down, Vector3.down,Vector3.down,Vector3.down};
                break;
            case Cubeside.TOP:
                vertices = new Vector3[]{voxelVerts[7], voxelVerts[6], voxelVerts[5], voxelVerts[4]};
                normals = new Vector3[]{Vector3.up, Vector3.up,Vector3.up,Vector3.up};
                break;
            case Cubeside.LEFT:
                vertices = new Vector3[]{voxelVerts[7], voxelVerts[4], voxelVerts[0], voxelVerts[3]};
                normals = new Vector3[]{Vector3.up, Vector3.up,Vector3.up,Vector3.up};
                break;
            case Cubeside.RIGHT:
                vertices = new Vector3[]{voxelVerts[5], voxelVerts[6], voxelVerts[2], voxelVerts[1]};
                normals = new Vector3[]{Vector3.right, Vector3.right,Vector3.right,Vector3.right};
                break;
            case Cubeside.BACK:
                vertices = new Vector3[]{voxelVerts[6], voxelVerts[7], voxelVerts[3], voxelVerts[2]};
                normals = new Vector3[]{Vector3.back, Vector3.back,Vector3.back,Vector3.back};
                break;
            default:
                break; 
        }

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.triangles = triangles;
        mesh.uv = uv;

        GameObject quad = new GameObject("quad");
        quad.transform.parent = this.gameObject.transform;

        MeshFilter mf = quad.AddComponent<MeshFilter>();
        mf.mesh = mesh;
    }

    void CombineQuads(){
        //combine children meshes
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while(i<meshFilters.Length){
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            i++;
        }

        //create a new mesh of the parent object
        MeshFilter mf = this.gameObject.AddComponent<MeshFilter>();
        mf.mesh = new Mesh();

        //add combined meshes on children as the parent's mesh
        mf.mesh.CombineMeshes(combine);

        //create a renderer for the parent
        MeshRenderer renderer = this.gameObject.AddComponent<MeshRenderer>();
        renderer.material = material;

        //delete all uncombined children
        foreach (Transform quad in this.transform)
        {
            Destroy(quad.gameObject);
        }
    }

    // Start is called before the first frame update
}
