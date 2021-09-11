using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }

    internal List<Keep> GetAll()
    {
      return _repo.GetAll();
    }

    internal Keep GetById(int id)
    {
      Keep found = _repo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      found.Views++;
      return found;
    }

    internal Keep Create(Keep newKeep)
    {
      return _repo.Create(newKeep);
    }

    internal Keep Edit(Keep updatedKeep)
    {
      Keep original = GetById(updatedKeep.Id);
      if (original.CreatorId != updatedKeep.CreatorId)
      {
        throw new Exception("not yours!");
      }
      original.Name = updatedKeep.Name ?? original.Name;
      original.Img = updatedKeep.Img ?? original.Img;
      original.Description = updatedKeep.Description != null ? updatedKeep.Description : original.Description;
      _repo.Edit(original);
      return original;
    }

    internal void Delete(int keepId, string userId)
    {
      Keep toDelete = GetById(keepId);
      if (toDelete.CreatorId != userId)
      {
        throw new Exception("That doesn't belong to you!");
      }
      _repo.Delete(keepId);
    }

  }
}