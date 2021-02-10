using PGMCLIP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PGMCLIP.AccesoDatos
{
    public class UsuarioAD
    {
        public static bool IniciarSesion(string usuario, string contraseña)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT usuario, contraseña FROM Usuario WHERE usuario = @usuario AND contraseña = @contraseña";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@contraseña", contraseña);

                cmd.CommandType = System.Data.CommandType.Text; //para que lo pueda leer sql
                cmd.CommandText = consulta;



                cn.Open(); //abro la conexión y le paso la consulta
                cmd.Connection = cn;
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    resultado = true;
                }
            }


            catch (Exception)
            {
                throw;
            }
            finally
            {
                cn.Close();

            }

            return resultado;

        }
  
        public static bool Registrar(User usuario)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "INSERT INTO Usuario VALUES(@usuario, @nombre, @apellido, @direccion, @telefono, @mail, @contraseña)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@usuario", usuario.usuario);
                cmd.Parameters.AddWithValue("@nombre", usuario.nombre);
                cmd.Parameters.AddWithValue("@apellido", usuario.apellido);
                cmd.Parameters.AddWithValue("@direccion", usuario.direccion);
                cmd.Parameters.AddWithValue("@telefono", usuario.telefono);
                cmd.Parameters.AddWithValue("@mail", usuario.mail);
                cmd.Parameters.AddWithValue("@contraseña", usuario.contraseña);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;


                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                resultado = true;


            }
            catch (Exception)
            { throw; }
            finally
            {
                cn.Close();

            }

            return resultado;
        }      
      

        public static List<User> Consultar()
        {
            List<User> resultado = new List<User>();

            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT id_usuario, usuario, nombre, apellido, direccion, telefono, mail, contraseña FROM Usuario";
                cmd.Parameters.Clear();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;


                cn.Open();
                cmd.Connection = cn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        User y = new User();
                        y.id_usuario = int.Parse( dr["id_usuario"].ToString());
                        y.usuario = dr["Usuario"].ToString();
                        y.nombre = dr["Nombre"].ToString();
                        y.apellido = dr["Apellido"].ToString();
                        y.direccion = dr["Direccion"].ToString();
                        y.telefono = dr["Telefono"].ToString();
                        y.mail = dr["Mail"].ToString();
                        resultado.Add(y);
                    }
                }
            }
            catch (Exception)
            { throw; }
            finally
            {
                cn.Close();

            }

            return resultado;
        }
        public static User updateUser(int id_usuario)
        {
            User resultado = new User();

            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM Usuario WHERE id_usuario= @id_usuario";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id_usuario", id_usuario);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;


                cn.Open();
                cmd.Connection = cn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        resultado.id_usuario = int.Parse (dr["id_usuario"].ToString());
                        resultado.usuario = dr["Usuario"].ToString();
                        resultado.nombre = dr["Nombre"].ToString();
                        resultado.apellido = dr["Apellido"].ToString();
                        resultado.direccion = dr["Direccion"].ToString();
                        resultado.telefono = dr["Telefono"].ToString();
                        resultado.mail = dr["Mail"].ToString();
                        resultado.contraseña = dr["Contraseña"].ToString();

                    }
                }
            }
            catch (Exception)
            { throw; }
            finally
            {
                cn.Close();

            }

            return resultado;
        }//bool para que confirme si se pudo hacer
        public static bool Actualizar(User usuario)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "UPDATE Usuario set Usuario= @usuario, @Nombre= @nombre, Apellido= @apellido, Direccion= @direccion, Telefono= @telefono, Mail= @mail, Contraseña= @contraseña WHERE id_usuario=@id_usuario";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@usuario", usuario.usuario);
                cmd.Parameters.AddWithValue("@nombre", usuario.nombre);
                cmd.Parameters.AddWithValue("@apellido", usuario.apellido);
                cmd.Parameters.AddWithValue("@direccion", usuario.direccion);
                cmd.Parameters.AddWithValue("@telefono", usuario.telefono);
                cmd.Parameters.AddWithValue("@mail", usuario.mail);
                cmd.Parameters.AddWithValue("@contraseña", usuario.contraseña);
                cmd.Parameters.AddWithValue("@id_usuario", usuario.id_usuario);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;


                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                resultado = true;


            }
            catch (Exception)
            { throw; }
            finally
            {
                cn.Close();

            }

            return resultado;
        }
      
            public static bool Eliminar(User usuario)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "DELETE FROM Usuario WHERE id_usuario = @id_usuario";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id_usuario", usuario.id_usuario);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;


                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                resultado = true;


            }
            catch (Exception)
            { throw; }
            finally
            {
                cn.Close();

            }

            return resultado;
        }

    }
}

