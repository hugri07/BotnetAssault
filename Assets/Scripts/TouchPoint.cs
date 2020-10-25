using UnityEngine;

public class TouchPoint : MonoBehaviour
{
    public static TouchPoint Instance;

    void Start()
    {
        Instance = this;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Touch" && other.tag != "Untagged")
        {
            // Debug.Log(other.transform.parent + " This is WireTap" + other.gameObject.name + "   " + other.tag);
            GameManager.Instance.StopClientWire(other.transform.parent);
        }
    }
}
