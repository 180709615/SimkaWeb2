﻿using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using APIConsume.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
//using System.Net.Mail;
using MimeKit;
using Microsoft.EntityFrameworkCore;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.Security.Cryptography;
using APIConsume.DAO;
using MailKit.Net.Smtp;
using MimeKit.Text;
using MailKit.Security;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Dynamic;

namespace APIControllers.Controllers
{

    public class HomeController : Controller
    {
        private readonly SIATMAX_SISTERContext _context;
        private IHttpContextAccessor _accessor;

        public HomeController(IHttpContextAccessor accessor, SIATMAX_SISTERContext context)
        {
            _accessor = accessor;
            _context = context;
        }

     
    

        public async Task<string> SendWa()
        {
            var accountSid = "AC0ba9ff0b818feb5f8fd74535f0c3a2a9";
            var authToken = "e8eb3dd5bd088ad8c02ce5b2a71c1dc3";
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
                new PhoneNumber("whatsapp:+6283862290039"));
            messageOptions.From = new PhoneNumber("whatsapp:+14155238886");
            messageOptions.Body = "Bapak/ Ibu telah login pada SIMKA UAJY";

            var message = MessageResource.Create(messageOptions);
            Console.WriteLine(message.Body);
            return await Task.FromResult("Email Sent Successfully!");
        }
        public async Task<string> SendEmail(string Name, string Email, string Message) 
        {

            //try
            //{
            //    // Credentials  
            //    //  var credentials = new NetworkCredential("sskkpuajy@gmail.com", "bersamasskkp");
            //    //var credentials = new NetworkCredential("testMhs@uajy.ac.id", "MhsCoba868");
            //    var credentials = new NetworkCredential("180709615@students.uajy.ac.id", "Deemust130600");

            //    // Mail message


            //    var mail = new MailMessage()
            //    {
            //        From = new MailAddress("180709615@students.uajy.ac.id"),
            //        Subject = "Informasi Login",
            //        Body = Message
            //    };
            //    mail.IsBodyHtml = true;
            //    mail.To.Add(new MailAddress("dimasprayoga2@gmail.com"));
            //    // Smtp client
            //    var client = new SmtpClient()
            //    {
            //        Port = 587,
            //        DeliveryMethod = SmtpDeliveryMethod.Network,
            //        UseDefaultCredentials = false,
            //        Host = "SMTP.Office365.com",
            //        EnableSsl = true,
            //        Credentials = credentials
            //    };
            //    client.Send(mail);
            //    return await Task.FromResult("Email Sent Successfully!");
            //}
            //catch (System.Exception e)
            //{
            //    Console.WriteLine("aaaaa");
            //    Console.WriteLine(e.Message);
            //    return e.Message;
            //}
            return ("emailsender");

        }
        
