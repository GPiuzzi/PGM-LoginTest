using PGMCLIP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PGMCLIP.Configuracion
{
    public class pdfConfig
    {
        public static List<tablaConfig> Configurar()
        {
            List<tablaConfig> resultado = new List<tablaConfig>();

            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT id_config, font, color, size FROM Config WHERE id_config=9";
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
                        tablaConfig y = new tablaConfig();
                        y.id_config = int.Parse(dr["id_config"].ToString());
                        y.font = bool.Parse(dr["Font"].ToString());
                        y.color = bool.Parse(dr["Color"].ToString());
                        y.size = bool.Parse(dr["Size"].ToString());

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
        public static tablaConfig updateConfig(int id_config)
        {
            tablaConfig resultado = new tablaConfig();

            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM Config WHERE id_config= 9";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id_config", id_config);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;


                cn.Open();
                cmd.Connection = cn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        resultado.id_config = int.Parse(dr["id_config"].ToString());
                        resultado.font = bool.Parse(dr["Font"].ToString());
                        resultado.color = bool.Parse(dr["Color"].ToString());
                        resultado.size = bool.Parse(dr["Size"].ToString());

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
        public static bool ActualizarConfig(tablaConfig x)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"].ToString();
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "UPDATE Config set Font= @font, Color = @color, Size = @size WHERE id_config = @id_config";
                cmd.Parameters.AddWithValue("@id_config", 9);
                cmd.Parameters.AddWithValue("@font", x.font);
                cmd.Parameters.AddWithValue("@color", x.color);
                cmd.Parameters.AddWithValue("@size", x.size);

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
       