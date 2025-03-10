using Microsoft.AspNetCore.Mvc;
using PerfilesWebApi.Models;
using System.Data;
using System.Diagnostics.Metrics;

namespace PerfilesWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EmpleadoController : ControllerBase
{
    [Route("Listado/{incluirDeshabilitados}")]
    [HttpGet]
    public IEnumerable<Empleado> Get(bool incluirDeshabilitados)
    {
        DataTable dt;
        DAL accesoDatosModel = new DAL();

        try
        {
            dt = accesoDatosModel.ExecuteQuery("[dbo].[spEmpleado_Listar] " + incluirDeshabilitados);
            if (dt.Rows.Count > 0)
            {
                List<Empleado> lista = dt.AsEnumerable()
                 .Select(row => new Empleado
                 {
                     IdEmpleado = row.Field<Int32>(0),
                     Apellidos = string.IsNullOrEmpty(row.Field<string>(1)) ? "" : row.Field<string>(1),
                     Nombres = string.IsNullOrEmpty(row.Field<string>(2)) ? "" : row.Field<string>(2),
                     FechaIngreso = row.Field<DateTime>(3),
                     Habilitado = row.Field<bool>(4)
                   }).ToList();

                return lista;
            }
            else
            {
                return new List<Empleado>();
            }
        }
        catch (Exception e)
        {
            return (IEnumerable<Empleado>)BadRequest(e.Message);
        }

    }

    [Route("ObtenerRegistro/{idEmpleado}")]
    [HttpGet]
    public Empleado Get(int idEmpleado)
    {
        DataTable dt;
        DAL accesoDatosModel = new DAL();

        try
        {
            dt = accesoDatosModel.ExecuteQuery("[dbo].[spEmpleado_ObtenerRegistro] " + idEmpleado.ToString());
            if (dt.Rows.Count > 0)
            {
                List<Empleado> lista = dt.AsEnumerable()
                 .Select(row => new Empleado
                 {
                     IdEmpleado = row.Field<Int32>(0),
                     IdDepartamentoAsignado = row.Field<Int16>(1),
                     Nombres = string.IsNullOrEmpty(row.Field<string>(2)) ? "" : row.Field<string>(2),
                     Apellidos = string.IsNullOrEmpty(row.Field<string>(3)) ? "" : row.Field<string>(3),
                     DPI = string.IsNullOrEmpty(row.Field<string>(4)) ? "" : row.Field<string>(4),
                     FechaNacimiento = row.Field<DateTime>(5),
                     Sexo = row.Field<string>(6),
                     Direccion = string.IsNullOrEmpty(row.Field<string>(7)) ? "" : row.Field<string>(7),
                     NIT = string.IsNullOrEmpty(row.Field<string>(8)) ? "" : row.Field<string>(8),
                     FechaIngreso = row.Field<DateTime>(9),
                     Habilitado = row.Field<bool>(10)
                 }).ToList();

                return lista[0];
            }
            else
            {
                return (Empleado)NotFound();
            }
        }
        catch (Exception e)
        {
            return (Empleado)BadRequest(e.Message);
        }

    }

    // POST api/<EmpleadoController>
    [HttpPost]
    [Route("insertarEmpleado")]
    public void Post([FromBody] Empleado value)
    {
        DAL accesoDatosModel = new DAL();

        try
        {
            int resultado = accesoDatosModel.ExecuteNonQuery("[dbo].[spEmpleado_Insertar] " + value.IdDepartamentoAsignado 
                + ",'" + value.Nombres + "','" + value.Apellidos 
                + "','" + value.DPI + "','" + value.FechaNacimiento
                + "','" + value.Sexo + "','" + value.Direccion
                + "','" + value.NIT + "','" + value.FechaIngreso
                + "'," + value.Habilitado);

        }
        catch (Exception e)
        {
            String mensaje = e.Message;
        }
    }

    [Route("actualizarEmpleado/{idEmpleado}")]
    [HttpPut]
    public void Put(int idEmpleado, [FromBody] Empleado value)
    {
        DAL accesoDatosModel = new DAL();

        try
        {
            int resultado = accesoDatosModel.ExecuteNonQuery("[dbo].[spEmpleado_Modificar] " + idEmpleado.ToString() + "," + value.IdDepartamentoAsignado
                + ",'" + value.Nombres + "','" + value.Apellidos
                + "','" + value.DPI + "','" + value.FechaNacimiento
                + "','" + value.Sexo + "','" + value.Direccion
                + "','" + value.NIT + "','" + value.FechaIngreso
                + "'," + value.Habilitado);

        }
        catch (Exception e)
        {
            String mensaje = e.Message;
        }
    }

    [Route("actualizarEstadoEmpleado/{idEmpleado}/{habilitado}")]
    [HttpPut]
    public void Put(int idEmpleado, bool habilitado)
    {
        DAL accesoDatosModel = new DAL();

        try
        {
            int resultado = accesoDatosModel.ExecuteNonQuery("[dbo].[spEmpleado_ModificarEstado] " + idEmpleado.ToString() + "," + habilitado + "");

        }
        catch (Exception e)
        {
            String mensaje = e.Message;
        }
    }


}
