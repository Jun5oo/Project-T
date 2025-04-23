using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    bool isDrag;

    GameObject lastInteracted; 

    public PlayerInput playerInput;

    InputAction clickAction;
    InputAction mouseAction;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        isDrag = false;

        lastInteracted = null; 
        playerInput.enabled = true;

        clickAction = playerInput.actions["Mouse Click"];
        mouseAction = playerInput.actions["Mouse Position"];

        clickAction.Enable(); 
        mouseAction.Enable(); 

        clickAction.started += OnMouseDragInput;
        clickAction.canceled += OnMouseUpInput; 
        mouseAction.performed += OnMouseMoveInput; 

    }

    public void OnMouseMoveInput(InputAction.CallbackContext context)
    {
        Vector2 mouseScreenPosition = context.ReadValue<Vector2>();
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        if (isDrag && lastInteracted != null)
        {
            IDraggable draggable = lastInteracted.GetComponent<IDraggable>(); 
            
            if(draggable != null)
                draggable.OnDrag(mousePosition); 
        }
        
        else
        {
            RaycastHit2D rayHit = Physics2D.Raycast(mousePosition, Vector3.forward);

            if (lastInteracted != null && (rayHit.collider == null || rayHit.collider.gameObject != lastInteracted))
            {
                IHoverable hoverable = lastInteracted.GetComponent<IHoverable>();

                if (hoverable != null)
                    hoverable.OnHoverExit();

                lastInteracted = null;
            }

            if (rayHit.collider != null)
            {
                GameObject hit = rayHit.collider.gameObject;

                if (hit != lastInteracted)
                {
                    IHoverable hoverable = hit.GetComponent<IHoverable>();

                    if (hoverable != null)
                        hoverable.OnHoverEnter();

                    lastInteracted = hit;
                }
            }
        }
    }
    public void OnMouseDragInput(InputAction.CallbackContext context)
    {
        Vector2 mouseScreenPosition = Input.mousePosition;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        RaycastHit2D rayHit = Physics2D.Raycast(mousePosition, Vector3.forward); 

        if(rayHit.collider != null)
        {
            GameObject hit = rayHit.collider.gameObject;
            IDraggable draggable = hit.GetComponent<IDraggable>();

            if(draggable != null)
            {
                isDrag = true;
                lastInteracted = hit; 

                IHoverable hoverable = hit.GetComponent<IHoverable>();

                if (hoverable != null)
                    hoverable.OnHoverExit();

                draggable.OnDragEnter(mousePosition); 
            }
        }
    }
    public void OnMouseUpInput(InputAction.CallbackContext context)
    {
        Vector2 mouseScreenPosition = Input.mousePosition;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        if (isDrag && lastInteracted != null)
        {
            IDraggable draggable = lastInteracted.GetComponent<IDraggable>(); 
            
            if(draggable != null)
            {
                draggable.OnDragExit(mousePosition);
                lastInteracted = null; 
            }
        }
        
        isDrag = false;
    }

}
