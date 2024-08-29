using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChunkLoader : MonoBehaviour
{
    [SerializeField] private ChunkHolderSO _holder;
    [SerializeField] private Transform _player;
    private void FixedUpdate()
    {
        if (_holder && _holder.Chunk)
        {
            if (_holder.CheckMethod == CheckType.distance)
            {
                if (_holder.useChunk)
                {
                    if (!_holder.Loaded)
                    {
                        Instantiate(_holder.Chunk);
                        _holder.Loaded = true;
                    }
                }

                else
                {
                    if (Vector3.Distance(transform.position, _player.position) < 3.0f)
                    {
                        if (!_holder.Loaded)
                        {
                            SceneManager.LoadSceneAsync(_holder.NextSceneName);
                            _holder.Loaded = true;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
