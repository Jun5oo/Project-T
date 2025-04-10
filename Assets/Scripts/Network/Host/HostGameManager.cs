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
            // CreateAllocationAsync: Relay �ý��ۿ��� ���� ���� ȣ���ؾ��ϴ� �Լ���, �߰� ������ ���� �ڿ� �Ҵ���� 
            // allocation: Relay ���ῡ �ʿ��� ������ ��� �ִ� ��ü 
            allocation = await Relay.Instance.CreateAllocationAsync(MaxConnection);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return; 
        }

        try
        {
            // �ش� Relay ������ AllocationID�� �߱� 
            joinCode = await Relay.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log(joinCode); 
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return;
        }

        // Netcode for GameObjects ���� �����͸� � ������ ������ �� �����ϴ� ��Ʈ��ũ ���۰����� ������ 
        UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        
        // RelayServerData: Relay ������ �����ϱ� ���� ������ ���� ��ü 
        RelayServerData relayServerData = new RelayServerData(allocation, "dtls"); 
        
        // �ش� Relay ������ �����ϱ� ���� ������ ���� 
        transport.SetRelayServerData(relayServerData);

        NetworkManager.Singleton.StartHost();

        NetworkManager.Singleton.SceneManager.LoadScene(GameSceneName, LoadSceneMode.Single);
    }
}
