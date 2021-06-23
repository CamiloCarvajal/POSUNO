﻿using Newtonsoft.Json;
using POSUNO.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace POSUNO.Helpers
{
    class ApiService
    {




        public static async Task<Response> LoginAsync(LoginRequest model) {
            try
            {
                string request = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");

                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };


                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri("https://localhost:44328")
                };

                HttpResponseMessage response = await client.PostAsync("api/Account/login", content);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        message = result
                    };
                }

                User user = JsonConvert.DeserializeObject<User>(result);
                return new Response
                {
                    IsSuccess = true,
                    result = user
                };

            }
            catch (Exception e)
            {
                return new Response
                {
                    IsSuccess = false,
                    message = e.Message
                };
            }

        }


    }
}