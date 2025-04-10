using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public static class AuthenticationWrapperHandler
{
    public static AuthenticationState AuthState { get; private set; } = AuthenticationState.NotAuthenticated; 

    public static async Task<AuthenticationState> DoAuth(int maxRetries = 5)
    {
        if (AuthState == AuthenticationState.Authenticated)
            return AuthState;

        // ���ÿ� Authenticating�� �õ����� �ʰ� �ϱ� ���� 

        if(AuthState == AuthenticationState.Authenticating)
        {
            Debug.LogWarning("Already authenticating!"); 
            await Authenticating();
            return AuthState; 
        }
        await SignInAnonymouslyAsync(maxRetries); 
 
        return AuthState; 
    }

    private static async Task<AuthenticationState> Authenticating()
    {
        while(AuthState == AuthenticationState.Authenticating || AuthState == AuthenticationState.NotAuthenticated)
        {
            await Task.Delay(200); 
        }

        return AuthState; 
    }

    private static async Task SignInAnonymouslyAsync(int maxTries)
    {
        AuthState = AuthenticationState.Authenticating;

        int retries = 0;

        while (AuthState == AuthenticationState.Authenticating && retries < maxTries)
        {
            try
            {
                // all code in try will be executed 

                // Relay ������ �����ϱ� ���� �ʿ��� Unity Cloud Service�� �α��� ���� ���� �����ϰ� �ϱ� ���� �ڵ� 
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

                if (AuthenticationService.Instance.IsSignedIn && AuthenticationService.Instance.IsAuthorized)
                {
                    AuthState = AuthenticationState.Authenticated;
                    break;
                }
            }
            catch(AuthenticationException authException)
            {
                // any exceptions are thrown inside the catch 
                Debug.LogError(authException);
                AuthState = AuthenticationState.Error; 
            }
            catch(RequestFailedException requestException)
            {
                Debug.LogError(requestException);
                AuthState = AuthenticationState.Error; 
            }

            retries++;
            await Task.Delay(1000);
        }

        // If it used all the tries, time out 
        if(AuthState != AuthenticationState.Authenticated)
        {
            Debug.LogWarning($"Player was not signed in successfully after {retries} tries"); 
            AuthState = AuthenticationState.TimeOut; 
        }
    }
}

public enum AuthenticationState
{
    NotAuthenticated,
    Authenticating, 
    Authenticated, 
    Error, 
    TimeOut
}
