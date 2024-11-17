using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    private Vector3 _offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // The initial difference between camera position and player position at the beginning of the frame
        _offset = transform.position - _player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        // Player position might have changed but we want to keep it always the same
        transform.position = _player.transform.position + _offset;
    }
}
