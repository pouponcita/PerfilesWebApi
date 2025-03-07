using Microsoft.AspNetCore.Mvc;
using PerfilesWebApi.Models;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PerfilesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        // GET: api/<DepartamentosController>
        [HttpGet("{incluirDeshabilitados}")]
        public IEnumerable<Departamento> Get(bool incluirDeshabilitados)
        {
            DataTable dt;
            DAL accesoDatosModel = new DAL();

            try
            {
                dt = accesoDatosModel.ExecuteQuery("[dbo].[spDepartamento_Listar] " + incluirDeshabilitados);
                if (dt.Rows.Count > 0)
                {
                    List<Departamento> lista = dt.AsEnumerable()
                     .Select(row => new Departamento
                     {
                         IdDepartamento = row.Field<Int16>(0),
                         Nombre = string.IsNullOrEmpty(row.Field<string>(1)) ? "" : row.Field<string>(1),
                         Habilitado = row.Field<bool>(2)
                     }).ToList();

                    return lista;
                }
                else
                {
                    return new List<Departamento>();
                }
            }
            catch (Exception e)
            {
                return (IEnumerable<Departamento>)BadRequest(e.Message);
            }

        }

        // GET api/<DepartamentosController>/5
        [HttpGet("{idDepartamento}")]
        public Departamento Get(int idDepartamento)
        {
            DataTable dt;
            DAL accesoDatosModel = new DAL();

            try
            {
                dt = accesoDatosModel.ExecuteQuery("[dbo].[spDepartamento_ObtenerRegistro] " + idDepartamento.ToString());
                if (dt.Rows.Count > 0)
                {
                    List<Departamento> lista = dt.AsEnumerable()
                     .Select(row => new Departamento
                     {
                         IdDepartamento = row.Field<Int16>(0),
                         Nombre = string.IsNullOrEmpty(row.Field<string>(1)) ? "" : row.Field<string>(1),
                         Habilitado = row.Field<bool>(2)
                     }).ToList();

                    return lista[0];
                }
                else
                {
                    return (Departamento)NotFound();
                }
            }
            catch (Exception e)
            {
                return (Departamento)BadRequest(e.Message);
            }

        }

        // POST api/<DepartamentosController>
        [HttpPost]
        [Route("insertarDepartamento")]
        public void Post([FromBody]Departamento value)
        {
            DAL accesoDatosModel = new DAL();

            try
            {
                int resultado = accesoDatosModel.ExecuteNonQuery("[dbo].[spDepartamento_Insertar] '" + value.Nombre + "'," + value.Habilitado.ToString());

            }
            catch (Exception e)
            {
                String mensaje = e.Message;
            }
        }

        // PUT api/<DepartamentosController>/5
        [HttpPut("{idDepartamento}")]
        public void Put(Int16 idDepartamento, [FromBody] Departamento value)
        {
            DAL accesoDatosModel = new DAL();

            try
            {
                int resultado = accesoDatosModel.ExecuteNonQuery("[dbo].[spDepartamento_Modificar] " + idDepartamento.ToString() + ",'" + value.Nombre + "'");

            }
            catch (Exception e)
            {
                String mensaje = e.Message;
            }
        }

        // PUT api/<DepartamentosController>/5/true
        [HttpPut("{idDepartamento}/{habilitado}")]
        public void Put(Int16 idDepartamento, bool habilitado)
        {
            DAL accesoDatosModel = new DAL();

            try
            {
                int resultado = accesoDatosModel.ExecuteNonQuery("[dbo].[spDepartamento_ModificarEstado] " + idDepartamento.ToString() + "," + habilitado + "");

            }
            catch (Exception e)
            {
                String mensaje = e.Message;
            }
        }


    }
}
