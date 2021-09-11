using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class ProfilesService
  {
    private readonly AccountsRepository _repo;

    public ProfilesService(AccountsRepository repo)
    {
      _repo = repo;
    }

    internal Profile GetProfileById(string id)
    {
      Profile profile = _repo.GetById(id);
      if (profile == null)
      {
        throw new Exception("Profile does not exist");
      }
      return profile;
    }
  }
}