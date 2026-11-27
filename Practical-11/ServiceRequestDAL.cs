using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using StudentCampusPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace StudentCampusPortal.DAL
{
    public class ServiceRequestDAL
    {
        private readonly string _connectionString;

        public ServiceRequestDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CampusConnection")
                ?? "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SmartCampusDB;Integrated Security=True;TrustServerCertificate=True";
        }

        public List<ServiceRequest> GetAll()
        {
            var list = new List<ServiceRequest>();
            using (var con = new SqlConnection(_connectionString))
            {
                const string query = "SELECT RequestId, StudentName, Department, ServiceType, Description, Priority, Status, RequestDate FROM ServiceRequests ORDER BY RequestId DESC";
                using (var cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new ServiceRequest
                            {
                                RequestId = Convert.ToInt32(dr["RequestId"]),
                                StudentName = dr["StudentName"]?.ToString() ?? string.Empty,
                                Department = dr["Department"]?.ToString() ?? string.Empty,
                                ServiceType = dr["ServiceType"]?.ToString() ?? string.Empty,
                                Description = dr["Description"]?.ToString() ?? string.Empty,
                                Priority = dr["Priority"]?.ToString() ?? "Medium",
                                Status = dr["Status"]?.ToString() ?? "Pending",
                                RequestDate = dr["RequestDate"] != DBNull.Value ? Convert.ToDateTime(dr["RequestDate"]) : DateTime.Now
                            });
                        }
                    }
                }
            }
            return list;
        }

        public ServiceRequest? GetById(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                const string query = "SELECT RequestId, StudentName, Department, ServiceType, Description, Priority, Status, RequestDate FROM ServiceRequests WHERE RequestId = @RequestId";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@RequestId", id);
                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new ServiceRequest
                            {
                                RequestId = Convert.ToInt32(dr["RequestId"]),
                                StudentName = dr["StudentName"]?.ToString() ?? string.Empty,
                                Department = dr["Department"]?.ToString() ?? string.Empty,
                                ServiceType = dr["ServiceType"]?.ToString() ?? string.Empty,
                                Description = dr["Description"]?.ToString() ?? string.Empty,
                                Priority = dr["Priority"]?.ToString() ?? "Medium",
                                Status = dr["Status"]?.ToString() ?? "Pending",
                                RequestDate = dr["RequestDate"] != DBNull.Value ? Convert.ToDateTime(dr["RequestDate"]) : DateTime.Now
                            };
                        }
                    }
                }
            }
            return null;
        }

        public int Insert(ServiceRequest request)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                const string query = @"
                    INSERT INTO ServiceRequests
                    (StudentName, Department, ServiceType, Description, Priority, Status, RequestDate)
                    VALUES
                    (@StudentName, @Department, @ServiceType, @Description, @Priority, @Status, @RequestDate);
                    SELECT SCOPE_IDENTITY();";

                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@StudentName", request.StudentName);
                    cmd.Parameters.AddWithValue("@Department", request.Department);
                    cmd.Parameters.AddWithValue("@ServiceType", request.ServiceType);
                    cmd.Parameters.AddWithValue("@Description", request.Description);
                    cmd.Parameters.AddWithValue("@Priority", string.IsNullOrEmpty(request.Priority) ? "Medium" : request.Priority);
                    cmd.Parameters.AddWithValue("@Status", string.IsNullOrEmpty(request.Status) ? "Pending" : request.Status);
                    cmd.Parameters.AddWithValue("@RequestDate", DateTime.Now);

                    con.Open();
                    var result = cmd.ExecuteScalar();
                    return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                }
            }
        }

        public bool UpdateStatus(int id, string status)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                const string query = @"
                    UPDATE ServiceRequests
                    SET Status = @Status
                    WHERE RequestId = @RequestId";

                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@RequestId", id);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }

        public bool Delete(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                const string query = "DELETE FROM ServiceRequests WHERE RequestId = @RequestId";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@RequestId", id);
                    con.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }
    }
}
