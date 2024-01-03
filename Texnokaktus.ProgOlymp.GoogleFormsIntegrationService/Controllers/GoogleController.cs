using Microsoft.AspNetCore.Mvc;
using Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.GoogleClient.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.GoogleFormsIntegrationService.Controllers;

public class GoogleController(LinkGenerator linkGenerator,
                              ILogger<GoogleController> logger,
                              IGoogleAuthenticationService googleAuthenticationService,
                              ITokenService tokenService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var accessToken = await tokenService.GetAccessTokenAsync();
        return View(accessToken is not null);
    }
    
    public IActionResult Auth()
    {
        var authUri = googleAuthenticationService.GetGoogleOAuthUrl(RedirectUri);
        return Redirect(authUri);
    }

    public IActionResult Complete(string? code, string? error) =>
        (code, error) switch
        {
            (not null, null) => RedirectToAction(nameof(Code), new { code }),
            (null, not null) => RedirectToAction(nameof(Error), new { error }),
            _                => throw new("Invalid Google response.")
        };

    public async Task<IActionResult> Code(string code)
    {
        var response = await googleAuthenticationService.GetAccessTokenAsync(code, RedirectUri);
        await tokenService.RegisterTokenAsync(response);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Revoke()
    {
        await tokenService.RevokeTokenAsync();
        return RedirectToAction(nameof(Index));
    }

    private string RedirectUri => linkGenerator.GetUriByAction(HttpContext, nameof(Complete))!;

    public IActionResult Error(string error) => View("Error", error);
}
