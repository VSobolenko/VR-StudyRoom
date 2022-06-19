using UnityEngine;

public class LiveButton : MonoBehaviour
{
    public  PhysicsInformation _physicsInformation;
    public  PhysicsObject _physicsObject;

    private void Start()
    {
        StopSimulation();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<OVRGrabber>(out var test))
        {
            StartSimulation();
        }
        else if (other.gameObject.TryGetComponent<CharacterController>(out var test2))
        {
            StartSimulation();
        }
        else if (other.gameObject.TryGetComponent<OVRHand>(out var test3))
        {
            StartSimulation();
        }
        else if (other.gameObject.TryGetComponent<FirstPersonMovement>(out var test4))
        {
            StartSimulation();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<OVRGrabber>(out var test))
        {
            StartSimulation();
        }
        else if (other.gameObject.TryGetComponent<CharacterController>(out var test2))
        {
            StartSimulation();
        }
        else if (other.gameObject.TryGetComponent<OVRHand>(out var test3))
        {
            StartSimulation();
        }
        else if (other.gameObject.TryGetComponent<FirstPersonMovement>(out var test4))
        {
            StartSimulation();
        }
    }

    public void StartSimulation()
    {
        if (_physicsInformation != null)
        {
            _physicsInformation.StartSimulation();
        }

        if (_physicsObject != null)
        {
            _physicsObject.StartSimulation();
        }
    }

    public void StopSimulation()
    {
        if (_physicsObject != null)
        {
            _physicsObject.StopSimulation();
        }
    }
}
