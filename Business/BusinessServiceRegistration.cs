using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessRules;
using Business.Concretes;
using Core.Business.Mailing;
using Core.Business.Mailing.MailKit;
using Core.CrossCuttingConcerns.Security.Entities;
using Core.CrossCuttingConcerns.Security.Token;
using Core.CrossCuttingConcerns.Security.Token.JWT;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework;
using DataAccess.Concretes.InMemory;
using Microsoft.Extensions.DependencyInjection;

namespace Business
{
    // Extension
    public static class BusinessServiceRegistration // Static
    {
        // Method Static
        // return: <genişletmek istenen tür>
        public static IServiceCollection AddBusinessServices(this IServiceCollection services) // this <genişletmek istenen tür>
        {
            services.AddAutoMapper(assemblies: AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
