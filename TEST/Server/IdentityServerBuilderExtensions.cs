//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography.X509Certificates;
//using System.Threading.Tasks;

//namespace TEST.Server
//{
//    public static class IdentityServerBuilderExtensions
//    {
//        public static IIdentityServerBuilder LoadSigningCredentialFrom(this IIdentityServerBuilder builder, string path)
//        {
//            if (!string.IsNullOrEmpty (path))
//            {
//                try
//                {
//   //  builder.AddSigningCredential(new X509Certificate2(path));
//                }
//                catch (Exception)
//                {

                 
//                }
           
//            }
//            else
//            {
//                builder.AddDeveloperSigningCredential();
//            }

//            return builder;
//        }
//    }
//}
