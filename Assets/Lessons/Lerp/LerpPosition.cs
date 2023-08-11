using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpPosition : MonoBehaviour
{
    [field: SerializeField] Transform StartTransform { get; set; }
    [field: SerializeField] Transform EndTransform { get; set; }
    [Range(0f, 1f)]
    public float t;

    private void Update()
    {
        transform.position = Vector2.Lerp(StartTransform.position, EndTransform.position, t);
    }
}
