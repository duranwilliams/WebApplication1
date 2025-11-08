// files: https://catalog.data.gov/dataset?q=&sort=metadata_created+desc
// This thing will read the files and store what is necessary.


using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics.CodeAnalysis;

internal class AffinityLogger
{
    [RequiresUnreferencedCode("Calls System.Reflection.MethodBase.GetCurrentMethod()")]
    internal static void CaptureLog(string v)
    {
        using (var conn = new SqlConnection(Environment.GetEnvironmentVariable("AffinityTablesConnStr")))
        {
            using (var command = new SqlCommand(System.Reflection.MethodBase.GetCurrentMethod().Name, conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                
                conn.Open();
                command.Parameters.AddWithValue("pFileName", v);                    

                command.ExecuteNonQuery();
            }
        }
    }
         
    
}