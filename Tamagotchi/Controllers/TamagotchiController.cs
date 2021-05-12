using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tamagotchi.Models;

namespace Tamagotchi.Controllers
{
  public class TamagotchiController : Controller
  {
    [HttpGet("/tamagotchis")]
    public ActionResult Index()
    {
      List<IndividualTamagotchi> allPets = IndividualTamagotchi.GetAll();
      return View(allPets);
    }
    [HttpGet("/tamagotchis/new")]
    public ActionResult New()
    {
      return View();
    }
    [HttpPost("/tamagotchis")]
    public ActionResult Create(string name)
    {
      IndividualTamagotchi newPet = new IndividualTamagotchi(name);
      return View(RedirectToAction("Index"));
    }
    [HttpPost("/tamagotchis/delete")]
    public ActionResult DeleteAll()
    {
      IndividualTamagotchi.ClearAll();
      return View();
    }
    [HttpGet("/tamagotchis/{id}")]
    public ActionResult Show(int id)
    {
      IndividualTamagotchi foundPet = IndividualTamagotchi.Find(id);
      return View(foundPet);
    }
  }
}