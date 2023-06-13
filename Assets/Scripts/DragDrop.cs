using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public GameObject Canvas;

    private bool isDragging = false;
    private GameObject startParent;
    private Vector2 startPosistion;
    private GameObject dropZone;
    private bool isOverDropZone;

    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.Find("Main Canvas");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Colliding!");
        isOverDropZone = true;
        dropZone = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log("Uncolliding!");
        isOverDropZone = false;
        dropZone = null;
        
    }

    public void BeginDrag()
    {
        isDragging = true;
        startParent = transform.parent.gameObject;
        startPosistion = transform.position;
        
    }

    public void EndDrag()
    {
        isDragging = false;
        if (isOverDropZone)
        {
            transform.SetParent(dropZone.transform, false);
        }
        else
        {
            transform.position = startPosistion;
            transform.SetParent(startParent.transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            //Goal: the card need to follow the mouse when clicked and held. Make the card have a parent canvas.
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
    }
}
