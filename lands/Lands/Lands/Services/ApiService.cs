﻿namespace Lands.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Models;
    using Newtonsoft.Json;
    using Plugin.Connectivity;

    public class ApiService
    {
        public async Task<Response> CheckConnection()
        {
            if(!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Please turn on your internet settings.",
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if(!isReachable)
            {
                return new Response
                {
                    IsSuccess=false,
                    Message="Check your internet connection",
                };
            }

            return new Response
            {
                IsSuccess = true,
                Message= "OK",
            };
        }

        public async Task<TokenResponse> GetToken(string urlBase, string username, string password)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);

                var response = await client.PostAsync("Token",
                    new StringContent(String.Format("grant_type=password&username={0}&password={1}",username,password),
                    Encoding.UTF8,"application/x-www-form-urlencoded"));

                var resultJSON = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<TokenResponse>(resultJSON);

                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Response> GetList<T>(string urlBase,string servicePrefix,string controller)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);

                var url = String.Format("{0}{1}",servicePrefix,controller);
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if(!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess=false,
                        Message=result,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess=true,
                    Message="OK",
                    Result=list
                };
            }
            catch(Exception ex)
            {
                return new Response
                {
                    IsSuccess=false,
                    Message=ex.Message,
                };
            }
        }
    }
}
