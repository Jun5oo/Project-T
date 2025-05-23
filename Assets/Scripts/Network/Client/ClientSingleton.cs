using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ClientSingleton : MonoBehaviour
{
    public  ClientGameManager GameManager { get; private set; }

    private static ClientSingleton instance;
    public static ClientSingleton Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<ClientSingleton>();
            
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

    public async Task<bool> CreateClient()
    {
        GameManager = new ClientGameManager();

        return await GameManager.InitAsync(); 
    }
}
