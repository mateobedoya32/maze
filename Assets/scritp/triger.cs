using UnityEngine;

public class trigger : MonoBehaviour
{
    [SerializeField]
    private GameObject txtmessage;

    private void OnTriggerEnter(Collider other)
    
    {
        if (other.CompareTag("Player"))
        {

            txtmessage.SetActive(true);

        }
    }
}
