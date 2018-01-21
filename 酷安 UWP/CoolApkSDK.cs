using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using System.Text.RegularExpressions;
using Windows.Networking.BackgroundTransfer;

namespace 酷安_UWP
{
    class CoolApkSDK
    {
        public static async Task<string> GetToken()
        {
            return await Web.GetHttp("http://l.w568w.ml/api/token.php");
        }
        
        public static async Task<string> GetCoolApkMessage(string url)
        {
            //这里感谢https://github.com/bjzhou/Coolapk-kotlin提供的 HTTP 头！！！！！！！！！！！！！

            /*
            User-Agent: Dalvik/2.1.0 (Linux; U; Android 5.1.1; Nexus 4 Build/LMY48T) (#Build; google; Nexus 4; LMY48T; 5.1.1) +CoolMarket/7.3
            X-Requested-With: XMLHttpRequest
            X-Sdk-Int: 22
            X-Sdk-Locale: zh-CN
            X-App-Id: coolmarket
            X-App-Token: 2a6e2adc2897c8d8133db17c2cd3b1045834ce58-d7d5-38eb-95d5-563167a1983d0x588f16cd
            X-App-Version: 7.3
            X-App-Code: 1701135
            X-Api-Version: 7
            */
            try
            {
                var mClient = new HttpClient();
                mClient.DefaultRequestHeaders.Add("User-Agent", "Dalvik/2.1.0 (Linux; U; Android 7.1.1; Nexus 4 Build/LMY48T) (#Build; google; Nexus 4; LMY48T; 5.1.1) +CoolMarket/7.3");
                mClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                mClient.DefaultRequestHeaders.Add("X-Sdk-Int", "22");
                mClient.DefaultRequestHeaders.Add("X-Sdk-Locale", "zh-CN");
                mClient.DefaultRequestHeaders.Add("X-App-Id", "coolmarket");
                mClient.DefaultRequestHeaders.Add("X-App-Token", await GetToken());
                mClient.DefaultRequestHeaders.Add("X-HeaderKey", "HeaderValue");
                mClient.DefaultRequestHeaders.Add("X-X-App-Version", "7.3");
                mClient.DefaultRequestHeaders.Add("X-App-Code", "1701135");
                mClient.DefaultRequestHeaders.Add("X-Api-Version", "7");
                //mClient.DefaultRequestHeaders.Add("X-App-Device", getAppDevice());
                return await mClient.GetStringAsync(new Uri(url));
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public static async Task<string> GetCoolApkUserFace(String UName)
        {
            String body = await Web.GetHttp("https://www.coolapk.com/u/" + UName);
            body = Regex.Split(body, @"<div class=""msg_box"">")[1];
            body = Regex.Split(body, @"src=""")[1];
            return Regex.Split(body, @"""")[0];
        }
    }
}


