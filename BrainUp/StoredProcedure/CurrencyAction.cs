using BrainUp.Data;
using BrainUp.Models;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace BrainUp.StoredProcedure
{
    public class CurrencyAction
    {
        public static int Delete(string symbol, BrainUpBdContext context)
        {
            int result = -1;
            try
            {
                var parameters = new[] {
                new SqlParameter("@symbol", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = symbol
                    }
                };

                result = context.Database.ExecuteSqlRaw(
                  "exec [dbo].[DeleteCurrency] @id", parameters:
                  parameters);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return result;
        }

        public static int Update(Currency currency, BrainUpBdContext context)
        {
            var courceId = -1;
            try
            {
                var parameters = new[] {
                     new SqlParameter("@symbol", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = currency.Symbol
                    },

                new SqlParameter("@name", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = currency.Name
                    },
                new SqlParameter("@usdEquivalent", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = currency.Name
                    }
                };

                var result = context.Database.ExecuteSqlRaw(
                  "exec [dbo].[UpdateCurrency] @symbol,@name, @usdEquivalent", parameters:
                  parameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return courceId;
        }

        public static List<Currency>? GetAllCurrencies(BrainUpBdContext context)
        {
            try
            {

                var result = context.Currencies.FromSqlRaw(
                  $"exec [dbo].[GetAllCategories]");

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
