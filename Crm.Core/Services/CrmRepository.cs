using System;
using System.Collections.Generic;
using Crm.Core.Models;
using System.Data;
using System.Data.SqlClient;



namespace Crm.Core.Services
{
    public class CrmRepository : BaseData, ICrmRepository
    {
        public CrmRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        /*
         Server=localhost\SQLEXPRESS01;Database=master;Trusted_Connection=True;
         */

        #region GETS
        public IList<Permission> GetPermissions()
        {
            IList<Permission> permissions = new List<Permission>();
            try
            {
                using (this.Initialize())
                {

                    this.Connection.Open();

                    string spName = @"dbo.[GetPermissions]";

                    SqlCommand cmd = new SqlCommand(spName, this.Connection);

                    //SqlParameter param1 = new SqlParameter();
                    //param1.ParameterName = "@employeeID";
                    //param1.SqlDbType = SqlDbType.Int;
                    //param1.Value = int.Parse(args[0].ToString());                    
                    //cmd.Parameters.Add(param1);                   
                    //set the SqlCommand type to stored procedure and execute
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();


                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Permission permission = null;
                            permission = new Permission()
                            {
                                Id = Convert.ToInt32(reader["Id"].ToString()),
                                Name = reader["Name"].ToString(),
                                Enabled = Convert.ToBoolean(reader["Enabled"].ToString())
                            };
                            permissions.Add(permission);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                this.Connection.Close();
            }


            return permissions;
        }
        public IList<Customer> GetCustomers()
        {
            IList<Customer> customers = new List<Customer>();
            try
            {
                using (this.Initialize())
                {
                    this.Connection.Open();
                    string spName = @"dbo.[GetCustomers]";
                    SqlCommand cmd = new SqlCommand(spName, this.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Customer customer = null;
                            customer = new Customer()
                            {
                                CustomerNo = Convert.ToInt32(reader["CustomerNo"].ToString()),
                                CustomerName = reader["CustomerName"].ToString(),
                                CustomerSurname = reader["CustomerSurname"].ToString(),
                                Address = reader["Address"].ToString(),
                                Country = reader["Country"].ToString(),
                                DateofBirth = Convert.ToDateTime(reader["DateofBirth"].ToString()),
                                Enabled = Convert.ToBoolean(reader["Enabled"].ToString()),
                                PostCode = reader["PostCode"].ToString()
                            };
                            customers.Add(customer);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                this.Connection.Close();
            }
            return customers;
        }
        public IList<CustomerCall> GetCustomerCalls()
        {
            IList<CustomerCall> customerCalls = new List<CustomerCall>();
            try
            {
                using (this.Initialize())
                {
                    this.Connection.Open();
                    string spName = @"dbo.[GetCustomerCalls]";
                    SqlCommand cmd = new SqlCommand(spName, this.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CustomerCall customerCall = null;
                            customerCall = new CustomerCall()
                            {
                                Id = new Guid(reader["Id"].ToString()),
                                Customer =  GetCustomer( Convert.ToInt32(reader["CustomerNo"].ToString())),
                                DateOfCall = Convert.ToDateTime(reader["DateOfCall"].ToString()),
                                TimeOfCall = TimeSpan.Parse(reader["TimeOfCall"].ToString()),
                                Subject = reader["Subject"].ToString(),
                                Description = reader["Description"].ToString(),
                                Enabled = Convert.ToBoolean(reader["Enabled"].ToString())
                            };


                            customerCalls.Add(customerCall);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                this.Connection.Close();
            }
            return customerCalls;
        }
        public IList<User> GetUsers()
        {
            IList<User> users = new List<User>();
            try
            {
                using (this.Initialize())
                {
                    this.Connection.Open();
                    string spName = @"dbo.[GetUsers]";
                    SqlCommand cmd = new SqlCommand(spName, this.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            User user = null;
                            user = new User()
                            {
                                UserName = reader["UserName"].ToString(),
                                Password = reader["Password"].ToString(),
                                Permission = GetPermission( Convert.ToInt32(reader["permissionId"].ToString())),
                                Enabled = Convert.ToBoolean(reader["Enabled"].ToString())
                            };
                            users.Add(user);
                        }
                    } else
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                this.Connection.Close();
            }
            return users;
        }
        #endregion

        #region GET_WITH_ID
        public Permission GetPermission(int id)
        {
            Permission permission = null;

            try
            {
                using (this.Initialize())
                {
                    this.Connection.Open();
                    string spName = @"dbo.[GetPermission]";
                    SqlCommand cmd = new SqlCommand(spName, this.Connection);

                    SqlParameter param1 = new SqlParameter();
                    param1.ParameterName = "@Id";
                    param1.SqlDbType = SqlDbType.Int;
                    param1.Value = id;
                    cmd.Parameters.Add(param1);

                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();


                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            permission = new Permission()
                            {
                                Id = Convert.ToInt32(reader["Id"].ToString()),
                                Name = reader["Name"].ToString(),
                                Enabled = Convert.ToBoolean(reader["Enabled"].ToString())
                            };
                        }
                    }
                    else
                        return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                this.Connection.Close();
            }

            return permission;
        }
        public Customer GetCustomer(int customerNo)
        {
            var customer = new Customer();
            try
            {
                using (this.Initialize())
                {
                    this.Connection.Open();
                    string spName = @"dbo.[GetCustomer]";
                    SqlCommand cmd = new SqlCommand(spName, this.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param1 = new SqlParameter();
                    param1.ParameterName = "@CustomerNo";
                    param1.SqlDbType = SqlDbType.Int;
                    param1.Value = customerNo;
                    cmd.Parameters.Add(param1);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            customer = new Customer()
                            {
                                CustomerNo = Convert.ToInt32(reader["CustomerNo"].ToString()),
                                CustomerName = reader["CustomerName"].ToString(),
                                CustomerSurname = reader["CustomerSurname"].ToString(),
                                Address = reader["Address"].ToString(),
                                Country = reader["Country"].ToString(),
                                DateofBirth = Convert.ToDateTime(reader["DateofBirth"].ToString()),
                                PostCode = reader["PostCode"].ToString(),
                                Enabled = Convert.ToBoolean(reader["Enabled"].ToString())
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                this.Connection.Close();
            }
            return customer;
        }
        public CustomerCall GetCustomerCall(Guid id)
        {
            var customerCall = new CustomerCall();

            try
            {
                using (this.Initialize())
                {
                    this.Connection.Open();
                    string spName = @"dbo.[GetCustomerCall]";
                    SqlCommand cmd = new SqlCommand(spName, this.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param1 = new SqlParameter();
                    param1.ParameterName = "@Id";
                    param1.SqlDbType = SqlDbType.UniqueIdentifier;
                    param1.Value = id;
                    cmd.Parameters.Add(param1);


                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            customerCall = new CustomerCall()
                            {
                                Id = new Guid(reader["Id"].ToString()),
                                Customer = GetCustomer(Convert.ToInt32(reader["CustomerNo"].ToString())),
                                DateOfCall = Convert.ToDateTime(reader["DateOfCall"].ToString()),
                                TimeOfCall = TimeSpan.Parse(reader["TimeOfCall"].ToString()),
                                Subject = reader["Subject"].ToString(),
                                Description = reader["Description"].ToString(),
                                Enabled = Convert.ToBoolean(reader["Enabled"].ToString())
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                this.Connection.Close();
            }
            return customerCall;
        }
        public User GetUser(string userName)
        {
            var user = new User();

            try
            {
                using (this.Initialize())
                {
                    this.Connection.Open();
                    string spName = @"dbo.[GetUser]";
                    SqlCommand cmd = new SqlCommand(spName, this.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param1 = new SqlParameter();
                    param1.ParameterName = "@UserName";
                    param1.SqlDbType = SqlDbType.NVarChar;
                    param1.Value = userName;
                    cmd.Parameters.Add(param1);


                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            user = new User()
                            {
                                UserName = reader["UserName"].ToString(),
                                Password = reader["Password"].ToString(),
                                Permission = GetPermission(Convert.ToInt32(reader["permissionId"].ToString())),
                                Enabled = Convert.ToBoolean(reader["Enabled"].ToString())
                            };
                        }
                    }
                    else
                        return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                this.Connection.Close();
            }
            return user;
        }
        #endregion
        #region PUTS
        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                using (this.Initialize())
                {
                    this.Connection.Open();
                    string spName = @"dbo.[UpdateCustomer]";
                    SqlCommand cmd = new SqlCommand(spName, this.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@CustomerNo", customer.CustomerNo));
                    cmd.Parameters.Add(new SqlParameter("@CustomerName", customer.CustomerName));
                    cmd.Parameters.Add(new SqlParameter("@CustomerSurname", customer.CustomerSurname));
                    cmd.Parameters.Add(new SqlParameter("@DateofBirth", customer.DateofBirth));
                    cmd.Parameters.Add(new SqlParameter("@Address", customer.Address));
                    cmd.Parameters.Add(new SqlParameter("@Country", customer.Country));
                    cmd.Parameters.Add(new SqlParameter("@PostCode", customer.PostCode));
                    cmd.Parameters.Add(new SqlParameter("@Enabled", customer.Enabled));



                    cmd.ExecuteNonQuery();


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                this.Connection.Close();
            }
            return true;
        }

        public bool UpdateCustomerCall(CustomerCall customerCall)
        {
            try
            {
                using (this.Initialize())
                {
                    this.Connection.Open();
                    string spName = @"dbo.[UpdateCustomerCall]";
                    SqlCommand cmd = new SqlCommand(spName, this.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@CustomerNo", customerCall.Customer.CustomerNo));
                    cmd.Parameters.Add(new SqlParameter("@DateOfCall", customerCall.DateOfCall));
                    cmd.Parameters.Add(new SqlParameter("@Description", customerCall.Description));
                    cmd.Parameters.Add(new SqlParameter("@Enabled", customerCall.Enabled));
                    cmd.Parameters.Add(new SqlParameter("@Id", customerCall.Id));
                    cmd.Parameters.Add(new SqlParameter("@Subject", customerCall.Subject));
                    cmd.Parameters.Add(new SqlParameter("@TimeOfCall", customerCall.TimeOfCall));

                    cmd.ExecuteNonQuery();


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                this.Connection.Close();
            }
            return true;
        }

        #endregion
        #region POSTS
        public Customer InsertCustomer(Customer customer)
        {
            int customerNo = new int();
            try
            {
                using (this.Initialize())
                {
                    this.Connection.Open();
                    string spName = @"dbo.[InsertCustomer]";
                    SqlCommand cmd = new SqlCommand(spName, this.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //cmd.Parameters.Add(new SqlParameter("@CustomerNo", customer.CustomerNo));
                    cmd.Parameters.Add(new SqlParameter("@CustomerName", customer.CustomerName));
                    cmd.Parameters.Add(new SqlParameter("@CustomerSurname", customer.CustomerSurname));
                    cmd.Parameters.Add(new SqlParameter("@DateofBirth", customer.DateofBirth));
                    cmd.Parameters.Add(new SqlParameter("@Address", customer.Address));
                    cmd.Parameters.Add(new SqlParameter("@Country", customer.Country));
                    cmd.Parameters.Add(new SqlParameter("@PostCode", customer.PostCode));
                    // cmd.Parameters.Add(new SqlParameter("@Enabled", customer.Enabled));

                    SqlDataReader reader = cmd.ExecuteReader();


                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            customer.CustomerNo = Convert.ToInt32(reader["CustomerNo"].ToString());
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                this.Connection.Close();
            }
            return customer;
        }

        public CustomerCall InsertCustomerCall(CustomerCall customerCall)
        {
            Guid id = new Guid();
            try
            {
                using (this.Initialize())
                {
                    this.Connection.Open();
                    string spName = @"dbo.[InsertCustomerCall]";
                    SqlCommand cmd = new SqlCommand(spName, this.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@CustomerNo", customerCall.Customer.CustomerNo));
                    cmd.Parameters.Add(new SqlParameter("@DateOfCall", customerCall.DateOfCall));
                    cmd.Parameters.Add(new SqlParameter("@Description", customerCall.Description));
                    //  cmd.Parameters.Add(new SqlParameter("@Enabled", customerCall.Enabled));
                    //  cmd.Parameters.Add(new SqlParameter("@Id", customerCall.Id));
                    cmd.Parameters.Add(new SqlParameter("@Subject", customerCall.Subject));
                    cmd.Parameters.Add(new SqlParameter("@TimeOfCall", customerCall.TimeOfCall));

                    SqlDataReader reader = cmd.ExecuteReader();


                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            customerCall.Id = new Guid(reader["Id"].ToString());
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                this.Connection.Close();
            }
            return customerCall;
        }

        #endregion


    }

}
