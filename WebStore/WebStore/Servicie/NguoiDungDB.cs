using CAIT.SQLHelper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using WedStore.Const;

namespace WedStore.Repositories
{
    public class NguoiDungDB
    {
        private readonly static string connectionString = ConnectStringValue.ConnectStringMyDB;

        public static List<NguoiDungDTO> GetAll()
        {
            object[] value = { };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Account_GetAll", value);
            List<NguoiDungDTO> lstResult = new List<NguoiDungDTO>();
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                foreach (DataRow dr in result.Rows)
                {
                    NguoiDungDTO account = new NguoiDungDTO();
                    account.UserName = dr["UserName"].ToString();
                    account.Password = dr["Password"].ToString();
                    account.FullName = dr["FullName"].ToString();
                    account.Age = string.IsNullOrEmpty(dr["Age"].ToString()) ? 0 : int.Parse(dr["Age"].ToString());
                    account.Gender = string.IsNullOrEmpty(dr["Gender"].ToString()) ? 0 : int.Parse(dr["Gender"].ToString());
                    account.Address = dr["Address"].ToString();
                    account.Email = dr["Email"].ToString();
                    account.Phone = "0" + dr["Phone"].ToString();
                    account.Authority = string.IsNullOrEmpty(dr["Authority"].ToString()) ? 0 : int.Parse(dr["Authority"].ToString());


                    lstResult.Add(account);
                }
            }
            return lstResult;
        }
        public static NguoiDungDTO CheckLogin(string UserName, string Password)
        {
            object[] value = { UserName };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Account_GetWithUser", value);
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                var acc = new NguoiDungDTO();
                acc.UserName = result.Rows[0]["UserName"].ToString();
                acc.Password = result.Rows[0]["Password"].ToString();
                bool isValidPassword = BCrypt.Net.BCrypt.Verify(Password, acc.Password);
                if (isValidPassword)
                {
                    acc.FullName = result.Rows[0]["FullName"].ToString();
                    acc.Authority = string.IsNullOrEmpty(result.Rows[0]["Authority"].ToString()) ? 0 : int.Parse(result.Rows[0]["Authority"].ToString());
                    return acc;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public static  NguoiDungDTO LoginUser(string UserName, string Password)
        {
           string userRole = ""; 
           NguoiDungDTO nd= new NguoiDungDTO();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_LOGIN", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", UserName);
                        command.Parameters.AddWithValue("@Password", Password);

                        SqlParameter userRoleParam = new SqlParameter("@UserRole", SqlDbType.VarChar, 10);
                        userRoleParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(userRoleParam);

                        SqlParameter fullNameParam = new SqlParameter("@FullName", SqlDbType.NVarChar, 255);
                        fullNameParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(fullNameParam);


						SqlParameter idUserParam = new SqlParameter("@idND", SqlDbType.VarChar, 50);
						idUserParam.Direction = ParameterDirection.Output;
						command.Parameters.Add(idUserParam);

						command.ExecuteNonQuery();

                        if (userRoleParam.Value == DBNull.Value)
                        {
                            return nd;
                        }

                        userRole = userRoleParam.Value.ToString();
                        string fullName= fullNameParam.Value.ToString();
                        string idUser = idUserParam.Value.ToString();
                        nd.idND= idUser;
                        nd.UserRole = userRole;
                        nd.UserName = UserName;
                        nd.Password=Password;
                        nd.FullName= fullName;
                        return nd;
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log the error (very important)
                Console.WriteLine($"Error during login: {ex.Message}");
                return nd;
               
            }
            catch (Exception ex)
            {
                //Log the error
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return nd;


            }
        }
        public static NguoiDungDTO GetAccountWithUser(string UserName)
        {
            object[] value = { UserName };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Account_GetWithUser", value);
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                var acc = new NguoiDungDTO();
                acc.UserName = result.Rows[0]["UserName"].ToString();
                acc.FullName = result.Rows[0]["FullName"].ToString();
                acc.Age = string.IsNullOrEmpty(result.Rows[0]["Age"].ToString()) ? 0 : int.Parse(result.Rows[0]["Age"].ToString());
                acc.Gender = string.IsNullOrEmpty(result.Rows[0]["Gender"].ToString()) ? 0 : int.Parse(result.Rows[0]["Gender"].ToString());
                acc.Email = result.Rows[0]["Email"].ToString();
                acc.Address = result.Rows[0]["Address"].ToString();
                int phone = string.IsNullOrEmpty(result.Rows[0]["Phone"].ToString()) ? 0 : int.Parse(result.Rows[0]["Phone"].ToString());
                acc.Phone = "0"+phone.ToString();
                acc.Authority = string.IsNullOrEmpty(result.Rows[0]["Authority"].ToString()) ? 0 : int.Parse(result.Rows[0]["Authority"].ToString());

                return acc;
            }
            return new NguoiDungDTO();
        }
        public static NguoiDungDTO GetAccountWithEmail(string Email)
        {
            object[] value = { Email };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Account_GetWithEmail", value);
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                var acc = new NguoiDungDTO();
                acc.FullName = result.Rows[0]["FullName"].ToString();
                acc.Email = result.Rows[0]["Email"].ToString();
                acc.Address = result.Rows[0]["Address"].ToString();
                int phone = string.IsNullOrEmpty(result.Rows[0]["Phone"].ToString()) ? 0 : int.Parse(result.Rows[0]["Phone"].ToString());
                acc.Phone = "0" + phone.ToString();
                return acc;
            }
            return new NguoiDungDTO();
        }
        public static bool Account_Create(NguoiDungDTO account)
        {
            object[] value = { account.UserName,account.Password,account.FullName,account.Age, 
                account.Gender,account.Address,account.Email,account.Phone,account.Authority };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Account_Create", value);

            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                return true;
            }
            return false;
        }
        public static bool Account_Update(NguoiDungDTO account)
        {
            object[] value = { account.UserName,account.FullName,account.Age,
                account.Gender,account.Address,account.Email,account.Phone,account.Authority };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Account_Update", value);

            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                return true;
            }
            return false;
        }
        public static bool Account_Delete(string userName)
        {
            object[] value = { userName };
            SQLCommand connection = new SQLCommand(ConnectStringValue.ConnectString);
            DataTable result = connection.Select("Account_Delete", value);

            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                return true;
            }
            return false;
        }
    }
}
