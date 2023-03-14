using Newtonsoft.Json;
using AppDoceNuevos.Models;
using AppDoceNuevos.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AppDoceNuevos.Services
{
    public class DirectorServices
    {
        HttpClient client;

        public DirectorServices()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://c914-187-209-95-30.ngrok.io/")
            };
        }

        public event Action<List<string>> Error;

        public async Task<bool> Ordenar(Docente d)
        {
            var json = JsonConvert.SerializeObject(d);
            var response = await client.PostAsync("api/Docente", new StringContent(json, Encoding.UTF8, "application/json"));
            return true;
        }

        public async Task<bool> Update(Docente d)
        {
            var json = JsonConvert.SerializeObject(d);
            var response = await client.PutAsync("api/Docente", new StringContent(json, Encoding.UTF8, "application/json"));

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errores = await response.Content.ReadAsStringAsync();
                LanzarErrrorJson(errores);
                return false;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                LanzarError("No se encontro el docente que queria editar");
            }
            return true;
        }

        public async Task<bool> AgregarDocente(Docente d)
        {
            var json = JsonConvert.SerializeObject(d);
            var response = await client.PostAsync("api/Docente", new StringContent(json, Encoding.UTF8, "application/json"));

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var error = await response.Content.ReadAsStringAsync();
                LanzarErrrorJson(error);
                return false;
            }
            return true;
        }

        public async Task<List<Docente>> GetDocentes()
        {
            List<Docente> list = new List<Docente>();
            var response = await client.GetAsync("api/Docente");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<Docente>>(json);
            }

            if (list != null)
            {
                return list;
            }
            else
            {
                return new List<Docente>();
            }
        }

   
        public async Task<bool> Delete(Docente d)
        {
            var response = await client.DeleteAsync("api/Docente/" + d.Id);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errores = await response.Content.ReadAsStringAsync();
                LanzarErrrorJson(errores);
                return false;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                LanzarError("No se encontro el Id del docente");
            }
            return true;
        }



        void LanzarError(string mensaje)
        {
            Error?.Invoke(new List<string> { mensaje });
        }
        void LanzarErrrorJson(string json)
        {
            List<string> obj = JsonConvert.DeserializeObject<List<string>>(json);
            if (obj != null)
            {
                Error?.Invoke(obj);
            }
        }

        public async Task<bool> Login(Usuario usuario)
        {
            var json = JsonConvert.SerializeObject(usuario);
            var response = await client.PostAsync("api/Usuario/login/", new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errores = await response.Content.ReadAsStringAsync();
                LanzarErrrorJson(errores);
                return false;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                var errores = await response.Content.ReadAsStringAsync();
                LanzarError(errores);
                return false;
            }
            return true;
        }

        public async Task<List<Usuario>> GetUsuarios()
        {
            List<Usuario> list = new List<Usuario>();
            var response = await client.GetAsync("api/usuario");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<Usuario>>(json);
            }

            if (list != null)
            {
                return list;
            }
            else
            {
                return new List<Usuario>();
            }
        }

        public async Task<bool> InsertUsuario(Usuario u)
        {
            var json = JsonConvert.SerializeObject(u);
            var response = await client.PostAsync("api/usuario", new StringContent(json, Encoding.UTF8, "application/json"));

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errores = await response.Content.ReadAsStringAsync();
                LanzarErrrorJson(errores);
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateUsuario(Usuario u)
        {
            var json = JsonConvert.SerializeObject(u);
            var response = await client.PutAsync("api/usuario", new StringContent(json, Encoding.UTF8, "application/json"));

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errores = await response.Content.ReadAsStringAsync();
                LanzarErrrorJson(errores);
                return false;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                LanzarError("No se encontro el usuario que desea editar");
            }
            return true;
        }

        public async Task<bool> DeleteUsuario(Usuario u)
        {
            var response = await client.DeleteAsync("api/usuario/" + u.Id);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errores = await response.Content.ReadAsStringAsync();
                LanzarErrrorJson(errores);
                return false;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                LanzarError("No se encontro el Id del Usuario");
            }
            return true;
        }

    }
}
