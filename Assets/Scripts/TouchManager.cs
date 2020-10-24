using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject touchPrefab;
    GameObject tempObj;


    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Vector2 tempPos = Camera.main.ScreenToWorldPoint(pointerEventData.position);
        tempObj = Instantiate(touchPrefab, tempPos, Quaternion.identity);
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (tempObj != null)
            Destroy(tempObj.gameObject);
    }
}