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

        // 동시에 Authenticating을 시도하지 않게 하기 위해 

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

                // Relay 서버에 접속하기 위해 필요한 Unity Cloud Service에 로그인 없이 접근 가능하게 하기 위한 코드 
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
