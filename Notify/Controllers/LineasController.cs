﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Notify.Models;

namespace Notify.Controllers
{
    public class LineasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Lineas
        public ActionResult Index()
        {
            var linea = db.Linea.Include(l => l.pedido).Include(l => l.producto);
            return View(linea.ToList());
        }

        // GET: Lineas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Linea linea = db.Linea.Find(id);
            if (linea == null)
            {
                return HttpNotFound();
            }
            return View(linea);
        }

        // GET: Lineas/Create
        public ActionResult Create()
        {
            ViewBag.id_pedido = new SelectList(db.Pedido, "id_pedido", "id_pedido");
            ViewBag.codigo = new SelectList(db.Producto, "codigo", "nombre");
            return View();
        }

        // POST: Lineas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_linea,cantidad,codigo,id_pedido")] Linea linea)
        {
            if (ModelState.IsValid)
            {
                db.Linea.Add(linea);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_pedido = new SelectList(db.Pedido, "id_pedido", "id_pedido", linea.id_pedido);
            ViewBag.codigo = new SelectList(db.Producto, "codigo", "nombre", linea.codigo);
            return View(linea);
        }

        // GET: Lineas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Linea linea = db.Linea.Find(id);
            if (linea == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_pedido = new SelectList(db.Pedido, "id_pedido", "id_pedido", linea.id_pedido);
            ViewBag.codigo = new SelectList(db.Producto, "codigo", "nombre", linea.codigo);
            return View(linea);
        }

        // POST: Lineas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_linea,cantidad,codigo,id_pedido")] Linea linea)
        {
            if (ModelState.IsValid)
            {
                db.Entry(linea).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_pedido = new SelectList(db.Pedido, "id_pedido", "id_pedido", linea.id_pedido);
            ViewBag.codigo = new SelectList(db.Producto, "codigo", "nombre", linea.codigo);
            return View(linea);
        }

        // GET: Lineas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Linea linea = db.Linea.Find(id);
            if (linea == null)
            {
                return HttpNotFound();
            }
            return View(linea);
        }

        // POST: Lineas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Linea linea = db.Linea.Find(id);
            db.Linea.Remove(linea);
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