using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vkrepo;
    private readonly KeepsRepository _keepsrepo;
    private readonly VaultsRepository _vaultsrepo;

    public VaultKeepsService(VaultKeepsRepository vkrepo, KeepsRepository keepsrepo, VaultsRepository vaultsrepo)
    {
      _vkrepo = vkrepo;
      _keepsrepo = keepsrepo;
      _vaultsrepo = vaultsrepo;
    }

    internal List<VaultKeepsViewModel> GetVaultKeeps(int vaultId)
    {
      return _vkrepo.GetVaultKeeps(vaultId);
    }

    private VaultKeep GetById(int id)
    {
      VaultKeep found = _vkrepo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }

    internal VaultKeep Create(VaultKeep newVK)
    {
      Keep keep = _keepsrepo.GetById(newVK.KeepId);
      if (keep.CreatorId != newVK.CreatorId)
      {
        throw new Exception("Bad request");
      }
      Vault vault = _vaultsrepo.GetById(newVK.VaultId);
      if (vault.CreatorId != newVK.CreatorId)
      {
        throw new Exception("Bad request");
      }
      keep.Keeps++;
      _keepsrepo.EditStats(keep);
      return _vkrepo.Create(newVK);
    }
    internal string Delete(int vkId, string userId)
    {
      VaultKeep vk = GetById(vkId);
      if (vk.CreatorId == userId)
      {
        _vkrepo.Delete(vkId);
        Keep keep = _keepsrepo.GetById(vk.KeepId);
        keep.Keeps--;
        _keepsrepo.EditStats(keep);
        return "Removed Keep from your collection";
      }
      throw new Exception("Bad request");
    }

  }
}