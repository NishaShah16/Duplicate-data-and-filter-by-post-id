using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace DiplicateReport.Models
{
    public class DataClass
    {
        public string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


        public List<Duplicate> GetAllCandidates()
        {
            List<Duplicate> duplicates = new List<Duplicate>();

            using (SqlConnection connection = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("GetAllCandidates", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@post", DBNull.Value);


                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    duplicates.Add(new Duplicate
                    {
                        id = reader.GetInt32(0),
                        IndexNo = reader.GetInt32(1),
                        RegistrationNo = reader.GetString(2),
                        CandidateName = reader.GetString(3),
                        PhotoPath = reader.GetString(4),
                        //post = reader.GetString(5),
                        //post_Id = reader.GetInt32(6)
                    });
                }
                reader.Close();
            }
            return duplicates;
        }
        public List<Duplicate> GetCandidatesByPost(string post)
        {
            List<Duplicate> duplicates = new List<Duplicate>();

            using (SqlConnection connection = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllDuplicateCandidates", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Pass DBNull.Value for @Post to return all records.
                    cmd.Parameters.AddWithValue("@post", post);

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            duplicates.Add(new Duplicate
                            {
                                id = reader.GetInt32(0),
                                IndexNo = reader.GetInt32(1),
                                RegistrationNo = reader.GetString(2),
                                CandidateName = reader.GetString(3),
                                PhotoPath = reader.GetString(4),
                                post = reader.GetString(5),
                                post_Id = reader.GetInt32(6)
                            });
                        }
                    }
                }
            }
            return duplicates;
        }

    }
}