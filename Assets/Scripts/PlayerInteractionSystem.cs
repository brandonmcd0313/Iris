using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInteractionSystem : MonoBehaviour
{
    [SerializeField] float _interactionRange;
    List<IInteractable> interactables = new List<IInteractable>();
    // Start is called before the first frame update
    void Start()
    {
        interactables = FindObjectsOfType<MonoBehaviour>().OfType<IInteractable>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        LookForInteractableObjectsInRange(_interactionRange);
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            InteractWithNearestInteractableObject();
        }
    }

    void LookForInteractableObjectsInRange(float range)
    {
        foreach (IInteractable interactable in interactables)
        {
            if(interactable == null)
            {
                interactables.Remove(interactable);
                continue;
            }
            //check if the interactable is in range
            if (Vector3.Distance(transform.position, interactable.gameObject.transform.position) <= range)
            {
                //if it is, call the OnPlayerApproach method
                interactable.OnPlayerApproach();
            }
            else
            {
                //if it is not, call the OnPlayerLeave method
                interactable.ResetToDefaults();
            }
        
        }
    }

    void InteractWithNearestInteractableObject()
    {
        //find the nearest interactable object
        IInteractable nearestInteractable = null;
        float nearestDistance = Mathf.Infinity;
        foreach (IInteractable interactable in interactables)
        {
            float distance = Vector3.Distance(transform.position, interactable.gameObject.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestInteractable = interactable;
            }
        }

        //if nearest distance is greater than range then there is no nearest interactable object
        if (nearestDistance > _interactionRange)
        {
            return;
        }
        
        //if there is a nearest interactable object, call the OnPlayerInteract method
        if (nearestInteractable != null)
        {
            nearestInteractable.OnPlayerInteract();
        }
    }
}

