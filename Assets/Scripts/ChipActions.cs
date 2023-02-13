using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChipActions : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {
    Rigidbody2D chipRB;
    [SerializeField] LineRenderer chipLine;
    GameManager gm;


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
        chipRB = GetComponent<Rigidbody2D>();

    }

    private void Awake() {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    public void OnPointerDown(PointerEventData eventData) {
    }


    public void OnBeginDrag(PointerEventData data) {
        //STARTING POSITION
        startPoint = data.position;

    }

    public void OnDrag(PointerEventData data) {
        if (gm.moveMode.isOn) {
            ChipDrag(data);
        }

        if (gm.attackMode.isOn) {
        }

    }

    public void OnEndDrag(PointerEventData data) {
        if (gm.moveMode.isOn) {
            ChipMovement(data);
        }

        if (gm.attackMode.isOn) {
        }

        
        
    }

    public void ChipMovement(PointerEventData data) {
        //ENDING POSITION
        endPoint = data.position;

        // CLEAR THE LINE
        chipLine.SetPosition(0, new Vector3(0f, 0f, 0f));

        //NORMALIZED VECTOR (DIRECTION OF FORCE)
        Vector2 directionOfDrag = -(endPoint - startPoint).normalized;

        //HOW LONG THE DRAG IS.
        CalculateDragDistance(startPoint, endPoint, directionOfDrag);
    }

    public void ChipDrag(PointerEventData data) {
        Vector2 currentPoint = data.position;

        //NORMALIZED VECTOR (DIRECTION OF DRAG LINE)
        Vector2 directionOfLine = (currentPoint - startPoint).normalized;

        //WHILE DRAGGIND DRAW A LINE
        DrawDragLine(startPoint, currentPoint, directionOfLine);
    }

    public void CalculateDragDistance(Vector2 start, Vector2 end, Vector2 direction) {
        float sizeOfDrag = Mathf.Sqrt(Mathf.Pow((end.x - start.x), 2F) + Mathf.Pow((end.y - start.y), 2F));

        if (sizeOfDrag >= maxDragSize) {
            chipRB.AddForce(direction * maxForceApplied);
        } else if (sizeOfDrag == minDragSize) {
            chipRB.AddForce(direction * minForceApplied);
        } else if (sizeOfDrag > minDragSize || sizeOfDrag < maxDragSize) {
            float calculatedForceApplied = (float)(sizeOfDrag * maxForceApplied) / maxDragSize;
            chipRB.AddForce(direction * calculatedForceApplied);
        }
    }

    public void DrawDragLine(Vector2 start, Vector2 end, Vector2 direction) {
        float sizeOfLine = Mathf.Sqrt(Mathf.Pow((end.x - start.x), 2F) + Mathf.Pow((end.y - start.y), 2F));

        if (sizeOfLine >= maxDragSize) {
            chipLine.SetPosition(0, new Vector3(3f*direction.x, 3f*direction.y, 0f));
        } else if (sizeOfLine == minDragSize) {
            chipLine.SetPosition(0, new Vector3(1f * direction.x, 1f * direction.y, 0f));
        } else if (sizeOfLine > minDragSize || sizeOfLine < maxDragSize) {
            float calculatedLineDrawn = (float)(sizeOfLine * 3f) / maxDragSize;
            chipLine.SetPosition(0, new Vector3(calculatedLineDrawn * direction.x, calculatedLineDrawn * direction.y, 0f));
        }
    }
}
