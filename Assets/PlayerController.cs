using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPun
{
    public float speed = 5f;
    public CharacterController controller;

    private void Start()
    {
        GetComponent<Renderer>().material.color =
            new Color(Random.value, Random.value, Random.value);
    }
    void Update()
    {
        if (!photonView.IsMine) return;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }
}
/*
using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPun
{
    public float speed = 5f;
    public CharacterController controller;

    void Update()
    {
        if (!photonView.IsMine) return;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }
}
*/