using BrainUp.Data;
using BrainUp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace BrainUp.StoredProcedure
{
    public class LevelAction
    {
        public static int Add(Level level, BrainUpBdContext context)
        {
            int courceId = -1;
            try
            {
                var parameters = new[] {
                    new SqlParameter("@name", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = level.Name
                    }

                };

                var result = context.Database.ExecuteSqlRaw(
                  "exec [dbo].[CreateLevel] @name", parameters:
                  parameters);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return courceId;
        }

        public static int Delete(int id, BrainUpBdContext context)
        {
            int result = -1;
            try
            {
                var parameters = new[] {
                new SqlParameter("@id", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = id
                    }
                };

                result = context.Database.ExecuteSqlRaw(
                  "exec [dbo].[DeleteLevel] @id", parameters:
                  parameters);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return result;
        }

        public static int Update(Level level, BrainUpBdContext context)
        {
            var courceId = -1;
            try
            {
                var parameters = new[] {
                     new SqlParameter("@id", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = level.Id
                    },

                new SqlParameter("@name", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = level.Name
                    }
                };

                var result = context.Database.ExecuteSqlRaw(
                  "exec [dbo].[UpdateLevel] @id,@name", parameters:
                  parameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return courceId;
        }

        public static List<Level>? GetAllLevels(BrainUpBdContext context)
        {
            try
            {

                var result = context.Levels.FromSqlRaw(
                  $"exec [dbo].[GetAllLevels] ");

                return result.ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null;
        }
    }
}
