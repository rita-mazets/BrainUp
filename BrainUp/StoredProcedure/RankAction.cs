using BrainUp.Data;
using BrainUp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Diagnostics;

namespace BrainUp.StoredProcedure
{
    public class RankAction
    {
        public static int Add(Rank rank, BrainUpBdContext context)
        {
            int courceId = -1;
            try
            {
                var parameters = new[] {
                    new SqlParameter("@courceId", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = rank.CourceId
                    },
                    new SqlParameter("@createdDate", SqlDbType.DateTime)
                    {
                      Direction = ParameterDirection.Input,
                      Value = rank.CreatedDate
                    },
                    new SqlParameter("@value", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = rank.Value
                    }
                };

                var result = context.Database.ExecuteSqlRaw(
                  "exec [dbo].[CreateFeedback] @courceId, @createdDate, @value", parameters:
                  parameters);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return courceId;
        }



        public static List<Feedback>? GetFeedbacksForCourse(int courceId, BrainUpBdContext context)
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

                var result = context.Feedbacks.FromSqlRaw(
                  $"exec [dbo].[GetRanksForCource] courceId");

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
