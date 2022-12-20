using BrainUp.Data;
using BrainUp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace BrainUp.StoredProcedure
{
    public class SubmenuAction
    {
        
        public static (int, int) Add(SubMenu submenu, BrainUpBdContext context)
        {
            int newPrimaryKey = -1;
            int courceId = -1;

            try
            {
                var parameters = new[] {
                new SqlParameter("@menuId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = submenu.MenuId
                    },
                    new SqlParameter("@name", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = submenu.Name
                    },
                    new SqlParameter("@id", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Output,
                      Value = 0
                    },
                    new SqlParameter("@courceId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Output,
                      Value = 0
                    }
                };

                context.Database.ExecuteSqlRaw(
                  "exec [dbo].[CreateSubmenu] @menuId,@name,@id out, @courceId out", parameters:
                  parameters);

                newPrimaryKey = Convert.ToInt32(parameters[2].Value);
                courceId = Convert.ToInt32(parameters[3].Value);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return (newPrimaryKey, courceId);
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
                },
                new SqlParameter("@courceId", SqlDbType.Int)
                {
                  Direction = ParameterDirection.Output,
                  Value = 0
                }

            };

                context.Database.ExecuteSqlRaw(
                  "exec [dbo].[DeleteMenu] @id, @courceId out", parameters:
                  parameters);

                result = Convert.ToInt32(parameters[1].Value);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return result;
        }

        public static int Update(SubMenu submenu, BrainUpBdContext context)
        {
            int newPrimaryKey = -1;
            int courceId = -1;

            try
            {
                var parameters = new[] {
                    new SqlParameter("@name", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = submenu.Name
                    },
                    new SqlParameter("@id", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = submenu.Id
                    },
                    new SqlParameter("@courceId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Output,
                      Value = 0
                    }
                };

                context.Database.ExecuteSqlRaw(
                  "exec [dbo].[UpdateSubmenu] @name,@id , @courceId out", parameters:
                  parameters);

                courceId = Convert.ToInt32(parameters[2].Value);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return courceId;
        }
    }
}
