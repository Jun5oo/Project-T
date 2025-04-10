using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HostGameManager
{
    private Allocation allocation;
    private string joinCode; 

    private const int MaxConnection = 2;
    private const string GameSceneName = "GamePlay"; 
    public async Task StartHostAsync()
    {
        try
        {
            // CreateAllocationAsync: Relay 시스템에서 가장 먼저 호출해야하는 함수로, 중계 연결을 위한 자원 할당받음 
            // allocation: Relay 연결에 필요한 정보를 담고 있는 객체 
            allocation = await Relay.Instance.CreateAllocationAsync(MaxConnection);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return; 
        }

        try
        {
            // 해당 Relay 서버의 AllocationID를 발급 
            joinCode = await Relay.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log(joinCode); 
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return;
        }

        // Netcode for GameObjects 에서 데이터를 어떤 식으로 보내는 지 결정하는 네트워크 전송계층을 가져옴 
        UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        
        // RelayServerData: Relay 서버에 연결하기 위한 정보를 담은 객체 
        RelayServerData relayServerData = new RelayServerData(allocation, "dtls"); 
        
        // 해당 Relay 서버로 연결하기 위한 데이터 세팅 
        transport.SetRelayServerData(relayServerData);

        NetworkManager.Singleton.StartHost();

        NetworkManager.Singleton.SceneManager.LoadScene(GameSceneName, LoadSceneMode.Single);
    }
}
