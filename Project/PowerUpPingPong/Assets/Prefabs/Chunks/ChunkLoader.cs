using UnityEngine;
using UnityEngine.SceneManagement;

public enum CheckType
{
    distance,
    trigger
}

public class ChunkLoader : MonoBehaviour
{
    [SerializeField] private bool _isLoaded, _shouldLoad;
    [SerializeField] private Transform _player;
    [SerializeField] private float _minDistance;
    [SerializeField] private CheckType _loadMethod;
    private void Update()
    {
        if (_player)
        {
            Debug.DrawLine(transform.position, _player.position, Color.magenta);

            if (_loadMethod == CheckType.distance)
            {
                DistanceCheck();
            }

            else if (_loadMethod == CheckType.trigger)
            {
                TriggerCheck();
            }
        }
    }

    private void TriggerCheck()
    {

    }

    private void DistanceCheck()
    {
        if (Vector3.Distance(_player.position, transform.position) < _minDistance)
        {
            LoadScene();
        }

        else
        {
            UnloadScene();
        }
    }
    private void LoadScene()
    {
        if (!_isLoaded)
        {
            SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            _isLoaded = true;
        }
    }

    private void UnloadScene()
    {
        if (_isLoaded)
        {
            SceneManager.UnloadSceneAsync(gameObject.name);
            _isLoaded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(_minDistance, _minDistance * 0.5f, _minDistance));
    }
}
