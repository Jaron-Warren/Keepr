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

    // internal List<Vault> GetAll()
    // {
    //   string sql = "SELECT * FROM vaults;";
    //   return _db.Query<Vault>(sql).ToList();
    // }

    internal Vault GetById(int id)
    {
      string sql = @"
      SELECT
      a.*,
      v.*
      FROM vaults v
      JOIN accounts a ON a.id = v.creatorId
      WHERE v.id = @id;
      ";
      return _db.Query<Profile, Vault, Vault>(sql, (prof, vault) =>
      {
        vault.Creator = prof;
        return vault;
      }, new { id }, splitOn: "id").FirstOrDefault();
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
      return GetById(newVault.Id);
    }

    internal Vault Edit(Vault updatedVault)
    {
      string sql = @"
      UPDATE vaults
      SET
        name = @Name,
        description = @Description,
        isPrivate = @IsPrivate
      WHERE id = @id
      ;";
      _db.Execute(sql, updatedVault);
      return updatedVault;
    }

    internal void Delete(int vaultId)
    {
      string sql = "DELETE FROM vaults WHERE id = @vaultId LIMIT 1;";
      _db.Execute(sql, new { vaultId });
    }
  }
}