using DL;
using ML;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
namespace BL
{
    public class Tarea
    {
        public static Result GetAll()
        {
            Result result = new Result();

            try
            {
                using (GestorTareasContext context = new GestorTareasContext())
                {
                    List<DL.Tarea> query = context.Tareas.ToList();

                    result.Objects = new List<object>();

                    foreach (DL.Tarea obj in query)
                    {
                        ML.Tarea tarea = new ML.Tarea
                        {
                            IdTarea = obj.IdTarea,
                            Titulo = obj.Titulo,
                            Descripcion = obj.Descripcion,
                            FechaCreacion = obj.FechaCreacion,
                            FechaLimite = obj.FechaLimite,
                            Prioridad = obj.Prioridad,
                            Estatus = obj.Estatus
                        };

                        result.Objects.Add(tarea);
                    }

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static Result GetById(int idTarea)
        {
            Result result = new Result();

            try
            {
                using (GestorTareasContext context = new GestorTareasContext())
                {
                    DL.Tarea? obj = context.Tareas.SingleOrDefault(t => t.IdTarea == idTarea);

                    if (obj != null)
                    {
                        ML.Tarea tarea = new ML.Tarea
                        {
                            IdTarea = obj.IdTarea,
                            Titulo = obj.Titulo,
                            Descripcion = obj.Descripcion,
                            FechaCreacion = obj.FechaCreacion,
                            FechaLimite = obj.FechaLimite,
                            Prioridad = obj.Prioridad,
                            Estatus = obj.Estatus
                        };

                        result.Object = tarea;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Tarea no encontrada.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static Result Add(ML.Tarea tarea)
        {
            Result result = new Result();

            try
            {
                if (tarea.FechaLimite < DateTime.Now)
                {
                    result.Correct = false;
                    result.ErrorMessage = "La fecha límite no puede ser menor a la fecha actual.";
                    return result;
                }

                using (GestorTareasContext context = new GestorTareasContext())
                {
                    DL.Tarea nueva = new DL.Tarea
                    {
                        Titulo = tarea.Titulo,
                        Descripcion = tarea.Descripcion,
                        FechaCreacion = DateTime.Now,
                        FechaLimite = tarea.FechaLimite,
                        Prioridad = tarea.Prioridad,
                        Estatus = tarea.Estatus
                    };

                    context.Tareas.Add(nueva);
                    context.SaveChanges();

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static Result Update(ML.Tarea tarea)
        {
            Result result = new Result();

            try
            {
                using (GestorTareasContext context = new GestorTareasContext())
                {
                    DL.Tarea? obj = context.Tareas.SingleOrDefault(t => t.IdTarea == tarea.IdTarea);

                    if (obj != null)
                    {
                        if (obj.Estatus == "Completada")
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se puede editar una tarea completada.";
                            return result;
                        }

                        if (tarea.FechaLimite < DateTime.Now)
                        {
                            result.Correct = false;
                            result.ErrorMessage = "La fecha límite no puede ser menor a la fecha actual.";
                            return result;
                        }

                        obj.Titulo = tarea.Titulo;
                        obj.Descripcion = tarea.Descripcion;
                        obj.FechaLimite = tarea.FechaLimite;
                        obj.Prioridad = tarea.Prioridad;
                        obj.Estatus = tarea.Estatus;

                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Tarea no encontrada.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static Result Delete(int idTarea)
        {
            Result result = new Result();

            try
            {
                using (GestorTareasContext context = new GestorTareasContext())
                {
                    DL.Tarea? obj = context.Tareas.SingleOrDefault(t => t.IdTarea == idTarea);

                    if (obj != null)
                    {
                        context.Tareas.Remove(obj);
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Tarea no encontrada.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static Result MarcarCompletada(int idTarea)
        {
            Result result = new Result();

            try
            {
                using (GestorTareasContext context = new GestorTareasContext())
                {
                    DL.Tarea? obj = context.Tareas.SingleOrDefault(t => t.IdTarea == idTarea);

                    if (obj != null)
                    {
                        obj.Estatus = "Completada";
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Tarea no encontrada.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
