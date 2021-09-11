using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("/api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _profilesService;
    private readonly KeepsService _keepsService;
    private readonly VaultsService _vaultsService;

    public ProfilesController(ProfilesService profilesService, KeepsService keepsService, VaultsService vaultsService)
    {
      _profilesService = profilesService;
      _keepsService = keepsService;
      _vaultsService = vaultsService;
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> Get(string id)
    {
      try
      {
        return Ok(_profilesService.GetProfileById(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetKeeps(string id)
    {
      try
      {
        return Ok(_keepsService.GetProfileKeeps(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/vaults")]
    public ActionResult<List<Vault>> GetVaults(string id)
    {
      try
      {
        return Ok(_vaultsService.GetProfileVaults(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }


}