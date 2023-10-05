using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void OnPlayerApproach();

    void OnPlayerInteract();

    void ResetToDefaults();
    GameObject gameObject { get; }
}
