using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HostSingleton : MonoBehaviour
{
    public HostGameManager GameManager { get; private set; }
    
    private static HostSingleton instance;
    public static HostSingleton Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<HostSingleton>();
            
            if(instance == null)
            {
                Debug.LogError("No ClientSingleton in the scene");
                return null; 
            }

            return instance; 
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject); 
    }

    public void CreateHost()
    {
        GameManager = new HostGameManager();
    }
}
