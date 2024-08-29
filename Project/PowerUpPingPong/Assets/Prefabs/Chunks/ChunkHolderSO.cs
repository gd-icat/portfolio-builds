using UnityEditor;
using UnityEngine;

public enum CheckType
{
    distance,
    trigger
}

[CreateAssetMenu]
public class ChunkHolderSO : ScriptableObject
{
    public GameObject Chunk;
    public string NextSceneName;
    public CheckType CheckMethod = CheckType.distance;
    public bool Loaded, useChunk;
}
