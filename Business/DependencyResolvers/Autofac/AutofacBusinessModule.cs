using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Business.Abstracts;
using Business.BusinessRules;
using Business.Concretes;
using Business.Profiles;
using Castle.DynamicProxy;
using Core.Business.Mailing;
using Core.Business.Mailing.MailKit;
using Core.CrossCuttingConcerns.Security.Entities;
using Core.CrossCuttingConcerns.Security.Token;
using Core.CrossCuttingConcerns.Security.Token.JWT;
using Core.Utilities.Interceptors;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework;
using Module = Autofac.Module;

namespace Business.DependencyResolvers.Autofac;

public class AutofacBusinessModule: Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<EfBrandDal>().As<IBrandDal>().SingleInstance();
        builder.RegisterType<BrandBusinessRules>().SingleInstance();
        builder.RegisterType<BrandManager>().As<IBrandService>().SingleInstance();

        builder.RegisterType<EfModelDal>().As<IModelDal>().SingleInstance();
        builder.RegisterType<ModelManager>().As<IModelService>().SingleInstance();

        builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();
        builder.RegisterType<UserBusinessRules>().SingleInstance();
        builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
        builder.RegisterType<AuthBusinessRules>().SingleInstance();
        builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();

        builder.RegisterType<JwtHelper>().As<ITokenHelper>();

        //builder.RegisterType<BrandMappingProfiles>().As<Profile>();
        //builder.Register(c => new MapperConfiguration(cfg =>
        //{
        //    foreach (Profile profile in c.Resolve<IEnumerable<Profile>>())
        //    {
        //        cfg.AddProfile(profile);
        //    }
        //})).AsSelf().SingleInstance();
        //builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
        //       .As<IMapper>().InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
               .AsImplementedInterfaces()
               .EnableInterfaceInterceptors(new ProxyGenerationOptions
               {
                   Selector = new AspectInterceptorSelector()
               })
               .SingleInstance();
    }
}