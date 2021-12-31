using APIConsume.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace SimkaUAJY.Controllers
{
    public class ViewDokumenController : Controller
    {
        private readonly SIATMAX_SISTERContext _context;
        private readonly DATA_SISTERContext _DATA_SISTERcontext;
        private string baseUrl = "https://sister.uajy.ac.id/ws.php/1.0";


        public ViewDokumenController(SIATMAX_SISTERContext context, DATA_SISTERContext contexData_Sister)
        {
            _DATA_SISTERcontext = contexData_Sister;

            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewDokumenProsiding(string jenis_tabel, string id)
        {
            dynamic obj = new ExpandoObject();
            if (id != null)
            {
                obj.id = id;
                obj.jenis_tabel = jenis_tabel;

                return View(obj);
            }
            return View();
        }

        public IActionResult GetDokumenProsiding(string jenis_tabel, string id)
        {

            if (jenis_tabel == "Publikasi")
            {
                TrPublikasi_DATA_SISTER obj = _DATA_SISTERcontext.TrPublikasi_DATA_SISTER.SingleOrDefault(a => a.id_riwayat_publikasi_paten == id);

                return Json(Convert.ToBase64String(obj.FILE_PROSIDING_ARTIKEL));

            }


            //Response.Headers.Add("X-Content-Type-Options", "nosniff");
            //   Response.Headers.Add("Content-Disposition", "inline");
            //Response.Headers.Add("content-length", obj.FILE_PROSIDING_ARTIKEL.Length.ToString());

            //return File(obj.FILE_PROSIDING_ARTIKEL, "application/pdf", "FileProsiding.pdf");
            return RedirectToAction("Index", "Home");

        }

        public IActionResult ViewDokumenSimilaritas(string jenis_tabel, string id)
        {
            dynamic obj = new ExpandoObject();
            if (id != null)
            {
                obj.id = id;
                obj.jenis_tabel = jenis_tabel;

                return View(obj);
            }
            return View();
        }

        public IActionResult GetDokumenSimilaritas(string jenis_tabel, string id)
        {

            if (jenis_tabel == "Publikasi")
            {
                TrPublikasi_DATA_SISTER obj = _DATA_SISTERcontext.TrPublikasi_DATA_SISTER.SingleOrDefault(a => a.id_riwayat_publikasi_paten == id);

                return Json(Convert.ToBase64String(obj.FILE_CEK_SIMILARITAS));

            }


            //Response.Headers.Add("X-Content-Type-Options", "nosniff");
            //   Response.Headers.Add("Content-Disposition", "inline");
            //Response.Headers.Add("content-length", obj.FILE_PROSIDING_ARTIKEL.Length.ToString());

            //return File(obj.FILE_PROSIDING_ARTIKEL, "application/pdf", "FileProsiding.pdf");
            return RedirectToAction("Index", "Home");

        }
        public IActionResult ViewDokumenPAK(string jenis_tabel, string id)
        {
            dynamic obj = new ExpandoObject();
            if (id != null)
            {
                obj.id = id;
                obj.jenis_tabel = jenis_tabel;

                return View(obj);
            }
            return View();
        }

        public IActionResult GetDokumenPAK(string jenis_tabel, string id)
        {

            if (jenis_tabel == "Publikasi")
            {
                TrPublikasi_DATA_SISTER obj = _DATA_SISTERcontext.TrPublikasi_DATA_SISTER.SingleOrDefault(a => a.id_riwayat_publikasi_paten == id);

                return Json(Convert.ToBase64String(obj.FILE_PEER_REVIEW_PAK));

            }


            //Response.Headers.Add("X-Content-Type-Options", "nosniff");
            //   Response.Headers.Add("Content-Disposition", "inline");
            //Response.Headers.Add("content-length", obj.FILE_PROSIDING_ARTIKEL.Length.ToString());

            //return File(obj.FILE_PROSIDING_ARTIKEL, "application/pdf", "FileProsiding.pdf");
            return RedirectToAction("Index", "Home");

        }
        public IActionResult ViewDokumenKorespondensi(string jenis_tabel, string id)
        {
            dynamic obj = new ExpandoObject();
            if (id != null)
            {
                obj.id = id;
                obj.jenis_tabel = jenis_tabel;

                return View(obj);
            }
            return View();
        }

        public IActionResult GetDokumenKorespondensi(string jenis_tabel, string id)
        {

            if (jenis_tabel == "Publikasi")
            {
                TrPublikasi_DATA_SISTER obj = _DATA_SISTERcontext.TrPublikasi_DATA_SISTER.SingleOrDefault(a => a.id_riwayat_publikasi_paten == id);

                return Json(Convert.ToBase64String(obj.FILE_PEER_KORESPONDENSI_REVIEWER));

            }


            //Response.Headers.Add("X-Content-Type-Options", "nosniff");
            //   Response.Headers.Add("Content-Disposition", "inline");
            //Response.Headers.Add("content-length", obj.FILE_PROSIDING_ARTIKEL.Length.ToString());

            //return File(obj.FILE_PROSIDING_ARTIKEL, "application/pdf", "FileProsiding.pdf");
            return RedirectToAction("Index", "Home");

        }
    }
}
