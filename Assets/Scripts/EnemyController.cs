using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private PlayerController _playerController;

    [SerializeField]
    private NavMeshAgent _agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerController.OnAllPickUpsCollected += DeactivateAgent;
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(_playerController.transform.position);
    }

    private void DeactivateAgent()
    {
        _agent.gameObject.SetActive(false);
    }

    private void OnDeactivate()
    {
        _playerController.OnAllPickUpsCollected -= DeactivateAgent;
    }
}
