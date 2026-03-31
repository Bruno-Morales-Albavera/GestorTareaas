using Microsoft.AspNetCore.Mvc;
using ML;

namespace PL.Controllers
    {
        public class TareasController : Controller
        {
            [HttpGet]
            public IActionResult GetAll()
            {
                ML.Result result = BL.Tarea.GetAll();
                ML.Tarea model = new ML.Tarea();
                model.Tareas = new List<ML.Tarea>();

                foreach (object obj in result.Objects)
                {
                    ML.Tarea tarea = (ML.Tarea)obj;
                    model.Tareas.Add(tarea);
                }

                return View(model);
            }

            [HttpGet]
            public IActionResult Form(int? idTarea)
            {
                ML.Tarea model = new ML.Tarea();

                if (idTarea.HasValue && idTarea.Value > 0)
                {
                    ML.Result result = BL.Tarea.GetById(idTarea.Value);
                    if (result.Correct && result.Object != null)
                    {
                        model = (ML.Tarea)result.Object;
                    }
                }
                else
                {
                    model.FechaLimite = DateTime.Now;
                }

                return PartialView("Form", model);
            }

            [HttpPost]
            public IActionResult Form(ML.Tarea tarea)
            {
                ML.Result result;

                if (tarea.IdTarea == 0)
                    result = BL.Tarea.Add(tarea);
                else
                    result = BL.Tarea.Update(tarea);

                return Json(new { success = result.Correct, message = result.ErrorMessage });
            }

            [HttpGet]
            public IActionResult Delete(int idTarea)
            {
                ML.Result result = BL.Tarea.Delete(idTarea);
                return Json(new { success = result.Correct, message = result.ErrorMessage });
            }

            [HttpGet]
            public IActionResult MarcarCompletada(int idTarea)
            {
                ML.Result result = BL.Tarea.MarcarCompletada(idTarea);
                return Json(new { success = result.Correct, message = result.ErrorMessage });
            }
        }
}
