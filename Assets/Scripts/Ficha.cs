using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ficha : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {
    Rigidbody2D rb;
    public LineRenderer lr;

    Vector2 startPoint;
    Vector2 endPoint;

    float minForceApplied;
    float maxForceApplied;
    float minDragSize;
    float maxDragSize;

    // Start is called before the first frame update
    void Start() {
        minForceApplied = 200f;
        maxForceApplied = 2000f;

        minDragSize = 100f;
        maxDragSize = 375f;
        rb = GetComponent<Rigidbody2D>();
    }


    public void OnPointerDown(PointerEventData eventData) {
    }


    public void OnBeginDrag(PointerEventData data) {
        startPoint = data.position;

    }

    public void OnDrag(PointerEventData data) {
        Vector2 currentPoint = data.position;
        Vector2 currentDragEnd = currentPoint - startPoint;

        //NORMALIZED VECTOR (DIRECTION OF DRAG LINE)
        Vector2 directionOfLine = (endPoint - startPoint).normalized;

        calculateDragDistance(startPoint, endPoint, directionOfLine);

    }

    public void OnEndDrag(PointerEventData data) {
        //ENDING POSITION
        endPoint = data.position;

        // CLEAR THE LINE
        lr.SetPosition(0, new Vector3(0f, 0f, 0f));

        //NORMALIZED VECTOR (DIRECTION OF FORCE)
        Vector2 directionOfDrag = -(endPoint - startPoint).normalized;

        //HOW LONG THE DRAG IS.
        calculateDragDistance(startPoint, endPoint, directionOfDrag);
        
    }

    public void calculateDragDistance(Vector2 start, Vector2 end, Vector2 direction) {
        float sizeOfDrag = Mathf.Sqrt(Mathf.Pow((end.x - start.x), 2F) + Mathf.Pow((end.y - start.y), 2F));

        if (sizeOfDrag >= maxDragSize) {
            rb.AddForce(direction * maxForceApplied);
        } else if (sizeOfDrag == minDragSize) {
            rb.AddForce(direction * minForceApplied);
        } else if (sizeOfDrag > minDragSize || sizeOfDrag < maxDragSize) {
            float calculatedForceApplied = (float)(sizeOfDrag * maxForceApplied) / maxDragSize;
            rb.AddForce(direction * calculatedForceApplied);
        }
    }

    public void calculateDragLine(Vector2 start, Vector2 end, Vector2 direction) {
        float sizeOfLine = Mathf.Sqrt(Mathf.Pow((end.x - start.x), 2F) + Mathf.Pow((end.y - start.y), 2F));

        if (sizeOfLine >= maxDragSize) {
            lr.SetPosition(0, new Vector3(0f, 0f, 0f));
        } else if (sizeOfLine == minDragSize) {
            rb.AddForce(direction * minForceApplied);
        } else if (sizeOfLine > minDragSize || sizeOfLine < maxDragSize) {
            float calculatedForceApplied = (float)(sizeOfLine * maxForceApplied) / maxDragSize;
            rb.AddForce(direction * calculatedForceApplied);
        }
    }
}
