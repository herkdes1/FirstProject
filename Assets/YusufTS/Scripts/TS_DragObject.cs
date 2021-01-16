using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YusufTS
{
    public class TS_DragObject : MonoBehaviour
    {
        bool isMoving;
        Vector3 targetPos;
        float speed = 15f;
        Plane plane;

        private void OnMouseDrag()
        {

            SetTarggetPosition();
            isMoving = true;
            if (isMoving)
            {
                MoveObject();
            }
        }

        void SetTarggetPosition()
        {
            plane = new Plane(Vector3.forward, transform.position);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float point = 0f;

            if (plane.Raycast(ray, out point))
                targetPos = ray.GetPoint(point);

        }
        void MoveObject()
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            if (transform.position == targetPos)
                isMoving = false;
            Debug.DrawLine(transform.position, targetPos, Color.red);
        }
    }

}

