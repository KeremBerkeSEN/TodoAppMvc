using System.ComponentModel.DataAnnotations;//"using" kalıbı bir keyword'tür aşağıda kullanacağımız bazı kelimelerin hangi kütüphanelerden geleceğini belirtmek 
// için kütüphane adının başına ekleriz "." ise bir operatördür ve amacı bir nesnenin özelliklerine, metodlarına veya alt üyelerine erişmek için kullanılır.
//"." operatörü yukarıda using keywordü ile kullanılacak metotların vs System namespace'inin ComponentModel alt namespace'inin DataAnnotations alt namespace'inde olduğunu belirtir.

namespace TodoMvcApp.Models //namespace bir keyword'tür namespace burada ifade edilecek tanımların başka yerlerdeki tanımlarla karıştırılmaması ve buraya özgü olduğunu belirtmek için kullanılır. 
{
    public class Todo//public,private ve protected gibi keyword'ler önüne yazıldığı sınıf vb. seylerin erişilebilme türlerini ifade eder
                     //class ise bir nesneyi düzenlemek ve özel bir şekilde inşaa etmek için parçalarına (class'larına) bölüp bu parçalardan tekrar bir bütün elde etmemizi sağlar.
                     //Todo ise buradaki sınıfın ismidir.
    {
        [Key]//"[Key]" bir attribute'dur. Bu attribute, bir özelliğin (property) birincil anahtar (primary key) olduğunu belirtir.
        //Amaç: Veritabanında bu özelliğin, tablo için benzersiz bir kimlik (ID) olarak kullanılmasını sağlamak.
        public int Id { get; set; }//"int" C# dilinde bir veri türüdür ve tam sayıları saklamak için kullanılır.
                                   // "get" Özelliğin değerini okumak için kullanılan bir accessor'dır. "set" özelliğin değerini yazmak (atamak) için kullanılan bir accessor'dır.
                                   //C# dilinde property (özellik), bir sınıfın dış dünyaya sunduğu kontrollü bir erişim noktasıdır. Property'ler, bir sınıfın içindeki alanlara (fields) erişimi kontrol etmek için kullanılır. Property'ler genellikle getter ve setter accessor'ları ile tanımlanır.
                                   //get accessor'ı ile Id özelliğinin(property) değeri okunabilir. set accessor'ı ile Id özelliğine bir değer atanabilir.

        [Required(ErrorMessage = "Başlık alanı zorunludur")]//"[Required]" bir attribute'dur. Bu attribute, bir özelliğin (property) zorunlu olduğunu belirtir.
                                                            //Amaç: Veritabanında veya kullanıcıdan alınan verilerde, bu özelliğin mutlaka bir değer almasını zorunlu kılmak.
                                                            //"ErrorMessage" bir parametredir ve Eğer bu özellik boş bırakılırsa, gösterilecek hata mesajını belirtir.
        [StringLength(200)]//"[StringLength]" bir attribute'dur. Bu attribute, bir özelliğin (property) maksimum karakter uzunluğunu belirtir.
                           //Amaç: Veritabanında veya kullanıcıdan alınan verilerde, bu özelliğin belirli bir uzunlukta olmasını sağlamak.
        public string Title { get; set; } = string.Empty;//"string" C# dilinde bir veri türüdür ve metin (karakter dizisi) değerlerini saklamak için kullanılır.
                                                         // "get" Özelliğin değerini okumak için kullanılan bir accessor'dır. "set" özelliğin değerini yazmak (atamak) için kullanılan bir accessor'dır.
                                                         //C# dilinde property (özellik), bir sınıfın dış dünyaya sunduğu kontrollü bir erişim noktasıdır. Property'ler, bir sınıfın içindeki alanlara (fields) erişimi kontrol etmek için kullanılır. Property'ler genellikle getter ve setter accessor'ları ile tanımlanır.
                                                         //get accessor'ı ile Title özelliğinin(property) değeri okunabilir. set accessor'ı ile Title özelliğine bir değer atanabilir.
                                                         //"=" atama operatörüdür. Sağdaki değeri soldaki özelliğe atar. "string.Empty" C# dilinde, boş bir metin (karakter dizisi) değerini temsil eder.
                                                         //Amaç: Title özelliğine özelliğine varsayılan olarak boş bir metin değeri atamak.

        public string? Description { get; set; }//"string" C# dilinde bir veri türüdür ve metin (karakter dizisi) değerlerini saklamak için kullanılır.
                                                //"?(Nullable)" operatörü "Description" özelliğinin(property) null değer alabileceğini belirtir. "get" Özelliğin değerini okumak için kullanılan bir accessor'dır. "set" özelliğin değerini yazmak (atamak) için kullanılan bir accessor'dır.
                                                //C# dilinde property (özellik), bir sınıfın dış dünyaya sunduğu kontrollü bir erişim noktasıdır. Property'ler, bir sınıfın içindeki alanlara (fields) erişimi kontrol etmek için kullanılır. Property'ler genellikle getter ve setter accessor'ları ile tanımlanır.
                                                //get accessor'ı ile Description özelliğinin(property) değeri okunabilir. set accessor'ı ile Description özelliğine bir değer atanabilir.

        public bool IsCompleted { get; set; }//"bool" C# dilinde bir veri türüdür ve true/false (doğru/yanlış) değerlerini saklamak için kullanılır.
                                             //"get" Özelliğin değerini okumak için kullanılan bir accessor'dır. "set" özelliğin değerini yazmak (atamak) için kullanılan bir accessor'dır.
                                             //C# dilinde property (özellik), bir sınıfın dış dünyaya sunduğu kontrollü bir erişim noktasıdır. Property'ler, bir sınıfın içindeki alanlara (fields) erişimi kontrol etmek için kullanılır. Property'ler genellikle getter ve setter accessor'ları ile tanımlanır.
                                             //get accessor'ı ile IsCompleted özelliğinin(property) değeri okunabilir. set accessor'ı ile IsCompleted özelliğine bir değer atanabilir.

        public DateTime CreatedDate { get; set; } = DateTime.Now;//"DateTime" C# dilinde bir veri türüdür ve tarih/saat değerlerini saklamak için kullanılır.
                                                                 // "get" Özelliğin değerini okumak için kullanılan bir accessor'dır. "set" özelliğin değerini yazmak (atamak) için kullanılan bir accessor'dır.
                                                                 //C# dilinde property (özellik), bir sınıfın dış dünyaya sunduğu kontrollü bir erişim noktasıdır. Property'ler, bir sınıfın içindeki alanlara (fields) erişimi kontrol etmek için kullanılır. Property'ler genellikle getter ve setter accessor'ları ile tanımlanır.
                                                                 //get accessor'ı ile CreatedDate özelliğinin(property) değeri okunabilir. set accessor'ı ile CreatedDate özelliğine bir değer atanabilir
                                                                 //"=" atama operatörüdür. Sağdaki değeri soldaki özelliğe atar. "DateTime.Now" C# dilinde, sistemin şu anki tarih ve saatini döndüren bir özelliktir. 
                                                                 //Amaç: CreatedDate özelliğine varsayılan olarak, nesne oluşturulduğu anda geçerli olan tarih ve saati atamak.

        public int UserId { get; set; }//"int" C# dilinde bir veri türüdür ve tam sayıları saklamak için kullanılır.
                                       // "get" Özelliğin değerini okumak için kullanılan bir accessor'dır. "set" özelliğin değerini yazmak (atamak) için kullanılan bir accessor'dır.
                                       //C# dilinde property (özellik), bir sınıfın dış dünyaya sunduğu kontrollü bir erişim noktasıdır. Property'ler, bir sınıfın içindeki alanlara (fields) erişimi kontrol etmek için kullanılır. Property'ler genellikle getter ve setter accessor'ları ile tanımlanır.
                                       //get accessor'ı ile UserId özelliğinin(property) değeri okunabilir. set accessor'ı ile UserId özelliğine bir değer atanabilir.
        public virtual User? User { get; set; }//"virtual" C# dilinde bir keyword'tür ve bir özelliğin veya metodun türetilmiş sınıflar tarafından geçersiz kılınabileceğini(override) belirtir.
                                               // "get" Özelliğin değerini okumak için kullanılan bir accessor'dır. "set" özelliğin değerini yazmak (atamak) için kullanılan bir accessor'dır.
                                               //C# dilinde property (özellik), bir sınıfın dış dünyaya sunduğu kontrollü bir erişim noktasıdır. Property'ler, bir sınıfın içindeki alanlara (fields) erişimi kontrol etmek için kullanılır. Property'ler genellikle getter ve setter accessor'ları ile tanımlanır.
                                               //get accessor'ı ile User özelliğinin(property) değeri okunabilir. set accessor'ı ile User özelliğine bir değer atanabilir.
                                               //"User" bu özelliğin veri türüdür. "?" işareti, özelliğin null değer alabileceğini belirtir.
    }
}
