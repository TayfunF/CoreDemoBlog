# Asp.Net Core 5.0 ve N Katmanlı Mimari İle Blog Sitesi

## Projenin Ön İzlemesi ;

https://user-images.githubusercontent.com/39940749/148678863-79c1a0ba-0d75-417f-9a1a-415d33a35506.mp4

#1- Asp.Net Core 5.0 N-Katmanlı Mimari (N-Tier Architecture)

#2- Dinamik Blog Projesi (Admin-Kullanıcı-Yazar Tablosu Olacak)

#3- Projenin Adı : CoreDemo

#4- Projenin Db ismi : CoreBlogDb

#5- Projede Kullanacağım Katmanlar

	+ 1-) Entity 		Layer : 
		// Projemizde bulunan tablolar ve bu tablolar içerisinde ki  sütunlar.
		// Visual Studio İçerisinde bu yapılar class olarak tutuluyor.
				
	+ 2-) Data Access 	Layer :
		// CRUD işlemlerinin temelini oluşturacak olan yapıyı kullanıcam.
	
	+ 3-) Business	 	Layer :
		// Validation'ları yaptığım katman.
		// Yani geçerlilik-doğrulama işlemlerini yaptığım katman.
	
	+ 4-) Presentation 	Layer :
		// Sunum yani UI işlemlerinin oluşturduğum katman.
		// Aynı zamanda Backend işlemleride controller tarafına yazılabilir.
		// Temelde Sunum kısmıdır.
	
	+ 5-) Core 			Layer :
	
	+ 6-) Api 			Layer :
	
#6- Bu katmanları ProjemdeNasıl Ekliyorum ? ;
	// (Ana Klasör Üzerine Sağ Click->Add->New Project Yoluyla->Class Library->Eklenecek Katman Adı Yaz)

*************** ENTITY LAYER START ***************	

#7- Entity Layer [Class Library] içeriğinde şu tablolar olacak :
	+ 1-) Makale    (Article)
	+ 2-) Kategori  (Category)
	+ 3-) Yorumlar  (Comment)
	+ 4-) Yazarlar  (Writer)
	+ 5-) Hakkınmda (About)
	+ 6-) İletişim  (Contact)
	
#8- Entity Layerda bu 6 tane tablonun her birinin içine prop tanımlıyorum.
	Örnek : Category için ; CategoryID, CategoryName , CategoryDescription,CategoryStatus gibi . . .
	
*************** ENTITY LAYER END ***************	

*************** DATA ACCESS LAYER START ***************

#9- Data Access Layer [Class Library] içeriğinde şu klasörleri oluşturuyorum :
	+ 1-) Abstract
	+ 2-) Concrete (Bu benim Context.cs yani db mi oluşturuyor.)
	+ 3-) Repositories
	
#10- Concrete içerisine Context.cs adında Class tanımladım.

#11- :DbContext tanımlamalarını yaptım.

#12- Bu Class içerisine DbSet<> prop larımı tanımlamadan önce katmanlar arası referans verdim.

#13- Katmanlar arası referans verme ; (HER ÜSTTEKİ KATMAN İÇİN ALTTAKİ KATMANLARI REFERANS VERMEYİ UNUTMA !!!)
	+ 1-) Amaç : Katmanları üst üste inşa etme.(Sırası E-->D.A-->B-->P)
	+ 2-) Örnek :DataAccessLayer altındaki Dependencies üzerine sağ click -> Add Projet Referans kısmından Entity Layer'ı seç
	+ 3-) Bu adımları tüm katmanlar için yaptım.

#14- Referans adımını bitirince artık 6 tane tablomun DbSet<> proplarını yazabilirim.

#15- Concrete klasörü içinde ki Context.cs içerisine tanımlıyorum. 

#16- appsettings.json içerisine ConnectionStrings 'i tanımladım.

#17- StartUp.cs içerisine mySql için gerekli tanımlamamı yaptım.

#18- Tablo ilişkilendirmeleri henüz yapmadım.(Bir kaç adım sonra yaptım.)

#19- phpMyAdmin 'i açtım ve sadece db ismini oluşturdum.

#20- DİKKAT !!! add-migration yazmadan önce PM Console de Default Project kısmında DataAccessLayer'ı seç !!!

