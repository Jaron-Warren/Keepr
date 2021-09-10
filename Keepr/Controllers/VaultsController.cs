using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Services;
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

  }
}