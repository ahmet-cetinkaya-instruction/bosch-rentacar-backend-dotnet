using AutoMapper;
using Business.BusinessRules;
using Business.Concretes;
using Business.Requests.Brands;
using Core.Business.Exceptions;
using Core.Business.Mailing;
using DataAccess.Abstracts;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore.Query;
using Moq;

namespace Business.Tests.Concretes
{
    [TestClass]
    public class BrandManagerTests
    {
        private Mock<IBrandDal> _brandDalMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IMailService> _mailServiceMock;
        private BrandManager _brandManager;

        // ClassInitialize: İlgili test classı başlangıcında çalışıyor.

        // Her bir test metodu için OnBefore metodu diyebiliriz.
        [TestInitialize]
        public void TestInitialize()
        {
            _brandDalMock = new Mock<IBrandDal>();
            // MockBehavior default = loose çalışıyor.
            // - loose'da da setup etmediğimiz her eleman default değerlere ve davranışa sahip oluyor.
            // Strict: her eleman setup edilmeli.
            _mapperMock = new Mock<IMapper>();
            _mailServiceMock = new Mock<IMailService>();
            BrandBusinessRules brandBusinessRules = new(_brandDalMock.Object);
            _brandManager = new BrandManager(_brandDalMock.Object, brandBusinessRules,
                                             _mapperMock.Object, _mailServiceMock.Object);
        }

        //1. en küçük parçamız: BrandManager içerisindeki metotlar
        //2. senaryomuz: Bir brand eklenirken, aynı isimde bir brand kaydı olduğunda hata vermelidir.
        [TestMethod]
        [Owner("Ahmet Çetinkaya")] // Çeşitli bilgilendirme Attribute ...
        public void BrandNotShouldBeHasDuplicatedName() //3. Çeşitli isimlendirme standartları olabilir örneğin Cucumber
        {
            //i Unit of work: BrandManager.Add
            //i Unit test: Unit of work (çalışma alanını) test eder.
            //! Test bağımsız çalışabilmelidir.
            //! Hızlı çalışmalı/sonuç vermeli
            //! Kodlar ve hata sonuçları anlaşılabilir olmalıdır ve doğruya yönlendirmelidir.

            //# Arrange: Hazırlık aşaması

            var expression = new CreateBrandRequest { Name = "BMW" };
            //var expected = BusinessException

            // Class içeisindeki bir property'i test dahilinde set edip kullanırsak, o set edilen değere test devamında ulaşmak için setup etmeliyiz.
            //_brandDalMock.SetupProperty(b => b.ExampleProperty);
            //_brandDalMock.SetupAllProperties();

            // IBrandDal.Get(brand=>brand.Name == "BMW") // Brand{Id=1, Name="BMW"}
            _brandDalMock.Setup(b => b.Get(brand => brand.Name == "BMW",
                        It.IsAny<Func<IQueryable<Brand>, IIncludableQueryable<Brand, object>>>(),
                        It.IsAny<bool>()
                        )).Returns(new Brand { Id = 1, Name = "BMW" });

            //# Action: Eyleme geçtiğimiz ve ilgili parçayı çağırıp çalıştırdığımız alan.
            var actual = () => _brandManager.Add(expression);

            //# Assert: Action'ın sonucu tabi tutulan koşullardan geçirilir.
            Assert.ThrowsException<BusinessException>(actual);
        }

        // Her testten sonra çalışacaktır.
        [TestCleanup]
        public void TestCleanup(){
        }

        // ClassCleanup
    }
}