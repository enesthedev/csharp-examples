using System;
namespace BasicBodyMassIndexCalculator.Common.Interfaces
{
    /*
        Dersde gördük mü bilmiyorum ancak sınıfın değerlerinin doğrulanabilmesi için bir aracı yazmam gerekiyordu aksi halde
        obje tabanlı programlamanın prensiblerine ters bir işlem yapılmış olucaktı.

        Bu kod olmasa yine sınıfı yaratabilir ve kodu çalıştırabilirdik ancak best-practise yani yapılması gerektiği gibi olmamış olurdu.
        ayriyetten person'ın dışında bir çalışan sınıfı gibi insanların bilgilerinin depolanması gerekeceği sınıflar da olmuş olsaydı IPerson yerine IHuman diyip diğer sınıfları
        buradan türetebilirdik.
    */
    public interface IPerson
    {
        string Name { get; set; }
        int Weight { get; set; }
        double Length { get; set; }
    }
}
