using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfilesWebApi.Models;
using System.Data;

namespace PerfilesWebApi.Controllers
{
    public class ReporteEmpleadosController : ControllerBase
    {
        [Route("ReporteEmpleados/{idDepartamento}/{fechaIngresoDesde}/{fechaIngresoHasta}/{incluirDeshabilitados}")]
        [HttpGet]
        public IEnumerable<Empleado> Get(Int16 idDepartamento,
                                        DateTime fechaIngresoDesde,
                                        DateTime fechaIngresoHasta,
                                        bool incluirDeshabilitados)
        {
            DataTable dt;
            DAL accesoDatosModel = new DAL();

            try
            {
                dt = accesoDatosModel.ExecuteQuery("[dbo].[spReporte_Empleados] " + idDepartamento + ",'" + fechaIngresoDesde + "','" + fechaIngresoHasta + "'," + incluirDeshabilitados);
                if (dt.Rows.Count > 0)
                {
                    List<Empleado> lista = dt.AsEnumerable()
                     .Select(row => new Empleado
                     {
                         IdEmpleado = row.Field<Int32>(0),
                         Nombres = string.IsNullOrEmpty(row.Field<string>(1)) ? "" : row.Field<string>(1),
                         Apellidos = string.IsNullOrEmpty(row.Field<string>(2)) ? "" : row.Field<string>(2),                   
                         Habilitado = row.Field<bool>(3)
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
    }
}
