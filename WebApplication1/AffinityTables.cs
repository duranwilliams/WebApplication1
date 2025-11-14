// files: https://catalog.data.gov/dataset?q=&sort=metadata_created+desc
// This thing will read the files and store what is necessary.


using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics.CodeAnalysis;

internal class AffinityTables
{
    public static List<String>? row { get; set; }

    [RequiresUnreferencedCode("Calls System.Reflection.MethodBase.GetCurrentMethod()")]
    internal static void spClearMainTable()
    {
        using (var conn = new SqlConnection(Environment.GetEnvironmentVariable("AffinityTablesConnStr")))
        {
            using (var command = new SqlCommand(System.Reflection.MethodBase.GetCurrentMethod().Name, conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {

                conn.Open();

                var rowArray = row.ToArray();

                command.ExecuteNonQuery();
            }
        }
    }

    [RequiresUnreferencedCode("Calls System.Reflection.MethodBase.GetCurrentMethod()")]
    internal static void spInsertGeneralDataRow(List<string> row)
    {
        using (var conn = new SqlConnection(Environment.GetEnvironmentVariable("AffinityTablesConnStr")))
        {
            using (var command = new SqlCommand(System.Reflection.MethodBase.GetCurrentMethod().Name, conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                
                conn.Open();

                var rowArray = row.ToArray();
                command.Parameters.AddWithValue("pFileName", rowArray[0]);                    
                command.Parameters.AddWithValue("pLink", rowArray[1]);
                command.Parameters.AddWithValue("pField01text", rowArray[2]);
                command.Parameters.AddWithValue("pField01comment", rowArray[3]);
                command.Parameters.AddWithValue("pField02text", rowArray[4]);
                command.Parameters.AddWithValue("pField02comment", rowArray[5]);

                command.ExecuteNonQuery();
            }
        }
    }
}