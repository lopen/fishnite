using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr; // Line render component
    private Transform[] points; // Transform array, used for storing points for drawing line
    
    // On wake, get line component
    private void Awake() {
        lr = GetComponent<LineRenderer>();
    }

    // Create line between transform points
    public void SetUpLine(Transform[] points) {
        lr.positionCount = points.Length;
        this.points = points;
    }


    // Update line points every frame
    private void Update() {
        for (int i = 0; i < points.Length; i++) {
            lr.SetPosition(i, points[i].position);
        }
    }
}
