using System;//"using" kalıbı bir keyword'tür aşağıda kullanacağımız bazı kelimelerin hangi kütüphanelerden geleceğini belirtmek 
// için kütüphane adının başına ekleriz "." ise bir operatördür ve amacı bir nesnenin özelliklerine, metodlarına veya alt üyelerine erişmek için kullanılır.
//"." operatörü buralarda using keywordü ile kullanılacak metotların vs System namespace'inin ComponentModel alt namespace'inin DataAnnotations alt namespace'inde olduğunu belirtir.
//veya kullanılacak metotların vs System namespace'inin Collections alt namespace'inin Generic alt namespace'inde olduğunu belirtir.
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoMvcApp.Models//namespace bir keyword'tür namespace burada ifade edilecek tanımların başka yerlerdeki tanımlarla karıştırılmaması ve buraya özgü olduğunu belirtmek için kullanılır. 
{
    public class User//public,private ve protected gibi keyword'ler önüne yazıldığı sınıf vb. seylerin erişilebilme türlerini ifade eder
                     //class ise bir nesneyi düzenlemek ve özel bir şekilde inşaa etmek için parçalarına (class'larına) bölüp bu parçalardan tekrar bir bütün elde etmemizi sağlar.
                     //Todo ise buradaki sınıfın ismidir.
    {
        public int Id { get; set; }//"int" C# dilinde bir veri türüdür ve tam sayıları saklamak için kullanılır.
                                   // "get" Özelliğin değerini okumak için kullanılan bir accessor'dır. "set" özelliğin değerini yazmak (atamak) için kullanılan bir accessor'dır.
                                   //C# dilinde property (özellik), bir sınıfın dış dünyaya sunduğu kontrollü bir erişim noktasıdır. Property'ler, bir sınıfın içindeki alanlara (fields) erişimi kontrol etmek için kullanılır. Property'ler genellikle getter ve setter accessor'ları ile tanımlanır.
                                   //get accessor'ı ile Id özelliğinin(property) değeri okunabilir. set accessor'ı ile Id özelliğine bir değer atanabilir.


        [Required]//"[Required]" bir attribute'dur. Bu attribute, bir özelliğin (property) zorunlu olduğunu belirtir.
        //Amaç: Veritabanında veya kullanıcıdan alınan verilerde, bu özelliğin mutlaka bir değer almasını zorunlu kılmak.
        [StringLength(50)]//"[StringLength]" bir attribute'dur. Bu attribute, bir özelliğin (property) maksimum karakter uzunluğunu belirtir.
        //Amaç: Veritabanında veya kullanıcıdan alınan verilerde, bu özelliğin belirli bir uzunlukta olmasını sağlamak.
        public string Username { get; set; } = string.Empty;//"string" C# dilinde bir veri türüdür ve metin (karakter dizisi) değerlerini saklamak için kullanılır.
                                                            // "get" Özelliğin değerini okumak için kullanılan bir accessor'dır. "set" özelliğin değerini yazmak (atamak) için kullanılan bir accessor'dır.
                                                            //C# dilinde property (özellik), bir sınıfın dış dünyaya sunduğu kontrollü bir erişim noktasıdır. Property'ler, bir sınıfın içindeki alanlara (fields) erişimi kontrol etmek için kullanılır. Property'ler genellikle getter ve setter accessor'ları ile tanımlanır.
                                                            //get accessor'ı ile Username özelliğinin(property) değeri okunabilir. set accessor'ı ile Username özelliğine bir değer atanabilir.
                                                            //"=" atama operatörüdür. Sağdaki değeri soldaki özelliğe atar. "string.Empty" C# dilinde, boş bir metin (karakter dizisi) değerini temsil eder.
                                                            //Amaç: Username özelliğine özelliğine varsayılan olarak boş bir metin değeri atamak.

        [Required]//"[Required]" bir attribute'dur. Bu attribute, bir özelliğin (property) zorunlu olduğunu belirtir.
        //Amaç: Veritabanında veya kullanıcıdan alınan verilerde, bu özelliğin mutlaka bir değer almasını zorunlu kılmak.
        public string Password { get; set; } = string.Empty;//"string" C# dilinde bir veri türüdür ve metin (karakter dizisi) değerlerini saklamak için kullanılır.
                                                            // "get" Özelliğin değerini okumak için kullanılan bir accessor'dır. "set" özelliğin değerini yazmak (atamak) için kullanılan bir accessor'dır.
                                                            //C# dilinde property (özellik), bir sınıfın dış dünyaya sunduğu kontrollü bir erişim noktasıdır. Property'ler, bir sınıfın içindeki alanlara (fields) erişimi kontrol etmek için kullanılır. Property'ler genellikle getter ve setter accessor'ları ile tanımlanır.
                                                            //get accessor'ı ile Password özelliğinin(property) değeri okunabilir. set accessor'ı ile Password özelliğine bir değer atanabilir.
                                                            //"=" atama operatörüdür. Sağdaki değeri soldaki özelliğe atar. "string.Empty" C# dilinde, boş bir metin (karakter dizisi) değerini temsil eder.
                                                            //Amaç: Password özelliğine varsayılan olarak boş bir metin değeri atamak.

        [EmailAddress]//"[EmailAddress]" bir attribute'dur. Bu attribute, bir özelliğin (property) e-posta adresi formatında olduğunu belirtir.
        //Amaç: Veritabanında veya kullanıcıdan alınan verilerde, bu özelliğin mutlaka geçerli bir e-posta adresi formatında olmasını sağlamak.
        public string? Email { get; set; }//"string" C# dilinde bir veri türüdür ve metin (karakter dizisi) değerlerini saklamak için kullanılır.
                                          //"?(Nullable)" operatörü "Email" özelliğinin(property) null değer alabileceğini belirtir. "get" Özelliğin değerini okumak için kullanılan bir accessor'dır. "set" özelliğin değerini yazmak (atamak) için kullanılan bir accessor'dır.
                                          //C# dilinde property (özellik), bir sınıfın dış dünyaya sunduğu kontrollü bir erişim noktasıdır. Property'ler, bir sınıfın içindeki alanlara (fields) erişimi kontrol etmek için kullanılır. Property'ler genellikle getter ve setter accessor'ları ile tanımlanır.
                                          //get accessor'ı ile Email özelliğinin(property) değeri okunabilir. set accessor'ı ile Email özelliğine bir değer atanabilir.


        public DateTime CreatedDate { get; set; } = DateTime.Now;//"DateTime" C# dilinde bir veri türüdür ve tarih/saat değerlerini saklamak için kullanılır.
                                                                 // "get" Özelliğin değerini okumak için kullanılan bir accessor'dır. "set" özelliğin değerini yazmak (atamak) için kullanılan bir accessor'dır.
                                                                 //C# dilinde property (özellik), bir sınıfın dış dünyaya sunduğu kontrollü bir erişim noktasıdır. Property'ler, bir sınıfın içindeki alanlara (fields) erişimi kontrol etmek için kullanılır. Property'ler genellikle getter ve setter accessor'ları ile tanımlanır.
                                                                 //get accessor'ı ile CreatedDate özelliğinin(property) değeri okunabilir. set accessor'ı ile CreatedDate özelliğine bir değer atanabilir
                                                                 //"=" atama operatörüdür. Sağdaki değeri soldaki özelliğe atar. "DateTime.Now" C# dilinde, sistemin şu anki tarih ve saatini döndüren bir özelliktir. 
                                                                 //Amaç: CreatedDate özelliğine varsayılan olarak, nesne oluşturulduğu anda geçerli olan tarih ve saati atamak.


        public virtual ICollection<Todo>? Todos { get; set; }//"virtual" C# dilinde bir keyword'tür ve bir özelliğin veya metodun türetilmiş sınıflar tarafından geçersiz kılınabileceğini(override) belirtir.
                                                             // "get" Özelliğin değerini okumak için kullanılan bir accessor'dır. "set" özelliğin değerini yazmak (atamak) için kullanılan bir accessor'dır.
                                                             //C# dilinde property (özellik), bir sınıfın dış dünyaya sunduğu kontrollü bir erişim noktasıdır. Property'ler, bir sınıfın içindeki alanlara (fields) erişimi kontrol etmek için kullanılır. Property'ler genellikle getter ve setter accessor'ları ile tanımlanır.
                                                             //get accessor'ı ile Todos özelliğinin(property) değeri okunabilir. set accessor'ı ile Todos özelliğine bir değer atanabilir.
                                                             //"ICollection<Todo>" C# dilinde bir koleksiyon (collection) arayüzüdür. "Todo" Koleksiyonun tutacağı veri türüdür. Bu durumda, Todo sınıfına ait nesneler saklanır.
                                                             //"ICollection<Todo>" bir liste, dizi veya başka bir koleksiyon türü olabilir."?" işareti, özelliğin null değer alabileceğini belirtir.
    }
}
