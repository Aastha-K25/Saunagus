// Namespace bruges til at organisere koden i projektet
// Det hjælper med at holde klasser samlet i logiske grupper
namespace RestSaunagusLib.Arkitektur;

// Modelklasse  - Klassen beskriver hvordan et Saunagus objekt ser ud

// En model bruges til at repræsentere data i systemet. Klassen fungerer som en skabelon for objekter
public class Saunagus
{
    // Property til unikt id - int bruges fordi id er et heltal
    public int Id { get; set; }

    // Property til dato for arrangementet
    // string bruges fordi datoen gemmes som tekst - og ikke som DateTime i denne opgave
    public string Dato { get; set; }

    // Property til lokation  - Fortæller hvor arrangementet afholdes
    public string Lokation { get; set; }

    // Property til antal deltagere/pladser  - int bruges fordi antal er et numerisk heltal
    public int Antal { get; set; }

    // Property til duft - Fortæller hvilken duft der bruges
    public string Duft { get; set; }

    // Override betyder at vi overskriver
    // standard ToString() metoden fra object-klassen

    // ToString bruges til at vise objektet som tekst
    // fx i console eller debugging
    public override string ToString()
    {
        // Returnerer alle værdier samlet som tekst
        // " " bruges til at lave mellemrum så teksten bliver mere læsbar
        return Id + " " + Dato + " " + Lokation + " " + Antal + " " + Duft;
    }
}

// get betyder:
// læs værdien
// set betyder:
// ændr værdien