﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Linq;

namespace FARApplication.Web.Utility
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class EncryptedActionParameterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            Dictionary<string, object> decryptedParameters = new Dictionary<string, object>();

          
            if (!string.IsNullOrEmpty(filterContext.HttpContext.Request.Query["q"]))
            {
                string encryptedQueryString = filterContext.HttpContext.Request.Query["q"].ToString();
                string decrptedString = Decrypt(encryptedQueryString.ToString());
                string[] paramsArrs = decrptedString.Split('?');

                for (int i = 0; i < paramsArrs.Length; i++)
                {
                    string[] paramArr = paramsArrs[i].Split('=');
                    //if(paramArr[1].GetType().Name == "String")
                    //    decryptedParameters.Add(paramArr[0], paramArr[1]);
                    //else
                    int val = 0;
                    if (Int32.TryParse(paramArr[1], out val))
                        decryptedParameters.Add(paramArr[0], Convert.ToInt32(paramArr[1]));
                    else
                        decryptedParameters.Add(paramArr[0], paramArr[1]);
                }
            }
            for (int i = 0; i < decryptedParameters.Count; i++)
            {
                filterContext.ActionArguments[decryptedParameters.Keys.ElementAt(i)] = decryptedParameters.Values.ElementAt(i);
            }
            base.OnActionExecuting(filterContext);

        }

        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "LUPHLAKMAKJ2SPBNI99212";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

    }
}
