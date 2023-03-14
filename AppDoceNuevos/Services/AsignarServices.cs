﻿using AppDoceNuevos.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDoceNuevos.Services
{
    public class AsignarServices
    {
        HttpClient cliente = new HttpClient
        {
            BaseAddress = new Uri("https://1ef2-187-209-95-30.ngrok.io/")
        };
        public event Action<string> Error;
        void LanzarError(string mensaje)
        {
            Error?.Invoke(mensaje);
        }
        void LanzarErrorJson(string json)
        {
            string obj = JsonConvert.DeserializeObject<string>(json);
            if (obj != null)
            {
                Error?.Invoke(obj);
            }
        }
        public async Task<List<Grupo>> GetGrupo()
        {
            List<Grupo> listaGrupo = new List<Grupo>();
            var response = await cliente.GetAsync("api/Grupo");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                listaGrupo = JsonConvert.DeserializeObject<List<Grupo>>(json);
            }

            if (listaGrupo != null)
            {
                return listaGrupo;
            }
            else
            {
                return new List<Grupo>();
            }

        }

        public async Task<List<Periodo>> GetPeriodo()
        {
            List<Periodo> listaPeriodo = new List<Periodo>();
            var response = await cliente.GetAsync("api/Periodo");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                listaPeriodo = JsonConvert.DeserializeObject<List<Periodo>>(json);
            }

            if (listaPeriodo != null)
            {
                return listaPeriodo;
            }
            else
            {
                return new List<Periodo>();
            }
        }
        public async Task<bool> InsertAsignarGrupo(DocenteGrupo docenteGrupo)
        {


            var json = JsonConvert.SerializeObject(docenteGrupo);
            var response = await cliente.PostAsync("api/DocenteGrupo", new StringContent(json, Encoding.UTF8,
                "application/json"));


            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) //BadRequest
            {
                var errores = await response.Content.ReadAsStringAsync();
                LanzarErrorJson(errores);
                return false;
            }
            return true;
        }
        public async Task<List<Asignatura>> GetAsignatura()
        {
            List<Asignatura> listaAsignatira = new List<Asignatura>();
            var response = await cliente.GetAsync("api/Asignatura");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                listaAsignatira = JsonConvert.DeserializeObject<List<Asignatura>>(json);
            }

            if (listaAsignatira != null)
            {
                return listaAsignatira;
            }
            else
            {
                return new List<Asignatura>();
            }
        }
        public async Task<List<DocenteAsignatura>> GetDocenteAsignatura()
        {
            List<DocenteAsignatura> listaDocenteAsignatura = new List<DocenteAsignatura>();
            var response = await cliente.GetAsync("api/DocenteAsignatura");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                listaDocenteAsignatura = JsonConvert.DeserializeObject<List<DocenteAsignatura>>(json);
            }

            if (listaDocenteAsignatura != null)
            {
                return listaDocenteAsignatura;
            }
            else
            {
                return new List<DocenteAsignatura>();
            }
        }
        public async Task<List<DocenteGrupo>> GetDocenteGrupo()
        {
            List<DocenteGrupo> listaDocenteGrupo = new List<DocenteGrupo>();
            var response = await cliente.GetAsync("api/DocenteGrupo");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                listaDocenteGrupo = JsonConvert.DeserializeObject<List<DocenteGrupo>>(json);
            }

            if (listaDocenteGrupo != null)
            {
                return listaDocenteGrupo;
            }
            else
            {
                return new List<DocenteGrupo>();
            }
        }
    }
}
