using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Keep> GetAll()
    {
      string sql = @"
      SELECT
      a.*,
      k.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId
      ";
      return _db.Query<Profile, Keep, Keep>(sql, (prof, keep) =>
      {
        keep.Creator = prof;
        return keep;
      }, splitOn: "id").ToList<Keep>();
    }

    internal Keep GetById(int id)
    {
      string sql = @"
      SELECT
      a.*,
      k.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId
      WHERE k.id = @id;
      ";
      return _db.Query<Profile, Keep, Keep>(sql, (prof, keep) =>
      {
        keep.Creator = prof;
        return keep;
      }, new { id }, splitOn: "id").FirstOrDefault();
    }

    internal List<Keep> GetProfileKeeps(string id)
    {
      string sql = @"
      SELECT
      a.*,
      k.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId
      WHERE a.id = @id;
      ";
      return _db.Query<Profile, Keep, Keep>(sql, (prof, keep) =>
      {
        keep.Creator = prof;
        return keep;
      }, new { id }, splitOn: "id").ToList<Keep>();
    }

    internal Keep Create(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps
      (name, description, creatorId, img, tags)
      VALUES
      (@Name, @Description, @CreatorId, @Img, @Tags);
      SELECT LAST_INSERT_ID()
      ;";
      newKeep.Id = _db.ExecuteScalar<int>(sql, newKeep);
      return GetById(newKeep.Id);
    }

    internal Keep Edit(Keep updatedKeep)
    {
      string sql = @"
      UPDATE keeps
      SET
        name = @Name,
        description = @Description,
        img = @Img
      WHERE id = @id
      ;";
      _db.Execute(sql, updatedKeep);
      return updatedKeep;
    }

    internal Keep EditStats(Keep updatedKeep)
    {
      string sql = @"
      UPDATE keeps
      SET
        views = @Views,
        keeps = @Keeps,
        shares = @Shares
      WHERE id = @id
      ;";
      _db.Execute(sql, updatedKeep);
      return updatedKeep;
    }

    internal void Delete(int keepId)
    {
      string sql = "DELETE FROM keeps WHERE id = @keepId LIMIT 1;";
      _db.Execute(sql, new { keepId });
    }
  }
}