using System;
namespace BasicBodyMassIndexCalculator.Common.Interfaces
{
    public interface IPerson
    {
        string Name { get; set; }
        int Weight { get; set; }
        double Length { get; set; }
    }
}
