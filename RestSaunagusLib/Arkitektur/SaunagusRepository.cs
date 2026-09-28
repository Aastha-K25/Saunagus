// Namespace bruges til at organisere projektets klasser
// og undgå navnekonflikter
namespace RestSaunagusLib.Arkitektur;

// Repository pattern bruges til at håndtere data. Repository fungerer som et data-lag mellem: Controller og data
// Repository styrer:
// - hente data
// - tilføje data
// - slette data
// - finde data
public class SaunagusRepository
{
    // Listen fungerer som vores midlertidige database. List bruges fordi vi skal kunne: tilføje, slette og hente flere objekter
    // static betyder at listen deles mellem alle objekte. Hvis static ikke bruges, vil listen blive nulstillet for hver request
    // private betyder at kun repository-klassen må ændre listen direkte
    private static List<Saunagus> _saunagusList;
    
    // static bruges så id fortsætter korrekt, mellem flere requests
    // private beskytter variable, så andre klasser ikke kan ændre den
    private static int _nextId = 6;     // Holder styr på næste ledige id

    // Constructor
    // Constructor kører automatis, når repository objektet oprettes
    public SaunagusRepository()
    {
        // Hvis listen endnu ikke er oprettet
        if (_saunagusList == null)
        {
            // Opretter listen med startdata
            _saunagusList = new List<Saunagus>()
            {
                // Opretter første objekt
                new Saunagus()
                {
                    Id = 1,
                    Dato = "21-12-2025",
                    Lokation = "Roskilde",
                    Antal = 10,
                    Duft = "Citron"
                },

                // Opretter andet objekt
                new Saunagus()
                {
                    Id = 2,
                    Dato = "22-12-2025",
                    Lokation = "Veddelev",
                    Antal = 8,
                    Duft = "Appelsin"
                },

                // Opretter tredje objekt
                new Saunagus()
                {
                    Id = 3,
                    Dato = "01-01-2026",
                    Lokation = "Veddelev",
                    Antal = 6,
                    Duft = "Citron"
                },

                // Opretter fjerde objekt
                new Saunagus()
                {
                    Id = 4,
                    Dato = "02-01-2026",
                    Lokation = "Roskilde",
                    Antal = 10,
                    Duft = "Sandeltræ"
                },

                // Opretter femte objekt
                new Saunagus()
                {
                    Id = 5,
                    Dato = "02-01-2026",
                    Lokation = "Veddelev",
                    Antal = 4,
                    Duft = "Lavendel"
                }
            };
        }
    }

    // Henter alle objekter fra liste, Returnerer en liste med alle Saunagus objekter
    public List<Saunagus> GetAll()
    {
        return _saunagusList;
    }

    // Finder ét objekt ud fra id. ? betyder at objektet kan være null. Returnerer null hvis objektet ikke findes
    public Saunagus? GetById(int id)
    {
        // Find søger efter første objekt, som matcher betingelsen
        // Lambda expression: s => s.Id == id betyder: find objekt hvor Id matcher
        return _saunagusList.Find(s => s.Id == id);
    }
    // Tilføjer nyt objekt til listen
    public Saunagus Add(Saunagus saunagus)
    {
        // Repository styrer id automatisk. Objektet får næste ledige id
        saunagus.Id = _nextId;
        // Går videre til næste id
        _nextId++;
        // Tilføjer objekt til listen
        _saunagusList.Add(saunagus);
        // Returnerer objektet
        return saunagus;
    }
    // Sletter objekt ud fra id
    public Saunagus? Delete(int id)
    {
        // Finder objektet i listen
        Saunagus? foundSaunagus =
            _saunagusList.Find(s => s.Id == id);

        // Hvis objektet findes
        if (foundSaunagus != null)
        {
            // Remove fjerner objektet fra listen
            _saunagusList.Remove(foundSaunagus);
        }

        // Returnerer slettet objekt eller null hvis objektet ikke findes
        return foundSaunagus;
    }

    // Finder alle arrangementer med en bestemt duft
    public List<Saunagus> GetByDuft(string duft)
    {
        // FindAll returnerer flere objekter. ToLower bruges for at gøre. søgningen case-insensitive
        // Det betyder: store og små bogstaver ignoreres
        return _saunagusList.FindAll(
            s => s.Duft.ToLower() ==
                 duft.ToLower()
        );
    }
    // Finder gennemsnit af antal pladser ud fra bestemt duft
    public double GetAverageAntalByDuft(
        string duft)
    {
        // Opretter ny liste med matchende objekter
        List<Saunagus> filteredList =
            _saunagusList.FindAll(
                s =>
                    s.Duft.ToLower() ==
                    duft.ToLower()
            );

        // Hvis listen er tom
        if (filteredList.Count == 0)
        {
            // Returnerer 0 for at undgå division med 0
            return 0;
        }
        // Variabel til samlet antal pladser
        int total = 0;
        // foreach gennemløber alle objekter i listen
        // Bruges til at lægge antal sammen
        foreach (Saunagus saunagus in filteredList)
        {
            total += saunagus.Antal;
        }
        // Returnerer gennemsnit. double bruges fordi gennemsnit. kan indeholde decimaler
        // Casting: (double) - sørger for decimal division
        return (double)total / filteredList.Count;
    }
}