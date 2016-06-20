using UnityEngine;
using System.Collections;

public class MoveAnimatorTeste : MonoBehaviour
{

    CapsuleCollider coliderCapsule;
    Rigidbody _rigidbody;

    [SerializeField] private bool run =  false;

    float tamanhoCapsule = 1.8f;
    float tamanhoCOmPulo = 1.1471f;
    float hor = 0f;
    float ver = 0f;
    Animator anim;
    private bool jump = false;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        coliderCapsule = GetComponent<CapsuleCollider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");


        anim.SetFloat("VertSpeed", ver, 0.2f, Time.deltaTime);
        anim.SetFloat("HorSpeed", hor, 0.2f, Time.deltaTime);


        if (Input.GetKey(KeyCode.Space))
        {
            coliderCapsule.height = tamanhoCOmPulo;
            jump = true;

            anim.SetBool("Jump", jump);
            _rigidbody.AddForce(Vector3.up * .1f, ForceMode.Impulse);
        } else
        {
            coliderCapsule.height = tamanhoCapsule;
            jump = false;
            anim.SetBool("Jump", jump);
        }


        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            run = true;
            anim.SetBool("Run", run);
        }
        else
        {
            run = false;
            anim.SetBool("Run", run);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            anim.SetLayerWeight(1, 1);
            if ((Input.GetAxisRaw("Vertical") == 0f) && (Input.GetAxisRaw("Horizontal") == 0f))
            {
                anim.SetBool("rightTurn", true);
            }
        } else
        {
            anim.SetBool("rightTurn", false);
        }


        if (Input.GetKey(KeyCode.E))
        {
            anim.SetLayerWeight(1, 1);
            if ((Input.GetAxisRaw("Vertical") == 0f) && (Input.GetAxisRaw("Horizontal") == 0f))
            {
                anim.SetBool("leftTurn", true);
            }
        }
        else
        {
            anim.SetBool("leftTurn", false);
            anim.SetLayerWeight(0, 1);
        }


    }
}
