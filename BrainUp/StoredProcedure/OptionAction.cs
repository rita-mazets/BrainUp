using BrainUp.Data;
using BrainUp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace BrainUp.StoredProcedure
{
    public static class OptionAction
    {
        public static int Add(Option option, BrainUpBdContext context)
        {
            int courceId = -1;
            try
            {
                var parameters = new[] {
                new SqlParameter("@taskId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = option.TaskId
                    },
                    new SqlParameter("@option", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = option.Option1
                    },
                     new SqlParameter("@isTrue", SqlDbType.Bit)
                    {
                      Direction = ParameterDirection.Input,
                      Value = option.IsTrue
                    },
                    new SqlParameter("@courceId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Output,
                      Value = 0
                    }

                };

                context.Database.ExecuteSqlRaw(
                  "exec [dbo].[CreateOption] @taskId, @option, @isTrue, @courceId out", parameters:
                  parameters);

                courceId = Convert.ToInt32(parameters[3].Value);
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
                    },
                    new SqlParameter("@courceId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Output,
                      Value = 0
                    }
                };

                context.Database.ExecuteSqlRaw(
                  "exec [dbo].[DeleteOption] @id, @courceId out", parameters:
                  parameters);

                result = Convert.ToInt32(parameters[1].Value); ;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return result;
        }

        public static int Update(Option option, BrainUpBdContext context)
        {
            var courceId = -1;
            try
            {
                var parameters = new[] {
                new SqlParameter("@id", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = option.TaskId
                    },
                    new SqlParameter("@option", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = option.Option1
                    },
                     new SqlParameter("@isTrue", SqlDbType.Bit)
                    {
                      Direction = ParameterDirection.Input,
                      Value = option.IsTrue
                    },
                    new SqlParameter("@courceId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Output,
                      Value = 0
                    }

                };

                context.Database.ExecuteSqlRaw(
                  "exec [dbo].[UpdateOption] @id,@option,@isTrue,@courceId out", parameters:
                  parameters);

                courceId = Convert.ToInt32(parameters[3].Value);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return courceId;
        }
    }
}
