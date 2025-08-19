namespace TodoMvcApp.Models;//namespace bir keyword'tür namespace burada ifade edilecek tanımların başka yerlerdeki tanımlarla karıştırılmaması ve buraya özgü olduğunu belirtmek için kullanılır. 
//
public class ErrorViewModel//public,private ve protected gibi keyword'ler önüne yazıldığı sınıf vb. seylerin erişilebilme türlerini ifade eder
                           //class ise bir nesneyi düzenlemek ve özel bir şekilde inşaa etmek için parçalarına (class'larına) bölüp bu parçalardan tekrar bir bütün elde etmemizi sağlar
{
    public string? RequestId { get; set; }//"string" C# dilinde bir veri türüdür ve metin (karakter dizisi) değerlerini saklamak için kullanılır.
                                          //"?(Nullable)" operatörü "RequestId" özelliğinin(property) null değer alabileceğini belirtir. "get" Özelliğin değerini okumak için kullanılan bir accessor'dır. "set" özelliğin değerini yazmak (atamak) için kullanılan bir accessor'dır.
                                          //C# dilinde property (özellik), bir sınıfın dış dünyaya sunduğu kontrollü bir erişim noktasıdır. Property'ler, bir sınıfın içindeki alanlara (fields) erişimi kontrol etmek için kullanılır. Property'ler genellikle getter ve setter accessor'ları ile tanımlanır.
                                          //get accessor'ı ile RequestId özelliğinin(property) değeri okunabilir. set accessor'ı ile RequestId özelliğine bir değer atanabilir.
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);//"bool" C# dilinde bir veri türüdür ve true veya false değerlerini saklamak için kullanılır.
    //C# dilinde => operatörü, lambda ifadesi veya expression-bodied member (kısa ifade gövdeli üye) tanımlamak için kullanılır. Bu operatör, bir özelliğin, metodun veya fonksiyonun kısa bir şekilde tanımlanmasını sağlar.
}//"ShowRequestId" özelliğin(property) adıdır. Bu özellik, RequestId özelliğinin boş olup olmadığını kontrol eder ve bir boolean değer döndürür.
//"string.IsNullOrEmpty" C# dilinde bir yardımcı metottur. Bir string'in null veya boş olup olmadığını kontrol eder.