using BrainUp.Data;
using BrainUp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace BrainUp.StoredProcedure
{
    public class CourceAction
    {
        public static int Add(Cource cource, BrainUpBdContext context)
        {
            int courceId = -1;
            try
            {
                var parameters = new[] {
                    new SqlParameter("@name", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = cource.Name
                    },
                      new SqlParameter("@start", SqlDbType.DateTime)
                    {
                      Direction = ParameterDirection.Input,
                      Value = cource.StartDate
                    },
                        new SqlParameter("@end", SqlDbType.DateTime)
                    {
                      Direction = ParameterDirection.Input,
                      Value = cource.EndDate
                    },
                     new SqlParameter("@shortDisc", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = cource.ShotDiscription
                    },
                    new SqlParameter("@disc", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = cource.Discription
                    },
                    new SqlParameter("@storageLink", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = cource.StorageLink
                    },
                     new SqlParameter("@image", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = cource.Image
                    },
                    new SqlParameter("@levelId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Output,
                      Value = cource.LevelId
                    },
                     new SqlParameter("@languageId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Output,
                      Value = cource.LanguageId
                    },
                      new SqlParameter("@categoryId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Output,
                      Value = cource.CategoryId
                    },
                        new SqlParameter("@price", SqlDbType.Float)
                    {
                      Direction = ParameterDirection.Output,
                      Value = cource.Price.Price1
                    },
                          new SqlParameter("@currency", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Output,
                      Value = cource.Price.CurrencySymbol
                    }

                };

                context.Database.ExecuteSqlRaw(
                  "exec [dbo].[CreateCource] @name,@start, @end, @shortDisc, @disc, @storageLink, @image" +
                  ", @levelId, @languageId, @categoryId, @price, @currency", parameters:
                  parameters);

                courceId = Convert.ToInt32(parameters[^1].Value);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return courceId;
        }

        public static List<Cource>? GetAllCources(int courceId, BrainUpBdContext context)
        {
            try
            {

                var result = context.Cources.FromSqlRaw(
                  $"exec [dbo].[GetAllCoursies]");

                return result.ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null;
        }

        public static List<Cource>? GetAllCourcesByCategory(int categoryId, BrainUpBdContext context)
        {
            try
            {
                var parameters = new[] {
                    new SqlParameter("@categoryId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = categoryId
                    }
                };

                var result = context.Cources.FromSqlRaw(
                  $"exec [dbo].[GetAllCoursies] @categoryId");

                return result.ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null;
        }

        public static List<Cource>? GetAllCourcesByUserEmail(string email, BrainUpBdContext context)
        {
            try
            {
                var parameters = new[] {
                    new SqlParameter("@UserEmail", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = email
                    }
                };

                var result = context.Cources.FromSqlRaw(
                  $"exec [dbo].[GetAllCoursies] @categoryId");

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
