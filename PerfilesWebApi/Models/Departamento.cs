using Microsoft.AspNetCore.Mvc;

namespace PerfilesWebApi.Models
{
    public class Departamento
    {
        public Int16 IdDepartamento { get; set; }
        public string Nombre { get; set; }
        public Boolean Habilitado { get; set; }

        public static explicit operator Departamento(NotFoundResult v)
        {
            throw new NotImplementedException();
        }

        public static explicit operator Departamento(BadRequestObjectResult v)
        {
            throw new NotImplementedException();
        }
    }
}
