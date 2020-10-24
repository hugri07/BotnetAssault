using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientWire : MonoBehaviour
{
    public GameObject clientObj, line;
    LineRenderer lineRenderer;
    EdgeCollider2D edgeCollider2D;

    public Transform severPos;
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
    }

}
