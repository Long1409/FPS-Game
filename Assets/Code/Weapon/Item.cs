using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public string name;
    public string discription;
    public Sprite icon;

    public virtual void Use()
    {
        Debug.Log(name + "was used");
    }
}
