using BrainUp.Data;
using BrainUp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace BrainUp.StoredProcedure
{
    public class FeedbackAction
    {
        public static int Add(Feedback feedback, BrainUpBdContext context)
        {
            int courceId = -1;
            try
            {
                var parameters = new[] {
                    new SqlParameter("@message", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = feedback.Message
                    },
                    new SqlParameter("@createdDate", SqlDbType.DateTime)
                    {
                      Direction = ParameterDirection.Input,
                      Value = feedback.CreatedDate
                    },
                    new SqlParameter("@courceId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = feedback.CourceId
                    }

                };

                var result = context.Database.ExecuteSqlRaw(
                  "exec [dbo].[CreateFeedback] @message, @createdDate, @courceId" , parameters:
                  parameters);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return courceId;
        }

        

        public static List<Feedback>? GetFeedbacksForCourse (int courceId, BrainUpBdContext context)
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
                  $"exec [dbo].[GetFeedbacksForCourse] @courceId");

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
