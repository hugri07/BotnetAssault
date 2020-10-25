using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientWire : MonoBehaviour
{
    public GameObject clientObj, line;
    LineRenderer lineRenderer;
    EdgeCollider2D edgeCollider2D;
    Package package;

    public enum WireColor { Red, Green, NoColor };
    public Transform severPos;
    public WireColor wireColor;
    Color thisColor;
    public int usedBy = 0;
    void Start()
    {
        lineRenderer = line.GetComponent<LineRenderer>();
        edgeCollider2D = line.GetComponent<EdgeCollider2D>();

        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, severPos.localPosition - transform.position);

        Vector2[] colliderpoints;
        colliderpoints = edgeCollider2D.points;
        colliderpoints[0] = Vector2.zero;
        colliderpoints[1] = severPos.position - transform.position;
        edgeCollider2D.points = colliderpoints;
        ChangeColor(2);
    }


    void ChangeColor(int colorInt = -1)
    {
        switch (colorInt)
        {
            case 1:
                //Debug.Log("red");
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;
                thisColor = Color.red;
                wireColor = WireColor.Red;
                break;
            case 2:
                //Debug.Log("green");
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;
                thisColor = Color.green;
                wireColor = WireColor.Green;
                break;
            case 3:
                //Debug.Log("No Color");
                lineRenderer.startColor = Color.white;
                lineRenderer.endColor = Color.white;
                thisColor = Color.white;
                wireColor = WireColor.NoColor;
                break;
        }
    }

    public Color GetColor()
    {
        return thisColor;
    }


    public void SetPackage(Package package)
    {
        this.package = package;
    }

    public void SetColor(int colorInt)
    {
        ChangeColor(colorInt);
    }

    public void StopThisClientWire()
    {
        SetColor(3);
        if (package != null)
        {
            package.KillPackage();
            package = null;
        }
    }

}
