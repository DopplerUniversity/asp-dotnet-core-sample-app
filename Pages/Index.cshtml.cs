using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using DopplerWebApp.Models;

namespace DopplerWebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public Doppler Doppler { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IOptions<Doppler> doppler)
    {
        _logger = logger;
        Doppler = doppler.Value;
    }

    public void OnGet()
    {

    }
}
