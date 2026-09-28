// Importerer ASP.NET Core MVC biblioteket

// Indeholder funktioner til:
// - controllers
// - routing
// - HTTP responses
using Microsoft.AspNetCore.Mvc;

// Importerer repository og model klasser
using RestSaunagusLib.Arkitektur;

// Namespace bruges til at organisere controller klasser
namespace RestSaunagusAPI.Controllers;

// ApiController fortæller ASP.NET Core at klassen er en REST API controller

// Giver automatisk:
// - model binding
// - JSON responses
// - HTTP fejl håndtering
[ApiController]

// Route bestemmer URL'en til controlleren
// [controller] bliver automatisk erstattet med controller navnet
// SaunagusController bliver derfor: api/saunagus
[Route("api/[controller]")]
public class SaunagusController : ControllerBase
{
    // Repository objekt som håndterer data
    // readonly betyder at variablen kun kan ændres i constructor
    // Controlleren bruger repository til at kommunikere med data-laget
    private readonly SaunagusRepository _repository;

    // Constructor
    // Kører automatisk når controlleren oprettes
    public SaunagusController()
    {
        // Opretter repository objekt
        _repository = new SaunagusRepository();
    }

    // --------------------------------------------------
    // HttpGet betyder:endpoint reagerer på GET requests
    // Endpoint: GET api/saunagus
    // Henter alle saunagus arrangementer
    [HttpGet]
    public ActionResult<List<Saunagus>> GetAll()
    {
        // Ok() returnerer: HTTP statuskode 200+ JSON data

        return Ok(_repository.GetAll());
    }

    // --------------------------------------------------
    // GET api/saunagus/1. {id} betyder: parameter sendes via URL
    // Finder ét objekt ud fra id
    [HttpGet("{id}")]
    public ActionResult<Saunagus> GetById(int id)
    {
        // Kalder repository metode for at finde objektet
        Saunagus? foundSaunagus =
            _repository.GetById(id);

        // Hvis objektet ikke findes
        if (foundSaunagus == null)
        {
            // Returnerer HTTP statuskode 404
            return NotFound();
        }
        // Returnerer:statuskode 200+ objekt som JSON
        return Ok(foundSaunagus);
    }

    // --------------------------------------------------
    // HttpPost bruges til at oprette data. Endpoint: POST api/saunagus
    [HttpPost]
    public ActionResult<Saunagus> Add(
        Saunagus saunagus)
    {
        // Kalder repository metode som tilføjer objektet
        Saunagus createdSaunagus =
            _repository.Add(saunagus);

        // Created returnerer: HTTP statuskode 201 + URL til nyt objekt + objekt som JSON

        return Created(
            "/api/saunagus/" +
            createdSaunagus.Id,

            createdSaunagus
        );
    }

    // --------------------------------------------------
    // HttpDelete bruges til at slette data. Endpoint: DELETE api/saunagus/1
    [HttpDelete("{id}")]
    public ActionResult<Saunagus> Delete(
        int id)
    {
        // Finder og sletter objekt
        Saunagus? deletedSaunagus =
            _repository.Delete(id);

        // Hvis objektet ikke findes
        if (deletedSaunagus == null)
        {
            // Returnerer statuskode 404
            return NotFound();
        }

        // Returnerer slettet objekt+ statuskode 200
        return Ok(deletedSaunagus);
    }

    // --------------------------------------------------

    // Special endpoint til filtering. Endpoint: GET api/saunagus/duft/citron
    // Finder alle arrangementermed bestemt duft
    [HttpGet("duft/{duft}")]
    public ActionResult<List<Saunagus>>
        GetByDuft(string duft)
    {
        // Kalder repository metode
        List<Saunagus> foundSaunagus =
            _repository.GetByDuft(duft);

        // Returnerer liste som JSON
        return Ok(foundSaunagus);
    }

    // --------------------------------------------------

    // Special endpoint til beregning. Endpoint: GET api/saunagus/average/citron
    // Returnerer gennemsnit ud fra bestemt duft
    [HttpGet("average/{duft}")]
    public ActionResult<double>
        GetAverageAntalByDuft(
            string duft)
    {
        // Kalder repository metode som beregner gennemsnit
        double average =
            _repository
                .GetAverageAntalByDuft(
                    duft
                );

        // Returnerer gennemsnittet som JSON
        return Ok(average);
    }
}