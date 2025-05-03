using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteSorter : MonoBehaviour
{
    private bool isStatic = false;
    public float offset = 0;
    private int SortingOrderBase = 0;
    private Renderer renderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        renderer.sortingOrder = (int)(SortingOrderBase - transform.position.y + offset);
        
    }
}
