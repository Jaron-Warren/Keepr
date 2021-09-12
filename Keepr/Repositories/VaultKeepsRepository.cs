using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<VaultKeepsViewModel> GetVaultKeeps(int vaultId)
    {
      string sql = @"
      SELECT
        k.*,
        vk.id AS vaultKeepId
      FROM vaultkeeps vk
      JOIN keeps k ON vk.keepId = k.id
      WHERE vk.vaultId = @vaultId
      ;";
      return _db.Query<VaultKeepsViewModel>(sql, new { vaultId }).ToList();
    }

    internal VaultKeep GetById(int id)
    {
      string sql = "SELECT * FROM vaultkeeps WHERE id = @id;";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }

    internal VaultKeep Create(VaultKeep newVK)
    {
      string sql = "INSERT INTO vaultkeeps (creatorId, vaultId, keepId) VALUES (@CreatorId, @VaultId, @KeepId); SELECT LAST_INSERT_ID();";
      newVK.Id = _db.ExecuteScalar<int>(sql, newVK);
      return newVK;
    }

    internal void Delete(int vkId)
    {
      string sql = "DELETE FROM vaultkeeps WHERE id = @vkId LIMIT 1;";
      _db.Execute(sql, new { vkId });
    }
  }
}