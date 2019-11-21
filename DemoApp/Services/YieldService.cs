using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Services
{

    class YieldService
    {     
        public static IEnumerable<string> YieldSha256Hash(int count) {
            var i = 0;
            while (i < count)
            {
                i++;
                yield return Sha256Service.ComputeSha256Hash(i.ToString());
            }            
        }

        public static async IAsyncEnumerable<string> YieldSha256HashAsync(int count)
        {
            var i = 0;
            while (i < count)
            {
                i++;
                await Task.Delay(1000); //something takes a while
                yield return Sha256Service.ComputeSha256Hash(i.ToString());
            }
        }             

        public static IEnumerable<string> YieldDemo()
        {
            yield return "Hello";
            yield return "Hello World";
            yield return "Hello Again";
        }

    }
}
