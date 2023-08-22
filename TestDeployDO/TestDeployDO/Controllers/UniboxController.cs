using Microsoft.AspNetCore.Mvc;
using TestDeployDO.Models;

namespace TestDeployDO.Controllers;

[ApiController]
[Route("/unibox")]
public class UniboxController : ControllerBase
{
    [HttpGet("/getBagsInUnibox")]
    public IEnumerable<PantLogsBag> Get(int id)
    {
        UniboxV1DbContext dbContext = new UniboxV1DbContext();

        return dbContext.PantLogsBags.Where(p => p.Log.Unibox.Id.Equals(id));
    }
}