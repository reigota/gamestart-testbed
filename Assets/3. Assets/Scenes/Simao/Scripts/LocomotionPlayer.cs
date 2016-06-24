using UnityEngine;
using System.Collections;

public class LocomotionPlayer : MonoBehaviour
{
    #region Variables (private)
    [SerializeField] private Animator anim;
    [SerializeField] private float directionDampTime = .25f;

    private float speed = 0.0f;
    private float horizontal = 0.0f;
    private float vertical = 0.0f;
    #endregion

    #region Properties (public)
    #endregion

    #region Unity event functions

    private void Awake()
    {
        anim = GetComponent<Animator>();

        if (anim.layerCount >= 2)
        {
            anim.SetLayerWeight(1, 1);
        }
    }

    void Start () {
	
	}

	void Update ()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        speed = new Vector3(horizontal, vertical).sqrMagnitude;

        anim.SetFloat("Speed", speed);
        anim.SetFloat("Direction", horizontal, directionDampTime, Time.deltaTime);
	}
    #endregion

    #region Methods

    #endregion Methods
}
