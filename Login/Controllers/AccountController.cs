using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login.Models;
using System.Data.SqlClient;

namespace Login.Controllers
{
    public class AccountController : Controller
    {
        //Aqui se generan las intancias para hacer la coneccion con la base de datos y los comandos para interactuar con ella.
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        

        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        void ConnectionString()
        {
            con.ConnectionString = "data source = .; database = QUICKCHEF; Integrated Security = SSPI";
           
        }
        //Este metodo verifica las cuentas, tiene como referencia la clase Account.cs del folder de modelos.
        [HttpPost]
        public ActionResult Verify(Account account)
        {
            ConnectionString();
            con.Open();
            com.Connection = con;
            //ojo que el nombre de la columna esta mal escrito, dice nobreAdministrador!!!
            com.CommandText = "SELECT * FROM usuarios WHERE nobreAdministrador='"+ account.Name +"' AND contrasenaAdministrador='"+account.Password+"'";
            dr = com.ExecuteReader();
            
            if (dr.Read())
            {
                con.Close();
                return View("Create");
            }
            else
            {
                con.Close();
                string mensaje = "Usuario o Contrasena incorrectos";
                ViewData["msg"] = mensaje;
                
                return View("Login");

                // return View("Error");
            }
                                  
        }
    }
}