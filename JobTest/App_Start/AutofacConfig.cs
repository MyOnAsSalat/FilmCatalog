using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using JobTest;
using JobTest.Repositories;

public class AutofacConfig
{
    public static void ConfigureContainer()
    {
        // получаем экземпляр контейнера
        var builder = new ContainerBuilder();

        // регистрируем контроллер в текущей сборке
        builder.RegisterControllers(typeof(MvcApplication).Assembly);

        // регистрируем споставление типов
        builder.RegisterType<FilmsRepository>().As<IFilmsRepository>();

        // создаем новый контейнер с теми зависимостями, которые определены выше
        var container = builder.Build();

        // установка сопоставителя зависимостей
        DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
    }
}