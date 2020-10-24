using UnityEngine;

public class TouchPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Touch" && other.tag != "Untagged")
        {
            Debug.Log(other.transform.parent + " This is WireTap" + other.gameObject.name + "   " + other.tag);
        }
    }
}
