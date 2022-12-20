using BrainUp.Data;
using BrainUp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace BrainUp.StoredProcedure
{
    public static class CategoryAction
    {
        public static int Add(Category category, BrainUpBdContext context)
        {
            int courceId = -1;
            try
            {
                var parameters = new[] {
                    new SqlParameter("@name", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = category.Name
                    }

                };

                var result = context.Database.ExecuteSqlRaw(
                  "exec [dbo].[CreateCategory] @name", parameters:
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
                  "exec [dbo].[DeleteCategory] @id", parameters:
                  parameters);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return result;
        }

        public static int Update(Category category, BrainUpBdContext context)
        {
            var courceId = -1;
            try
            {
                var parameters = new[] {
                     new SqlParameter("@id", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = category.Id
                    },
           
                new SqlParameter("@name", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = category.Name
                    }
                };

                var result = context.Database.ExecuteSqlRaw(
                  "exec [dbo].[UpdateCategory] @id,@name", parameters:
                  parameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return courceId;
        }

        public static List<Category>? GetAllCategories(BrainUpBdContext context)
        {
            try
            {

                var result = context.Categories.FromSqlRaw(
                  $"exec [dbo].[GetAllCategories] ");

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
