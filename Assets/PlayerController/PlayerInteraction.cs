using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject Current = null;

    [SerializeField] private InteractableObject interactableObject;

    void Start()
    {
        //DontDestroyOnLoad(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        
        if (other.gameObject.CompareTag("Interactable"))
        {
            Debug.Log("Here");
            Current = other.gameObject;
            PlayerInputActions.InteractEvent += Interact;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            PlayerInputActions.InteractEvent -= Interact;
            Current = null;
        }
    }

    public void Interact()
    {
        interactableObject = Current.GetComponent<InteractableObject>();

        interactableObject.Interact();
    }


}
