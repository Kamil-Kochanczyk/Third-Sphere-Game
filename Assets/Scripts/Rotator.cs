using UnityEngine;

/*
 * Apparently, if two colliders collide and at least one of them is a trigger, you can use OnTrigger... methods on either one of them,
 * i.e. you can call them on the trigger itself or you can call them on the collider that collided with the trigger.
 * 
 * Static colliders - they don't move so they don't need to be rigid bodies.
 * Dynamic colliders - they move so they should be rigid bodies, otherwise there can be performance issues.
 * 
 * What if you need some sort of a mix between a static collider and a dynamic collider?
 * Example would be a moving platform - it collides, it moves (dynamic collider) but shouldn't be affected by forces (static collider).
 * In this situation you can disable gravity in the rigid body component.
 * But this will only take care of gravity, what about other forces?
 * You can enable kinematic rigid body.
 * This will prevent registering any physics forces but collisions will still be registered.
 */

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _pickUpParticles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag.Equals("Player"))
        //{
        //    gameObject.SetActive(false);
        //}

        if (other.gameObject.layer == 3)
        {
            ParticleSystem effect = Instantiate(_pickUpParticles, transform.position + (1.13f * Vector3.up), Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constant);
            gameObject.SetActive(false);
        }
    }
}
