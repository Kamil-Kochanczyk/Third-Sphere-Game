using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    private PlayerController _playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerController.OnCollectPickUp += PlayPickUpSound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayPickUpSound(object sender, OnCollectPickUpArgs onCollectPickUpArgs)
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    private void OnDestroy()
    {
        _playerController.OnCollectPickUp -= PlayPickUpSound;
    }
}