#21- Migration kısmını oluşturuyorum. (PM Console ile add-migration , update-database)  

#22- Migration da ilişkileri tanımlıyorum. 
	+ 1-) Category.cs içerisine ; public List<Blog> Blogs {get; set;}
	+ 2-) Blog.cs içerisine     ; public int CategoryID { get; set; } ve public Category Category {get; set;} 

#23- DİKKAT 2 !!! Bu iki aşamayı yaptıktan sonra Pm Console gel. Project Default olarak DataAccessLayer 'ı seç ondan sonra add-migration mig2 , update-database yap.

#24- Şimdi sırada blog ve yorum arası ilişki kurulması gerekiyor. Bunun için ;
	+ 1-) Blog.cs içerisine     ; public List<Comment> Comments { get; set; }
	+ 2-) Comment.cs içerisine  ; public int BlogID {get; set;}
	+ 3-) Comments.cs içerisine ; public Blog Blog {get; set;}

#25- Bu üç aşamayı yaptıktan sonra add-migration mig3 , update-database yap.

#26- GenericRepository kullanarak projeye devam edicem. O yüzden GenericRepository oluşturdum. Neden ??? Çünkü !!! ;
	+ 1-) Her CRUD işlemi için bir metot tanımlanacak
	+ 2-) Metotların imzası olarak inferfaceler kullanılacak
	+ 3-) Abstract üzerinde soyut ifade olarak interfaceleri tanımla
	+ 4-) Concrete üzerinde somut ifade olarak bu interfacelerin içinde yer alan metotların içini doldur.
	+ 5-) Generic olunca tüm metotlara uygulayabiliriz.
	+ 6-) Ekleme-Silme-Güncelleme işlemi için VOID türünde metot oluşturucam.
	
#27- DataAccessLayer->Abstract klasörü üzerine sağ click->Add->Class [Türü Interface !!!] (İsmi IGenericDAL.cs) [DAL : Data Access Layer]	 

#28- Oluşturulan IGenericDAL Interface 'i içerisine Generic yapının tanımlarını yaptım. T tipinde bir entity üzerinden CRUD işlemlerini tanımladım.

#29- Bu interface'i çalıştıracak olan repositories klasöründe  GenericRepository.cs adında bir class oluşturdum. IGenericDAL'den Miras alıp içerisine Generic CRUD kodlarını yazdım. (Kod içerisinden bakılabilir).

#29- Startup.cs içerisine gidip IGenericDAL-GenericRepository haberleşmesi için gerekli olan alttaki şu kodu yazdım ; 
		*** services.AddScoped(typeof(IGenericDAL<>), typeof(GenericRepository<>)); ***
        (//HER EKLEDİĞİM Abstract klasörü içinde ki Interface Class'ım için onun karşılığı olan Repositories klasöründeki karşılığı için AddScoped 'unu tanımlamamız gerekiyor). 
		( Bunun anlamı o interface'i gördüğümde o class'a ulaşacağım demektir. (Dependency Injection))
		ÖRNEK : Abstract klasörüne IBlogDAL.cs adında Interface Class tanımlasaydım onu da Startup.cs içerisine yazıcaktım. services.AddScoped(typeof(IBlogDAL<>), typeof(BlogRepository<>)); şeklinde . . .

#30- DAL içerisindeki Abstract klasörü içerisine 6 tane Interface tanımlıyorum ve IGenericDAL<Category> 'yi katılım veriyorum.
	+ 1-) IAboutDAL
	+ 2-) IBlogDAL
	+ 3-) ICategoryDAL
	+ 4-) ICommentDAL
	+ 5-) IContactDAL
	+ 6-) IWriterDAL

#31- DAL içerisine yeni bir klasör ekliyorum. İsmi EntityFramework. Bu klasör içerisine 30. adımdaki 6 tane Interface 'ime karşılık gelecek şekilde 5 tane repository tanımlıyorum.
	+ 1-) EfAboutRepository.cs
	+ 2-) EfBlogRepository
	+ 3-) EfCategoryRepository
	+ 4-) EfCommentRepository
	+ 5-) EfContactRepository
	+ 6-) EfWriterRepository
	
