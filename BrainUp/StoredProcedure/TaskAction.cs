using BrainUp.Data;
using BrainUp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using Task = BrainUp.Models.Task;

namespace BrainUp.StoredProcedure
{
    public class TaskAction
    {
        public static int Add(Task task, BrainUpBdContext context)
        {
            int courceId = -1;
            try
            {
                var parameters = new[] {
                new SqlParameter("@submenuId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = task.SubMenuId
                    },
                    new SqlParameter("@name", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = task.Name
                    },
                     new SqlParameter("@image", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = task.Image
                    },
                    new SqlParameter("@condition", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = task.Condition
                    },
                    new SqlParameter("@point", SqlDbType.Float)
                    {
                      Direction = ParameterDirection.Input,
                      Value = task.Point
                    },
                    new SqlParameter("@courceId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Output,
                      Value = 0
                    }

                };

                context.Database.ExecuteSqlRaw(
                  "exec [dbo].[CreateTask] @submenuId,@name, @image, @condition, @point, @courceId out", parameters:
                  parameters);

                courceId = Convert.ToInt32(parameters[5].Value);
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
                  "exec [dbo].[DeleteTask] @id, @courceId out", parameters:
                  parameters);

                result = Convert.ToInt32(parameters[1].Value); ;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return result;
        }

        public static int Update(Task task, BrainUpBdContext context)
        {
            var courceId = -1;
            try
            {
                var parameters = new[] {
                    new SqlParameter("@id", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = task.Id
                    },
                    new SqlParameter("@name", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = task.Name
                    },
                    new SqlParameter("@image", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = task.Image
                    },
                    new SqlParameter("@condition", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = task.Condition
                    },
                    new SqlParameter("@point", SqlDbType.Float)
                    {
                      Direction = ParameterDirection.Input,
                      Value = task.Point
                    },
                    new SqlParameter("@courceId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Output,
                      Value = 0
                    }
                };

                context.Database.ExecuteSqlRaw(
                  "exec [dbo].[UpdateTask] @id,@name,@image,@condition,@point,@courceId out", parameters:
                  parameters);

                courceId = Convert.ToInt32(parameters[5].Value);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return courceId;
        }

        public static List<MenuSubmenuTaskName>? ReadAllMenuWithSubmenuTaskForCource(int courceId, BrainUpBdContext context)
        {
            try
            {
                var parameters = new[] {
                    new SqlParameter("@courceId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = courceId
                    }
                };

                var result = context.MenuSubmenuTaskNames.FromSqlRaw(
                  $"exec [dbo].[ReadAllMenuWithSubmenuTaskForCource] @courceId", parameters: parameters);

                return result.ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null;
        }

        public static List<Task>? Read(int taskId, BrainUpBdContext context)
        {
            try
            {
                var parameters = new[] {
                    new SqlParameter("@id", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = taskId
                    }
                };

                var result = context.Tasks.FromSqlRaw(
                  $"exec [dbo].[ReadTask] @id", parameters: parameters);

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
