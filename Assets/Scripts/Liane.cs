using UnityEngine;

public class Liane : MonoBehaviour
{
    private float _originalGravity = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //QUE TENGO QUE HACER, HACER QUE CUANDO CHOQUE LIANE TRUE, QUE ISGROUNDED Y ON COLLISION EXT LIANE FALSE
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerMovement controller = other.gameObject.GetComponent<PlayerMovement>();
            controller.Invoke("OnLiane", 0);
        }
    }
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement controller = other.gameObject.GetComponent<PlayerMovement>();
            controller.Invoke("OffLiane", 0);
        }
    }
}
