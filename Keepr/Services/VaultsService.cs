using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;

    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }

    internal List<Vault> GetAll()
    {
      return _repo.GetAll();
    }

    internal Vault Create(Vault newVault)
    {
      return _repo.Create(newVault);
    }
  }
}