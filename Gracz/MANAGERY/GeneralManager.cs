using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    [SerializeReference] public List<Manager> managers = new ();


    void OnEnable()
    {
        foreach(var manager in managers)
        {
            manager.Initialize(this);
        }
    }

    public T GetManager<T>() where T : Manager
    {
        return (T)managers.Find(manager => manager is T);
    }
}