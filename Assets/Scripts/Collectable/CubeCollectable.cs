using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface Collectable
{
    void Collected();
}
public class CubeCollectable : MonoBehaviour, Collectable
{
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }
    public void Collected()
    {
        UIController.inst.CubeCollected(1);
        gameObject.SetActive(false);
    }
    
}
