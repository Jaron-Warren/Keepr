using System;
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

    // internal List<Vault> GetAll()
    // {
    //   return _repo.GetAll();
    // }

    internal List<Vault> GetProfileVaults(string id, string userId = "null")
    {
      List<Vault> vaults = _repo.GetProfileVaults(id);
      if (vaults[0] != null && userId != vaults[0].CreatorId)
      {
        vaults.RemoveAll(v => v.IsPrivate == true);
      }
      return vaults;
    }

    internal Vault GetByIdAuth(int vaultId, string userId)
    {
      Vault found = _repo.GetById(vaultId);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      if (found.IsPrivate == true && found.CreatorId != userId)
      {
        throw new Exception("Bad request");
      }
      return found;
    }

    internal Vault GetById(int vaultId)
    {
      Vault found = _repo.GetById(vaultId);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      if (found.IsPrivate == true)
      {
        throw new Exception("Bad request");
      }
      return found;
    }

    internal Vault Create(Vault newVault)
    {
      return _repo.Create(newVault);
    }

    internal Vault Edit(Vault updatedVault)
    {
      Vault original = GetById(updatedVault.Id);
      if (original.CreatorId != updatedVault.CreatorId)
      {
        throw new Exception("not yours!");
      }
      original.Name = updatedVault.Name ?? original.Name;
      original.IsPrivate = updatedVault.IsPrivate ?? original.IsPrivate;
      original.Description = updatedVault.Description != null ? updatedVault.Description : original.Description;
      _repo.Edit(original);
      return original;
    }

    internal void Delete(int vaultId, string userId)
    {
      Vault toDelete = GetByIdAuth(vaultId, userId);
      if (toDelete.CreatorId != userId)
      {
        throw new Exception("That doesn't belong to you!");
      }
      _repo.Delete(vaultId);
    }

  }
}