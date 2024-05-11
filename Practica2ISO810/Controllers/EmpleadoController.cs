using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Practica2ISO810.Models;

namespace Practica2ISO810.Controllers
{
    public class EmpleadoController : Controller
    {
        private EmpleadosDBEntities db = new EmpleadosDBEntities();

        // GET: Empleado
        public ActionResult Index()
        {
            return View(db.Empleados.ToList());
        }

        public ActionResult GenerarArchivo()
        {
            // Recuperar los datos de los residentes desde la base de datos
            var empleados = db.Empleados.ToList();
            // Generar el contenido del archivo de texto
            var stringBuilder = new StringBuilder();
            foreach (var empleado in empleados)
            {
                stringBuilder.AppendLine($"ID: {empleado.IDEmpleado}");
                stringBuilder.AppendLine($"Nombre Completo: {empleado.NombreCompleto}");
                stringBuilder.AppendLine($"Cedula: {empleado.Cedula}");
                stringBuilder.AppendLine($"Salario: {empleado.Salario}");
                stringBuilder.AppendLine($"Departamento: {empleado.Departamento}");
                stringBuilder.AppendLine($"Cargo: {empleado.Cargo}");
                stringBuilder.AppendLine($"Salario: {empleado.Salario}");
                stringBuilder.AppendLine($"Fecha De Inicio: {empleado.FechaDeInicio}");
                stringBuilder.AppendLine($"Fecha de Nacimiento: {empleado.FechaDeNacimiento}");
            }
            var contenidoArchivo = stringBuilder.ToString();
            // Escribir el contenido en un archivo
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "C:\\Users\\Jhonson\\Downloads\\Empleado.txt");
            System.IO.File.WriteAllText(filePath, contenidoArchivo);
            // Devolver el archivo para su descarga
            byte[] archivoBytes = System.IO.File.ReadAllBytes(filePath);
            return File(archivoBytes, "text/plain", "Empleado.txt");
        }


        // GET: Empleado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: Empleado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empleado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDEmpleado,NombreCompleto,Cedula,Departamento,Cargo,Salario,FechaDeInicio,FechaDeNacimiento")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Empleados.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(empleado);
        }

        // GET: Empleado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDEmpleado,NombreCompleto,Cedula,Departamento,Cargo,Salario,FechaDeInicio,FechaDeNacimiento")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empleado);
        }

        // GET: Empleado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleado empleado = db.Empleados.Find(id);
            db.Empleados.Remove(empleado);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}




