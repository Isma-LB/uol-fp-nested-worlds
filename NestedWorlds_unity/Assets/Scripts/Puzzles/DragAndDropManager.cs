using System;
using System.Collections;
using System.Collections.Generic;
using IsmaLB.Input;
using UnityEngine;

namespace IsmaLB.Puzzles
{
    public class DragAndDropManager : MonoBehaviour
    {
        [SerializeField] LayerMask dragLayerMask;
        [SerializeField] Camera cam;
        [SerializeField] InputReader inputReader;
        DraggableItem draggableItem;
        Vector2 pointerPos;
        void OnEnable()
        {
            inputReader.grabPressedEvent += OnGrabPressed;
            inputReader.grabReleasedEvent += OnGrabReleased;
            inputReader.pointerPositionEvent += OnPointerPosition;
        }
        void OnDisable()
        {
            inputReader.grabPressedEvent -= OnGrabPressed;
            inputReader.grabReleasedEvent -= OnGrabReleased;
            inputReader.pointerPositionEvent -= OnPointerPosition;
        }

        void Update()
        {
            if (draggableItem != null)
            {
                Drag();
            }
        }
        void AttemptDrag()
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(pointerPos);
            Collider2D target = Physics2D.OverlapPoint(mousePos, dragLayerMask);
            if (target)
            {
                InitDrag(target);
            }
        }
        void InitDrag(Collider2D target)
        {
            if (target.TryGetComponent<DraggableItem>(out DraggableItem item))
            {
                draggableItem = item;
            }
        }
        void Drag()
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(pointerPos);
            mousePos.z = draggableItem.transform.position.z;
            draggableItem.transform.position = mousePos;
        }
        void Drop()
        {
            draggableItem = null;
        }
        // Input callbacks
        private void OnGrabPressed() => AttemptDrag();
        private void OnPointerPosition(Vector2 pos) => pointerPos = pos;
        private void OnGrabReleased() => Drop();

    }
}
