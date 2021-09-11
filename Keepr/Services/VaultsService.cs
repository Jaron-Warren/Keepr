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

    internal List<Vault> GetProfileVaults(string id)
    {
      return _repo.GetProfileVaults(id);
    }

    internal Vault GetById(int id)
    {
      Vault found = _repo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
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
      Vault toDelete = GetById(vaultId);
      if (toDelete.CreatorId != userId)
      {
        throw new Exception("That doesn't belong to you!");
      }
      _repo.Delete(vaultId);
    }

  }
}