        [HttpPost]
        public IActionResult do_login(string username, string password)
        {
            
            TempData["username"] = username;
            ClaimsIdentity identity = null;
            bool isAuthenticated = false;
            string strrole = "";

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                TempData["Message"] = "";
            }
            else
            {
                var mstKaryawan = _context.MstKaryawan.FirstOrDefault(a => a.Npp == username);
                if(mstKaryawan != null) // Data Karyawan ditemukan
                {
                    if (mstKaryawan.PASSWORD_RIPEM == getHash(password))
                    {
                        strrole = "role1";
                        HttpContext.Session.SetString("NPP", username);
                        HttpContext.Session.SetString("Nama", mstKaryawan.NamaLengkapGelar);

                        isAuthenticated = true;
                        identity = new ClaimsIdentity(new[] {
                                    new Claim(ClaimTypes.Name, username),
                                    new Claim("username", username),
                                    new Claim("role", strrole),
                                    //new Claim("menu",getSidebarMenu())
                                }, CookieAuthenticationDefaults.AuthenticationScheme);
                        var getRole = (new LoginDAO()).GetUserRole(username);
                        foreach (var role in getRole) // Memasukkan list role kedalam claim
                        {
                            identity.AddClaim(new Claim(ClaimTypes.Role, role.Deskripsi));
                            identity.AddClaim(new Claim("Roles", role.Deskripsi));
                        }
                    }
                    else
                    {
                        TempData["err_message"] = "Gagal Login! Password anda salah.";
                    }
                }
                else
                {
                    TempData["err_message"] = "Data Karyawan tidak ditemukan";
                }
            }
            if (isAuthenticated)
            {
                // berhasil login
                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                // gagal login
                return RedirectToAction("Index");
            }
        }

        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //bersihan sessionn
            return RedirectToAction("Index", "Home");
        }

        

        [Authorize]
        public IActionResult Dashboard()
        {
            return View();


        }
        public IActionResult Simkadosen()
        {

            if (HttpContext.Session.GetString("NPP") != null)
                return RedirectToAction("Index", "SimkaDosen");
            else
            {
                TempData["Message"] = "Sesi Berakhir.";
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult SimkaKaryawan()
        {

            if (HttpContext.Session.GetString("NPP") != null)
                return RedirectToAction("Index", "SimkaKaryawan");
            else
            {
                TempData["Message"] = "Sesi Berakhir.";
                return RedirectToAction("Index", "Home");
            }
        }



        public IActionResult SimkaAdmin()
        {


            if (HttpContext.Session.GetString("NPP") != null)
                return RedirectToAction("Index", "SimkaAdmin");
            else
            {
                TempData["Message"] = "Sesi Berakhir.";
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult SimkaPengembangan()
        {
            return View();
        }
        public JsonResult FindKegiatan(int tridharma_id)
        {
            //Use EF core to get all colors of this Code
            var Kegiatan = _context.RefPengembangan
                  .Where(p => p.IdRefJnsAppraisal == tridharma_id)
              .Select(p => new { p.IdRefPengembangan, p.Deskripsi });

            //return SelectList with your text and value
            var kegiatan = Kegiatan.ToList();
            Console.WriteLine(kegiatan);
            return new JsonResult(kegiatan);


        }
        public async Task<IActionResult> semuadosen(string sortOrder, string searchString)
        {

            Sister_Dosen dosen = new Sister_Dosen();
            Sister_Dosen dosjadi = new Sister_Dosen();
            Sister_Token reservation = new Sister_Token();
            using (var httpClient = new HttpClient())
            {
                var akun = new Sister_Akun();
                akun.username = "GV3lhqNadhHePiwVQ5Y3Vw";
                akun.password = "5QW4jKhZ8r8QDmYMHiepjHwpH/wcfTioexezIS9AS8XYPMPnNHhEHLbfpeDsP0R8";
                akun.id_pengguna = "bd5df696-05d3-4db1-9e32-7c6be4e5ad36";
                var json = JsonConvert.SerializeObject(akun);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "https://sister.uajy.ac.id/api.php/0.1/Login";


                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.PostAsync(url, data);
                string apiResponse = await response.Content.ReadAsStringAsync();

                reservation = JsonConvert.DeserializeObject<Sister_Token>(apiResponse);

                var auten = new Sister_auth();
                auten.id_pengguna = "bd5df696-05d3-4db1-9e32-7c6be4e5ad36";
                auten.id_token = reservation.data.id_token;
                json = JsonConvert.SerializeObject(auten);
                data = new StringContent(json, Encoding.UTF8, "application/json");
                url = "https://sister.uajy.ac.id/api.php/0.1/Referensi/doseninternal";

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await httpClient.PostAsync(url, data);
                apiResponse = await response.Content.ReadAsStringAsync();


                dosen = JsonConvert.DeserializeObject<Sister_Dosen>(apiResponse);

                ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewData["FakultasSortParm"] = sortOrder == "Fakultas" ? "fakultas_desc" : "Fakultas";
                ViewData["ProgramStudiSortParam"] = sortOrder == "ProgramStudi" ? "ProgramStudi_desc" : "ProgramStudi";

                if (!String.IsNullOrEmpty(searchString))
                {
                    dosen.data = dosen.data.Where(s => s.nama_dosen.ToLower().Contains(searchString.ToLower())).ToArray();
                    return View(dosen);
                }

                switch (sortOrder)
                {
                    case "name_desc":

                        dosen.data = dosen.data.OrderByDescending(x => x.nama_dosen).ToArray();

                        dosen.data = dosen.data.Where(x => x.fakultas.Equals("TEKNIK", StringComparison.OrdinalIgnoreCase)).ToArray();


                        break;

                    case "fakultas_desc":

                        dosen.data = dosen.data.OrderByDescending(x => x.fakultas).ToArray();



                        break;
                    case "Fakultas":

                        dosen.data = dosen.data.OrderBy(x => x.fakultas).ToArray();



                        break;
                    case "ProgramStudi":

                        dosen.data = dosen.data.OrderBy(x => x.nama_program_studi).ToArray();


                        break;

                    case "ProgramStudi_desc":

                        dosen.data = dosen.data.OrderByDescending(x => x.nama_program_studi).ToArray();


                        break;

                    case "ft":
                        dosen.data = dosen.data.Where(x => x.fakultas.Equals("TEKNIK", StringComparison.OrdinalIgnoreCase)).ToArray();

                        break;
                    default:

                        dosen = JsonConvert.DeserializeObject<Sister_Dosen>(apiResponse);
                        dosen.data = dosen.data.OrderBy(x => x.nama_dosen).ToArray();


                        break;



                }
                return View(dosen);
            }


        }

        public ViewResult GetReservation() => View();

        [HttpPost]
        public async Task<IActionResult> GetReservation(int id)
        {
            Reservation reservation = new Reservation();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:8888/api/Reservation/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                }
            }
            return View(reservation);
        }
        [HttpPost]
        public async Task<IActionResult> DetailDosen(string idDosen)
        {

            using (var httpClient = new HttpClient())
            {
                Sister_DosenPenelitian dospen = new Sister_DosenPenelitian();
                Sister_Token reservation = new Sister_Token();
                PenelitianReq request = new PenelitianReq();
                var akun = new Sister_Akun();
                akun.username = "GV3lhqNadhHePiwVQ5Y3Vw";
                akun.password = "5QW4jKhZ8r8QDmYMHiepjHwpH/wcfTioexezIS9AS8XYPMPnNHhEHLbfpeDsP0R8";
                akun.id_pengguna = "bd5df696-05d3-4db1-9e32-7c6be4e5ad36";
                var json = JsonConvert.SerializeObject(akun);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "https://sister.uajy.ac.id/api.php/0.1/Login";


                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.PostAsync(url, data);
                string apiResponse = await response.Content.ReadAsStringAsync();

                reservation = JsonConvert.DeserializeObject<Sister_Token>(apiResponse);

                var DetailDos = new Sister_DetailDosenRequest();
                DetailDos.id_token = reservation.data.id_token;
                DetailDos.id_dosen = idDosen;
                Console.WriteLine(idDosen);
                json = JsonConvert.SerializeObject(DetailDos);
                data = new StringContent(json, Encoding.UTF8, "application/json");
                url = "https://sister.uajy.ac.id/api.php/0.1/Dosen/detail";

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await httpClient.PostAsync(url, data);
                apiResponse = await response.Content.ReadAsStringAsync();

                dospen.dos = (Sister_DosenDetail)JsonConvert.DeserializeObject(apiResponse, typeof(Sister_DosenDetail));


                request.id_token = reservation.data.id_token;
                request.id_dosen = idDosen;
                request.updated_after = new Updated_After();
                request.updated_after.bulan = "01";
                request.updated_after.tahun = "1990";
                request.updated_after.tanggal = "01";
                json = JsonConvert.SerializeObject(request);
                data = new StringContent(json, Encoding.UTF8, "application/json");

                url = "https://sister.uajy.ac.id/api.php/0.1/Penelitian";

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await httpClient.PostAsync(url, data);
                apiResponse = await response.Content.ReadAsStringAsync();

                dospen.pen = (Penelitian)JsonConvert.DeserializeObject(apiResponse, typeof(Penelitian));

                request.id_token = reservation.data.id_token;
                request.id_dosen = idDosen;
                request.updated_after = new Updated_After();
                request.updated_after.bulan = "01";
                request.updated_after.tahun = "1990";
                request.updated_after.tanggal = "01";
                json = JsonConvert.SerializeObject(request);
                data = new StringContent(json, Encoding.UTF8, "application/json");

                url = "https://sister.uajy.ac.id/api.php/0.1/Pengabdian";

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await httpClient.PostAsync(url, data);
                apiResponse = await response.Content.ReadAsStringAsync();
                dospen.abdi = (Pengabdian)JsonConvert.DeserializeObject(apiResponse, typeof(Pengabdian));


                url = "https://sister.uajy.ac.id/api.php/0.1/Pembicara";
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await httpClient.PostAsync(url, data);
                apiResponse = await response.Content.ReadAsStringAsync();
                dospen.bicara = (Pembicara)JsonConvert.DeserializeObject(apiResponse, typeof(Pembicara));


                url = "https://sister.uajy.ac.id/api.php/0.1/JabatanStruktural";
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await httpClient.PostAsync(url, data);
                apiResponse = await response.Content.ReadAsStringAsync();


                url = "https://sister.uajy.ac.id/api.php/0.1/JabatanStruktural/detail";
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await httpClient.PostAsync(url, data);
                apiResponse = await response.Content.ReadAsStringAsync();



                url = "https://sister.uajy.ac.id/api.php/0.1/Publikasi";
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await httpClient.PostAsync(url, data);
                apiResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine(apiResponse);
                dospen.publi = (Publikasi)JsonConvert.DeserializeObject(apiResponse, typeof(Publikasi));
                //  dospen.bicara = (Pembicara)JsonConvert.DeserializeObject(apiResponse, typeof(Pembicara));

                url = "https://sister.uajy.ac.id/api.php/0.1/Paten";
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await httpClient.PostAsync(url, data);
                apiResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine(apiResponse);

                url = "https://sister.uajy.ac.id/api.php/0.1/Pengajaran";
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await httpClient.PostAsync(url, data);
                apiResponse = await response.Content.ReadAsStringAsync();
                dospen.ajar = (Pengajaran)JsonConvert.DeserializeObject(apiResponse, typeof(Pengajaran));


                return await Task.FromResult(View(dospen));
            }
        }
        public async Task<IActionResult> pt()
        {
            string idDosen = null;

         

                using (var httpClient = new HttpClient())
                {
                try
                {
                    Sister_Token TokenSister = new Sister_Token();
                    var akun = new Sister_Akun();
                    akun.username = "GV3lhqNadhHePiwVQ5Y3Vw";
                    akun.password = "5QW4jKhZ8r8QDmYMHiepjHwpH/wcfTioexezIS9AS8XYPMPnNHhEHLbfpeDsP0R8";
                    akun.id_pengguna = "bd5df696-05d3-4db1-9e32-7c6be4e5ad36";
                    var json = JsonConvert.SerializeObject(akun);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    var url = "https://sister.uajy.ac.id/api.php/0.1/Login";
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await httpClient.PostAsync(url, data);
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("test");
                    TokenSister = (Sister_Token)JsonConvert.DeserializeObject(apiResponse, typeof(Sister_Token));
                    SisterReq request = new SisterReq();
                   // idDosen = _context.MstKaryawan.FirstOrDefault(a => a.Npp == npp).ID_DOSEN_SISTER;
                    request.id_token = TokenSister.data.id_token;
                    request.id_pengguna = "bd5df696-05d3-4db1-9e32-7c6be4e5ad36";
                    json = JsonConvert.SerializeObject(request);
                    data = new StringContent(json, Encoding.UTF8, "application/json");
                    url = "https://sister.uajy.ac.id/api.php/0.1/Referensi/pt";

                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    response = await httpClient.PostAsync(url, data);
                    apiResponse = await response.Content.ReadAsStringAsync();



                    return Json(new
                    {
                        apiResponse
                    });
                }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            
            
        }
        public async Task<IActionResult> mhs()
        {
            string idDosen = null;



            using (var httpClient = new HttpClient())
            {
                try
                {
                    Sister_Token TokenSister = new Sister_Token();
                    var akun = new Sister_Akun();
                    akun.username = "GV3lhqNadhHePiwVQ5Y3Vw";
                    akun.password = "5QW4jKhZ8r8QDmYMHiepjHwpH/wcfTioexezIS9AS8XYPMPnNHhEHLbfpeDsP0R8";
                    akun.id_pengguna = "bd5df696-05d3-4db1-9e32-7c6be4e5ad36";
                    var json = JsonConvert.SerializeObject(akun);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    var url = "https://sister.uajy.ac.id/api.php/0.1/Login";
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await httpClient.PostAsync(url, data);
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    Console.WriteLine("test");
                    TokenSister = (Sister_Token)JsonConvert.DeserializeObject(apiResponse, typeof(Sister_Token));
                    SisterReqMhs request = new SisterReqMhs();
                    // idDosen = _context.MstKaryawan.FirstOrDefault(a => a.Npp == npp).ID_DOSEN_SISTER;
                    request.id_token = TokenSister.data.id_token;
                    request.id_pengguna = "bd5df696-05d3-4db1-9e32-7c6be4e5ad36";
                    request.id_universitas = "b590f88f-2e3f-4cce-9c59-ccd7c3399b07";
                    request.id_program_studi = "";
                    request.keyword = "";
                    json = JsonConvert.SerializeObject(request);
                    data = new StringContent(json, Encoding.UTF8, "application/json");
                    url = "https://sister.uajy.ac.id/api.php/0.1//Mahasiswa";

                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    response = await httpClient.PostAsync(url, data);
                    apiResponse = await response.Content.ReadAsStringAsync();



                    return Json(new
                    {
                        apiResponse
                    });
                }
                catch (Exception)
                {
                    throw;
                }
            }


        }
        public static string getHash(string password)
        {

            Encoding enc = Encoding.GetEncoding(1252);
            RIPEMD160 ripemdHasher = RIPEMD160.Create();
            byte[] data = ripemdHasher.ComputeHash(Encoding.Default.GetBytes(password));
            string str = enc.GetString(data);

            return str;
        }
       
        [HttpPost]
        public async Task<IActionResult> do_lupaPW(string npp)
        {
            if (npp != null)
            {
                var dataDosen = _context.MstKaryawan.FirstOrDefault(a => a.Npp == npp);

                if (dataDosen != null)
                {
                    var sampleUsername = "180709615@students.uajy.ac.id";
                    var samplePassword = "Deemust130600";
                    sampleUsername = "dkmasprayoga2@gmail.com";
                    samplePassword = "dimasgamasdigam2000";
                    var uuid = Guid.NewGuid().ToString();
                    var informasilogin = "  <br><br><br> SIMKA 2 DEV <br><br>" +
                                    "Waktu Dan Tanggal:  " + DateTime.Now +
                                    
                                    "<br> Silahkan klik link di bawah ini untuk memperbarui password Anda " +
                                     //"<br><a href='https://localhost:44393/Home/ResetPasswordForm?uuid=" + uuid + "' class='btn btn-info' target='_blank'>https://localhost:44393/Home/ResetPasswordForm?uuid=" + uuid + " </a>" +
                                     "<br><a href='https://simka2-dev.uajy.ac.id/Home/ResetPasswordForm?uuid=" + uuid + "' class='btn btn-info' target='_blank'>https://simka2-dev.uajy.ac.id/Home/ResetPasswordForm?uuid=" + uuid + " </a>" +

                                    "<br>Apabila Bapak/ Ibu tidak mengenali aktivitas ini agar segera menghubungi KSI melalui " +
                                    "<a href='http://ksi.uajy.ac.id/helpdesk/open.php' target='_blank'> http://ksi.uajy.ac.id/helpdesk</a>" +
                                    "<br>Terimakasih ";

                    using var smtp = new SmtpClient();
                    try
                    {
                        //Update uuid npp tersebut biar nanti bisa di get data npp nya

                        var result = (new MstKaryawanDAO()).UbahUUID_Reset_Pswd(npp, uuid);
                        // Credentials  
                        //  var credentials = new NetworkCredential("sskkpuajy@gmail.com", "bersamasskkp");
                        //var credentials = new NetworkCredential("testMhs@uajy.ac.id", "MhsCoba868");
                        var credentials = new NetworkCredential("180709615@students.uajy.ac.id", "Deemust130600");

                        // Mail message


                        var email = new MimeMessage();
                        email.From.Add(MailboxAddress.Parse("dkmasprayoga2@gmail.com")); // Nanti diganti dengan email dari KSI
                        //email.To.Add(MailboxAddress.Parse("180709615@students.uajy.ac.id"));
                        email.To.Add(MailboxAddress.Parse("dimasprayoga2@gmail.com"));
                       //email.To.Add(MailboxAddress.Parse(dataDosen.Email));
                        email.Subject = "Lupa password";

                        var bodyBuilder = new BodyBuilder();
                        bodyBuilder.HtmlBody = informasilogin;  
                        email.Body = bodyBuilder.ToMessageBody();


                        var smtpProvider =""; // Menentukan Provider SMTP
                        //if (sampleUsername.Contains("gmail"))
                        //    smtpProvider = "smtp.office365.com"; // Kalau Email asal menggunakan Outlook
                        //else
                            smtpProvider = "smtp.gmail.com";


                        // send email

                        smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                        smtp.Connect(smtpProvider, 587, SecureSocketOptions.StartTls);
                        smtp.Authenticate(sampleUsername,samplePassword);
                        await smtp.SendAsync(email);

                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine("aaaaa");
                        Console.WriteLine(e.Message);

                        TempData["err_message"] = e.Message;
                        TempData["success"] = false;
                    }
                    finally
                    {
                        smtp.Disconnect(true);
                        smtp.Dispose();
                    }


                    string emailDosen = dataDosen.Email;
                    string pattern = @"(?<=[\w]{2})[\w-\._\+%]*(?=[\w]{1}@)";
                    string hideEmail = Regex.Replace(emailDosen, pattern, m => new string('*', m.Length));

                    TempData["err_message"] = "Silahkan cek email Anda " + hideEmail;
                    TempData["success"] = true;
                   

                }
                else
                {
                    TempData["success"] = false;
                    TempData["err_message"] = "Data tidak ditemukan";

                }
            }

            return RedirectToAction("LupaPassword");
            
            
        }
        public IActionResult LupaPassword()
        {
            return View();
        }
        public IActionResult ResetPasswordForm(string uuid)
        {
            if (uuid != null)
            {
                var dataDosen = _context.MstKaryawan.FirstOrDefault(a => a.UUID_LUPA_PWD == uuid);

                if (dataDosen != null)
                {
                    TempData["isLinkValid"] = true;

                    HttpContext.Session.SetString("NPPResetPassword", dataDosen.Npp);
                    var model = new ResetPasswordForm();
                    return View(model);

                }
                else
                {
                    TempData["isLinkValid"] = false;
                    TempData["Message"] = "Link tidak valid.";
                    TempData["alert"] = "<script>alert('Link tidak valid.');window.location.replace('/Home')</script>";

                    return View();

                    //return RedirectToAction("Index", "Home");
                }


            }
            else
            {
                TempData["isLinkValid"] = false;
                TempData["Message"] = "Link tidak valid.";
                TempData["alert"] = "<script>alert('Link tidak valid');location.href='/Home'</script>";
                return View();
                //return RedirectToAction("Index", "Home");
            }
            //var model = new ResetPasswordForm();
            //return View(model);


        }

        [HttpPost]
        public IActionResult ResetPasswordForm(ResetPasswordForm model)
        {
            TempData["isLinkValid"] = true; // Kasih tau kalau uuid udah valid jadi NPP yang mau reset password udah masuk di session

            if (ModelState.IsValid)
            {

                var npp = HttpContext.Session.GetString("NPPResetPassword");
                var data = _context.MstKaryawan.FirstOrDefault(a => a.Npp == npp);
                if (data != null)
                {
                    try
                    {
                        var result =  (new MstKaryawanDAO()).UbahPassword(npp, model.passwordBaru, getHash(model.passwordBaru));
                        var update = (new MstKaryawanDAO()).UbahUUID_Reset_Pswd(npp, null);

                        TempData["isLinkValid"] = true;

                        TempData["SuccessMessage"] = "Berhasil";
                        TempData["alert"] = "<script>alert('Reset Password Berhasil');window.location.replace('/Home')</script>";
                        return View(model);

                    }
                    catch (Exception ex)
                    {
                        TempData["alert"] = "<script>alert('"+ex.Message+"');</script>";
                        return View(model);
                    }
                }

            }
            //var a = model;
            return View(model);
        }

       
        public IActionResult _LayoutSidebar()
        {
            return View();
        }

        
    }



}

       

      
       

       /* 
        public ViewResult AddReservation() => View();

        [HttpPost]
        public async Task<IActionResult> AddReservation(Reservation reservation)
        {
            Reservation receivedReservation = new Reservation();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                StringContent content = new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:8888/api/Reservation", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    try
                    {
                        receivedReservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                    }
                    catch (Exception)
                    {
                        ViewBag.Result = apiResponse;
                        return View();
                    }
                }
            }
            return View(receivedReservation);
        }

        public async Task<IActionResult> UpdateReservation(int id)
        {
            Reservation reservation = new Reservation();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:8888/api/Reservation/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                }
            }
            return View(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReservation(Reservation reservation)
        {
            Reservation receivedReservation = new Reservation();
            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(reservation.Id.ToString()), "Id");
                content.Add(new StringContent(reservation.Name), "Name");
                content.Add(new StringContent(reservation.StartLocation), "StartLocation");
                content.Add(new StringContent(reservation.EndLocation), "EndLocation");

                using (var response = await httpClient.PutAsync("http://localhost:8888/api/Reservation", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedReservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                }
            }
            return View(receivedReservation);
        }

        public async Task<IActionResult> UpdateReservationPatch(int id)
        {
            Reservation reservation = new Reservation();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:8888/api/Reservation/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                }
            }
            return View(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReservationPatch(int id, Reservation reservation)
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost:8888/api/Reservation/" + id),
                    Method = new HttpMethod("Patch"),
                    Content = new StringContent("[{ \"op\": \"replace\", \"path\": \"Name\", \"value\": \"" + reservation.Name + "\"},{ \"op\": \"replace\", \"path\": \"StartLocation\", \"value\": \"" + reservation.StartLocation + "\"}]", Encoding.UTF8, "application/json")
                };

                var response = await httpClient.SendAsync(request);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReservation(int ReservationId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:8888/api/Reservation/" + ReservationId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult AddFile() => View();

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile file)
        {
            string apiResponse = "";
            using (var httpClient = new HttpClient())
            {
                using (var fileStream = file.OpenReadStream())
                {
                    var form = new MultipartFormDataContent();
                    form.Add(new StreamContent(fileStream), "file", file.FileName);

                    using (var response = await httpClient.PostAsync("http://localhost:8888/api/Reservation/UploadFile", form))
                    {
                        response.EnsureSuccessStatusCode();
                        apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            return View((object)apiResponse);
        }
    }
}*/
