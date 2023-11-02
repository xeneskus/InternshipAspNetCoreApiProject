using AutoMapper;

namespace MyProject.Service.Mapping
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DtoMapper>();
            });
            return config.CreateMapper();

        });//bu lazy classı yaptıgı işlem lazyloading sadece ihtiyaç oldugu anda yüklemeye yarıyor. biz ne zaman çağırırsak

        public static IMapper Mapper => lazy.Value; //çağırmak için kullandığımız yer prop yaptık burda bu şekilde tek get yapmış oluruz set yok sadece data alacak
        //bu mapperi çagırdıgımız an yukarıdaki kod memorye 1 kere yüklenecek ve kullanmaya devam edicez
    }
}
