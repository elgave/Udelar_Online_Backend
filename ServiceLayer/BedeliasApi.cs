using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Utilidades;

namespace ServiceLayer
{
    public class BedeliasApi : IBedeliasApi
    {
        public bool MatricularseACurso(DTMatricula matricula)
        {
            
            string json = JsonConvert.SerializeObject(matricula);
           
            dynamic respuesta = Post("https://localhost:44387/bedeliasapi/matricularse", json);

            return Convert.ToBoolean(respuesta);
        }

        public dynamic Post(string url,string json)
        {
            try
            {
                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("applicarion/json", json, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                dynamic datos = JsonConvert.DeserializeObject(response.Content);

                return datos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
