﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TimeloggerCore.Common.Helpers
{
    public static class JsonSerializer
    {
        public static T Deserialize<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static Task<T> DeserializeAsync<T>(string jsonString)
        {
            return Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(jsonString));
        }
        public static T Deserialize<T>(object obj)
        {
            var jsonString = Serialize(obj);
            return Deserialize<T>(jsonString);
        }

        public static Task<T> DeserializeAsync<T>(object obj)
        {
            var jsonString = Task.Factory.StartNew(() => Serialize(obj));
            return Task.Factory.StartNew(() => Deserialize<T>(jsonString.Result));
        }

        public async static Task<T> DeserializeAsync<T>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public static Task<string> SerializeAsync(object obj)
        {
            return Task.Factory.StartNew(() => JsonConvert.SerializeObject(obj));
        }
    }
}
