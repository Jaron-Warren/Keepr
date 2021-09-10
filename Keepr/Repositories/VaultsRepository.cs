using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Vault> GetAll()
    {
      string sql = "SELECT * FROM vaults;";
      return _db.Query<Vault>(sql).ToList();
    }

    internal Vault Create(Vault newVault)
    {
      string sql = @"
      INSERT INTO vaults
      (name, description, isPrivate, creatorId)
      VALUES
      (@Name, @Description, @IsPrivate, @CreatorId);
      SELECT LAST_INSERT_ID()
      ;";
      newVault.Id = _db.ExecuteScalar<int>(sql, newVault);
      return newVault;
    }
  }
}