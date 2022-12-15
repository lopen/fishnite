using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTest : MonoBehaviour
{
    [SerializeField] private Transform[] points; // Array of transform points
    [SerializeField] private LineController line; // Line to be drawn

    // Start is called before the first frame update / Set up initial line from specified transform points
    private void Start() {
        line.SetUpLine(points);
    }
}
