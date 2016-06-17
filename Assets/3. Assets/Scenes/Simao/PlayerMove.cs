using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    Animator anim;
    float hor = 0f;
    float ver = 0f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");


        anim.SetFloat("Forward", ver, 0.2f, Time.deltaTime);
        anim.SetFloat("Turn", hor, 0.2f, Time.deltaTime);

    }
}
