
using UnityEngine;
using UnityEditor;

/*
Unity uses a left-handed and Y-up world coordinate systems. If the imported mesh has a different coordinate system, 
it will conflict with the Unity world coordinate system. This script switches automatically the Y and Z axes of the imported FBX files. 

Based on https://gist.github.com/FlaShG/a82ae94d5789e92f2bec by Sascha Graeff (2015)
*/

public class FBXAxesFixer : AssetPostprocessor
{
    void OnPostprocessModel(GameObject g)
    {
        if(assetImporter.ToString() == " (UnityEngine.FBXImporter)")
        {
            FixFBXImport(g.transform, 0);
        }
    }

    private static void FixFBXImport(Transform t, int depth)
    {
        var filter = t.GetComponent<MeshFilter>();
        bool hasMesh = filter;

        if(!hasMesh)
            return;

        var mesh = filter.sharedMesh;

        var vertices = mesh.vertices;
        var normals = mesh.normals;

        //Switch y and z axis
        var newVertices = new Vector3[mesh.vertexCount];
        var newNormals = new Vector3[mesh.vertexCount];
        for(int i = 0; i < mesh.vertexCount; ++i)
        {
            var v = vertices[i];
            var n = normals[i];

            newVertices[i] = new Vector3(v.x, v.z, v.y);
            newNormals[i] = new Vector3(n.x, n.z, n.y);
        }
        mesh.vertices = newVertices;
        mesh.normals = newNormals;

        //Fix flipped normals
        var tris = mesh.triangles;
        for(int i = 0; i < tris.Length; i += 3)
        {
            var buffer = tris[i];
            tris[i] = tris[i + 2];
            tris[i + 2] = buffer;
        }
        mesh.triangles = tris;

        //Recalculate the bounding volume of the Mesh from the vertices
        mesh.RecalculateBounds();
    }
}
