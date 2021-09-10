using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("/api/[controller]")]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _service;

    public VaultsController(VaultsService service)
    {
      _service = service;
    }

    [HttpGet]
    public ActionResult<List<Vault>> Get()
    {
      try
      {
        List<Vault> vaults = _service.GetAll();
        return Ok(vaults);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault newVault)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newVault.CreatorId = userInfo.Id;
        Vault vault = _service.Create(newVault);
        return Ok(vault);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

  }
}