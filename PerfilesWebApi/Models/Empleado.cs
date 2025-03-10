using Microsoft.AspNetCore.Mvc;

namespace PerfilesWebApi.Models
{
    public class Empleado
    {
        public Int32 IdEmpleado { get; set; }
        public Int16 IdDepartamentoAsignado { get; set; }
        public string Departamento { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string DPI { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string Direccion { get; set; }
        public string NIT { get; set; }
        public DateTime FechaIngreso { get; set; }
        public Boolean Habilitado { get; set; }
        public Boolean DepartamentoHabilitado { get; set; }

        public int Edad {
            get
            {
                DateTime hoy = DateTime.Today;
                int edad = hoy.Year - FechaNacimiento.Year;

                // Verificar si el cumpleaños aún no ha ocurrido este año
                if (FechaNacimiento.Date > hoy.AddYears(-edad))
                {
                    edad--;
                }

                return edad;
            }
        }

        public static explicit operator Empleado(NotFoundResult v)
        {
            throw new NotImplementedException();
        }

        public static explicit operator Empleado(BadRequestObjectResult v)
        {
            throw new NotImplementedException();
        }
    }
}
