using BrainUp.Data;
using BrainUp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace BrainUp.StoredProcedure
{
    public static class ContentAction
    {
        public static int Add(Content content, BrainUpBdContext context)
        {
            int courceId = -1;
            try
            {
                var parameters = new[] {
                new SqlParameter("@submenuId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = content.SubMenuId
                    },
                    new SqlParameter("@name", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = content.Name
                    },
                    new SqlParameter("@text", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = content.Text
                    },
                    new SqlParameter("@courceId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Output,
                      Value = 0
                    }

                };

                context.Database.ExecuteSqlRaw(
                  "exec [dbo].[CreateContent] @submenuId,@name,@text,@courceId out", parameters:
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
                  "exec [dbo].[DeleteContent] @id, @courceId out", parameters:
                  parameters);

                result = Convert.ToInt32(parameters[1].Value);;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return result;
        }

        public static int Update(Content content, BrainUpBdContext context)
        {
            var courceId = -1;
            try
            {
                var parameters = new[] {
                    new SqlParameter("@id", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = content.Id
                    },
                    new SqlParameter("@name", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = content.Name
                    },
                    new SqlParameter("@text", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = content.Text
                    },
                    new SqlParameter("@courceId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Output,
                      Value = 0
                    }

                };

                context.Database.ExecuteSqlRaw(
                  "exec [dbo].[UpdateContent] @id,@name,@text,@courceId out", parameters:
                  parameters);

                courceId = Convert.ToInt32(parameters[3].Value);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return courceId;
        }

        public static List<MenuSubmenuContentName>? ReadAllMenuWithSubmenuContentForCource(int courceId, BrainUpBdContext context)
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

                var result = context.MenuSubmenuContentNames.FromSqlRaw(
                  $"exec [dbo].[ReadAllMenuWithSubmenuContentForCource] @courceId", parameters: parameters);

                return result.ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null;
        }

        public static List<Content>? ReadContent(int contentId, BrainUpBdContext context)
        {
            try
            {
                var parameters = new[] {
                    new SqlParameter("@contentId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = contentId
                    }
                };

                var result = context.Contents.FromSqlRaw(
                  $"exec [dbo].[ReadContent] @contentId", parameters: parameters);

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
