using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class Ficha : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {
    Rigidbody2D rb;

    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 vectorDirectionOfDrag;
    Vector2 vectorDistanceOfDrag;
    public float sizeOfDrag;

    public float minForce;
    public float maxForce;

    public float minDragSize;
    public float maxDragSize;
    // Start is called before the first frame update
    void Start() {
        minForce = 200f;
        maxForce = 2000f;

        minDragSize = 90f;
        maxDragSize = 300f;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void FixedUpdate() { 
    }

    public void OnPointerDown(PointerEventData eventData) {
    }


    public void OnBeginDrag(PointerEventData data) {
        startPoint = data.position;

    }

    public void OnDrag(PointerEventData data) {
    }

    public void OnEndDrag(PointerEventData data) {
        endPoint = data.position;

        vectorDirectionOfDrag = -(endPoint - startPoint).normalized;
        vectorDistanceOfDrag = endPoint - startPoint;

        sizeOfDrag = Mathf.Sqrt(Mathf.Pow(vectorDistanceOfDrag.x, 2F) + Mathf.Pow(vectorDistanceOfDrag.y, 2F));

        if (sizeOfDrag >= maxDragSize) {
            Debug.Log(0);
            rb.AddForce(vectorDirectionOfDrag * maxForce);
        } else if (sizeOfDrag <= minDragSize) {
            Debug.Log(1);
            rb.AddForce(vectorDirectionOfDrag * minForce);
        } else {
            Debug.Log(2);
            float fuerzaDeTres = (float)(sizeOfDrag * maxForce) / maxDragSize;
            rb.AddForce(vectorDirectionOfDrag * fuerzaDeTres);
        }
        
    }
}
