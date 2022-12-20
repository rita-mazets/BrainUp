using BrainUp.Data;
using BrainUp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using Task = System.Threading.Tasks.Task;

namespace BrainUp.StoredProcedure
{
    public static class MenuAction
    {
        public static int Add(Menu menu, BrainUpBdContext context)
        {
            try
            {
                var parameters = new[] {
            new SqlParameter("@courceId", SqlDbType.Int)
                {
                  Direction = ParameterDirection.Input,
                  Value = menu.CourceId
                },
                new SqlParameter("@name", SqlDbType.NVarChar)
                {
                  Direction = ParameterDirection.Input,
                  Value = menu.Name
                },
                new SqlParameter("@id", SqlDbType.Int)
                {
                  Direction = ParameterDirection.Output,
                  Value = 0
                }

            };

                context.Database.ExecuteSqlRaw(
                  "exec [dbo].[CreateMenu] @courceId,@name,@id out", parameters:
                  parameters);

                var newPrimaryKey = Convert.ToInt32(parameters[2].Value);
                return newPrimaryKey;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return -1;
        }

        public static int Delete(int id, BrainUpBdContext context)
        {
            try
            {
                var parameters = new[] {
            new SqlParameter("@id", SqlDbType.Int)
                {
                  Direction = ParameterDirection.Input,
                  Value = id
                },
                new SqlParameter("@flag", SqlDbType.Bit)
                {
                  Direction = ParameterDirection.Output,
                  Value = 0
                }

            };

                context.Database.ExecuteSqlRaw(
                  "exec [dbo].[DeleteMenu] @id, @flag out", parameters:
                  parameters);

                var isSuccessfull = Convert.ToInt32(parameters[1].Value);
                return isSuccessfull;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return 0;
        }

        public static int Update(Menu menu, BrainUpBdContext context)
        {
            try
            {
                var parameters = new[] {
                new SqlParameter("@id", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = menu.Id
                    },
                    new SqlParameter("@name", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = menu.Name
                    },
                    new SqlParameter("@flag", SqlDbType.Bit)
                    {
                      Direction = ParameterDirection.Output,
                      Value = 0
                    }
                };

                context.Database.ExecuteSqlRaw(
                  "exec [dbo].[UpdateMenu] @id,@name,@flag out", parameters:
                  parameters);

                var result = Convert.ToInt32(parameters[2].Value);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return 0;
        }

        public static async Task<IEnumerable<Menu>> ReadAll(int courceId, BrainUpBdContext context)
        {
            var courceIdPar = new SqlParameter("@courceId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = courceId
            };

            return await Task.Run(async () =>
            {
                var result = context.Database.SqlQueryRaw<Menu>("[dbo].[ReadAllMenuForCource] @courceId", courceIdPar);
               
                return result;
            });

        }
    }
}
