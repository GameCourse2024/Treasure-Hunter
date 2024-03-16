using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PSystemAdd : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem pSystemPrefab;
    [Tooltip("Enter the tree offset to balance the particles")]
    [SerializeField]
    private float offSet = 0;

    void Start()
    {
        Terrain terrain = Terrain.activeTerrain;
        TreeInstance[] trees = terrain.terrainData.treeInstances;

        foreach (TreeInstance tree in trees)
        {
            UnityEngine.Vector3 treePosition = UnityEngine.Vector3.Scale(tree.position, terrain.terrainData.size) + terrain.transform.position;
            float treeHeight = tree.position.y* terrain.terrainData.size.y;
            treePosition.y += treeHeight + offSet;
            Instantiate(pSystemPrefab, treePosition, UnityEngine.Quaternion.identity, transform);
        }
    }
}
