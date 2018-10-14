using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialDetection : MonoBehaviour
{
    [SerializeField]
    private float detectionRadius = 1f;

    [SerializeField]
    private LayerMask targetLayerMask;

    [SerializeField]
    private EventsManager.EventType eventType;

    private void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, detectionRadius, targetLayerMask);

        if (collider != null)
        {
            EventsManager.Instance.RouteEvent(
                this,
                new EventWithGurl(
                    eventType,
                    collider.gameObject.transform)
            );
        }
    }

    private void OnDrawGizmos()
    {
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, detectionRadius);
    }
}