#32- Bu Ef lere karşılık startup.cs içerisine 6 tane AddScoped ekledim.
	+ 1-) services.AddScoped<IAboutDAL , EfAboutRepository>();
    + 2-) services.AddScoped<IBlogDAL , EfBlogRepository>();
    + 3-) services.AddScoped<ICategoryDAL , EfCategoryRepository>();
    + 4-) services.AddScoped<ICommentDAL , EfCommentRepository>();
    + 5-) services.AddScoped<IContactDAL , EfContactRepository >();
    + 6-) services.AddScoped<IWriterDAL , EfWriterRepository>();
	

*************** DATA ACCESS LAYER END ***************

*************** BUSINESS LAYER START ***************

#33- Business Layer içerisine 3 tane klasör oluşturdum.
	+ 1-) Abstract (DAL daki Interfacelerim gibi olucaklar). +  [Business Katmanında Abstract klasörü içerisinde ki Interfaceler : Service olarak adlandırılıyor].
	+ 2-) Concrete (Bu benim Context.cs yani db mi oluşturuyor). [Business Katmanında Concrete klasörü içerisinde ki Classlar : Manager olarak adlandırılıyor].
	+ 3-) ValidationRules

#34- Abstract klasörü içerisine ICategoryService.cs adında Interface Class oluşturuyorum. (İçerisine CRUD kodlar yazdım. Kodlarına bakılabilir).

#35- Concrete klasörü içerisine CategoryManager.cs adında bir class oluşturuyorum. 
	+1-) Public yapıp ICategoryService 'i kalıtım verdim.
	+2-) Implement işlemini yaptım.
	+3-) ICategoryDAL _categoryDAL; oluşturdum.
	+4-) Constructor tanımladım.
	+5-) Iplementlerin içini doldurdum.
	
#36- Presentation (Web) Katmanında Controller klasöründe CategoryController.cs Controller oluşturdum. İçerisine Constructor tanımladım.

#37- DB me gidip Categories tabloma manuel girişler yaptım.
	
#38- CategoryController 'a bu giriş yaptığım değerleri listelemek için Index IActionResult 'a Getlist metodunu çağırdır.

#39- Index sayfamı AddView diyerek oluşturdum. İçerisine @using ve @model ekledim. Body kısmına listeleme için tablo oluşturdum.

#40- Projeye UI Temamı ekledim. Düzenlemeler gerçekleştirdim.

#41- UserLayout isminde Views-->Shared klasöründe MasterPage yapımı oluşturdum. Eklediğim temanın içinde ki Index sayfasındaki kodları MasterPage 'ime aldım ve RenderBody() 'i en alta ekledim.

#42- PartialView kullandım. Bunun için adımlarım.
	+1-) Presentation (Web) katmanımdaki Views klasörü altında ki shared klasörü içerisine HeaderPartialView adında bir view PartialView oluşturdum.
	+2-) UserLayout.cshtml MasterPage yapımın içerisinde @await komutu yardımıyla bu partial view 'ı kullandım.
	+3-) UserLayout.cshtml 'deki header' a ait nav kodlarımı kesip HeaderNavPartialView.cshtml PartialView 'ıma yapıştırdım. Kestiğim kısma da @await Html.PartialAsync ("HeaderNavPartialView") yazdım.
	+4-) Her bölmek istediğim alan için bu ilk 3 adımdakileri yaptım.
	
#43- .NetCore 'de Partial View çağırabilmek için @await komutu kullanıyoruz.

#44- Business Layer 'daki Abstract klasörüne IBlogService.cs adında yeni bir Interface tanımladım. Hemen ardından Concrete klasörüne BlogManager 'ı ekledim. Kalıtım verdim. Metodları Implement ettim.

#45- BlogController 'ıma geliyorum. İçerisine Context ve IBlogDAL 'ı tanımlıyorum. Index metodu içine Listeleme yapabilmek için gerekli kodu yazıyorum.

#46- Blog yazılarımı gösterebilmek için Sql de Blog tablom içerisine manuel eklemeler yapıyorum. 

#47- Blog yazılarını görebilmek için CoreDemo-->Blog-->Index sayfası içerisine @forerach döngüsü oluşturuyorum ve @k ile DB den bilgileri çekiyorum.

*************** BUSINESS LAYER END ***************
