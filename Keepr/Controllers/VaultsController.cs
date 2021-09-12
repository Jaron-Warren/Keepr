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
    private readonly VaultKeepsService _vkService;

    public VaultsController(VaultsService service, VaultKeepsService vkService)
    {
      _service = service;
      _vkService = vkService;
    }

    // [HttpGet]
    // public ActionResult<List<Vault>> Get()
    // {
    //   try
    //   {
    //     List<Vault> vaults = _service.GetAll();
    //     return Ok(vaults);
    //   }
    //   catch (Exception err)
    //   {
    //     return BadRequest(err.Message);
    //   }
    // }

    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> Get(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Vault vault;
        if (userInfo == null)
        {
          vault = _service.GetById(id);
          return Ok(vault);
        }
        vault = _service.GetByIdAuth(id, userInfo.Id);
        return Ok(vault);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<List<VaultKeepsViewModel>>> GetVaultKeeps(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<VaultKeepsViewModel> keeps;
        if (userInfo == null)
        {
          keeps = _vkService.GetVaultKeeps(id);
          return Ok(keeps);
        }
        keeps = _vkService.GetVaultKeepsAuth(id, userInfo.Id);
        return Ok(keeps);
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

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Edit([FromBody] Vault updatedVault, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updatedVault.CreatorId = userInfo.Id;
        updatedVault.Id = id;
        Vault vault = _service.Edit(updatedVault);
        return Ok(vault);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _service.Delete(id, userInfo.Id);
        return Ok("vault has been deleted");
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

  }
}