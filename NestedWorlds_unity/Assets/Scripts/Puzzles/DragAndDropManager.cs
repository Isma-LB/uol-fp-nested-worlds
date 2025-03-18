using IsmaLB.Input;
using UnityEngine;

namespace IsmaLB.Puzzles
{
    public class DragAndDropManager : MonoBehaviour
    {
        [SerializeField] LayerMask dragLayerMask;
        [SerializeField] Camera cam;
        [SerializeField, Min(0)] int dragMarginInPixels = 32;
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
            Vector2 screenPos = LimitScreenPoint(pointerPos, cam.pixelWidth, cam.pixelHeight, dragMarginInPixels);
            Vector3 mousePos = cam.ScreenToWorldPoint(screenPos);
            mousePos.z = draggableItem.transform.position.z;
            draggableItem.transform.position = mousePos;
        }
        void Drop()
        {
            draggableItem = null;
        }
        Vector2 LimitScreenPoint(Vector3 pos, int width, int height, int margin = 0)
        {
            if (pos.x >= width - margin) pos.x = width - 1 - margin;
            else if (pos.x < 0 + margin) pos.x = 0 + margin;
            if (pos.y >= height - margin) pos.y = height - 1 - margin;
            else if (pos.y < 0 + margin) pos.y = 0 + margin;
            return pos;
        }
        // Input callbacks
        private void OnGrabPressed() => AttemptDrag();
        private void OnPointerPosition(Vector2 pos) => pointerPos = pos;
        private void OnGrabReleased() => Drop();

    }
}
