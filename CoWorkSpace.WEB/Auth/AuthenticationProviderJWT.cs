using CoWorkSpace.WEB.Helpers;
using CoWorkSpace.WEB.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;

namespace CoWorkSpace.WEB.Auth;

public class AuthenticationProviderJWT(IJSRuntime js, HttpClient httpClient) : AuthenticationStateProvider, ILoginServices
{
    private readonly IJSRuntime js = js;
    private readonly HttpClient httpClient = httpClient;
    private static readonly string TOKENKEY = "TOKENKEY";
    private static AuthenticationState Anonimo = new(new ClaimsPrincipal(new ClaimsIdentity()));

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await js.GetFromLocalStorage(TOKENKEY);

        if (string.IsNullOrEmpty(token))
        {
            return Anonimo;
        }

        return BuildAuthenticationState(token);
    }

    private AuthenticationState BuildAuthenticationState(string token)
    {
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
    }

    private static List<Claim>? ParseClaimsFromJwt(string token)
    {
        var claims = new List<Claim>();
        var payload = token.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!)));
        if (keyValuePairs.TryGetValue("role", out var roleValue))
        {
            claims.Add(new Claim(ClaimTypes.Role, roleValue.ToString()!));
        }

        return claims;
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }

        return Convert.FromBase64String(base64);
    }

    public async Task Login(string token)
    {
        await js.SetInLocalStorage(TOKENKEY, token);
        var authState = BuildAuthenticationState(token);
        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }

    public async Task Logout()
    {
        httpClient.DefaultRequestHeaders.Authorization = null;
        NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));
    }
}