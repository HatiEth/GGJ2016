using UnityEngine;
using System.Collections;

public interface Interactable {

    void Interact(Picker picker);
}


public interface Outlineable
{
    void onMouseover();
}