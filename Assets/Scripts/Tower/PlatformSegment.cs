using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PlatformSegment : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Collider _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        _rigidbody.isKinematic = true;
    }

    public void FlyAway(int force)
    {
        _collider.enabled = false;
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;

        _rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
    }
}
