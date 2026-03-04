using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering.Universal.Internal;

public class MovementDirection : MonoBehaviour {

    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    Animator animator;

    float sideways;
    float forward;

    public void Update()
    {
        forward = Vector3.Dot(gameObject.transform.forward.normalized, rb.linearVelocity.normalized);
        sideways = Vector3.Dot(gameObject.transform.right.normalized, rb.linearVelocity.normalized);

        if(forward < 0.3 && forward > -0.3) { forward = 0; }
        if (sideways < 0.3 && sideways > -0.3) { sideways = 0; }
        animator.SetFloat("Horizontal", sideways);
        animator.SetFloat("Vertical", forward);
    }

}
