using BrainUp.Data;
using BrainUp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace BrainUp.StoredProcedure
{
    public class LanguageAction
    {
        public static int Add(Language language, BrainUpBdContext context)
        {
            int courceId = -1;
            try
            {
                var parameters = new[] {
                    new SqlParameter("@name", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = language.Name
                    }

                };

                var result = context.Database.ExecuteSqlRaw(
                  "exec [dbo].[CreateLanguage] @name", parameters:
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
                  "exec [dbo].[DeleteLanguage] @id", parameters:
                  parameters);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return result;
        }

        public static int Update(Language language, BrainUpBdContext context)
        {
            var courceId = -1;
            try
            {
                var parameters = new[] {
                     new SqlParameter("@id", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = language.Id
                    },

                new SqlParameter("@name", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = language.Name
                    }
                };

                var result = context.Database.ExecuteSqlRaw(
                  "exec [dbo].[UpdateLanguage] @id,@name", parameters:
                  parameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return courceId;
        }

        public static List<Language>? GetAllLanguages(BrainUpBdContext context)
        {
            try
            {

                var result = context.Languages.FromSqlRaw(
                  $"exec [dbo].[GetAllLanguages]");

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
