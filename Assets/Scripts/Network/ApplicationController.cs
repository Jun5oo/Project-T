using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ApplicationController : MonoBehaviour
{
    [SerializeField] private HostSingleton hostPrefab; 
    [SerializeField] private ClientSingleton clientPrefab; 
    
    private async void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        await LaunchInMode(SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null); 
    }

    private async Task LaunchInMode(bool isDedicatedServer)
    {
        if (isDedicatedServer)
        {

        }

        else
        {
            HostSingleton hostSingleton = Instantiate(hostPrefab);
            hostSingleton.CreateHost();

            ClientSingleton clientSingleton = Instantiate(clientPrefab);
            bool authenticated = await clientSingleton.CreateClient();

            if (authenticated)
            {
                clientSingleton.GameManager.GoToMenu(); 
            }
        }
    }

}
