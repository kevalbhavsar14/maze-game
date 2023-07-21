using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] AudioSource impactSource;
    [SerializeField] AudioSource rollSource;
    
    Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.WakeUp();
        if (transform.position.y < -10 && GameManager.Instance.GameState == GameState.Running)
        {
            GameManager.Instance.GameState = GameState.Lost;
        }
        if (GameManager.Instance.GameState != GameState.Running)
        {
            rollSource.Pause();
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Vector3 contactPoint = collision.contacts[0].point;
    //    float angle = Vector3.Angle(contactPoint - transform.position, rb.velocity);
    //    float volume = angle.Limit(0, 90).Map(0, 90, 1, 0);
    //    impactSource.volume = volume * rb.velocity.magnitude.Limit(0, 1);
    //    impactSource.Play();
    //}

    private void OnCollisionStay(Collision collision)
    {
        if (!rollSource.isPlaying)
        {
            rollSource.Play();
        }
        rollSource.volume = rb.velocity.magnitude * 0.7f;
        rollSource.pitch = rb.velocity.magnitude * 0.2f + 1;
    }

    private void OnCollisionExit(Collision collision)
    {        
        rollSource.Pause();
    }
}
